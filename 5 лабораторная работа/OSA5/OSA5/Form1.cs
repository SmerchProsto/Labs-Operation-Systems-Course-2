using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace OSA5
{
    public partial class Form1 : Form
    {

        static object X = new object();
        static object Y = new object();
        bool __lockWasTaken = false;
        bool __lockWasTaken2 = false;
        Thread A, B;
        //int count = 0;

        public Form1()
        {
            //Monitor.Enter(X, ref __lockWasTaken);
            A = new Thread(() =>
            {
                textBox1.Invoke(new Action(() => textBox1.Text += "T(1): A - занимает X" + Environment.NewLine));
                Monitor.Enter(X);
                lock (X)
                {
                    Thread.Sleep(100);
                    
                }
                Monitor.Exit(X);
                textBox1.Invoke(new Action(() => textBox1.Text += "T(4): A - занимает X" + Environment.NewLine));
                Monitor.Enter(X);
                lock (X)
                {
                    Thread.Sleep(100);
                   
                }
                Monitor.Exit(X);
                textBox1.Invoke(new Action(() => textBox1.Text += "T(4): A - пытается занять Y" + Environment.NewLine));
                Monitor.Enter(Y);
                lock (Y)
                {
                    Thread.Sleep(100);//Взаимоблокировка
                    
                }
                Monitor.Exit(Y);
            });
           // Monitor.Enter(Y, ref __lockWasTaken2);
            B = new Thread(() =>
            {
                textBox1.Invoke(new Action(() => textBox1.Text += "T(2) Б - вытесняет А и занимает Х" + Environment.NewLine));
                Monitor.Enter(X);
                lock (X) { Thread.Sleep(200);  }
                Monitor.Exit(X);


                textBox1.Invoke(new Action(() => textBox1.Text += "T(3): Б - занимает Y" + Environment.NewLine));
                Monitor.Enter(Y);
                lock (Y)
                {
                    Thread.Sleep(100);
                    

                }
                Monitor.Exit(Y);
                textBox1.Invoke(new Action(() => textBox1.Text += "T(4): Б - пытается занять Х." + Environment.NewLine));
                Monitor.Enter(X);
                lock (X)
                {
                    Thread.Sleep(100);
                    
                }
                Monitor.Exit(X);
                textBox1.Invoke(new Action(() => textBox1.Text += "T(5): Б - занимает Y" + Environment.NewLine));
                Monitor.Enter(Y);
                lock (Y)
                {
                    Thread.Sleep(100);
                   
                }
                Monitor.Exit(Y);
                textBox1.Invoke(new Action(() => textBox1.Text += "T(5): Б - пытается занять Х. Попытка взаимоблокировки!" + Environment.NewLine));
                Monitor.Enter(X);
                lock (X)
                {
                    Thread.Sleep(100);//Взаимоблокировка
                    
                }
                Monitor.Exit(X);
                textBox1.Invoke(new Action(() => textBox1.Text += "Блокировок нет!" + Environment.NewLine));
            });
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            B.Priority = ThreadPriority.Highest;
            A.Priority = ThreadPriority.Lowest;

        }
        
        

       

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                A.Start();
                B.Start();
           
            }
            catch
            {
                MessageBox.Show("Уже было запущено!");
                A.Abort();
                B.Abort();
            }
        }
       

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                Lock_OFF(Y, __lockWasTaken2);
                textBox1.Text += "Ресурс свободен!" + Environment.NewLine;
                Thread.Sleep(1000);
            }
            if (comboBox1.SelectedIndex == 1)
            {
                Lock_OFF(X, __lockWasTaken);
                Thread.Sleep(1000);
                textBox1.Text += "Ресурс свободен!" + Environment.NewLine;
            }
        }

        public void Lock_OFF(object obj, bool lock_)
        {
            object __lockObj = obj;

            if (lock_)
            {
                Monitor.Exit(__lockObj);
            }
        }
    }
}
