using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nSt.NxtControlLib.Behaviors
{
    public interface IWalkBehavior
    {
        bool IsWalking { get; }

        void Walk(int powerPercentage);
        void Walk(int powerPercentage, TimeSpan forHowLong);

        void WalkReverse(int powerPercentage);
        void WalkReverse(int powerPercentage, TimeSpan forHowLong);
        
        void Turn(bool clockwise, int powerPercentage, double radius = 0.5);
        void Turn(bool clockwise, int powerPercentage, TimeSpan forHowLong, double radius = 0.5);
        
        void TurnAround(bool clockwise, int powerPercentage);
        void TurnAround(bool clockwise, int powerPercentage, TimeSpan forHowLong);
        
        void Stop();
    }
}
