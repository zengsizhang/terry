namespace LostMinerLib
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AutoImport : Form
    {
        private Button button1;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;

        public AutoImport()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.button1 = new Button();
            this.label1 = new Label();
            this.textBox1 = new TextBox();
            this.label2 = new Label();
            this.textBox2 = new TextBox();
            this.label3 = new Label();
            this.textBox3 = new TextBox();
            this.label4 = new Label();
            this.textBox4 = new TextBox();
            this.label5 = new Label();
            this.textBox5 = new TextBox();
            base.SuspendLayout();
            this.button1.Location = new Point(0xc6, 0xdb);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 0x23);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据类型";
            this.textBox1.Location = new Point(0x5d, 0x23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(100, 0x15);
            this.textBox1.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x25, 0x43);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "频率";
            this.textBox2.Location = new Point(0x5d, 0x43);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(100, 0x15);
            this.textBox2.TabIndex = 4;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x19, 100);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "均值";
            this.textBox3.Location = new Point(0x5d, 100);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(100, 0x15);
            this.textBox3.TabIndex = 6;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x19, 0x86);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x29, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "标准差";
            this.textBox4.Location = new Point(0x5e, 130);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(100, 0x15);
            this.textBox4.TabIndex = 8;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(13, 170);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x41, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "服务器路径";
            this.textBox5.Location = new Point(0x5d, 170);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Size(0xb3, 0x15);
            this.textBox5.TabIndex = 10;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x11c, 0x106);
            base.Controls.Add(this.textBox5);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.textBox4);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.textBox3);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.button1);
            base.Name = "AutoImport";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "自动导入";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

