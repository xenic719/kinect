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
        public int Unit1_Check(int selectAction, Joint leftShoulder, Joint rightShoulder, Joint leftElbow, Joint rightElbow)
        {
            if(selectAction ==1)
            {
                if(leftElbow.Position.Y + 0.05 < leftShoulder.Position.Y)
                {
                    Console.WriteLine("左手在高一點");
                    return 1;
                }
                if(rightElbow.Position.Y + 0.05 < rightShoulder.Position.Y)
                {
                    Console.WriteLine("右手在高一點");
                    return 3;
                }
                    
                if(leftElbow.Position.Y -0.05 > leftShoulder.Position.Y)
                {
                    Console.WriteLine("左手在低一點");
                    return 2;
                }
                if(rightElbow.Position.Y - 0.05 > rightShoulder.Position.Y)
                {
                    Console.WriteLine("右手在低一點");
                    return 4;
                }
            }
            else if (selectAction == 2)
            {
                if (leftElbow.Position.Z + 0.1 < leftShoulder.Position.Z)
                {
                    return 1;
                }
                if (rightElbow.Position.Z + 0.1 < rightShoulder.Position.Z)
                {
                    return 3;
                }

                if (leftElbow.Position.Z - 0.15 > leftShoulder.Position.Z)
                {
                    return 2;
                }
                if (rightElbow.Position.Z - 0.15 > rightShoulder.Position.Z)
                {
                    return 4;
                }
            }            
            return 5;
        }


        public int Unit2_Check(int selectAction, Joint leftShoulder, Joint rightShoulder, Joint leftElbow, Joint rightElbow)
        {
            if (selectAction == 1)
            {
                if (leftElbow.Position.Z < leftShoulder.Position.Z)
                {
                    return 1;
                }
                else if (leftElbow.Position.Z > leftShoulder.Position.Z)
                {
                    return 2;
                }
            }
            else if (selectAction == 2)
            {
                if (rightElbow.Position.Z < rightShoulder.Position.Z)
                {
                    return 3;
                }
                else if (rightElbow.Position.Z > rightShoulder.Position.Z)
                {
                    return 4;
                }
            }
            return 3;
        }
    }
}
