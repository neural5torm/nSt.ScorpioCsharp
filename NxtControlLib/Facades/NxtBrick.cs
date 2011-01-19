using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nSt.NxtControlLib.Façades
{
    public abstract class NxtBrick : INxtBrick
    {
        public abstract bool IsConnected { get; }

        public abstract bool ClearSensor(NxtBrick.Sensor sensor);
        public abstract bool Connect(string portName);
        public abstract void Disconnect();
        public abstract bool GetBatteryPower(out int power);
        public abstract bool GetDeviceInformation(out string deviceName, out byte[] btAddress, out int btSignalStrength, out int freeUserFlash);
        
        public abstract bool GetSensorValue(NxtBrick.Sensor sensor, out NxtBrick.SensorValues sensorValues);
        public abstract bool GetUltrasonicSensorsValue(NxtBrick.Sensor sensor, out int value);
        public abstract bool GetVersion(out string protocolVersion, out string firmwareVersion);
        public abstract bool IsAlive();

        public abstract bool PlayTone(short frequency, short duration);

        public abstract bool SetBrickName(string deviceName);
        public abstract bool SetMotorState(NxtBrick.Motor motor, NxtBrick.MotorState state);
        public abstract bool SetSensorMode(NxtBrick.Sensor sensor, NxtBrick.SensorType type, NxtBrick.SensorMode mode);


        #region Nested Types

        public enum Motor
        {
            A = 0,
            B = 1,
            C = 2,
            All = 255,
        }
        [Flags]
        public enum MotorMode
        {
            None = 0,
            On = 1,
            Brake = 2,
            Regulated = 4,
        }
        public enum MotorRegulationMode
        {
            Idle = 0,
            Speed = 1,
            Sync = 2,
        }
        public enum MotorRunState
        {
            Idle = 0,
            RampUp = 16,
            Running = 32,
            RampDown = 64,
        }

        public enum Sensor
        {
            First = 0,
            Second = 1,
            Third = 2,
            Fourth = 3,
        }
        public enum SensorMode
        {
            Raw = 0,
            Boolean = 32,
            TransitionCounter = 64,
            PeriodicCounter = 96,
            PCTFullScale = 128,
            Celsius = 160,
            Fahrenheit = 192,
            AngleSteps = 224,
        }
        public enum SensorType
        {
            NoSensor = 0,
            Switch = 1,
            Temperature = 2,
            Reflection = 3,
            Angle = 4,
            LightActive = 5,
            LightInactive = 6,
            SoundDB = 7,
            SoundDBA = 8,
            Custom = 9,
            Lowspeed = 10,
            Lowspeed9V = 11
        }

        public struct MotorState
        {
            public int BlockTachoCount;
            public NxtBrick.MotorMode Mode;
            public int Power;
            public NxtBrick.MotorRegulationMode Regulation;
            public int RotationCount;
            public NxtBrick.MotorRunState RunState;
            public int TachoCount;
            public int TachoLimit;
            public int TurnRatio;

            public static implicit operator AForge.Robotics.Lego.NXTBrick.MotorState(MotorState ms)
            {
                AForge.Robotics.Lego.NXTBrick.MotorState state;
                state.BlockTachoCount = ms.BlockTachoCount;
                state.Mode = (AForge.Robotics.Lego.NXTBrick.MotorMode)ms.Mode;
                state.Power = ms.Power;
                state.Regulation = (AForge.Robotics.Lego.NXTBrick.MotorRegulationMode)ms.Regulation;
                state.RotationCount = ms.RotationCount;
                state.RunState = (AForge.Robotics.Lego.NXTBrick.MotorRunState)ms.RunState;
                state.TachoCount = ms.TachoCount;
                state.TachoLimit = ms.TachoLimit;
                state.TurnRatio = ms.TurnRatio;

                return state;
            }
        }
        public struct SensorValues
        {
            public short Calibrated;
            public bool IsCalibrated;
            public bool IsValid;
            public ushort Normalized;
            public ushort Raw;
            public short Scaled;
            public NxtBrick.SensorMode SensorMode;
            public NxtBrick.SensorType SensorType;

            public static implicit operator SensorValues(AForge.Robotics.Lego.NXTBrick.SensorValues sensorValues)
            {
                NxtBrick.SensorValues values;
                values.Calibrated = sensorValues.Calibrated;
                values.IsCalibrated = sensorValues.IsCalibrated;
                values.IsValid = sensorValues.IsValid;
                values.Normalized = sensorValues.Normalized;
                values.Raw = sensorValues.Raw;
                values.Scaled = sensorValues.Scaled;
                values.SensorMode = (SensorMode)sensorValues.SensorMode;
                values.SensorType = (SensorType)sensorValues.SensorType;

                return values;
            }
        }

        #endregion
    }
}
