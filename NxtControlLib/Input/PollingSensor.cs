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
    public abstract class PollingSensor<TSensorValue> : IDisposable
        where TSensorValue : struct
    {
        protected const ushort MAX_NORMALIZED = 1023;

        public virtual string Name { get { return GetType().Name; } }

        protected INxtBrick Brick { get; private set; }
        public NxtBrick.Sensor Sensor { get; private set; }

        protected Task SensorPoller { get; private set; }
        private volatile CancellationTokenSource cancellation;

        private volatile IList<TSensorValue> sensorValues;

        /// <summary>
        /// Time resolution between two pollings
        /// </summary>
        public TimeSpan TimeResolution { get; protected set; }
        public readonly TimeSpan DefaultTimeResolution = TimeSpan.FromMilliseconds(100);

        /// <summary>
        /// Value resolution: finest value change (in natural number percentage) for firing an OnChange event
        /// (any value resolution less than 1 means all changes are notified)
        /// </summary>
        public int ValueResolutionPercentage { get; protected set; }
        public readonly int DefaultValueResolution = 10;

        public abstract TSensorValue MaxSensorValue { get; }
        public abstract TSensorValue MinSensorValue { get; }

        public delegate void SensorEventHandler(object sender, SensorEventArgs<TSensorValue> e);
        public event SensorEventHandler OnChange;



        public PollingSensor(INxtBrick nxtBrick, NxtBrick.Sensor sensor)
        {
            Brick = nxtBrick;
            Sensor = sensor;

            TimeResolution = DefaultTimeResolution;
            ValueResolutionPercentage = DefaultValueResolution;
        }

        public abstract void InitSensor();


        public abstract bool GetValue(out TSensorValue value);


        /// <summary>
        /// Starts retrieving all sensor values (no value filtering)
        /// </summary>
        public void StartGettingValues() { StartGettingValues(TimeResolution); }

        public void StartGettingValues(TimeSpan timeResolution)
        {
            if (IsSensing())
                return; // TODOlater throw exception?

            InitSensor();

            TimeResolution = timeResolution;
            cancellation = new CancellationTokenSource();
            sensorValues = new List<TSensorValue>();

            SensorPoller = Task.Factory.StartNew(() =>
                           {
                               try
                               {
                                   for (; ; )
                                   {
                                       TSensorValue value;
                                       if (GetValue(out value))
                                           sensorValues.Add(value);

                                       Thread.Sleep(TimeResolution);
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

        public IList<TSensorValue> StopGettingValues()
        {
            if (!IsSensing() || sensorValues == null)
                throw new NxtControlException(Name, "Could not stop getting: no list of sensor values being stored or incompatible event-driven sensing task in progress");

            cancellation.Cancel();
            SensorPoller.Wait(TimeResolution.Add(TimeResolution));
            SensorPoller = null;

            var retValues = sensorValues;
            sensorValues = null;
            return retValues;
        }


        public void StartSensing() { StartSensing(TimeResolution); }

        public void StartSensing(TimeSpan timeResolution) { StartSensing(timeResolution, ValueResolutionPercentage); }

        public void StartSensing(int valueResolution) { StartSensing(TimeResolution, valueResolution); }

        public void StartSensing(TimeSpan timeResolution, int valueResolution)
        {
            if (IsSensing())
                return; // TODOlater throw exception? 

            InitSensor();

            // Store the default TimeRes as set by the passed parameter
            TimeResolution = timeResolution;
            ValueResolutionPercentage = valueResolution;
            cancellation = new CancellationTokenSource();

            SensorPoller = Task.Factory.StartNew(() =>
                           {
                               try
                               {
                                   TSensorValue previousVal = default(TSensorValue);
                                   TSensorValue sensorVal;

                                   for (; ; )
                                   {
                                       DateTime preDate = DateTime.Now;

                                       var ok = GetValue(out sensorVal);                                       
                                       if (ok && ValueChanged(previousVal, sensorVal))
                                       {
                                           RaiseEventOnMainThread(OnChange, this, new SensorEventArgs<TSensorValue>(sensorVal));
                                           previousVal = sensorVal;
                                       }

                                       // sleep for the necessary amount of time
                                       DateTime postDate = DateTime.Now;
                                       TimeSpan intervalRemainder = TimeResolution - (postDate - preDate);
                                       Thread.Sleep((int)Math.Max(intervalRemainder.TotalMilliseconds, DefaultTimeResolution.TotalMilliseconds));                                       

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

        protected abstract bool ValueChanged(TSensorValue previousVal, TSensorValue newVal);

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
            SensorPoller.Wait(TimeResolution.Add(TimeResolution));
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
