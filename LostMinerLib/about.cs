namespace LostMinerLib
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class about : Form
    {
        private IContainer components = null;
        private Label label1;

        public about()
        {
            this.InitializeComponent();
            this.label1.Text = "深圳供电局有限公司窃电漏计分析及预警系统 版本2.0\r\n";
            this.label1.Text = this.label1.Text + "Copyright @ 2014 数智挖掘 All Right Reserved  \r\n";
            this.label1.Text = this.label1.Text + "sdminer.com \r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            base.Close();
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
            this.label1 = new Label();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Dock = DockStyle.Fill;
            this.label1.Location = new Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x11c, 0x106);
            base.Controls.Add(this.label1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "about";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "关于";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

