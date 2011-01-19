using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using AForge.Robotics.Lego;
using nSt.NxtControlLib.Façades;

namespace nSt.NxtControlLib.Input
{
    public class UltrasonicSensor : PollingSensor<int>
    {
        public override int MaxSensorVal { get { return 255; } }
        public override int MinSensorVal { get { return 0; } }


        public UltrasonicSensor(INxtBrick nxtBrick, NxtBrick.Sensor sensor)
            : base(nxtBrick, sensor)
        {
            ValResPercent = 1;
        }



        public override void InitSensor()
        {
            Brick.SetSensorMode(Sensor,
                NxtBrick.SensorType.Lowspeed9V,
                NxtBrick.SensorMode.Raw);
            //Thread.Sleep(300);
        }

        public override int GetValue()
        {
            int value;
            bool ok;

#if DEBUG
            DateTime t0 = DateTime.Now;
#endif
            ok = Brick.GetUltrasonicSensorsValue(Sensor, out value);
#if DEBUG
            //Debug.WriteLine(t0.TimeOfDay + ": time to sense distance: " + (DateTime.Now - t0));
#endif            

            if (!ok)
                return MinSensorVal;
            return value;
        }

        protected override bool ValueChanged(int previousVal, int newVal)
        {
            if (ValResPercent < 1)
                return newVal != previousVal;

            return (newVal > (previousVal * (100 + ValResPercent)) / 100
                    || newVal < (previousVal * (100 - ValResPercent)) / 100);               
        }
    }
}
