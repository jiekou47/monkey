using AxWMPLib;
using System.IO.Ports;
using System.Windows.Forms;

namespace monkey
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
            button1 = new Button();
            comboBox1 = new ComboBox();
            label1 = new Label();
            comboBox2 = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            openFileDialog1 = new OpenFileDialog();
            textBox1 = new TextBox();
            button2 = new Button();
            label4 = new Label();
            textBox3 = new TextBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            groupBox1 = new GroupBox();
            label5 = new Label();
            comboBox3 = new ComboBox();
            comboBox4 = new ComboBox();
            button3 = new Button();
            saveFileDialog1 = new SaveFileDialog();
            textBox5 = new TextBox();
            comboBox5 = new ComboBox();
            button5 = new Button();
            button4 = new Button();
            button6 = new Button();
            comboBox6 = new ComboBox();
            label6 = new Label();
            label7 = new Label();
            comboBox7 = new ComboBox();
            button7 = new Button();
            textBox2 = new TextBox();
            button10 = new Button();
            button9 = new Button();
            button8 = new Button();
            button11 = new Button();
            button12 = new Button();
            textBox6 = new TextBox();
            label9 = new Label();
            textBox4 = new TextBox();
            button13 = new Button();
            label8 = new Label();
            textBox7 = new TextBox();
            button14 = new Button();
            label10 = new Label();
            button15 = new Button();
            label11 = new Label();
            textBox8 = new TextBox();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            button16 = new Button();
            button17 = new Button();
            monkey = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            button18 = new Button();
            label15 = new Label();
            comboBox8 = new ComboBox();
            richTextBox1 = new RichTextBox();
            label16 = new Label();
            comboBox9 = new ComboBox();
            button19 = new Button();
            groupBox1.SuspendLayout();
            monkey.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(782, 49);
            button1.Name = "button1";
            button1.Size = new Size(158, 93);
            button1.TabIndex = 0;
            button1.Text = "运行monkey";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(170, 52);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(270, 39);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 55);
            label1.Name = "label1";
            label1.Size = new Size(86, 31);
            label1.TabIndex = 2;
            label1.Text = "设备：";
            label1.Click += label1_Click;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "1", "2", "3" });
            comboBox2.Location = new Point(170, 172);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(270, 39);
            comboBox2.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 172);
            label2.Name = "label2";
            label2.Size = new Size(134, 31);
            label2.TabIndex = 5;
            label2.Text = "日志级别：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(615, 254);
            label3.Name = "label3";
            label3.Size = new Size(74, 31);
            label3.TabIndex = 6;
            label3.Text = "seed:";
            label3.Click += label3_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(219, 104);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(557, 38);
            textBox1.TabIndex = 8;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button2
            // 
            button2.Location = new Point(34, 102);
            button2.Name = "button2";
            button2.Size = new Size(159, 44);
            button2.TabIndex = 9;
            button2.Text = "导入白名单";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 114);
            label4.Name = "label4";
            label4.Size = new Size(86, 31);
            label4.TabIndex = 11;
            label4.Text = "应用：";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(712, 251);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(270, 38);
            textBox3.TabIndex = 13;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(29, 121);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(176, 35);
            checkBox1.TabIndex = 14;
            checkBox1.Text = "忽略CRASH";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(29, 79);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(147, 35);
            checkBox2.TabIndex = 15;
            checkBox2.Text = "忽略ANR";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(29, 37);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(206, 35);
            checkBox3.TabIndex = 16;
            checkBox3.Text = "忽略exception";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox3);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Location = new Point(647, 47);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(377, 194);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "忽略参数";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(27, 318);
            label5.Name = "label5";
            label5.Size = new Size(134, 31);
            label5.TabIndex = 20;
            label5.Text = "事件比例：";
            label5.Click += label5_Click;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "触摸事件", "导航事件" });
            comboBox3.Location = new Point(170, 316);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(270, 39);
            comboBox3.TabIndex = 21;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Items.AddRange(new object[] { "10%" });
            comboBox4.Location = new Point(471, 318);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(114, 39);
            comboBox4.TabIndex = 22;
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
            // 
            // button3
            // 
            button3.Location = new Point(620, 51);
            button3.Name = "button3";
            button3.Size = new Size(158, 44);
            button3.TabIndex = 23;
            button3.Text = "日志保存";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.FileOk += saveFileDialog1_FileOk;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(2011, 1040);
            textBox5.Multiline = true;
            textBox5.Name = "textBox5";
            textBox5.ScrollBars = ScrollBars.Vertical;
            textBox5.Size = new Size(185, 83);
            textBox5.TabIndex = 25;
            textBox5.TextChanged += textBox5_TextChanged;
            // 
            // comboBox5
            // 
            comboBox5.FormattingEnabled = true;
            comboBox5.Location = new Point(170, 111);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(270, 39);
            comboBox5.TabIndex = 26;
            comboBox5.SelectedIndexChanged += comboBox5_SelectedIndexChanged;
            // 
            // button5
            // 
            button5.Location = new Point(423, 49);
            button5.Name = "button5";
            button5.Size = new Size(171, 46);
            button5.TabIndex = 28;
            button5.Text = "停止测试";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.Location = new Point(219, 49);
            button4.Name = "button4";
            button4.Size = new Size(171, 46);
            button4.TabIndex = 29;
            button4.Text = "清空所有日志";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click_1;
            // 
            // button6
            // 
            button6.Location = new Point(34, 49);
            button6.Name = "button6";
            button6.Size = new Size(150, 46);
            button6.TabIndex = 30;
            button6.Text = "初始化应用";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // comboBox6
            // 
            comboBox6.FormattingEnabled = true;
            comboBox6.Items.AddRange(new object[] { "9600", "115200", "1500000" });
            comboBox6.Location = new Point(167, 119);
            comboBox6.Name = "comboBox6";
            comboBox6.Size = new Size(231, 39);
            comboBox6.TabIndex = 31;
            comboBox6.SelectedIndexChanged += comboBox6_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(27, 65);
            label6.Name = "label6";
            label6.Size = new Size(86, 31);
            label6.TabIndex = 32;
            label6.Text = "串口：";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(27, 122);
            label7.Name = "label7";
            label7.Size = new Size(110, 31);
            label7.TabIndex = 33;
            label7.Text = "波特率：";
            // 
            // comboBox7
            // 
            comboBox7.Font = new Font("Arial", 7.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox7.FormattingEnabled = true;
            comboBox7.Location = new Point(102, 65);
            comboBox7.Name = "comboBox7";
            comboBox7.Size = new Size(296, 31);
            comboBox7.TabIndex = 34;
            // 
            // button7
            // 
            button7.Location = new Point(425, 122);
            button7.Name = "button7";
            button7.RightToLeft = RightToLeft.Yes;
            button7.Size = new Size(150, 46);
            button7.TabIndex = 35;
            button7.Text = "打开串口";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(1151, 1049);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(633, 38);
            textBox2.TabIndex = 38;
            textBox2.TextChanged += textBox2_TextChanged_2;
            // 
            // button10
            // 
            button10.Location = new Point(1803, 1049);
            button10.Name = "button10";
            button10.Size = new Size(114, 38);
            button10.TabIndex = 40;
            button10.Text = "退出";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // button9
            // 
            button9.Location = new Point(221, 159);
            button9.Name = "button9";
            button9.Size = new Size(150, 51);
            button9.TabIndex = 42;
            button9.Text = "关闭ADB";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click_1;
            // 
            // button8
            // 
            button8.Location = new Point(34, 159);
            button8.Name = "button8";
            button8.Size = new Size(159, 51);
            button8.TabIndex = 43;
            button8.Text = "打开ADB";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button11
            // 
            button11.Location = new Point(712, 1036);
            button11.Name = "button11";
            button11.Size = new Size(150, 38);
            button11.TabIndex = 44;
            button11.Text = "开始测试";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // button12
            // 
            button12.Location = new Point(892, 1035);
            button12.Name = "button12";
            button12.Size = new Size(138, 38);
            button12.TabIndex = 45;
            button12.Text = "终止";
            button12.UseVisualStyleBackColor = true;
            button12.Click += button12_Click;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(65, 986);
            textBox6.Multiline = true;
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(600, 88);
            textBox6.TabIndex = 46;
            textBox6.TextChanged += textBox6_TextChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(62, 949);
            label9.Name = "label9";
            label9.Size = new Size(134, 31);
            label9.TabIndex = 49;
            label9.Text = "测试次数：";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(202, 942);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(124, 38);
            textBox4.TabIndex = 50;
            // 
            // button13
            // 
            button13.Location = new Point(8, 122);
            button13.Name = "button13";
            button13.Size = new Size(150, 46);
            button13.TabIndex = 51;
            button13.Text = "标定摄像头";
            button13.UseVisualStyleBackColor = true;
            button13.Click += button13_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(27, 247);
            label8.Name = "label8";
            label8.Size = new Size(86, 31);
            label8.TabIndex = 52;
            label8.Text = "次数：";
            // 
            // textBox7
            // 
            textBox7.Location = new Point(170, 244);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(270, 38);
            textBox7.TabIndex = 53;
            textBox7.TextChanged += textBox7_TextChanged;
            // 
            // button14
            // 
            button14.Location = new Point(425, 65);
            button14.Name = "button14";
            button14.Size = new Size(150, 46);
            button14.TabIndex = 54;
            button14.Text = "刷新";
            button14.UseVisualStyleBackColor = true;
            button14.Click += button14_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(992, 1319);
            label10.Name = "label10";
            label10.Size = new Size(0, 31);
            label10.TabIndex = 55;
            // 
            // button15
            // 
            button15.Location = new Point(185, 122);
            button15.Name = "button15";
            button15.Size = new Size(150, 46);
            button15.TabIndex = 56;
            button15.Text = "测试";
            button15.UseVisualStyleBackColor = true;
            button15.Click += button15_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(8, 65);
            label11.Name = "label11";
            label11.Size = new Size(134, 31);
            label11.TabIndex = 57;
            label11.Text = "黑屏参数：";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(185, 59);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(148, 38);
            textBox8.TabIndex = 58;
            textBox8.TextChanged += textBox8_TextChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(62, 1092);
            label12.Name = "label12";
            label12.Size = new Size(134, 31);
            label12.TabIndex = 59;
            label12.Text = "测试轮次：";
            label12.Click += label12_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(283, 1092);
            label13.Name = "label13";
            label13.Size = new Size(164, 31);
            label13.TabIndex = 60;
            label13.Text = "异常黑屏次数:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(596, 1092);
            label14.Name = "label14";
            label14.Size = new Size(158, 31);
            label14.TabIndex = 61;
            label14.Text = "当前像素值：";
            label14.Click += label14_Click;
            // 
            // button16
            // 
            button16.Location = new Point(8, 182);
            button16.Name = "button16";
            button16.Size = new Size(150, 46);
            button16.TabIndex = 62;
            button16.Text = "黑屏检测";
            button16.UseVisualStyleBackColor = true;
            button16.Click += button16_Click;
            // 
            // button17
            // 
            button17.Location = new Point(892, 969);
            button17.Name = "button17";
            button17.Size = new Size(138, 46);
            button17.TabIndex = 63;
            button17.Text = "can初始化";
            button17.UseVisualStyleBackColor = true;
            button17.Click += button17_Click;
            // 
            // monkey
            // 
            monkey.Controls.Add(groupBox2);
            monkey.Controls.Add(label1);
            monkey.Controls.Add(comboBox1);
            monkey.Controls.Add(label4);
            monkey.Controls.Add(comboBox5);
            monkey.Controls.Add(label5);
            monkey.Controls.Add(comboBox3);
            monkey.Controls.Add(comboBox4);
            monkey.Controls.Add(label3);
            monkey.Controls.Add(textBox3);
            monkey.Controls.Add(label8);
            monkey.Controls.Add(textBox7);
            monkey.Controls.Add(label2);
            monkey.Controls.Add(comboBox2);
            monkey.Controls.Add(groupBox1);
            monkey.Location = new Point(65, 12);
            monkey.Name = "monkey";
            monkey.Size = new Size(1024, 612);
            monkey.TabIndex = 64;
            monkey.TabStop = false;
            monkey.Text = "monkey设置";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button3);
            groupBox2.Controls.Add(button6);
            groupBox2.Controls.Add(button4);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(button5);
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(button8);
            groupBox2.Controls.Add(button9);
            groupBox2.Location = new Point(6, 371);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(959, 234);
            groupBox2.TabIndex = 54;
            groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(comboBox7);
            groupBox3.Controls.Add(button14);
            groupBox3.Controls.Add(button7);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(comboBox6);
            groupBox3.Location = new Point(62, 674);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(602, 263);
            groupBox3.TabIndex = 65;
            groupBox3.TabStop = false;
            groupBox3.Text = "串口和继电器设置";
            groupBox3.Enter += groupBox3_Enter;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(button18);
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(textBox8);
            groupBox4.Controls.Add(button13);
            groupBox4.Controls.Add(button15);
            groupBox4.Controls.Add(button16);
            groupBox4.Location = new Point(712, 674);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(377, 263);
            groupBox4.TabIndex = 66;
            groupBox4.TabStop = false;
            groupBox4.Text = "摄像头设置";
            // 
            // button18
            // 
            button18.Location = new Point(185, 182);
            button18.Name = "button18";
            button18.Size = new Size(150, 46);
            button18.TabIndex = 69;
            button18.Text = "圆形";
            button18.UseVisualStyleBackColor = true;
            button18.Click += button18_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(390, 945);
            label15.Name = "label15";
            label15.Size = new Size(134, 31);
            label15.TabIndex = 67;
            label15.Text = "执行方式：";
            // 
            // comboBox8
            // 
            comboBox8.FormattingEnabled = true;
            comboBox8.Location = new Point(521, 943);
            comboBox8.Name = "comboBox8";
            comboBox8.Size = new Size(143, 39);
            comboBox8.TabIndex = 68;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(1151, 28);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(1082, 987);
            richTextBox1.TabIndex = 55;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(1179, 36);
            label16.Name = "label16";
            label16.Size = new Size(134, 31);
            label16.TabIndex = 69;
            label16.Text = "打印设置：";
            // 
            // comboBox9
            // 
            comboBox9.FormattingEnabled = true;
            comboBox9.Items.AddRange(new object[] { "实时", "交互" });
            comboBox9.Location = new Point(1329, 33);
            comboBox9.Name = "comboBox9";
            comboBox9.Size = new Size(242, 39);
            comboBox9.TabIndex = 70;
            comboBox9.SelectedIndexChanged += comboBox9_SelectedIndexChanged;
            // 
            // button19
            // 
            button19.Location = new Point(2072, 28);
            button19.Name = "button19";
            button19.Size = new Size(124, 46);
            button19.TabIndex = 71;
            button19.Text = "清除缓存";
            button19.UseVisualStyleBackColor = true;
            button19.Click += button19_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2333, 1163);
            Controls.Add(button19);
            Controls.Add(comboBox9);
            Controls.Add(label16);
            Controls.Add(richTextBox1);
            Controls.Add(comboBox8);
            Controls.Add(label15);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(monkey);
            Controls.Add(button17);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(textBox6);
            Controls.Add(textBox4);
            Controls.Add(label9);
            Controls.Add(label10);
            Controls.Add(button12);
            Controls.Add(button11);
            Controls.Add(button10);
            Controls.Add(textBox2);
            Controls.Add(textBox5);
            Name = "Form1";
            Text = "工具";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            monkey.ResumeLayout(false);
            monkey.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ComboBox comboBox1;
        private Label label1;
        private ComboBox comboBox2;
        private Label label2;
        private Label label3;
        private OpenFileDialog openFileDialog1;
        public TextBox textBox1;
        private Button button2;
        private Label label4;
        private TextBox textBox3;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private GroupBox groupBox1;
        private Label label5;
        private ComboBox comboBox3;
        private ComboBox comboBox4;
        private Button button3;
        private SaveFileDialog saveFileDialog1;
        private TextBox textBox5;
        private ComboBox comboBox5;
        private Button button5;
        private Button button4;
        private Button button6;
        private ComboBox comboBox6;
        private Label label6;
        private Label label7;
        private ComboBox comboBox7;
        private Button button7;
        private TextBox textBox2;
        private Button button10;
        private Button button9;
        private Button button8;
        private Button button11;
        private Button button12;
        private TextBox textBox6;
        private Label label9;
        private TextBox textBox4;
        private Button button13;
        private Label label8;
        private TextBox textBox7;
        private Button button14;
        private Label label10;
        private PictureBox pictureBox1;
        private Button button15;
        private Label label11;
        private TextBox textBox8;
        private Label label12;
        private Label label13;
        private Label label14;
        private Button button16;
        private Button button17;
        private GroupBox monkey;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label label15;
        private ComboBox comboBox8;
        private Button button18;
        private RichTextBox richTextBox1;
        private Label label16;
        private ComboBox comboBox9;
        private Button button19;
    }
}
