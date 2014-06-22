namespace TimeSearcher.Wizard
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Filters;
    using TimeSearcher.Search;

    public class TimePanel : UserControl
    {
        private RadioButton combinedView;
        private IContainer components;
        private ContextMenuStrip contextMenu;
        private FlowLayoutPanel currentPanel;
        private DataSet dataset;
        private List<ForecastRiverPanel> forecastPanels;
        private int id;
        private RadioButton riverView;
        private Label title;
        private TimePanelType type;
        private string[] variables;
        private RadioButton variableView;

        public TimePanel() : this("Default", Size.Empty, TimePanelType.Current)
        {
        }

        public TimePanel(string name, Size size, TimePanelType type)
        {
            this.id = -1;
            this.InitializeComponent();
            this.variables = WizardForm.TSForm.DataSet.getDynVarNames(WizardForm.TSForm.LogMenuItemChecked);
            this.title.Text = name;
            this.type = type;
            base.Size = size;
            this.currentPanel.Size = new Size(size.Width - 10, size.Height - 0x23);
            this.Container.ControlAdded += new ControlEventHandler(this.Container_ControlAdded);
            foreach (string str in this.variables)
            {
                this.contextMenu.Items.Add(new ToolStripMenuItem(str));
            }
            ((ToolStripMenuItem) this.contextMenu.Items[0]).Checked = true;
            this.contextMenu.Items.Add("-");
            this.contextMenu.Items.Add(new ToolStripMenuItem("Zoom"));
            this.contextMenu.Items.Add(new ToolStripMenuItem("Parameter Spectrum"));
            if ((type != TimePanelType.SearchSettings) && (type != TimePanelType.Variables))
            {
                this.combinedView.Visible = false;
            }
        }

        public void AddForecastPanel(Panel panel)
        {
            panel.BorderStyle = BorderStyle.None;
            this.currentPanel.Controls.Add(panel);
        }

        private void Container_ControlAdded(object sender, ControlEventArgs e)
        {
            ForecastRiverPanel control = e.Control as ForecastRiverPanel;
            switch (this.type)
            {
                case TimePanelType.SearchSettings:
                    if (control != null)
                    {
                        if (control.MvrsInfo.SearchOptions.hasAS())
                        {
                            this.title.Text = this.title.Text + "/ AS ";
                        }
                        if (control.MvrsInfo.SearchOptions.hasLT())
                        {
                            this.title.Text = this.title.Text + "/ LT ";
                        }
                        if (control.MvrsInfo.SearchOptions.hasOT())
                        {
                            this.title.Text = this.title.Text + "/ OT ";
                        }
                        if (control.MvrsInfo.SearchOptions.hasNR())
                        {
                            this.title.Text = this.title.Text + "/ NR ";
                        }
                    }
                    break;

                case TimePanelType.Variables:
                    if (control != null)
                    {
                        for (int i = 0; i < control.MvrsInfo.Variables.Length; i++)
                        {
                            this.title.Text = this.title.Text + "/" + this.variables[control.MvrsInfo.Variables[i]];
                        }
                    }
                    break;
            }
        }

        public void DeselectAll()
        {
            if ((this.type != TimePanelType.SearchSettings) && (this.type != TimePanelType.Variables))
            {
                foreach (TimePanel panel in WizardForm.Instance.Panel.Controls)
                {
                    foreach (ForecastRiverPanel panel2 in panel.Container.Controls)
                    {
                        panel2.Unselect();
                    }
                }
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

        public static MultiVarResSearch GetMVRS(int start, int end, SearchOptions so)
        {
            TimeSearcherForm tSForm = WizardForm.TSForm;
            int itemIdx = tSForm.DataSet.GetSelectedAndEnabledItemIdx()[0];
            return new MultiVarResSearch(tSForm.DataSet, so, MultiVarResSearch.GetPatternsFromItemIdx(tSForm.DataSet, itemIdx), start, end);
        }

        public static string GetNameFromType(TimePanelType type)
        {
            switch (type)
            {
                case TimePanelType.Current:
                    return "Current";

                case TimePanelType.Tolerance:
                    return "Tolerance Spectrum";

                case TimePanelType.Startpoint:
                    return "Start Point";

                case TimePanelType.Endpoint:
                    return "End Point";

                case TimePanelType.SearchSettings:
                    return "Search Settings";
            }
            return "Default";
        }

        public static void InitForecast(ForecastRiverPanel panel, SearchOptions so, int tolerance, int[] variables)
        {
            panel.Tolerance = tolerance;
            TimeSearcherForm tSForm = WizardForm.TSForm;
            MultiVarResSearch search = GetMVRS(panel.PatternStart, panel.PatternEnd, so);
            int[] numArray = search.PerformSearch(tolerance, variables);
            ArrayList enabledItemIdxList = tSForm.DataSet.GetEnabledItemIdxList();
            for (int i = 0; i < numArray.Length; i++)
            {
                enabledItemIdxList.Remove(numArray[i]);
            }
            int[] excludedItemIdx = (int[]) enabledItemIdxList.ToArray(typeof(int));
            panel.UnmatchedIndices = excludedItemIdx;
            panel.UnmatchedEntity = new UnmatchedEntity(excludedItemIdx, tSForm.DataSet.DisablingManager);
            tSForm.DataSet.DisablingManager.AddEntity(panel.UnmatchedEntity);
            DataSet statDataSet = tSForm.DataSet.GetStatDataSet();
            panel.UpdateStatDataSet(statDataSet);
            tSForm.DataSet.DisablingManager.DematerializeAllEntities();
            panel.MvrsInfo = search.MVRSInfo;
            panel.Invalidate();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.title = new Label();
            this.contextMenu = new ContextMenuStrip(this.components);
            this.currentPanel = new FlowLayoutPanel();
            this.variableView = new RadioButton();
            this.riverView = new RadioButton();
            this.combinedView = new RadioButton();
            base.SuspendLayout();
            this.title.AutoSize = true;
            this.title.Location = new Point(5, 2);
            this.title.Name = "title";
            this.title.Size = new Size(0x23, 0x11);
            this.title.TabIndex = 1;
            this.title.Text = "Title";
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new Size(0x3d, 4);
            this.currentPanel.Location = new Point(3, 0x16);
            this.currentPanel.Name = "currentPanel";
            this.currentPanel.Size = new Size(400, 0xcd);
            this.currentPanel.TabIndex = 5;
            this.variableView.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.variableView.AutoSize = true;
            this.variableView.Location = new Point(0xd1, -2);
            this.variableView.Name = "variableView";
            this.variableView.Size = new Size(0x72, 0x15);
            this.variableView.TabIndex = 7;
            this.variableView.Text = "Variable View";
            this.variableView.UseVisualStyleBackColor = true;
            this.variableView.CheckedChanged += new EventHandler(this.variableView_CheckedChanged);
            this.riverView.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.riverView.AutoSize = true;
            this.riverView.Checked = true;
            this.riverView.Location = new Point(0x71, -2);
            this.riverView.Name = "riverView";
            this.riverView.Size = new Size(90, 0x15);
            this.riverView.TabIndex = 6;
            this.riverView.TabStop = true;
            this.riverView.Text = "River Plot";
            this.riverView.UseVisualStyleBackColor = true;
            this.riverView.CheckedChanged += new EventHandler(this.riverView_CheckedChanged);
            this.combinedView.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.combinedView.AutoSize = true;
            this.combinedView.Location = new Point(0x13f, -2);
            this.combinedView.Name = "combinedView";
            this.combinedView.Size = new Size(0x5c, 0x15);
            this.combinedView.TabIndex = 8;
            this.combinedView.Text = "Combined";
            this.combinedView.UseVisualStyleBackColor = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ContextMenuStrip = this.contextMenu;
            base.Controls.Add(this.combinedView);
            base.Controls.Add(this.riverView);
            base.Controls.Add(this.currentPanel);
            base.Controls.Add(this.variableView);
            base.Controls.Add(this.title);
            this.MaximumSize = new Size(0x400, 0x200);
            this.MinimumSize = new Size(400, 200);
            base.Name = "TimePanel";
            base.Size = new Size(0x19b, 0xe9);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void riverView_CheckedChanged(object sender, EventArgs e)
        {
            if (this.riverView.Checked)
            {
                List<ForecastRiverPanel> list = new List<ForecastRiverPanel>();
                foreach (ForecastItemPanel panel in this.Container.Controls)
                {
                    WizardForm instance = WizardForm.Instance;
                    ForecastRiverPanel panel2 = new ForecastRiverPanel(WizardForm.TSForm, this, WizardForm.TSForm.DataSet.GetStatDataSet(), panel.MVRSInfo.Variables[0], 0, instance.CurrentPanel.ResultArea.StartPointIndex, instance.CurrentPanel.ResultArea.EndPointIndex);
                    panel2.PatternStart = instance.CurrentPanel.PatternArea.StartPointIndex;
                    panel2.PatternEnd = instance.CurrentPanel.PatternArea.EndPointIndex;
                    panel2.Size = panel.Size;
                    InitForecast(panel2, panel.MVRSInfo.SearchOptions, panel.MVRSInfo.Tolerance, panel.MVRSInfo.Variables);
                    list.Add(panel2);
                }
                this.Container.Controls.Clear();
                this.Container.Controls.AddRange(list.ToArray());
            }
        }

        public void ToggleRadioControls(bool value)
        {
            this.variableView.Visible = value;
            this.riverView.Visible = value;
            this.combinedView.Visible = value;
        }

        public static void Update(Control.ControlCollection controls, PreviewState state)
        {
            foreach (Control control in controls)
            {
                (control as TimePanel).UpdatePanels(state);
            }
        }

        public void UpdateArea(int start, int end)
        {
            foreach (Control control in this.Container.Controls)
            {
                ((IForecast) control).SetStartEndIndex(start, end);
            }
        }

        public void UpdateArea(int pStart, int pEnd, int rStart, int rEnd)
        {
            ForecastItemPanel panel = this.Container.Controls[0] as ForecastItemPanel;
            ForecastRiverPanel panel2 = this.Container.Controls[1] as ForecastRiverPanel;
            panel.SetStartEndIndex(pStart, pEnd);
            panel2.SetStartEndIndex(rStart, rEnd);
        }

        private void UpdatePanels(PreviewState state)
        {
            float startMin;
            float patEndIndex;
            int tolerance;
            float num4;
            ForecastRiverPanel panel;
            ForecastItemPanel panel2;
            DataSet set;
            switch (this.type)
            {
                case TimePanelType.Tolerance:
                    foreach (ForecastRiverPanel panel3 in this.Container.Controls)
                    {
                        panel3.PatternStart = state.MVRSInfo.PatStartIndex;
                        panel3.PatternEnd = state.MVRSInfo.PatEndIndex;
                        InitForecast(panel3, state.MVRSInfo.SearchOptions, panel3.Tolerance, state.MVRSInfo.Variables);
                    }
                    break;

                case TimePanelType.Startpoint:
                    startMin = state.StartMin;
                    patEndIndex = state.MVRSInfo.PatEndIndex;
                    num4 = ((float) (state.StartMax - state.StartMin)) / (this.Container.Controls.Count - 1f);
                    tolerance = state.MVRSInfo.Tolerance;
                    for (int i = 0; i < this.Container.Controls.Count; i++)
                    {
                        panel = this.Container.Controls[i] as ForecastRiverPanel;
                        panel.PatternStart = (int) startMin;
                        panel.PatternEnd = (int) patEndIndex;
                        InitForecast(panel, state.MVRSInfo.SearchOptions, tolerance, state.MVRSInfo.Variables);
                        startMin += num4;
                    }
                    break;

                case TimePanelType.Endpoint:
                    startMin = state.MVRSInfo.PatStartIndex;
                    patEndIndex = state.EndMin;
                    num4 = ((float) (state.EndMax - state.EndMin)) / (this.Container.Controls.Count - 1f);
                    tolerance = state.MVRSInfo.Tolerance;
                    for (int j = 0; j < this.Container.Controls.Count; j++)
                    {
                        panel = this.Container.Controls[j] as ForecastRiverPanel;
                        panel.PatternStart = (int) startMin;
                        panel.PatternEnd = (int) patEndIndex;
                        InitForecast(panel, state.MVRSInfo.SearchOptions, tolerance, state.MVRSInfo.Variables);
                        patEndIndex += num4;
                    }
                    break;

                case TimePanelType.SearchSettings:
                    panel2 = this.Container.Controls[0] as ForecastItemPanel;
                    panel = this.Container.Controls[1] as ForecastRiverPanel;
                    panel.PatternStart = state.MVRSInfo.PatStartIndex;
                    panel.PatternEnd = state.MVRSInfo.PatEndIndex;
                    set = DataSet.CloneDataSet(WizardForm.TSForm.DataSet);
                    InitForecast(panel, panel.MvrsInfo.SearchOptions, state.MVRSInfo.Tolerance, state.MVRSInfo.Variables);
                    Console.Write(set.calcEnabledNumItems());
                    set.DisableItems(panel.UnmatchedIndices);
                    panel2.DataSet = set;
                    panel2.Reset();
                    break;

                case TimePanelType.Variables:
                    panel2 = this.Container.Controls[0] as ForecastItemPanel;
                    panel = this.Container.Controls[1] as ForecastRiverPanel;
                    panel.PatternStart = state.MVRSInfo.PatStartIndex;
                    panel.PatternEnd = state.MVRSInfo.PatEndIndex;
                    set = DataSet.CloneDataSet(WizardForm.TSForm.DataSet);
                    InitForecast(panel, state.MVRSInfo.SearchOptions, state.MVRSInfo.Tolerance, state.MVRSInfo.Variables);
                    Console.Write(set.calcEnabledNumItems());
                    set.DisableItems(panel.UnmatchedIndices);
                    panel2.DataSet = set;
                    panel2.Reset();
                    break;
            }
        }

        private void variableView_CheckedChanged(object sender, EventArgs e)
        {
            if (this.variableView.Checked)
            {
                List<ForecastItemPanel> list = new List<ForecastItemPanel>();
                foreach (ForecastRiverPanel panel in this.Container.Controls)
                {
                    DataSet dataset = DataSet.CloneDataSet(WizardForm.TSForm.DataSet);
                    dataset.DisableItems(panel.UnmatchedIndices);
                    ForecastItemPanel item = new ForecastItemPanel(WizardForm.TSForm, panel.MvrsInfo.Variables[0], dataset, 0, dataset.NumTimePoints - 1);
                    item.Size = panel.Size;
                    item.MVRSInfo = panel.MvrsInfo;
                    list.Add(item);
                }
                this.Container.Controls.Clear();
                this.Container.Controls.AddRange(list.ToArray());
            }
        }

        public FlowLayoutPanel Container
        {
            get
            {
                return this.currentPanel;
            }
        }

        public DataSet Dataset
        {
            get
            {
                return this.dataset;
            }
            set
            {
                this.dataset = value;
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public TimePanelType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
    }
}

