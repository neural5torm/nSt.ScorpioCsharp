using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using nSt.NxtControlLib.Façades;

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

        public override ushort GetValue()
        {
            NxtBrick.SensorValues values;
            bool ok;

#if DEBUG
            DateTime t0 = DateTime.Now;
#endif
            ok = Brick.GetSensorValue(Sensor, out values);                       
#if DEBUG
            //Debug.WriteLine(t0.TimeOfDay + ": time to sense light level: " + (DateTime.Now - t0));
#endif

            if (!ok)
                return MinSensorVal;
            return values.Normalized;                            
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
