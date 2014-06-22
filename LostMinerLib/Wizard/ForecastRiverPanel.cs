namespace TimeSearcher.Wizard
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Filters;
    using TimeSearcher.Panels;
    using TimeSearcher.Search;
    using Util.Stat;

    public class ForecastRiverPanel : RiverPanel, IForecast
    {
        private bool axisEnabled;
        private bool highlighted;
        private int patternEnd;
        private int patternStart;
        private bool selected;
        private TimePanel timePanel;
        private int tolerance;
        private TimeSearcher.Filters.UnmatchedEntity unmatchedEntity;
        private int[] unmatchedIndices;

        public ForecastRiverPanel(TimeSearcherForm frm, TimePanel tp, DataSet dataSet, int variableIndex) : this(frm, tp, dataSet, variableIndex, 50, 0, dataSet.NumTimePoints - 1)
        {
        }

        public ForecastRiverPanel(TimeSearcherForm frm, TimePanel tp, DataSet dataSet, int variableIndex, int margin, int start, int end) : base(frm, dataSet, variableIndex, margin, start, end)
        {
            this.timePanel = tp;
            base.MouseEnter += new EventHandler(this.ForecastRiverPanel_MouseEnter);
            base.MouseLeave += new EventHandler(this.ForecastRiverPanel_MouseLeave);
            base.MouseClick += new MouseEventHandler(this.ForecastRiverPanel_MouseClick);
        }

        private void ForecastRiverPanel_MouseClick(object sender, MouseEventArgs e)
        {
            this.timePanel.DeselectAll();
            WizardForm.Instance.SetSettingValues(this.patternStart, this.patternEnd, base.mvrsInfo.Tolerance, base.mvrsInfo.SearchOptions.ToBoolArray, base.mvrsInfo.Variables);
            this.selected = true;
            base.Invalidate();
        }

        private void ForecastRiverPanel_MouseEnter(object sender, EventArgs e)
        {
            this.highlighted = true;
            base.Invalidate();
        }

        private void ForecastRiverPanel_MouseLeave(object sender, EventArgs e)
        {
            this.highlighted = false;
            base.Invalidate();
        }

        protected override void renderGraphsAndAxesRP_Smooth(Graphics grfx)
        {
            TimeSearcherForm tSForm = WizardForm.TSForm;
            grfx.SmoothingMode = SmoothingMode.AntiAlias;
            grfx.FillRectangle(Configuration.appBackgroundBrush, base.ClientRectangle);
            foreach (GraphGap gap in base._graphGaps)
            {
                gap.FillGap(grfx);
            }
            for (int i = 0; i < base._graphs.Length; i++)
            {
                if (i == Configuration.riverGraphMedianIndex)
                {
                    MultiVarResSearchInfo info = base.getMVRSInfo();
                    if (info == null)
                    {
                        base._graphs[i].renderGraph(grfx, Configuration.riverGraphMedianPen);
                    }
                    else
                    {
                        if (base._tsForm.IsForecastSourceVisible)
                        {
                            base._incGraph.renderGraph(grfx, Configuration.riverGraphIncompletePen);
                        }
                        base._graphs[i].RenderSubGraph(grfx, base._startIndex, base._startIndex, info.PatStartIndex, Configuration.riverGraphBeforeForecastPen);
                        base._graphs[i].RenderSubGraph(grfx, base._startIndex, info.PatStartIndex, info.PatEndIndex, Configuration.riverGraphDuringForecastPen);
                        base._graphs[i].RenderSubGraph(grfx, base._startIndex, info.PatEndIndex, base._endIndex, Configuration.riverGraphAfterForecastPen);
                    }
                }
                else
                {
                    base._graphs[i].renderGraph(grfx, Configuration.riverGraphRegularPen);
                }
            }
            if (this.axisEnabled)
            {
                base._vAxis.Paint(grfx);
                base._hAxis.Paint(grfx);
                base._hAxis.PaintSearchTriangles(grfx);
            }
            else
            {
                switch (this.timePanel.Type)
                {
                    case TimePanelType.Tolerance:
                        grfx.DrawString(base.mvrsInfo.Tolerance + "%", WizardForm.TSForm.Font, Brushes.Black, (float) 5f, (float) (base.Size.Height - 20f));
                        break;

                    case TimePanelType.Startpoint:
                        grfx.DrawString(base.DataSet.getTimePointName(this.patternStart).ToString(), WizardForm.TSForm.Font, Brushes.Black, (float) 5f, (float) (base.Size.Height - 20f));
                        break;

                    case TimePanelType.Endpoint:
                        grfx.DrawString(base.DataSet.getTimePointName(this.patternEnd).ToString(), WizardForm.TSForm.Font, Brushes.Black, (float) 5f, (float) (base.Size.Height - 20f));
                        break;
                }
                grfx.DrawString(base._graphs[StatType.MED.Ord].DataVariable.getMaxValue().ToString(), WizardForm.TSForm.Font, Brushes.Black, (float) 5f, (float) 5f);
            }
            if (this.highlighted)
            {
                grfx.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.Aqua)), base.ClientRectangle);
            }
            if (this.selected)
            {
                grfx.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.DarkBlue)), base.ClientRectangle);
            }
        }

        public void Unselect()
        {
            if (this.selected)
            {
                this.selected = false;
                base.Invalidate();
            }
        }

        public bool AxisEnabled
        {
            set
            {
                this.axisEnabled = value;
            }
        }

        public int PatternEnd
        {
            get
            {
                return this.patternEnd;
            }
            set
            {
                this.patternEnd = value;
            }
        }

        public int PatternStart
        {
            get
            {
                return this.patternStart;
            }
            set
            {
                this.patternStart = value;
            }
        }

        public int Tolerance
        {
            get
            {
                return this.tolerance;
            }
            set
            {
                this.tolerance = value;
            }
        }

        public TimeSearcher.Filters.UnmatchedEntity UnmatchedEntity
        {
            get
            {
                return this.unmatchedEntity;
            }
            set
            {
                this.unmatchedEntity = value;
            }
        }

        public int[] UnmatchedIndices
        {
            get
            {
                return this.unmatchedIndices;
            }
            set
            {
                this.unmatchedIndices = value;
            }
        }
    }
}

