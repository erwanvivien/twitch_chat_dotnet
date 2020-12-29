using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ChatBot
{
    class Winapi
    {
        public static IntPtr hwnd = IntPtr.Zero;

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd,
                  UInt32 Msg, int wParam, int lParam);

        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;

        const int VK_LEFT = 0x25;
        const int VK_UP = 0x26;
        const int VK_RIGHT = 0x27;
        const int VK_DOWN = 0x28;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        static public IntPtr set_window(string wName)
        {
            string name = wName.ToLower();
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle.ToLower().Contains(name))
                {
                    Console.WriteLine("WIN: This window was found: '" +
                            pList.MainWindowTitle + "'");
                    // Might need to do some findWindowEx
                    return pList.MainWindowHandle;
                }
            }

            return IntPtr.Zero;
        }

        public bool good(Dictionary<string, string> settings)
        {
            if (hwnd == IntPtr.Zero)
            {
                Console.WriteLine("WIN: No window with name '" + settings["window name"] + "' was found.");
                return false;
            }

            return true;
        }

        public Winapi(Dictionary<string, string> settings)
        {
            hwnd = set_window(settings["window name"]);
        }

        static public void act_upward()
        {
            Console.WriteLine("FCT: upward");
            PostMessage(hwnd, WM_KEYDOWN, VK_UP, 0);
            PostMessage(hwnd, WM_KEYUP, VK_UP, 0);

        }
        static public void act_downward()
        {
            Console.WriteLine("FCT: downward");
            PostMessage(hwnd, WM_KEYDOWN, VK_DOWN, 0);
            PostMessage(hwnd, WM_KEYUP, VK_DOWN, 0);

        }
        static public void act_left()
        {
            Console.WriteLine("FCT: left");
            PostMessage(hwnd, WM_KEYDOWN, VK_LEFT, 0);
            PostMessage(hwnd, WM_KEYUP, VK_LEFT, 0);

        }
        static public void act_right()
        {
            Console.WriteLine("FCT: right");
            PostMessage(hwnd, WM_KEYDOWN, VK_RIGHT, 0);
            PostMessage(hwnd, WM_KEYUP, VK_RIGHT, 0);

        }
        static public void act_a()
        {
            Console.WriteLine("FCT: a");
            // PostMessage(hwnd, WM_KEYDOWN, 'A', 0);
            PostMessage(hwnd, WM_KEYUP, 'A', 0);
        }
        static public void act_b()
        {
            Console.WriteLine("FCT: b");
            // PostMessage(hwnd, WM_KEYDOWN, 'B', 0);
            PostMessage(hwnd, WM_KEYUP, 'B', 0);
        }
        static public void act_x()
        {
            Console.WriteLine("FCT: x");
            // PostMessage(hwnd, WM_KEYDOWN, 'X', 0);
            PostMessage(hwnd, WM_KEYUP, 'X', 0);
        }
        static public void act_y()
        {
            Console.WriteLine("FCT: y");
            // PostMessage(hwnd, WM_KEYDOWN, 'Y', 0);
            PostMessage(hwnd, WM_KEYUP, 'Y', 0);
        }
        static public void act_l1()
        {
            Console.WriteLine("FCT: l1");
        }
        static public void act_l2()
        {
            Console.WriteLine("FCT: l2");
        }
        static public void act_r1()
        {
            Console.WriteLine("FCT: r1");
        }
        static public void act_r2()
        {
            Console.WriteLine("FCT: r2");
        }
        static public void act_help()
        {
            Console.WriteLine("FCT: help");
        }
        static public void act_nothing()
        {
            Console.WriteLine("FCT: nothing");
        }
    }
}
