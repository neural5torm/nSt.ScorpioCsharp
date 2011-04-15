using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.Stubs;

using nSt.NxtControlLib;


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
            var stubSensor = new StubUltrasonicSensor(Brick, NxtBrick.Sensor.First);
            stubSensor.InitSensor();

            // Act 
            // start sensing
            stubSensor.StartGettingValues();
            Thread.Sleep(1000);
            var values = stubSensor.StopGettingValues();

            // Assert
            Assert.AreNotEqual(0, values.Count);
            Assert.IsTrue(values.All((value) => value >= stubSensor.MinSensorVal && value <= stubSensor.MaxSensorVal));

        }

        [TestMethod]
        public void StubSensorReturnsNothingWhenDisconnected()
        {
            // Arrange
            var stubSensor = new StubUltrasonicSensor(Brick, NxtBrick.Sensor.First);
            Brick.ConnectionFckedUp = true;
            stubSensor.InitSensor();

            // Act 
            // start sensing
            stubSensor.StartGettingValues();
            Thread.Sleep(1000);
            var values = stubSensor.StopGettingValues();

            // Assert
            Assert.AreEqual(0, values.Count);
        }
    }
}
