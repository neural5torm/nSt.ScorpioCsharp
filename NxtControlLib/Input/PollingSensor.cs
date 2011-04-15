using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Robotics.Lego;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using nSt.NxtControlLib.Exceptions;

namespace nSt.NxtControlLib.Input
{
    // TODOlater: add forSeconds param like in Walk() methods (and possibly, raise Stop events)
    public abstract class PollingSensor<TSensorVal> : IDisposable
        where TSensorVal : struct
    {
        protected const ushort MAX_NORMALIZED = 1023;

        public virtual string Name { get { return GetType().Name; } }

        protected INxtBrick Brick { get; private set; }
        public NxtBrick.Sensor Sensor { get; private set; }

        protected Task SensorPoller { get; private set; }
        private volatile CancellationTokenSource cancellation;

        private volatile IList<TSensorVal> sensorValues;

        /// <summary>
        /// Time resolution between two pollings
        /// </summary>
        public TimeSpan TimeRes { get; protected set; }
        public readonly TimeSpan DefaultTimeRes = TimeSpan.FromMilliseconds(100);

        /// <summary>
        /// Value resolution: finest value change (in natural number percentage) for firing an OnChange event
        /// (any value resolution less than 1 means all changes are notified)
        /// </summary>
        public int ValResPercent { get; protected set; }
        public readonly int DefaultValRes = 10;

        public abstract TSensorVal MaxSensorVal { get; }
        public abstract TSensorVal MinSensorVal { get; }

        public delegate void SensorEventHandler(object sender, SensorEventArgs<TSensorVal> e);
        public event SensorEventHandler OnChange;



        public PollingSensor(INxtBrick nxtBrick, NxtBrick.Sensor sensor)
        {
            Brick = nxtBrick;
            Sensor = sensor;

            TimeRes = DefaultTimeRes;
            ValResPercent = DefaultValRes;
        }

        public abstract void InitSensor();


        public abstract bool GetValue(out TSensorVal value);


        /// <summary>
        /// Starts retrieving all sensor values (no value filtering)
        /// </summary>
        public void StartGettingValues() { StartGettingValues(TimeRes); }

        public void StartGettingValues(TimeSpan timeRes)
        {
            if (IsSensing())
                return; // TODOlater throw exception?

            InitSensor();

            TimeRes = timeRes;
            cancellation = new CancellationTokenSource();
            sensorValues = new List<TSensorVal>();

            SensorPoller = Task.Factory.StartNew(() =>
                           {
                               try
                               {
                                   for (; ; )
                                   {
                                       TSensorVal value;
                                       if (GetValue(out value))
                                           sensorValues.Add(value);

                                       Thread.Sleep(TimeRes);
                                       cancellation.Token.ThrowIfCancellationRequested();
                                   }
                               }
                               catch (OperationCanceledException)
                               {
                                   Debug.WriteLine(Name + " sensor values retrieving task interrupted.");
                               }
                               catch (Exception ex)
                               {
                                   Debug.WriteLine(string.Format("{0}\n{1}", ex.Message, ex.Source));
                               }
                           },
                           cancellation.Token);
        }

        public IList<TSensorVal> StopGettingValues()
        {
            if (!IsSensing() || sensorValues == null)
                throw new NxtControlException(Name, "Could not stop getting: no list of sensor values being stored or incompatible event-driven sensing task in progress");

            cancellation.Cancel();
            SensorPoller.Wait(TimeRes.Add(TimeRes));
            SensorPoller = null;

            var retValues = sensorValues;
            sensorValues = null;
            return retValues;
        }


        public void StartSensing() { StartSensing(TimeRes); }

        public void StartSensing(TimeSpan timeRes) { StartSensing(timeRes, ValResPercent); }

        public void StartSensing(int valRes) { StartSensing(TimeRes, valRes); }

        public void StartSensing(TimeSpan timeRes, int valRes)
        {
            if (IsSensing())
                return; // TODOlater throw exception? 

            InitSensor();

            // Store the default TimeRes as set by the passed parameter
            TimeRes = timeRes;
            ValResPercent = valRes;
            cancellation = new CancellationTokenSource();

            SensorPoller = Task.Factory.StartNew(() =>
                           {
                               try
                               {
                                   TSensorVal previousVal = default(TSensorVal);
                                   TSensorVal sensorVal;

                                   for (; ; )
                                   {
                                       DateTime preDate = DateTime.Now;

                                       var ok = GetValue(out sensorVal);                                       
                                       if (ok && ValueChanged(previousVal, sensorVal))
                                       {
                                           RaiseEventOnMainThread(OnChange, this, new SensorEventArgs<TSensorVal>(sensorVal));
                                           previousVal = sensorVal;
                                       }

                                       // sleep for the necessary amount of time
                                       DateTime postDate = DateTime.Now;
                                       TimeSpan timeResRemainder = TimeRes - (postDate - preDate);
                                       Thread.Sleep((int)Math.Max(timeResRemainder.TotalMilliseconds, DefaultTimeRes.TotalMilliseconds));                                       

                                       cancellation.Token.ThrowIfCancellationRequested();
                                   }
                               }
                               catch (OperationCanceledException)
                               {
                                   Debug.WriteLine(Name + " poller interrupted.");
                               }
                               catch (Exception ex)
                               {
                                   Debug.WriteLine(string.Format("{0}\n{1}", ex.Message, ex.Source));
                               }
                           },
                           cancellation.Token);
        }

        protected abstract bool ValueChanged(TSensorVal previousVal, TSensorVal newVal);

        /// <summary>
        /// By itowlson, http://stackoverflow.com/questions/1698889/raise-events-in-net-on-the-main-ui-thread
        /// </summary>
        /// <param name="event"></param>
        /// <param name="args"></param>
        private void RaiseEventOnMainThread(Delegate @event, params object[] args)
        {
            if (@event == null)
                return;

            foreach (Delegate d in @event.GetInvocationList())
            {
                ISynchronizeInvoke syncer = d.Target as ISynchronizeInvoke;
                if (syncer == null)
                {
                    d.DynamicInvoke(args);
                }
                else
                {
                    syncer.BeginInvoke(d, args);  // cleanup omitted
                }
            }
        }

        public bool IsSensing() { return SensorPoller != null; }

        public void StopSensing()
        {
            if (!IsSensing() || sensorValues != null)
                throw new NxtControlException(Name, "Could not stop sensing: no event-driven sensing task started or incompatible list of sensor values storing task in progress");

            cancellation.Cancel();
            SensorPoller.Wait(TimeRes.Add(TimeRes));
            SensorPoller = null;
        }



        public void Dispose()
        {
            StopSensing();
            foreach (var d in OnChange.GetInvocationList())
                OnChange -= d as SensorEventHandler;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
