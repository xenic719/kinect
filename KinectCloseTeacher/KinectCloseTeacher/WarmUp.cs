using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectCloseTeacher
{
    public class WarmUp
    {
        MainWindow trr = new MainWindow();
        AllAction StartAction = new AllAction();
        int second = 0, keepTime = 100, failSecond = 1;


        public int SecondSet()
        {
            return second;
        }
        public int failSecondSet()
        {
            return failSecond;
        }

        public int StartUnit5(int selectAction, int times, Joint centerShoulder, Joint leftShoulder, Joint rightShoulder, Joint leftHand, Joint rightHand, Joint leftElbow,
        Joint rightElbow, Joint leftWrist, Joint rightWrist, Joint leftKnee, Joint rightKnee, Joint head)
        {
            //第一個動作

            if (selectAction == 1)
            {
                if (times < 10)
                {
                    if (StartAction.CheckRunPosture(leftKnee, rightKnee))
                    {
                        times++;
                    }
                }
                return times;
            }
            else if (selectAction == 2)
            {
                if (times < 10)
                {
                    if (StartAction.ForwardRaiseHand(leftShoulder, rightShoulder, leftElbow, rightElbow, leftHand, rightHand, leftWrist, rightWrist))
                    {
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
