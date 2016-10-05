using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectCloseTeacher
{
    public class Unit_2
    {
        AllAction StartAction = new AllAction();
        int second = 0, keepTime = 100, failSecond = 1;
        bool CheckIsAction, isCompelete = false;

        public bool HiddenSet_U2(int selectAction, int times, Joint centerShoulder, Joint leftShoulder, Joint rightShoulder, Joint leftHand, Joint rightHand, Joint leftElbow,
        Joint rightElbow, Joint leftWrist, Joint rightWrist, Joint leftKnee, Joint rightKnee)
        {
            if (StartAction.forlefthand(leftShoulder, leftHand, leftElbow, leftWrist, rightShoulder, rightHand, rightElbow, rightWrist) && selectAction == 1)
            {
                return true;
            }
            else if (StartAction.forrighthand(rightShoulder, rightHand, rightElbow, rightWrist, rightShoulder, rightHand, rightElbow, rightWrist) && selectAction == 2)
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

        public int StartUnit2(int selectAction, int times, Joint centerShoulder, Joint leftShoulder, Joint rightShoulder, Joint leftHand, Joint rightHand, Joint leftElbow,
        Joint rightElbow, Joint leftWrist, Joint rightWrist, Joint leftKnee, Joint rightKnee)
        {
            if (selectAction == 1)
            {
                if (times < 10)
                {
                    if (second < keepTime)
                    {
                        CheckIsAction = StartAction.forlefthand(leftShoulder, leftHand, leftElbow, leftWrist, rightShoulder, rightHand, rightElbow, rightWrist);
                        if (CheckIsAction && !isCompelete)
                        {
                            second++;
                        }
                        else
                        {
                            failSecond++;
                            Console.WriteLine(failSecond);
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
                        CheckIsAction = StartAction.forrighthand(rightShoulder, rightHand, rightElbow, rightWrist, rightShoulder, rightHand, rightElbow, rightWrist);
                        if (CheckIsAction && !isCompelete)
                        {
                            second++;
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
