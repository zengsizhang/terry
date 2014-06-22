namespace LostMinerLib
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class importdataproc : Form
    {
        private IContainer components = null;

        public importdataproc()
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
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x11c, 0x106);
            base.ControlBox = false;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "importdataproc";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.ResumeLayout(false);
        }
    }
}

