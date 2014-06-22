namespace TimeSearcher.Wizard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Search;

    public class WizardForm : Form
    {
        private CheckBox cbAllSettings;
        private CheckBox cbAllVariables;
        private CheckBox cbAS;
        private CheckBox cbEndPoint;
        private CheckBox cbLT;
        private CheckBox cbNR;
        private CheckBox cbOT;
        private CheckBox cbSettings;
        private CheckBox cbStartPoint;
        private CheckBox cbTolerance;
        private CheckBox cbVariables;
        private CheckBox checkBox8;
        private CheckedListBox clVariables;
        private ComboBox comboBox1;
        private IContainer components;
        private CurrentRiverPanel crp;
        private DataSet dataSet;
        private static Size defaultSize = new Size(410, 235);
        private Slider epSlider;
        private FlowLayoutPanel flPanel;
        private FlowLayoutPanel flPanelDefault;
        private static TimeSearcherForm form;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private static WizardForm instance;
        private Label label1;
        private Label label2;
        private Label label3;
        private Size panelSize;
        private int rows;
        private List<bool[]> searchSettingsCombinations;
        private Slider spSlider;
        private Slider sTolerance;
        private TabControl tabPanel;
        private TabPage tabSettings;
        private string[] variables;
        private List<bool[]> variablesCombinations;

        public WizardForm()
        {
            this.InitializeComponent();
        }

        public WizardForm(Form owner)
        {
            form = owner as TimeSearcherForm;
            instance = this;
            this.dataSet = form.DataSet.GetStatDataSet();
            this.InitializeComponent();
            this.Init();
            this.panelSize = new Size(this.flPanel.Width - 30, (this.flPanel.Height / 3) - 10);
        }

        private void AddCurrentRiverPanel()
        {
            int nVar = 0;
            TimePanel tp = this.CreateNewPanel(TimePanelType.Current, nVar, defaultSize);
            this.crp = new CurrentRiverPanel(this, tp, form.DataSet.GetStatDataSet(), nVar);
            this.crp.SetInnerSize(new Size(tp.Container.Size.Width - 10, tp.Container.Height - 10));
            this.crp.PatternArea.SetArea(this.spSlider.Value, this.epSlider.Value);
            tp.AddForecastPanel(this.crp);
            this.flPanelDefault.Controls.Add(tp);
            ToolTip tip = new ToolTip();
            Button control = new Button();
            Button button2 = new Button();
            Button button3 = new Button();
            Size size = new Size(20, 20);
            control.Size = button3.Size = button2.Size = size;
            control.Location = new Point(100, 0);
            control.Text = "F";
            control.Click += delegate (object sender, EventArgs e) {
                this.crp.ResultArea.SetArea(this.crp.PatternArea.EndPointIndex + 1, this.epSlider.Maximum);
                this.crp.Invalidate();
            };
            button3.Location = new Point(125, 0);
            button3.Text = "R";
            button3.Click += delegate (object sender, EventArgs e) {
                this.crp.ResultArea.SetArea(this.epSlider.Maximum - 1, this.epSlider.Maximum);
                this.crp.Invalidate();
            };
            button2.Location = new Point(150, 0);
            button2.Text = "W";
            button2.Click += delegate (object sender, EventArgs e) {
                this.crp.ResultArea.SetArea(this.spSlider.Minimum, this.epSlider.Maximum);
                this.crp.Invalidate();
            };
            tip.SetToolTip(control, "Set the viewable area to the forecast outcome");
            tip.SetToolTip(button3, "Set the viewable area to the forecast result");
            tip.SetToolTip(button2, "Set the viewable area to the entire plot");
            tp.Controls.Add(control);
            tp.Controls.Add(button3);
            tp.Controls.Add(button2);
            tp.ToggleRadioControls(false);
        }

        private void AddSearchSettingTimePanel(List<bool[]> combinations)
        {
            foreach (bool[] flagArray in combinations)
            {
                if (!this.IsComboIn(flagArray, this.searchSettingsCombinations))
                {
                    this.searchSettingsCombinations.Add(flagArray);
                    int variableIndex = 0;
                    TimePanel tp = this.CreateNewPanel(TimePanelType.SearchSettings, 0, this.panelSize);
                    Size size = tp.Container.Size;
                    int width = (size.Width - 65) / 2;
                    ForecastRiverPanel panel = new ForecastRiverPanel(form, tp, form.DataSet.GetStatDataSet(), variableIndex, 0, this.crp.ResultArea.StartPointIndex, this.crp.ResultArea.EndPointIndex);
                    panel.PatternStart = this.crp.PatternArea.StartPointIndex;
                    panel.PatternEnd = this.crp.PatternArea.EndPointIndex;
                    DataSet dataset = DataSet.CloneDataSet(form.DataSet);
                    panel.Size = new Size(width, size.Height);
                    TimePanel.InitForecast(panel, this.CreateSearchOptions(flagArray), this.sTolerance.Value, this.GetSelectedVariablesFromForm);
                    dataset.DisableItems(panel.UnmatchedIndices);
                    ForecastItemPanel panel3 = new ForecastItemPanel(form, variableIndex, dataset, this.crp.PatternArea.StartPointIndex, this.crp.PatternArea.EndPointIndex);
                    panel3.Size = panel.Size;
                    tp.Container.Controls.Add(panel3);
                    tp.Container.Controls.Add(panel);
                    this.flPanel.Controls.Add(tp);
                }
            }
        }

        private void AddVariablesTimePanel(List<bool[]> combinations)
        {
            foreach (bool[] flagArray in combinations)
            {
                if (!this.IsComboIn(flagArray, this.variablesCombinations))
                {
                    this.variablesCombinations.Add(flagArray);
                    int variableIndex = 0;
                    TimePanel tp = this.CreateNewPanel(TimePanelType.Variables, 0, this.panelSize);
                    Size size = tp.Container.Size;
                    int width = (size.Width - 65) / 2;
                    ForecastRiverPanel panel = new ForecastRiverPanel(form, tp, form.DataSet.GetStatDataSet(), variableIndex, 0, this.crp.ResultArea.StartPointIndex, this.crp.ResultArea.EndPointIndex);
                    panel.PatternStart = this.crp.PatternArea.StartPointIndex;
                    panel.PatternEnd = this.crp.PatternArea.EndPointIndex;
                    DataSet dataset = DataSet.CloneDataSet(form.DataSet);
                    panel.Size = new Size(width, size.Height);
                    TimePanel.InitForecast(panel, this.GetSearchOptionsFromForm, this.sTolerance.Value, this.ConvertBoolToIntArray(flagArray));
                    dataset.DisableItems(panel.UnmatchedIndices);
                    ForecastItemPanel panel3 = new ForecastItemPanel(form, variableIndex, dataset, this.crp.PatternArea.StartPointIndex, this.crp.PatternArea.EndPointIndex);
                    panel3.Size = panel.Size;
                    tp.Container.Controls.Add(panel3);
                    tp.Container.Controls.Add(panel);
                    this.flPanel.Controls.Add(tp);
                }
            }
        }

        private void cbAllSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbAllSettings.Checked)
            {
                CombinationGenerator generator = new CombinationGenerator(4);
                this.AddSearchSettingTimePanel(generator.List);
                this.cbLT.Checked = true;
                this.cbAS.Checked = true;
                this.cbOT.Checked = true;
                this.cbNR.Checked = true;
            }
            else
            {
                this.RemovePanels(TimePanelType.SearchSettings);
                List<bool[]> combinations = new List<bool[]>();
                bool[] toBoolArray = this.GetSearchOptionsFromForm.ToBoolArray;
                combinations.Add(toBoolArray);
                this.AddSearchSettingTimePanel(combinations);
            }
        }

        private void cbAllVariables_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbAllVariables.Checked)
            {
                this.RemovePanels(TimePanelType.Variables);
                CombinationGenerator generator = new CombinationGenerator(this.clVariables.Items.Count);
                this.AddVariablesTimePanel(generator.List);
                for (int i = 0; i < this.clVariables.Items.Count; i++)
                {
                    this.clVariables.SetItemChecked(i, true);
                }
            }
            else
            {
                this.RemovePanels(TimePanelType.Variables);
                List<bool[]> combinations = new List<bool[]>();
                combinations.Add(this.ConvertIntToBoolArray(this.GetSelectedVariablesFromForm, this.clVariables.Items.Count));
                this.AddVariablesTimePanel(combinations);
            }
        }

        private void cbAS_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbAllSettings.Checked)
            {
                if (this.cbLT.Checked)
                {
                    int[] numArray;
                    List<bool[]> searchSettings = this.GetSearchSettings(out numArray);
                    searchSettings = this.remapArray(searchSettings, numArray, 4);
                    this.AddSearchSettingTimePanel(searchSettings);
                }
                else
                {
                    this.RemoveSearchSettingsTimePanel(SearchSettingType.AmplitudeScaling);
                }
            }
            else
            {
                this.RemovePanels(TimePanelType.SearchSettings);
                List<bool[]> combinations = new List<bool[]>();
                bool[] toBoolArray = this.GetSearchOptionsFromForm.ToBoolArray;
                combinations.Add(toBoolArray);
                this.AddSearchSettingTimePanel(combinations);
            }
        }

        private void cbEndPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.cbEndPoint.Checked)
            {
                this.flPanel.Controls.RemoveByKey(TimePanelType.Endpoint.ToString());
            }
            else
            {
                int nVar = 0;
                int num2 = 10;
                this.rows++;
                DataSet statDataSet = form.DataSet.GetStatDataSet();
                TimePanel tp = this.CreateNewPanel(TimePanelType.Endpoint, nVar, this.panelSize);
                tp.Name = tp.Type.ToString();
                Size size = tp.Container.Size;
                int width = (size.Width - 65) / num2;
                float num4 = this.spSlider.Value;
                float num5 = ((float) (this.epSlider.Maximum - this.epSlider.Minimum)) / ((float) (num2 - 1));
                float minimum = this.epSlider.Minimum;
                for (int i = 1; i <= num2; i++)
                {
                    ForecastRiverPanel panel = new ForecastRiverPanel(form, tp, statDataSet, nVar, 0, this.crp.ResultArea.StartPointIndex, this.crp.ResultArea.EndPointIndex);
                    panel.AxisEnabled = false;
                    panel.Size = new Size(width, size.Height);
                    panel.PatternStart = (int) num4;
                    panel.PatternEnd = (int) minimum;
                    TimePanel.InitForecast(panel, this.GetSearchOptionsFromForm, this.sTolerance.Value, this.GetSelectedVariablesFromForm);
                    minimum += num5;
                    tp.AddForecastPanel(panel);
                }
                this.flPanel.Controls.Add(tp);
            }
        }

        private void cbLT_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbAllSettings.Checked)
            {
                if (this.cbLT.Checked)
                {
                    int[] numArray;
                    List<bool[]> searchSettings = this.GetSearchSettings(out numArray);
                    searchSettings = this.remapArray(searchSettings, numArray, 4);
                    this.AddSearchSettingTimePanel(searchSettings);
                }
                else
                {
                    this.RemoveSearchSettingsTimePanel(SearchSettingType.LinearTrend);
                }
            }
            else
            {
                this.RemovePanels(TimePanelType.SearchSettings);
                List<bool[]> combinations = new List<bool[]>();
                bool[] toBoolArray = this.GetSearchOptionsFromForm.ToBoolArray;
                combinations.Add(toBoolArray);
                this.AddSearchSettingTimePanel(combinations);
            }
        }

        private void cbNR_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbAllSettings.Checked)
            {
                if (this.cbNR.Checked)
                {
                    int[] numArray;
                    List<bool[]> searchSettings = this.GetSearchSettings(out numArray);
                    searchSettings = this.remapArray(searchSettings, numArray, 4);
                    this.AddSearchSettingTimePanel(searchSettings);
                }
                else
                {
                    this.RemoveSearchSettingsTimePanel(SearchSettingType.NoiseReduction);
                }
            }
            else
            {
                this.RemovePanels(TimePanelType.SearchSettings);
                List<bool[]> combinations = new List<bool[]>();
                bool[] toBoolArray = this.GetSearchOptionsFromForm.ToBoolArray;
                combinations.Add(toBoolArray);
                this.AddSearchSettingTimePanel(combinations);
            }
        }

        private void cbOT_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbAllSettings.Checked)
            {
                if (this.cbOT.Checked)
                {
                    int[] numArray;
                    List<bool[]> searchSettings = this.GetSearchSettings(out numArray);
                    searchSettings = this.remapArray(searchSettings, numArray, 4);
                    this.AddSearchSettingTimePanel(searchSettings);
                }
                else
                {
                    this.RemoveSearchSettingsTimePanel(SearchSettingType.OffsetTranslation);
                }
            }
            else
            {
                this.RemovePanels(TimePanelType.SearchSettings);
                List<bool[]> combinations = new List<bool[]>();
                bool[] toBoolArray = this.GetSearchOptionsFromForm.ToBoolArray;
                combinations.Add(toBoolArray);
                this.AddSearchSettingTimePanel(combinations);
            }
        }

        private void cbSettings_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = this.cbSettings.Checked;
            this.cbAllSettings.Enabled = this.cbLT.Enabled = this.cbOT.Enabled = this.cbAS.Enabled = this.cbNR.Enabled = flag;
        }

        private void cbStartPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.cbStartPoint.Checked)
            {
                this.flPanel.Controls.RemoveByKey(TimePanelType.Startpoint.ToString());
            }
            else
            {
                int nVar = 0;
                int num2 = 10;
                this.rows++;
                DataSet statDataSet = form.DataSet.GetStatDataSet();
                TimePanel tp = this.CreateNewPanel(TimePanelType.Startpoint, nVar, this.panelSize);
                tp.Name = tp.Type.ToString();
                Size size = tp.Container.Size;
                int width = (size.Width - 65) / num2;
                float minimum = this.spSlider.Minimum;
                float num5 = ((float) (this.spSlider.Maximum - this.spSlider.Minimum)) / ((float) num2);
                float maximum = this.spSlider.Maximum;
                for (int i = 1; i <= num2; i++)
                {
                    ForecastRiverPanel panel = new ForecastRiverPanel(form, tp, statDataSet, nVar, 0, this.crp.ResultArea.StartPointIndex, this.crp.ResultArea.EndPointIndex);
                    panel.AxisEnabled = false;
                    panel.Size = new Size(width, size.Height);
                    panel.PatternStart = (int) minimum;
                    panel.PatternEnd = (int) maximum;
                    TimePanel.InitForecast(panel, this.GetSearchOptionsFromForm, this.sTolerance.Value, this.GetSelectedVariablesFromForm);
                    minimum += num5;
                    tp.AddForecastPanel(panel);
                }
                this.flPanel.Controls.Add(tp);
            }
        }

        private void cbTolerance_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.cbTolerance.Checked)
            {
                this.flPanel.Controls.RemoveByKey(TimePanelType.Tolerance.ToString());
            }
            else
            {
                int nVar = 0;
                int num2 = 10;
                this.rows++;
                DataSet statDataSet = form.DataSet.GetStatDataSet();
                TimePanel tp = this.CreateNewPanel(TimePanelType.Tolerance, nVar, this.panelSize);
                tp.Name = tp.Type.ToString();
                Size size = tp.Container.Size;
                int width = (size.Width - 65) / num2;
                for (int i = 1; i <= num2; i++)
                {
                    ForecastRiverPanel panel = new ForecastRiverPanel(form, tp, statDataSet, nVar, 0, this.crp.ResultArea.StartPointIndex, this.crp.ResultArea.EndPointIndex);
                    panel.Size = new Size(width, size.Height);
                    panel.PatternStart = this.crp.PatternArea.StartPointIndex;
                    panel.PatternEnd = this.crp.PatternArea.EndPointIndex;
                    TimePanel.InitForecast(panel, this.GetSearchOptionsFromForm, i * 10, this.GetSelectedVariablesFromForm);
                    tp.AddForecastPanel(panel);
                }
                this.flPanel.Controls.Add(tp);
            }
        }

        private void cbVariables_CheckedChanged(object sender, EventArgs e)
        {
            this.cbAllVariables.Enabled = this.cbVariables.Checked;
            this.clVariables.Enabled = this.cbVariables.Checked;
        }

        private void clVariables_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.cbVariables.Checked)
            {
                if (this.cbAllVariables.Checked)
                {
                    if (e.NewValue == CheckState.Unchecked)
                    {
                        this.RemoveVariablesTimepanel(e.Index);
                    }
                    else
                    {
                        int[] getSelectedVariablesFromForm = this.GetSelectedVariablesFromForm;
                        int[] destinationArray = new int[getSelectedVariablesFromForm.Length + 1];
                        destinationArray[0] = e.Index;
                        Array.Copy(getSelectedVariablesFromForm, 0, destinationArray, 1, getSelectedVariablesFromForm.Length);
                        CombinationGenerator generator = new CombinationGenerator(this.clVariables.CheckedItems.Count + 1);
                        List<bool[]> combos = generator.List;
                        combos = this.remapArray(combos, destinationArray, this.clVariables.Items.Count);
                        this.AddVariablesTimePanel(combos);
                    }
                }
                else
                {
                    int[] numArray3;
                    if (e.NewValue == CheckState.Checked)
                    {
                        int[] sourceArray = this.GetSelectedVariablesFromForm;
                        numArray3 = new int[sourceArray.Length + 1];
                        numArray3[0] = e.Index;
                        Array.Copy(sourceArray, 0, numArray3, 1, sourceArray.Length);
                    }
                    else
                    {
                        int[] numArray5 = this.GetSelectedVariablesFromForm;
                        numArray3 = new int[numArray5.Length - 1];
                        int index = 0;
                        for (int i = 0; i < numArray5.Length; i++)
                        {
                            if (e.Index != numArray5[i])
                            {
                                numArray3[index] = numArray5[i];
                                index++;
                            }
                        }
                    }
                    this.RemovePanels(TimePanelType.Variables);
                    List<bool[]> combinations = new List<bool[]>();
                    combinations.Add(this.ConvertIntToBoolArray(numArray3, this.clVariables.Items.Count));
                    this.AddVariablesTimePanel(combinations);
                }
            }
        }

        private int[] ConvertBoolToIntArray(bool[] combo)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < combo.Length; i++)
            {
                if (combo[i])
                {
                    list.Add(i);
                }
            }
            return list.ToArray();
        }

        private bool[] ConvertIntToBoolArray(int[] indices, int length)
        {
            bool[] flagArray = new bool[length];
            for (int i = 0; i < indices.Length; i++)
            {
                flagArray[indices[i]] = true;
            }
            return flagArray;
        }

        private TimePanel CreateNewPanel(TimePanelType type, int nVar, Size size)
        {
            return new TimePanel(TimePanel.GetNameFromType(type), size, type);
        }

        private TimePanel CreateNewPanel(TimePanelType type, int nVar, Size size, int margin)
        {
            return this.CreateNewPanel(type, nVar, size, 50);
        }

        private SearchOptions CreateSearchOptions(bool[] array)
        {
            SearchOptions defaultSearchOpts = MultiVarResSearch.GetDefaultSearchOpts();
            defaultSearchOpts.setLT(array[0]);
            defaultSearchOpts.setOT(array[1]);
            defaultSearchOpts.setAS(array[2]);
            defaultSearchOpts.setNR(array[3]);
            return defaultSearchOpts;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void EPTrackBar_ValueChanged(object sender, EventArgs e)
        {
            this.crp.PatternArea.SetArea(this.spSlider.Value, this.epSlider.Value);
            this.spSlider.Maximum = this.epSlider.Value - 1;
            TimePanel.Update(this.flPanel.Controls, this.PreviewState);
            this.crp.UpdateForecast(this.PreviewState);
        }

        private List<bool[]> GetSearchSettings(out int[] indices)
        {
            SearchOptions getSearchOptionsFromForm = this.GetSearchOptionsFromForm;
            List<int> list = new List<int>();
            if (getSearchOptionsFromForm.hasLT())
            {
                list.Add(0);
            }
            if (getSearchOptionsFromForm.hasOT())
            {
                list.Add(1);
            }
            if (getSearchOptionsFromForm.hasAS())
            {
                list.Add(2);
            }
            if (getSearchOptionsFromForm.hasNR())
            {
                list.Add(3);
            }
            CombinationGenerator generator = new CombinationGenerator(list.Count);
            indices = list.ToArray();
            return generator.List;
        }

        public void Init()
        {
            this.SliderSetup();
            this.AddCurrentRiverPanel();
            this.searchSettingsCombinations = new List<bool[]>();
            this.variablesCombinations = new List<bool[]>();
            this.sTolerance.TrackBar.ValueChanged += new EventHandler(this.STolerance_ValueChanged);
            this.spSlider.TrackBar.ValueChanged += new EventHandler(this.SPTrackBar_ValueChanged);
            this.epSlider.TrackBar.ValueChanged += new EventHandler(this.EPTrackBar_ValueChanged);
            foreach (string str in this.dataSet.getDynVarNames(TSForm.LogMenuItemChecked))
            {
                this.clVariables.Items.Add(str);
            }
            this.clVariables.SetItemChecked(0, true);
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.flPanel = new FlowLayoutPanel();
            this.label2 = new Label();
            this.comboBox1 = new ComboBox();
            this.label3 = new Label();
            this.tabPanel = new TabControl();
            this.tabSettings = new TabPage();
            this.cbAllVariables = new CheckBox();
            this.clVariables = new CheckedListBox();
            this.cbVariables = new CheckBox();
            this.label1 = new Label();
            this.sTolerance = new Slider();
            this.checkBox8 = new CheckBox();
            this.cbSettings = new CheckBox();
            this.groupBox2 = new GroupBox();
            this.cbAllSettings = new CheckBox();
            this.cbNR = new CheckBox();
            this.cbAS = new CheckBox();
            this.cbOT = new CheckBox();
            this.cbLT = new CheckBox();
            this.cbEndPoint = new CheckBox();
            this.epSlider = new Slider();
            this.cbStartPoint = new CheckBox();
            this.spSlider = new Slider();
            this.cbTolerance = new CheckBox();
            this.flPanelDefault = new FlowLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.tabPanel.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.flPanel);
            this.groupBox1.Location = new Point(427, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(842, 720);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Forecasts preview";
            this.flPanel.AutoScroll = true;
//            this.flPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.flPanel.Location = new Point(5, 23);
            this.flPanel.Name = "flPanel";
            this.flPanel.Size = new Size(830, 693);
            this.flPanel.TabIndex = 0;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(9, 183);
            this.label2.Name = "label2";
            this.label2.Size = new Size(124, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Selected variables";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(144, 255);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(221, 24);
            this.comboBox1.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(9, 0x10d);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x7e, 0x11);
            this.label3.TabIndex = 7;
            this.label3.Text = "Selected Algorithm";
            this.tabPanel.Controls.Add(this.tabSettings);
            this.tabPanel.Location = new Point(2, 0xf4);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.SelectedIndex = 0;
            this.tabPanel.Size = new Size(0x1a5, 0x1dd);
            this.tabPanel.TabIndex = 5;
            this.tabSettings.BackColor = SystemColors.Control;
            this.tabSettings.Controls.Add(this.cbAllVariables);
            this.tabSettings.Controls.Add(this.clVariables);
            this.tabSettings.Controls.Add(this.label2);
            this.tabSettings.Controls.Add(this.cbVariables);
            this.tabSettings.Controls.Add(this.label1);
            this.tabSettings.Controls.Add(this.sTolerance);
            this.tabSettings.Controls.Add(this.checkBox8);
            this.tabSettings.Controls.Add(this.cbSettings);
            this.tabSettings.Controls.Add(this.label3);
            this.tabSettings.Controls.Add(this.comboBox1);
            this.tabSettings.Controls.Add(this.groupBox2);
            this.tabSettings.Controls.Add(this.cbEndPoint);
            this.tabSettings.Controls.Add(this.epSlider);
            this.tabSettings.Controls.Add(this.cbStartPoint);
            this.tabSettings.Controls.Add(this.spSlider);
            this.tabSettings.Controls.Add(this.cbTolerance);
            this.tabSettings.Location = new Point(4, 0x19);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new Padding(3);
            this.tabSettings.Size = new Size(0x19d, 0x1c0);
            this.tabSettings.TabIndex = 0;
            this.tabSettings.Text = "Search settings";
            this.cbAllVariables.AutoSize = true;
            this.cbAllVariables.CheckAlign = ContentAlignment.MiddleRight;
            this.cbAllVariables.Enabled = false;
            this.cbAllVariables.Location = new Point(0xc2, 0xb7);
            this.cbAllVariables.Name = "cbAllVariables";
            this.cbAllVariables.Size = new Size(0xab, 0x15);
            this.cbAllVariables.TabIndex = 11;
            this.cbAllVariables.Text = "Show all Combinations";
            this.cbAllVariables.UseVisualStyleBackColor = true;
            this.cbAllVariables.CheckedChanged += new EventHandler(this.cbAllVariables_CheckedChanged);
            this.clVariables.Enabled = false;
            this.clVariables.FormattingEnabled = true;
            this.clVariables.Location = new Point(12, 0xce);
            this.clVariables.Name = "clVariables";
            this.clVariables.Size = new Size(0x161, 0x37);
            this.clVariables.TabIndex = 4;
            this.clVariables.ItemCheck += new ItemCheckEventHandler(this.clVariables_ItemCheck);
            this.cbVariables.AutoSize = true;
            this.cbVariables.Location = new Point(0x17b, 0xda);
            this.cbVariables.Name = "cbVariables";
            this.cbVariables.Size = new Size(0x12, 0x11);
            this.cbVariables.TabIndex = 9;
            this.cbVariables.UseVisualStyleBackColor = true;
            this.cbVariables.CheckedChanged += new EventHandler(this.cbVariables_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x138, 6);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5f, 0x11);
            this.label1.TabIndex = 6;
            this.label1.Text = "Show Preview";
            this.sTolerance.AutoSize = true;
        //    this.sTolerance.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.sTolerance.FloatMode = false;
            this.sTolerance.Location = new Point(12, 9);
            this.sTolerance.Maximum = 100;
            this.sTolerance.Minimum = 0;
            this.sTolerance.Name = "sTolerance";
            this.sTolerance.PercentMode = true;
            this.sTolerance.Size = new Size(0x161, 0x52);
            this.sTolerance.TabIndex = 0;
            this.sTolerance.Title = "Tolerance";
            this.sTolerance.Value = 0;
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new Point(0x17b, 0x111);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new Size(0x12, 0x11);
            this.checkBox8.TabIndex = 10;
            this.checkBox8.UseVisualStyleBackColor = true;
            this.cbSettings.AutoSize = true;
            this.cbSettings.Location = new Point(0x17b, 100);
            this.cbSettings.Name = "cbSettings";
            this.cbSettings.Size = new Size(0x12, 0x11);
            this.cbSettings.TabIndex = 4;
            this.cbSettings.UseVisualStyleBackColor = true;
            this.cbSettings.CheckedChanged += new EventHandler(this.cbSettings_CheckedChanged);
            this.groupBox2.Controls.Add(this.cbAllSettings);
            this.groupBox2.Controls.Add(this.cbNR);
            this.groupBox2.Controls.Add(this.cbAS);
            this.groupBox2.Controls.Add(this.cbOT);
            this.groupBox2.Controls.Add(this.cbLT);
            this.groupBox2.Location = new Point(6, 0x5b);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x167, 0x52);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            this.cbAllSettings.AutoSize = true;
            this.cbAllSettings.CheckAlign = ContentAlignment.MiddleRight;
            this.cbAllSettings.Enabled = false;
            this.cbAllSettings.Location = new Point(0xbc, 9);
            this.cbAllSettings.Name = "cbAllSettings";
            this.cbAllSettings.Size = new Size(0xab, 0x15);
            this.cbAllSettings.TabIndex = 5;
            this.cbAllSettings.Text = "Show all Combinations";
            this.cbAllSettings.UseVisualStyleBackColor = true;
            this.cbAllSettings.CheckedChanged += new EventHandler(this.cbAllSettings_CheckedChanged);
            this.cbNR.AutoSize = true;
            this.cbNR.Enabled = false;
            this.cbNR.Location = new Point(0xdb, 0x35);
            this.cbNR.Name = "cbNR";
            this.cbNR.Size = new Size(0x81, 0x15);
            this.cbNR.TabIndex = 3;
            this.cbNR.Text = "Noise reduction";
            this.cbNR.UseVisualStyleBackColor = true;
            this.cbNR.CheckedChanged += new EventHandler(this.cbNR_CheckedChanged);
            this.cbAS.AutoSize = true;
            this.cbAS.Enabled = false;
            this.cbAS.Location = new Point(6, 0x37);
            this.cbAS.Name = "cbAS";
            this.cbAS.Size = new Size(140, 0x15);
            this.cbAS.TabIndex = 2;
            this.cbAS.Text = "Amplitude scaling";
            this.cbAS.UseVisualStyleBackColor = true;
            this.cbAS.CheckedChanged += new EventHandler(this.cbAS_CheckedChanged);
            this.cbOT.AutoSize = true;
            this.cbOT.Enabled = false;
            this.cbOT.Location = new Point(0xdb, 0x1d);
            this.cbOT.Name = "cbOT";
            this.cbOT.Size = new Size(0x8a, 0x15);
            this.cbOT.TabIndex = 1;
            this.cbOT.Text = "Offset translation";
            this.cbOT.UseVisualStyleBackColor = true;
            this.cbOT.CheckedChanged += new EventHandler(this.cbOT_CheckedChanged);
            this.cbLT.AutoSize = true;
            this.cbLT.Enabled = false;
            this.cbLT.Location = new Point(6, 0x1f);
            this.cbLT.Name = "cbLT";
            this.cbLT.Size = new Size(0xa1, 0x15);
            this.cbLT.TabIndex = 0;
            this.cbLT.Text = "Linear trend removal";
            this.cbLT.UseVisualStyleBackColor = true;
            this.cbLT.CheckedChanged += new EventHandler(this.cbLT_CheckedChanged);
            this.cbEndPoint.AutoSize = true;
            this.cbEndPoint.Location = new Point(0x17b, 0x187);
            this.cbEndPoint.Name = "cbEndPoint";
            this.cbEndPoint.Size = new Size(0x12, 0x11);
            this.cbEndPoint.TabIndex = 5;
            this.cbEndPoint.UseVisualStyleBackColor = true;
            this.cbEndPoint.CheckedChanged += new EventHandler(this.cbEndPoint_CheckedChanged);
            this.epSlider.AutoSize = true;
//            this.epSlider.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.epSlider.FloatMode = false;
            this.epSlider.Location = new Point(12, 0x16b);
            this.epSlider.Maximum = 10;
            this.epSlider.Minimum = 0;
            this.epSlider.Name = "epSlider";
            this.epSlider.PercentMode = false;
            this.epSlider.Size = new Size(0x161, 0x57);
            this.epSlider.TabIndex = 4;
            this.epSlider.Title = "End Point";
            this.epSlider.Value = 0;
            this.cbStartPoint.AutoSize = true;
            this.cbStartPoint.Location = new Point(0x17b, 0x13d);
            this.cbStartPoint.Name = "cbStartPoint";
            this.cbStartPoint.Size = new Size(0x12, 0x11);
            this.cbStartPoint.TabIndex = 3;
            this.cbStartPoint.UseVisualStyleBackColor = true;
            this.cbStartPoint.CheckedChanged += new EventHandler(this.cbStartPoint_CheckedChanged);
            this.spSlider.AutoSize = true;
//            this.spSlider.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.spSlider.FloatMode = false;
            this.spSlider.Location = new Point(12, 0x11f);
            this.spSlider.Maximum = 10;
            this.spSlider.Minimum = 0;
            this.spSlider.Name = "spSlider";
            this.spSlider.PercentMode = false;
            this.spSlider.Size = new Size(0x161, 0x52);
            this.spSlider.TabIndex = 2;
            this.spSlider.Title = "Start Point";
            this.spSlider.Value = 0;
            this.cbTolerance.AutoSize = true;
            this.cbTolerance.Location = new Point(0x17b, 0x26);
            this.cbTolerance.Name = "cbTolerance";
            this.cbTolerance.Size = new Size(0x12, 0x11);
            this.cbTolerance.TabIndex = 1;
            this.cbTolerance.UseVisualStyleBackColor = true;
            this.cbTolerance.CheckedChanged += new EventHandler(this.cbTolerance_CheckedChanged);
            this.flPanelDefault.Location = new Point(4, 3);
            this.flPanelDefault.Name = "flPanelDefault";
            this.flPanelDefault.Size = new Size(417, 237);
            this.flPanelDefault.TabIndex = 6;
            base.AutoScaleDimensions = new SizeF(8, 16);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(1274, 722);
            base.Controls.Add(this.tabPanel);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.flPanelDefault);
            this.DoubleBuffered = true;
//            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "WizardForm";
            this.Text = "Wizard";
            this.groupBox1.ResumeLayout(false);
            this.tabPanel.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }

        private bool IsComboIn(bool[] combo, List<bool[]> combinations)
        {
            bool flag = false;
            foreach (bool[] flagArray in combinations)
            {
                flag = false;
                for (int i = 0; i < combo.Length; i++)
                {
                    if (combo[i] != flagArray[i])
                    {
                        flag = false;
                        break;
                    }
                    flag = true;
                }
                if (flag)
                {
                    return flag;
                }
            }
            return flag;
        }

        private List<bool[]> remapArray(List<bool[]> combos, int[] indices, int size)
        {
            List<bool[]> list = new List<bool[]>();
            foreach (bool[] flagArray in combos)
            {
                bool[] item = new bool[size];
                for (int i = 0; i < flagArray.Length; i++)
                {
                    if (flagArray[i])
                    {
                        item[indices[i]] = true;
                    }
                }
                list.Add(item);
            }
            return list;
        }

        private void RemovePanels(TimePanelType type)
        {
            List<Control> list = new List<Control>();
            foreach (TimePanel panel in this.flPanel.Controls)
            {
                if (panel.Type == type)
                {
                    list.Add(panel);
                }
            }
            foreach (Control control in list)
            {
                this.flPanel.Controls.Remove(control);
            }
            if (type == TimePanelType.Variables)
            {
                this.variablesCombinations.Clear();
            }
            if (type == TimePanelType.SearchSettings)
            {
                this.searchSettingsCombinations.Clear();
            }
        }

        private void RemoveSearchSettingsTimePanel(SearchSettingType type)
        {
            List<Control> list = new List<Control>();
            List<bool[]> list2 = new List<bool[]>();
            foreach (TimePanel panel in this.flPanel.Controls)
            {
                if (panel.Type == TimePanelType.SearchSettings)
                {
                    foreach (IForecast forecast in panel.Container.Controls)
                    {
                        ForecastRiverPanel panel2 = forecast as ForecastRiverPanel;
                        if (panel2 != null)
                        {
                            bool flag = false;
                            switch (type)
                            {
                                case SearchSettingType.LinearTrend:
                                    flag = panel2.MvrsInfo.SearchOptions.hasLT();
                                    break;

                                case SearchSettingType.OffsetTranslation:
                                    flag = panel2.MvrsInfo.SearchOptions.hasOT();
                                    break;

                                case SearchSettingType.AmplitudeScaling:
                                    flag = panel2.MvrsInfo.SearchOptions.hasAS();
                                    break;

                                case SearchSettingType.NoiseReduction:
                                    flag = panel2.MvrsInfo.SearchOptions.hasNR();
                                    break;
                            }
                            if (flag)
                            {
                                list.Add(panel);
                            }
                            else
                            {
                                list2.Add(panel2.MvrsInfo.SearchOptions.ToBoolArray);
                            }
                        }
                    }
                    continue;
                }
            }
            foreach (Control control in list)
            {
                this.flPanel.Controls.Remove(control);
            }
            this.searchSettingsCombinations = list2;
        }

        private void RemoveVariablesTimepanel(int varIdx)
        {
            List<Control> list = new List<Control>();
            List<bool[]> list2 = new List<bool[]>();
            foreach (TimePanel panel in this.flPanel.Controls)
            {
                if (panel.Type == TimePanelType.Variables)
                {
                    foreach (IForecast forecast in panel.Container.Controls)
                    {
                        ForecastRiverPanel panel2 = forecast as ForecastRiverPanel;
                        if (panel2 != null)
                        {
                            for (int i = 0; i < panel2.MvrsInfo.Variables.Length; i++)
                            {
                                if (panel2.MvrsInfo.Variables[i] == varIdx)
                                {
                                    list.Add(panel);
                                }
                            }
                            for (int j = 0; j < this.variablesCombinations.Count; j++)
                            {
                                if (this.variablesCombinations[j][varIdx] == null)
                                {
                                    list2.Add(this.variablesCombinations[j]);
                                }
                            }
                        }
                    }
                    continue;
                }
            }
            foreach (Control control in list)
            {
                this.flPanel.Controls.Remove(control);
            }
            this.variablesCombinations = list2;
        }

        public void SetSettingValues(int start, int end, int tolerance, bool[] so, int[] variables)
        {
            this.spSlider.Value = start;
            this.epSlider.Value = end;
            this.sTolerance.Value = tolerance;
            this.cbLT.Checked = so[0];
            this.cbAS.Checked = so[2];
            this.cbOT.Checked = so[1];
            this.cbNR.Checked = so[3];
            for (int i = 0; i < this.clVariables.Items.Count; i++)
            {
                this.clVariables.SetItemChecked(i, false);
            }
            foreach (int num2 in variables)
            {
                this.clVariables.SetItemChecked(num2, true);
            }
        }

        private void SliderSetup()
        {
            this.spSlider.FloatMode = true;
            this.spSlider.Minimum = 0;
            this.spSlider.Maximum = (this.dataSet.NumTimePoints - 1) / 2;
            this.epSlider.FloatMode = true;
            this.epSlider.Minimum = this.spSlider.Maximum + 1;
            this.epSlider.Maximum = this.dataSet.NumTimePoints - 1;
        }

        private void SPTrackBar_ValueChanged(object sender, EventArgs e)
        {
            this.crp.PatternArea.SetArea(this.spSlider.Value, this.epSlider.Value);
            this.epSlider.Minimum = this.spSlider.Value + 1;
            TimePanel.Update(this.flPanel.Controls, this.PreviewState);
            this.crp.UpdateForecast(this.PreviewState);
        }

        private void STolerance_ValueChanged(object sender, EventArgs e)
        {
            TimePanel.Update(this.flPanel.Controls, this.PreviewState);
            this.crp.UpdateForecast(this.PreviewState);
        }

        public void UpdateSliders(int spValue, int epValue)
        {
            this.spSlider.Maximum = epValue - 1;
            this.epSlider.Minimum = spValue + 1;
            this.spSlider.Value = spValue;
            this.epSlider.Value = epValue;
        }

        public CurrentRiverPanel CurrentPanel
        {
            get
            {
                return this.crp;
            }
        }

        public SearchOptions GetSearchOptionsFromForm
        {
            get
            {
                SearchOptions defaultSearchOpts = MultiVarResSearch.GetDefaultSearchOpts();
                defaultSearchOpts.setLT(this.cbLT.Checked);
                defaultSearchOpts.setOT(this.cbOT.Checked);
                defaultSearchOpts.setAS(this.cbAS.Checked);
                defaultSearchOpts.setNR(this.cbNR.Checked);
                return defaultSearchOpts;
            }
        }

        private int[] GetSelectedVariablesFromForm
        {
            get
            {
                int[] numArray = new int[this.clVariables.CheckedIndices.Count];
                for (int i = 0; i < this.clVariables.CheckedIndices.Count; i++)
                {
                    numArray[i] = this.clVariables.CheckedIndices[i];
                }
                return numArray;
            }
        }

        public static WizardForm Instance
        {
            get
            {
                return instance;
            }
        }

        public MultiVarResSearchInfo MVRSInfo
        {
            get
            {
                return new MultiVarResSearchInfo(this.crp.PatternArea.StartPointIndex, this.crp.PatternArea.EndPointIndex, 0, this.sTolerance.Value, this.GetSearchOptionsFromForm, this.GetSelectedVariablesFromForm);
            }
        }

        public FlowLayoutPanel Panel
        {
            get
            {
                return this.flPanel;
            }
        }

        public TimeSearcher.Wizard.PreviewState PreviewState
        {
            get
            {
                return new TimeSearcher.Wizard.PreviewState(this.MVRSInfo, this.spSlider.Minimum, this.spSlider.Maximum, this.epSlider.Minimum, this.epSlider.Maximum);
            }
        }

        public static TimeSearcherForm TSForm
        {
            get
            {
                return form;
            }
        }
    }
}

