using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Utilities;

namespace CapturePrtSc
{
    public partial class Form1 : Form
    {
        globalKeyboardHook gkh = new globalKeyboardHook();
        /*
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        */

        public Form1()
        {
            InitializeComponent();
            //gkh.HookedKeys.Add(Keys.A);
            //gkh.HookedKeys.Add(Keys.B);
            gkh.HookedKeys.Add(Keys.PrintScreen);
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
        }

        void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PrintScreen)
                btnCapture_Click(sender, e);
            //lstLog.Items.Add("Up\t" + e.KeyCode.ToString());
            e.Handled = true;
        }

        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            //lstLog.Items.Add("Down\t" + e.KeyCode.ToString());
            e.Handled = true;
        }


        private void btnCapture_Click(object sender, EventArgs e)
        {
            //_hookID = SetHook(_proc);
            //UnhookWindowsHookEx(_hookID);

            Screenshot shot = new Screenshot();
            Bitmap bmpScreenshot = shot.Capturing();
            string imgName = DateTime.Now.Ticks.ToString() + ".jpg";
            
            // Save the screenshot to the specified path of the project
            bmpScreenshot.Save(Environment.CurrentDirectory + "\\" + imgName, ImageFormat.Png);
        }

        /*
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Console.WriteLine((Keys)vkCode);
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        */
    }
}
