using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.Windows;

namespace KinectCloseTeacher
{
    public class Unit_Action_Check
    {
        //判別Unit1的動作
        public float[]  Unit1_Check(int selectAction, Joint leftShoulder, Joint rightShoulder, Joint leftElbow, Joint rightElbow)
        {
            float [] res = new float[2];
            if(selectAction ==1)
            {
                if(leftElbow.Position.Y + 0.05 < leftShoulder.Position.Y)
                {
                    res[0] = 1;
                    res[1] = leftShoulder.Position.Y - leftElbow.Position.Y;
                   // Console.WriteLine("左手在高一點");
                    return res;
                }
                if(rightElbow.Position.Y + 0.05 < rightShoulder.Position.Y)
                {
                    res[0] = 3;
                    res[1] = rightShoulder.Position.Y - rightElbow.Position.Y;
                   // Console.WriteLine("右手在高一點");
                    return res;
                }
                    
                if(leftElbow.Position.Y -0.05 > leftShoulder.Position.Y)
                {
                    res[0] = 2;
                    res[1] = leftElbow.Position.Y - leftShoulder.Position.Y;
                   // Console.WriteLine("左手在低一點");
                    return res;
                }
                if(rightElbow.Position.Y - 0.05 > rightShoulder.Position.Y)
                {
                    res[0] = 4;
                    res[1] =rightShoulder.Position.Y - rightElbow.Position.Y;
                    //Console.WriteLine("右手在低一點");
                    return res;
                }
            }
            else if (selectAction == 2)
            {
                if (leftElbow.Position.Z + 0.05 < leftShoulder.Position.Z)
                {
                    res[0] = 1;
                    res[1] = leftShoulder.Position.Z - leftElbow.Position.Z;
                    return res;
                }
                if (rightElbow.Position.Z + 0.05 < rightShoulder.Position.Z)
                {
                    res[0] = 3;
                    res[1] = rightShoulder.Position.Z - rightElbow.Position.Z;
                    return res;
                }

                if (leftElbow.Position.Z - 0.05 > leftShoulder.Position.Z)
                {
                    res[0] = 2;
                    res[1] = leftElbow.Position.Z - leftShoulder.Position.Z;
                    return res;
                }
                if (rightElbow.Position.Z - 0.05 > rightShoulder.Position.Z)
                {
                    res[0] = 4;
                    res[1] = rightElbow.Position.Z - rightShoulder.Position.Z;
                    return res;
                }
            }
            res[0] = 5;
            res[1] = 0; 
            return res;
        }


        public float[] Unit2_Check(int selectAction, Joint leftShoulder, Joint rightShoulder, Joint leftElbow, Joint rightElbow)
        {
            float[] res = new float[2];
            if (selectAction == 1)
            {
                if (leftElbow.Position.Z + 0.05 < leftShoulder.Position.Z)
                {
                    res[0] = 1;
                    res[1] = leftShoulder.Position.Z - leftElbow.Position.Z;
                    return res;
                }
                else if (leftElbow.Position.Z - 0.05 > leftShoulder.Position.Z)
                {
                    res[0] = 2;
                    res[1] = leftElbow.Position.Z - leftShoulder.Position.Z;
                    return res;
                }
            }
            else if (selectAction == 2)
            {
                if (rightElbow.Position.Z + 0.05 < rightShoulder.Position.Z)
                {
                    res[0] = 3;
                    res[1] = rightShoulder.Position.Z - rightElbow.Position.Z;
                    return res;
                }
                else if (rightElbow.Position.Z - 0.05 > rightShoulder.Position.Z)
                {
                    res[0] = 4;
                    res[1] = leftElbow.Position.Z - leftShoulder.Position.Z;
                    return res;
                }
            }

            res[0] = 5;
            res[1] = 0;
            return res;
        }
    }
}
