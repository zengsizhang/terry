namespace TimeSearcher.Panels
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Search;

    public class RiverPanel : VarIndexedPanel
    {
        protected int _endIndex;
        protected GraphGap[] _graphGaps;
        protected Graph[] _graphs;
        protected DataAxisH _hAxis;
        protected Graph _incGraph;
        private DataItem _incItem;
        protected int _margin;
        protected int _startIndex;
        private TimeSearcher.DataSet _statDataSet;
        private const int _tolerance = 10;
        protected TimeSearcherForm _tsForm;
        protected DataAxisV _vAxis;
        protected const int defaultMargin = 50;
        protected MultiVarResSearchInfo mvrsInfo;

        public RiverPanel()
        {
        }

        public RiverPanel(TimeSearcherForm frm, TimeSearcher.DataSet dataSet, int variableIndex) : this(frm, dataSet, variableIndex, 50, 0, dataSet.NumTimePoints - 1)
        {
        }

        public RiverPanel(TimeSearcherForm frm, TimeSearcher.DataSet dataSet, int variableIndex, int margin, int start, int end) : base(variableIndex)
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.Selectable, true);
            this._margin = margin;
            this._tsForm = frm;
            this._statDataSet = dataSet;
            this._startIndex = start;
            this._endIndex = end;
            this.initAxesAndGraphs();
            this.initIncItem(this.GetEmptyIncItem());
            base.ResizeRedraw = true;
            base.Paint += new PaintEventHandler(this.OnPaint);
            base.Resize += new EventHandler(this.OnResize);
        }

        private void buildGraph()
        {
            if ((this._tsForm.WindowState != FormWindowState.Minimized) && this._tsForm.IsDataSetLoaded)
            {
                int s = this._startIndex;
                int e = this._endIndex;
                this._hAxis.setSize(base.ClientRectangle);
                this._vAxis.setSize(base.ClientRectangle);
                this._hAxis.setScale(s, e);
                this._vAxis.setScale();
                this._incGraph.BuildGraph(s, e);
                for (int i = 0; i < this._graphs.Length; i++)
                {
                    this._graphs[i].BuildGraph(s, e);
                }
                this.initGraphGaps();
            }
        }

        public DataItem GetEmptyIncItem()
        {
            return DataItem.GetEmptyItem(this._statDataSet.NumTimePoints, this._statDataSet.NumDynVar);
        }

        protected MultiVarResSearchInfo getMVRSInfo()
        {
            MultiVarResSearchInfo mvrsInfo = null;
            if (MultiVarResSearchForm.MVRSFormInstance != null)
            {
                return MultiVarResSearchForm.MVRSFormInstance.MVRSInfo;
            }
            if (this.mvrsInfo != null)
            {
                mvrsInfo = this.mvrsInfo;
            }
            return mvrsInfo;
        }

        private void initAxesAndGraphs()
        {
            base._hMargin = this._margin;
            base._vMargin = this._margin / 2;
            this._hAxis = new DataAxisH(base.ClientRectangle, this._statDataSet, base._hMargin, base._vMargin);
            this._vAxis = new DataAxisV(base.ClientRectangle, this._statDataSet, base._varIndex, base._hMargin, base._vMargin);
            this.initGraphs(this._statDataSet);
        }

        private void initGraphGaps()
        {
            this._graphGaps = new GraphGap[this._graphs.Length - 1];
            for (int i = 0; i < this._graphGaps.Length; i++)
            {
                this._graphGaps[i] = new GraphGap(this._graphs[i + 1], this._graphs[i], Configuration.riverGraphGapBrushes[i]);
            }
        }

        private void initGraphs(TimeSearcher.DataSet statDataSet)
        {
            this._graphs = new Graph[this._statDataSet.NumItems];
            for (int i = 0; i < this._graphs.Length; i++)
            {
                this._graphs[i] = new Graph(this._statDataSet.GetItem(i).GetVar(base._varIndex), this._hAxis.Domain, this._vAxis);
            }
        }

        public void initIncItem(DataItem incItem)
        {
            this._incItem = incItem;
            this._incGraph = new Graph(incItem.GetVar(base._varIndex), this._hAxis.Domain, this._vAxis);
        }

        private void OnPaint(object obj, PaintEventArgs pea)
        {
            if (this._tsForm.IsDataSetLoaded)
            {
                Graphics grfx = pea.Graphics;
                this.renderGraphsAndAxesRP_Smooth(grfx);
            }
        }

        private void OnResize(object obj, EventArgs ea)
        {
            this.buildGraph();
        }

        protected virtual void renderGraphsAndAxesRP_Smooth(Graphics grfx)
        {
            grfx.SmoothingMode = SmoothingMode.AntiAlias;
            grfx.FillRectangle(Configuration.appBackgroundBrush, base.ClientRectangle);
            foreach (GraphGap gap in this._graphGaps)
            {
                gap.FillGap(grfx);
            }
            for (int i = 0; i < this._graphs.Length; i++)
            {
                if (i == Configuration.riverGraphMedianIndex)
                {
                    MultiVarResSearchInfo info = this.getMVRSInfo();
                    if (info == null)
                    {
                        this._graphs[i].renderGraph(grfx, Configuration.riverGraphMedianPen);
                    }
                    else
                    {
                        if (this._tsForm.IsForecastSourceVisible)
                        {
                            this._incGraph.renderGraph(grfx, Configuration.riverGraphIncompletePen);
                        }
                        this._graphs[i].RenderSubGraph(grfx, this._startIndex, this._startIndex, info.PatStartIndex, Configuration.riverGraphBeforeForecastPen);
                        this._graphs[i].RenderSubGraph(grfx, this._startIndex, info.PatStartIndex, info.PatEndIndex, Configuration.riverGraphDuringForecastPen);
                        this._graphs[i].RenderSubGraph(grfx, this._startIndex, info.PatEndIndex, this._endIndex, Configuration.riverGraphAfterForecastPen);
                    }
                }
                else
                {
                    this._graphs[i].renderGraph(grfx, Configuration.riverGraphRegularPen);
                }
            }
            this._hAxis.Paint(grfx);
            this._vAxis.Paint(grfx);
            this._hAxis.PaintSearchTriangles(grfx);
        }

        public void SetStartEndIndex(int startIdx, int endIdx)
        {
            this._startIndex = startIdx;
            this._endIndex = endIdx;
            this.buildGraph();
            base.Invalidate();
        }

        public void UpdateIncItem(DataItem incItem)
        {
            this.initIncItem(incItem);
            this.buildGraph();
            base.Invalidate();
        }

        public void UpdateStatDataSet(TimeSearcher.DataSet newStatDataSet)
        {
            this._statDataSet = newStatDataSet;
            this.initGraphs(this._statDataSet);
            this.buildGraph();
            base.Invalidate();
        }

        public TimeSearcher.DataSet DataSet
        {
            get
            {
                return this._statDataSet;
            }
        }

        public DataAxisH HAxis
        {
            get
            {
                return this._hAxis;
            }
        }

        public MultiVarResSearchInfo MvrsInfo
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

        public DataAxisV VAxis
        {
            get
            {
                return this._vAxis;
            }
        }
    }
}

