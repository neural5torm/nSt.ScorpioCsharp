using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;

using nSt.NxtControlLib.Input;
using nSt.NxtControlLib.Output;
using nSt.NxtControlLib.Behaviors;

namespace nSt.NxtControlLib
{
    // TODOlater extract base logic to abstract class
    // TODOlater check if NXT brick is connected/responds before accessing sensors/motors through corresponding properties
    
    // TODO use NxtStatusSensor.Connection to update status bar with "disconnected"/"connected" captions
    // TODO create boolean Properties for each sensor/motor/behavior, acting as stop/start switches
    
    public class Scorpio : IDisposable
    {
        public string Name { get { return "Scorpio V1"; } }

        public INxtBrick Brick { get; private set; }
        public string BluetoothPortName { get; private set; }

        public PollingSensor<ushort> LightIntensitySensor { get; private set; }
        public PollingSensor<ushort> SoundLevelSensor { get; private set; }
        public PollingSensor<double> SoundRatioSensor { get; private set; }
        public PollingSensor<int>    UltrasonicSensor { get; private set; }
        public PollingSensor<bool>   TouchSensor { get; private set; }

        public Motor MotorTail { get; private set; }
        public Motor MotorRight { get; private set; }
        public Motor MotorLeft { get; private set; }

        public IWalkBehavior WalkBehavior { get; private set; }

        public bool IsConnectionStarted { get; private set; }        
        bool IsConnected { get { return Brick.IsConnected;  } }

        

        public Scorpio()
            : this(string.Empty)
        { }

        /// <summary>
        /// This is where all sensor and motor properties are initialized
        /// </summary>
        /// <param name="bluetoothPortName"></param>
        public Scorpio(string bluetoothPortName)
        {
            Brick = new AForgeNxtBrickFacade();
            BluetoothPortName = bluetoothPortName;

            // Sensors:
            // 1
            TouchSensor = new TouchSensor(Brick, NxtBrick.Sensor.First);
            // 2
            LightIntensitySensor = new LightSensor(Brick, NxtBrick.Sensor.Second);
            // 3 (shared)
            SoundLevelSensor = new SoundSensor(Brick, NxtBrick.Sensor.Third);
            SoundRatioSensor = new SoundDbaRatioSensor(Brick, NxtBrick.Sensor.Third);
            // 4
            UltrasonicSensor = new UltrasonicSensor(Brick, NxtBrick.Sensor.Fourth);

            // Motor sensors:
            // 1

            // Motors:
            // A,B,C
            MotorTail = new Motor(Brick, NxtBrick.Motor.A);
            MotorRight = new Motor(Brick, NxtBrick.Motor.B);
            MotorLeft = new Motor(Brick, NxtBrick.Motor.C);

            // Walk:
            WalkBehavior = new TwoMotorWalkBehavior(MotorLeft, MotorRight);
        }

                
        public bool TryConnect()
        {
            if (string.IsNullOrWhiteSpace(BluetoothPortName))
                return TryAutoDetectAndConnect();

            return TryConnect(BluetoothPortName);
        }

        private bool TryAutoDetectAndConnect()
        {
            foreach (string portName in SerialPort.GetPortNames())
            {
                if (TryConnect(portName))
                {
                    // if found                    
                    return true;
                }
            }

            Debug.WriteLine(Name + " could not connect to any BT port.");
            return false;
        }

        /// <summary>
        /// This is where the actual connection happens
        /// </summary>
        /// <param name="bluetoothPortName"></param>
        /// <returns></returns>
        public bool TryConnect(string bluetoothPortName)
        {
            if (!IsConnectionStarted)
            {
                //var sw = Stopwatch.StartNew();

                // TODOlater execute in a Task 
                Brick.Connect(bluetoothPortName);

                //Debug.WriteLine("It took " + sw.ElapsedMilliseconds + "ms to try to connect.");
                //sw.Stop();

                if (IsConnected)
                {
                    BluetoothPortName = bluetoothPortName;
                    IsConnectionStarted = true;
                    Debug.WriteLine(GetStatusReport());
                }
                else
                {
                    Debug.WriteLine(string.Format(Name + " could not connect to NXT brick at port {0}", bluetoothPortName));
                }
            }
            else            
                Trace.WriteLine("Connection already started");
            
            return IsConnectionStarted;
        }
      
        /// <summary>
        /// This is where all sensors and motors are stopped 
        /// </summary>
        public void DisconnectAll()
        {
            // Stop sensing
            if (LightIntensitySensor.IsSensing())
                LightIntensitySensor.StopSensing();            
            if (SoundLevelSensor.IsSensing())
                SoundLevelSensor.StopSensing();
            if (SoundRatioSensor.IsSensing())
                SoundRatioSensor.StopSensing();
            if (UltrasonicSensor.IsSensing())
                UltrasonicSensor.StopSensing();
            if (TouchSensor.IsSensing())
                TouchSensor.StopSensing();
            //TODO clear sensors?

            // Stop behaviors
            if (WalkBehavior.IsWalking)
                WalkBehavior.Stop();

            // Stop motors
            if (MotorTail.IsRunning)
                MotorTail.Stop();
            if (MotorRight.IsRunning)
                MotorRight.Stop();
            if (MotorLeft.IsRunning)
                MotorLeft.Stop();

            System.Threading.Thread.Sleep(1000);

            Brick.Disconnect();
            IsConnectionStarted = false;
        }


        public string GetStatusReport()
        {
            if (!IsConnected)
            {
                return Name + " not connected.";
            }

            StringBuilder reportSB = new StringBuilder(Name + " connected successfully to port " + BluetoothPortName + ":");

            string deviceName;
            byte[] btAddressArray;
            int btSignalStrength,
                freeUserFlash;
            if (Brick.GetDeviceInformation(out deviceName, out btAddressArray,
                out btSignalStrength, out freeUserFlash))
            {
                long btAddress = 0;
                for (int i = 0; i < btAddressArray.Length; i++)
                {
                    btAddress += (int)btAddressArray[i] << i * 8;
                }

                reportSB.AppendLine("\nDevice name: " + deviceName.TrimEnd('\x0'))
                    .AppendLine("Bluetooth address: " + btAddress.ToString("8x"))
                    .AppendLine("Signal strength: " + btSignalStrength)
                    .AppendLine("Free user flash space: " + freeUserFlash + " bytes");
            }

            string protocolVersion,
                firmwareVersion;
            if (Brick.GetVersion(out protocolVersion, out firmwareVersion))
            {
                reportSB.AppendLine("Firmware: v." + firmwareVersion)
                    .AppendLine("Protocol: v." + protocolVersion);
            }

            int battery;
            if (Brick.GetBatteryPower(out battery))
                reportSB.AppendLine("Battery: " + battery / 1000.0 + "V");

            return reportSB.ToString();                        
        }


        public override string ToString()
        {
            return Name;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
