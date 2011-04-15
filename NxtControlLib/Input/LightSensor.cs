using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


namespace nSt.NxtControlLib.Input
{
    public class LightSensor : PollingSensor<ushort>
    {
        public override ushort MaxSensorVal { get { return 1023; } }
        public override ushort MinSensorVal { get { return 0; } }


        public LightSensor(INxtBrick nxtBrick, NxtBrick.Sensor sensor)
            : base(nxtBrick, sensor)
        { }


        public override void InitSensor()
        {
            Brick.SetSensorMode(Sensor,
                NxtBrick.SensorType.LightInactive,
                NxtBrick.SensorMode.Raw);
        }

        public override bool GetValue(out ushort value)
        {
            NxtBrick.SensorValues values;
            
            var ok = Brick.GetSensorValue(Sensor, out values);                       

            value = values.Normalized;
            return ok;         
        }

        protected override bool ValueChanged(ushort previousVal, ushort newVal)
        {
            if (ValResPercent < 1)
                return newVal != previousVal;

            return (newVal > (previousVal * (100 + ValResPercent / 2)) / 100
                    || newVal < (previousVal * (100 - ValResPercent / 2)) / 100);                
        }
    }
}
