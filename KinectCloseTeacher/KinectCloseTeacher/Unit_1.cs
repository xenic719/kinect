using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.Windows;
using Microsoft.Speech;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System.Speech.Synthesis;

namespace KinectCloseTeacher
{

    public class Unit_1
    {
        AllAction StartAction = new AllAction();
        int second = 0, keepTime= 100, failSecond = 1;
        bool CheckIsAction,isCompelete=false;
        public bool HiddenSet(int selectAction, int times, Joint centerShoulder, Joint leftShoulder, Joint rightShoulder, Joint leftHand, Joint rightHand, Joint leftElbow,
        Joint rightElbow, Joint leftWrist, Joint rightWrist)
        {
            if (StartAction.handflat(centerShoulder, leftShoulder, rightShoulder, leftHand, rightHand, leftElbow, rightElbow, leftWrist, rightWrist) && selectAction == 1)
            {
                return true;
            }
            else if (StartAction.fortwohand(leftShoulder, leftHand, leftElbow, leftWrist, rightShoulder, rightHand, rightElbow, rightWrist) && selectAction == 2)
            {
                return true;
            }
            return false;
        }
        public int SecondSet()
        {
            return second;
        }
        public int failSecondSet()
        {
            return failSecond;
        }
        public int StartUnit1(int selectAction, int times, Joint centerShoulder, Joint leftShoulder, Joint rightShoulder, Joint leftHand, Joint rightHand, Joint leftElbow,
        Joint rightElbow, Joint leftWrist, Joint rightWrist)
        {
            if (selectAction == 1)
            {
                if (times < 10)
                {
                    if (second < keepTime)
                    {
                        CheckIsAction = StartAction.handflat(centerShoulder, leftShoulder, rightShoulder, leftHand, rightHand, leftElbow, rightElbow, leftWrist, rightWrist);
                        if (CheckIsAction && !isCompelete)
                        {
                            second++;
                        }
                        else
                        {
                            failSecond++;
                        }
                    }
                    else
                    {
                        second = 0;
                        times++;
                    }
                }
                return times;
            }
            else if (selectAction == 2)
            {
                if (times < 10)
                {
                    if (second < keepTime)
                    {
                        CheckIsAction = StartAction.fortwohand(leftShoulder, leftHand, leftElbow, leftWrist, rightShoulder, rightHand, rightElbow, rightWrist);
                        if (CheckIsAction && !isCompelete)
                        {
                            second++;
                        }
                        else
                        {
                            failSecond++;
                        }
                    }
                    else
                    {
                        second = 0;
                        times++;
                    }
                }
                return times;
            }
            else
            {
                return 1000;
            }
        }
    }
}
