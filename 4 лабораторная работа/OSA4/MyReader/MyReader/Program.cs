using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyReader
{
    class Program
    {
        public const int WM_COPYDATA = 0x4A;

        public static readonly string FILE_PATH = @"C:\Users\smerc\OneDrive\Рабочий стол\лабы ос\4 лабораторная работа\OSA4\myinfo.txt";
        public static readonly string TMP_FILE_PATH = @"C:\Users\smerc\OneDrive\Рабочий стол\лабы ос\4 лабораторная работа\OSA4\Test\";

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern string SendMessage(int hWnd, int msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        public struct COPYDATASTRUCT
        {
            public int cbData;
            public IntPtr dwData;
            public IntPtr lpData;
        }

        static void Main(string[] args)
        {
            if (args.Length > 1)//
            {
                return;
            }

            DoWork(int.Parse(args[0]));
        }

        public static void DoWork(int timer)
        {
            while(true)
            {
                SendString(GetLastString());
                Thread.Sleep(timer);
            }
        }

        public static string GetLastString()
        {
            while (File.Exists(TMP_FILE_PATH + "do_not_read")) 
            {
            }

            File.Create(TMP_FILE_PATH + "do_not_write").Close();

            string str = File.ReadLines(FILE_PATH).Last();

            File.Delete(TMP_FILE_PATH + "do_not_write");

            return str;
        }

        public static void SendString(string message)
        {
            IntPtr processHandle = FindWindow(null, "Form1");

            byte[] arr = Encoding.UTF8.GetBytes(message);
            IntPtr arrPointer = Marshal.AllocHGlobal(arr.Length);
            Marshal.Copy(arr, 0, arrPointer, arr.Length);

            COPYDATASTRUCT cds = new COPYDATASTRUCT();
            cds.dwData = (IntPtr)(2 * message.Length);
            cds.cbData = arr.Length;
            cds.lpData = arrPointer;

            try
            {
                SendMessage(processHandle.ToInt32(), WM_COPYDATA, IntPtr.Zero, ref cds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Marshal.FreeHGlobal(arrPointer);
            }
        }
    }
}
