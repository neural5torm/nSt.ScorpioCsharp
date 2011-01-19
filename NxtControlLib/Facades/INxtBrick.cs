using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Robotics.Lego;

namespace nSt.NxtControlLib.Façades
{   
    public interface INxtBrick
    {
        bool IsConnected { get; }

        bool ClearSensor(NxtBrick.Sensor sensor);
        bool Connect(string portName);
        void Disconnect();
        bool GetBatteryPower(out int power);
        bool GetDeviceInformation(out string deviceName, out byte[] btAddress, out int btSignalStrength, out int freeUserFlash);
        //bool GetMotorState(NXTBrick.Motor motor, out NXTBrick.MotorState state);
        bool GetSensorValue(NxtBrick.Sensor sensor, out NxtBrick.SensorValues sensorValues);
        bool GetUltrasonicSensorsValue(NxtBrick.Sensor sensor, out int value);
        bool GetVersion(out string protocolVersion, out string firmwareVersion);
        bool IsAlive();
        //bool LsGetStatus(NXTBrick.Sensor sensor, out int readyBytes);
        //bool LsRead(NXTBrick.Sensor sensor, byte[] readValues, out int bytesRead);
        //bool LsWrite(NXTBrick.Sensor sensor, byte[] data, int expectedBytes);
        bool PlayTone(short frequency, short duration);
        //bool ResetMotorPosition(NXTBrick.Motor motor);
        
        bool SetBrickName(string deviceName);
        bool SetMotorState(NxtBrick.Motor motor, NxtBrick.MotorState state);
        bool SetSensorMode(NxtBrick.Sensor sensor, NxtBrick.SensorType type, NxtBrick.SensorMode mode);        
    }
}
