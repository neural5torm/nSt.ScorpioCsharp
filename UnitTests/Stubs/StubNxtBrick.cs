using System.Collections.Generic;
using System.Linq;
using System.Text;
using nSt.NxtControlLib.Façades;
using System.Diagnostics;
using System;

namespace UnitTests.Stubs
{
    class StubNxtBrick : NxtBrick
    {
        Dictionary<NxtBrick.Sensor, NxtBrick.SensorType> sensorDic;
        protected Dictionary<NxtBrick.Sensor, NxtBrick.SensorType> SensorDic
        {
            get
            {
                if (sensorDic == null)
                {
                    sensorDic = new Dictionary<NxtBrick.Sensor, NxtBrick.SensorType>();
                    sensorDic[NxtBrick.Sensor.First] = NxtBrick.SensorType.NoSensor;
                    sensorDic[NxtBrick.Sensor.Second] = NxtBrick.SensorType.NoSensor;
                    sensorDic[NxtBrick.Sensor.Third] = NxtBrick.SensorType.NoSensor;
                    sensorDic[NxtBrick.Sensor.Fourth] = NxtBrick.SensorType.NoSensor;
                }
                return sensorDic;
            }
            private set { sensorDic = value; }
        }

        private bool connectionFckedUp = false;
        public bool ConnectionFckedUp { 
            get { return connectionFckedUp; }
            set { if (value) isConnected = false; connectionFckedUp = value; }
        }


        #region NxtBrick methods

        public bool isConnected;
        public override bool IsConnected { get { return isConnected; } }

        public override bool ClearSensor(NxtBrick.Sensor sensor)
        {
            if (SensorDic.ContainsKey(sensor))
            {
                SensorDic[sensor] = NxtBrick.SensorType.NoSensor;
                return true;
            }
            print("no sensor {0} to clear", sensor);
            return false;
        }

        public override bool Connect(string portName)
        {
            print("{0} connects to BT port {1}", GetType().Name, portName);

            if (!ConnectionFckedUp)
                isConnected = true;

            print("{0} {1}managed to connect to BT port {2}", GetType().Name, IsConnected ? string.Empty:"hasn't ", portName);
            return IsConnected;
        }

        public override void Disconnect()
        {
            isConnected = false;
        }

        public override bool GetBatteryPower(out int power)
        {
            throw new NotImplementedException();
        }

        public override bool GetDeviceInformation(out string deviceName, out byte[] btAddress, out int btSignalStrength, out int freeUserFlash)
        {
            throw new NotImplementedException();
        }

        public override bool GetSensorValue(NxtBrick.Sensor sensor, out NxtBrick.SensorValues sensorValues)
        {            
            sensorValues = new NxtBrick.SensorValues();
            sensorValues.Raw = ushort.MinValue;

            if (IsConnected)
            {
                switch (SensorDic[sensor])
                {
                    case NxtBrick.SensorType.NoSensor:
                        break;
                    case NxtBrick.SensorType.Switch:
                        break;
                    case NxtBrick.SensorType.Temperature:
                        break;
                    case NxtBrick.SensorType.Reflection:
                        break;
                    case NxtBrick.SensorType.Angle:
                        break;
                    case NxtBrick.SensorType.LightActive:
                        break;
                    case NxtBrick.SensorType.LightInactive:
                        break;
                    case NxtBrick.SensorType.SoundDB:
                        break;
                    case NxtBrick.SensorType.SoundDBA:
                        break;
                    case NxtBrick.SensorType.Custom:
                        break;
                    case NxtBrick.SensorType.Lowspeed:
                        break;
                    case NxtBrick.SensorType.Lowspeed9V:
                        break;
                    default:
                        print("Problem: could not retrieve data because of unknown sensor type {0} set for sensor {1}",
                            SensorDic[sensor], sensor);
                        return false;
                }
                return true;
            }
            print("Problem: could not retrieve data because of broken connection");
            return false;
        }

        public override bool GetUltrasonicSensorsValue(NxtBrick.Sensor sensor, out int value)
        {           
            if (IsConnected)
            {
                Random rand = new Random();
                value = rand.Next(1024) - 512;
                return true;
            }
            print("Problem: could not retrieve data because of broken connection");
            value = -1;
            return false;
        }

        public override bool GetVersion(out string protocolVersion, out string firmwareVersion)
        {
            throw new NotImplementedException();
        }

        public override bool IsAlive()
        {
            throw new NotImplementedException();
        }

        public override bool PlayTone(short frequency, short duration)
        {
            throw new NotImplementedException();
        }

        public override bool SetBrickName(string deviceName)
        {
            throw new NotImplementedException();
        }

        public override bool SetMotorState(NxtBrick.Motor motor, NxtBrick.MotorState state)
        {
            throw new NotImplementedException();
        }

        public override bool SetSensorMode(NxtBrick.Sensor sensor, NxtBrick.SensorType type, NxtBrick.SensorMode mode)
        {
            if (IsConnected)
            {
                print("sensor port {0} set to {1} type in {2} mode", sensor, type, mode);
                if (SensorDic[sensor] != NxtBrick.SensorType.NoSensor)
                {
                    print("Problem: sensor {0} already set", sensor);
                    return false;
                }
                if (!SensorDic.ContainsKey(sensor))
                {
                    print("Problem: sensor {0} inexistent", sensor);
                    return false;
                }
                SensorDic[sensor] = type;
                return true;
            }
            print("Problem: could not retrieve data because of broken connection");
            return false;
        }

        #endregion NxtBrick methods

        #region Private stuff

        private void print(object format, params object[] args)
        {
            Debug.WriteLine("DEBUG info: " + prettyprint(string.Format(format.ToString(), args)));
        }
        private void printfragment(object line)
        {
            Debug.Write(line.ToString());
        }

        private string prettyprint(string text)
        {
            StringBuilder sb = new StringBuilder(text);

            sb.Remove(0, 1);
            sb.Insert(0, text.Substring(0, 1).ToString().ToUpper());

            if (!text.EndsWith("."))
                sb.Append(".");

            return sb.ToString();
        }
        #endregion
    }
}
