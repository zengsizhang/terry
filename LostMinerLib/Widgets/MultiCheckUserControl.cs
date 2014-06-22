namespace TimeSearcher.Widgets
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class MultiCheckUserControl : UserControl
    {
        private string[] _checkNames;
        private CheckBox[] _checks;
        private Container components;

        public event StatusChanged ChecksChanged;

        public MultiCheckUserControl(string[] checkNames)
        {
            this.InitializeComponent();
            this._checkNames = checkNames;
            this.myInit();
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
            base.Name = "MultiCheckUserControl";
            base.Size = new Size(0xc0, 0xb8);
        }

        private void MultiCheckUserControl_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChecksChanged(this);
        }

        private void myInit()
        {
            this._checks = new CheckBox[this.NumChecks];
            for (int i = 0; i < this.NumChecks; i++)
            {
                this._checks[i] = new CheckBox();
                this._checks[i].Text = this._checkNames[i];
                this._checks[i].Checked = true;
                this._checks[i].Dock = DockStyle.Top;
                this._checks[i].CheckedChanged += new EventHandler(this.MultiCheckUserControl_CheckedChanged);
            }
            for (int j = this.NumChecks; j > 0; j--)
            {
                base.Controls.Add(this._checks[j - 1]);
            }
        }

        protected virtual void OnChecksChanged(MultiCheckUserControl sourceMCUC)
        {
            if (this.ChecksChanged != null)
            {
                this.ChecksChanged(sourceMCUC);
            }
        }

        public int[] CheckedIndex
        {
            get
            {
                ArrayList list = new ArrayList();
                for (int i = 0; i < this.NumChecks; i++)
                {
                    if (this._checks[i].Checked)
                    {
                        list.Add(i);
                    }
                }
                return (int[]) list.ToArray(typeof(int));
            }
        }

        public int NumChecks
        {
            get
            {
                return this._checkNames.Length;
            }
        }

        public delegate void StatusChanged(MultiCheckUserControl sourceMCUC);
    }
}

