namespace TimeSearcher.Panels
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Domain;
    using TimeSearcher.Search;

    public class OverviewVariablePanel : Panel
    {
        private Button _btnLeftEdge;
        private Button _btnRightEdge;
        private ComboBox _cbList;
        private Panel _choicePanel;
        private const int _comboHeight = 0x19;
        private const int _comboWidth = 200;
        private readonly int _edgeIndexCount;
        private bool _isShowingEdge;
        private readonly int _lastIndex;
        private int _myIndex;
        private OverviewPanel _overviewPanel;
        private OverviewPanel[] _overviewPanels;
        private TimeSearcherForm _tsForm;

        public OverviewVariablePanel(TimeSearcherForm frm, int varIdx, OverviewPanel[] overviewPanels, DataSet dataSet, bool logChecked)
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            string[] varNames = dataSet.getDynVarNames(logChecked);
            this._isShowingEdge = false;
            this._lastIndex = dataSet.NumTimePoints - 1;
            if (dataSet.StaticVar.TypeVal == StaticVariable.Type.BipolarDateTime)
            {
                this._edgeIndexCount = ((UniformBipolarDateTimeDomain) dataSet.StaticVar.Domain).PoleIndexCount;
                this.putEdgeButtons();
            }
            else
            {
                this._edgeIndexCount = 0;
            }
            this._choicePanel = new Panel();
            this._choicePanel.Dock = DockStyle.Top;
            this._choicePanel.Height = 0x19;
            this._choicePanel.BackColor = Color.White;
            this._cbList = new ComboBox();
            this._cbList.DropDownStyle = ComboBoxStyle.DropDownList;
            this._cbList.Width = 200;
            this._cbList.Dock = DockStyle.Left;
            this._cbList.Height = 0x19;
            this._cbList.Width = 200;
            this._cbList.Visible = true;
            this._tsForm = frm;
            this._myIndex = varIdx;
            this._overviewPanels = overviewPanels;
            this.setOverviewPanel(overviewPanels[this._myIndex]);
            this.populateList(varNames);
            this._cbList.SelectedIndexChanged += new EventHandler(this.variableChanged);
            base.Resize += new EventHandler(this.OnResize);
        }

        private void _btnLeftEdge_Click(object sender, EventArgs e)
        {
            this._isShowingEdge = !this._isShowingEdge;
            if (this._isShowingEdge)
            {
                this._overviewPanel.setStartEndIndexOfOverview(0, this._edgeIndexCount);
                this._btnLeftEdge.Text = ">";
                this._btnRightEdge.Visible = false;
            }
            else
            {
                this._overviewPanel.setStartEndIndexOfOverview(0, this._lastIndex);
                this._btnLeftEdge.Text = "<";
                this._btnRightEdge.Visible = true;
            }
        }

        private void _btnRightEdge_Click(object sender, EventArgs e)
        {
            this._isShowingEdge = !this._isShowingEdge;
            if (this._isShowingEdge)
            {
                this._overviewPanel.setStartEndIndexOfOverview(this._lastIndex - this._edgeIndexCount, this._lastIndex);
                this._btnRightEdge.Text = "<";
                this._btnLeftEdge.Visible = false;
            }
            else
            {
                this._overviewPanel.setStartEndIndexOfOverview(0, this._lastIndex);
                this._btnRightEdge.Text = ">";
                this._btnLeftEdge.Visible = true;
            }
        }

        public void clearResults(int varIdx)
        {
            this._overviewPanels[varIdx].clearResults();
        }

        public ComboBox getCombo()
        {
            return this._cbList;
        }

        private void InitializeComponent()
        {
            this._btnLeftEdge = new Button();
            this._btnRightEdge = new Button();
            this._btnLeftEdge.Location = new Point(0x11, 0x11);
            this._btnLeftEdge.Name = "_btnLeftEdge";
            this._btnLeftEdge.Size = new Size(0x12, 0x12);
            this._btnLeftEdge.TabIndex = 0;
            this._btnLeftEdge.Text = "<";
            this._btnLeftEdge.Click += new EventHandler(this._btnLeftEdge_Click);
            this._btnRightEdge.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this._btnRightEdge.Location = new Point(140, 14);
            this._btnRightEdge.Name = "_btnRightEdge";
            this._btnRightEdge.Size = new Size(0x12, 0x12);
            this._btnRightEdge.TabIndex = 0;
            this._btnRightEdge.Text = ">";
            this._btnRightEdge.Click += new EventHandler(this._btnRightEdge_Click);
        }

        public void invalidateOverview()
        {
            this._overviewPanel.Invalidate();
        }

        private void locateButtons()
        {
            if ((this._btnLeftEdge != null) && (this._btnRightEdge != null))
            {
                this._btnLeftEdge.Location = new Point(0, base.Height - this._btnLeftEdge.Height);
                this._btnRightEdge.Location = new Point(base.Width - this._btnRightEdge.Width, base.Height - this._btnRightEdge.Height);
            }
        }

        private void OnResize(object obj, EventArgs ea)
        {
            this.setOverviewPanelSize();
            this.locateButtons();
        }

        private void populateList(string[] varNames)
        {
            for (int i = 0; i < varNames.Length; i++)
            {
                this._cbList.Items.Add(varNames[i]);
            }
            this._cbList.SelectedIndex = this._myIndex;
        }

        private void putEdgeButtons()
        {
            this.InitializeComponent();
            base.Controls.Add(this._btnLeftEdge);
            base.Controls.Add(this._btnRightEdge);
        }

        private void setOverviewPanel(OverviewPanel qp)
        {
            if (this._overviewPanel != null)
            {
                this._overviewPanel.Parent = null;
            }
            this._overviewPanel = qp;
            this._overviewPanel.Parent = this;
            this.setOverviewPanelSize();
        }

        private void setOverviewPanelSize()
        {
            if (this._overviewPanel != null)
            {
                this._overviewPanel.SetBounds(base.ClientRectangle.Left, base.ClientRectangle.Top, base.ClientRectangle.Width, base.ClientRectangle.Height);
            }
        }

        public void setResults(SearchResults searchResults)
        {
            this._overviewPanels[searchResults.getVarIndex()].setResults(searchResults);
        }

        private void variableChanged(object sender, EventArgs ea)
        {
            this.setOverviewPanel(this._overviewPanels[this._cbList.SelectedIndex]);
            this.setOverviewPanel(this._overviewPanel);
        }
    }
}

