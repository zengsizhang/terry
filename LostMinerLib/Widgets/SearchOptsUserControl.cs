namespace TimeSearcher.Widgets
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher.Search;

    public class SearchOptsUserControl : UserControl
    {
        private ComboBox _algoComboBox;
        private AutoSearcher _autoSearcher;
        private CheckBox _chkAmplitude;
        private CheckBox _chkAutoSearch;
        private CheckBox _chkLinearTrend;
        private CheckBox _chkNoise;
        private CheckBox _chkOffset;
        private Label _lblAlgo;
        private SearchOptions _searchOpts;
        private GroupBox _selectionsGroupBox;
        private Container components;

        public SearchOptsUserControl(SearchOptions searchOpts, AutoSearcher autoSearcher)
        {
            this.InitializeComponent();
            this._searchOpts = searchOpts;
            this._autoSearcher = autoSearcher;
            this.myInit();
        }

        private void _algoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._searchOpts.setAlgo(this.algoComboIndexToAlgo(this._algoComboBox.SelectedIndex));
            this._autoSearcher.autoSearch(this._chkAutoSearch.Checked);
        }

        private void _chkAmplitude_CheckedChanged(object sender, EventArgs e)
        {
            bool cond = ((CheckBox) sender).Checked;
            this._searchOpts.setAS(cond);
            this._autoSearcher.autoSearch(this._chkAutoSearch.Checked);
        }

        private void _chkAutoSearch_CheckedChanged(object sender, EventArgs e)
        {
            this._autoSearcher.autoSearch(this._chkAutoSearch.Checked);
        }

        private void _chkLinearTrend_CheckedChanged(object sender, EventArgs e)
        {
            bool cond = ((CheckBox) sender).Checked;
            this._searchOpts.setLT(cond);
            this._autoSearcher.autoSearch(this._chkAutoSearch.Checked);
        }

        private void _chkNoise_CheckedChanged(object sender, EventArgs e)
        {
            bool cond = ((CheckBox) sender).Checked;
            this._searchOpts.setNR(cond);
            this._autoSearcher.autoSearch(this._chkAutoSearch.Checked);
        }

        private void _chkOffset_CheckedChanged(object sender, EventArgs e)
        {
            bool cond = ((CheckBox) sender).Checked;
            this._searchOpts.setOT(cond);
            this._autoSearcher.autoSearch(this._chkAutoSearch.Checked);
        }

        private SearchAlgo.Algo algoComboIndexToAlgo(int index)
        {
            SearchAlgo.Algo none = SearchAlgo.Algo.None;
            switch (index)
            {
                case 0:
                    return SearchAlgo.Algo.Envelope;

                case 1:
                    return SearchAlgo.Algo.Euclidean;
            }
            return none;
        }

        private int algoToAlgoComboIndex(SearchAlgo.Algo algo)
        {
            switch (algo)
            {
                case SearchAlgo.Algo.Envelope:
                    return 0;

                case SearchAlgo.Algo.Euclidean:
                    return 1;
            }
            return 0;
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
            this._selectionsGroupBox = new GroupBox();
            this._chkNoise = new CheckBox();
            this._chkAmplitude = new CheckBox();
            this._chkOffset = new CheckBox();
            this._chkLinearTrend = new CheckBox();
            this._lblAlgo = new Label();
            this._algoComboBox = new ComboBox();
            this._chkAutoSearch = new CheckBox();
            this._selectionsGroupBox.SuspendLayout();
            base.SuspendLayout();
            this._selectionsGroupBox.Controls.Add(this._chkNoise);
            this._selectionsGroupBox.Controls.Add(this._chkAmplitude);
            this._selectionsGroupBox.Controls.Add(this._chkOffset);
            this._selectionsGroupBox.Controls.Add(this._chkLinearTrend);
            this._selectionsGroupBox.Location = new Point(0x10, 0x10);
            this._selectionsGroupBox.Name = "_selectionsGroupBox";
            this._selectionsGroupBox.Size = new Size(0xc0, 160);
            this._selectionsGroupBox.TabIndex = 5;
            this._selectionsGroupBox.TabStop = false;
            this._selectionsGroupBox.Text = "Transformations to be applied";
            this._chkNoise.Location = new Point(0x18, 0x74);
            this._chkNoise.Name = "_chkNoise";
            this._chkNoise.Size = new Size(0x88, 0x18);
            this._chkNoise.TabIndex = 7;
            this._chkNoise.Text = "Noise Reduction";
            this._chkNoise.CheckedChanged += new EventHandler(this._chkNoise_CheckedChanged);
            this._chkAmplitude.Location = new Point(0x18, 0x54);
            this._chkAmplitude.Name = "_chkAmplitude";
            this._chkAmplitude.Size = new Size(0xe8, 0x18);
            this._chkAmplitude.TabIndex = 6;
            this._chkAmplitude.Text = "Amplitude Scaling";
            this._chkAmplitude.CheckedChanged += new EventHandler(this._chkAmplitude_CheckedChanged);
            this._chkOffset.Location = new Point(0x18, 0x34);
            this._chkOffset.Name = "_chkOffset";
            this._chkOffset.Size = new Size(0xe8, 0x18);
            this._chkOffset.TabIndex = 5;
            this._chkOffset.Text = "Offset Translation";
            this._chkOffset.CheckedChanged += new EventHandler(this._chkOffset_CheckedChanged);
            this._chkLinearTrend.Location = new Point(0x18, 20);
            this._chkLinearTrend.Name = "_chkLinearTrend";
            this._chkLinearTrend.Size = new Size(0x88, 0x18);
            this._chkLinearTrend.TabIndex = 4;
            this._chkLinearTrend.Text = "Linear Trend Removal";
            this._chkLinearTrend.CheckedChanged += new EventHandler(this._chkLinearTrend_CheckedChanged);
            this._lblAlgo.Location = new Point(0x20, 200);
            this._lblAlgo.Name = "_lblAlgo";
            this._lblAlgo.Size = new Size(0x38, 0x10);
            this._lblAlgo.TabIndex = 10;
            this._lblAlgo.Text = "Algorithm:";
            this._algoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this._algoComboBox.ImeMode = ImeMode.On;
            this._algoComboBox.ItemHeight = 13;
            this._algoComboBox.Items.AddRange(new object[] { "Envelope", "Euclidean" });
            this._algoComboBox.Location = new Point(0x68, 200);
            this._algoComboBox.Name = "_algoComboBox";
            this._algoComboBox.Size = new Size(0x60, 0x15);
            this._algoComboBox.TabIndex = 9;
            this._algoComboBox.SelectedIndexChanged += new EventHandler(this._algoComboBox_SelectedIndexChanged);
            this._chkAutoSearch.Checked = true;
            this._chkAutoSearch.CheckState = CheckState.Checked;
            this._chkAutoSearch.Location = new Point(40, 0xe8);
            this._chkAutoSearch.Name = "_chkAutoSearch";
            this._chkAutoSearch.Size = new Size(0xa8, 0x20);
            this._chkAutoSearch.TabIndex = 8;
            this._chkAutoSearch.Text = "Perform search again if any selection above changes";
            this._chkAutoSearch.CheckedChanged += new EventHandler(this._chkAutoSearch_CheckedChanged);
            base.Controls.Add(this._lblAlgo);
            base.Controls.Add(this._algoComboBox);
            base.Controls.Add(this._chkAutoSearch);
            base.Controls.Add(this._selectionsGroupBox);
            base.Name = "SearchOptsUserControl";
            base.Size = new Size(0xe8, 280);
            this._selectionsGroupBox.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void installEvents()
        {
            this._chkLinearTrend.CheckedChanged += new EventHandler(this._chkLinearTrend_CheckedChanged);
            this._chkOffset.CheckedChanged += new EventHandler(this._chkOffset_CheckedChanged);
            this._chkAmplitude.CheckedChanged += new EventHandler(this._chkAmplitude_CheckedChanged);
            this._chkNoise.CheckedChanged += new EventHandler(this._chkNoise_CheckedChanged);
            this._algoComboBox.SelectedIndexChanged += new EventHandler(this._algoComboBox_SelectedIndexChanged);
            this._chkAutoSearch.CheckedChanged += new EventHandler(this._chkAutoSearch_CheckedChanged);
        }

        private void myInit()
        {
            SearchOptionsView view = this._searchOpts.getView();
            this.uninstallEvents();
            this._chkLinearTrend.Checked = view.hasLinearT();
            this._chkOffset.Checked = view.hasOffsetT();
            this._chkAmplitude.Checked = view.hasAmplitudeS();
            this._chkNoise.Checked = view.hasNoiseR();
            this._algoComboBox.SelectedIndex = this.algoToAlgoComboIndex(view.getAlgo());
            this._chkAutoSearch.Checked = view.hasAutoSearch();
            this.installEvents();
        }

        private void uninstallEvents()
        {
            this._chkLinearTrend.CheckedChanged -= new EventHandler(this._chkLinearTrend_CheckedChanged);
            this._chkOffset.CheckedChanged -= new EventHandler(this._chkOffset_CheckedChanged);
            this._chkAmplitude.CheckedChanged -= new EventHandler(this._chkAmplitude_CheckedChanged);
            this._chkNoise.CheckedChanged -= new EventHandler(this._chkNoise_CheckedChanged);
            this._algoComboBox.SelectedIndexChanged -= new EventHandler(this._algoComboBox_SelectedIndexChanged);
            this._chkAutoSearch.CheckedChanged -= new EventHandler(this._chkAutoSearch_CheckedChanged);
        }
    }
}

