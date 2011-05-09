using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nSt.NxtControlLib.Input;
using nSt.NxtControlLib;

namespace UnitTests.Stubs
{
    internal class StubUltrasonicSensor : PollingSensor<int>
    {
        public override int MaxSensorValue { get { return 255; } }
        public override int MinSensorValue { get { return 0; } }


        public StubUltrasonicSensor(StubNxtBrick brick, NxtBrick.Sensor sensor)
            : base(brick, sensor)
        {
            ValueResolutionPercentage = 1;
        }



        public override void InitSensor()
        {
            // stub
            Brick.SetSensorMode(Sensor,
                NxtBrick.SensorType.Lowspeed9V,
                NxtBrick.SensorMode.Raw);
            
        }

        public override bool GetValue(out int value)
        {
            return Brick.GetUltrasonicSensorsValue(Sensor, out value);         
        }

        protected override bool ValueChanged(int previousVal, int newVal)
        {
            if (ValueResolutionPercentage < 1)
                return newVal != previousVal;

            return (newVal > (previousVal * (100 + ValueResolutionPercentage)) / 100
                    || newVal < (previousVal * (100 - ValueResolutionPercentage)) / 100);               
        }
    }
}
