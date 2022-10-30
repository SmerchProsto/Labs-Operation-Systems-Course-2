using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Management;
using System.IO;

namespace MyWriter
{
    class Program
    {
        public const int WM_COPYDATA = 0x4A;
        public static readonly string NO_INFO = "NO INFO";
        public static readonly string ERROR = "ERROR";
        public static readonly string FILE_PATH = @"C:\Users\smerc\OneDrive\Рабочий стол\лабы ос\4 лабораторная работа\OSA4\myinfo.txt";

        public static readonly string TMP_FILE_PATH = @"C:\Users\smerc\OneDrive\Рабочий стол\лабы ос\4 лабораторная работа\OSA4\Test\";
        static Object obj = new object();
        static void Main(string[] args)
        {
            if (args.Length < 4 || args.Length > 4)
            {
                return;
            }
            else
            {
                string deviceName = args[0];
                bool deleteNoInfoLines = bool.Parse(args[1]);
                //int updateTime = int.Parse(args[2]);
                int updateTime = int.Parse("10000");
                bool print = bool.Parse(args[3]);

                if (!print)
                {
                    Console.WriteLine(string.Format("args: {0}", string.Join(" ", args)));
                }

                lock (obj) { DoWork(deviceName, deleteNoInfoLines, updateTime, print); }
            }
        }

        public static void DoWork(string name, bool delete, int timer, bool print)
        {
            while (true)
            {
                string[] arr = GetDeviceInformation(name, delete);

                string data = DateTime.Now.ToString() + Environment.NewLine + string.Join("", arr);

                WriteToFile(data.Replace("\r\n", "#"));

                if (print)
                {
                    Console.Clear();
                    Console.Write(data);
                }

                Thread.Sleep(timer);
            }
        }

        public static void WriteToFile(string data)
        {
            while(File.Exists(TMP_FILE_PATH + "do_not_write"))
            {

            }

            File.Create(TMP_FILE_PATH + "do_not_read").Close();

            using (StreamWriter writer = new StreamWriter(FILE_PATH, true, Encoding.ASCII))
            {
                writer.WriteLine(data);
            }

            File.Delete(TMP_FILE_PATH + "do_not_read");
        }

        private static string[] GetDeviceInformation(string name, bool delete = true)
        {
            ManagementClass management = new ManagementClass("Win32_" + name);

            try
            {
                List<string> list = FormProperties(management.GetInstances(), management.Properties);

                if (delete)
                {
                    list = DeleteNoInfoLines(list);
                }

                return list.ToArray();
            }
            catch (Exception e)
            {

            }

            return new string[] { ERROR };
        }

        private static List<string> FormProperties(ManagementObjectCollection collection, PropertyDataCollection properties)
        {
            List<string> rows = new List<string>();

            foreach (ManagementObject item in collection)
            {
                foreach (PropertyData property in properties)
                {
                    string str = property.Name + ": ";

                    Object value = item.Properties[property.Name].Value;

                    str += (value == null) ? NO_INFO : value.ToString();

                    rows.Add(str + Environment.NewLine);
                }

                return rows;
            }

            return null;
        }

        private static List<string> DeleteNoInfoLines(List<string> list)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < list.Count(); i++)
            {
                if (!list[i].Contains(NO_INFO))
                {
                    result.Add(list[i]);
                }
            }

            return result;
        }
    }
}
