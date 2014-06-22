namespace TimeSearcher
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SetSizeForm : Form
    {
        private Button _cancelButton;
        private Form _form;
        private Label _lblHeight;
        private Label _lblWidth;
        private NumericUpDown _nudHeight;
        private NumericUpDown _nudWidth;
        private Button _okButton;
        private ComboBox _widthHeightCombo;
        private Container components;
        private Label label1;

        public SetSizeForm(Form form)
        {
            this.InitializeComponent();
            this._form = form;
            this._nudWidth.Value = this._form.Width;
            this._nudHeight.Value = this._form.Height;
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            this._form.Size = new Size((int) this._nudWidth.Value, (int) this._nudHeight.Value);
            base.Close();
        }

        private void _widthHeightCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] strArray = ((string) this._widthHeightCombo.SelectedItem).Split(new char[] { 'x' });
            int num = Convert.ToInt32(strArray[0]);
            int num2 = Convert.ToInt32(strArray[1]);
            this._nudWidth.Value = num;
            this._nudHeight.Value = num2;
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SetSizeForm));
            this._lblWidth = new Label();
            this._lblHeight = new Label();
            this._nudWidth = new NumericUpDown();
            this._nudHeight = new NumericUpDown();
            this._okButton = new Button();
            this._cancelButton = new Button();
            this._widthHeightCombo = new ComboBox();
            this.label1 = new Label();
            this._nudWidth.BeginInit();
            this._nudHeight.BeginInit();
            base.SuspendLayout();
            this._lblWidth.Location = new Point(0x10, 80);
            this._lblWidth.Name = "_lblWidth";
            this._lblWidth.Size = new Size(40, 0x10);
            this._lblWidth.TabIndex = 0;
            this._lblWidth.Text = "Width:";
            this._lblHeight.Location = new Point(0x10, 0xe8);
            this._lblHeight.Name = "_lblHeight";
            this._lblHeight.Size = new Size(0x30, 0x10);
            this._lblHeight.TabIndex = 1;
            this._lblHeight.Text = "Height:";
            this._nudWidth.Location = new Point(80, 80);
            int[] bits = new int[4];
            bits[0] = 0x640;
            this._nudWidth.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 100;
            this._nudWidth.Minimum = new decimal(numArray2);
            this._nudWidth.Name = "_nudWidth";
            this._nudWidth.Size = new Size(0x38, 20);
            this._nudWidth.TabIndex = 2;
            int[] numArray3 = new int[4];
            numArray3[0] = 100;
            this._nudWidth.Value = new decimal(numArray3);
            this._nudHeight.Location = new Point(80, 0xe8);
            int[] numArray4 = new int[4];
            numArray4[0] = 0x4b0;
            this._nudHeight.Maximum = new decimal(numArray4);
            int[] numArray5 = new int[4];
            numArray5[0] = 100;
            this._nudHeight.Minimum = new decimal(numArray5);
            this._nudHeight.Name = "_nudHeight";
            this._nudHeight.Size = new Size(0x38, 20);
            this._nudHeight.TabIndex = 3;
            int[] numArray6 = new int[4];
            numArray6[0] = 100;
            this._nudHeight.Value = new decimal(numArray6);
            this._okButton.Location = new Point(0x20, 0xb0);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new Size(0x38, 0x17);
            this._okButton.TabIndex = 4;
            this._okButton.Text = "OK";
            this._okButton.Click += new EventHandler(this._okButton_Click);
            this._cancelButton.Location = new Point(0x70, 0xb0);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new Size(0x38, 0x17);
            this._cancelButton.TabIndex = 5;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.Click += new EventHandler(this._cancelButton_Click);
            this._widthHeightCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this._widthHeightCombo.Items.AddRange(new object[] { "640x480", "720x480", "800x600", "1024x768", "1152x864", "1280x1024", "1600x1200" });
            this._widthHeightCombo.Location = new Point(0x60, 0x20);
            this._widthHeightCombo.Name = "_widthHeightCombo";
            this._widthHeightCombo.Size = new Size(0x79, 0x15);
            this._widthHeightCombo.TabIndex = 6;
            this._widthHeightCombo.SelectedIndexChanged += new EventHandler(this._widthHeightCombo_SelectedIndexChanged);
            this.label1.Location = new Point(8, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x58, 0x18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Width x Height:";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(240, 0xde);
            base.Controls.Add(this.label1);
            base.Controls.Add(this._widthHeightCombo);
            base.Controls.Add(this._cancelButton);
            base.Controls.Add(this._okButton);
            base.Controls.Add(this._nudHeight);
            base.Controls.Add(this._nudWidth);
            base.Controls.Add(this._lblHeight);
            base.Controls.Add(this._lblWidth);
            base.Name = "SetSizeForm";
            this.Text = "SetSizeForm";
            this._nudWidth.EndInit();
            this._nudHeight.EndInit();
            base.ResumeLayout(false);
        }
    }
}

