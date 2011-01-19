using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using nSt.NxtControlLib.Façades;

namespace nSt.NxtControlLib.Input
{
    /// <summary>
    /// <!>Highly experimental!
    /// </summary>
    public class SoundDbaRatioSensor : PollingSensor<double>
    {
        public override double MaxSensorVal { get { return 1.0; } }
        public override double MinSensorVal { get { return 0.0; } }


        public SoundDbaRatioSensor(INxtBrick nxtBrick, NxtBrick.Sensor sensor)
            : base(nxtBrick, sensor)
        { TimeRes = TimeSpan.FromMilliseconds(300); }
        

        public override void InitSensor()
        { }

        public override double GetValue()
        {
            // sensor sensibilities found at http://www.convict.lu/htm/rob/NXT_sound_sensor.htm
#if DEBUG
            DateTime t0 = DateTime.Now;
#endif
            double value = 3.0 * Math.Sqrt(Math.Max(GetDbaValue() - 0.1, 0.0));
            value += Math.Sqrt(Math.Max(GetDbValue() - 0.1, 0.0));
#if DEBUG
            //Debug.WriteLine(t0.TimeOfDay + ": time to sense DBA sound level: " + (DateTime.Now - t0));
#endif            
            return value / 4.0;
        }

        protected override bool ValueChanged(double previousVal, double newVal)
        {
            if (ValResPercent < 1)
                return newVal != previousVal;

            return (newVal > (previousVal * (100.0 + ValResPercent)) / 100.0
                    || newVal < (previousVal * (100.0 - ValResPercent)) / 100.0);
        }



        private double GetDbaValue()
        {
            NxtBrick.SensorValues sensorValues;
            bool ok;        

            lock (Brick)
            {
                // init the sensor
                Brick.ClearSensor(Sensor);
                Brick.SetSensorMode(Sensor,
                   NxtBrick.SensorType.SoundDBA,
                   NxtBrick.SensorMode.Raw);
                ok = Brick.GetSensorValue(Sensor, out sensorValues);
            }

            if (!ok)
                return MinSensorVal;
            return (double)sensorValues.Normalized / MAX_NORMALIZED;            
        }

        private double GetDbValue()
        {
            NxtBrick.SensorValues sensorValues;
            bool ok;

            lock (Brick)
            {
                // init the sensor
                Brick.ClearSensor(Sensor);
                Brick.SetSensorMode(Sensor,
                   NxtBrick.SensorType.SoundDB,
                   NxtBrick.SensorMode.Raw);
                ok = Brick.GetSensorValue(Sensor, out sensorValues);
            }

            if (!ok)
                return MinSensorVal;
            return (double)sensorValues.Normalized / MAX_NORMALIZED;
        }
    }
}
