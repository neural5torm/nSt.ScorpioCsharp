using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.Stubs;

using nSt.NxtControlLib;
using System.Diagnostics;


namespace UnitTests
{
    [TestClass]
    public class UltrasonicSensorUnitTest
    {
        //object semaphore = 1;

        private Scorpio scorpio = new Scorpio();
        private Scorpio Scorpio
        {
            get
            {
                if (!scorpio.IsConnectionStarted)
                {
                    lock (scorpio)
                    {
                        if (!scorpio.IsConnectionStarted)
                            while (!scorpio.TryConnect())
                            { Debug.WriteLine(GetType().Name + ": Attempted connection to NXT Brick..."); }
                    }
                }
                return scorpio;
            }
        }

        private StubNxtBrick stubBrick;
        private StubNxtBrick StubBrick
        {
            get
            {
                if (stubBrick == null)
                {
                    stubBrick = new StubNxtBrick();
                    stubBrick.Connect("COMstub");
                }
                return stubBrick;
            }
        }
        

        
        // Stub sensor unit tests
        [TestMethod]
        public void StubSensorReturnsValuesBetween0And255()
        {
            // Arrange
            var stubSensor = new StubUltrasonicSensor(StubBrick, NxtBrick.Sensor.First);
            stubSensor.InitSensor();

            // Act 
            // start sensing
            stubSensor.StartGettingValues();
            Thread.Sleep(1000);
            var values = stubSensor.StopGettingValues();

            // Assert
            Assert.AreNotEqual(0, values.Count);
            Assert.IsTrue(values.All((value) => value >= stubSensor.MinSensorValue && value <= stubSensor.MaxSensorValue));

        }

        [TestMethod]
        public void StubSensorReturnsNothingWhenDisconnected()
        {
            // Arrange
            var stubSensor = new StubUltrasonicSensor(StubBrick, NxtBrick.Sensor.First);
            StubBrick.ConnectionFckedUp = true;
            stubSensor.InitSensor();

            // Act 
            // start sensing
            stubSensor.StartGettingValues();
            Thread.Sleep(1000);
            var values = stubSensor.StopGettingValues();

            // Assert
            Assert.AreEqual(0, values.Count);
        }



        // Scorpio sensor unit tests
        [TestMethod]
        public void SensorReturnsValuesBetween0And255()
        {
            // Arrange
            
            // Act
            try
            {
                // start sensing
                Scorpio.UltrasonicSensor.StartGettingValues(TimeSpan.FromMilliseconds(100));
                Thread.Sleep(1000);
                var values = Scorpio.UltrasonicSensor.StopGettingValues();

                // Assert
                Assert.AreNotEqual(0, values.Count);
                Assert.IsTrue(values.All((value) => value >= 0 && value <= 255));
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
            finally
            {
                scorpio.DisconnectAll();
            }
        }

        [TestMethod]
        public void SensorReturnsNotOnly0Values()
        {
            // Arrange
            
            // Act
            try
            {
                // start sensing
                Scorpio.UltrasonicSensor.StartGettingValues(TimeSpan.FromMilliseconds(100));
                Thread.Sleep(1000);
                var values = Scorpio.UltrasonicSensor.StopGettingValues();
        
                // Assert
                Assert.AreNotEqual(0, values.Count);
                Assert.IsFalse(values.All((value) => value == 0));
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
            finally
            {
                Scorpio.DisconnectAll();
            }

        }

        

    }
}
