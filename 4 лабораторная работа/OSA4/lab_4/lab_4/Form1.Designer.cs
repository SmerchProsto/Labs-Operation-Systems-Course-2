
namespace lab_4
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.startProgramsB = new System.Windows.Forms.Button();
            this.deviceName = new System.Windows.Forms.TextBox();
            this.killProgramsB = new System.Windows.Forms.Button();
            this.writerArgs = new System.Windows.Forms.TextBox();
            this.hideCheck = new System.Windows.Forms.CheckBox();
            this.readerArgs = new System.Windows.Forms.TextBox();
            this.saveB = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(580, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(380, 388);
            this.textBox1.TabIndex = 0;
            // 
            // startProgramsB
            // 
            this.startProgramsB.Location = new System.Drawing.Point(580, 419);
            this.startProgramsB.Name = "startProgramsB";
            this.startProgramsB.Size = new System.Drawing.Size(181, 46);
            this.startProgramsB.TabIndex = 1;
            this.startProgramsB.Text = "Старт";
            this.startProgramsB.UseVisualStyleBackColor = true;
            this.startProgramsB.Click += new System.EventHandler(this.button1_Click);
            // 
            // deviceName
            // 
            this.deviceName.Location = new System.Drawing.Point(12, 12);
            this.deviceName.Name = "deviceName";
            this.deviceName.Size = new System.Drawing.Size(162, 20);
            this.deviceName.TabIndex = 2;
            // 
            // killProgramsB
            // 
            this.killProgramsB.Location = new System.Drawing.Point(779, 420);
            this.killProgramsB.Name = "killProgramsB";
            this.killProgramsB.Size = new System.Drawing.Size(181, 45);
            this.killProgramsB.TabIndex = 3;
            this.killProgramsB.Text = "Стоп";
            this.killProgramsB.UseVisualStyleBackColor = true;
            this.killProgramsB.Click += new System.EventHandler(this.button2_Click);
            // 
            // writerArgs
            // 
            this.writerArgs.Location = new System.Drawing.Point(12, 185);
            this.writerArgs.Name = "writerArgs";
            this.writerArgs.Size = new System.Drawing.Size(162, 20);
            this.writerArgs.TabIndex = 4;
            this.writerArgs.Visible = false;
            // 
            // hideCheck
            // 
            this.hideCheck.AutoSize = true;
            this.hideCheck.Location = new System.Drawing.Point(915, 471);
            this.hideCheck.Name = "hideCheck";
            this.hideCheck.Size = new System.Drawing.Size(52, 17);
            this.hideCheck.TabIndex = 5;
            this.hideCheck.Text = "Фон.";
            this.hideCheck.UseVisualStyleBackColor = true;
            // 
            // readerArgs
            // 
            this.readerArgs.Location = new System.Drawing.Point(12, 234);
            this.readerArgs.Name = "readerArgs";
            this.readerArgs.Size = new System.Drawing.Size(162, 20);
            this.readerArgs.TabIndex = 6;
            this.readerArgs.Visible = false;
            // 
            // saveB
            // 
            this.saveB.Location = new System.Drawing.Point(12, 38);
            this.saveB.Name = "saveB";
            this.saveB.Size = new System.Drawing.Size(74, 23);
            this.saveB.TabIndex = 7;
            this.saveB.Text = "Сохранить";
            this.saveB.UseVisualStyleBackColor = true;
            this.saveB.Click += new System.EventHandler(this.saveB_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(296, 431);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Отключить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(441, 431);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "button2";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(37, 104);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 12;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(244, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(330, 388);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(37, 141);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 500);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.saveB);
            this.Controls.Add(this.readerArgs);
            this.Controls.Add(this.hideCheck);
            this.Controls.Add(this.writerArgs);
            this.Controls.Add(this.killProgramsB);
            this.Controls.Add(this.deviceName);
            this.Controls.Add(this.startProgramsB);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button startProgramsB;
        private System.Windows.Forms.TextBox deviceName;
        private System.Windows.Forms.Button killProgramsB;
        private System.Windows.Forms.TextBox writerArgs;
        private System.Windows.Forms.CheckBox hideCheck;
        private System.Windows.Forms.TextBox readerArgs;
        private System.Windows.Forms.Button saveB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
    }
}

