namespace TimeSearcher
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class OptionsForm : Form
    {
        private Button _browseFileBtn;
        private Button _CancelBtn;
        private TabPage _generalTabPage;
        private GroupBox _groupBoxVariables;
        public string _lastFilename = "";
        public RadioButton _lastFileRadioBtn;
        private Label _lblNOV;
        public RadioButton _noFileRadioBtn;
        private NumericUpDown _nudNOV;
        private Button _OKBtn;
        private const string _optionsFilePath = "tsDirs/Config/ts.ini";
        private TabControl _optionsTabControl;
        private const string _spareOptionsFilePath = "tsDirs/Spares/ts.ini";
        public TextBox _startupFilename;
        private GroupBox _startupGroup;
        public RadioButton _textFileRadioBtn;
        private string _tsTitle = "深圳供电局有限公司窃电漏计分析及预警系统 版本2.0";
        private Container components;

        public OptionsForm()
        {
            this.InitializeComponent();
            this._noFileRadioBtn.CheckedChanged += new EventHandler(this.startupRadioCheckChanged);
            this._textFileRadioBtn.CheckedChanged += new EventHandler(this.startupRadioCheckChanged);
            this._lastFileRadioBtn.CheckedChanged += new EventHandler(this.startupRadioCheckChanged);
            this._OKBtn.DialogResult = DialogResult.OK;
            this._CancelBtn.DialogResult = DialogResult.Cancel;
            this._noFileRadioBtn.Checked = true;
            this.readOptions();
        }

        private void browseFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "tqd files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this._startupFilename.Text = dialog.FileName;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public int getCheckedRadioIndex()
        {
            if (this._noFileRadioBtn.Checked)
            {
                return 1;
            }
            if (this._textFileRadioBtn.Checked)
            {
                return 2;
            }
            if (this._lastFileRadioBtn.Checked)
            {
                return 3;
            }
            return 0;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this._optionsTabControl = new System.Windows.Forms.TabControl();
            this._generalTabPage = new System.Windows.Forms.TabPage();
            this._groupBoxVariables = new System.Windows.Forms.GroupBox();
            this._lblNOV = new System.Windows.Forms.Label();
            this._nudNOV = new System.Windows.Forms.NumericUpDown();
            this._OKBtn = new System.Windows.Forms.Button();
            this._CancelBtn = new System.Windows.Forms.Button();
            this._startupGroup = new System.Windows.Forms.GroupBox();
            this._lastFileRadioBtn = new System.Windows.Forms.RadioButton();
            this._noFileRadioBtn = new System.Windows.Forms.RadioButton();
            this._textFileRadioBtn = new System.Windows.Forms.RadioButton();
            this._browseFileBtn = new System.Windows.Forms.Button();
            this._startupFilename = new System.Windows.Forms.TextBox();
            this._optionsTabControl.SuspendLayout();
            this._generalTabPage.SuspendLayout();
            this._groupBoxVariables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._nudNOV)).BeginInit();
            this._startupGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // _optionsTabControl
            // 
            this._optionsTabControl.Controls.Add(this._generalTabPage);
            this._optionsTabControl.Location = new System.Drawing.Point(0, 0);
            this._optionsTabControl.Name = "_optionsTabControl";
            this._optionsTabControl.SelectedIndex = 0;
            this._optionsTabControl.Size = new System.Drawing.Size(499, 345);
            this._optionsTabControl.TabIndex = 0;
            // 
            // _generalTabPage
            // 
            this._generalTabPage.Controls.Add(this._groupBoxVariables);
            this._generalTabPage.Controls.Add(this._OKBtn);
            this._generalTabPage.Controls.Add(this._CancelBtn);
            this._generalTabPage.Controls.Add(this._startupGroup);
            this._generalTabPage.Location = new System.Drawing.Point(4, 22);
            this._generalTabPage.Name = "_generalTabPage";
            this._generalTabPage.Size = new System.Drawing.Size(491, 319);
            this._generalTabPage.TabIndex = 0;
            this._generalTabPage.Text = "general";
            // 
            // _groupBoxVariables
            // 
            this._groupBoxVariables.Controls.Add(this._lblNOV);
            this._groupBoxVariables.Controls.Add(this._nudNOV);
            this._groupBoxVariables.Location = new System.Drawing.Point(16, 157);
            this._groupBoxVariables.Name = "_groupBoxVariables";
            this._groupBoxVariables.Size = new System.Drawing.Size(464, 67);
            this._groupBoxVariables.TabIndex = 4;
            this._groupBoxVariables.TabStop = false;
            this._groupBoxVariables.Text = "Variables";
            // 
            // _lblNOV
            // 
            this._lblNOV.Location = new System.Drawing.Point(32, 30);
            this._lblNOV.Name = "_lblNOV";
            this._lblNOV.Size = new System.Drawing.Size(189, 23);
            this._lblNOV.TabIndex = 1;
            this._lblNOV.Text = "Number of variables to display";
            // 
            // _nudNOV
            // 
            this._nudNOV.Location = new System.Drawing.Point(230, 30);
            this._nudNOV.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._nudNOV.Name = "_nudNOV";
            this._nudNOV.Size = new System.Drawing.Size(48, 21);
            this._nudNOV.TabIndex = 0;
            this._nudNOV.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _OKBtn
            // 
            this._OKBtn.Location = new System.Drawing.Point(288, 284);
            this._OKBtn.Name = "_OKBtn";
            this._OKBtn.Size = new System.Drawing.Size(90, 25);
            this._OKBtn.TabIndex = 3;
            this._OKBtn.Text = "OK";
            // 
            // _CancelBtn
            // 
            this._CancelBtn.Location = new System.Drawing.Point(394, 284);
            this._CancelBtn.Name = "_CancelBtn";
            this._CancelBtn.Size = new System.Drawing.Size(90, 25);
            this._CancelBtn.TabIndex = 2;
            this._CancelBtn.Text = "Cancel";
            // 
            // _startupGroup
            // 
            this._startupGroup.Controls.Add(this._lastFileRadioBtn);
            this._startupGroup.Controls.Add(this._noFileRadioBtn);
            this._startupGroup.Controls.Add(this._textFileRadioBtn);
            this._startupGroup.Controls.Add(this._browseFileBtn);
            this._startupGroup.Controls.Add(this._startupFilename);
            this._startupGroup.Location = new System.Drawing.Point(19, 9);
            this._startupGroup.Name = "_startupGroup";
            this._startupGroup.Size = new System.Drawing.Size(461, 249);
            this._startupGroup.TabIndex = 1;
            this._startupGroup.TabStop = false;
            this._startupGroup.Text = "Startup";
            // 
            // _lastFileRadioBtn
            // 
            this._lastFileRadioBtn.Location = new System.Drawing.Point(19, 103);
            this._lastFileRadioBtn.Name = "_lastFileRadioBtn";
            this._lastFileRadioBtn.Size = new System.Drawing.Size(394, 26);
            this._lastFileRadioBtn.TabIndex = 7;
            this._lastFileRadioBtn.Text = "Remember last opened file next time the application starts";
            // 
            // _noFileRadioBtn
            // 
            this._noFileRadioBtn.Location = new System.Drawing.Point(19, 17);
            this._noFileRadioBtn.Name = "_noFileRadioBtn";
            this._noFileRadioBtn.Size = new System.Drawing.Size(250, 26);
            this._noFileRadioBtn.TabIndex = 6;
            this._noFileRadioBtn.Text = "Do not open any file on startup";
            // 
            // _textFileRadioBtn
            // 
            this._textFileRadioBtn.Location = new System.Drawing.Point(19, 43);
            this._textFileRadioBtn.Name = "_textFileRadioBtn";
            this._textFileRadioBtn.Size = new System.Drawing.Size(327, 17);
            this._textFileRadioBtn.TabIndex = 5;
            this._textFileRadioBtn.Text = "Always open the application with this file";
            // 
            // _browseFileBtn
            // 
            this._browseFileBtn.Location = new System.Drawing.Point(413, 69);
            this._browseFileBtn.Name = "_browseFileBtn";
            this._browseFileBtn.Size = new System.Drawing.Size(29, 26);
            this._browseFileBtn.TabIndex = 2;
            this._browseFileBtn.Text = "...";
            this._browseFileBtn.Click += new System.EventHandler(this.browseFileBtn_Click);
            // 
            // _startupFilename
            // 
            this._startupFilename.Location = new System.Drawing.Point(86, 69);
            this._startupFilename.Name = "_startupFilename";
            this._startupFilename.Size = new System.Drawing.Size(317, 21);
            this._startupFilename.TabIndex = 1;
            // 
            // OptionsForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(415, 316);
            this.Controls.Add(this._optionsTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsForm";
            this.Text = "OptionsForm";
            this._optionsTabControl.ResumeLayout(false);
            this._generalTabPage.ResumeLayout(false);
            this._groupBoxVariables.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._nudNOV)).EndInit();
            this._startupGroup.ResumeLayout(false);
            this._startupGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        private void readOptions()
        {
            try
            {
                FileStream stream;
                try
                {
                    stream = new FileStream("tsDirs/Config/ts.ini", FileMode.Open);
                }
                catch (FileNotFoundException)
                {
                    File.Copy("tsDirs/Spares/ts.ini", "tsDirs/Config/ts.ini");
                    File.SetAttributes("tsDirs/Config/ts.ini", FileAttributes.Normal);
                    stream = new FileStream("tsDirs/Config/ts.ini", FileMode.Open);
                }
                StreamReader reader = new StreamReader(stream);
                while (reader.Peek() != -1)
                {
                    string str2;
                    string str = reader.ReadLine();
                    string[] strArray = str.Split(" ".ToCharArray());
                    if (((str2 = strArray[0]) != null) && (str2 != "[startup]"))
                    {
                        if (str2 == "startupOption")
                        {
                            this.selectRadio(Convert.ToInt16(strArray[2]));
                        }
                        else
                        {
                            if (str2 == "startupFilename")
                            {
                                this._startupFilename.Text = str.Remove(0, str.IndexOf(strArray[2]));
                                continue;
                            }
                            if (str2 == "lastOpenedFilename")
                            {
                                this._lastFilename = str.Remove(0, str.IndexOf(strArray[2]));
                                continue;
                            }
                            if (str2 == "NOV")
                            {
                                this.NOV = Convert.ToInt16(str.Remove(0, str.IndexOf(strArray[2])));
                                continue;
                            }
                            if (str2 == "tsTitle")
                            {
                                this._tsTitle = str.Remove(0, str.IndexOf(strArray[2]));
                            }
                        }
                    }
                }
                if (this.NOV <= 0)
                {
                    this.NOV = 1;
                }
                reader.Close();
                stream.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("OptionsForm: CAUGHT Exception " + exception);
                Console.WriteLine("{0}", exception.Message);
            }
        }

        private void selectRadio(int i)
        {
            switch (i)
            {
                case 1:
                    this._noFileRadioBtn.Checked = true;
                    break;

                case 2:
                    this._textFileRadioBtn.Checked = true;
                    break;

                case 3:
                    this._lastFileRadioBtn.Checked = true;
                    break;
            }
        }

        private void startupRadioCheckChanged(object obj, EventArgs ea)
        {
            if (!this._textFileRadioBtn.Checked)
            {
                this._browseFileBtn.Enabled = false;
                this._startupFilename.Enabled = false;
            }
            else
            {
                this._browseFileBtn.Enabled = true;
                this._startupFilename.Enabled = true;
            }
        }

        public void writeOptions()
        {
            try
            {
                Directory.SetCurrentDirectory(SharedData.BaseDir);
                FileStream stream = new FileStream("tsDirs/Config/ts.ini", FileMode.Create);
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine("[startup]");
                writer.WriteLine("startupOption = {0}", this.getCheckedRadioIndex());
                writer.WriteLine("startupFilename = {0}", this._startupFilename.Text);
                writer.WriteLine("lastOpenedFilename = {0}", this._lastFilename);
                writer.WriteLine("NOV = {0}", this.NOV);
                writer.Close();
                stream.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("OptionsForm: CAUGHT FileFormatException " + exception);
                Console.WriteLine("{0}", exception.Message);
            }
        }

        public int NOV
        {
            get
            {
                return (int) this._nudNOV.Value;
            }
            set
            {
                this._nudNOV.Value = value;
            }
        }

        public string TsTitle
        {
            get
            {
                return this._tsTitle;
            }
        }
    }
}

