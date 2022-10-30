
namespace Osa3
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.MenuText;
			this.textBox1.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBox1.ForeColor = System.Drawing.SystemColors.Menu;
			this.textBox1.Location = new System.Drawing.Point(228, 12);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(560, 349);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "Здравствуй\r\n";
			// 
			// textBox2
			// 
			this.textBox2.BackColor = System.Drawing.SystemColors.MenuText;
			this.textBox2.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBox2.ForeColor = System.Drawing.SystemColors.Window;
			this.textBox2.Location = new System.Drawing.Point(228, 359);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(560, 50);
			this.textBox2.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(476, 415);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Enter";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Location = new System.Drawing.Point(-2, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(205, 158);
			this.label1.TabIndex = 3;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.TransparencyKey = System.Drawing.Color.Red;
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}

