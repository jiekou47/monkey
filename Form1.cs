using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Management;
using System.Text.RegularExpressions;
using OpenCvSharp;
using driver;
using OpenCvSharp.Extensions;
using ZLGCAN;
using System.Runtime.InteropServices;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Concurrent;
using System.ComponentModel;







namespace monkey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lastcmd[0] = "test";

            PopulateAdbDevice();
            textBox2.KeyDown += TextBox2_KeyDown;
            comboBox9.SelectedIndex = 0;
            // 启动写入线程
            //new Thread(new ThreadStart(WriteData)) { IsBackground = true }.Start();

            // 启动读取线程
            //new Thread(new ThreadStart(ReadData)) { IsBackground = true }.Start();


        }


        //private int blackcounter = 0;
        private bool keydown = false;
        private API myapi = null;
        private Rectangle GroiRectangle;
        private Bitmap image = new Bitmap(640, 480);
        private int roiX, roiY, roiWidth, roiHeight;
        private Rectangle roi;
        private SerialPort serialPort1 = new SerialPort();
        private SerialPort serialPort2 = new SerialPort();
        private bool monkeyclicked = false;
        private string com = null;
        private string[] packlist = null;
        private string[] packlistCM = null;

        private string[] lastcmd = new string[10];
        private int Gcmdindex = 11;
        bool cmdempty = true;
        bool taskcancel = false;
        private Queue<string> dataQueue = new Queue<string>();
        private ConcurrentQueue<string> _dataQueue = new ConcurrentQueue<string>();
        private object queueLock = new object();

        public void ReadData()
        {
            while (true)
            {
                if (this.IsHandleCreated)
                {

                    this.Invoke((MethodInvoker)delegate
                    {
                        // 线程安全地操作UI控件
                        if (_dataQueue.Count > 0)
                        {
                            Debug.WriteLine("读取数据");
                            //string indata = dataQueue.Dequeue();
                            if (_dataQueue.TryDequeue(out string data))
                            {
                                // 处理数据
                                richTextBox1.AppendText(data);
                                richTextBox1.ScrollToCaret();
                            }


                        }


                    });

                }
                Debug.WriteLine("test!");
                Debug.WriteLine(dataQueue.Count);

                Task.Delay(100).Wait();

            }

        }

        public void WriteData()
        {

        }
        public bool serialinit()
        {
            serialPort1.Close();
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(this.DataReceivedHandler);

            serialPort1.PortName = com;
            serialPort1.BaudRate = int.Parse(comboBox6.Text);
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.DataBits = 8;
            serialPort1.Handshake = Handshake.None;
            serialPort1.Encoding = Encoding.UTF8;

            serialPort1.Open();
            MessageBox.Show("串口已打开");
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void DataReceivedHandler(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string debug = null;

            this.Invoke((MethodInvoker)delegate
            {

                debug = comboBox9.Text;
                Debug.WriteLine(debug);
            });

            if (debug == "实时")
            {
                string indata = serialPort1.ReadExisting();
                //lock (dataQueue) 
                //{
                //    Debug.WriteLine("加入数据中");

                //    dataQueue.Enqueue(indata);
                //}
                //Debug.WriteLine("加入数据中");
                //_dataQueue.Enqueue(indata);
                //_backgroundWorker.ReportProgress(0, indata);



                if (this.IsHandleCreated)
                {

                    this.Invoke((MethodInvoker)delegate
                    {
                        // 线程安全地操作ui控件

                        richTextBox1.AppendText(indata);
                        richTextBox1.ScrollToCaret();

                    });

                }
            }


        }
        public event EventHandler TextBoxValueChanged;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBoxValueChanged?.Invoke(this, EventArgs.Empty);
        }
        private static string[] GetHarewareInfo(string hardType, string propKey)
        {

            List<string> strs = new List<string>();
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + hardType))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        if (hardInfo.Properties[propKey].Value != null)
                        {
                            string str = hardInfo.Properties[propKey].Value.ToString();
                            strs.Add(str);
                        }
                    }
                }
                return strs.ToArray();
            }
            catch
            {
                return null;
            }
            finally
            {
                strs = null;
            }
        }
        public string[] GetComlist()
        {
            List<string> strs = new List<string>();
            string[] comlist = null;
            string[] strArr = GetHarewareInfo("Win32_PnPEntity", "Name");
            foreach (string s in strArr)
            {

                if (s.Contains("COM"))
                {
                    strs.Add(s);
                }

            }
            comlist = strs.ToArray();

            return comlist;

        }
        public static string GetComNum()
        {
            string comNum = null;
            string[] strArr = GetHarewareInfo("Win32_PnPEntity", "Name");
            foreach (string s in strArr)
            {
                Debug.WriteLine(s);
                if (s.Contains("CH340"))
                {
                    int start = s.IndexOf("(") + 1;
                    int end = s.IndexOf(")");
                    comNum = s.Substring(start, end - start);
                    break;
                }

            }
            return comNum;
        }

        private void PopulateAdbDevice()
        {
            // 清除之前的串口列表  
            int i = 0;
            comboBox1.Items.Clear();

            // 获取可用的串口列表  
            string[] ports = SerialPort.GetPortNames();


            com = GetComNum();
            string[] comlist = GetComlist();
            comboBox7.DataSource = comlist;
            Debug.WriteLine("串口号为:" + com);


            string[] prePhase = new string[100];
            //string[] outputLines = RunCmd("adb devices");
            //Debug.WriteLine(prePhase[0]);

            //if (outputLines.Length < 5)
            //{
            //    MessageBox.Show("请确保设备已打开调试模式并正常连接到电脑！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //foreach (var line in outputLines)
            //{
            //    //sb.AppendLine(line);

            //    if (i > 3)
            //    {
            //        string Rline = line.Replace("device", "");
            //        comboBox1.Items.Add(Rline);
            //        Debug.WriteLine(Rline);
            //    }
            //    i++;

            //}

        }
        public void Runcmd1(string cmdStr)
        {
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                p.Start();//启动程序

                //向cmd窗口发送输入信息
                p.StandardInput.WriteLine(cmdStr);
                Thread.Sleep(10000);

                p.StandardInput.AutoFlush = true;
                p.StandardInput.WriteLine("exit");
                string output = p.StandardOutput.ReadToEnd();

                p.WaitForExit();//等待程序执行完退出进程
                MessageBox.Show(output);
                p.Close();
            }
            catch
            {

            }

        }
        public string[] RunCmd(string cmdStr)
        {
            //bool result = false;
            string[] result = new string[100];
            List<string> lines = new List<string>(); // 使用List来存储行，因为它可以动态增长  
            //string input;
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                p.Start();//启动程序

                //向cmd窗口发送输入信息

                p.StandardInput.WriteLine(cmdStr + "&exit");



                p.StandardInput.AutoFlush = true;
                //p.StandardInput.WriteLine("exit");




                //获取cmd窗口的输出信息
                string output = p.StandardOutput.ReadToEnd();

                //StreamReader reader = p.StandardOutput;
                string[] outputLines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                /*while ((reader.ReadLine()) != null)  
                {
                    lines.Add(reader.ReadLine()); // 将输入的行添加到列表中  
                    Debug.WriteLine(reader.ReadLine());
                }

                // 将List转换为字符串数组（如果需要的话）  
                string[] linesArray = lines.ToArray();*/
                //string line=reader.ReadLine();
                //while (!reader.EndOfStream)
                //{
                //    str += line + "  ";
                //    line = reader.ReadLine();
                //}

                p.WaitForExit();//等待程序执行完退出进程
                p.Close();


                MessageBox.Show(output);
                result = outputLines;




            }
            catch
            {

            }
            return result;
        }
        public string[] RunCmd2(string[] cmdStr)
        {
            //bool result = false;
            string[] result = new string[100];
            List<string> lines = new List<string>(); // 使用List来存储行，因为它可以动态增长  
            //string input;
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                p.Start();//启动程序

                //向cmd窗口发送输入信息
                foreach (string line in cmdStr)
                {
                    if (line != null)
                    {
                        p.StandardInput.WriteLine(line);
                    }

                }


                p.StandardInput.AutoFlush = true;
                p.StandardInput.WriteLine("exit");

                //获取cmd窗口的输出信息
                string output = p.StandardOutput.ReadToEnd();

                //StreamReader reader = p.StandardOutput;
                string[] outputLines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                /*while ((reader.ReadLine()) != null)  
                {
                    lines.Add(reader.ReadLine()); // 将输入的行添加到列表中  
                    Debug.WriteLine(reader.ReadLine());
                }

                // 将List转换为字符串数组（如果需要的话）  
                string[] linesArray = lines.ToArray();*/
                //string line=reader.ReadLine();
                //while (!reader.EndOfStream)
                //{
                //    str += line + "  ";
                //    line = reader.ReadLine();
                //}

                p.WaitForExit();//等待程序执行完退出进程
                p.Close();


                MessageBox.Show(output);
                result = outputLines;

            }
            catch
            {

            }
            return result;
        }
        private void get_uidata()
        {

        }
        private void mydebug()
        {
            MessageBox.Show(textBox1.Text);
            MessageBox.Show(comboBox5.Text);

        }

        private void Tlogcat()
        {
            Thread thread = new Thread(new ThreadStart(logcat));

            thread.Start();
            thread.Join();

        }
        private void logcat()
        {
            Runcmd1("adb logcat -v time > D:\\logcat.log");
        }
        public string phase_str()
        {
            string packagetext = null;
            string whitelist = null;
            string seedtext = null;
            string logleveltext = "-v";
            string whitetext = null;
            string blacktext = null;
            string counter = textBox7.Text;
            string timer = null;
            string cmd = null;
            string white = textBox1.Text;
            string package = Gettext(comboBox5);
            string loglevel = Gettext(comboBox2);
            int selectedIndex = comboBox5.SelectedIndex;
            string pack1 = null;
            if (package.Length < 1 && white.Length < 1)
            {
                cmd = null;
                MessageBox.Show("请指定白名单后再重试！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (package.Length > 1)
            {

                whitelist = null;
            }
            else
            {
                package = null;
                whitelist = "--pkg-whitelist-file  /data/whitelist.txt";
            }
            switch (loglevel)
            {
                case "1":
                    logleveltext = "-v"; break;
                case "2":
                    logleveltext = "-v -v"; break;
                case "3":
                    logleveltext = "-v -v -v"; break;
            }
            //Debug.WriteLine("日志等级为：" + logleveltext);
            if (package != null)
            {
                cmd = "monkey -p " + packlistCM[selectedIndex] + " --pct-appswitch 20 --pct-touch 40 --pct-motion 10 --pct-trackball 0 --pct-anyevent 10 --pct-flip 0 --pct-pinchzoom 0 --pct-syskeys 0 --ignore-crashes --ignore-timeouts --ignore-security-exceptions --kill-process-after-error " + logleveltext + " --throttle 300 " + counter + " >/data/" + package + ".log&\r";
            }
            else
            {
                cmd = "monkey " + whitelist + " --pct-appswitch 20 --pct-touch 40 --pct-motion 10 --pct-trackball 0 --pct-anyevent 10 --pct-flip 0 --pct-pinchzoom 0  --pct-syskeys 0 --ignore-crashes --ignore-timeouts --ignore-security-exceptions --kill-process-after-error " + logleveltext + " --throttle 300 " + counter + " >/data/monkey.log&\r";
            }
            return cmd;
        }
        private string Gettext(System.Windows.Forms.ComboBox uiname)
        {

            string str = uiname.Text;
            return str;
        }
        private void sendsrlcmd(string str)
        {
            serialPort1.WriteLine(str + "\r\n");
        }
        private void sendsrlcmd1(string str)
        {
            serialPort2.WriteLine(str + "\r\n");
        }
        private void deletelog(string str)
        {
            sendsrlcmd("rm /data/" + str + ".log");
            sendsrlcmd("rm -f /data/system/dropbox/*");
            sendsrlcmd("rm -f /data/anr/*");
            sendsrlcmd("rm -f /sdcard/ssilog/crash/*");
            sendsrlcmd("rm /data/logcat" + str + ".log");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("monkey 运行中");
            string cmdF = null;
            monkeyclicked = true;
            Debug.WriteLine("这是一条调试信息");
            //Tlogcat();
            //RunCmd("monkey -p com.ssi.engineeringmode");
            //SerialPort sp = (SerialPort)sender;
            //string indata = sp.ReadExisting();
            string pack = comboBox5.Text;
            cmdF = phase_str();
            //sendsrlcmd("logcat -c");
            //Thread.Sleep(2000);
            if (cmdF != null)
            {
                serialPort1.WriteLine("su\r");
                deletelog(pack);
                if (pack != null)
                {

                    sendsrlcmd("logcat -v time > /data/logcat" + pack + ".log&");
                }
                else
                {
                    sendsrlcmd("logcat -v time > /data/logcat.log&");
                }

                serialPort1.WriteLine(cmdF);
                Debug.WriteLine("发送monkey命令：" + cmdF);
            }
            else
            {

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        { }


        private void button2_Click(object sender, EventArgs e)
        {
            if (adbexist())
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                //文件过滤
                dialog.Filter = "Text Files (*.txt)|*.txt|Binary Files (*.bin)|*.bin";
                //关闭选择多文件
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = dialog.FileName;
                    string path = textBox1.Text;

                    //Debug.WriteLine(prePhase[0]);
                    RunCmd("adb root");
                    RunCmd("adb push " + path + " /data");
                }
            }
        }
        private bool adbexist()
        {
            string[] outputLines = RunCmd("adb devices");
            if (outputLines.Length < 5)
            {
                MessageBox.Show("请确保设备已打开调试模式并正常连接到电脑！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private static byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public void sendhex(string hex)
        {
            byte[] byteArray = strToHexByte(hex);
            serialPort1.Write(byteArray, 0, byteArray.Length);

        }
        public void open()
        {
            sendhex("A0 01 01 A2");
            Debug.WriteLine("打开继电器");
        }

        public void close()
        {
            sendhex("A0 01 00 A1");
            Debug.WriteLine("关闭继电器");
        }
        public void delay(int seconds, char char1)
        {
            if (char1 == '毫')
            {
                Task.Delay(seconds).Wait();
            }
            else if (char1 == '秒')
            {
                Task.Delay(seconds * 1000).Wait();
            }

        }
        private void saveLog(string[] str)
        {
            RunCmd2(str);
        }

        public bool check_cmd(string[] cmd)
        {
            foreach (string str2 in cmd)
            {

                if (str2.Length > 3 && str2[0] == '打')
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("请先设置串口！");
                        return false;
                    }
                }
                else if (str2.Length > 3 && str2[0] == '发')
                {
                    if (false)
                    {
                        MessageBox.Show("请先设置CAN参数！");
                        return false;
                    }
                }
                else if (str2.Length > 3 && str2[0] == '黑')
                {
                    if (myapi == null)
                    {
                        MessageBox.Show("请先标定摄像头！");
                        return false;
                    }
                }

            }
            return true;
        }


        private void runtest_main(string[] str)
        {
            string pattern = "\\d+";
            int blackcounter = 0;
            bool ret = check_cmd(str);
            if (ret)
            {
                int count = int.Parse(textBox4.Text);
                int j = 0;
                Debug.WriteLine(count);
                for (int i = 0; i < count; i++)
                {
                    if (taskcancel)
                    {
                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            label13.Text = "异常黑屏次数为:";
                        });

                        break;
                    }

                    j = i + 1;
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        label12.Text = "测试轮次：" + j.ToString();

                    });

                    foreach (string str2 in str)
                    {


                        if (str2.Length > 3 && str2[0] == '打')
                        {
                            open();

                            MatchCollection matches = Regex.Matches(str2, pattern);
                            if (matches.Count == 1)
                            {
                                string str3 = matches[0].Value;
                                int t = int.Parse(str3);
                                Debug.WriteLine("打开时间为：{0}", t);
                                if (str2[str2.Length - 2] == '毫')
                                {
                                    delay(t, '毫');
                                }
                                else
                                {
                                    delay(t, '秒');
                                }
                                //sendsrlcmd1("am start -n com.dfssi.android.systemsettings/com.dfssi.android.systemsettings.main.ui.MainActivity");
                                //Task.Delay(1000).Wait();
                                //sendsrlcmd1("input tap 1000 300");
                                //Task.Delay(7000).Wait();
                                //bool isblack = API.capAdetected();
                                //if (isblack)
                                //{
                                //    blackcounter++;
                                //    //这里面做黑屏处理！！！
                                //}

                            }

                        }
                        else if (str2.Length > 3 && str2[0] == '关')
                        {
                            close();
                            MatchCollection matches = Regex.Matches(str2, pattern);
                            if (matches.Count == 1)
                            {
                                string str3 = matches[0].Value;
                                int t = int.Parse(str3);
                                Debug.WriteLine("关闭时间为：{0}", t);
                                if (str2[str2.Length - 2] == '毫')
                                {
                                    delay(t, '毫');
                                }
                                else
                                {
                                    delay(t, '秒');
                                }

                            }
                        }
                        else if (str2.Length > 3 && str2[0] == '等')
                        {
                            MatchCollection matches = Regex.Matches(str2, pattern);
                            if (matches.Count == 1)
                            {
                                string str3 = matches[0].Value;
                                int t = int.Parse(str3);
                                Debug.WriteLine("等待时间为：{0}", t);
                                if (str2[str2.Length - 2] == '毫')
                                {
                                    delay(t, '毫');
                                }
                                else
                                {
                                    delay(t, '秒');
                                }
                            }
                        }
                        else if (str2.Length > 3 && str2[0] == '黑')
                        {

                            MatchCollection matches = Regex.Matches(str2, pattern);
                            if (matches.Count == 1)
                            {
                                string str3 = matches[0].Value;
                                int t = int.Parse(str3);
                                int counter = 0;
                                Mat temp = new Mat();
                                for (int k = 0; k < t; k++)
                                {

                                    bool isblack = myapi.capAdetected(temp);
                                    if (isblack)
                                    {
                                        counter++;
                                    }

                                }
                                if (counter == t)
                                {
                                    blackcounter++;
                                    //这里面做黑屏处理！！！
                                    API.save_pic(temp);
                                    Debug.WriteLine("黑屏次数为：{0}", blackcounter);
                                    this.BeginInvoke((MethodInvoker)delegate
                                    {

                                        label13.Text = "异常黑屏次数为：" + blackcounter.ToString();
                                    });
                                }
                                temp.Release();

                            }
                            else
                            {
                                Mat temp = new Mat();
                                bool isblack = myapi.capAdetected(temp);
                                if (isblack)
                                {
                                    blackcounter++;
                                    //这里面做黑屏处理！！！
                                    API.save_pic(temp);
                                    //Debug.WriteLine("黑屏次数为：{0}", blackcounter);
                                    this.BeginInvoke((MethodInvoker)delegate
                                    {

                                        label13.Text = "异常黑屏次数为：" + blackcounter.ToString();
                                    });
                                }
                                temp.Release();
                            }


                        }
                        else if (str2.Length > 3 && str2[0] == '点')
                        {
                            sendsrlcmd1("input tap 1000 300");
                        }
                        else if (str2.Length > 3 && str2[0] == '发')
                        {

                        }
                    }

                    // 异步更新label12的文本
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        //label12.Text = "测试轮次：" + j.ToString();
                        string str1 = null;
                        //label13.Text = "异常黑屏次数为：" + blackcounter.ToString();
                        if (myapi != null)
                        {
                            str1 = myapi.currentB;
                            label14.Text = "当前像素值：" + str1;
                        }

                    });
                }
            }
            else
            {
                return;
            }



        }
        private async void button3_Click(object sender, EventArgs e)
        {
            string file = comboBox5.Text;
            string white = textBox1.Text;

            if (file.Length < 1 && white.Length < 1)
            {
                Debug.WriteLine("白名单为：" + white);
                MessageBox.Show("请指定正确包名或者白名单后再重试！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (file.Length > 1)
                {
                    if (adbexist())
                    {
                        MessageBox.Show("将导出" + file + "日志！请确认！");
                        string[] cmdstr = new string[10];


                        cmdstr[0] = "mkdir " + file;
                        cmdstr[1] = "cd " + file;
                        cmdstr[2] = "adb root";
                        cmdstr[3] = "adb pull /data/" + file + ".log";
                        cmdstr[4] = "adb pull /data/logcat" + file + ".log";
                        cmdstr[5] = "adb pull /data/anr";
                        cmdstr[6] = "adb pull /data/system/dropbox";
                        cmdstr[7] = "adb pull /sdcard/ssilog/crash";
                        await Task.Run(() => saveLog(cmdstr));
                        MessageBox.Show("日志保存在./" + file);
                    }
                }
                else
                {
                    if (adbexist())
                    {
                        MessageBox.Show("将导出白名单日志！请确认！");
                        string[] cmdstr = new string[10];
                        cmdstr[0] = "mkdir whitelist";
                        cmdstr[1] = "cd whitelist";
                        cmdstr[2] = "adb root";
                        cmdstr[3] = "adb pull /data/monkey.log";
                        cmdstr[4] = "adb pull /data/logcat.log";
                        cmdstr[5] = "adb pull /data/anr";
                        cmdstr[6] = "adb pull /data/system/dropbox";
                        cmdstr[7] = "adb pull /sdcard/ssilog/crash";
                        await Task.Run(() => saveLog(cmdstr));

                    }
                }
            }
        }
        private string transACII(string file)
        {
            string str = null;
            switch (file)
            {
                case "手机互联":
                    str = "easyconn"; break;
                case "系统设置":
                    str = "systemsettings"; break;
                case "工装检测":
                    str = "generaltoolinginspection"; break;
                case "工程模式":
                    str = "engineeringmode"; break;
                case "驾驶辅助":
                    str = "h7camera"; break;
                case "空调":
                    str = "air"; break;
                case "媒体":
                    str = "fmradio"; break;
                case "用户手册":
                    str = "systemInfo"; break;
                case "UI ":
                    str = "h7lanuchnner"; break;
                case "日历":
                    str = "dialer"; break;
                case "开发模式":
                    str = "h7devtools"; break;
                case "虚拟开关":
                    str = "virtualSwitch"; break;
                case "视频":
                    str = "movieplayer"; break;
                case "图片":
                    str = "picture"; break;
            }
            return str;
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            RunCmd("adb push " + path + " /data");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sendsrlcmd("pgrep -f monkey | xargs kill -9");
            sendsrlcmd("pgrep -f logcat | xargs kill -9");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("请确保测试已完成并已导出相关日志！");
            if (packlist != null)
            {
                foreach (var str in packlist)
                {
                    deletelog(str);
                    Debug.WriteLine(str);
                }
                sendsrlcmd("rm /data/monkey.log");
                sendsrlcmd("rm /data/logcat.log");
                MessageBox.Show("日志已清空！");

            }
            else
            {
                MessageBox.Show("请点击初始化按钮！");
            }

        }
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            string[] Apacklist = null;
            string[] ApacklistCM = null;
            List<string> pack = new List<string>();
            List<string> packCM = new List<string>();
            dialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            //文件过滤
            dialog.Filter = "Text Files (*.txt)|*.txt|Binary Files (*.bin)|*.bin";
            //关闭选择多文件
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filepath = @dialog.FileName;
                using (StreamReader sr = new StreamReader(filepath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        packCM.Add(line);
                        line = line.Substring(line.LastIndexOf('.') + 1);
                        pack.Add(line);
                    }
                    ApacklistCM = packCM.ToArray();
                    Apacklist = pack.ToArray();
                }
                packlistCM = ApacklistCM;
                packlist = Apacklist;
                comboBox5.DataSource = Apacklist;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            serialinit();
        }

        private void button9_Click(object sender, EventArgs e)
        {

            string cmd = textBox2.Text;
            sendsrlcmd(cmd);
        }
        private bool cmdNempty()
        {
            foreach (var str in lastcmd)
            {
                if (string.IsNullOrEmpty(str))
                {
                    return true;
                }
            }
            return false;
        }
        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine(e.ToString);

            if (e.KeyCode == Keys.Enter)
            {
                string dataToSend = textBox2.Text;
                //serialPort1.DiscardInBuffer();

                if (!string.IsNullOrEmpty(dataToSend))
                {
                    try
                    {


                        sendsrlcmd(dataToSend);
                        bool Notfull = cmdNempty();
                        if (Notfull)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                if (string.IsNullOrEmpty(lastcmd[i]))
                                {
                                    lastcmd[i] = textBox2.Text;
                                    Gcmdindex = lastcmd1();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Debug.WriteLine("超过10个数据");
                            for (int i = 0; i < 9; i++)
                            {
                                lastcmd[i] = lastcmd[i + 1];
                            }
                            lastcmd[9] = textBox2.Text;
                            Gcmdindex = 9;
                        }
                        textBox2.Clear(); // 可选：清除文本框以便输入新的数据  
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error sending data: " + ex.Message);
                    }
                    //for (int i = 0; i < 10; i++)
                    //{
                    //    Debug.WriteLine("index{0} is {1}", i, lastcmd[i]);
                    //}
                    //Debug.WriteLine("Gcmdindex is {0}", Gcmdindex);
                }
                int bytesToRead = serialPort1.BytesToRead;
                Debug.WriteLine(bytesToRead);
                string indata = serialPort1.ReadExisting();

                if (this.IsHandleCreated)
                {

                    this.Invoke((MethodInvoker)delegate
                    {

                        // 线程安全地操作UI控件

                        richTextBox1.AppendText(indata);
                        richTextBox1.ScrollToCaret();

                    });

                }
                keydown = true;
                e.SuppressKeyPress = true; // 阻止发出默认的回车声  
            }
            if (e.KeyCode == Keys.Up)
            {

                textBox2.Clear();
                if (Gcmdindex == 11)
                {
                    Gcmdindex = lastcmd1();
                    textBox2.Text = lastcmd[Gcmdindex];
                    if (Gcmdindex > 0)
                    {
                        Gcmdindex = Gcmdindex - 1;
                    }
                }
                else
                {
                    textBox2.Text = lastcmd[Gcmdindex];
                    if (Gcmdindex > 0)
                    {
                        Gcmdindex = Gcmdindex - 1;
                    }
                    else
                    {
                        Gcmdindex = lastcmd1();
                    }
                }
            }
        }
        private int lastcmd1()
        {
            for (int i = 0; i < 10; i++)
            {
                if (string.IsNullOrEmpty(lastcmd[i]))
                {
                    return i - 1;
                }
            }
            return 9;
        }
        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_2(object sender, EventArgs e)
        {

        }





        private void button10_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(sender);
            serialPort1.Write(new byte[] { 0x03 }, 0, 1);
            serialPort1.Write("\r\n");
            Debug.WriteLine("发送crtl+c");
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            sendsrlcmd("setprop persist.sys.usb_model host");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sendsrlcmd("setprop persist.sys.usb_model device");
        }
        private string[] phase(string str)
        {
            char[] delimiterChars = { '，' };
            string[] parts = str.Split(delimiterChars);
            foreach (string part in parts)
            {
                Debug.WriteLine(part);
            }
            return parts;
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            //serialPort2.Close();


            //serialPort2.PortName = "COM4";
            //serialPort2.BaudRate = 1500000;
            //serialPort2.Parity = Parity.None;
            //serialPort2.StopBits = StopBits.One;
            //serialPort2.DataBits = 8;
            //serialPort2.Handshake = Handshake.None;
            //serialPort2.Encoding = Encoding.UTF8;

            //serialPort2.Open();
            //MessageBox.Show("串口2已打开");

            string str = textBox6.Text;
            string[] str_split = null;
            button11.Enabled = false;
            button11.Text = "测试中";
            int task_len = textBox6.Lines.Length;
            Debug.WriteLine(task_len);
            Task[] tasks = new Task[task_len];
            if (str.Length < 3)
            {
                MessageBox.Show("请正确输入控制参数！");
                return;
            }
            if (false)
            {
                foreach (var line in textBox6.Lines)
                {
                    int i = 0;
                    string temp = line.Substring(2);
                    str_split = phase(temp);


                    Debug.WriteLine(line, str_split);
                    tasks[i] = Task.Run(() => runtest_main(str_split));

                    i++;
                }
                foreach (var task in tasks)
                {
                    if (task != null)
                    {
                        await task;
                    }

                }
            }
            else
            {
                foreach (var line in textBox6.Lines)
                {
                    string temp = line.Substring(2);
                    str_split = phase(temp);
                    Debug.WriteLine(line, str_split);
                    await Task.Run(() => runtest_main(str_split));
                }
            }

            button11.Enabled = true;
            taskcancel = false;
            button11.Text = "开始测试";

        }

        private void button12_Click(object sender, EventArgs e)
        {
            taskcancel = true;
            MessageBox.Show("本轮测试完成，后续测试会中断！");
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

            myapi = new API(this);//CaptureFrame();
            myapi.test();

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            PopulateAdbDevice();
        }
        private void CaptureFrame()
        {
            var capture = new VideoCapture(0);


            if (!capture.IsOpened())
            {
                throw new ApplicationException("无法打开摄像头");
            }
            try
            {
                // 从摄像头读取一帧图像
                Mat frame = new Mat();
                if (capture.Read(frame))
                {
                    // 将Mat对象转换为Bitmap对象
                    Bitmap bitmap = BitmapConverter.ToBitmap(frame);
                    // 显示图像到PictureBox控件
                    pictureBox1.Invoke((MethodInvoker)delegate
                    {
                        pictureBox1.Image = bitmap;
                    });
                }
                else
                {
                    // 无法读取帧时的处理
                    Console.WriteLine("无法从摄像头读取帧");
                }
            }
            catch (Exception ex)
            {
                // 异常处理
                Console.WriteLine(ex.Message);
            }
            capture.Release();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // 鼠标按下时记录起始坐标
            roiX = e.X;
            roiY = e.Y;
            roiWidth = 0;
            roiHeight = 0;
            roi = new Rectangle(roiX, roiY, roiWidth, roiHeight);
            Debug.WriteLine("down");
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 鼠标移动时更新ROI大小
                roiWidth = e.X - roiX;
                roiHeight = e.Y - roiY;
                roi = new Rectangle(roiX, roiY, Math.Abs(roiWidth), Math.Abs(roiHeight));
                // 重绘PictureBox以显示ROI
                Debug.WriteLine("move");

            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //
            ControlPaint.DrawReversibleFrame(roi, Color.Red, FrameStyle.Thick); // 重绘PictureBox以显示ROI

            //if (roi.Width > 0 && roi.Height > 0)
            //{
            //    // 提取ROI区域的图像
            //    Bitmap roiImage = image.Clone(roi, image.PixelFormat);
            //    // 计算平均像素值的代码
            //    // ...
            //    roiImage.Dispose(); // 释放ROI图像资源
            //    Debug.WriteLine("up");

            //}
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Mat temp = new Mat();
            bool isblack = myapi.capAdetected(temp);
            //API.Rectdetected_frompic();
            string str1 = null;


            str1 = myapi.currentB;

            label14.Text = "当前像素值：" + str1;
            //myapi.test1(this);

            //temp.Release();

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private async void button16_Click(object sender, EventArgs e)
        {
            await Task.Run(() => blacktest());

            taskcancel = false;
        }
        public void blacktest()
        {
            int blackcounter = 0;
            while (!taskcancel)
            {
                myapi.blacktest();
            }

        }
        public int channel_index_ = 1;
        public IntPtr device_handle_;
        public IntPtr channel_handle_;
        public IProperty property_;

        List<string> list_box_data_ = new List<string>();
        static object lock_obj = new object();

        public bool m_bOpen = false;
        public bool m_bStart = false;

        uint[] kBaudrate =
        {
            //1000000,//1000kbps
            //800000,//800kbps
            500000,//500kbps
            250000,//250kbps
            125000,//125kbps
            100000,//100kbps
            50000,//50kbps
            20000,//20kbps
            10000,//10kbps
            5000 //5kbps
        };

        private bool setBaudrate(UInt32 baud)
        {
            string path = channel_index_ + "/baud_rate";
            string value = baud.ToString();
            //char* pathCh = (char*)System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(path).ToPointer();
            //char* valueCh = (char*)System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(value).ToPointer();
            return 1 == Method.ZCAN_SetValue(device_handle_, path, Encoding.ASCII.GetBytes(value));
        }

        private void button17_Click(object sender, EventArgs e)
        {
            device_handle_ = Method.ZCAN_OpenDevice(Define.ZCAN_USBCAN_2E_U, 0, 0);
            Debug.WriteLine("handle is {0}", device_handle_);
            if ((int)device_handle_ == 0)
            {
                MessageBox.Show("打开设备失败,请检查设备类型和设备索引号是否正确", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            m_bOpen = true;
            if (!m_bOpen)
            {
                MessageBox.Show("设备还没打开", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            uint type = Define.ZCAN_USBCAN_2E_U;
            if (!setBaudrate(kBaudrate[0]))
            {
                MessageBox.Show("设置波特率失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            ZCAN_CHANNEL_INIT_CONFIG config_ = new ZCAN_CHANNEL_INIT_CONFIG();

            IntPtr pConfig = Marshal.AllocHGlobal(Marshal.SizeOf(config_));
            Marshal.StructureToPtr(config_, pConfig, true);

            //int size = sizeof(ZCAN_CHANNEL_INIT_CONFIG);
            //IntPtr ptr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(size);
            //System.Runtime.InteropServices.Marshal.StructureToPtr(config_, ptr, true);
            channel_handle_ = Method.ZCAN_InitCAN(device_handle_, 1, pConfig);
            Debug.WriteLine("chan handle is {0}", channel_handle_);

            Marshal.FreeHGlobal(pConfig);


            //Marshal.FreeHGlobal(ptr);

            if ((int)channel_handle_ == 0)
            {
                MessageBox.Show("初始化CAN失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (Method.ZCAN_StartCAN(channel_handle_) != Define.STATUS_OK)
            {
                MessageBox.Show("启动CAN失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            m_bStart = true;


        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            API.Yuandetected_frompic();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            serialPort1.DiscardInBuffer();
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        

        //private void pictureBox1_Paint(object sender, PaintEventArgs e)
        //{
        //    e.Graphics.Clear(pictureBox1.BackColor); // 清除背景
        //    if (roi.Width > 0 && roi.Height > 0)
        //    {
        //        // 绘制 ROI 边框
        //        ControlPaint.DrawReversibleFrame(roi, Color.Red, FrameStyle.Thick);
        //    }
        //}

        public string TextBoxValue
        {
            get { return textBox8.Text; }
        }
    }
}
