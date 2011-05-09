using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


namespace nSt.NxtControlLib.Input
{
    /// <summary>
    /// <!>Highly experimental!
    /// </summary>
    public class SoundDbaRatioSensor : PollingSensor<double>
    {
        public override double MaxSensorValue { get { return 1.0; } }
        public override double MinSensorValue { get { return 0.0; } }


        public SoundDbaRatioSensor(INxtBrick nxtBrick, NxtBrick.Sensor sensor)
            : base(nxtBrick, sensor)
        { TimeResolution = TimeSpan.FromMilliseconds(300); }
        

        public override void InitSensor()
        { }

        public override bool GetValue(out double value)
        {
            // Sensor sensibilities found at http://www.convict.lu/htm/rob/NXT_sound_sensor.htm

            double dba, db;

            var ok1 = GetDbaValue(out dba);
            var ok2 = GetDbValue(out db);

            var dbaRatio = 3.0 * Math.Sqrt(Math.Max(dba - 0.1, 0.0));
            dbaRatio += Math.Sqrt(Math.Max(db - 0.1, 0.0));
            value = dbaRatio / 4.0;

            return ok1 && ok2;
        }

        protected override bool ValueChanged(double previousVal, double newVal)
        {
            if (ValueResolutionPercentage < 1)
                return newVal != previousVal;

            return (newVal > (previousVal * (100.0 + ValueResolutionPercentage)) / 100.0
                    || newVal < (previousVal * (100.0 - ValueResolutionPercentage)) / 100.0);
        }



        private bool GetDbaValue(out double value)
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

            value = (double)sensorValues.Normalized / MAX_NORMALIZED;
            return ok;
        }

        private bool GetDbValue(out double value)
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

            value = (double)sensorValues.Normalized / MAX_NORMALIZED;
            return ok;
        }
    }
}
