namespace TimeSearcher.Wizard
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Panels;
    using TimeSearcher.Search;

    public class ForecastItemPanel : VarIndexedPanel, IForecast
    {
        private TimeSearcher.DataSet _dataSet;
        protected int _endIndex;
        private Graph _graphIP;
        protected GraphCollection _graphs;
        private DataAxisH _hAxis;
        private readonly int _itemIndex;
        private const int _overviewMargin = 0x19;
        protected int _startIndex;
        private readonly TimeSearcherForm _tsForm;
        private DataAxisV _vAxis;
        private MultiVarResSearchInfo mvrsInfo;

        public ForecastItemPanel(TimeSearcherForm frm, int varIndex, TimeSearcher.DataSet dataset) : this(frm, varIndex, dataset, 0, dataset.NumTimePoints - 1)
        {
        }

        public ForecastItemPanel(TimeSearcherForm frm, int varIndex, TimeSearcher.DataSet dataset, int start, int end) : base(varIndex)
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.Selectable, true);
            this._startIndex = start;
            this._endIndex = end;
            this._tsForm = frm;
            this._dataSet = dataset;
            this.initAxes();
            this.initGraphs(base._varIndex, this._itemIndex);
            base.ResizeRedraw = true;
            base.Resize += new EventHandler(this.OnResize);
            base.Paint += new PaintEventHandler(this.OnPaint);
        }

        private void ALEXrenderGraphIP(Graphics grfx)
        {
            grfx.SmoothingMode = SmoothingMode.AntiAlias;
            grfx.FillRectangle(Configuration.appBackgroundBrush, base.ClientRectangle);
            this._graphIP.RenderGraphWithMatches(grfx, 0, true);
            this._hAxis.Paint(grfx);
            this._vAxis.Paint(grfx);
        }

        private void buildGraph()
        {
            if ((this._tsForm.WindowState != FormWindowState.Minimized) && this._tsForm.IsDataSetLoaded)
            {
                this._hAxis.setSize(base.ClientRectangle);
                this._vAxis.setSize(base.ClientRectangle);
                this._hAxis.setScale(this._startIndex, this._endIndex);
                this._vAxis.setScale();
                this._graphs.BuildVarGraphs(this._startIndex, this._endIndex);
            }
        }

        private void initAxes()
        {
            base._hMargin = 0x19;
            base._vMargin = 0x19;
            this._hAxis = new DataAxisH(base.ClientRectangle, this._dataSet, base._hMargin, base._vMargin);
            this._vAxis = new DataAxisV(base.ClientRectangle, this._dataSet, base._varIndex, base._hMargin, base._vMargin);
        }

        private void initGraphs(int varIdx, int itemIdx)
        {
            this._graphs = new GraphCollection(this._dataSet, base._varIndex, this._hAxis.Domain, this._vAxis);
        }

        private void OnPaint(object obj, PaintEventArgs pea)
        {
            if (this._tsForm.IsDataSetLoaded)
            {
                Graphics grfx = pea.Graphics;
                this.renderGraphsAndAxesQP_Smooth(grfx);
            }
        }

        private void OnResize(object obj, EventArgs ea)
        {
            if (this._tsForm.WindowState != FormWindowState.Minimized)
            {
                this.buildGraph();
                base.Invalidate();
            }
        }

        protected virtual void renderGraphsAndAxesQP_Smooth(Graphics grfx)
        {
            grfx.SmoothingMode = SmoothingMode.AntiAlias;
            grfx.FillRectangle(Configuration.appBackgroundBrush, base.ClientRectangle);
            this._graphs.renderGraphs(grfx, 0);
            bool isForecastSourceVisible = this._tsForm.IsForecastSourceVisible;
            this._hAxis.Paint(grfx);
            this._vAxis.Paint(grfx);
            this._hAxis.PaintSearchTriangles(grfx);
        }

        public void Reset()
        {
            this.initGraphs(base._varIndex, this._itemIndex);
            this.buildGraph();
            base.Invalidate();
        }

        public void SetStartEndIndex(int startIdx, int endIdx)
        {
            this._startIndex = startIdx;
            this._endIndex = endIdx;
            this.buildGraph();
            base.Invalidate();
        }

        public TimeSearcher.DataSet DataSet
        {
            get
            {
                return this._dataSet;
            }
            set
            {
                this._dataSet = value;
            }
        }

        public MultiVarResSearchInfo MVRSInfo
        {
            get
            {
                return this.mvrsInfo;
            }
            set
            {
                this.mvrsInfo = value;
            }
        }
    }
}

