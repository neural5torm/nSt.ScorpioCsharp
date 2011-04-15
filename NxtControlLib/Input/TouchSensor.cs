using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace nSt.NxtControlLib.Input
{
    public class TouchSensor : PollingSensor<bool>
    {
        public override bool MaxSensorVal { get { return true; } }
        public override bool MinSensorVal { get { return false; } }


        public TouchSensor(INxtBrick nxtBrick, NxtBrick.Sensor sensor)
            : base(nxtBrick, sensor)
        { }
       

        public override void InitSensor()
        {
            Brick.ClearSensor(Sensor);
            Brick.SetSensorMode(Sensor,
                NxtBrick.SensorType.Switch,
                NxtBrick.SensorMode.Boolean);
        }

        public override bool GetValue(out bool value)
        {
            NxtBrick.SensorValues values;
            
            var ok = Brick.GetSensorValue(Sensor, out values);            

            value = (values.Scaled == 1);
            return ok;
        }

        protected override bool ValueChanged(bool previousVal, bool newVal)
        {
            return previousVal != newVal;
        }
    }
}
