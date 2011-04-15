using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace nSt.NxtControlLib.Output
{    
    public class Motor : IDisposable
    {
        private INxtBrick Brick { get; set; }

        public NxtBrick.MotorRegulationMode RegulationMode { get; set; }

        public NxtBrick.Motor MotorPort { get; private set; }
        public NxtBrick.MotorState State { get; private set; }
        public bool IsRunning
        {
            get
            {
                return State.RunState != NxtBrick.MotorRunState.Idle
                    && State.Mode.HasFlag(NxtBrick.MotorMode.None);
            }
        }


        public Motor(INxtBrick nxtBrick, NxtBrick.Motor motor)
        {
            Brick = nxtBrick;
            MotorPort = motor;

            RegulationMode = NxtBrick.MotorRegulationMode.Idle;
        }


        public void Start(uint powerPercentage, bool forward)
        {
            Start(Convert.ToInt32(powerPercentage) * (forward ? 1 : -1));
        }
        public void Start(int powerPercentage)
        {
            NxtBrick.MotorState state = new NxtBrick.MotorState();
            
            state.Power = powerPercentage;
            
            state.Regulation = RegulationMode; // Synced or idle
            // Turn on the motor
            state.Mode = NxtBrick.MotorMode.On | 
                ((RegulationMode == NxtBrick.MotorRegulationMode.Sync ||
                    RegulationMode == NxtBrick.MotorRegulationMode.Speed) ? NxtBrick.MotorMode.Regulated : 0);
            state.RunState = NxtBrick.MotorRunState.Running; // Motor will be running
            state.TachoLimit = 0; // Run forever
            State = state;

            Execute(State);
        }

        public void Stop()
        {
            NxtBrick.MotorState state = new NxtBrick.MotorState();

            state.Power = 0;
            state.Mode = NxtBrick.MotorMode.None;
            state.RunState = NxtBrick.MotorRunState.Idle; // Motor will be idle
            State = state;

            Execute(State);
        }       

        public void Dispose()
        {
            Stop();
        }


        private void Execute(NxtBrick.MotorState state)
        {
            // parellelize this slow command 
            Task.Factory.StartNew(() =>
                {
                    Brick.SetMotorState(MotorPort, state);
                    Debug.WriteLine("Motor on Port " + MotorPort.ToString() + ": " + state.Power + "%");
                });
        }        
    }
}
