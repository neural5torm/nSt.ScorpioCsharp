using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using nSt.NxtControlLib;
using nSt.NxtControlLib.Input;


namespace NxtControlTestWindow
{
    // TODOnow create complex Behavior classes (see UltrasonicSensor_OnChange handler)
    // TOOD use keyboard input to learn complex behaviors (create a KeyEnvLearningBehavior class?)
    public partial class TestForm : Form
    {
        Scorpio1 scorpio = new Scorpio1();

        int previousUltrasonicValue = 255;

        const string START = "Start";
        const string STOP = "Stop";

        const int COLLISION_DIST = 30;
        const int MIN_LIGHT_LEVEL = 15;

        public bool Refreshing { get; private set; }


        public TestForm()
        {
            InitializeComponent();

        }


        #region Test UI methods
        private void ConnectUI()
        {
            // Sensors:
            // light
            lightProgressBar.Minimum = scorpio.LightIntensitySensor.MinSensorVal;
            lightProgressBar.Maximum = scorpio.LightIntensitySensor.MaxSensorVal;
            scorpio.LightIntensitySensor.OnChange += new PollingSensor<ushort>.SensorEventHandler(LightSensor_OnChange);
            scorpio.LightIntensitySensor.StartSensing();
            // sound level
            soundLevelProgressBar.Minimum = scorpio.SoundLevelSensor.MinSensorVal;
            soundLevelProgressBar.Maximum = scorpio.SoundLevelSensor.MaxSensorVal;
            if (soundLevelProgressBar.Enabled)
            {
                scorpio.SoundLevelSensor.OnChange += new PollingSensor<ushort>.SensorEventHandler(SoundLevelSensor_OnChange);
                scorpio.SoundLevelSensor.StartSensing();
            }
            // sound ratio
            soundRatioProgressBar.Minimum = (int)(scorpio.SoundRatioSensor.MinSensorVal * 100.0);
            soundRatioProgressBar.Maximum = (int)(scorpio.SoundRatioSensor.MaxSensorVal * 100.0);
            if (soundRatioProgressBar.Enabled)
            {
                scorpio.SoundRatioSensor.OnChange += new PollingSensor<double>.SensorEventHandler(SoundRatioSensor_OnChange);
                scorpio.SoundRatioSensor.StartSensing();
            }
            // ultrasonic distance
            ultrasonicProgressBar.Minimum = scorpio.UltrasonicSensor.MinSensorVal;
            ultrasonicProgressBar.Maximum = scorpio.UltrasonicSensor.MaxSensorVal;
            scorpio.UltrasonicSensor.OnChange += new PollingSensor<int>.SensorEventHandler(UltrasonicSensor_OnChange);
            scorpio.UltrasonicSensor.StartSensing();
            // touch
            touchCheckBox.Checked = scorpio.TouchSensor.MinSensorVal;
            scorpio.TouchSensor.OnChange += new PollingSensor<bool>.SensorEventHandler(TouchSensor_OnChange);
            scorpio.TouchSensor.StartSensing();
            // Motors:
        }

        private void Disconnect()
        {
            // Reinitialize UI controls
            scorpio.LightIntensitySensor.OnChange -= LightSensor_OnChange;
            scorpio.SoundLevelSensor.OnChange -= SoundLevelSensor_OnChange;
            scorpio.SoundRatioSensor.OnChange -= SoundRatioSensor_OnChange;
            scorpio.UltrasonicSensor.OnChange -= UltrasonicSensor_OnChange;
            scorpio.TouchSensor.OnChange -= TouchSensor_OnChange;

            scorpio.DisconnectAll();

            lightProgressBar.Value = 0;
            soundLevelProgressBar.Value = 0;
            soundRatioProgressBar.Value = 0;
            ultrasonicProgressBar.Value = 0;
            touchCheckBox.Checked = false;
            touchProgressBar.Value = 0;
        }

        private void HandleMotors()
        {
            if (!scorpio.IsConnectionStarted)
                return;

            // Walk Motors
            if (checkBoxLeftMotor.Checked)
                scorpio.MotorLeft.Start(Convert.ToUInt32(trackBar1.Value), !checkBoxLeftReverse.Checked);
            else if (scorpio.MotorLeft.IsRunning)
                scorpio.MotorLeft.Stop();

            if (checkBoxRightMotor.Checked)
                scorpio.MotorRight.Start(Convert.ToUInt32(trackBar1.Value), !checkBoxRightReverse.Checked);
            else if (scorpio.MotorRight.IsRunning)
                scorpio.MotorRight.Stop();

            // Tail Motor
            if (checkBoxTailMotor.Checked)
                scorpio.MotorTail.Start(Convert.ToUInt32(trackBar1.Value), !checkBoxTailReverse.Checked);
            else if (scorpio.MotorTail.IsRunning)
                scorpio.MotorTail.Stop();

        }

        private void HandleWalk()
        {
            if (!scorpio.IsConnectionStarted)
                return;

            // Walk
            if (checkBoxWalk.Checked)
            {
                foreach (RadioButton control in groupBox3.Controls)
                {
                    if (control.Checked)
                        switch (control.Name.Replace("radioButton", string.Empty))
                        {
                            case "Ahead":
                                scorpio.WalkBehavior.Walk(trackBar1.Value);
                                RefreshUI(true, true);
                                break;
                            case "TurnLeft":
                                scorpio.WalkBehavior.Turn(false, trackBar1.Value);
                                RefreshUI(true, true);
                                break;
                            case "TurnRight":
                                scorpio.WalkBehavior.Turn(true, trackBar1.Value);
                                RefreshUI(true, true);
                                break;
                            case "TurnAroundLeft":
                                scorpio.WalkBehavior.TurnAround(false, trackBar1.Value);
                                RefreshUI(false, true);
                                break;
                            case "TurnAroundRight":
                                scorpio.WalkBehavior.TurnAround(true, trackBar1.Value);
                                RefreshUI(true, false);
                                break;
                            case "Back":
                                scorpio.WalkBehavior.WalkReverse(trackBar1.Value);
                                RefreshUI(false, false);
                                break;
                            default:
                                break;
                        }
                }
            }
            else if (scorpio.WalkBehavior.IsWalking)
            {
                scorpio.WalkBehavior.Stop();
            }
        }

        private void RefreshUI(bool leftForward, bool rightForward)
        {
            Refreshing = true;

            checkBoxLeftMotor.Checked = scorpio.MotorLeft.IsRunning;
            checkBoxRightMotor.Checked = scorpio.MotorRight.IsRunning;
            
            // TODO determine the current rotation direction/power directly from the IMotor or MotorSensor
            checkBoxLeftReverse.Checked = !leftForward;
            checkBoxRightReverse.Checked = !rightForward;

            checkBoxWalk.Checked = scorpio.WalkBehavior.IsWalking;

            Refreshing = false;
        }
        #endregion


        #region Sensor callbacks
        void LightSensor_OnChange(object sender, SensorEventArgs<ushort> e)
        {
            lightProgressBar.Value = e.SensorVal;
            //Debug.WriteLine("New light level value: " + e.SensorVal);

            if (e.SensorVal < MIN_LIGHT_LEVEL && checkBoxLight.Checked)
            {
                button1_Click(sender, e);
                MessageBox.Show("Good night!");
            }
        }

        void SoundLevelSensor_OnChange(object sender, SensorEventArgs<ushort> e)
        {
            soundLevelProgressBar.Value = e.SensorVal;
            //Debug.WriteLine("New sound level value: " + e.SensorVal);
        }

        void SoundRatioSensor_OnChange(object sender, SensorEventArgs<double> e)
        {
            soundRatioProgressBar.Value = (int)(e.SensorVal * 100.0);
            //Debug.WriteLine("New sound level value: " + e.SensorVal);
        }

        void UltrasonicSensor_OnChange(object sender, SensorEventArgs<int> e)
        {
            ultrasonicProgressBar.Value = e.SensorVal;
            Debug.WriteLine("New ultrasound distance value: approx " + e.SensorVal + " cm");

            if (checkBoxCollision.Checked
                && scorpio.MotorLeft.IsRunning && scorpio.MotorRight.IsRunning
                && e.SensorVal < COLLISION_DIST && previousUltrasonicValue < COLLISION_DIST)
            {
                // TODOnow Create complex behavior to handle random turn+walk away
                // for now, just walk back
                checkBoxRightReverse.Checked = checkBoxLeftReverse.Checked = true;

                previousUltrasonicValue = int.MaxValue;
            }
            else if (previousUltrasonicValue == int.MaxValue)
            {
                if (e.SensorVal > COLLISION_DIST)
                    previousUltrasonicValue = e.SensorVal;
            }
            else
                previousUltrasonicValue = e.SensorVal;
            Debug.WriteLine("previous ultrasonic value: " + previousUltrasonicValue);
        }

        void TouchSensor_OnChange(object sender, SensorEventArgs<bool> e)
        {
            touchCheckBox.Checked = e.SensorVal;
            //Debug.WriteLine("New touch sensor value: pressed=" + e.SensorVal);

            if (checkBoxTouch.Checked && e.SensorVal)
            {
                // Stop all possible motors
                checkBoxLeftMotor.Checked =
                    checkBoxRightMotor.Checked =
                    checkBoxTailMotor.Checked =
                    checkBoxWalk.Checked = false;
            }
        }
        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = string.Empty;
            button1.Enabled = false;

            if (button1.Text == START)
            {
                if (scorpio.TryConnect())
                {
                    // Connect UI to nxtControl 
                    ConnectUI();

                    button1.Text = STOP;

                    HandleWalk();
                    HandleMotors();
                }
                else
                    toolStripStatusLabel1.Text = "Could not connect to Scorpio: switch it on or bring it closer.";

            }
            else
            {                
                Disconnect();

                button1.Text = START;
            }

            button1.Enabled = true;
        }

        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
            button1.Text = START;
        }

        private void checkBoxMotor_CheckedChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
                HandleMotors();
        }

        private void checkBoxWalk_CheckedChanged(object sender, EventArgs e)
        {
            HandleWalk();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxWalk.Checked)
                HandleWalk();
            else
                HandleMotors();
        }


        private void radioButtonWalk_CheckedChanged(object sender, EventArgs e)
        {
            HandleWalk();
        }

        private void radioButtonWalk_Click(object sender, EventArgs e)
        {
            HandleWalk();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (checkBoxKeyboardControl.Checked)
            {
                switch (keyData)
                {
                    case Keys.S:
                        radioButtonBack.Checked = true;
                        break;
                    case Keys.Q:
                        radioButtonTurnAroundLeft.Checked = true;
                        break;
                    case Keys.D:
                        radioButtonTurnAroundRight.Checked = true;
                        break;
                    case Keys.Z:
                        radioButtonAhead.Checked = true;
                        break;
                    case Keys.A:
                        radioButtonTurnLeft.Checked = true;
                        break;
                    case Keys.E:
                        radioButtonTurnRight.Checked = true;
                        break;
                    case Keys.X:
                        checkBoxWalk.Checked = !checkBoxWalk.Checked;
                        break;
                    case Keys.Space:
                        checkBoxWalk.Checked = false;
                        checkBoxLeftMotor.Checked = false;
                        checkBoxRightMotor.Checked = false;
                        checkBoxTailMotor.Checked = false;
                        break;
                    default:
                        return base.ProcessCmdKey(ref msg, keyData);
                }
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }      

    } // Form class
} 
