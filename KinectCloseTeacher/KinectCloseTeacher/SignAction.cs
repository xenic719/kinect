using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectCloseTeacher
{
    internal class SignAction
    {

        string[,] sign = new string[5, 3] { { "請水平舉起雙手", "請向上舉直雙手", "動作完成" },
                                            { "請向上舉直左手", "請向上舉直右手", "動作完成" },
                                            { "請抬起左膝", "請抬起右膝", "動作完成" } ,
                                            { "請", "請" , "動作完成"},
                                            { "請原地踏步", "請向前舉起手臂", "動作完成"}};
        string[,] video = new string[5, 2] { { "\\Source\\Video\\TwoHandFlat.mp4", "\\Source\\Video\\TwoHandHight.mp4" }
                                           , { "\\Source\\Video\\LeftHand.mp4", "\\Source\\Video\\RightHand.mp4" }
                                           , { "\\Source\\Video\\Step.mp4", "\\Source\\Video\\TwoHandFront.mp4" }
                                           , { "\\Source\\Video\\Step.mp4", "\\Source\\Video\\TwoHandFront.mp4" }
                                           , { "\\Source\\Video\\Step.mp4", "\\Source\\Video\\TwoHandFront.mp4" }};

        //左正
        string[,] LeftPostiveVoice = new string[5, 2] { { "\\Source\\Voice\\LeftHeight.mp3", "\\Source\\Voice\\HandLeftStr.mp3" }
                                           , { "\\Source\\Voice\\HandLeftStr.mp3", "" }
                                           , { "\\Source\\Voice\\LegLeftHeight.mp3", "" }
                                           , { "", "" }
                                           , { "", "\\Source\\Voice\\在高一點.m4a" }};
        //左負
        string[,] LeftNegativeVoice = new string[5, 2] { { "\\Source\\Voice\\LeftLow.mp3" , "\\Source\\Voice\\HandLeftFor.mp3" }
                                              , { "\\Source\\Voice\\HandLeftFor.mp3" , "" }
                                              , { "\\Source\\Voice\\LegLeftLow.mp3" , "" }
                                              , { "" , "" }
                                              , { "" , "\\Source\\Voice\\往前一點.m4a" }};

        string[,] RightPostiveVoice = new string[5, 2] { { "\\Source\\Voice\\RightHeight.mp3", "\\Source\\Voice\\HandRightStr.mp3" }
                                           , { "", "\\Source\\Voice\\HandRightStr.mp3" }
                                           , { "", "\\Source\\Voice\\LegRightHeight.mp3" }
                                           , { "", "" }
                                           , { "", "\\Source\\Voice\\在高一點.m4a" }};

        string[,] RightNegativeVoice = new string[5, 2] { { "\\Source\\Voice\\RightLow.mp3" , "\\Source\\Voice\\HandRightFor.mp3" }
                                              , { "" , "\\Source\\Voice\\HandRightFor.mp3" }
                                              , { "" , "\\Source\\Voice\\LegRightLow.mp3" }
                                              , { "" , "" }
                                              , { "" , "\\Source\\Voice\\往前一點.m4a" }};

        string[] VideoArr = new string[2];
        string[] LPV = new string[2];
        string[] RPV = new string[2];
        string[] LNV = new string[2];
        string[] RNV = new string[2];


        //取得動作指令
        public string ReturnAct(int selectUnit, int selectAct)
        {
            return sign[selectUnit - 1, selectAct - 1];
        }

        //取得影片路徑
        public string [] ReturnVideo(int selectUnit)
        {
            for (int j = 0; j < video.GetLength(1); j++)
            {
                VideoArr[j] = video[selectUnit - 1, j];
            }
            return VideoArr;
        }

        //取得聲音路徑
        public string [] ReturnLeftNegativeVoice(int selectUnit)
        {
            for (int j = 0; j < LeftNegativeVoice.GetLength(1); j++)
            {
                LNV[j] = LeftNegativeVoice[selectUnit-1, j];
            }
        return LNV;
        }

        public string[] ReturnRightNegativeVoice(int selectUnit)
        {
            for (int j = 0; j < RightNegativeVoice.GetLength(1); j++)
            {
                RNV[j] = RightNegativeVoice[selectUnit - 1, j];
            }
            return RNV;
        }

        public string[] ReturnLeftPostiveVoice(int selectUnit)
        {
            for (int j = 0; j < LeftPostiveVoice.GetLength(1); j++)
            {
                LPV[j] = LeftPostiveVoice[selectUnit - 1, j];
            }
            return LPV;
        }

        //取得反向聲音路徑
        public string[] ReturnRightPostiveVoice(int selectUnit)
        {
            for (int j = 0; j < RightPostiveVoice.GetLength(1); j++)
            {
                RPV[j] = RightPostiveVoice[selectUnit - 1, j];
            }
            return RPV;
        }
    }
}
