namespace TimeSearcher.AttrWidgets
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Attribute;

    public class StringRangeChooser : RangeUserControl
    {
        private CheckBox _chkAttr;
        private Label _lblValue;
        private ComboBox _strCombo;
        private IContainer components;

        public StringRangeChooser(int rangeIndex, string chkName, AttrValue[] allStrings) : base(rangeIndex, allStrings[0], allStrings[0])
        {
            this.InitializeComponent();
            this._chkAttr.Text = chkName;
            base.setChecked(this._chkAttr.Checked);
            this.myInit(AttrValue.GetStrValues(allStrings));
        }

        private void _chkAttr_CheckedChanged(object sender, EventArgs e)
        {
            base.setChecked(this._chkAttr.Checked);
            this.rangeControlsSetEnabled(this._chkAttr.Checked);
        }

        private void _strCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AttrValue minRange = new AttrString((string) this._strCombo.SelectedItem);
            base.setMinMaxRange(minRange, minRange);
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
            this._strCombo = new ComboBox();
            this._lblValue = new Label();
            this._chkAttr = new CheckBox();
            base.SuspendLayout();
            this._strCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this._strCombo.Enabled = false;
            this._strCombo.Location = new Point(120, 32);
            this._strCombo.Name = "_strCombo";
            this._strCombo.Size = new Size(144, 21);
            this._strCombo.TabIndex = 0;
            this._strCombo.SelectedIndexChanged += new EventHandler(this._strCombo_SelectedIndexChanged);
            this._lblValue.Location = new Point(120, 16);
            this._lblValue.Name = "_lblValue";
            this._lblValue.Size = new Size(100, 16);
            this._lblValue.TabIndex = 1;
            this._lblValue.Text = "Value";
            this._chkAttr.Location = new Point(8, 32);
            this._chkAttr.Name = "_chkAttr";
            this._chkAttr.Size = new Size(112, 24);
            this._chkAttr.TabIndex = 6;
            this._chkAttr.Text = "Attr name";
            this._chkAttr.CheckedChanged += new EventHandler(this._chkAttr_CheckedChanged);
            base.Controls.Add(this._chkAttr);
            base.Controls.Add(this._lblValue);
            base.Controls.Add(this._strCombo);
            base.Name = "StringRangeChooser";
            base.Size = new Size(280, 64);
            base.ResumeLayout(false);
        }

        private void myInit(string[] allStrings)
        {
            string[] destinationArray = new string[allStrings.Length];
            Array.Copy(allStrings, destinationArray, destinationArray.Length);
            Array.Sort<string>(destinationArray);
            string[] items = Utils.removeDuplicatesInSorted(destinationArray);
            this._strCombo.Items.AddRange(items);
        }

        private void rangeControlsSetEnabled(bool shdEnable)
        {
            this._strCombo.Enabled = shdEnable;
        }

        protected override void setMinMaxRangeOnGui()
        {
            int index = this._strCombo.Items.IndexOf(base.MinRange);
            this._strCombo.SelectedIndex = index;
        }
    }
}

