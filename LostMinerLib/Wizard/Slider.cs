namespace TimeSearcher.Wizard
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Slider : UserControl
    {
        private IContainer components;
        private bool floatMode;
        private Label max;
        private Label min;
        private bool percentMode;
        private Label title;
        private System.Windows.Forms.TrackBar trackBar;
        private Label value;

        public Slider()
        {
            this.InitializeComponent();
            this.min.Text = this.trackBar.Minimum.ToString();
            this.max.Text = this.trackBar.Maximum.ToString();
            this.trackBar.ValueChanged += new EventHandler(this.trackBar_ValueChanged);
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
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.value = new Label();
            this.min = new Label();
            this.max = new Label();
            this.title = new Label();
            this.trackBar.BeginInit();
            base.SuspendLayout();
            this.trackBar.Location = new Point(0x16, 0x17);
            this.trackBar.MaximumSize = new Size(350, 0x38);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new Size(0x13a, 0x38);
            this.trackBar.TabIndex = 0;
            this.value.Anchor = AnchorStyles.Bottom;
            this.value.AutoSize = true;
            this.value.Location = new Point(0xa6, 0x3d);
            this.value.Name = "value";
            this.value.Size = new Size(0x2c, 0x11);
            this.value.TabIndex = 1;
            this.value.Text = "Value";
            this.min.Anchor = AnchorStyles.Left;
            this.min.AutoSize = true;
            this.min.Location = new Point(3, 0x16);
            this.min.Name = "min";
            this.min.Size = new Size(30, 0x11);
            this.min.TabIndex = 2;
            this.min.Text = "Min";
            this.min.TextAlign = ContentAlignment.TopRight;
            this.max.Anchor = AnchorStyles.Right;
            this.max.AutoSize = true;
            this.max.Location = new Point(320, 0x16);
            this.max.Name = "max";
            this.max.Size = new Size(0x21, 0x11);
            this.max.TabIndex = 3;
            this.max.Text = "Max";
            this.title.AutoSize = true;
            this.title.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.title.Location = new Point(1, 1);
            this.title.Name = "title";
            this.title.Size = new Size(40, 0x11);
            this.title.TabIndex = 4;
            this.title.Text = "Title";
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.title);
            base.Controls.Add(this.max);
            base.Controls.Add(this.min);
            base.Controls.Add(this.value);
            base.Controls.Add(this.trackBar);
            base.Name = "Slider";
            base.Size = new Size(0x161, 0x52);
            this.trackBar.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            string str;
            if (this.floatMode)
            {
                str = (((float) this.trackBar.Value) / 10f).ToString("0.0");
            }
            else
            {
                str = this.trackBar.Value.ToString();
            }
            if (this.percentMode)
            {
                str = str + "%";
            }
            this.value.Text = str;
        }

        public bool FloatMode
        {
            get
            {
                return this.floatMode;
            }
            set
            {
                this.floatMode = value;
                this.trackBar_ValueChanged(this, EventArgs.Empty);
            }
        }

        public int Maximum
        {
            get
            {
                return this.trackBar.Maximum;
            }
            set
            {
                this.trackBar.Maximum = value;
                if (this.FloatMode)
                {
                    this.max.Text = (((float) this.trackBar.Maximum) / 10f).ToString("0.0");
                }
                else
                {
                    this.max.Text = this.trackBar.Maximum.ToString();
                }
            }
        }

        public int Minimum
        {
            get
            {
                return this.trackBar.Minimum;
            }
            set
            {
                this.trackBar.Minimum = value;
                if (WizardForm.TSForm != null)
                {
                    this.min.Text = WizardForm.TSForm.DataSet.getTimePointName(value);
                }
            }
        }

        public bool PercentMode
        {
            get
            {
                return this.percentMode;
            }
            set
            {
                this.percentMode = value;
                this.trackBar_ValueChanged(this, EventArgs.Empty);
            }
        }

        public string Title
        {
            get
            {
                return this.title.Text;
            }
            set
            {
                this.title.Text = value;
            }
        }

        public System.Windows.Forms.TrackBar TrackBar
        {
            get
            {
                return this.trackBar;
            }
        }

        public int Value
        {
            get
            {
                return this.trackBar.Value;
            }
            set
            {
                this.trackBar.Value = value;
            }
        }
    }
}

