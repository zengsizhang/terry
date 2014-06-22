namespace TimeSearcher.AttrWidgets
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher.Attribute;

    public class DateTimeRangeChooser : RangeUserControl
    {
        private CheckBox _chkAttr;
        private DateTimePicker _endDtPicker;
        private Label _lblEnd;
        private Label _lblStart;
        private DateTimePicker _startDtPicker;
        private IContainer components;

        public DateTimeRangeChooser(int rangeIndex, string chkName, AttrValue minLimit, AttrValue maxLimit) : base(rangeIndex, minLimit, maxLimit)
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

        private void _endDtPicker_ValueChanged(object sender, EventArgs e)
        {
            base.setMaxRange(new AttrDateTime(Convert.ToString(this._endDtPicker.Value)));
        }

        private void _startDtPicker_ValueChanged(object sender, EventArgs e)
        {
            base.setMinRange(new AttrDateTime(Convert.ToString(this._startDtPicker.Value)));
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
            this._startDtPicker = new DateTimePicker();
            this._endDtPicker = new DateTimePicker();
            this._lblStart = new Label();
            this._lblEnd = new Label();
            this._chkAttr = new CheckBox();
            base.SuspendLayout();
            this._startDtPicker.Location = new Point(120, 32);
            this._startDtPicker.Name = "_startDtPicker";
            this._startDtPicker.TabIndex = 0;
            this._startDtPicker.ValueChanged += new EventHandler(this._startDtPicker_ValueChanged);
            this._endDtPicker.Location = new Point(336, 32);
            this._endDtPicker.Name = "_endDtPicker";
            this._endDtPicker.TabIndex = 1;
            this._endDtPicker.ValueChanged += new EventHandler(this._endDtPicker_ValueChanged);
            this._lblStart.Location = new Point(120, 10);
            this._lblStart.Name = "_lblStart";
            this._lblStart.Size = new Size(100, 10);
            this._lblStart.TabIndex = 2;
            this._lblStart.Text = "Minimum value";
            this._lblEnd.Location = new Point(336, 10);
            this._lblEnd.Name = "_lblEnd";
            this._lblEnd.Size = new Size(100, 10);
            this._lblEnd.TabIndex = 3;
            this._lblEnd.Text = "Maximum value";
            this._chkAttr.Checked = true;
            this._chkAttr.CheckState = CheckState.Checked;
            this._chkAttr.Location = new Point(8, 32);
            this._chkAttr.Name = "_chkAttr";
            this._chkAttr.Size = new Size(112, 24);
            this._chkAttr.TabIndex = 4;
            this._chkAttr.Text = "Attr name";
            this._chkAttr.CheckedChanged += new EventHandler(this._chkAttr_CheckedChanged);
            base.Controls.Add(this._chkAttr);
            base.Controls.Add(this._lblEnd);
            base.Controls.Add(this._lblStart);
            base.Controls.Add(this._endDtPicker);
            base.Controls.Add(this._startDtPicker);
            base.Name = "DateTimeRangeChooser";
            base.Size = new Size(544, 64);
            base.ResumeLayout(false);
        }

        private void rangeControlsSetEnabled(bool shdEnable)
        {
            this._startDtPicker.Enabled = shdEnable;
            this._endDtPicker.Enabled = shdEnable;
        }

        protected override void setMinMaxRangeOnGui()
        {
            this._startDtPicker.Value = Convert.ToDateTime(base.MinRange.StrValue);
            this._endDtPicker.Value = Convert.ToDateTime(base.MaxRange.StrValue);
        }
    }
}

