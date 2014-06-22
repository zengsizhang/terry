namespace TimeSearcher.AttrWidgets
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher.Attribute;

    public class DoubleRangeChooser : RangeUserControl
    {
        private CheckBox _chkAttr;
        private TextBox _endTextBox;
        private Label _lblEnd;
        private Label _lblStart;
        private Button _maxOkButton;
        private Button _minOkButton;
        private TextBox _startTextBox;
        private IContainer components;

        public DoubleRangeChooser(int rangeIndex, string chkName, AttrValue minLimit, AttrValue maxLimit) : base(rangeIndex, minLimit, maxLimit)
        {
            this.InitializeComponent();
            this._chkAttr.Text = chkName;
            base.setChecked(this._chkAttr.Checked);
        }

        private void _chkAttr_CheckedChanged(object sender, EventArgs e)
        {
            base.setChecked(this._chkAttr.Checked);
            this.rangeControlsSetEnabled(this._chkAttr.Checked);
        }

        private void _maxOkButton_Click(object sender, EventArgs e)
        {
            base.setMaxRange(new AttrDouble(this._endTextBox.Text));
        }

        private void _minOkButton_Click(object sender, EventArgs e)
        {
            base.setMinRange(new AttrDouble(this._startTextBox.Text));
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
            this._startTextBox = new TextBox();
            this._endTextBox = new TextBox();
            this._lblStart = new Label();
            this._lblEnd = new Label();
            this._chkAttr = new CheckBox();
            this._minOkButton = new Button();
            this._maxOkButton = new Button();
            base.SuspendLayout();
            this._startTextBox.Location = new Point(120, 32);
            this._startTextBox.Name = "_startTextBox";
          //  this._startTextBox.RightToLeft = RightToLeft.Yes;
            this._startTextBox.TabIndex = 0;
            this._startTextBox.Text = "";
            this._endTextBox.Location = new Point(336, 32);
            this._endTextBox.Name = "_endTextBox";
//            this._endTextBox.RightToLeft = RightToLeft.Yes;
            this._endTextBox.TabIndex = 1;
            this._endTextBox.Text = "";
            this._lblStart.Location = new Point(120, 16);
            this._lblStart.Name = "_lblStart";
            this._lblStart.Size = new Size(88, 16);
            this._lblStart.TabIndex = 2;
            this._lblStart.Text = "Minimum value";
            this._lblEnd.Location = new Point(336, 16);
            this._lblEnd.Name = "_lblEnd";
            this._lblEnd.Size = new Size(96, 16);
            this._lblEnd.TabIndex = 3;
            this._lblEnd.Text = "Maximum value";
            this._chkAttr.Checked = true;
            this._chkAttr.CheckState = CheckState.Checked;
            this._chkAttr.Location = new Point(8, 32);
            this._chkAttr.Name = "_chkAttr";
            this._chkAttr.Size = new Size(112, 24);
            this._chkAttr.TabIndex = 5;
            this._chkAttr.Text = "Attr name";
            this._chkAttr.CheckedChanged += new EventHandler(this._chkAttr_CheckedChanged);
            this._minOkButton.Location = new Point(232, 32);
            this._minOkButton.Name = "_minOkButton";
            this._minOkButton.Size = new Size(24, 20);
            this._minOkButton.TabIndex = 6;
            this._minOkButton.Text = "ok";
            this._minOkButton.Click += new EventHandler(this._minOkButton_Click);
            this._maxOkButton.Location = new Point(448, 32);
            this._maxOkButton.Name = "_maxOkButton";
            this._maxOkButton.Size = new Size(24, 20);
            this._maxOkButton.TabIndex = 7;
            this._maxOkButton.Text = "ok";
            this._maxOkButton.Click += new EventHandler(this._maxOkButton_Click);
            base.Controls.Add(this._maxOkButton);
            base.Controls.Add(this._minOkButton);
            base.Controls.Add(this._chkAttr);
            base.Controls.Add(this._lblEnd);
            base.Controls.Add(this._lblStart);
            base.Controls.Add(this._endTextBox);
            base.Controls.Add(this._startTextBox);
            base.Name = "DoubleRangeChooser";
            base.Size = new Size(480, 64);
            base.ResumeLayout(false);
        }

        private void rangeControlsSetEnabled(bool shdEnable)
        {
            this._startTextBox.Enabled = shdEnable;
            this._minOkButton.Enabled = shdEnable;
            this._endTextBox.Enabled = shdEnable;
            this._maxOkButton.Enabled = shdEnable;
        }

        protected override void setMinMaxRangeOnGui()
        {
            this._startTextBox.Text = base.MinRange.StrValue;
            this._endTextBox.Text = base.MaxRange.StrValue;
        }
    }
}

