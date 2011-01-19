using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Robotics.Lego;
using System.Diagnostics;
using nSt.NxtControlLib.Façades;

namespace nSt.NxtControlLib.Input
{
    public class SoundSensor : PollingSensor<ushort>
    {
        public override ushort MaxSensorVal { get { return 1023; } }
        public override ushort MinSensorVal { get { return 0; } }


        public SoundSensor(INxtBrick nxtBrick, NxtBrick.Sensor sensor)
            : base(nxtBrick, sensor)
        { }


        public override void InitSensor()
        {
            Brick.ClearSensor(Sensor);
            Brick.SetSensorMode(Sensor, 
                NxtBrick.SensorType.SoundDB, 
                NxtBrick.SensorMode.Raw);
        }

        public override ushort GetValue()
        {
            NxtBrick.SensorValues values;
            bool ok;

#if DEBUG
            DateTime t0 = DateTime.Now;
#endif
            lock (Brick)
            {                
                ok = Brick.GetSensorValue(Sensor, out values);
            }
#if DEBUG
            //Debug.WriteLine(t0.TimeOfDay + ": time to sense sound level: " + (DateTime.Now - t0));
#endif

            if (!ok)
                return MinSensorVal;
            return values.Normalized;
        }

        protected override bool ValueChanged(ushort previousVal, ushort newVal)
        {
            if (ValResPercent < 1)
                return newVal != previousVal;

            return (newVal > (previousVal * (100 + ValResPercent)) / 100
                    || newVal < (previousVal * (100 - ValResPercent)) / 100);                
        }
    }
}
