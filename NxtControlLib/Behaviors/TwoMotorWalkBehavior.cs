using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using nSt.NxtControlLib.Output;
using AForge.Robotics.Lego;
using nSt.NxtControlLib.Façades;

namespace nSt.NxtControlLib.Behaviors
{
    public class TwoMotorWalkBehavior : IWalkBehavior
    {
        public Motor LeftMotor { get; private set; }
        public Motor RightMotor { get; private set; }

        public bool ForcedSync
        {
            get
            {
                return LeftMotor.RegulationMode == NxtBrick.MotorRegulationMode.Sync &&
                    RightMotor.RegulationMode == NxtBrick.MotorRegulationMode.Sync;
            }
            set
            {                
                    LeftMotor.RegulationMode = 
                        RightMotor.RegulationMode = (value ? NxtBrick.MotorRegulationMode.Sync : NxtBrick.MotorRegulationMode.Idle);
            }
        }

        public Task WalkTask { get; private set; }
        public bool IsWalking
        {
            get
            {
                return LeftMotor.IsRunning || RightMotor.IsRunning ||
                    WalkTask != null && !WalkTask.IsCompleted;
            }
        }


        // C'tor
        public TwoMotorWalkBehavior(Motor leftMotor, Motor rightMotor)
        {
            LeftMotor = leftMotor;
            RightMotor = rightMotor;
        }


        public void Walk(int powerPercentage, TimeSpan forHowLong)
        {
            Start(powerPercentage, powerPercentage, forHowLong);
        }
        public void Walk(int powerPercentage)
        {
            Walk(powerPercentage, TimeSpan.Zero);                        
        }        

        public void WalkReverse(int powerPercentage)
        {
            WalkReverse(powerPercentage, TimeSpan.Zero);
        }
        public void WalkReverse(int powerPercentage, TimeSpan forHowLong)
        {
            Walk(-1 * Math.Abs(powerPercentage), forHowLong);
        }

        public void Turn(bool clockwise, int powerPercentage, double radius = 0.5)
        {
            Turn(clockwise, powerPercentage, TimeSpan.Zero, radius);
        }
        public void Turn(bool clockwise, int powerPercentage, TimeSpan forHowLong, double radius = 0.5)
        {
            if (radius < 1.0 && radius >= 0.0)
            {
                int slowMotorPower = Convert.ToInt32(Math.Round(powerPercentage * (radius * 2.0 - 1.0)));

                if (clockwise)
                    Start(powerPercentage, slowMotorPower, forHowLong);
                else
                    Start(slowMotorPower, powerPercentage, forHowLong);
            }
        }

        public void TurnAround(bool clockwise, int powerPercentage)
        {
            TurnAround(clockwise, powerPercentage, TimeSpan.Zero);
        }
        public void TurnAround(bool clockwise, int powerPercentage, TimeSpan forHowLong)
        {
            Turn(clockwise, powerPercentage, forHowLong, 0.0);
        }

        public void Stop()
        {
            if (WalkTask != null)
                WalkTask.Wait(0);

            Interrupt();            
        }



        private void Start(int leftPower, int rightPower)
        {
            ForcedSync = (leftPower == rightPower);

            LeftMotor.Start(leftPower);
            RightMotor.Start(rightPower);

            // TODOlater raise a WalkStarted event
        }
        private void Start(int leftPower, int rightPower, TimeSpan forHowLong)
        {
            if (forHowLong == TimeSpan.Zero)
            {
                Start(leftPower, rightPower);
            }
            else // use a parallel Task to handle the time-bound action                
                if (WalkTask == null)
                { 
                    WalkTask = Task.Factory.StartNew(() =>
                    {
                        Start(leftPower, rightPower);
                        Thread.Sleep(forHowLong);
                        Interrupt();
                        WalkTask = null;
                    });
                }
        }

        private void Interrupt()
        {
            LeftMotor.Stop();
            RightMotor.Stop();

            // TODOlater raise a WalkStopped event
        }
    }
}
