using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nSt.NxtControlLib.Input;
using nSt.NxtControlLib.Façades;

namespace UnitTests.Stubs
{
    internal class StubUltrasonicSensor : PollingSensor<int>
    {
        public override int MaxSensorVal { get { return 255; } }
        public override int MinSensorVal { get { return 0; } }


        public StubUltrasonicSensor(StubNxtBrick brick, NxtBrick.Sensor sensor)
            : base(brick, sensor)
        {
            ValResPercent = 1;
        }



        public override void InitSensor()
        {
            // stub
            Brick.SetSensorMode(Sensor,
                NxtBrick.SensorType.Lowspeed9V,
                NxtBrick.SensorMode.Raw);
            
        }

        public override int GetValue()
        {
            int value;
            bool ok;

            ok = Brick.GetUltrasonicSensorsValue(Sensor, out value);         

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
