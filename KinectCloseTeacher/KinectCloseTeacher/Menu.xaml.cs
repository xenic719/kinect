using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using System.Windows.Threading;

namespace KinectCloseTeacher
{
    public partial class Menu : Page
    {
        KinectSensor sensor = (from sensorToCheck in KinectSensor.KinectSensors
                               where sensorToCheck.Status == KinectStatus.Connected
                               select sensorToCheck).FirstOrDefault();
        //變數初始化定義
        public int selectUnit = 0;
        public int selectAction = 0;
        public Unit_1 unit_1;
        public Unit_2 unit_2;
        public Unit_3 unit_3;
        public double Min_Right_Knee;
        public double Min_Left_Knee;
        public double Min_Knee;
        SignAction sign;
        byte[] pixelData;
        Skeleton[] skeletons;
        bool IniSet = false;

        public Joint head, rightHand, leftHand, leftShoulder, rightShoulder, centerShoulder, rightElbow, leftFoot, rightFoot, leftHip
                    , rightHip, centerHip, leftKnee, rightKnee, leftWrist, rightWrist, spine, leftAnkle, rightAnkle, leftElbow;

        private void EndSystem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //關閉Kinect
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
           //sensor.Stop();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //初始化
            unit_1 = new Unit_1();
            unit_2 = new Unit_2();
            unit_3 = new Unit_3();
            sign = new SignAction();

            //啟動框架
            sensor.SkeletonFrameReady += runtime_SkeletonFrameReady;
            sensor.ColorFrameReady += runtime_VideoFrameReady;
        }

        //計算膝蓋Z軸距離
        private void Lock_Click(object sender, RoutedEventArgs e)
        {
            Min_Left_Knee = leftKnee.Position.Z;
            Min_Right_Knee = rightKnee.Position.Z;
            Min_Knee = Min_Left_Knee - Min_Right_Knee;
            MessageBox.Show(Convert.ToString(Min_Knee));
        }
        
        public Menu()
        {
            InitializeComponent();
            sensor.Start(); //啟動Kinect Sensor
            sensor.ElevationAngle = 0; //調整Kinect角度為0
            //載入與卸載
            this.Loaded += new RoutedEventHandler(Window_Loaded);
            this.Unloaded += new RoutedEventHandler(Window_Unloaded);

            //平滑處理，防止高頻率微小抖動和突發大跳動造成的關節雜訊
            var parameters = new TransformSmoothParameters
            {
                Smoothing = 0.3f,
                Correction = 0.0f,
                Prediction = 0.0f,
                JitterRadius = 1.0f,
                MaxDeviationRadius = 0.5f
            };

            sensor.SkeletonStream.Enable(parameters);//載入平滑處理參數
            sensor.ColorStream.Enable();//開啟，彩色影像
            sensor.SkeletonStream.Enable();//開啟，骨架追蹤
        }

        //彩色影像，處理函數
        void runtime_VideoFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            bool receivedData = false;
            using (ColorImageFrame CFrame = e.OpenColorImageFrame())
            {
                if (CFrame == null)
                {
                    // The image processing took too long. More than 2 frames behind.
                }
                else
                {
                    pixelData = new byte[CFrame.PixelDataLength];
                    CFrame.CopyPixelDataTo(pixelData);
                    receivedData = true;
                }
            }
            if (receivedData)
            {
                BitmapSource source = BitmapSource.Create(640, 480, 150, 150, PixelFormats.Bgr32, null, pixelData, 640 * 4);
                videoImage.Source = source;
            }
        }

        //骨架追蹤，處理函數
        void runtime_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            bool receivedData = false;
            using (SkeletonFrame SFrame = e.OpenSkeletonFrame())
            {
                if (SFrame != null)
                {
                    Skeleton[] FrameSkeletons = new Skeleton[SFrame.SkeletonArrayLength];
                    SFrame.CopySkeletonDataTo(FrameSkeletons);
                    skeletons = new Skeleton[SFrame.SkeletonArrayLength];
                    SFrame.CopySkeletonDataTo(skeletons);
                    // The image processing took too long. More than 2 frames behind.
                    receivedData = true;
                }
            }

            if (receivedData)
            {
                Skeleton currentSkeleton = (from s in skeletons where s.TrackingState == SkeletonTrackingState.Tracked select s).FirstOrDefault();
                if (currentSkeleton != null)
                {
                    //取得骨架關節點 3D(X、Y、Z)座標值。
                    head = currentSkeleton.Joints[JointType.Head];
                    rightHand = currentSkeleton.Joints[JointType.HandRight];
                    leftHand = currentSkeleton.Joints[JointType.HandLeft];
                    leftShoulder = currentSkeleton.Joints[JointType.ShoulderLeft];
                    rightShoulder = currentSkeleton.Joints[JointType.ShoulderRight];
                    centerShoulder = currentSkeleton.Joints[JointType.ShoulderCenter];
                    leftAnkle = currentSkeleton.Joints[JointType.AnkleLeft];
                    rightAnkle = currentSkeleton.Joints[JointType.AnkleRight];
                    leftElbow = currentSkeleton.Joints[JointType.ElbowLeft];
                    rightElbow = currentSkeleton.Joints[JointType.ElbowRight];
                    leftFoot = currentSkeleton.Joints[JointType.FootLeft];
                    rightFoot = currentSkeleton.Joints[JointType.FootRight];
                    leftHip = currentSkeleton.Joints[JointType.HipLeft];
                    rightHip = currentSkeleton.Joints[JointType.HipRight];
                    centerHip = currentSkeleton.Joints[JointType.HipCenter];
                    leftKnee = currentSkeleton.Joints[JointType.KneeLeft];
                    rightKnee = currentSkeleton.Joints[JointType.KneeRight];
                    leftWrist = currentSkeleton.Joints[JointType.WristLeft];
                    rightWrist = currentSkeleton.Joints[JointType.WristRight];
                    spine = currentSkeleton.Joints[JointType.Spine];

                    //設定紅點座標
                    
                    SetEllipsePosition(ellipseHead, head, false);
                    SetEllipsePosition(ellipseLeftHand, leftHand, false);
                    SetEllipsePosition(ellipseRightHand, rightHand, false);
                    SetEllipsePosition(ellipseLeftFoot, leftFoot, false);
                    SetEllipsePosition(ellipseRightFoot, rightFoot, false);
                    SetEllipsePosition(ellipseLeftElbow, leftElbow, false);
                    SetEllipsePosition(ellipseRightElbow, rightElbow, false);
                    SetEllipsePosition(ellipseLeftAnkle, leftAnkle, false);
                    SetEllipsePosition(ellipseRightAnkle, rightAnkle, false);
                    SetEllipsePosition(ellipseLeftWrist, leftWrist, false);
                    SetEllipsePosition(ellipseRightWrist, rightWrist, false);
                    SetEllipsePosition(ellipseLeftShoulder, leftShoulder, false);
                    SetEllipsePosition(ellipseRightShoulder, rightShoulder, false);
                    SetEllipsePosition(ellipseLeftKnee, leftKnee, false);
                    SetEllipsePosition(ellipseRightKnee, rightKnee, false);
                    SetEllipsePosition(ellipseCenterShoulder, centerShoulder, false);
                    SetEllipsePosition(ellipseLeftHip, leftHip, false);
                    SetEllipsePosition(ellipseRightHip, rightHip, false);
                    SetEllipsePosition(ellipseCenterHip, centerHip, false);
                    SetEllipsePosition(ellipseSpine, spine, false);
                    
                    if(IniSet)
                    {
                        
                    }

                    KinectStatusSign.Content = "Kinect已連結";
                }
                else
                {
                    KinectStatusSign.Content = "Kinect尚未連結";
                }
            }
        }

        //設定圖案位置
        private void SetEllipsePosition(Ellipse ellipse, Joint joint, bool isHighlighted)
        {
            //將3D 座標轉換成螢幕上大小，如640*320 的 2D 座標值
            Microsoft.Kinect.SkeletonPoint vector = new Microsoft.Kinect.SkeletonPoint();
            vector.X = ScaleVector(640, joint.Position.X);
            vector.Y = ScaleVector(480, -joint.Position.Y);
            vector.Z = joint.Position.Z; // Z值原封不動

            Joint updatedJoint = new Joint();
            updatedJoint = joint;
            updatedJoint.TrackingState = JointTrackingState.Tracked;
            updatedJoint.Position = vector;

            //得到 2D座標值(X、Y)後，將值設定為圖案顯示的位置
            Canvas.SetLeft(ellipse, updatedJoint.Position.X);
            Canvas.SetTop(ellipse, updatedJoint.Position.Y);
        }

        //處理螢幕大小2D座標值
        private float ScaleVector(int length, float position)
        {
            float value = (((((float)length) / 1f) / 2f) * position) + (length / 2);
            if (value > length)
            {
                return (float)length;
            }
            if (value < 0f)
            {
                return 0f;
            }
            return value;
        }

        //選擇Unit_0按鈕
        private void U5_Click(object sender, RoutedEventArgs e)
        {
            selectUnit = 5;
            selectAction = 1;
            Unit_Page up = new Unit_Page(selectUnit, selectAction);
            up.Tag = this;
            this.NavigationService.Navigate(up);
        }

        //選擇Unit_1按鈕
        private void U1_Click(object sender, RoutedEventArgs e)
        {
            selectUnit = 1;
            selectAction = 1;
            sensor.Stop();
            Unit_Page up = new Unit_Page(selectUnit,selectAction);
            up.Tag = this;
            this.NavigationService.Navigate(up);
        }

        //選擇Unit_2按鈕
        private void U2_Click(object sender, RoutedEventArgs e)
        {
            selectUnit = 2;
            selectAction = 1;
            Unit_Page up = new Unit_Page(selectUnit, selectAction);
            up.Tag = this;
            this.NavigationService.Navigate(up);
        }

        //選擇Unit_3按鈕
        private void U3_Click(object sender, RoutedEventArgs e)
        {
            selectUnit = 3;
            selectAction = 1;
            Unit_Page up = new Unit_Page(selectUnit, selectAction);
            up.Tag = this;
            this.NavigationService.Navigate(up);
        }
    }
}
