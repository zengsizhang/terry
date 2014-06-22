namespace TimeSearcher.AttrWidgets
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher.Attribute;

    public class DayOfWeekRangeChooser : RangeUserControl
    {
        private CheckBox _chkAttr;
        private ComboBox _endCombo;
        private Label _lblEnd;
        private Label _lblStart;
        private ComboBox _startCombo;
        private IContainer components;

        public DayOfWeekRangeChooser(int rangeIndex, string chkName, AttrValue minLimit, AttrValue maxLimit) : base(rangeIndex, minLimit, maxLimit)
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

        private void _endCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.setMaxRange(new AttrDayOfWeek((string) this._endCombo.SelectedItem));
        }

        private void _startCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.setMinRange(new AttrDayOfWeek((string) this._startCombo.SelectedItem));
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
            this._startCombo = new ComboBox();
            this._lblStart = new Label();
            this._lblEnd = new Label();
            this._endCombo = new ComboBox();
            this._chkAttr = new CheckBox();
            base.SuspendLayout();
            this._startCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this._startCombo.Items.AddRange(new object[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" });
            this._startCombo.Location = new Point(120, 32);
            this._startCombo.Name = "_startCombo";
            this._startCombo.Size = new Size(121, 21);
            this._startCombo.TabIndex = 0;
            this._startCombo.SelectedIndexChanged += new EventHandler(this._startCombo_SelectedIndexChanged);
            this._lblStart.Location = new Point(120, 16);
            this._lblStart.Name = "_lblStart";
            this._lblStart.Size = new Size(112, 16);
            this._lblStart.TabIndex = 1;
            this._lblStart.Text = "Minimum value";
            this._lblEnd.Location = new Point(336, 16);
            this._lblEnd.Name = "_lblEnd";
            this._lblEnd.Size = new Size(88, 16);
            this._lblEnd.TabIndex = 2;
            this._lblEnd.Text = "Maximum value";
            this._endCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this._endCombo.Items.AddRange(new object[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" });
            this._endCombo.Location = new Point(336, 32);
            this._endCombo.Name = "_endCombo";
            this._endCombo.Size = new Size(121, 21);
            this._endCombo.TabIndex = 3;
            this._endCombo.SelectedIndexChanged += new EventHandler(this._endCombo_SelectedIndexChanged);
            this._chkAttr.Checked = true;
            this._chkAttr.CheckState = CheckState.Checked;
            this._chkAttr.Location = new Point(8, 32);
            this._chkAttr.Name = "_chkAttr";
            this._chkAttr.Size = new Size(112, 24);
            this._chkAttr.TabIndex = 6;
            this._chkAttr.Text = "Attr name";
            this._chkAttr.CheckedChanged += new EventHandler(this._chkAttr_CheckedChanged);
            base.Controls.Add(this._chkAttr);
            base.Controls.Add(this._endCombo);
            base.Controls.Add(this._lblEnd);
            base.Controls.Add(this._lblStart);
            base.Controls.Add(this._startCombo);
            base.Name = "DayOfWeekRangeChooser";
            base.Size = new Size(464, 64);
            base.ResumeLayout(false);
        }

        private void rangeControlsSetEnabled(bool shdEnable)
        {
            this._startCombo.Enabled = shdEnable;
            this._endCombo.Enabled = shdEnable;
        }

        protected override void setMinMaxRangeOnGui()
        {
            int index = this._startCombo.Items.IndexOf(base.MinRange);
            this._startCombo.SelectedIndex = index;
            int num2 = this._endCombo.Items.IndexOf(base.MaxRange);
            this._endCombo.SelectedIndex = num2;
        }
    }
}

