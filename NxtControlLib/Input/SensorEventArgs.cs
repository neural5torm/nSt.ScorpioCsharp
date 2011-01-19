using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nSt.NxtControlLib.Input
{
    public class SensorEventArgs<TSensorVal> : EventArgs
        where TSensorVal : struct
    {
        public TSensorVal SensorVal { get; set; }


        public SensorEventArgs(TSensorVal sensorVal)
        {
            SensorVal = sensorVal;
        }
    }
}
