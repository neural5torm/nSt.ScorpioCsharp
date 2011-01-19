using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using nSt.NxtControlLib.Façades;

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

        public override bool GetValue()
        {
            NxtBrick.SensorValues values;
            bool ok;

#if DEBUG
            DateTime t0 = DateTime.Now;
#endif
            ok = Brick.GetSensorValue(Sensor, out values);            
#if DEBUG
            //Debug.WriteLine(t0.TimeOfDay + ": time to sense touch: " + (DateTime.Now - t0));
#endif

            if (!ok)
                return MinSensorVal;
            return values.Scaled == 1;
        }

        protected override bool ValueChanged(bool previousVal, bool newVal)
        {
            return previousVal != newVal;
        }
    }
}
