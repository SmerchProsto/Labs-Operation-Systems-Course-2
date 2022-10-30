using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Osa3
{
    public partial class Form1 : Form
    {
        public string path = "C:\\OSKurs";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text += ("Путь работы: " + path) + Environment.NewLine;
        }
        private void Cr(string nFol)
        {
            string folderName = @path;
            string pathString = Path.Combine(folderName, nFol);
            Directory.CreateDirectory(pathString);
            textBox1.Text += ("Путь к новой папке: \n" + pathString + Environment.NewLine);
        }
        private void CrFile(string nFile, string pathString)
        {
            string fileName = nFile;
            pathString = Path.Combine(pathString, fileName);
            if (!File.Exists(pathString))
            {
                using (FileStream fs = File.Create(pathString))
                {
                    for (byte i = 0; i < 100; i++)
                    {
                        fs.WriteByte(i);
                    }
                }
            }
            else
            {
                MessageBox.Show("AlreadyexistsFile: " + fileName);
                return;
            }
            textBox1.Text += ("Создан файл: " + fileName) + Environment.NewLine;
        }
        private void Dir(string path)
        {
            int files = 0;
            int papki = 0;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] fileNames = dirInfo.GetFiles("*.*");
            textBox1.Text += "Список файлов:" + Environment.NewLine;
            foreach (FileInfo fi in fileNames)
            {
                string stringBuffer = "Имя:  | " + fi.Name + " |  Время последнего изменения:  | " + fi.LastAccessTime + " |  Размер:  | " + fi.Length + "Байт";
                textBox1.Text += stringBuffer + Environment.NewLine;
                files++;
            }
            DirectoryInfo dir = new DirectoryInfo(path);
            textBox1.Text += "Список папок:" + Environment.NewLine;
            foreach (var item in dir.GetDirectories())
            {
                int i = Encoding.Unicode.GetBytes(item.Name).Length;
                if (i > 255) { MessageBox.Show("Длина имени папки не может превышать 255 байт!"); }
                else
                {
                    textBox1.Text += item.Name + " |  Время последнего изменения:  | " + item.LastAccessTime + Environment.NewLine;//Список папок

                    papki++;
                }
            }
            textBox1.Text += "Файлов: " + files + Environment.NewLine + "Папок: " + papki + Environment.NewLine;
        }
        private void CopyDir(string sourcePath, string targetPath)
        {
            string fileName = "";

            string destFile = Path.Combine(targetPath, fileName);
            Directory.CreateDirectory(targetPath);

            if (Directory.Exists(sourcePath))
            {
                string[] files = Directory.GetFiles(sourcePath);

                foreach (string s in files)
                {
                    fileName = Path.GetFileName(s);
                    destFile = Path.Combine(targetPath, fileName);
                    File.Copy(s, destFile, true);
                }
                DirectoryInfo dir = new DirectoryInfo(path);
                foreach (var item in dir.GetDirectories())
                {

                }
            }
            else
            {
                textBox1.Text += ("Такого пути не существует!") + Environment.NewLine;
            }
            textBox1.Text += "Готово!" + Environment.NewLine;
        }
      
        private void Del(string pathDir)
        {

            try
            {
                Directory.Delete(pathDir);
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);


                if (Directory.Exists(pathDir))
                {
                    try
                    {

                        Directory.Delete(pathDir, true);
                    }
                    catch (IOException a)
                    {
                        MessageBox.Show(a.Message);
                    }
                }
                textBox1.Text += "Удалено: " + pathDir;
            }
        }
        private void OpenProc(string fileName)
        {
            string fileDir = Path.Combine(path, fileName);
            Process.Start(fileDir);
        }
        private void SearchFiles(string pathDir, string format)
        {
            Directory.GetFiles(pathDir, format).ToList().ForEach(f => textBox1.Text+=(f)+ Environment.NewLine);
        }
        private void MoveFiles(string pathDir,string name, string pathToCopy)
        {
            File.Move(pathDir + name, pathToCopy);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Text = textBox2.Text;
            string[] words = Text.Split(' ');

            if (words[0] == "dir")
            {
                Dir(path);
            }
            else if (words[0] == "clear")
            {
                textBox1.Text = null;
            }
            else if (words[0] == "del")
            {
                Del(path);
            }
            
            else
            {

                try
                {
                    {
                        if (words[1].Trim() != string.Empty)
                        {
                            if (words[0] == "cd")
                            {
                                if (!Directory.Exists(words[1]))
                                {
                                    MessageBox.Show("Неправильный путь!");
                                }
                                else
                                {
                                    path = @words[1];
                                    textBox1.Text += ("Новый путь задан: " + words[1]) + Environment.NewLine;
                                }

                            }
                            else if (words[0] == "mkdir")
                            {
                                Cr(words[1]);
                            }
                            else if (words[0] == "NUL>")
                            {
                                CrFile(words[1], path);
                            }
                            else if (words[0] == "copy")
                            {
                                CopyDir(path, words[1]);
                            }
                            else if (words[0] == ">")
                            {
                                OpenProc(words[1]);
                            }
                            else if(words[0]== "DIR")
                            {
                                SearchFiles(path, words[1]);
                            }
                            else if (words[0] == "move")
                            {
                                MoveFiles(path, words[1], words[2]);
                            }


                            else textBox1.Text += "Ошибка" + Environment.NewLine;
                        }else if(!Directory.Exists(words[1]))
                            {
                                MessageBox.Show("Ошибка");
                            }
                        else textBox1.Text += "Ошибка" + Environment.NewLine;
                    }
                }
                catch { textBox1.Text += "Ошибка(catch)" + Environment.NewLine; }
            }
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.Refresh();

        }
    }

}

