using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectCloseTeacher
{
    public class Unit_4
    {
        MainWindow trr = new MainWindow();
        AllAction StartAction = new AllAction();
        int second = 0, keepTime = 100, failSecond = 1;
        bool CheckIsAction, isCompelete = false;

        public bool HiddenSet(int selectAction, int times, Joint rightKnee, Joint rightAnkle, Joint rightFoot, Joint leftKnee, Joint leftAnkle, Joint leftFoot)
        {
            if (StartAction.RightLeg(rightKnee, rightAnkle, rightFoot) && selectAction == 1)
            {
                return true;
            }
            else if (StartAction.LeftLeg(rightKnee, rightAnkle, rightFoot) && selectAction == 2)
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

        public int StartUnit4(int selectAction, int times, Joint rightKnee, Joint rightAnkle, Joint rightFoot,
            Joint leftKnee, Joint leftAnkle, Joint leftFoot)
        {
            //第一個動作
            if (selectAction == 1)
            {
                if (times < 10)
                {
                    if (second < keepTime)
                    {
                        CheckIsAction = StartAction.RightLeg(rightKnee, rightAnkle, rightFoot);
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
                        CheckIsAction = StartAction.LeftLeg(leftKnee, leftAnkle, leftFoot);
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
