using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using UnitTests.Stubs;
using nSt.NxtControlLib.Façades;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class UltrasonicSensorUnitTest
    {

        private StubNxtBrick brick;
        private StubNxtBrick Brick
        {
            get
            {
                if (brick == null)
                {
                    brick = new StubNxtBrick();
                    brick.Connect("COMstub");
                }
                return brick;
            }
        }
        

        // TODOnow test the ultrasonic sensor outputs with real and random/mock data using an Ultrasonic/stub sensor

        [TestMethod]
        public void StubSensorReturnsValuesBetween0And255()
        {
            // Arrange
            StubUltrasonicSensor stubSensor = new StubUltrasonicSensor(Brick, NxtBrick.Sensor.First);
            List<int> values = new List<int>();
            brick.ConnectionFckedUp = true;
            stubSensor.InitSensor();

            // Act 
            Task.Factory.StartNew(() =>
            {
                // TODOnow install on GitHub
                // TODOnow startsensing
            });

            // Assert
            Assert.AreNotEqual(0, values.Count);
            Assert.IsTrue(values.All((value) => value >= 0 && value <= 255));

        }
    }
}
