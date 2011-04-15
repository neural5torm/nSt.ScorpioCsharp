using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AForge.Robotics.Lego;


namespace nSt.NxtControlLib
{
    // TODOlater: implement REAL façade (remove base class dependency)
    public class AForgeNxtBrickFacade : NXTBrick, INxtBrick
    {
        public AForgeNxtBrickFacade()
            : base()
        { }



        public bool ClearSensor(NxtBrick.Sensor sensor)
        {
            return base.ClearSensor((NXTBrick.Sensor)sensor);
        }

        public bool SetSensorMode(NxtBrick.Sensor sensor, NxtBrick.SensorType type, NxtBrick.SensorMode mode)
        {
            return base.SetSensorMode((Sensor)sensor, (SensorType)type, (SensorMode)mode);
        }

        public bool GetSensorValue(NxtBrick.Sensor sensor, out NxtBrick.SensorValues values)
        {
            SensorValues sv;
            bool ret = base.GetSensorValue((Sensor)sensor, out sv);

            values = sv;
            return ret;
        }

        public bool GetUltrasonicSensorsValue(NxtBrick.Sensor sensor, out int value)
        {
            return base.GetUltrasonicSensorsValue((Sensor)sensor, out value);
        }

        public bool SetMotorState(NxtBrick.Motor motor, NxtBrick.MotorState state)
        {
            
            return base.SetMotorState((Motor)motor, state);
        }
    }
}
