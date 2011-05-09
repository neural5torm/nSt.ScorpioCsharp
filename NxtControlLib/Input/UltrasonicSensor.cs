using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

using AForge.Robotics.Lego;

namespace nSt.NxtControlLib.Input
{
    public class UltrasonicSensor : PollingSensor<int>
    {
        public override int MaxSensorValue { get { return 255; } }
        public override int MinSensorValue { get { return 0; } }


        public UltrasonicSensor(INxtBrick nxtBrick, NxtBrick.Sensor sensor)
            : base(nxtBrick, sensor)
        {
            ValueResolutionPercentage = 1;
        }



        public override void InitSensor()
        {
            Brick.SetSensorMode(Sensor,
                NxtBrick.SensorType.Lowspeed9V,
                NxtBrick.SensorMode.Raw);
            //Thread.Sleep(300);
        }

        public override bool GetValue(out int value)
        {
            return Brick.GetUltrasonicSensorsValue(Sensor, out value);
        }

        protected override bool ValueChanged(int previousVal, int newVal)
        {
            if (ValueResolutionPercentage < 1)
                return newVal != previousVal;

            return (newVal < (previousVal * (100 - ValueResolutionPercentage)) / 100.0 
                    || newVal > (previousVal * (100 + ValueResolutionPercentage)) / 100.0);               
        }
    }
}
