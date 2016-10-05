
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.Windows;

namespace KinectCloseTeacher
{
    internal class AllAction
    {
        bool runflag = false;
        bool raiseflag = false;

        //雙腳平放
        public bool twoLeg(Joint leftKnee, Joint leftHip, Joint rightKnee, Joint rightHip)
        {
            if((leftHip.Position.Y - 0.3 >leftKnee.Position.Y)&&(rightHip.Position.Y - 0.3 > rightKnee.Position.Y))
            {
                return true;
            }
            return false;
        }

        //放下手臂
        public bool handputdown(Joint leftShoulder, Joint leftHand, Joint leftElbow, Joint leftWrist, Joint rightShoulder, Joint rightHand, Joint rightElbow, Joint rightWrist)
        {
            if ((leftElbow.Position.Y < leftShoulder.Position.Y ) && (leftWrist.Position.Y < leftElbow.Position.Y)&&
                (leftHand.Position.Y<leftWrist.Position.Y) && (rightElbow.Position.Y < rightShoulder.Position.Y) &&
                (rightWrist.Position.Y < rightElbow.Position.Y) && (rightHand.Position.Y < rightWrist.Position.Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判定雙手水平舉起
        public bool handflat(Joint centerShoulder, Joint leftShoulder, Joint rightShoulder, Joint leftHand, Joint rightHand, Joint leftElbow,
            Joint rightElbow, Joint leftWrist, Joint rightWrist)
        {
            if (
                ((rightShoulder.Position.Y + 0.1) >= leftShoulder.Position.Y) && ((rightShoulder.Position.Y - 0.15) <= leftShoulder.Position.Y) &&
                ((leftElbow.Position.Y + 0.1) >= leftShoulder.Position.Y) && ((leftElbow.Position.Y - 0.15) <= leftShoulder.Position.Y) &&
                ((rightElbow.Position.Y + 0.1) >= leftShoulder.Position.Y) && ((rightElbow.Position.Y - 0.15) <= leftShoulder.Position.Y) &&
                ((leftHand.Position.Y + 0.1) >= leftShoulder.Position.Y) && ((leftHand.Position.Y - 0.15) <= leftShoulder.Position.Y) &&
                ((rightHand.Position.Y + 0.1) >= leftShoulder.Position.Y) && ((rightHand.Position.Y - 0.15) <= leftShoulder.Position.Y) &&
                ((leftWrist.Position.Y + 0.1) >= leftShoulder.Position.Y) && ((leftWrist.Position.Y - 0.15) <= leftShoulder.Position.Y) &&
                ((rightWrist.Position.Y + 0.1) >= leftShoulder.Position.Y) && ((rightWrist.Position.Y - 0.15) <= leftShoulder.Position.Y) &&
                ((rightHand.Position.X > rightShoulder.Position.X) && (leftHand.Position.X < leftShoulder.Position.X)) &&
                ((rightHand.Position.Z + 0.15) >= rightShoulder.Position.Z) && ((rightHand.Position.Z - 0.15) <= rightShoulder.Position.Z) &&
                ((leftHand.Position.Z + 0.15) >= leftShoulder.Position.Z) && ((leftHand.Position.Z - 0.15) <= leftShoulder.Position.Z))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判定雙手向上舉直
        public bool fortwohand(Joint leftShoulder, Joint leftHand, Joint leftElbow, Joint leftWrist, Joint rightShoulder, Joint rightHand, Joint rightElbow, Joint rightWrist)
        {
            if ((((leftShoulder.Position.X + 0.1) >= leftShoulder.Position.X) && ((leftShoulder.Position.X - 0.15) <= leftShoulder.Position.X) &&
               ((leftElbow.Position.X + 0.1) >= leftShoulder.Position.X) && ((leftElbow.Position.X - 0.15) <= leftShoulder.Position.X) &&
               ((leftHand.Position.X + 0.1) >= leftShoulder.Position.X) && ((leftHand.Position.X - 0.15) <= leftShoulder.Position.X) &&
               ((leftWrist.Position.X + 0.1) >= leftShoulder.Position.X) && ((leftWrist.Position.X - 0.15) <= leftShoulder.Position.X) &&
               (leftElbow.Position.Y > leftShoulder.Position.Y) && (leftElbow.Position.Z + 0.1 >= leftShoulder.Position.Z) &&
               (leftElbow.Position.Z - 0.1) <= leftShoulder.Position.Z) && (((rightShoulder.Position.X + 0.1) >= rightShoulder.Position.X) &&
               ((rightShoulder.Position.X - 0.15) <= rightShoulder.Position.X) &&
               ((rightElbow.Position.X + 0.1) >= rightShoulder.Position.X) && ((rightElbow.Position.X - 0.15) <= rightShoulder.Position.X) &&
               ((rightHand.Position.X + 0.1) >= rightShoulder.Position.X) && ((rightHand.Position.X - 0.15) <= rightShoulder.Position.X) &&
               ((rightWrist.Position.X + 0.1) >= rightShoulder.Position.X) && ((rightWrist.Position.X - 0.15) <= rightShoulder.Position.X) &&
               (rightElbow.Position.Y > rightShoulder.Position.Y) &&
               (rightElbow.Position.Z + 0.1 >= rightShoulder.Position.Z) && (rightElbow.Position.Z - 0.1) <= rightShoulder.Position.Z))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //向上舉起右手
        public bool forrighthand(Joint leftShoulder, Joint leftHand, Joint leftElbow, Joint leftWrist, Joint rightShoulder, Joint rightHand, Joint rightElbow, Joint rightWrist)
        {
            if (((rightShoulder.Position.X + 0.1) >= rightShoulder.Position.X) && ((rightShoulder.Position.X - 0.15) <= rightShoulder.Position.X) &&
                ((rightElbow.Position.X + 0.1) >= rightShoulder.Position.X) && ((rightElbow.Position.X - 0.15) <= rightShoulder.Position.X) &&
                ((rightHand.Position.X + 0.1) >= rightShoulder.Position.X) && ((rightHand.Position.X - 0.15) <= rightShoulder.Position.X) &&
                ((rightWrist.Position.X + 0.1) >= rightShoulder.Position.X) && ((rightWrist.Position.X - 0.15) <= rightShoulder.Position.X) &&
                (rightElbow.Position.Y > rightShoulder.Position.Y) &&
                (rightElbow.Position.Z + 0.1 >= rightShoulder.Position.Z) && (rightElbow.Position.Z - 0.1) <= rightShoulder.Position.Z)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //向上舉直左手
        public bool forlefthand(Joint leftShoulder, Joint leftHand, Joint leftElbow, Joint leftWrist, Joint rightShoulder, Joint rightHand, Joint rightElbow, Joint rightWrist)
        {
            if (((leftShoulder.Position.X + 0.1) >= leftShoulder.Position.X) && ((leftShoulder.Position.X - 0.15) <= leftShoulder.Position.X) &&
               ((leftElbow.Position.X + 0.1) >= leftShoulder.Position.X) && ((leftElbow.Position.X - 0.15) <= leftShoulder.Position.X) &&
               ((leftHand.Position.X + 0.1) >= leftShoulder.Position.X) && ((leftHand.Position.X - 0.15) <= leftShoulder.Position.X) &&
               ((leftWrist.Position.X + 0.1) >= leftShoulder.Position.X) && ((leftWrist.Position.X - 0.15) <= leftShoulder.Position.X) &&
               (leftElbow.Position.Y > leftShoulder.Position.Y) &&
               (leftElbow.Position.Z + 0.1 >= leftShoulder.Position.Z) && (leftElbow.Position.Z - 0.1) <= leftShoulder.Position.Z)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判定右膝是否高過腰
        public bool raiserightknee(Joint rightHip, Joint rightKnee)
        {
            if ((rightKnee.Position.Y + 0.1) >= rightHip.Position.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判定左膝是否高過腰
        public bool raiseleftknee(Joint leftHip, Joint leftKnee)
        {
            if ((leftKnee.Position.Y + 0.1) >= leftHip.Position.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //原地踏步
        public bool CheckRunPosture(Joint leftKnee, Joint rightKnee)
        {
            if (leftKnee.Position.Z - rightKnee.Position.Z > 0.2)
            {
                runflag = true;
            }
            if ((rightKnee.Position.Z - leftKnee.Position.Z > 0.2) && runflag)
            {
                runflag = false;
                return true;
            }
            return false;
        }

        //向前平舉手臂
        public bool ForwardRaiseHand(Joint leftShoulder, Joint rightShoulder, Joint leftElbow, Joint rightElbow, Joint leftHand, Joint rightHand, Joint leftWrist, Joint rightWrist)
        {
            if (((leftHand.Position.Y + 0.1) >= leftShoulder.Position.Y) && ((leftHand.Position.Y - 0.15) <= leftShoulder.Position.Y) &&
               ((leftWrist.Position.Y + 0.1) >= leftShoulder.Position.Y) && ((leftWrist.Position.Y - 0.15) <= leftShoulder.Position.Y)&&
               ((rightHand.Position.Y + 0.1) >= rightShoulder.Position.Y) && ((rightHand.Position.Y - 0.15) <= rightShoulder.Position.Y) &&
               ((rightWrist.Position.Y + 0.1) >= rightShoulder.Position.Y) && ((rightWrist.Position.Y - 0.15) <= rightShoulder.Position.Y))
            {
                raiseflag = true;
            }
            if (((leftHand.Position.Y ) <= leftElbow.Position.Y )&& ((rightHand.Position.Y) <= rightElbow.Position.Y) && raiseflag)
            {
                raiseflag = false;
                return true;
            }
            return false;
        }

        //右腿後拉
        public bool RightLeg (Joint rightKnee, Joint rightAnkle, Joint rightFoot)
        {
            if(rightAnkle.Position.Y + 0.1 > rightKnee.Position.Y)
            {
                return true;
            }
            return false;
        }

        //左腿後拉
        public bool LeftLeg(Joint leftKnee, Joint leftAnkle, Joint lefttFoot)
        {
            if (leftAnkle.Position.Y + 0.1 > leftKnee.Position.Y)
            {
                return true;
            }
            return false;
        }
    }
}
