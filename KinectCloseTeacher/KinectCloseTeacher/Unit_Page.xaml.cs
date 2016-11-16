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
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Kinect;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System.IO;
using System.Threading;
using System.Windows.Media.Animation;
using System.Media;
using Microsoft.Speech.Synthesis;
using System.Net.Http;
using System.Net;


namespace KinectCloseTeacher
{
    /// <summary>
    /// Unit_Page.xaml 的互動邏輯
    /// </summary>
    public partial class Unit_Page : Page
    {
        DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.1) };
        int sec = 100,failSecond;

        KinectSensor sensor = (from sensorToCheck in KinectSensor.KinectSensors
                               where sensorToCheck.Status == KinectStatus.Connected
                               select sensorToCheck).FirstOrDefault();

        private SpeechRecognitionEngine speechRecognizer;
        private DispatcherTimer readyTimer;
        private SpeechSynthesizer synthesizer; //文字轉語音
        public int times = 0, second = 0, preTime;
        bool flag = false, finishflag = false;
        byte[] pixelData;
        string[] LPV = new string[2];
        string[] RPV = new string[2];
        string[] LNV = new string[2];
        string[] RNV = new string[2];
        string[] GetVideo = new string[4];
        int unit = 0, action = 0, finish = 0;
        float[] res = new float[2];
        Skeleton[] skeletons;
        Menu menu = new Menu();
        SignAction sign = new SignAction();
        AllAction AAction = new AllAction();
        Unit_Action_Check UAC = new Unit_Action_Check();
        public Joint head, rightHand, leftHand, leftShoulder, rightShoulder, centerShoulder, rightElbow, leftFoot, rightFoot, leftHip
                   , rightHip, centerHip, leftKnee, rightKnee, leftWrist, rightWrist, spine, leftAnkle, rightAnkle, leftElbow, aa, bb;

        int actionunit;
        int actionname;
        int actiontime;
        int wrongnumber = 0 ;
        float differece = 0;
        float totaldifferece = 0;



        //用if區分
        public Unit_1 unit_1;
        public Unit_2 unit_2;
        public Unit_3 unit_3;
        private void test_Click(object sender, RoutedEventArgs e)
        {
            /*string address = "http://www.google.com/";
            //string address = "http://www.google.com/s?"+"ActionUnit="+ "&ActionName="+ "&ActionTime="+ "&WrongNumber="+"&Diffierence=";
            HttpWebRequest response = (HttpWebRequest)WebRequest.Create(address);
            response.Method = "GET";
            using (WebResponse wr = response.GetResponse())
            {
                //在這裡對接收到的頁面內容進行處理
                //Console.Write((int)response.StatusCode);
            }
            */
        }

        public Unit_4 unit_4;
        public WarmUp WU;
        
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.speechRecognizer != null && sensor != null)
            {
                sensor.AudioSource.Stop();
                sensor.Stop();
                this.speechRecognizer.RecognizeAsyncCancel();
                this.speechRecognizer.RecognizeAsyncStop();
            }
            if (this.readyTimer != null)
            {
                this.readyTimer.Stop();
                this.readyTimer = null;
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        public Unit_Page(int selectUnit,int selectAction)
        {
            
            if ( selectUnit == 1)
            {
                unit_1 = new Unit_1();
            }
            else if(selectUnit == 2)
            {
                unit_2 = new Unit_2();
            }
            else if (selectUnit == 3)
            {
                unit_3 = new Unit_3();
            }
            else if (selectUnit == 4)
            {
                unit_4 = new Unit_4();
            }
                
            else if (selectUnit == 0)
            {
                WU = new WarmUp();
            }
                

            InitializeComponent();
            unit = selectUnit;
            action = selectAction;  
            //sensor.Start(); //啟動Kinect Sensor
            sensor.ElevationAngle = 0; //調整Kinect角度為0
            //載入與卸載
            var parameters = new TransformSmoothParameters
            {
                Smoothing = 0.3f,
                Correction = 0.0f,
                Prediction = 0.0f,
                JitterRadius = 1.0f,
                MaxDeviationRadius = 0.5f
            };
            setVoice();
            sensor.SkeletonStream.Enable(parameters);//載入平滑處理參數
            sensor.ColorStream.Enable();//開啟，彩色影像
            sensor.SkeletonStream.Enable();//開啟，骨架追蹤

            GetVideo = sign.ReturnVideo(unit);
            VideoElement.Source = new Uri(Directory.GetCurrentDirectory() + GetVideo[0]);
            VideoElement.LoadedBehavior = MediaState.Manual;
            VideoElement.UnloadedBehavior = MediaState.Manual;
            VideoElement.Play();

            GoodElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\good.m4a");
            GoodElement.LoadedBehavior = MediaState.Manual;
            GoodElement.UnloadedBehavior = MediaState.Manual;

            BGMusicElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\黃金傳說 - 做菜歌.mp3");
            BGMusicElement.LoadedBehavior = MediaState.Manual;
            BGMusicElement.UnloadedBehavior = MediaState.Manual;
            BGMusicElement.Play();

            OneElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\1.mp3");
            OneElement.LoadedBehavior = MediaState.Manual;
            OneElement.UnloadedBehavior = MediaState.Manual;

            TwoElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\2.mp3");
            TwoElement.LoadedBehavior = MediaState.Manual;
            TwoElement.UnloadedBehavior = MediaState.Manual;

            ThreeElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\3.mp3");
            ThreeElement.LoadedBehavior = MediaState.Manual;
            ThreeElement.UnloadedBehavior = MediaState.Manual;

            FourElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\4.mp3");
            FourElement.LoadedBehavior = MediaState.Manual;
            FourElement.UnloadedBehavior = MediaState.Manual;

            FiveElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\5.mp3");
            FiveElement.LoadedBehavior = MediaState.Manual;
            FiveElement.UnloadedBehavior = MediaState.Manual;

            SixElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\6.mp3");
            SixElement.LoadedBehavior = MediaState.Manual;
            SixElement.UnloadedBehavior = MediaState.Manual;

            SevenElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\7.mp3");
            SevenElement.LoadedBehavior = MediaState.Manual;
            SevenElement.UnloadedBehavior = MediaState.Manual;

            EightElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\8.mp3");
            EightElement.LoadedBehavior = MediaState.Manual;
            EightElement.UnloadedBehavior = MediaState.Manual;

            NineElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\9.mp3");
            NineElement.LoadedBehavior = MediaState.Manual;
            NineElement.UnloadedBehavior = MediaState.Manual;

            TenElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\10.mp3");
            TenElement.LoadedBehavior = MediaState.Manual;
            TenElement.UnloadedBehavior = MediaState.Manual;

            ElevenElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\11.mp3");
            ElevenElement.LoadedBehavior = MediaState.Manual;
            ElevenElement.UnloadedBehavior = MediaState.Manual;

            TwelveElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\12.mp3");
            TwelveElement.LoadedBehavior = MediaState.Manual;
            TwelveElement.UnloadedBehavior = MediaState.Manual;

            ThirteenElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\13.mp3");
            ThirteenElement.LoadedBehavior = MediaState.Manual;
            ThirteenElement.UnloadedBehavior = MediaState.Manual;

            FourteenElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\14.mp3");
            FourteenElement.LoadedBehavior = MediaState.Manual;
            FourteenElement.UnloadedBehavior = MediaState.Manual;

            FifteenElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\15.mp3");
            FifteenElement.LoadedBehavior = MediaState.Manual;
            FifteenElement.UnloadedBehavior = MediaState.Manual;

            SixteenElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\16.mp3");
            SixteenElement.LoadedBehavior = MediaState.Manual;
            SixteenElement.UnloadedBehavior = MediaState.Manual;

            SeventeenElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\17.mp3");
            SeventeenElement.LoadedBehavior = MediaState.Manual;
            SeventeenElement.UnloadedBehavior = MediaState.Manual;

            EighteenElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\18.mp3");
            EighteenElement.LoadedBehavior = MediaState.Manual;
            EighteenElement.UnloadedBehavior = MediaState.Manual;

            NineteenElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\19.mp3");
            NineteenElement.LoadedBehavior = MediaState.Manual;
            NineteenElement.UnloadedBehavior = MediaState.Manual;

            CMoneElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\CM1.mp3");
            CMoneElement.LoadedBehavior = MediaState.Manual;
            CMoneElement.UnloadedBehavior = MediaState.Manual;

            CMtwoElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\CM2.mp3");
            CMtwoElement.LoadedBehavior = MediaState.Manual;
            CMtwoElement.UnloadedBehavior = MediaState.Manual;

            CMthreeElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\Source\\Voice\\CM3.mp3");
            CMthreeElement.LoadedBehavior = MediaState.Manual;
            CMthreeElement.UnloadedBehavior = MediaState.Manual;

        }

        private void timer_Tick()
        {
            if (sec > 0)
            {
                LeftNegativeElement.Stop();
                LeftPositiveElement.Stop();
                RightNegativeElement.Stop();
                RightPositiveElement.Stop();
                if (sec == 75)
                {
                    GoodElement.Stop();
                    GoodElement.Play();
                }
                sec--;
                A.Value = sec;  //timerText是介面上的一個TextBlock
            }
            else
            {
                timer.Stop();
            }
        }


        //當Unit改變時，修改聲音路徑
        private void changeVoice()
        {
            LeftPositiveElement.Source = new Uri(Directory.GetCurrentDirectory() + LPV[action-1]);
            LeftPositiveElement.LoadedBehavior = MediaState.Manual;
            LeftPositiveElement.UnloadedBehavior = MediaState.Manual;

            LeftNegativeElement.Source = new Uri(Directory.GetCurrentDirectory() + LNV[action - 1]);
            LeftNegativeElement.LoadedBehavior = MediaState.Manual;
            LeftNegativeElement.UnloadedBehavior = MediaState.Manual;

            RightPositiveElement.Source = new Uri(Directory.GetCurrentDirectory() + RPV[action - 1]);
            RightPositiveElement.LoadedBehavior = MediaState.Manual;
            RightPositiveElement.UnloadedBehavior = MediaState.Manual;

            RightNegativeElement.Source = new Uri(Directory.GetCurrentDirectory() + RNV[action - 1]);
            RightNegativeElement.LoadedBehavior = MediaState.Manual;
            RightNegativeElement.UnloadedBehavior = MediaState.Manual;

            VideoElement.Source = new Uri(Directory.GetCurrentDirectory() + GetVideo[action -1]);
            VideoElement.LoadedBehavior = MediaState.Manual;
            VideoElement.UnloadedBehavior = MediaState.Manual;
        }

        //設定初始聲音
        private void setVoice()
        {
            LPV = sign.ReturnLeftPostiveVoice(unit);
            LNV = sign.ReturnLeftNegativeVoice(unit);
            RPV = sign.ReturnRightPostiveVoice(unit);
            RNV = sign.ReturnRightNegativeVoice(unit);

            LeftPositiveElement.Source = new Uri(Directory.GetCurrentDirectory() + LPV[0]);
            LeftPositiveElement.LoadedBehavior = MediaState.Manual;
            LeftPositiveElement.UnloadedBehavior = MediaState.Manual;

            LeftNegativeElement.Source = new Uri(Directory.GetCurrentDirectory() + LNV[0]);
            LeftNegativeElement.LoadedBehavior = MediaState.Manual;
            LeftNegativeElement.UnloadedBehavior = MediaState.Manual;

            RightPositiveElement.Source = new Uri(Directory.GetCurrentDirectory() + RPV[0]);
            RightPositiveElement.LoadedBehavior = MediaState.Manual;
            RightPositiveElement.UnloadedBehavior = MediaState.Manual;

            RightNegativeElement.Source = new Uri(Directory.GetCurrentDirectory() + RNV[0]);
            RightNegativeElement.LoadedBehavior = MediaState.Manual;
            RightNegativeElement.UnloadedBehavior = MediaState.Manual;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            sensor.SkeletonFrameReady += runtime_SkeletonFrameReady;
            sensor.ColorFrameReady += runtime_VideoFrameReady;

            //動作標題
            if(unit == 5)
            {
                UnitNumber.Content = "暖身運動";
            }
            else if(unit == 1)
            {
                UnitNumber.Content = "手部伸展訓練";
            }
            else if (unit == 2)
            {
                UnitNumber.Content = "手部伸展進階";
            }
            else if (unit == 3)
            {
                UnitNumber.Content = "下肢伸展訓練";
            }
            else if (unit == 4)
            {
                UnitNumber.Content = "下肢伸展進階";
            }

            synthesizer = new SpeechSynthesizer();//宣告一個新的文字語音合成
            Siri_Speech();//設定文字語音合成音量與速度
            this.speechRecognizer = this.CreateSpeechRecognizer();//初始化語音辨識，建立文法字句
            if (this.speechRecognizer != null && sensor != null)
            {
                // 在"SDK Release Notes"裡有提到:語音初始化，需要等待4秒才能就緒
                this.readyTimer = new DispatcherTimer();
                this.readyTimer.Tick += this.ReadyTimerTick;
                this.readyTimer.Interval = new TimeSpan(0, 0, 4);//等待4秒
                this.readyTimer.Start();
            }
        }

        private SpeechRecognitionEngine CreateSpeechRecognizer()
        {
            RecognizerInfo ri = GetKinectRecognizer();//取得 Kinect 語音識別
            if (ri == null)
            {
                MessageBox.Show(@"初始化語音識別有問題","無法載入語音識別",MessageBoxButton.OK,MessageBoxImage.Error);
                //this.Close();
                return null;
            }

            SpeechRecognitionEngine sre;//建立語音識別引擎
            try
            {
                sre = new SpeechRecognitionEngine(ri.Id);
            }
            catch
            {
                MessageBox.Show(@"初始化語音識別有問題", "無法載入語音識別", MessageBoxButton.OK, MessageBoxImage.Error);
                //this.Close();
                return null;
            }

            //========================================================
            //建立文法字句
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Culture = ri.Culture;
            gBuilder.Append(new Choices("stop", "play", "loud", "xiao"));
            gBuilder.Append(new Choices(" ", " ", " ", "sheng"));

            //===============================================
            // Create the actual Grammar instance, and then load it into the speech recognizer.
            var g = new Grammar(gBuilder);
            sre.LoadGrammar(g);//載入文法字句
            sre.SpeechRecognized += this.SreSpeechRecognized;//接受語音事件
            //sre.SpeechHypothesized += this.SreSpeechHypothesized;//推斷語音事件
            return sre;
        }

        //初始化語音辨識
        private static RecognizerInfo GetKinectRecognizer()
        {
            Func<RecognizerInfo, bool> matchingFunc = r =>
            {
                string value;
                r.AdditionalInfo.TryGetValue("Kinect", out value);
                return "True".Equals(value, StringComparison.InvariantCultureIgnoreCase) && "en-US".Equals(r.Culture.Name, StringComparison.InvariantCultureIgnoreCase);
            };
            return SpeechRecognitionEngine.InstalledRecognizers().Where(matchingFunc).FirstOrDefault();
        }

        //接受語音事件
        private void SreSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            /*
            if (e.Result.Confidence < 0.2)//肯定度低於0.6，判為錯誤語句
            {
                return;
            }*/
            switch (e.Result.Text.ToUpperInvariant())
            {
                case "STOP":
                    VideoElement.Pause();
                    break;
                case "PLAY":
                    VideoElement.Play();
                    break;
                case "LOUD":
                    VideoElement.Volume += 2;
                    break;
                case "XIAO SHENG":
                    VideoElement.Volume += -1;
                    break;
                case "TIMEOUT":
                    unit = 7;
                    MessageBox.Show("暫停");
                    break;
            }
        }

        //文字合成音 
        void Siri_Speech()
        {
            synthesizer.Volume = 100;//聲音大小(0 ~ 100)      
            synthesizer.Rate = -2;//聲音速度(-10 ~ 10)
        }

        private void ReadyTimerTick(object sender, EventArgs e)
        {
            this.Start();//讀取使用者語音
            this.readyTimer.Stop();
            this.readyTimer = null;
        }

        //初始化語音訊號
        private void Start()
        {
            var audioSource = sensor.AudioSource;
            audioSource.EchoCancellationMode = EchoCancellationMode.None; // No AEC for this sample
            audioSource.AutomaticGainControlEnabled = false; // Important to turn this off for speech recognition
            var kinectStream = audioSource.Start();//開啟Kinect語音串流           

            Stream s = kinectStream;
            this.speechRecognizer.SetInputToAudioStream(
                        s, new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
            this.speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);

        }
        
        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            VideoElement.Play();  // 控制影片播放
        }

        private void btn_pasue_Click(object sender, RoutedEventArgs e)
        {
            VideoElement.Pause(); // 控制影片暫停
        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            VideoElement.Stop(); // 控制影片停止
        }

        private void runtime_VideoFrameReady(object sender, ColorImageFrameReadyEventArgs e)
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
                videoImagex.Source = source;
            }
        }

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
                    SetEllipsePosition(ellipseHead,  head, false);
                    SetEllipsePosition(ellipseLeftHand,  leftHand, false);
                    SetEllipsePosition(ellipseRightHand,  rightHand, false);
                    SetEllipsePosition(ellipseLeftFoot,  leftFoot, false);
                    SetEllipsePosition(ellipseRightFoot,  rightFoot, false);
                    SetEllipsePosition(ellipseLeftElbow,  leftElbow, false);
                    SetEllipsePosition(ellipseRightElbow,  rightElbow, false);
                    SetEllipsePosition(ellipseLeftAnkle,  leftAnkle, false);
                    SetEllipsePosition(ellipseRightAnkle,  rightAnkle, false);
                    SetEllipsePosition(ellipseLeftWrist,  leftWrist, false);
                    SetEllipsePosition(ellipseRightWrist,  rightWrist, false);
                    SetEllipsePosition(ellipseLeftShoulder,  leftShoulder, false);
                    SetEllipsePosition(ellipseRightShoulder,  rightShoulder, false);
                    SetEllipsePosition(ellipseLeftKnee,  leftKnee, false);
                    SetEllipsePosition(ellipseRightKnee,  rightKnee, false);
                    SetEllipsePosition(ellipseCenterShoulder,  centerShoulder, false);
                    SetEllipsePosition(ellipseLeftHip,  leftHip, false);
                    SetEllipsePosition(ellipseRightHip,  rightHip, false);
                    SetEllipsePosition(ellipseCenterHip,  centerHip, false);
                    SetEllipsePosition(ellipseSpine,  spine, false);

                    //設定頁面標籤內容
                    TimesLabel.Content = times;
                    AllTimesLabel.Content = "5";

                    
                    //當完成次數為5，改變動作與聲音
                    if (times == 5)
                    {
                        action++;
                        if (action<=2)
                        {
                            changeVoice();
                        }
                        times = 0;
                    }

                    //判別單元
                    if (finish == 1)
                    {
                        if(AAction.handputdown(leftShoulder, leftHand, leftElbow, leftWrist, rightShoulder, rightHand, rightElbow, rightWrist))
                        {
                            finish = 0;
                        }
                    }
                    else if(finish == 2)
                    {
                        if(AAction.twoLeg(leftKnee,leftHip,rightKnee,rightHip))
                        {
                            finish = 0;
                        }
                    }
                    else if(finish == 0)
                    {
                        actionunit = unit;
                        actionname = action;
                        //判斷單元
                        if (unit == 1)
                        {
                            res = UAC.Unit1_Check(action, leftShoulder, rightShoulder, leftElbow, rightElbow);
                            second = unit_1.SecondSet();
                            failSecond = unit_1.failSecondSet();
                            loadingSet();
                            preTime = times;
                            times = unit_1.StartUnit1(action, times, centerShoulder, leftShoulder, rightShoulder, leftHand, rightHand, leftElbow, rightElbow, leftWrist, rightWrist);
                        }
                        if (unit == 2)
                        {
                            res = UAC.Unit2_Check(action, leftShoulder, rightShoulder, leftElbow, rightElbow);
                            second = unit_2.SecondSet();
                            failSecond = unit_2.failSecondSet();
                            loadingSet();
                            preTime = times;
                            times = unit_2.StartUnit2(action, times, centerShoulder, leftShoulder, rightShoulder, leftHand, rightHand, leftElbow, rightElbow, leftWrist, rightWrist, leftKnee, rightKnee);
                        }
                        if (unit == 3)
                        {
                            //a = UAC.Unit2_Check(action, leftShoulder, rightShoulder, leftElbow, rightElbow);
                            failSecond = unit_3.failSecondSet();
                            loadingSet();
                            preTime = times;
                            second = unit_3.SecondSet();
                            times = unit_3.StartUnit3(action, times, rightKnee, rightAnkle, rightHip, leftKnee, leftAnkle, leftHip);
                        }
                        if (unit == 4)
                        {
                            wrongnumber = unit_4.failSecondSet();
                            failSecond = unit_4.failSecondSet();
                            loadingSet();
                            preTime = times;
                            second = unit_4.SecondSet();
                            times = unit_4.StartUnit4(action, times, rightKnee, rightAnkle, rightFoot, leftKnee, leftAnkle, leftFoot);
                        }

                        if (unit == 5)
                        {
                            second = WU.SecondSet();
                            times = WU.StartUnit5(action, times, centerShoulder, leftShoulder, rightShoulder, leftHand, rightHand, leftElbow, rightElbow, leftWrist, rightWrist, leftKnee, rightKnee, head);
                            label.Content = "第" + times + "次";
                        }
                    }

                    //回傳次數為1000時表示已完成所有動作，重置所有變數、聲音並回歸Menu頁面
                    if (times == 1000)
                    {
                        times = 0;
                        unit = 0;
                        action = 0;
                        VideoElement.Stop();
                        GoodElement.Stop();
                        LeftNegativeElement.Stop();
                        LeftPositiveElement.Stop();
                        RightNegativeElement.Stop();
                        RightPositiveElement.Stop();
                        MessageBox.Show("恭喜您完成此單元訓練!");
                        this.NavigationService.Navigate(menu);
                    }
                    ResetTime();
                    FailMessage(res);
                    label.Content = "第" + times + "次";
                    actselect.Content = action;
                    if(unit != 0)
                    {
                        actioning.Content = sign.ReturnAct(unit, action);
                    }
                }
            }
        }
        
        //動作完成時,影片重複播放
        private void ResetTime()
        {
            if (preTime != times)
            {
                VideoElement.Stop();
                VideoElement.Play();
                sec = 100;
                preTime = times;
                A.Value = 100;
                failSecond = 1;
                if(unit == 1 || unit == 2)
                {
                    finish = 1;
                }
                else if(unit == 3 || unit ==4)
                {
                    finish = 2;
                }
                else
                {
                    finish = 0;
                }
            }
        }
        
        //判斷動作播放指導聲音
        private void FailMessage(float [] res)
        {
            int TensNum = 0,DigitsNum;
            if (failSecond % 150 == 0)
            {
                LeftPositiveElement.Stop();
                RightPositiveElement.Stop();
                LeftNegativeElement.Stop();
                RightNegativeElement.Stop();
                GoodElement.Stop();
                OneElement.Stop();
                TwoElement.Stop();
                ThreeElement.Stop();
                FourElement.Stop();
                FiveElement.Stop();
                SixElement.Stop();
                SevenElement.Stop();
                EightElement.Stop();
                NineElement.Stop();
                TenElement.Stop();
                ElevenElement.Stop();
                TwelveElement.Stop();
                ThirteenElement.Stop();
                FourteenElement.Stop();
                FifteenElement.Stop();
                SixteenElement.Stop();
                SeventeenElement.Stop();
                EighteenElement.Stop();
                NineteenElement.Stop();
                CMoneElement.Stop();
                CMtwoElement.Stop();
                CMthreeElement.Stop();


                Console.WriteLine(res[1]);
                if(res[1] < 0)
                {
                    res[1] = -res[1];
                }
                res[1] = Convert.ToInt32(res[1] * 83 / 0.6);
                TensNum = Convert.ToInt32( res[1] / 10);
                DigitsNum = Convert.ToInt32(res[1] % 10);
                totaldifferece += res[1];
                Console.WriteLine(TensNum+"/n"+DigitsNum);

                if (res[0] == 1)
                {
                    wrongnumber++;
                    LeftPositiveElement.Play();
                }
                else if(res[0] == 2)
                {
                    wrongnumber++;
                    LeftNegativeElement.Play();
                }
                else if (res[0] == 3)
                {
                    wrongnumber++;
                    RightPositiveElement.Play();
                }
                else if (res[0] == 4)
                {
                    wrongnumber++;
                    RightNegativeElement.Play();
                }

                //計算與標準動作差距
                if(TensNum == 1)
                {
                    TenElement.Play();
                }
                else if(TensNum == 2 )
                {
                    TwoElement.Play();
                }
                else if(TensNum ==3)
                {
                    ThreeElement.Play();
                }
                else if (TensNum == 4)
                {
                    FourElement.Play();
                }
                else if (TensNum == 5)
                {
                    FiveElement.Play();
                }
                else if (TensNum == 6)
                {
                    SixElement.Play();
                }
                else if (TensNum == 7)
                {
                    SevenElement.Play();
                }
                else if (TensNum == 8)
                {
                    EightElement.Play();
                }
                else if (TensNum == 9)
                {
                    NineElement.Play();
                }


                if (DigitsNum == 1)
                {
                    ElevenElement.Play();
                }
                else if (DigitsNum == 2)
                {
                    TwelveElement.Play();
                }
                else if (DigitsNum == 3)
                {
                    ThirteenElement.Play();
                }
                else if (DigitsNum == 4)
                {
                    FourteenElement.Play();
                }
                else if (DigitsNum == 5)
                {
                    FifteenElement.Play();
                }
                else if (DigitsNum == 6)
                {
                    SixteenElement.Play();
                }
                else if (DigitsNum == 7)
                {
                    SeventeenElement.Play();
                }
                else if (DigitsNum == 8)
                {
                    EighteenElement.Play();
                }
                else if (DigitsNum == 9)
                {
                    NineteenElement.Play();
                }


                if(TensNum == 0)
                {
                    CMoneElement.Play();
                }
                else if(TensNum >= 1)
                {
                    if(DigitsNum == 0)
                    {
                        CMtwoElement.Play();
                    }
                    else
                    {
                        CMthreeElement.Play();
                    }
                    
                }


                
            }
        }

        //傳值到網頁
        private void HttpGet()
        {
            /*
            string address = "http://www.google.com/s?"+"ActionUnit=" + actionunit + "&ActionName=" + actionname +  "&ActionTime=" + actiontime + 
            "&WrongNumber=" + wrongnumber + "&Diffierence="+ totaldifferece/wrongnumber;
            HttpWebRequest response = (HttpWebRequest)WebRequest.Create(address);
            response.Method = "GET";

            using (WebResponse wr = response.GetResponse())
            {
                //在這裡對接收到的頁面內容進行處理
                //Console.Write((int)response.StatusCode);
            }
            actionunit = 0;
            actionname = 0;
            actiontime = 0;
            wrongnumber = 0 ;
            differece = 0;
            totaldifferece = 0;
         */
        }

        //載入flag
        private void loadingSet() 
        {
            if (unit == 1)
            {
                flag = unit_1.HiddenSet(action, times, centerShoulder, leftShoulder, rightShoulder, leftHand, rightHand, leftElbow, rightElbow, leftWrist, rightWrist);
            }
            else if (unit == 2)
            {
                flag = unit_2.HiddenSet_U2(action, times, centerShoulder, leftShoulder, rightShoulder, leftHand, rightHand, leftElbow, rightElbow, leftWrist, rightWrist, leftKnee, rightKnee);
            }
            else if (unit == 3)
            {
                flag = unit_3.HiddenSet(action, times, rightKnee, rightAnkle, rightHip, leftKnee, leftAnkle, leftHip);
            }
            else if (unit == 4)
            {
                flag = unit_4.HiddenSet(action, times, rightKnee, rightAnkle, rightFoot, leftKnee, leftAnkle, leftFoot);

            }
            else if (unit ==5)
            {

            }

            if (flag)
            {
                timer_Tick();
                timer.Start();
            }
        }

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

        //返回按鍵,關閉聲音撥放計算
        private void back_Click(object sender, RoutedEventArgs e)
        {
            times = 0;
            second = 0;
            unit = 0;
            action = 0;
            res[0] = 0;
            res[1] = 0;
            finish = 0;
            VideoElement.Stop();
            GoodElement.Stop();
            LeftNegativeElement.Stop();
            LeftPositiveElement.Stop();
            RightNegativeElement.Stop();
            RightPositiveElement.Stop();
            BGMusicElement.Stop();
            this.NavigationService.Navigate(menu);
        }
    }
}
