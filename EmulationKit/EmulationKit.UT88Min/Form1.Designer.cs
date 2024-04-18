namespace EmulationKit.UT88Min
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ResetButton = new Button();
            TickButton = new Button();
            GeneratorControls = new GroupBox();
            StopButton = new Button();
            CycleNo = new Label();
            label1 = new Label();
            manualRB = new RadioButton();
            automaticRB = new RadioButton();
            groupBox1 = new GroupBox();
            splitContainer1 = new SplitContainer();
            registersView = new ListView();
            registerName = new ColumnHeader();
            registerValue = new ColumnHeader();
            flagsView = new ListView();
            flagName = new ColumnHeader();
            flagValue = new ColumnHeader();
            groupBox2 = new GroupBox();
            button1 = new Button();
            KeyboardF = new Button();
            KeyboardE = new Button();
            KeyboardD = new Button();
            KeyboardC = new Button();
            KeyboardB = new Button();
            KeyboardA = new Button();
            Keyboard9 = new Button();
            Keyboard8 = new Button();
            Keyboard7 = new Button();
            Keyboard6 = new Button();
            Keyboard5 = new Button();
            Keyboard4 = new Button();
            Keyboard3 = new Button();
            Keyboard2 = new Button();
            Keyboard1 = new Button();
            Keyboard0 = new Button();
            indicators = new Label();
            groupBox3 = new GroupBox();
            timer1 = new System.Windows.Forms.Timer(components);
            GeneratorControls.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // ResetButton
            // 
            ResetButton.Location = new Point(15, 71);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(75, 23);
            ResetButton.TabIndex = 0;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = true;
            ResetButton.Click += ResetButton_Click;
            // 
            // TickButton
            // 
            TickButton.Location = new Point(111, 71);
            TickButton.Name = "TickButton";
            TickButton.Size = new Size(75, 23);
            TickButton.TabIndex = 1;
            TickButton.Text = "Tick";
            TickButton.UseVisualStyleBackColor = true;
            TickButton.Click += TickButton_Click;
            // 
            // GeneratorControls
            // 
            GeneratorControls.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GeneratorControls.Controls.Add(StopButton);
            GeneratorControls.Controls.Add(CycleNo);
            GeneratorControls.Controls.Add(label1);
            GeneratorControls.Controls.Add(manualRB);
            GeneratorControls.Controls.Add(automaticRB);
            GeneratorControls.Controls.Add(ResetButton);
            GeneratorControls.Controls.Add(TickButton);
            GeneratorControls.Location = new Point(916, 12);
            GeneratorControls.Name = "GeneratorControls";
            GeneratorControls.Size = new Size(200, 186);
            GeneratorControls.TabIndex = 2;
            GeneratorControls.TabStop = false;
            GeneratorControls.Text = "Generator";
            // 
            // StopButton
            // 
            StopButton.Location = new Point(111, 104);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(75, 23);
            StopButton.TabIndex = 6;
            StopButton.Text = "Stop";
            StopButton.UseVisualStyleBackColor = true;
            StopButton.Click += StopButton_Click;
            // 
            // CycleNo
            // 
            CycleNo.AutoSize = true;
            CycleNo.Location = new Point(111, 141);
            CycleNo.Name = "CycleNo";
            CycleNo.Size = new Size(13, 15);
            CycleNo.TabIndex = 5;
            CycleNo.Text = "0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 141);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 4;
            label1.Text = "Cycle NO: ";
            // 
            // manualRB
            // 
            manualRB.AutoSize = true;
            manualRB.Checked = true;
            manualRB.Location = new Point(111, 30);
            manualRB.Name = "manualRB";
            manualRB.Size = new Size(65, 19);
            manualRB.TabIndex = 3;
            manualRB.TabStop = true;
            manualRB.Text = "Manual";
            manualRB.UseVisualStyleBackColor = true;
            // 
            // automaticRB
            // 
            automaticRB.AutoSize = true;
            automaticRB.Location = new Point(15, 30);
            automaticRB.Name = "automaticRB";
            automaticRB.Size = new Size(81, 19);
            automaticRB.TabIndex = 2;
            automaticRB.TabStop = true;
            automaticRB.Text = "Automatic";
            automaticRB.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox1.Controls.Add(splitContainer1);
            groupBox1.Location = new Point(916, 214);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 436);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Registers and Flags";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 19);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(registersView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(flagsView);
            splitContainer1.Size = new Size(194, 414);
            splitContainer1.SplitterDistance = 200;
            splitContainer1.TabIndex = 0;
            // 
            // registersView
            // 
            registersView.Columns.AddRange(new ColumnHeader[] { registerName, registerValue });
            registersView.Dock = DockStyle.Fill;
            registersView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            registersView.Location = new Point(0, 0);
            registersView.Name = "registersView";
            registersView.Size = new Size(194, 200);
            registersView.TabIndex = 0;
            registersView.UseCompatibleStateImageBehavior = false;
            registersView.View = View.Details;
            // 
            // registerName
            // 
            registerName.Text = "Name";
            // 
            // registerValue
            // 
            registerValue.Text = "Value";
            // 
            // flagsView
            // 
            flagsView.Columns.AddRange(new ColumnHeader[] { flagName, flagValue });
            flagsView.Dock = DockStyle.Fill;
            flagsView.Location = new Point(0, 0);
            flagsView.Name = "flagsView";
            flagsView.Size = new Size(194, 210);
            flagsView.TabIndex = 0;
            flagsView.UseCompatibleStateImageBehavior = false;
            flagsView.View = View.Details;
            // 
            // flagName
            // 
            flagName.Text = "Name";
            // 
            // flagValue
            // 
            flagValue.Text = "Value";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(KeyboardF);
            groupBox2.Controls.Add(KeyboardE);
            groupBox2.Controls.Add(KeyboardD);
            groupBox2.Controls.Add(KeyboardC);
            groupBox2.Controls.Add(KeyboardB);
            groupBox2.Controls.Add(KeyboardA);
            groupBox2.Controls.Add(Keyboard9);
            groupBox2.Controls.Add(Keyboard8);
            groupBox2.Controls.Add(Keyboard7);
            groupBox2.Controls.Add(Keyboard6);
            groupBox2.Controls.Add(Keyboard5);
            groupBox2.Controls.Add(Keyboard4);
            groupBox2.Controls.Add(Keyboard3);
            groupBox2.Controls.Add(Keyboard2);
            groupBox2.Controls.Add(Keyboard1);
            groupBox2.Controls.Add(Keyboard0);
            groupBox2.Controls.Add(indicators);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(898, 186);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "UT-88 Controls";
            // 
            // button1
            // 
            button1.Location = new Point(817, 22);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 17;
            button1.Text = "ShowStack";
            button1.UseVisualStyleBackColor = true;
            button1.Click += ShowStack_Click;
            // 
            // KeyboardF
            // 
            KeyboardF.Location = new Point(399, 96);
            KeyboardF.Name = "KeyboardF";
            KeyboardF.Size = new Size(30, 31);
            KeyboardF.TabIndex = 16;
            KeyboardF.Tag = "15";
            KeyboardF.Text = "F";
            KeyboardF.UseVisualStyleBackColor = true;
            KeyboardF.Click += Keyboard_Click;
            // 
            // KeyboardE
            // 
            KeyboardE.Location = new Point(363, 96);
            KeyboardE.Name = "KeyboardE";
            KeyboardE.Size = new Size(30, 31);
            KeyboardE.TabIndex = 15;
            KeyboardE.Tag = "14";
            KeyboardE.Text = "E";
            KeyboardE.UseVisualStyleBackColor = true;
            KeyboardE.Click += Keyboard_Click;
            // 
            // KeyboardD
            // 
            KeyboardD.Location = new Point(399, 59);
            KeyboardD.Name = "KeyboardD";
            KeyboardD.Size = new Size(30, 31);
            KeyboardD.TabIndex = 14;
            KeyboardD.Tag = "13";
            KeyboardD.Text = "D";
            KeyboardD.UseVisualStyleBackColor = true;
            KeyboardD.Click += Keyboard_Click;
            // 
            // KeyboardC
            // 
            KeyboardC.Location = new Point(363, 59);
            KeyboardC.Name = "KeyboardC";
            KeyboardC.Size = new Size(30, 31);
            KeyboardC.TabIndex = 13;
            KeyboardC.Tag = "12";
            KeyboardC.Text = "C";
            KeyboardC.UseVisualStyleBackColor = true;
            KeyboardC.Click += Keyboard_Click;
            // 
            // KeyboardB
            // 
            KeyboardB.Location = new Point(399, 22);
            KeyboardB.Name = "KeyboardB";
            KeyboardB.Size = new Size(30, 31);
            KeyboardB.TabIndex = 12;
            KeyboardB.Tag = "11";
            KeyboardB.Text = "B";
            KeyboardB.UseVisualStyleBackColor = true;
            KeyboardB.Click += Keyboard_Click;
            // 
            // KeyboardA
            // 
            KeyboardA.Location = new Point(363, 22);
            KeyboardA.Name = "KeyboardA";
            KeyboardA.Size = new Size(30, 31);
            KeyboardA.TabIndex = 11;
            KeyboardA.Tag = "10";
            KeyboardA.Text = "A";
            KeyboardA.UseVisualStyleBackColor = true;
            KeyboardA.Click += Keyboard_Click;
            // 
            // Keyboard9
            // 
            Keyboard9.Location = new Point(304, 22);
            Keyboard9.Name = "Keyboard9";
            Keyboard9.Size = new Size(30, 31);
            Keyboard9.TabIndex = 10;
            Keyboard9.Tag = "9";
            Keyboard9.Text = "9";
            Keyboard9.UseVisualStyleBackColor = true;
            Keyboard9.Click += Keyboard_Click;
            // 
            // Keyboard8
            // 
            Keyboard8.Location = new Point(268, 22);
            Keyboard8.Name = "Keyboard8";
            Keyboard8.Size = new Size(30, 31);
            Keyboard8.TabIndex = 9;
            Keyboard8.Tag = "8";
            Keyboard8.Text = "8";
            Keyboard8.UseVisualStyleBackColor = true;
            Keyboard8.Click += Keyboard_Click;
            // 
            // Keyboard7
            // 
            Keyboard7.Location = new Point(232, 22);
            Keyboard7.Name = "Keyboard7";
            Keyboard7.Size = new Size(30, 31);
            Keyboard7.TabIndex = 8;
            Keyboard7.Tag = "7";
            Keyboard7.Text = "7";
            Keyboard7.UseVisualStyleBackColor = true;
            Keyboard7.Click += Keyboard_Click;
            // 
            // Keyboard6
            // 
            Keyboard6.Location = new Point(304, 59);
            Keyboard6.Name = "Keyboard6";
            Keyboard6.Size = new Size(30, 31);
            Keyboard6.TabIndex = 7;
            Keyboard6.Tag = "6";
            Keyboard6.Text = "6";
            Keyboard6.UseVisualStyleBackColor = true;
            Keyboard6.Click += Keyboard_Click;
            // 
            // Keyboard5
            // 
            Keyboard5.Location = new Point(268, 59);
            Keyboard5.Name = "Keyboard5";
            Keyboard5.Size = new Size(30, 31);
            Keyboard5.TabIndex = 6;
            Keyboard5.Tag = "5";
            Keyboard5.Text = "5";
            Keyboard5.UseVisualStyleBackColor = true;
            Keyboard5.Click += Keyboard_Click;
            // 
            // Keyboard4
            // 
            Keyboard4.Location = new Point(232, 59);
            Keyboard4.Name = "Keyboard4";
            Keyboard4.Size = new Size(30, 31);
            Keyboard4.TabIndex = 5;
            Keyboard4.Tag = "4";
            Keyboard4.Text = "4";
            Keyboard4.UseVisualStyleBackColor = true;
            Keyboard4.Click += Keyboard_Click;
            // 
            // Keyboard3
            // 
            Keyboard3.Location = new Point(304, 96);
            Keyboard3.Name = "Keyboard3";
            Keyboard3.Size = new Size(30, 31);
            Keyboard3.TabIndex = 4;
            Keyboard3.Tag = "3";
            Keyboard3.Text = "3";
            Keyboard3.UseVisualStyleBackColor = true;
            Keyboard3.Click += Keyboard_Click;
            // 
            // Keyboard2
            // 
            Keyboard2.Location = new Point(268, 96);
            Keyboard2.Name = "Keyboard2";
            Keyboard2.Size = new Size(30, 31);
            Keyboard2.TabIndex = 3;
            Keyboard2.Tag = "2";
            Keyboard2.Text = "2";
            Keyboard2.UseVisualStyleBackColor = true;
            Keyboard2.Click += Keyboard_Click;
            // 
            // Keyboard1
            // 
            Keyboard1.Location = new Point(232, 96);
            Keyboard1.Name = "Keyboard1";
            Keyboard1.Size = new Size(30, 31);
            Keyboard1.TabIndex = 2;
            Keyboard1.Tag = "1";
            Keyboard1.Text = "1";
            Keyboard1.UseVisualStyleBackColor = true;
            Keyboard1.Click += Keyboard_Click;
            // 
            // Keyboard0
            // 
            Keyboard0.Location = new Point(232, 133);
            Keyboard0.Name = "Keyboard0";
            Keyboard0.Size = new Size(102, 31);
            Keyboard0.TabIndex = 1;
            Keyboard0.Tag = "0";
            Keyboard0.Text = "0";
            Keyboard0.UseVisualStyleBackColor = true;
            Keyboard0.Click += Keyboard_Click;
            // 
            // indicators
            // 
            indicators.AutoSize = true;
            indicators.Font = new Font("Segoe UI", 40F);
            indicators.Location = new Point(6, 22);
            indicators.Name = "indicators";
            indicators.Size = new Size(204, 72);
            indicators.TabIndex = 0;
            indicators.Text = "000000";
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Location = new Point(12, 214);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(898, 433);
            groupBox3.TabIndex = 5;
            groupBox3.TabStop = false;
            groupBox3.Text = "Execution Log";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1128, 662);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(GeneratorControls);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            GeneratorControls.ResumeLayout(false);
            GeneratorControls.PerformLayout();
            groupBox1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button ResetButton;
        private Button TickButton;
        private GroupBox GeneratorControls;
        private RadioButton manualRB;
        private RadioButton automaticRB;
        private Label CycleNo;
        private Label label1;
        private GroupBox groupBox1;
        private SplitContainer splitContainer1;
        private ListView registersView;
        private ColumnHeader registerName;
        private ColumnHeader registerValue;
        private ListView flagsView;
        private ColumnHeader flagName;
        private ColumnHeader flagValue;
        private GroupBox groupBox2;
        private Label indicators;
        private Button Keyboard0;
        private Button Keyboard8;
        private Button Keyboard7;
        private Button Keyboard6;
        private Button Keyboard5;
        private Button Keyboard4;
        private Button Keyboard3;
        private Button Keyboard2;
        private Button Keyboard1;
        private Button KeyboardF;
        private Button KeyboardE;
        private Button KeyboardD;
        private Button KeyboardC;
        private Button KeyboardB;
        private Button KeyboardA;
        private Button Keyboard9;
        private GroupBox groupBox3;
        private Button StopButton;
        private Button button1;
        private System.Windows.Forms.Timer timer1;
    }
}
