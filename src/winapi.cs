using System;
using System.Collections.Generic;
using System.Linq;
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
                  UInt32 Msg, Int64 wParam, Int64 lParam);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        const int WM_CHAR = 0x102;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;

        const int VK_LEFT = 0x25;
        const int VK_UP = 0x26;
        const int VK_RIGHT = 0x27;
        const int VK_DOWN = 0x28;

        const int VK_F13 = 0x7C;
        const int VK_F14 = 0x7D;
        const int VK_F15 = 0x7E;
        const int VK_F16 = 0x7F;
        const int VK_F17 = 0x80;
        const int VK_F18 = 0x81;
        const int VK_F19 = 0x82;
        const int VK_F20 = 0x83;
        const int VK_F21 = 0x84;
        const int VK_F22 = 0x85;
        const int VK_F23 = 0x86;
        const int VK_F24 = 0x87;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);
        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            //  You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }

        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                EnumWindowsProc childProc = new EnumWindowsProc(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        [DllImport("kernel32.dll")]
        static extern int GetProcessId(IntPtr handle);

        static public IntPtr set_window(string wName)
        {
            string name = wName.ToLower();
            foreach (Process pList in Process.GetProcesses())
            {
                if (!pList.MainWindowTitle.ToLower().Contains(name))
                    continue;

                Console.WriteLine("WIN: This window was found: '" +
                        pList.MainWindowTitle + "' with hwnd : " + pList.MainWindowHandle);
                Console.WriteLine("===============================================\n");
                return pList.MainWindowHandle;
            }

            return IntPtr.Zero;
        }

        public bool good(Dictionary<string, string> settings)
        {
            if (hwnd != IntPtr.Zero)
                return true;

            Console.WriteLine("WIN: No window with name '" + settings["window name"] + "' was found.");
            return false;
        }

        public Winapi(Dictionary<string, string> settings)
        {
            hwnd = set_window(settings["window name"]);
        }

        private static void loop_child(IntPtr current, int keycode, bool down = false, int level = 0)
        {
            /// might not be required
            List<IntPtr> children = GetChildWindows(current);

            // This ↓ is debugging
            Console.WriteLine(new string('\t', level) + current);

            // foreach (var child in children)
            //     loop_child(child, keycode, down, level + 1);

            if (down)
                PostMessage(current, WM_KEYDOWN, keycode, 0x00000001);
            PostMessage(current, WM_KEYUP, keycode, 0xC0000001);

            // Raw inputs
            // if (down)
            //     keybd_event((byte)keycode, 0, 0x00, 0);
            // keybd_event((byte)keycode, 0, 0x02, 0);
        }

        static public void send_up_down(int keycode)
        {
            loop_child(hwnd, keycode, true);
        }
        static public void send_up(int keycode)
        {
            loop_child(hwnd, keycode);
        }
        static public void act_upward()
        {
            Console.WriteLine("FCT: upward");
            send_up_down(VK_UP);
        }
        static public void act_downward()
        {
            Console.WriteLine("FCT: downward");
            send_up_down(VK_DOWN);
        }
        static public void act_left()
        {
            Console.WriteLine("FCT: left");
            send_up_down(VK_LEFT);
        }
        static public void act_right()
        {
            Console.WriteLine("FCT: right");
            send_up_down(VK_RIGHT);
        }
        static public void act_a()
        {
            Console.WriteLine("FCT: a");
            send_up('A');
        }
        static public void act_b()
        {
            Console.WriteLine("FCT: b");
            send_up('B');
        }
        static public void act_x()
        {
            Console.WriteLine("FCT: x");
            send_up('X');
        }
        static public void act_y()
        {
            Console.WriteLine("FCT: y");
            send_up('Y');
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
