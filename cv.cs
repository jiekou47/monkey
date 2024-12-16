using OpenCvSharp;
using System.Diagnostics;
using Point = OpenCvSharp.Point;
using Size = OpenCvSharp.Size;
using Rect = OpenCvSharp.Rect;
using monkey;
using System.Text.RegularExpressions;


namespace driver
{
    internal class API
    {
        public API(Form1 myform) {
            roi = int.Parse(myform.TextBoxValue);
            
        }
        public string currentB = null;
        public static int num = 0;
        VideoCapture capture;
        static Point[] rectStart = new Point[5];
        static Point[] rectEnd = new Point[5];
        static Point[] Lpoint = new Point[10];
        static bool isRectDrawing;
        static int roi_heigt = 0;
        static int roi_width = 0;
        public static Mat img = new Mat();
        //public Mat temp = new Mat();
        static Scalar color = new Scalar(0, 0, 255); // 红色线条
        
        static int roi = 0;
        static Rect[] rect = new Rect[5];
        public bool[] boolArray = new bool[5] ;
        public static bool Rectdetected_frompic()
        {
            //读取图像
            Mat image = Cv2.ImRead("C:\\Users\\34401\\Desktop\\5.jpg", ImreadModes.Color);

            if (image.Empty())
            {
                Debug.WriteLine("图片未找到！");
                return false;
            }

            // 转换为灰度图像
            Mat grayImage = new Mat();

            Cv2.CvtColor(image, grayImage, ColorConversionCodes.RGB2GRAY);   //灰度图
            //二值化
            //Mat binImage = new Mat();
            //double thresholdValue = 127.0; // 阈值，可以根据实际情况调整  
            //double maxValue = 255.0; // 最大值，通常为255  
            //ThresholdTypes thresholdType = ThresholdTypes.Binary; // 二值化类型  
            //Cv2.Threshold(grayImage, binImage, thresholdValue, maxValue, thresholdType);
            // 应用高斯模糊
            Mat blurredImage = new Mat();
            Cv2.GaussianBlur(grayImage, blurredImage, new Size(5, 5), 0);

            // 应用 Canny 算法检测边缘
            Mat cannyImage = new Mat();
            Cv2.Canny(blurredImage, cannyImage, 75, 200);

            // 查找轮廓
            OpenCvSharp.Point[][] contours;

            List<OpenCvSharp.Point[]> pointsList = new List<OpenCvSharp.Point[]>();

            HierarchyIndex[] hierarchly;
            //Cv2.FindContours(cannyImage, contours, hierarchly, RetrType.List, ChainApproxMethod.ChainApproxSimple);
            Cv2.FindContours(cannyImage, out contours, out hierarchly, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple, new OpenCvSharp.Point(0, 0));   //获得轮廓

            // 遍历所有检测到的轮廓  


            // 遍历轮廓，筛选矩形
            for (int i = 0; i < contours.Length; i++)
            {
                Debug.WriteLine("总轮廓个数为：{0}", contours.Length);
                // 对轮廓点进行多边形近似
                Point[] points = Cv2.ApproxPolyDP(contours[i], 0.02 * Cv2.ArcLength(contours[i], true), true);
                double cnt_len = Cv2.ArcLength(contours[i], true);//计算轮廓周长

                // 筛选出四个顶点的轮廓，即矩形
                if (points.Length < 4 || Cv2.ContourArea(contours[i]) < 10000)
                {
                    continue;
                }

                else
                {

                    // 绘制矩形轮廓
                    //cv2.drawcontours(dst_image, contours, i, color, 2, linetypes.link8, hierarchly);
                    //Cv2.drawcontours(image, new[] { new seq<point>(contour) }, -1, scalar.red, 3);
                    double area = Cv2.ContourArea(contours[i]);
                    //Debug.WriteLine("轮廓面积为：{0}", area);

                    pointsList.Add(contours[i]);

                    Debug.WriteLine("识别到一个矩形！");


                }
            }
            OpenCvSharp.Point[][] pointArray = pointsList.ToArray();
            Debug.WriteLine("矩形轮廓个数为：{0}", pointArray.Length);
            for (int i = 0; i < pointArray.Length; i++)
            {
                double area = Cv2.ContourArea(pointArray[i]);
                Debug.WriteLine("轮廓面积为：{0}", area);
            }

            Cv2.DrawContours(image, pointArray, -1, Scalar.Red, 3);
            // 显示图像
            Cv2.ImShow("Image with Rectangles", image);
            Cv2.WaitKey(0);
            return false;
         }

        public void blackdetected_fromcap()
        {
            
        }
        
        public static void Get_roi(Mat frame) {
            Cv2.NamedWindow("show");
            // 调整窗口大小
            Cv2.ResizeWindow("show", 1200, 1000);
            //Cv2.PutText(frame, "Hello, OpenCVSharp!", new Point(50, 50),
            //HersheyFonts.HersheyTriplex, 1.0, new Scalar(255, 255, 255), 2, LineTypes.Link8);
            Cv2.ImShow("show", frame);
            
            Cv2.SetMouseCallback("show", MouseCallback);
            Cv2.WaitKey(0);
            return;
        }

        public void test()
        {
            // 创建一个空白图像
            Point start_point = new Point(300, 115);
            Point end_point = new Point(475, 225);
            // 绘制矩形
            //Cv2.Rectangle(img, start_point, end_point, color, 3, (LineTypes)8, 0);
            // 显示图像
            using (var capture = new VideoCapture()) {
                capture.Open(0);
                capture.Read(img);
                for(int i = 0; i < 5; i++)
                {
                    //boolArray[i] = false;
                    rectStart[i].X = 0;
                    rectStart[i].Y = 0;
                    rectEnd[i].X = 0;
                    rectEnd[i].Y = 0;
                    rect[i].X = 0;
                    rect[i].Y = 0;
                    rect[i].Width = 0;
                    rect[i].Height = 0;

                }
                API.Get_roi(img);
                //capture.Release();
                Cv2.WaitKey(0);
                Cv2.DestroyAllWindows();
            } 
            
        }
        public bool blacktest()
        {

            int blackcounter = 0;
            Mat temp = new Mat();
            Mat temp1 = new Mat();
            for (int i = 0; i < 2; i++)
            {   //Mat temp1 = new Mat();
                if (i == 0)
                {
                    bool isblack = capAdetected(temp);
                    if (isblack)
                    {

                        blackcounter++;
                    }
                }
                else
                {
                    bool isblack = capAdetected(temp1);
                    if (isblack)
                    {

                        blackcounter++;
                    }
                }
                
                
                
            }
            if (blackcounter == 2)
            {
                using (var capture1 = new VideoCapture())
                {
                    //Mat temp_ = new Mat();
                    DateTime now = DateTime.Now;
                    string fi = now.ToString().Replace("/", "").Replace(":", "");
                    //int Bvalue = (int)meanScalar[0];
                    //string Scounter = counter.ToString();
                    fi = fi + ".jpg";

                    Debug.WriteLine("文件名为：" + fi);
                    string filename = Path.Combine("pic", fi);
                    //Cv2.ImWrite(filename, temp);
                    if (Cv2.ImWrite(filename, temp1)) // ImWrite返回true表示保存成功
                    {
                        Debug.WriteLine($"图像已保存为：{filename}");
                    }
                    else
                    {
                        Debug.WriteLine("保存图像失败");
                    }
                    //img = null;
                    Debug.WriteLine("testing");
                    //capture1.Open(0);
                    //if (!capture1.IsOpened())
                    //{
                    //    throw new ApplicationException("无法打开摄像头");
                    //}
                    //try
                    //{
                    //    if (capture1.Read(temp_))
                    //    {
                    //        DateTime now = DateTime.Now;
                    //        string fi = now.ToString().Replace("/", "").Replace(":", "");
                    //        int Bvalue = (int)meanScalar[0];
                    //        string Scounter = counter.ToString();
                    //        fi = fi + " " + " " + ".jpg";

                    //        Debug.WriteLine("文件名为：" + fi);
                    //        string filename = Path.Combine("pic", fi);
                    //        Cv2.ImWrite(filename, temp);
                    //        if (Cv2.ImWrite(filename, temp)) // ImWrite返回true表示保存成功
                    //        {
                    //            Debug.WriteLine($"图像已保存为：{filename}");
                    //        }
                    //        else
                    //        {
                    //            Debug.WriteLine("保存图像失败");
                    //        }
                    //        img = null;
                    //        Debug.WriteLine("testing");
                    //    }

                    //    temp_.Release();
                    temp1.Release();
                    temp.Release();
                    return true;

                    }
                   

                    

                }
            temp1.Release();
            temp.Release();
            return false;   
            }

        public void save_pic(Mat temp)
        {
   
            DateTime now = DateTime.Now;
            string fi = now.ToString().Replace("/", "").Replace(":", "");
            fi = fi + " " + " " + ".jpg";
            Debug.WriteLine("文件名为：" + fi);
            string filename = Path.Combine("pic", fi);
            string folderPath = @"pic";

            // 检查文件夹是否存在
            if (!Directory.Exists(folderPath))
            {
                // 文件夹不存在，创建文件夹
                Directory.CreateDirectory(folderPath);
                Console.WriteLine("文件夹已创建。");
            }
            else
            {
                // 文件夹已存在
                Console.WriteLine("文件夹已存在。");
            }
            if (Cv2.ImWrite(filename, temp)) // ImWrite返回true表示保存成功
            {
                Debug.WriteLine($"图像已保存为：{filename}");
            }
            else
            {
                Debug.WriteLine("保存图像失败");
            }
            Debug.WriteLine("testing");
        }
            

            
         
        public static void MouseCallback(MouseEventTypes eventType, int x, int y, MouseEventFlags flags, IntPtr userdata)
        {
            // 根据事件类型处理鼠标事件
           
            if (eventType == MouseEventTypes.LButtonDown)
            {
                isRectDrawing = false;
                for(int i = 0; i < 5; i++) {
                    bool isEmpty = rectStart[i].X == 0 && rectStart[i].Y == 0;
                    if (isEmpty)
                    {
                        rectStart[i] = new Point(x, y);
                        Debug.WriteLine($"Left button down at: ({x}, {y})");
                        num = i;
                        break;
                    }
                    
                }
                
            }
            else if (eventType == MouseEventTypes.LButtonUp)
            {
                isRectDrawing = true;
               
                rectEnd[num] = new Point(x, y);
                roi_heigt = y - rectStart[num].Y;
                roi_width = x - rectStart[num].X;
                Cv2.Rectangle(img, rectStart[num], rectEnd[num], color, 3, (LineTypes)8, 0);
                Debug.WriteLine("鼠标松开");
                Debug.WriteLine($"Left button up at: ({x}, {y})");
                //API.showE(img);
                
                


                for (int i = 0; i < 5; i++)
                {
                    bool isEmpty = (rect[i].Width == 0 && rect[i].Height == 0);
                    if (isEmpty)
                    {
                        rect[i] = new Rect(rectStart[num].X, rectStart[num].Y, roi_width, roi_heigt);
                        Debug.WriteLine("roi_heigt is {0},roi_width is {1},i is {2}", roi_heigt, roi_width,i);
                        break;
                    }
                }
                Debug.WriteLine("for over");
                API.showE(img);

            }
            else if (eventType == MouseEventTypes.MouseMove && isRectDrawing)
            {
                
                
                
               // Debug.WriteLine("鼠标移动中");
            }
        }

        public void test1(Form1 myform)
        {
            Debug.WriteLine("form1.textbox is {0}", myform.textBox1.Text);
            myform.textBox1.Text = "text1";
        }
        public  bool capAdetected(Mat temp) {
            using (var capture1 = new VideoCapture()) {
                capture1.Open(0);
                if (!capture1.IsOpened())
                {
                    throw new ApplicationException("无法打开摄像头");
                }
                try
                {
                    // 从摄像头读取一帧图像
                    //Mat frame = new Mat();
                    if (capture1.Read(temp))
                    {
                        Size newSize = new Size(1200, 1000);
                        Mat Rframe = new Mat();
                        Cv2.Resize(temp, Rframe, newSize);
                        //showE(frame);
                        // 提取ROI区域
                        for(int i = 0; i < 5; i++)
                        {
                            bool isEmpty = (rect[i].Width == 0 && rect[i].Height == 0);
                            if (!isEmpty)
                            {
                                Mat roiMat = temp[rect[i]];
                                // 计算平均值
                                Scalar meanScalar = Cv2.Mean(roiMat);
                                currentB = ((int)meanScalar[0]).ToString();
                                // 输出平均值
                                Debug.WriteLine($"ROI的平均像素值：{meanScalar[0]}（B通道）, {meanScalar[1]}（G通道）, {meanScalar[2]}（R通道）");
                                //float roi_value = roi_mean(Rframe);
                                Debug.WriteLine("rect {0} 不为空",i);
                                Debug.WriteLine("roi is {0}", roi);

                                if (meanScalar[0] < roi)
                                {

                                    //DateTime now = DateTime.Now;
                                    //string fi = now.ToString().Replace("/", "").Replace(":", "");
                                    //int Bvalue = (int)meanScalar[0];
                                    //string Scounter = counter.ToString();
                                    //fi = fi + " " + Bvalue.ToString() + " " + Scounter + ".jpg";

                                    //Debug.WriteLine("文件名为：" + fi);
                                    //string filename = Path.Combine("pic", fi);
                                    //Cv2.ImWrite(filename, frame);
                                    //if (Cv2.ImWrite(filename, frame)) // ImWrite返回true表示保存成功
                                    //{
                                    //    Debug.WriteLine($"图像已保存为：{filename}");
                                    //}
                                    //else
                                    //{
                                    //    Debug.WriteLine("保存图像失败");
                                    //}
                                    boolArray[i] = true;
                            }
                                roiMat.Release();
                            }
                        }
                        bool error = iserror();
                        if (error)
                        {
                            //DateTime now = DateTime.Now;
                            //string fi = now.ToString().Replace("/", "").Replace(":", "");
                            ////int Bvalue = (int)meanScalar[0];
                            //string Scounter = counter.ToString();
                            //fi = fi + " " + " " + Scounter + ".jpg";

                            //Debug.WriteLine("文件名为：" + fi);
                            //string filename = Path.Combine("pic", fi);
                            //Cv2.ImWrite(filename, frame);
                            //if (Cv2.ImWrite(filename, frame)) // ImWrite返回true表示保存成功
                            //{
                            //    Debug.WriteLine($"图像已保存为：{filename}");
                            //}
                            //else
                            //{
                            //    Debug.WriteLine("保存图像失败");
                            //}
                            for (int i = 0; i < 4; i++)
                            {
                                boolArray[i] = false;
                            }
                            Rframe.Release();

                            //frame.Release();
                            capture1.Release();
                            return true;
                        }
                        Rframe.Release();
                      
                        //frame.Release();
                      
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

                capture1.Release();
               

            }     
            return false;
        }
        static float roi_mean(Mat frame)
        {
            return 1;
        }
        public bool iserror()
        {
            int hash = 0;
           
            for (int i = 0; i < num + 1; i++)
            {
                if (boolArray[i])
                {
                    hash++;
                }
            }
            if (hash == num + 1)
            {
                return true;
            }


            return false;
        }
        public static void showE(Mat frame)
        {
            //Cv2.NamedWindow("show");
            // 调整窗口大小
            //Cv2.ResizeWindow("show", 1200, 1000);
            //Cv2.PutText(frame, "Hello, OpenCVSharp!", new Point(50, 50),
            // HersheyFonts.HersheyTriplex, 1.0, new Scalar(255, 255, 255), 2, LineTypes.Link8);
           // Cv2.Rectangle(frame, rectStart[num], rectEnd[num], color, 3, (LineTypes)8, 0);

            Cv2.ImShow("show", frame);
            //Cv2.SetMouseCallback("show", MouseCallback);
            Cv2.WaitKey(0);
            
        }
    }
}
