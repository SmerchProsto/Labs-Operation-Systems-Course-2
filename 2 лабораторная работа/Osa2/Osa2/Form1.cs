using System;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;



namespace Osa2
{
    public partial class Form1 : Form
    {
        //Чтение
        const int PROCESS_WM_READ = 0x0010;
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(long hProcess, long lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
        //

        //Запись
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(long hProcess, long lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);
        //
        public Form1()
        {
            InitializeComponent();
        } // нет
        private void Form1_Load(object sender, EventArgs e)
        {

        }// нет

       

        public void Vizov()
        {
            int rz = Convert.ToInt32(textBox4.Text);
            string textStr = Convert.ToString(("0x" + textBox3.Text.ToString()));

            Process process = Process.GetProcessesByName("notepad++")[0];
            IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
            int bytesRead = 0;
            byte[] buffer = new byte[rz * 2];
            ReadProcessMemory((int)processHandle, Convert.ToInt64(textStr), buffer, buffer.Length, ref bytesRead);
            MessageBox.Show(Encoding.UTF8.GetString(buffer) + " (" + bytesRead.ToString() + "bytes)");
            textBox1.Text = Encoding.UTF8.GetString(buffer);
        }

        public long transformAdress(string number)
        {

            long result = 0;
            
            char[] a = number.ToCharArray();
            char elem = number[0];
            int aL = number.Length - 1;

            for (int i = 0; i < aL; i++) {
                long val = (long)Math.Pow(16, aL - i);
                if (a[i] == 'A')
                {
                    result += 10 * val;
                }
                else if (a[i] == 'B')
                {
                    result += 11 * val;
                }
                else if (a[i] == 'C')
                {
                    result += 12 * val;
                }
                else if (a[i] == 'D')
                {
                    result += 13 * val;
                }
                else if (a[i] == 'E')
                {
                    result += 14 * val;
                }
                else if (a[i] == 'F')
                {
                    result += 15 * val;
                }
                else
                {
                    result += a[i] * val;
                }

            }
            
            return result;
		}

        public void Zapis()
        {
            //int rz = Convert.ToInt32(textBox4.Text);
            string textStr = Convert.ToString(("0x" + textBox3.Text));

            Process process = Process.GetProcessesByName("notepad++")[0];
            IntPtr processHandle = OpenProcess((PROCESS_VM_WRITE | PROCESS_VM_OPERATION), false, process.Id);
            int bytesWritten = 0;
            byte[] buffer = Encoding.UTF8.GetBytes(string.Format("{0}\0", textBox2.Text));
            WriteProcessMemory((int)processHandle, Convert.ToInt64(textStr), buffer, buffer.Length, ref bytesWritten);
            
        }
        
        public void Fpodk()
        {
            using var query = new ManagementObjectSearcher("SELECT AllocatedBaseSize FROM Win32_PageFileUsage");
            foreach (ManagementBaseObject obj in query.Get())
            {
                uint used = (uint)obj.GetPropertyValue("AllocatedBaseSize") * 1048576;
                textBox5.Text = used.ToString() + " Байт";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vizov();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Zapis();
        }

        public void Узнать_Click(object sender, EventArgs e)
        {
            Fpodk();
        }

    }
}
