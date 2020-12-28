using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChatBot
{
    class Winapi
    {
        public IntPtr hwnd = IntPtr.Zero;

        static public IntPtr set_window(string wName)
        {
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle.Contains(wName))
                {
                    return pList.MainWindowHandle;
                }
            }

            return IntPtr.Zero;
        }

        public Winapi(Dictionary<string, string> settings)
        {
            this.hwnd = set_window(settings["window name"]);
        }

        static public void act_upward()
        {
            Console.WriteLine("FCT: upward");
        }
        static public void act_downward()
        {
            Console.WriteLine("FCT: downward");
        }
        static public void act_left()
        {
            Console.WriteLine("FCT: left");
        }
        static public void act_right()
        {
            Console.WriteLine("FCT: right");
        }
        static public void act_a()
        {
            Console.WriteLine("FCT: a");
        }
        static public void act_b()
        {
            Console.WriteLine("FCT: b");
        }
        static public void act_x()
        {
            Console.WriteLine("FCT: x");
        }
        static public void act_y()
        {
            Console.WriteLine("FCT: y");
        }
        static public void act_l1()
        {
            Console.WriteLine("FCT: y");
        }
        static public void act_l2()
        {
            Console.WriteLine("FCT: y");
        }
        static public void act_r1()
        {
            Console.WriteLine("FCT: y");
        }
        static public void act_r2()
        {
            Console.WriteLine("FCT: y");
        }
        static public void act_help()
        {
            Console.WriteLine("FCT: y");
        }
        static public void act_nothing()
        {
            Console.WriteLine("FCT: nothing");
        }
    }
}
