using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace lab_4
{
    public partial class Form1 : Form
    {
       
        
        public const int WM_COPYDATA = 0x4A;

        public readonly string WRITER_PATH = @"C:\Users\smerc\OneDrive\Рабочий стол\лабы ос\4 лабораторная работа\OSA4\MyWriter\MyWriter\bin\Debug\MyWriter.exe";
        public readonly string WRITER2_PATH = @"C:\Users\smerc\OneDrive\Рабочий стол\лабы ос\4 лабораторная работа\OSA4\MyWriter2\MyWriter\bin\Debug\MyWriter.exe";
        public readonly string READER_PATH = @"C:\Users\smerc\OneDrive\Рабочий стол\лабы ос\4 лабораторная работа\OSA4\MyReader\MyReader\bin\Debug\MyReader.exe";

        public string lastData = string.Empty;

        Process writer = new Process();
        Process writer2 = new Process();
        Process reader = new Process();

        const int OPEN_EXISTING = 3;
        const uint GENERIC_READ = 0x80000000;
        const uint GENERIC_WRITE = 0x40000000;
        const uint IOCTL_STORAGE_EJECT_MEDIA = 0x2D4808;


        public struct COPYDATASTRUCT
        {
            public int cbData;
            public IntPtr dwData;
            public IntPtr lpData;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.ScrollBars = ScrollBars.Both;

            deviceName.Text = "PointingDevice";
            writerArgs.Text = "true 250 true";
            readerArgs.Text = "250";

            writer.StartInfo.FileName = WRITER_PATH;
            writer2.StartInfo.FileName = WRITER2_PATH;
            reader.StartInfo.FileName = READER_PATH;

            LoadInfo();


        }

        private void StartPrograms(string writerArgs, string readerArgs)
        {
            startProgramsB.Enabled = false;

            writer.StartInfo.Arguments = writerArgs;
            writer2.StartInfo.Arguments = writerArgs;
            reader.StartInfo.Arguments = readerArgs;

            if (hideCheck.Checked)
            {
                writer.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                writer2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                reader.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            else
            {
                writer.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                writer2.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                reader.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            }

            writer.Start();
            writer2.Start();
            reader.Start();
        }

        private void KillProgram(Process process)
        {
            try
            {
                process.Kill();
            }
            catch
            {
                string[] tmpArr = process.StartInfo.FileName.Split('\\');
                string programName = tmpArr[tmpArr.Length - 1];

                MessageBox.Show(string.Format("Программа" + programName + " закрыта"));
            }
            finally
            {
                startProgramsB.Enabled = true;
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string writerArgs = deviceName.Text + " " + this.writerArgs.Text;

            StartPrograms(writerArgs, readerArgs.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KillProgram(writer);
            KillProgram(writer2);
            KillProgram(reader);
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            switch(m.Msg)
            {
                case WM_COPYDATA:

                    COPYDATASTRUCT data = new COPYDATASTRUCT() { };
                    data = (COPYDATASTRUCT)Marshal.PtrToStructure(m.LParam, typeof(COPYDATASTRUCT));

                    byte[] arr = new byte[data.cbData];
                    Marshal.Copy(data.lpData, arr, 0, data.cbData);

                    string str = Encoding.UTF8.GetString(arr);
                    string[] rows = str.Split('#');

                    textBox1.Clear();
                    for(int i = 0; i < rows.Length; i++)
                    {
                        textBox1.Text += rows[i] + "\r\n";
                    }

                    lastData = string.Join("\r\n", rows);

                    break;
            }
            base.WndProc(ref m);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Process tmp = Process.GetProcessesByName("MyWriter")[0];
                tmp.Kill();

                tmp = Process.GetProcessesByName("MyWriter2")[0];
                tmp.Kill();

                tmp = Process.GetProcessesByName("MyReader")[0];
                tmp.Kill();
            }
            catch
            {

            }
        }

        private void saveB_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(deviceName.Text + ".txt"))
            {
                writer.WriteLine(lastData);
            }
        }


        //ОТКЛЮЧЕНИЕ ЮСБ УСТРОЙСТВ!!!!!
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int CloseHandle(IntPtr handle);

        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int DeviceIoControl
            (IntPtr deviceHandle, uint ioControlCode,
              IntPtr inBuffer, int inBufferSize,
              IntPtr outBuffer, int outBufferSize,
              ref int bytesReturned, IntPtr overlapped);

        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern IntPtr CreateFile
            (string filename, uint desiredAccess,
              uint shareMode, IntPtr securityAttributes,
              int creationDisposition, int flagsAndAttributes,
              IntPtr templateFile);

        string diskName = string.Empty;
        List<string> kvp;
        private void UsbDiskList()
        {
            //string diskName = string.Empty;
            kvp = new List<string>();
            //предварительно очищаем список
            comboBox1.Items.Clear();

            //Получение списка накопителей подключенных через интерфейс USB
            foreach (ManagementObject drive in new ManagementObjectSearcher("select * from Win32_DiskDrive where InterfaceType='USB'").Get())
            {
                //Получаем букву накопителя
                foreach (ManagementObject partition in
                   new ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + drive["DeviceID"] + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
                {
                    foreach (System.Management.ManagementObject disk in
                       new System.Management.ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + partition["DeviceID"] + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get())
                    {
                        //Получение буквы устройства
                        diskName = disk["Name"].ToString().Trim();
                        comboBox1.Items.Add(diskName + " (" + drive["Model"] + ")");



                        kvp.Add(diskName);
                        //richTextBox1.Text += diskName;
                    }
                }
            }
        }

//	Метод для извлечения USB накопителя.
        static void EjectDrive(string driveLetter)
        {
            string path = "\\\\.\\" + driveLetter;

            IntPtr handle = CreateFile(path, GENERIC_READ | GENERIC_WRITE, 0,
                IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);

            if ((long)handle == -1)
            {
                MessageBox.Show("Невозможно извлечь USB устройство!",
           "Извлечение USB накопителей", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int dummy = 0;
            DeviceIoControl(handle, IOCTL_STORAGE_EJECT_MEDIA, IntPtr.Zero, 0,
                IntPtr.Zero, 0, ref dummy, IntPtr.Zero);
            int returnValue = DeviceIoControl(handle, IOCTL_STORAGE_EJECT_MEDIA,
                     IntPtr.Zero, 0, IntPtr.Zero, 0, ref dummy, IntPtr.Zero);
            CloseHandle(handle);
            MessageBox.Show("USB устройство, успешно извлечено!",
         "Извлечение USB накопителей", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
//	Загрузка букв USB накопителей при запуске программы
        private void LoadInfo()
        {
            //Загрузка букв USB накопителей при запуске программы
            UsbDiskList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoadInfo();
            EjectDrive(diskName);
            LoadInfo();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
           
            string path = @"C:\Users\smerc\OneDrive\Рабочий стол\лабы ос\4 лабораторная работа\OSA4\usb.txt";

            string diskName = string.Empty;

            //предварительно очищаем список
            richTextBox1.Clear();

            //Получение списка накопителей подключенных через интерфейс USB
            foreach (System.Management.ManagementObject drive in
            new System.Management.ManagementObjectSearcher(
            "select * from Win32_DiskDrive where InterfaceType='USB'").Get())
            {
                //Получаем букву накопителя
                foreach (System.Management.ManagementObject partition in
                new System.Management.ManagementObjectSearcher(
                "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + drive["DeviceID"]
                + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
                {
                    foreach (System.Management.ManagementObject disk in
                    new System.Management.ManagementObjectSearcher(
                    "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='"
                    + partition["DeviceID"]
                    + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get())
                    {
                        //Получение буквы устройства
                        diskName = disk["Name"].ToString().Trim();
                        richTextBox1.Text += ("Буква накопителя = " + diskName + "\n");
                    }
                }
                //	Получение информации о подключенном устройстве.
                //Получение модели устройства
                richTextBox1.Text += ("Модель = " + drive["Model"] + "\n");

                //Получение объема устройства в гигабайтах
                decimal dSize = Math.Round((Convert.ToDecimal(
                new System.Management.ManagementObject("Win32_LogicalDisk.DeviceID='"
                + diskName + "'")["Size"]) / 1073741824), 2);
                richTextBox1.Text += ("Полный объем = " + dSize + " gb" + "\n");

                //Получение свободного места на устройстве в гигабайтах
                decimal dFree = Math.Round((Convert.ToDecimal(
                new System.Management.ManagementObject("Win32_LogicalDisk.DeviceID='"
                + diskName + "'")["FreeSpace"]) / 1073741824), 2);
                richTextBox1.Text += ("Свободный объем = " + dFree + " gb" + "\n");

                //Получение использованного места на устройстве
                decimal dUsed = dSize - dFree;
                richTextBox1.Text += ("Используемый объем = " + dUsed + " gb" + "\n");
                //	Запись информации в файл о подключенном USB – накопителе.

                using (FileStream file = File.Open(path, FileMode.Append, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(file, Encoding.UTF8))
                {
                    {
                        writer.WriteLine("Модель = " + drive["Model"] + "\n" +
                            "Полный объем = " + dSize + " gb" + "\n" +
                            "Свободный объем = " + dFree + " gb" + "\n" +
                            "Используемый объем = " + dUsed + " gb" + "\n" +
                            DateTime.Now.ToString() + "\n");
                    }
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            LoadInfo();
        }
    }
}
