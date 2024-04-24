namespace CubeSolver
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            checkBox2 = new CheckBox();
            button41 = new Button();
            button40 = new Button();
            button39 = new Button();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            hScrollBar1 = new HScrollBar();
            checkBox1 = new CheckBox();
            button44 = new Button();
            button42 = new Button();
            button35 = new Button();
            button32 = new Button();
            imageBox1 = new Emgu.CV.UI.ImageBox();
            tabPage2 = new TabPage();
            button1 = new Button();
            groupBox1 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            numericUpDown2 = new NumericUpDown();
            numericUpDown1 = new NumericUpDown();
            button37 = new Button();
            button36 = new Button();
            textBox1 = new TextBox();
            groupBox3 = new GroupBox();
            radioButton15 = new RadioButton();
            radioButton14 = new RadioButton();
            radioButton13 = new RadioButton();
            groupBox2 = new GroupBox();
            radioButton12 = new RadioButton();
            radioButton6 = new RadioButton();
            radioButton11 = new RadioButton();
            radioButton5 = new RadioButton();
            radioButton10 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton9 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton8 = new RadioButton();
            radioButton7 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            button47 = new Button();
            button46 = new Button();
            button34 = new Button();
            button38 = new Button();
            button33 = new Button();
            button31 = new Button();
            button29 = new Button();
            button30 = new Button();
            button28 = new Button();
            button8 = new Button();
            button17 = new Button();
            button18 = new Button();
            button19 = new Button();
            button20 = new Button();
            button21 = new Button();
            button22 = new Button();
            button23 = new Button();
            button24 = new Button();
            button25 = new Button();
            button26 = new Button();
            button27 = new Button();
            button9 = new Button();
            button10 = new Button();
            button11 = new Button();
            button12 = new Button();
            button16 = new Button();
            button15 = new Button();
            button13 = new Button();
            button14 = new Button();
            button3 = new Button();
            button2 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            btnConnect = new Button();
            label1 = new Label();
            comboBox1 = new ComboBox();
            button43 = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)imageBox1).BeginInit();
            tabPage2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1453, 742);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button43);
            tabPage1.Controls.Add(checkBox2);
            tabPage1.Controls.Add(button41);
            tabPage1.Controls.Add(button40);
            tabPage1.Controls.Add(button39);
            tabPage1.Controls.Add(textBox3);
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(hScrollBar1);
            tabPage1.Controls.Add(checkBox1);
            tabPage1.Controls.Add(button44);
            tabPage1.Controls.Add(button42);
            tabPage1.Controls.Add(button35);
            tabPage1.Controls.Add(button32);
            tabPage1.Controls.Add(imageBox1);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1445, 712);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "主页面";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(1195, 131);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(51, 21);
            checkBox2.TabIndex = 9;
            checkBox2.Text = "滤波";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // button41
            // 
            button41.BackColor = Color.Red;
            button41.ForeColor = Color.Black;
            button41.Location = new Point(1184, 634);
            button41.Name = "button41";
            button41.Size = new Size(106, 37);
            button41.TabIndex = 8;
            button41.Text = "急停";
            button41.UseVisualStyleBackColor = false;
            button41.Click += button41_Click;
            // 
            // button40
            // 
            button40.Location = new Point(1313, 634);
            button40.Name = "button40";
            button40.Size = new Size(106, 37);
            button40.TabIndex = 7;
            button40.Text = "弹出魔方";
            button40.UseVisualStyleBackColor = true;
            button40.Click += button40_Click;
            // 
            // button39
            // 
            button39.Location = new Point(1184, 15);
            button39.Name = "button39";
            button39.Size = new Size(106, 37);
            button39.TabIndex = 7;
            button39.Text = "自动解魔方";
            button39.UseVisualStyleBackColor = true;
            button39.Click += button39_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(1045, 427);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(374, 186);
            textBox3.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(1045, 172);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(374, 178);
            textBox2.TabIndex = 6;
            textBox2.Text = "LFBBRLRRLUFUULDUFBFBUFDLFUDRRFLRFLRDLRBULDDUDBDLBUFUFURRFFDFLRFDLUUBBDDRFLURFUFRUFDBRRRDFBDRRUULRLBBDDRLLDBLDDRULFURULLBDFURBDLDUBBRBLFBDBUBBBUDFLLFBF";
            // 
            // hScrollBar1
            // 
            hScrollBar1.Location = new Point(1136, 75);
            hScrollBar1.Maximum = 200;
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new Size(283, 17);
            hScrollBar1.TabIndex = 5;
            hScrollBar1.Scroll += hScrollBar1_Scroll;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(1041, 75);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(75, 21);
            checkBox1.TabIndex = 4;
            checkBox1.Text = "自动聚焦";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button44
            // 
            button44.Location = new Point(1045, 634);
            button44.Name = "button44";
            button44.Size = new Size(119, 37);
            button44.TabIndex = 3;
            button44.Text = "执行结果";
            button44.UseVisualStyleBackColor = true;
            button44.Click += button44_Click;
            // 
            // button42
            // 
            button42.Location = new Point(1041, 120);
            button42.Name = "button42";
            button42.Size = new Size(119, 37);
            button42.TabIndex = 3;
            button42.Text = "识别魔方";
            button42.UseVisualStyleBackColor = true;
            button42.Click += button42_Click;
            // 
            // button35
            // 
            button35.Location = new Point(1041, 15);
            button35.Name = "button35";
            button35.Size = new Size(119, 37);
            button35.TabIndex = 3;
            button35.Text = "启动摄像头";
            button35.UseVisualStyleBackColor = true;
            button35.Click += button35_Click;
            // 
            // button32
            // 
            button32.Location = new Point(1045, 375);
            button32.Name = "button32";
            button32.Size = new Size(119, 37);
            button32.TabIndex = 3;
            button32.Text = "魔方求解";
            button32.UseVisualStyleBackColor = true;
            button32.Click += button32_Click;
            // 
            // imageBox1
            // 
            imageBox1.BorderStyle = BorderStyle.FixedSingle;
            imageBox1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            imageBox1.Location = new Point(18, 15);
            imageBox1.Name = "imageBox1";
            imageBox1.Size = new Size(993, 670);
            imageBox1.SizeMode = PictureBoxSizeMode.Zoom;
            imageBox1.TabIndex = 2;
            imageBox1.TabStop = false;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(button1);
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Controls.Add(btnConnect);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(comboBox1);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1445, 712);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "控制页面";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(45, 792);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(88, 33);
            button1.TabIndex = 51;
            button1.Text = "Test";
            button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(numericUpDown2);
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(button37);
            groupBox1.Controls.Add(button36);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(button47);
            groupBox1.Controls.Add(button46);
            groupBox1.Controls.Add(button34);
            groupBox1.Controls.Add(button38);
            groupBox1.Controls.Add(button33);
            groupBox1.Controls.Add(button31);
            groupBox1.Controls.Add(button29);
            groupBox1.Controls.Add(button30);
            groupBox1.Controls.Add(button28);
            groupBox1.Controls.Add(button8);
            groupBox1.Controls.Add(button17);
            groupBox1.Controls.Add(button18);
            groupBox1.Controls.Add(button19);
            groupBox1.Controls.Add(button20);
            groupBox1.Controls.Add(button21);
            groupBox1.Controls.Add(button22);
            groupBox1.Controls.Add(button23);
            groupBox1.Controls.Add(button24);
            groupBox1.Controls.Add(button25);
            groupBox1.Controls.Add(button26);
            groupBox1.Controls.Add(button27);
            groupBox1.Controls.Add(button9);
            groupBox1.Controls.Add(button10);
            groupBox1.Controls.Add(button11);
            groupBox1.Controls.Add(button12);
            groupBox1.Controls.Add(button16);
            groupBox1.Controls.Add(button15);
            groupBox1.Controls.Add(button13);
            groupBox1.Controls.Add(button14);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(button6);
            groupBox1.Controls.Add(button7);
            groupBox1.Location = new Point(601, 32);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4);
            groupBox1.Size = new Size(835, 544);
            groupBox1.TabIndex = 46;
            groupBox1.TabStop = false;
            groupBox1.Text = "步进电机控制";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(559, 411);
            label4.Name = "label4";
            label4.Size = new Size(32, 17);
            label4.TabIndex = 68;
            label4.Text = "停顿";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(559, 386);
            label3.Name = "label3";
            label3.Size = new Size(32, 17);
            label3.TabIndex = 68;
            label3.Text = "速度";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown2.Location = new Point(608, 409);
            numericUpDown2.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(78, 23);
            numericUpDown2.TabIndex = 67;
            numericUpDown2.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // numericUpDown1
            // 
            numericUpDown1.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            numericUpDown1.Location = new Point(608, 380);
            numericUpDown1.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(78, 23);
            numericUpDown1.TabIndex = 67;
            numericUpDown1.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // button37
            // 
            button37.Location = new Point(575, 56);
            button37.Name = "button37";
            button37.Size = new Size(142, 33);
            button37.TabIndex = 66;
            button37.Text = "关闭原点模式";
            button37.UseVisualStyleBackColor = true;
            button37.Click += button37_Click;
            // 
            // button36
            // 
            button36.Location = new Point(418, 56);
            button36.Name = "button36";
            button36.Size = new Size(126, 33);
            button36.TabIndex = 66;
            button36.Text = "启动原点模式";
            button36.UseVisualStyleBackColor = true;
            button36.Click += button36_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(404, 480);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(155, 23);
            textBox1.TabIndex = 65;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(radioButton15);
            groupBox3.Controls.Add(radioButton14);
            groupBox3.Controls.Add(radioButton13);
            groupBox3.Location = new Point(336, 429);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(58, 108);
            groupBox3.TabIndex = 64;
            groupBox3.TabStop = false;
            // 
            // radioButton15
            // 
            radioButton15.AutoSize = true;
            radioButton15.Location = new Point(6, 72);
            radioButton15.Name = "radioButton15";
            radioButton15.Size = new Size(33, 21);
            radioButton15.TabIndex = 0;
            radioButton15.TabStop = true;
            radioButton15.Text = "2";
            radioButton15.UseVisualStyleBackColor = true;
            radioButton15.CheckedChanged += radioButton_CheckedChanged2;
            // 
            // radioButton14
            // 
            radioButton14.AutoSize = true;
            radioButton14.Location = new Point(6, 45);
            radioButton14.Name = "radioButton14";
            radioButton14.Size = new Size(29, 21);
            radioButton14.TabIndex = 0;
            radioButton14.TabStop = true;
            radioButton14.Text = "'";
            radioButton14.UseVisualStyleBackColor = true;
            radioButton14.CheckedChanged += radioButton_CheckedChanged2;
            // 
            // radioButton13
            // 
            radioButton13.AutoSize = true;
            radioButton13.Checked = true;
            radioButton13.Location = new Point(6, 25);
            radioButton13.Name = "radioButton13";
            radioButton13.Size = new Size(14, 13);
            radioButton13.TabIndex = 0;
            radioButton13.TabStop = true;
            radioButton13.UseVisualStyleBackColor = true;
            radioButton13.CheckedChanged += radioButton_CheckedChanged2;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(radioButton12);
            groupBox2.Controls.Add(radioButton6);
            groupBox2.Controls.Add(radioButton11);
            groupBox2.Controls.Add(radioButton5);
            groupBox2.Controls.Add(radioButton10);
            groupBox2.Controls.Add(radioButton4);
            groupBox2.Controls.Add(radioButton9);
            groupBox2.Controls.Add(radioButton3);
            groupBox2.Controls.Add(radioButton8);
            groupBox2.Controls.Add(radioButton7);
            groupBox2.Controls.Add(radioButton2);
            groupBox2.Controls.Add(radioButton1);
            groupBox2.Location = new Point(59, 445);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(260, 81);
            groupBox2.TabIndex = 63;
            groupBox2.TabStop = false;
            // 
            // radioButton12
            // 
            radioButton12.AutoSize = true;
            radioButton12.Location = new Point(216, 45);
            radioButton12.Name = "radioButton12";
            radioButton12.Size = new Size(34, 21);
            radioButton12.TabIndex = 5;
            radioButton12.TabStop = true;
            radioButton12.Text = "b";
            radioButton12.UseVisualStyleBackColor = true;
            radioButton12.CheckedChanged += radioButton_CheckedChanged1;
            // 
            // radioButton6
            // 
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(216, 18);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(34, 21);
            radioButton6.TabIndex = 5;
            radioButton6.TabStop = true;
            radioButton6.Text = "B";
            radioButton6.UseVisualStyleBackColor = true;
            radioButton6.CheckedChanged += radioButton_CheckedChanged1;
            // 
            // radioButton11
            // 
            radioButton11.AutoSize = true;
            radioButton11.Location = new Point(175, 45);
            radioButton11.Name = "radioButton11";
            radioButton11.Size = new Size(29, 21);
            radioButton11.TabIndex = 4;
            radioButton11.TabStop = true;
            radioButton11.Text = "l";
            radioButton11.UseVisualStyleBackColor = true;
            radioButton11.CheckedChanged += radioButton_CheckedChanged1;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(175, 18);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(32, 21);
            radioButton5.TabIndex = 4;
            radioButton5.TabStop = true;
            radioButton5.Text = "L";
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.CheckedChanged += radioButton_CheckedChanged1;
            // 
            // radioButton10
            // 
            radioButton10.AutoSize = true;
            radioButton10.Location = new Point(134, 45);
            radioButton10.Name = "radioButton10";
            radioButton10.Size = new Size(34, 21);
            radioButton10.TabIndex = 3;
            radioButton10.TabStop = true;
            radioButton10.Text = "d";
            radioButton10.UseVisualStyleBackColor = true;
            radioButton10.CheckedChanged += radioButton_CheckedChanged1;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(134, 18);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(35, 21);
            radioButton4.TabIndex = 3;
            radioButton4.TabStop = true;
            radioButton4.Text = "D";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += radioButton_CheckedChanged1;
            // 
            // radioButton9
            // 
            radioButton9.AutoSize = true;
            radioButton9.Location = new Point(93, 45);
            radioButton9.Name = "radioButton9";
            radioButton9.Size = new Size(30, 21);
            radioButton9.TabIndex = 2;
            radioButton9.TabStop = true;
            radioButton9.Text = "f";
            radioButton9.UseVisualStyleBackColor = true;
            radioButton9.CheckedChanged += radioButton_CheckedChanged1;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(93, 18);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(32, 21);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Text = "F";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton_CheckedChanged1;
            // 
            // radioButton8
            // 
            radioButton8.AutoSize = true;
            radioButton8.Location = new Point(52, 45);
            radioButton8.Name = "radioButton8";
            radioButton8.Size = new Size(31, 21);
            radioButton8.TabIndex = 1;
            radioButton8.TabStop = true;
            radioButton8.Text = "r";
            radioButton8.UseVisualStyleBackColor = true;
            radioButton8.CheckedChanged += radioButton_CheckedChanged1;
            // 
            // radioButton7
            // 
            radioButton7.AutoSize = true;
            radioButton7.Location = new Point(11, 45);
            radioButton7.Name = "radioButton7";
            radioButton7.Size = new Size(33, 21);
            radioButton7.TabIndex = 0;
            radioButton7.TabStop = true;
            radioButton7.Text = "u";
            radioButton7.UseVisualStyleBackColor = true;
            radioButton7.CheckedChanged += radioButton_CheckedChanged1;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(52, 18);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(34, 21);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "R";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton_CheckedChanged1;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(11, 18);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(35, 21);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "U";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton_CheckedChanged1;
            // 
            // button47
            // 
            button47.Location = new Point(705, 243);
            button47.Name = "button47";
            button47.Size = new Size(75, 33);
            button47.TabIndex = 62;
            button47.Text = "Restore A";
            button47.UseVisualStyleBackColor = true;
            button47.Click += button47_Click;
            // 
            // button46
            // 
            button46.Location = new Point(551, 243);
            button46.Name = "button46";
            button46.Size = new Size(135, 33);
            button46.TabIndex = 61;
            button46.Text = "A FullCube";
            button46.UseVisualStyleBackColor = true;
            button46.Click += button46_Click;
            // 
            // button34
            // 
            button34.Location = new Point(575, 291);
            button34.Name = "button34";
            button34.Size = new Size(85, 34);
            button34.TabIndex = 60;
            button34.Text = "All to Pos0";
            button34.UseVisualStyleBackColor = true;
            button34.Click += button34_Click;
            // 
            // button38
            // 
            button38.Location = new Point(705, 380);
            button38.Name = "button38";
            button38.Size = new Size(110, 52);
            button38.TabIndex = 58;
            button38.Text = "设置速度";
            button38.UseVisualStyleBackColor = true;
            button38.Click += button38_Click;
            // 
            // button33
            // 
            button33.Location = new Point(576, 474);
            button33.Name = "button33";
            button33.Size = new Size(110, 35);
            button33.TabIndex = 58;
            button33.Text = "执行";
            button33.UseVisualStyleBackColor = true;
            button33.Click += button33_Click;
            // 
            // button31
            // 
            button31.Location = new Point(705, 196);
            button31.Name = "button31";
            button31.Size = new Size(75, 33);
            button31.TabIndex = 57;
            button31.Text = "Restore B";
            button31.UseVisualStyleBackColor = true;
            button31.Click += button31_Click;
            // 
            // button29
            // 
            button29.Location = new Point(705, 148);
            button29.Name = "button29";
            button29.Size = new Size(75, 33);
            button29.TabIndex = 57;
            button29.Text = "Restore C";
            button29.UseVisualStyleBackColor = true;
            button29.Click += button29_Click;
            // 
            // button30
            // 
            button30.Location = new Point(551, 196);
            button30.Name = "button30";
            button30.Size = new Size(134, 32);
            button30.TabIndex = 56;
            button30.Text = "B FullCube";
            button30.UseVisualStyleBackColor = true;
            button30.Click += button30_Click;
            // 
            // button28
            // 
            button28.Location = new Point(551, 148);
            button28.Name = "button28";
            button28.Size = new Size(134, 33);
            button28.TabIndex = 56;
            button28.Text = "C FullCube";
            button28.UseVisualStyleBackColor = true;
            button28.Click += button28_Click;
            // 
            // button8
            // 
            button8.Location = new Point(405, 389);
            button8.Margin = new Padding(4);
            button8.Name = "button8";
            button8.Size = new Size(118, 33);
            button8.TabIndex = 54;
            button8.Text = "C Pos - 1";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button17
            // 
            button17.Location = new Point(256, 389);
            button17.Margin = new Padding(4);
            button17.Name = "button17";
            button17.Size = new Size(124, 33);
            button17.TabIndex = 55;
            button17.Text = "C Pos0 + 1";
            button17.UseVisualStyleBackColor = true;
            button17.Click += button17_Click;
            // 
            // button18
            // 
            button18.Location = new Point(405, 342);
            button18.Margin = new Padding(4);
            button18.Name = "button18";
            button18.Size = new Size(118, 33);
            button18.TabIndex = 52;
            button18.Text = "B Pos - 1";
            button18.UseVisualStyleBackColor = true;
            button18.Click += button18_Click;
            // 
            // button19
            // 
            button19.Location = new Point(255, 342);
            button19.Margin = new Padding(4);
            button19.Name = "button19";
            button19.Size = new Size(124, 33);
            button19.TabIndex = 53;
            button19.Text = "B Pos0 + 1";
            button19.UseVisualStyleBackColor = true;
            button19.Click += button19_Click;
            // 
            // button20
            // 
            button20.Location = new Point(404, 291);
            button20.Margin = new Padding(4);
            button20.Name = "button20";
            button20.Size = new Size(118, 33);
            button20.TabIndex = 50;
            button20.Text = "A Pos - 1";
            button20.UseVisualStyleBackColor = true;
            button20.Click += button20_Click;
            // 
            // button21
            // 
            button21.Location = new Point(255, 291);
            button21.Margin = new Padding(4);
            button21.Name = "button21";
            button21.Size = new Size(124, 33);
            button21.TabIndex = 51;
            button21.Text = "A Pos + 1";
            button21.UseVisualStyleBackColor = true;
            button21.Click += button21_Click;
            // 
            // button22
            // 
            button22.Location = new Point(404, 148);
            button22.Margin = new Padding(4);
            button22.Name = "button22";
            button22.Size = new Size(118, 33);
            button22.TabIndex = 45;
            button22.Text = "X CCW 90°";
            button22.UseVisualStyleBackColor = true;
            button22.Click += button22_Click;
            // 
            // button23
            // 
            button23.Location = new Point(254, 148);
            button23.Margin = new Padding(4);
            button23.Name = "button23";
            button23.Size = new Size(124, 33);
            button23.TabIndex = 44;
            button23.Text = "X CW 90°";
            button23.UseVisualStyleBackColor = true;
            button23.Click += button23_Click;
            // 
            // button24
            // 
            button24.Location = new Point(254, 195);
            button24.Margin = new Padding(4);
            button24.Name = "button24";
            button24.Size = new Size(124, 33);
            button24.TabIndex = 46;
            button24.Text = "Y CW 90°";
            button24.UseVisualStyleBackColor = true;
            button24.Click += button24_Click;
            // 
            // button25
            // 
            button25.Location = new Point(404, 195);
            button25.Margin = new Padding(4);
            button25.Name = "button25";
            button25.Size = new Size(118, 33);
            button25.TabIndex = 47;
            button25.Text = "Y CCW 90°";
            button25.UseVisualStyleBackColor = true;
            button25.Click += button25_Click;
            // 
            // button26
            // 
            button26.Location = new Point(404, 242);
            button26.Margin = new Padding(4);
            button26.Name = "button26";
            button26.Size = new Size(118, 33);
            button26.TabIndex = 48;
            button26.Text = "Z CCW 90°";
            button26.UseVisualStyleBackColor = true;
            button26.Click += button26_Click;
            // 
            // button27
            // 
            button27.Location = new Point(254, 242);
            button27.Margin = new Padding(4);
            button27.Name = "button27";
            button27.Size = new Size(124, 33);
            button27.TabIndex = 49;
            button27.Text = "Z CW 90°";
            button27.UseVisualStyleBackColor = true;
            button27.Click += button27_Click;
            // 
            // button9
            // 
            button9.Location = new Point(139, 389);
            button9.Margin = new Padding(4);
            button9.Name = "button9";
            button9.Size = new Size(88, 33);
            button9.TabIndex = 42;
            button9.Text = "C PULL 1";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button10
            // 
            button10.Location = new Point(26, 389);
            button10.Margin = new Padding(4);
            button10.Name = "button10";
            button10.Size = new Size(88, 33);
            button10.TabIndex = 43;
            button10.Text = "C PUSH 1";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // button11
            // 
            button11.Location = new Point(139, 342);
            button11.Margin = new Padding(4);
            button11.Name = "button11";
            button11.Size = new Size(88, 33);
            button11.TabIndex = 39;
            button11.Text = "B PULL 1";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // button12
            // 
            button12.Location = new Point(25, 342);
            button12.Margin = new Padding(4);
            button12.Name = "button12";
            button12.Size = new Size(88, 33);
            button12.TabIndex = 40;
            button12.Text = "B PUSH 1";
            button12.UseVisualStyleBackColor = true;
            button12.Click += button12_Click;
            // 
            // button16
            // 
            button16.Location = new Point(215, 56);
            button16.Margin = new Padding(4);
            button16.Name = "button16";
            button16.Size = new Size(165, 33);
            button16.TabIndex = 28;
            button16.Text = "关闭步进电机";
            button16.UseVisualStyleBackColor = true;
            button16.Click += button16_Click;
            // 
            // button15
            // 
            button15.Location = new Point(29, 56);
            button15.Margin = new Padding(4);
            button15.Name = "button15";
            button15.Size = new Size(157, 33);
            button15.TabIndex = 27;
            button15.Text = "使能步进电机";
            button15.UseVisualStyleBackColor = true;
            button15.Click += button15_Click;
            // 
            // button13
            // 
            button13.Location = new Point(138, 291);
            button13.Margin = new Padding(4);
            button13.Name = "button13";
            button13.Size = new Size(88, 33);
            button13.TabIndex = 24;
            button13.Text = "A PULL 1";
            button13.UseVisualStyleBackColor = true;
            button13.Click += button13_Click;
            // 
            // button14
            // 
            button14.Location = new Point(25, 291);
            button14.Margin = new Padding(4);
            button14.Name = "button14";
            button14.Size = new Size(88, 33);
            button14.TabIndex = 25;
            button14.Text = "A PUSH 1";
            button14.UseVisualStyleBackColor = true;
            button14.Click += button14_Click;
            // 
            // button3
            // 
            button3.Location = new Point(138, 148);
            button3.Margin = new Padding(4);
            button3.Name = "button3";
            button3.Size = new Size(88, 33);
            button3.TabIndex = 9;
            button3.Text = "X CCW 1";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(24, 148);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(88, 33);
            button2.TabIndex = 8;
            button2.Text = "X CW 1";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button4
            // 
            button4.Location = new Point(24, 195);
            button4.Margin = new Padding(4);
            button4.Name = "button4";
            button4.Size = new Size(88, 33);
            button4.TabIndex = 10;
            button4.Text = "Y CW 1";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(138, 195);
            button5.Margin = new Padding(4);
            button5.Name = "button5";
            button5.Size = new Size(88, 33);
            button5.TabIndex = 11;
            button5.Text = "Y CCW 1";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(138, 242);
            button6.Margin = new Padding(4);
            button6.Name = "button6";
            button6.Size = new Size(88, 33);
            button6.TabIndex = 12;
            button6.Text = "Z CCW 1";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(24, 242);
            button7.Margin = new Padding(4);
            button7.Name = "button7";
            button7.Size = new Size(88, 33);
            button7.TabIndex = 13;
            button7.Text = "Z CW 1";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // btnConnect
            // 
            btnConnect.Enabled = false;
            btnConnect.Location = new Point(374, 32);
            btnConnect.Margin = new Padding(4);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(88, 33);
            btnConnect.TabIndex = 45;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 40);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 44;
            label1.Text = "可用串口";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(169, 35);
            comboBox1.Margin = new Padding(4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(174, 25);
            comboBox1.TabIndex = 43;
            // 
            // button43
            // 
            button43.Location = new Point(1232, 375);
            button43.Name = "button43";
            button43.Size = new Size(108, 37);
            button43.TabIndex = 10;
            button43.Text = "打乱魔方";
            button43.UseVisualStyleBackColor = true;
            button43.Click += button43_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1453, 742);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "CubeSolver";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)imageBox1).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button button1;
        private GroupBox groupBox1;
        private Button button16;
        private Button button15;
        private Button button13;
        private Button button14;
        private Button button3;
        private Button button2;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button btnConnect;
        private Label label1;
        private ComboBox comboBox1;
        private Button button9;
        private Button button10;
        private Button button11;
        private Button button12;
        private Button button8;
        private Button button17;
        private Button button18;
        private Button button19;
        private Button button20;
        private Button button21;
        private Button button22;
        private Button button23;
        private Button button24;
        private Button button25;
        private Button button26;
        private Button button27;
        private Button button28;
        private Button button29;
        private Button button30;
        private Emgu.CV.UI.ImageBox imageBox1;
        private Button button32;
        private Button button33;
        private Button button34;
        private Button button42;
        private Button button35;
        private CheckBox checkBox1;
        private HScrollBar hScrollBar1;
        private TextBox textBox3;
        private TextBox textBox2;
        private Button button44;
        private Button button46;
        private Button button47;
        private Button button31;
        private GroupBox groupBox2;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private TextBox textBox1;
        private GroupBox groupBox3;
        private RadioButton radioButton15;
        private RadioButton radioButton14;
        private RadioButton radioButton13;
        private RadioButton radioButton12;
        private RadioButton radioButton6;
        private RadioButton radioButton11;
        private RadioButton radioButton5;
        private RadioButton radioButton10;
        private RadioButton radioButton4;
        private RadioButton radioButton9;
        private RadioButton radioButton3;
        private RadioButton radioButton8;
        private RadioButton radioButton7;
        private Button button37;
        private Button button36;
        private NumericUpDown numericUpDown1;
        private Button button38;
        private Label label4;
        private Label label3;
        private NumericUpDown numericUpDown2;
        private Button button40;
        private Button button39;
        private Button button41;
        private CheckBox checkBox2;
        private Button button43;
    }
}