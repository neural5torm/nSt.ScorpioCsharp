namespace NxtControlTestWindow
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxTouch = new System.Windows.Forms.CheckBox();
            this.checkBoxLight = new System.Windows.Forms.CheckBox();
            this.checkBoxCollision = new System.Windows.Forms.CheckBox();
            this.touchCheckBox = new System.Windows.Forms.CheckBox();
            this.soundRatioProgressBar = new System.Windows.Forms.ProgressBar();
            this.touchProgressBar = new System.Windows.Forms.ProgressBar();
            this.ultrasonicProgressBar = new System.Windows.Forms.ProgressBar();
            this.soundLevelProgressBar = new System.Windows.Forms.ProgressBar();
            this.lightProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonBack = new System.Windows.Forms.RadioButton();
            this.radioButtonTurnAroundRight = new System.Windows.Forms.RadioButton();
            this.radioButtonTurnAroundLeft = new System.Windows.Forms.RadioButton();
            this.radioButtonTurnRight = new System.Windows.Forms.RadioButton();
            this.radioButtonTurnLeft = new System.Windows.Forms.RadioButton();
            this.radioButtonAhead = new System.Windows.Forms.RadioButton();
            this.checkBoxWalk = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.checkBoxTailReverse = new System.Windows.Forms.CheckBox();
            this.checkBoxRightReverse = new System.Windows.Forms.CheckBox();
            this.checkBoxLeftReverse = new System.Windows.Forms.CheckBox();
            this.checkBoxTailMotor = new System.Windows.Forms.CheckBox();
            this.checkBoxRightMotor = new System.Windows.Forms.CheckBox();
            this.checkBoxLeftMotor = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBoxKeyboardControl = new System.Windows.Forms.CheckBox();
            label3 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Enabled = false;
            label3.Location = new System.Drawing.Point(6, 63);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(61, 13);
            label3.TabIndex = 13;
            label3.Text = "Sound ratio";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 104);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(38, 13);
            label5.TabIndex = 9;
            label5.Text = "Touch";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 83);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(54, 13);
            label4.TabIndex = 8;
            label4.Text = "Ultrasonic";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Enabled = false;
            label2.Location = new System.Drawing.Point(6, 42);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(63, 13);
            label2.TabIndex = 5;
            label2.Text = "Sound level";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 21);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(55, 13);
            label1.TabIndex = 3;
            label1.Text = "Light level";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(127, 369);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 32);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxTouch);
            this.groupBox1.Controls.Add(this.checkBoxLight);
            this.groupBox1.Controls.Add(this.checkBoxCollision);
            this.groupBox1.Controls.Add(this.touchCheckBox);
            this.groupBox1.Controls.Add(label3);
            this.groupBox1.Controls.Add(this.soundRatioProgressBar);
            this.groupBox1.Controls.Add(this.touchProgressBar);
            this.groupBox1.Controls.Add(this.ultrasonicProgressBar);
            this.groupBox1.Controls.Add(label5);
            this.groupBox1.Controls.Add(label4);
            this.groupBox1.Controls.Add(label2);
            this.groupBox1.Controls.Add(this.soundLevelProgressBar);
            this.groupBox1.Controls.Add(label1);
            this.groupBox1.Controls.Add(this.lightProgressBar);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 149);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sensors";
            // 
            // checkBoxTouch
            // 
            this.checkBoxTouch.AutoEllipsis = true;
            this.checkBoxTouch.Checked = true;
            this.checkBoxTouch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTouch.Location = new System.Drawing.Point(126, 124);
            this.checkBoxTouch.Name = "checkBoxTouch";
            this.checkBoxTouch.Size = new System.Drawing.Size(108, 16);
            this.checkBoxTouch.TabIndex = 17;
            this.checkBoxTouch.Text = "Touch detection";
            this.checkBoxTouch.UseVisualStyleBackColor = true;
            // 
            // checkBoxLight
            // 
            this.checkBoxLight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxLight.Checked = true;
            this.checkBoxLight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLight.Location = new System.Drawing.Point(237, 123);
            this.checkBoxLight.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxLight.Name = "checkBoxLight";
            this.checkBoxLight.Size = new System.Drawing.Size(98, 18);
            this.checkBoxLight.TabIndex = 16;
            this.checkBoxLight.Text = "Light detection";
            this.checkBoxLight.UseVisualStyleBackColor = true;
            // 
            // checkBoxCollision
            // 
            this.checkBoxCollision.AutoSize = true;
            this.checkBoxCollision.Checked = true;
            this.checkBoxCollision.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCollision.Location = new System.Drawing.Point(9, 124);
            this.checkBoxCollision.Name = "checkBoxCollision";
            this.checkBoxCollision.Size = new System.Drawing.Size(111, 17);
            this.checkBoxCollision.TabIndex = 15;
            this.checkBoxCollision.Text = "Collision detection";
            this.checkBoxCollision.UseVisualStyleBackColor = true;
            // 
            // touchCheckBox
            // 
            this.touchCheckBox.AutoSize = true;
            this.touchCheckBox.Location = new System.Drawing.Point(77, 104);
            this.touchCheckBox.Name = "touchCheckBox";
            this.touchCheckBox.Size = new System.Drawing.Size(15, 14);
            this.touchCheckBox.TabIndex = 14;
            this.touchCheckBox.UseVisualStyleBackColor = true;
            // 
            // soundRatioProgressBar
            // 
            this.soundRatioProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.soundRatioProgressBar.Enabled = false;
            this.soundRatioProgressBar.Location = new System.Drawing.Point(77, 61);
            this.soundRatioProgressBar.Name = "soundRatioProgressBar";
            this.soundRatioProgressBar.Size = new System.Drawing.Size(258, 15);
            this.soundRatioProgressBar.TabIndex = 12;
            // 
            // touchProgressBar
            // 
            this.touchProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.touchProgressBar.Location = new System.Drawing.Point(114, 102);
            this.touchProgressBar.Name = "touchProgressBar";
            this.touchProgressBar.Size = new System.Drawing.Size(221, 15);
            this.touchProgressBar.TabIndex = 11;
            this.touchProgressBar.Visible = false;
            // 
            // ultrasonicProgressBar
            // 
            this.ultrasonicProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ultrasonicProgressBar.Location = new System.Drawing.Point(77, 81);
            this.ultrasonicProgressBar.Name = "ultrasonicProgressBar";
            this.ultrasonicProgressBar.Size = new System.Drawing.Size(258, 15);
            this.ultrasonicProgressBar.TabIndex = 10;
            // 
            // soundLevelProgressBar
            // 
            this.soundLevelProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.soundLevelProgressBar.Enabled = false;
            this.soundLevelProgressBar.Location = new System.Drawing.Point(77, 40);
            this.soundLevelProgressBar.Name = "soundLevelProgressBar";
            this.soundLevelProgressBar.Size = new System.Drawing.Size(258, 15);
            this.soundLevelProgressBar.TabIndex = 4;
            // 
            // lightProgressBar
            // 
            this.lightProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lightProgressBar.Location = new System.Drawing.Point(77, 19);
            this.lightProgressBar.Name = "lightProgressBar";
            this.lightProgressBar.Size = new System.Drawing.Size(258, 15);
            this.lightProgressBar.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.checkBoxWalk);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.trackBar1);
            this.groupBox2.Controls.Add(this.checkBoxTailReverse);
            this.groupBox2.Controls.Add(this.checkBoxRightReverse);
            this.groupBox2.Controls.Add(this.checkBoxLeftReverse);
            this.groupBox2.Controls.Add(this.checkBoxTailMotor);
            this.groupBox2.Controls.Add(this.checkBoxRightMotor);
            this.groupBox2.Controls.Add(this.checkBoxLeftMotor);
            this.groupBox2.Location = new System.Drawing.Point(13, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(341, 195);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Motors";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.radioButtonBack);
            this.groupBox3.Controls.Add(this.radioButtonTurnAroundRight);
            this.groupBox3.Controls.Add(this.radioButtonTurnAroundLeft);
            this.groupBox3.Controls.Add(this.radioButtonTurnRight);
            this.groupBox3.Controls.Add(this.radioButtonTurnLeft);
            this.groupBox3.Controls.Add(this.radioButtonAhead);
            this.groupBox3.Location = new System.Drawing.Point(77, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(233, 103);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            // 
            // radioButtonBack
            // 
            this.radioButtonBack.AutoSize = true;
            this.radioButtonBack.Location = new System.Drawing.Point(6, 79);
            this.radioButtonBack.Name = "radioButtonBack";
            this.radioButtonBack.Size = new System.Drawing.Size(66, 17);
            this.radioButtonBack.TabIndex = 5;
            this.radioButtonBack.Text = "Back (S)";
            this.radioButtonBack.UseVisualStyleBackColor = true;
            this.radioButtonBack.CheckedChanged += new System.EventHandler(this.radioButtonWalk_CheckedChanged);
            this.radioButtonBack.Click += new System.EventHandler(this.radioButtonWalk_Click);
            // 
            // radioButtonTurnAroundRight
            // 
            this.radioButtonTurnAroundRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonTurnAroundRight.Location = new System.Drawing.Point(117, 56);
            this.radioButtonTurnAroundRight.Margin = new System.Windows.Forms.Padding(0);
            this.radioButtonTurnAroundRight.Name = "radioButtonTurnAroundRight";
            this.radioButtonTurnAroundRight.Size = new System.Drawing.Size(113, 17);
            this.radioButtonTurnAroundRight.TabIndex = 4;
            this.radioButtonTurnAroundRight.Text = "Turn around R (D)";
            this.radioButtonTurnAroundRight.UseVisualStyleBackColor = true;
            this.radioButtonTurnAroundRight.CheckedChanged += new System.EventHandler(this.radioButtonWalk_CheckedChanged);
            this.radioButtonTurnAroundRight.Click += new System.EventHandler(this.radioButtonWalk_Click);
            // 
            // radioButtonTurnAroundLeft
            // 
            this.radioButtonTurnAroundLeft.AutoSize = true;
            this.radioButtonTurnAroundLeft.Location = new System.Drawing.Point(6, 56);
            this.radioButtonTurnAroundLeft.Name = "radioButtonTurnAroundLeft";
            this.radioButtonTurnAroundLeft.Size = new System.Drawing.Size(109, 17);
            this.radioButtonTurnAroundLeft.TabIndex = 3;
            this.radioButtonTurnAroundLeft.Text = "Turn around L (Q)";
            this.radioButtonTurnAroundLeft.UseVisualStyleBackColor = true;
            this.radioButtonTurnAroundLeft.CheckedChanged += new System.EventHandler(this.radioButtonWalk_CheckedChanged);
            this.radioButtonTurnAroundLeft.Click += new System.EventHandler(this.radioButtonWalk_Click);
            // 
            // radioButtonTurnRight
            // 
            this.radioButtonTurnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonTurnRight.AutoSize = true;
            this.radioButtonTurnRight.Location = new System.Drawing.Point(117, 32);
            this.radioButtonTurnRight.Name = "radioButtonTurnRight";
            this.radioButtonTurnRight.Size = new System.Drawing.Size(91, 17);
            this.radioButtonTurnRight.TabIndex = 2;
            this.radioButtonTurnRight.Text = "Turn Right (E)";
            this.radioButtonTurnRight.UseVisualStyleBackColor = true;
            this.radioButtonTurnRight.CheckedChanged += new System.EventHandler(this.radioButtonWalk_CheckedChanged);
            this.radioButtonTurnRight.Click += new System.EventHandler(this.radioButtonWalk_Click);
            // 
            // radioButtonTurnLeft
            // 
            this.radioButtonTurnLeft.AutoSize = true;
            this.radioButtonTurnLeft.Location = new System.Drawing.Point(6, 33);
            this.radioButtonTurnLeft.Name = "radioButtonTurnLeft";
            this.radioButtonTurnLeft.Size = new System.Drawing.Size(84, 17);
            this.radioButtonTurnLeft.TabIndex = 1;
            this.radioButtonTurnLeft.Text = "Turn Left (A)";
            this.radioButtonTurnLeft.UseVisualStyleBackColor = true;
            this.radioButtonTurnLeft.CheckedChanged += new System.EventHandler(this.radioButtonWalk_CheckedChanged);
            this.radioButtonTurnLeft.Click += new System.EventHandler(this.radioButtonWalk_Click);
            // 
            // radioButtonAhead
            // 
            this.radioButtonAhead.AutoSize = true;
            this.radioButtonAhead.Checked = true;
            this.radioButtonAhead.Location = new System.Drawing.Point(6, 10);
            this.radioButtonAhead.Name = "radioButtonAhead";
            this.radioButtonAhead.Size = new System.Drawing.Size(72, 17);
            this.radioButtonAhead.TabIndex = 0;
            this.radioButtonAhead.TabStop = true;
            this.radioButtonAhead.Text = "Ahead (Z)";
            this.radioButtonAhead.UseVisualStyleBackColor = true;
            this.radioButtonAhead.CheckedChanged += new System.EventHandler(this.radioButtonWalk_CheckedChanged);
            this.radioButtonAhead.Click += new System.EventHandler(this.radioButtonWalk_Click);
            // 
            // checkBoxWalk
            // 
            this.checkBoxWalk.AutoSize = true;
            this.checkBoxWalk.Location = new System.Drawing.Point(9, 115);
            this.checkBoxWalk.Name = "checkBoxWalk";
            this.checkBoxWalk.Size = new System.Drawing.Size(67, 17);
            this.checkBoxWalk.TabIndex = 19;
            this.checkBoxWalk.Text = "Walk (X)";
            this.checkBoxWalk.UseVisualStyleBackColor = true;
            this.checkBoxWalk.CheckedChanged += new System.EventHandler(this.checkBoxWalk_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(124, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "0°";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(124, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "0°";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(124, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "0°";
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.AutoSize = false;
            this.trackBar1.LargeChange = 10;
            this.trackBar1.Location = new System.Drawing.Point(309, 11);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 50;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(32, 178);
            this.trackBar1.TabIndex = 15;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar1.Value = 90;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // checkBoxTailReverse
            // 
            this.checkBoxTailReverse.AutoSize = true;
            this.checkBoxTailReverse.Enabled = false;
            this.checkBoxTailReverse.Location = new System.Drawing.Point(77, 65);
            this.checkBoxTailReverse.Name = "checkBoxTailReverse";
            this.checkBoxTailReverse.Size = new System.Drawing.Size(41, 17);
            this.checkBoxTailReverse.TabIndex = 14;
            this.checkBoxTailReverse.Text = "rev";
            this.checkBoxTailReverse.UseVisualStyleBackColor = true;
            this.checkBoxTailReverse.CheckedChanged += new System.EventHandler(this.checkBoxMotor_CheckedChanged);
            // 
            // checkBoxRightReverse
            // 
            this.checkBoxRightReverse.AutoSize = true;
            this.checkBoxRightReverse.Location = new System.Drawing.Point(77, 42);
            this.checkBoxRightReverse.Name = "checkBoxRightReverse";
            this.checkBoxRightReverse.Size = new System.Drawing.Size(41, 17);
            this.checkBoxRightReverse.TabIndex = 14;
            this.checkBoxRightReverse.Text = "rev";
            this.checkBoxRightReverse.UseVisualStyleBackColor = true;
            this.checkBoxRightReverse.CheckedChanged += new System.EventHandler(this.checkBoxMotor_CheckedChanged);
            // 
            // checkBoxLeftReverse
            // 
            this.checkBoxLeftReverse.AutoSize = true;
            this.checkBoxLeftReverse.Location = new System.Drawing.Point(77, 19);
            this.checkBoxLeftReverse.Name = "checkBoxLeftReverse";
            this.checkBoxLeftReverse.Size = new System.Drawing.Size(41, 17);
            this.checkBoxLeftReverse.TabIndex = 14;
            this.checkBoxLeftReverse.Text = "rev";
            this.checkBoxLeftReverse.UseVisualStyleBackColor = true;
            this.checkBoxLeftReverse.CheckedChanged += new System.EventHandler(this.checkBoxMotor_CheckedChanged);
            // 
            // checkBoxTailMotor
            // 
            this.checkBoxTailMotor.AutoSize = true;
            this.checkBoxTailMotor.Enabled = false;
            this.checkBoxTailMotor.Location = new System.Drawing.Point(9, 65);
            this.checkBoxTailMotor.Name = "checkBoxTailMotor";
            this.checkBoxTailMotor.Size = new System.Drawing.Size(43, 17);
            this.checkBoxTailMotor.TabIndex = 2;
            this.checkBoxTailMotor.Text = "Tail";
            this.checkBoxTailMotor.UseVisualStyleBackColor = true;
            this.checkBoxTailMotor.CheckedChanged += new System.EventHandler(this.checkBoxMotor_CheckedChanged);
            // 
            // checkBoxRightMotor
            // 
            this.checkBoxRightMotor.AutoSize = true;
            this.checkBoxRightMotor.Location = new System.Drawing.Point(9, 42);
            this.checkBoxRightMotor.Name = "checkBoxRightMotor";
            this.checkBoxRightMotor.Size = new System.Drawing.Size(51, 17);
            this.checkBoxRightMotor.TabIndex = 1;
            this.checkBoxRightMotor.Text = "Right";
            this.checkBoxRightMotor.UseVisualStyleBackColor = true;
            this.checkBoxRightMotor.CheckedChanged += new System.EventHandler(this.checkBoxMotor_CheckedChanged);
            // 
            // checkBoxLeftMotor
            // 
            this.checkBoxLeftMotor.AutoSize = true;
            this.checkBoxLeftMotor.Location = new System.Drawing.Point(9, 19);
            this.checkBoxLeftMotor.Name = "checkBoxLeftMotor";
            this.checkBoxLeftMotor.Size = new System.Drawing.Size(44, 17);
            this.checkBoxLeftMotor.TabIndex = 0;
            this.checkBoxLeftMotor.Text = "Left";
            this.checkBoxLeftMotor.UseVisualStyleBackColor = true;
            this.checkBoxLeftMotor.CheckedChanged += new System.EventHandler(this.checkBoxMotor_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 404);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(366, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(60, 17);
            this.toolStripStatusLabel1.Text = "Welcome!";
            // 
            // checkBoxKeyboardControl
            // 
            this.checkBoxKeyboardControl.AutoSize = true;
            this.checkBoxKeyboardControl.Checked = true;
            this.checkBoxKeyboardControl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKeyboardControl.Location = new System.Drawing.Point(13, 378);
            this.checkBoxKeyboardControl.Name = "checkBoxKeyboardControl";
            this.checkBoxKeyboardControl.Size = new System.Drawing.Size(106, 17);
            this.checkBoxKeyboardControl.TabIndex = 21;
            this.checkBoxKeyboardControl.Text = "Keyboard control";
            this.checkBoxKeyboardControl.UseVisualStyleBackColor = true;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 426);
            this.Controls.Add(this.checkBoxKeyboardControl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "TestForm";
            this.Text = "NxtControl Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar lightProgressBar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar touchProgressBar;
        private System.Windows.Forms.ProgressBar ultrasonicProgressBar;
        private System.Windows.Forms.ProgressBar soundLevelProgressBar;
        private System.Windows.Forms.CheckBox checkBoxTailMotor;
        private System.Windows.Forms.CheckBox checkBoxRightMotor;
        private System.Windows.Forms.CheckBox checkBoxLeftMotor;
        private System.Windows.Forms.CheckBox checkBoxTailReverse;
        private System.Windows.Forms.CheckBox checkBoxRightReverse;
        private System.Windows.Forms.CheckBox checkBoxLeftReverse;
        private System.Windows.Forms.ProgressBar soundRatioProgressBar;
        private System.Windows.Forms.CheckBox touchCheckBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckBox checkBoxCollision;
        private System.Windows.Forms.CheckBox checkBoxLight;
        private System.Windows.Forms.CheckBox checkBoxTouch;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonBack;
        private System.Windows.Forms.RadioButton radioButtonTurnAroundRight;
        private System.Windows.Forms.RadioButton radioButtonTurnAroundLeft;
        private System.Windows.Forms.RadioButton radioButtonTurnRight;
        private System.Windows.Forms.RadioButton radioButtonTurnLeft;
        private System.Windows.Forms.RadioButton radioButtonAhead;
        private System.Windows.Forms.CheckBox checkBoxWalk;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.CheckBox checkBoxKeyboardControl;
    }
}

