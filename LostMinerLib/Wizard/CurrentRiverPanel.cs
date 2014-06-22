namespace TimeSearcher.Wizard
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher;

    public class CurrentRiverPanel : ForecastRiverPanel
    {
        private bool dragLeftWidget;
        private bool dragRightWidget;
        private WizardForm owner;
        private RectangleWidget patternArea;
        private RectangleWidget resultArea;

        public CurrentRiverPanel(WizardForm form, TimePanel tp, DataSet dataSet, int variableIndex) : base(WizardForm.TSForm, tp, dataSet, variableIndex, 50, 0, dataSet.NumTimePoints - 1)
        {
            this.owner = form;
            this.patternArea = new RectangleWidget(this, new Rectangle(base._margin, 0, 100, 0xa5), Color.Blue, Color.LightBlue);
            this.resultArea = new RectangleWidget(this, new Rectangle(200, 0, 150, 0xa5), Color.Yellow, Color.LightYellow);
            base.AxisEnabled = true;
            base.MouseUp += new MouseEventHandler(this.CurrentRiverPanel_MouseUp);
            base.MouseDown += new MouseEventHandler(this.CurrentRiverPanel_MouseDown);
            base.MouseMove += new MouseEventHandler(this.CurrentRiverPanel_MouseMove);
            this.patternArea.ValueChanged += new EventHandler(this.patternArea_ValueChanged);
            this.resultArea.ValueChanged += new EventHandler(this.resultArea_ValueChanged);
        }

        private void CurrentRiverPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.resultArea.MouseDown(e.Location))
            {
                this.dragRightWidget = true;
            }
            else if (this.patternArea.MouseDown(e.Location))
            {
                this.dragLeftWidget = true;
            }
        }

        private void CurrentRiverPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.dragLeftWidget)
            {
                this.patternArea.MouseMove(e.Location);
            }
            else if (this.dragRightWidget)
            {
                this.resultArea.MouseMove(e.Location);
            }
        }

        private void CurrentRiverPanel_MouseUp(object sender, MouseEventArgs e)
        {
            this.patternArea.MouseUp();
            this.resultArea.MouseUp();
            this.dragLeftWidget = this.dragRightWidget = false;
        }

        public int GetCoordinateFromTimepoint(int tp)
        {
            return base._hAxis.getCoordinateFromIndex(tp);
        }

        public int GetTimepointFromCoordinate(int x)
        {
            return base._hAxis.getIndexFromCoordinate(x);
        }

        private void patternArea_ValueChanged(object sender, EventArgs e)
        {
            this.owner.UpdateSliders(this.patternArea.StartPointIndex, this.patternArea.EndPointIndex);
            TimePanel.Update(this.owner.Panel.Controls, this.owner.PreviewState);
            foreach (Control control in this.owner.Panel.Controls)
            {
                TimePanel panel = control as TimePanel;
                if ((panel.Type == TimePanelType.SearchSettings) || (panel.Type == TimePanelType.Variables))
                {
                    panel.UpdateArea(this.patternArea.StartPointIndex, this.patternArea.EndPointIndex, this.resultArea.StartPointIndex, this.resultArea.EndPointIndex);
                }
            }
        }

        protected override void renderGraphsAndAxesRP_Smooth(Graphics grfx)
        {
            base.renderGraphsAndAxesRP_Smooth(grfx);
            this.patternArea.Draw(grfx);
            this.resultArea.Draw(grfx);
        }

        private void resultArea_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control control in this.owner.Panel.Controls)
            {
                TimePanel panel = control as TimePanel;
                if ((panel.Type == TimePanelType.SearchSettings) || (panel.Type == TimePanelType.Variables))
                {
                    panel.UpdateArea(this.patternArea.StartPointIndex, this.patternArea.EndPointIndex, this.resultArea.StartPointIndex, this.resultArea.EndPointIndex);
                }
                else
                {
                    panel.UpdateArea(this.resultArea.StartPointIndex, this.resultArea.EndPointIndex);
                }
            }
        }

        public void SetInnerSize(Size size)
        {
            int max = size.Width - (base._margin / 4);
            float minPixels = ((float) max) / ((float) (base.DataSet.NumTimePoints - 1));
            base.Size = size;
            this.resultArea.SetExtents(base._margin, max, minPixels);
            this.patternArea.SetExtents(base._margin, max, minPixels);
        }

        public void UpdateForecast(PreviewState state)
        {
            base.PatternStart = state.MVRSInfo.PatStartIndex;
            base.PatternEnd = state.MVRSInfo.PatEndIndex;
            TimePanel.InitForecast(this, state.MVRSInfo.SearchOptions, state.MVRSInfo.Tolerance, state.MVRSInfo.Variables);
        }

        public RectangleWidget PatternArea
        {
            get
            {
                return this.patternArea;
            }
        }

        public RectangleWidget ResultArea
        {
            get
            {
                return this.resultArea;
            }
        }
    }
}

