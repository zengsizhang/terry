namespace TimeSearcher.Search
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Filters;
    using TimeSearcher.Widgets;

    public class MultiVarResSearchForm : Form, AutoSearcher
    {
        private Button _browseFileBtn;
        private ComboBox _comboEndIdx;
        private ComboBox _comboStartIdx;
        private RadioButton _fromFileRdBtn;
        private Label _lblEnd;
        private Label _lblExplanation;
        private Label _lblExplanation2;
        private Label _lblLoadedFile;
        private Label _lblPerc;
        private Label _lblResults;
        private Label _lblStart;
        private Label _lblTolerance;
        private MultiVarResSearch _mvrs;
        private static MultiVarResSearchForm _mvrsForm;
        private TabControl _mvrsTabControl;
        private NumericUpDown _nudPerc;
        private OpenFileDialog _openTxtFileDlg;
        private double[][] _patterns;
        private double[][] _patternsFromFile;
        private int _patternsFromFileLength;
        private int _patternsLength;
        private SearchOptions _searchOpts;
        private TabPage _searchOptsTabPage;
        private SearchOptsUserControl _searchOptsUC;
        private TabPage _searchTabPage;
        private RadioButton _selectedItemRdBtn;
        private TabPage _selectVarsTabPage;
        private GroupBox _sourceGrpBox;
        private TabPage _sourceTabPage;
        private StatusBar _statusBar;
        private readonly TimeSearcherForm _tsForm;
        private UnmatchedEntity _unmatchedEntity;
        private MultiCheckUserControl _varChecks;
        private Container components;

        public MultiVarResSearchForm(TimeSearcherForm tsForm)
        {
            this.InitializeComponent();
            this._tsForm = tsForm;
            this.myInit();
        }

        private void _browseFileBtn_Click(object sender, EventArgs e)
        {
            if (Utils.SafeShowDialog(this._openTxtFileDlg) == DialogResult.OK)
            {
                this._lblLoadedFile.Text = Utils.GetFileNameOnly(this._openTxtFileDlg.FileName);
                this._patternsFromFile = this.loadIncompleteTs(this._openTxtFileDlg.FileName, out this._patternsFromFileLength);
                if (this._fromFileRdBtn.Checked)
                {
                    this.updatePatterns(this._patternsFromFile, this._patternsFromFileLength);
                }
            }
        }

        private void _comboEndIdx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._comboEndIdx.SelectedIndex <= this._comboStartIdx.SelectedIndex)
            {
                this._comboEndIdx.SelectedIndex = this._comboStartIdx.SelectedIndex + 1;
                this._statusBar.Text = "End time point cannot be less than start time point!";
            }
            else
            {
                this._statusBar.Text = "";
            }
            this.updateMVRS();
            this.performSearch();
        }

        private void _comboStartIdx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._comboStartIdx.SelectedIndex >= this._comboEndIdx.SelectedIndex)
            {
                this._comboStartIdx.SelectedIndex = this._comboEndIdx.SelectedIndex - 1;
                this._statusBar.Text = "Start time point cannot be greater than end time point!";
            }
            else
            {
                this._statusBar.Text = "";
            }
            this.updateMVRS();
            this.performSearch();
        }

        private void _fromFileRdBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (this._fromFileRdBtn.Checked)
            {
                if (this._patternsFromFile == null)
                {
                    if (Utils.SafeShowDialog(this._openTxtFileDlg) != DialogResult.OK)
                    {
                        this._selectedItemRdBtn.Checked = true;
                        return;
                    }
                    this._lblLoadedFile.Text = Utils.GetFileNameOnly(this._openTxtFileDlg.FileName);
                    this._patternsFromFile = this.loadIncompleteTs(this._openTxtFileDlg.FileName, out this._patternsFromFileLength);
                }
                this.updatePatterns(this._patternsFromFile, this._patternsFromFileLength);
            }
        }

        private void _nudPerc_ValueChanged(object sender, EventArgs e)
        {
            this.performSearch();
        }

        private void _selectedItemRdBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (this._selectedItemRdBtn.Checked)
            {
                int itemIdx = this._tsForm.DataSet.GetSelectedAndEnabledItemIdx()[0];
                double[][] patternsFromItemIdx = MultiVarResSearch.GetPatternsFromItemIdx(this._tsForm.DataSet, itemIdx);
                this.updatePatterns(patternsFromItemIdx, patternsFromItemIdx[0].Length);
            }
        }

        private void _varChecks_ChecksChanged(MultiCheckUserControl sourceMCUC)
        {
            this.autoSearch(true);
        }

        public void autoSearch(bool isAutoSearchChecked)
        {
            if (isAutoSearchChecked)
            {
                this.performSearch();
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

        private DataItem GetIncompleteItem(double[][] patterns)
        {
            return new DataItem("incomplete", this._tsForm.DataSet.NumItems + 1, patterns, this._tsForm.DataSet.getDynVarNames(false));
        }

        private void InitializeComponent()
        {
            this._mvrsTabControl = new System.Windows.Forms.TabControl();
            this._sourceTabPage = new System.Windows.Forms.TabPage();
            this._sourceGrpBox = new System.Windows.Forms.GroupBox();
            this._lblLoadedFile = new System.Windows.Forms.Label();
            this._browseFileBtn = new System.Windows.Forms.Button();
            this._fromFileRdBtn = new System.Windows.Forms.RadioButton();
            this._selectedItemRdBtn = new System.Windows.Forms.RadioButton();
            this._searchTabPage = new System.Windows.Forms.TabPage();
            this._statusBar = new System.Windows.Forms.StatusBar();
            this._lblExplanation2 = new System.Windows.Forms.Label();
            this._lblExplanation = new System.Windows.Forms.Label();
            this._lblResults = new System.Windows.Forms.Label();
            this._lblPerc = new System.Windows.Forms.Label();
            this._lblTolerance = new System.Windows.Forms.Label();
            this._lblEnd = new System.Windows.Forms.Label();
            this._lblStart = new System.Windows.Forms.Label();
            this._comboEndIdx = new System.Windows.Forms.ComboBox();
            this._comboStartIdx = new System.Windows.Forms.ComboBox();
            this._nudPerc = new System.Windows.Forms.NumericUpDown();
            this._searchOptsTabPage = new System.Windows.Forms.TabPage();
            this._selectVarsTabPage = new System.Windows.Forms.TabPage();
            this._openTxtFileDlg = new System.Windows.Forms.OpenFileDialog();
            this._mvrsTabControl.SuspendLayout();
            this._sourceTabPage.SuspendLayout();
            this._sourceGrpBox.SuspendLayout();
            this._searchTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._nudPerc)).BeginInit();
            this.SuspendLayout();
            // 
            // _mvrsTabControl
            // 
            this._mvrsTabControl.Controls.Add(this._sourceTabPage);
            this._mvrsTabControl.Controls.Add(this._searchTabPage);
            this._mvrsTabControl.Controls.Add(this._searchOptsTabPage);
            this._mvrsTabControl.Controls.Add(this._selectVarsTabPage);
            this._mvrsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mvrsTabControl.Location = new System.Drawing.Point(0, 0);
            this._mvrsTabControl.Name = "_mvrsTabControl";
            this._mvrsTabControl.SelectedIndex = 0;
            this._mvrsTabControl.Size = new System.Drawing.Size(320, 310);
            this._mvrsTabControl.TabIndex = 0;
            // 
            // _sourceTabPage
            // 
            this._sourceTabPage.Controls.Add(this._sourceGrpBox);
            this._sourceTabPage.Location = new System.Drawing.Point(4, 22);
            this._sourceTabPage.Name = "_sourceTabPage";
            this._sourceTabPage.Size = new System.Drawing.Size(312, 284);
            this._sourceTabPage.TabIndex = 0;
            this._sourceTabPage.Text = "Source";
            // 
            // _sourceGrpBox
            // 
            this._sourceGrpBox.Controls.Add(this._lblLoadedFile);
            this._sourceGrpBox.Controls.Add(this._browseFileBtn);
            this._sourceGrpBox.Controls.Add(this._fromFileRdBtn);
            this._sourceGrpBox.Controls.Add(this._selectedItemRdBtn);
            this._sourceGrpBox.Location = new System.Drawing.Point(29, 43);
            this._sourceGrpBox.Name = "_sourceGrpBox";
            this._sourceGrpBox.Size = new System.Drawing.Size(326, 198);
            this._sourceGrpBox.TabIndex = 0;
            this._sourceGrpBox.TabStop = false;
            this._sourceGrpBox.Text = "Search Pattern Source";
            // 
            // _lblLoadedFile
            // 
            this._lblLoadedFile.Location = new System.Drawing.Point(58, 121);
            this._lblLoadedFile.Name = "_lblLoadedFile";
            this._lblLoadedFile.Size = new System.Drawing.Size(240, 25);
            this._lblLoadedFile.TabIndex = 4;
            this._lblLoadedFile.Text = "<none>";
            this._lblLoadedFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _browseFileBtn
            // 
            this._browseFileBtn.Location = new System.Drawing.Point(115, 86);
            this._browseFileBtn.Name = "_browseFileBtn";
            this._browseFileBtn.Size = new System.Drawing.Size(29, 26);
            this._browseFileBtn.TabIndex = 3;
            this._browseFileBtn.Text = "...";
            this._browseFileBtn.Click += new System.EventHandler(this._browseFileBtn_Click);
            // 
            // _fromFileRdBtn
            // 
            this._fromFileRdBtn.Location = new System.Drawing.Point(29, 86);
            this._fromFileRdBtn.Name = "_fromFileRdBtn";
            this._fromFileRdBtn.Size = new System.Drawing.Size(96, 26);
            this._fromFileRdBtn.TabIndex = 1;
            this._fromFileRdBtn.Text = "From File:";
            this._fromFileRdBtn.CheckedChanged += new System.EventHandler(this._fromFileRdBtn_CheckedChanged);
            // 
            // _selectedItemRdBtn
            // 
            this._selectedItemRdBtn.Location = new System.Drawing.Point(29, 43);
            this._selectedItemRdBtn.Name = "_selectedItemRdBtn";
            this._selectedItemRdBtn.Size = new System.Drawing.Size(115, 26);
            this._selectedItemRdBtn.TabIndex = 0;
            this._selectedItemRdBtn.Text = "Selected item";
            this._selectedItemRdBtn.CheckedChanged += new System.EventHandler(this._selectedItemRdBtn_CheckedChanged);
            // 
            // _searchTabPage
            // 
            this._searchTabPage.Controls.Add(this._statusBar);
            this._searchTabPage.Controls.Add(this._lblExplanation2);
            this._searchTabPage.Controls.Add(this._lblExplanation);
            this._searchTabPage.Controls.Add(this._lblResults);
            this._searchTabPage.Controls.Add(this._lblPerc);
            this._searchTabPage.Controls.Add(this._lblTolerance);
            this._searchTabPage.Controls.Add(this._lblEnd);
            this._searchTabPage.Controls.Add(this._lblStart);
            this._searchTabPage.Controls.Add(this._comboEndIdx);
            this._searchTabPage.Controls.Add(this._comboStartIdx);
            this._searchTabPage.Controls.Add(this._nudPerc);
            this._searchTabPage.Location = new System.Drawing.Point(4, 22);
            this._searchTabPage.Name = "_searchTabPage";
            this._searchTabPage.Size = new System.Drawing.Size(312, 284);
            this._searchTabPage.TabIndex = 1;
            this._searchTabPage.Text = "Search";
            // 
            // _statusBar
            // 
            this._statusBar.Location = new System.Drawing.Point(0, 260);
            this._statusBar.Name = "_statusBar";
            this._statusBar.Size = new System.Drawing.Size(312, 24);
            this._statusBar.TabIndex = 21;
            // 
            // _lblExplanation2
            // 
            this._lblExplanation2.Location = new System.Drawing.Point(48, 241);
            this._lblExplanation2.Name = "_lblExplanation2";
            this._lblExplanation2.Size = new System.Drawing.Size(298, 35);
            this._lblExplanation2.TabIndex = 20;
            this._lblExplanation2.Text = "In River Plot view, the matched period is brown, the past is black, and the forec" +
                "ast is red.";
            // 
            // _lblExplanation
            // 
            this._lblExplanation.Location = new System.Drawing.Point(202, 112);
            this._lblExplanation.Name = "_lblExplanation";
            this._lblExplanation.Size = new System.Drawing.Size(163, 43);
            this._lblExplanation.TabIndex = 19;
            this._lblExplanation.Text = "(Results are updated when tolerance changes.)";
            // 
            // _lblResults
            // 
            this._lblResults.Location = new System.Drawing.Point(48, 172);
            this._lblResults.Name = "_lblResults";
            this._lblResults.Size = new System.Drawing.Size(250, 52);
            this._lblResults.TabIndex = 18;
            // 
            // _lblPerc
            // 
            this._lblPerc.Location = new System.Drawing.Point(163, 121);
            this._lblPerc.Name = "_lblPerc";
            this._lblPerc.Size = new System.Drawing.Size(19, 17);
            this._lblPerc.TabIndex = 17;
            this._lblPerc.Text = "%";
            // 
            // _lblTolerance
            // 
            this._lblTolerance.Location = new System.Drawing.Point(19, 121);
            this._lblTolerance.Name = "_lblTolerance";
            this._lblTolerance.Size = new System.Drawing.Size(77, 17);
            this._lblTolerance.TabIndex = 16;
            this._lblTolerance.Text = "Tolerance ";
            // 
            // _lblEnd
            // 
            this._lblEnd.Location = new System.Drawing.Point(182, 17);
            this._lblEnd.Name = "_lblEnd";
            this._lblEnd.Size = new System.Drawing.Size(120, 17);
            this._lblEnd.TabIndex = 15;
            this._lblEnd.Text = "End time point";
            // 
            // _lblStart
            // 
            this._lblStart.Location = new System.Drawing.Point(29, 17);
            this._lblStart.Name = "_lblStart";
            this._lblStart.Size = new System.Drawing.Size(120, 17);
            this._lblStart.TabIndex = 14;
            this._lblStart.Text = "Start time point";
            // 
            // _comboEndIdx
            // 
            this._comboEndIdx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboEndIdx.Location = new System.Drawing.Point(182, 43);
            this._comboEndIdx.Name = "_comboEndIdx";
            this._comboEndIdx.Size = new System.Drawing.Size(146, 20);
            this._comboEndIdx.TabIndex = 13;
            // 
            // _comboStartIdx
            // 
            this._comboStartIdx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboStartIdx.Location = new System.Drawing.Point(19, 43);
            this._comboStartIdx.Name = "_comboStartIdx";
            this._comboStartIdx.Size = new System.Drawing.Size(145, 20);
            this._comboStartIdx.TabIndex = 12;
            // 
            // _nudPerc
            // 
            this._nudPerc.Location = new System.Drawing.Point(96, 121);
            this._nudPerc.Name = "_nudPerc";
            this._nudPerc.Size = new System.Drawing.Size(58, 21);
            this._nudPerc.TabIndex = 11;
            this._nudPerc.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._nudPerc.ValueChanged += new System.EventHandler(this._nudPerc_ValueChanged);
            // 
            // _searchOptsTabPage
            // 
            this._searchOptsTabPage.Location = new System.Drawing.Point(4, 22);
            this._searchOptsTabPage.Name = "_searchOptsTabPage";
            this._searchOptsTabPage.Size = new System.Drawing.Size(312, 284);
            this._searchOptsTabPage.TabIndex = 2;
            this._searchOptsTabPage.Text = "Search Options";
            // 
            // _selectVarsTabPage
            // 
            this._selectVarsTabPage.AutoScroll = true;
            this._selectVarsTabPage.Location = new System.Drawing.Point(4, 22);
            this._selectVarsTabPage.Name = "_selectVarsTabPage";
            this._selectVarsTabPage.Size = new System.Drawing.Size(312, 284);
            this._selectVarsTabPage.TabIndex = 3;
            this._selectVarsTabPage.Text = "Selected Variables";
            // 
            // _openTxtFileDlg
            // 
            this._openTxtFileDlg.Filter = ".txt files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // MultiVarResSearchForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(320, 310);
            this.Controls.Add(this._mvrsTabControl);
            this.Name = "MultiVarResSearchForm";
            this.Text = "Multi-Variate Restricted Search Form";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MultiVarResSearchForm_Closing);
            this.Load += new System.EventHandler(this.MultiVarResSearchForm_Load);
            this._mvrsTabControl.ResumeLayout(false);
            this._sourceTabPage.ResumeLayout(false);
            this._sourceGrpBox.ResumeLayout(false);
            this._searchTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._nudPerc)).EndInit();
            this.ResumeLayout(false);

        }

        private void installComboEvents()
        {
            this._comboStartIdx.SelectedIndexChanged += new EventHandler(this._comboStartIdx_SelectedIndexChanged);
            this._comboEndIdx.SelectedIndexChanged += new EventHandler(this._comboEndIdx_SelectedIndexChanged);
        }

        private double[][] loadIncompleteTs(string path, out int patLength)
        {
            string[] strArray2;
            StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open));
            double[][] numArray = new double[this._tsForm.DataSet.NumDynVar][];
            ArrayList list = new ArrayList();
            for (int i = 0; reader.Peek() > -1; i++)
            {
                list.Add(reader.ReadLine());
            }
            string[] strArray = (string[]) list.ToArray(typeof(string));
            int numTimePoints = this._tsForm.DataSet.NumTimePoints;
            int length = numTimePoints;
            if (strArray.Length == 1)
            {
                strArray2 = Utils.SplitCsvLine(strArray[0]);
                int numDynVar = this._tsForm.DataSet.NumDynVar;
                length = strArray2.Length / numDynVar;
                for (int j = 0; j < numDynVar; j++)
                {
                    numArray[j] = new double[numTimePoints];
                }
                for (int k = 0; k < length; k++)
                {
                    for (int n = 0; n < numDynVar; n++)
                    {
                        numArray[n][k] = Convert.ToDouble(strArray2[(numDynVar * k) + n]);
                    }
                }
                for (int m = length; m < numTimePoints; m++)
                {
                    for (int num9 = 0; num9 < numDynVar; num9++)
                    {
                        numArray[num9][m] = SharedData.missingValue;
                    }
                }
            }
            else
            {
                for (int num10 = 0; num10 < numArray.Length; num10++)
                {
                    string line = strArray[num10];
                    strArray2 = Utils.SplitCsvLine(line);
                    length = strArray2.Length;
                    numArray[num10] = new double[numTimePoints];
                    for (int num11 = 0; num11 < strArray2.Length; num11++)
                    {
                        numArray[num10][num11] = Convert.ToDouble(strArray2[num11]);
                    }
                    for (int num12 = strArray2.Length; num12 < numTimePoints; num12++)
                    {
                        numArray[num10][num12] = SharedData.missingValue;
                    }
                }
            }
            reader.Close();
            patLength = length;
            return numArray;
        }

        private void MultiVarResSearchForm_Closing(object sender, CancelEventArgs e)
        {
            this._tsForm.RiverView.UpdateIncItem(this._tsForm.RiverView.GetRP(0).GetEmptyIncItem());
            this._tsForm.VarView.UpdateIncItem(this._tsForm.VarView.getQP(0).GetEmptyIncItem());
            _mvrsForm = null;
            if (this._unmatchedEntity != null)
            {
                this._unmatchedEntity.Dematerialize();
                this._tsForm.UpdateVisibleTabsDisplay();
            }
        }

        private void MultiVarResSearchForm_Load(object sender, EventArgs e)
        {
            _mvrsForm = this;
        }

        private void myInit()
        {
            this._searchOpts = MultiVarResSearch.GetDefaultSearchOpts();
            this._searchOptsUC = new SearchOptsUserControl(this._searchOpts, this);
            this._searchOptsUC.Dock = DockStyle.Fill;
            this._searchOptsTabPage.Controls.Add(this._searchOptsUC);
            this._varChecks = new MultiCheckUserControl(this._tsForm.DataSet.getDynVarNames(false));
            this._varChecks.Dock = DockStyle.Fill;
            this._varChecks.ChecksChanged += new MultiCheckUserControl.StatusChanged(this._varChecks_ChecksChanged);
            this._selectVarsTabPage.Controls.Add(this._varChecks);
            this.installComboEvents();
            this._selectedItemRdBtn.Checked = true;
        }

        private int patLen()
        {
            return (this._comboEndIdx.SelectedIndex - this._comboStartIdx.SelectedIndex);
        }

        private void performSearch()
        {
            if (this._unmatchedEntity != null)
            {
                this._unmatchedEntity.Dematerialize();
            }
            int[] numArray = this._mvrs.PerformSearch((int) this._nudPerc.Value, this._varChecks.CheckedIndex);
            ArrayList enabledItemIdxList = this._tsForm.DataSet.GetEnabledItemIdxList();
            for (int i = 0; i < numArray.Length; i++)
            {
                enabledItemIdxList.Remove(numArray[i]);
            }
            int[] excludedItemIdx = (int[]) enabledItemIdxList.ToArray(typeof(int));
            this._unmatchedEntity = new UnmatchedEntity(excludedItemIdx, this._tsForm.DataSet.DisablingManager);
            this._tsForm.DataSet.DisablingManager.AddEntity(this._unmatchedEntity);
            this._tsForm.UpdateVisibleTabsDisplay();
            this._tsForm.RiverView.Invalidate();
            this.updateResultsLabel();
            this._statusBar.Text = "";
        }

        private void uninstallComboEvents()
        {
            this._comboStartIdx.SelectedIndexChanged -= new EventHandler(this._comboStartIdx_SelectedIndexChanged);
            this._comboEndIdx.SelectedIndexChanged -= new EventHandler(this._comboEndIdx_SelectedIndexChanged);
        }

        private void updateMVRS()
        {
            this._mvrs = new MultiVarResSearch(this._tsForm.DataSet, this._searchOpts, this._patterns, this._comboStartIdx.SelectedIndex, this._comboEndIdx.SelectedIndex);
        }

        private void updatePatterns(double[][] patterns, int patternsLength)
        {
            this._patterns = patterns;
            this._patternsLength = patternsLength;
            this.updateStartEndCombo(patternsLength);
            this.updateMVRS();
            this._tsForm.RiverView.UpdateIncItem(this.GetIncompleteItem(this._patterns));
            this._tsForm.VarView.UpdateIncItem(this.GetIncompleteItem(this._patterns));
            this.performSearch();
        }

        private void updateResultsLabel()
        {
            this._lblResults.Text = string.Concat(new object[] { "Forecast results are based on ", this.patLen(), " time points and ", this._mvrs.MVRSInfo.HitCount, " similar time series." });
        }

        private void updateStartEndCombo(int shorterLen)
        {
            string[] destinationArray = new string[shorterLen];
            Array.Copy(this._tsForm.DataSet.TimePointNames, 0, destinationArray, 0, destinationArray.Length);
            this.uninstallComboEvents();
            this._comboStartIdx.Items.Clear();
            this._comboEndIdx.Items.Clear();
            this._comboStartIdx.Items.AddRange(destinationArray);
            this._comboEndIdx.Items.AddRange(destinationArray);
            this._comboEndIdx.SelectedIndex = destinationArray.Length - 1;
            this._comboStartIdx.SelectedIndex = 0;
            this.installComboEvents();
        }

        public static MultiVarResSearchForm MVRSFormInstance
        {
            get
            {
                return _mvrsForm;
            }
        }

        public MultiVarResSearchInfo MVRSInfo
        {
            get
            {
                return this._mvrs.MVRSInfo;
            }
        }
    }
}

