namespace TimeSearcher.AttrWidgets
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TimePicker : UserControl
    {
        private ComboBox _comboDay;
        private ComboBox _comboMonth;
        private int[] _daysInMonth = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private NumericUpDown _nudYear;
        private Container components;

        public TimePicker()
        {
            this.InitializeComponent();
        }

        private void _comboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this._comboDay.SelectedIndex;
            int num = this._daysInMonth[this._comboMonth.SelectedIndex];
            this._comboDay.Items.Clear();
            string[] items = new string[num];
            for (int i = 0; i < num; i++)
            {
                items[i] = Convert.ToString(i);
            }
            this._comboDay.Items.AddRange(items);
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
            this._comboDay = new ComboBox();
            this._comboMonth = new ComboBox();
            this._nudYear = new NumericUpDown();
            this._nudYear.BeginInit();
            base.SuspendLayout();
            this._comboDay.DropDownStyle = ComboBoxStyle.DropDownList;
            this._comboDay.Location = new Point(120, 8);
            this._comboDay.Name = "_comboDay";
            this._comboDay.Size = new Size(64, 21);
            this._comboDay.TabIndex = 1;
            this._comboMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            this._comboMonth.Items.AddRange(new object[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
            this._comboMonth.Location = new Point(8, 8);
            this._comboMonth.Name = "_comboMonth";
            this._comboMonth.Size = new Size(104, 21);
            this._comboMonth.TabIndex = 2;
            this._comboMonth.SelectedIndexChanged += new EventHandler(this._comboMonth_SelectedIndexChanged);
            this._nudYear.Location = new Point(192, 8);
            int[] bits = new int[4];
            bits[0] = 2300;
            this._nudYear.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 1970;
            this._nudYear.Minimum = new decimal(numArray2);
            this._nudYear.Name = "_nudYear";
            this._nudYear.Size = new Size(56, 20);
            this._nudYear.TabIndex = 3;
            int[] numArray3 = new int[4];
            numArray3[0] = 2005;
            this._nudYear.Value = new decimal(numArray3);
            base.Controls.Add(this._nudYear);
            base.Controls.Add(this._comboMonth);
            base.Controls.Add(this._comboDay);
            base.Name = "TimePicker";
            base.Size = new Size(4096, 32);
            this._nudYear.EndInit();
            base.ResumeLayout(false);
        }
    }
}

