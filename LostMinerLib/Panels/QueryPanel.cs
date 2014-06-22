namespace TimeSearcher.Panels
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Search;
    using TimeSearcher.Widgets;
    using System.Collections;

    public class QueryPanel : VarIndexedPanel
    {
        private Bitmap _baseBitmap;
        private bool _bDrag;
        public Graphics _bmpGrfx;
        private static Point _currMousePosition;
        private readonly TimeSearcher.DataSet _dataSet;
        protected int _endIndex;
        protected GraphCollection _graphs;
        protected DataAxisH _hAxis;
        protected Graph _incGraph;
        private DataItem _incItem;
        private TimeBox _incompleteTimeBox;
        private Point _lastPoint;
        protected int _margin;
        private static Point _oldMouseMovePosition;
        private PanelPaintState _prevPps;
        protected int _startIndex;
        private int _sx_index_end;
        private int _sx_index_start;
        private readonly TimeBoxManager _timeBoxManager;
        private const int _tolerance = 10;
        protected TimeSearcherForm _tsForm;
        protected DataAxisV _vAxis;
        protected const int defaultMargin = 50;
        private ArrayList warn_zone;

        public QueryPanel(TimeSearcherForm frm, TimeSearcher.DataSet dataSet, int variableIndex) : this(frm, dataSet, variableIndex, 50, 0, dataSet.NumTimePoints - 1)
        {
        }

        public QueryPanel(TimeSearcherForm frm, TimeSearcher.DataSet dataSet, int variableIndex, int margin, int start, int end) : base(variableIndex)
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.Selectable, true);
            this._bDrag = false;
            this._margin = margin;
            this._tsForm = frm;
            this._dataSet = dataSet;
            this._timeBoxManager = this._dataSet.TimeBoxManager;
            _currMousePosition = new Point(-1, -1);
            this._startIndex = start;
            this._endIndex = end;
            this.initAxesAndGraphs();
            this.initBaseBitmapAndPps();
            this.initIncItem(this.GetEmptyIncItem());
            base.ResizeRedraw = true;
            base.Paint += new PaintEventHandler(this.OnPaint);
            base.Resize += new EventHandler(this.OnResize);
            base.MouseMove += new MouseEventHandler(this.OnMouseMove);
            base.MouseDown += new MouseEventHandler(this.OnMouseDown);
            base.MouseUp += new MouseEventHandler(this.OnMouseUp);
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
                this._graphs.BuildVarGraphs(s, e);
                this._incGraph.BuildGraph(s, e);
            }
        }

        public void clearResults()
        {
            this._graphs.clearResults();
            this._hAxis.clearResults();
            this._tsForm.OVP.clearResults(base._varIndex);
        }

        private void commitTimeBox()
        {
            if (this._bDrag)
            {
                this._bDrag = false;
                if (this._incompleteTimeBox.commit())
                {
                    string x_start_value = this._tsForm.DataSet.getTimePointName(this._incompleteTimeBox.hStartIndex);
                    string x_end_value = this._tsForm.DataSet.getTimePointName(this._incompleteTimeBox.hEndIndex);
                    this._tsForm.search_b_time = x_start_value;
                    this._tsForm.search_e_time = x_end_value;
                    this._tsForm._tbPanel.b_e_time.Text = "  选择时间范围为:" + x_start_value + "-" + x_end_value;
                    this._timeBoxManager.addTimeBox(this._incompleteTimeBox);
                }
                this._incompleteTimeBox = null;
                this._tsForm.StrMode = "select";
            }
        }

        public void displayResults()
        {
            base.Invalidate();
            this._tsForm.OVP.invalidateOverview();
        }

        public DataItem GetEmptyIncItem()
        {
            return DataItem.GetEmptyItem(this._dataSet.NumTimePoints, this._dataSet.NumDynVar);
        }

        public int getEndIndex()
        {
            return this._endIndex;
        }

        public int[] GetItemIdxHavingResults()
        {
            return this._graphs.GetItemIdxHavingResults();
        }

        public int getStartIndex()
        {
            return this._startIndex;
        }

        public int getVarIndex()
        {
            return base._varIndex;
        }

        private void initAxesAndGraphs()
        {
            base._hMargin = this._margin;
            base._vMargin = this._margin / 2;
            this._hAxis = new DataAxisH(base.ClientRectangle, this._dataSet, base._hMargin, base._vMargin);
            this._vAxis = new DataAxisV(base.ClientRectangle, this._dataSet, base._varIndex, base._hMargin, base._vMargin);
            this._graphs = new GraphCollection(this._dataSet, base._varIndex, this._hAxis.Domain, this._vAxis);
        }

        private void initBaseBitmapAndPps()
        {
            this._baseBitmap = new Bitmap(base.Width, base.Height);
            this._bmpGrfx = Graphics.FromImage(this._baseBitmap);
            this._bmpGrfx.SmoothingMode = SmoothingMode.None;
            base.CreateGraphics().SmoothingMode = SmoothingMode.None;
            this._bmpGrfx.FillRectangle(Configuration.appBackgroundBrush, 0, 0, this._baseBitmap.Width, this._baseBitmap.Height);
            this._hAxis.Paint(this._bmpGrfx);
            this._vAxis.Paint(this._bmpGrfx);
            this._prevPps = new PanelPaintState(this._graphs, this._dataSet.NumItems);
        }

        public void initIncItem(DataItem incItem)
        {
            this._incItem = incItem;
            this._incGraph = new Graph(incItem.GetVar(base._varIndex), this._hAxis.Domain, this._vAxis);
        }

        private void OnMouseDown(object obj, MouseEventArgs mea)
        {
            _currMousePosition = new Point(mea.X, mea.Y);
            switch (this._tsForm.StrMode)
            {
                case "select":
                    if (!this._timeBoxManager.IsMouseOverAnyTimeBox(this, _currMousePosition))
                    {
                        if (!this.pickGraph(mea))
                        {
                            this.selectTimeLine(mea.X);
                        }
                        this._timeBoxManager.UpdateSelectionOfTBs(this, this._tsForm.IsControlKeyDown, mea);
                        break;
                    }
                    if (!this._timeBoxManager.getTBmouseIsOver(this, _oldMouseMovePosition).isSelected)
                    {
                        this._timeBoxManager.UpdateSelectionOfTBs(this, this._tsForm.IsControlKeyDown, mea);
                    }
                    break;

                case "timebox":
                    this._incompleteTimeBox = new FilterBox(_currMousePosition, this, this._tsForm);
                    break;

                case "searchbox":
                    this._incompleteTimeBox = new SearchBox(_currMousePosition, this, this._tsForm);
                    break;
            }
        }

        private void OnMouseMove(object obj, MouseEventArgs mea)
        {
            _currMousePosition = new Point(mea.X, mea.Y);
            if ((_oldMouseMovePosition != _currMousePosition) && !this._tsForm.bUpdating)
            {
                this._bDrag = mea.Button == MouseButtons.Left;
                this._tsForm.showStatus(this._hAxis, this._vAxis, _currMousePosition);
                if (!this._bDrag)
                {
                    bool flag = this.pickGraph(mea);
                    if (this._timeBoxManager.mouseMoveOnly(this, _oldMouseMovePosition, mea) || flag)
                    {
                        base.Invalidate();
                    }
                }
                else
                {
                    string mode_str = this._tsForm.StrMode;
                    if ((mode_str!= null) && (mode_str == "select"))
                    {
                        this._timeBoxManager.mouseDownAndMove(this, _oldMouseMovePosition, mea);
                        this._timeBoxManager.commitMoveOnSelectedTBs(mea);
                    }
                    base.Invalidate();
                }
                this._lastPoint = _currMousePosition;
                _oldMouseMovePosition = new Point(mea.X, mea.Y);
            }
        }

        private void OnMouseUp(object obj, MouseEventArgs mea)
        {
            if (!this._tsForm.bUpdating)
            {
                _currMousePosition = new Point(mea.X, mea.Y);
                string mode_str = this._tsForm.StrMode;
                if (mode_str != null)
                {
                    if (!(mode_str== "select"))
                    {
                        if (mode_str == "timebox")
                        {
                            this.commitTimeBox();
                            this._tsForm.RefillItemsList();
                        }
                        else if (mode_str == "searchbox")
                        {
                            this.commitTimeBox();
                        }
                    }
                    else
                    {
                        if (this._bDrag)
                        {
                            this._bDrag = false;
                            this._timeBoxManager.commitMoveOnSelectedTBs(mea);
                            if (this._timeBoxManager.SelectedTBsContainDisablingEntities())
                            {
                                this._tsForm.RefillItemsList();
                            }
                        }
                        Point p = new Point(mea.X, mea.Y);
                        this._timeBoxManager.get_curr_loc(this, p, mea);
                    }
                }
                this._tsForm.invalidateVisiblePanels();
            }
        }

        private void OnPaint(object obj, PaintEventArgs pea)
        {
            if (this._tsForm.IsDataSetLoaded)
            {
                int num;
                Graphics grfx = pea.Graphics;
                this.renderGraphsAndAxesQP_Smooth(grfx);
                if (this._tsForm.ShdDrawTimeLine && (this._dataSet.currTimePtIndex >= this._startIndex))
                {
                    num = this._hAxis.getCoordinateFromIndex(this._dataSet.currTimePtIndex);
                    grfx.DrawLine(Configuration.timeLinePen, num, base.Bounds.Top, num, base.Bounds.Bottom);
                }
                if ((this._sx_index_start > 0) && (this._sx_index_end > 0))
                {
                    num = this._hAxis.getCoordinateFromIndex(this._sx_index_start);
                    grfx.DrawLine(Configuration.wran_pen, num, base.Bounds.Top, num, base.Bounds.Bottom);
                    int num1 = this._hAxis.getCoordinateFromIndex(this._sx_index_end);
                    grfx.DrawLine(Configuration.wran_pen, num1, base.Bounds.Top, num1, base.Bounds.Bottom);
                }
                //if (warn_zone.Count > 0)
                //{
                //    for (int i = 0; i < warn_zone.Count; i++)
                //    {
                //             num = this._hAxis.getCoordinateFromIndex(int.Parse(warn_zone[i].ToString().Split(';')[0]));
                //            grfx.DrawLine(Configuration.wran_pen, num, base.Bounds.Top, num, base.Bounds.Bottom);
                //            int num1 = this._hAxis.getCoordinateFromIndex(int.Parse(warn_zone[i].ToString().Split(';')[1]));
                //            grfx.DrawLine(Configuration.wran_pen, num1, base.Bounds.Top, num1, base.Bounds.Bottom);
                //    }
                //}
                this._timeBoxManager.paintAllBoxes(grfx, this);
                if (this._bDrag && ((this._tsForm.StrMode == "timebox") || (this._tsForm.StrMode == "searchbox")))
                {
                    this._incompleteTimeBox.paintIntermediate(this._lastPoint, grfx, this);
                }
            }
        }

        private void OnResize(object obj, EventArgs ea)
        {
            if (this._tsForm.WindowState != FormWindowState.Minimized)
            {
                this.buildGraph();
                this.initBaseBitmapAndPps();
                this._timeBoxManager.resizeRelocateAllBoxes(this);
            }
        }

        private bool pickGraph(MouseEventArgs mea)
        {
            int num = this._hAxis.getFloorIndexFromCoordinate(mea.X);
            if ((this._hAxis.contains(_currMousePosition) && this._vAxis.contains(_currMousePosition)) && ((num <= (this._dataSet.NumTimePoints - 2)) && (num >= 0)))
            {
                double num4;
                int[] enabledItemIdx = this._dataSet.GetEnabledItemIdx();
                int localTpIdx = num - this._startIndex;
                localTpIdx = this._graphs.getGraph(0).moveLineWithinBounds(localTpIdx);
                Point[] pointArray = new Point[enabledItemIdx.Length];
                Point[] pointArray2 = new Point[enabledItemIdx.Length];
                for (int i = 0; i < enabledItemIdx.Length; i++)
                {
                    pointArray[i] = this._graphs.getGraph(enabledItemIdx[i]).GetPoint(localTpIdx);
                    pointArray2[i] = this._graphs.getGraph(enabledItemIdx[i]).GetPoint(localTpIdx + 1);
                }
                int index = Utils.NearestSegIndex(new Point(mea.X, mea.Y), pointArray, pointArray2, out num4);
                if (num4 > 10.0)
                {
                    if (this._dataSet.HighlightedItemIdx != -1)
                    {
                        this._dataSet.HighlightedItemIdx = -1;
                        return true;
                    }
                    return false;
                }
                int num6 = enabledItemIdx[index];
                DataItemStatus status = this._dataSet.GetItem(num6).Status;
                //删除选中线后，显示用户详细数据
                //if (status.Highlighted)
                //{
                //    if (mea.Button == MouseButtons.Left)
                //    {
                        
                //        this._tsForm.ItemsListManager.SelectDataItem(num6);
                //        this._dataSet.HighlightedItemIdx = -1;
                //        return true;
                //    }
                //    return false;
                //}
                if (status.Selected)
                {
                    //删除选中线后，显示用户详细数据
                    //if (mea.Button == MouseButtons.Left)
                    //{
                    //    this._tsForm.ItemsListManager.SelectDataItem(num6);
                    //    this._dataSet.HighlightedItemIdx = -1;
                    //    return true;
                    //}
                    if (this._dataSet.HighlightedItemIdx != -1)
                    {
                        this._dataSet.HighlightedItemIdx = -1;
                        return true;
                    }
                    return false;
                }
                if (status.Enabled)
                {
                    this._dataSet.HighlightedItemIdx = num6;
                    return true;
                }
                Console.WriteLine("Unexpected item status in QP.pickGraph(): please report.");
            }
            return false;
        }

        private bool pickGraph2(MouseEventArgs mea)
        {
            bool flag = false;
            int localTpIdx = this._hAxis.getFloorIndexFromCoordinate(mea.X);
            if ((this._hAxis.contains(_currMousePosition) && this._vAxis.contains(_currMousePosition)) && ((localTpIdx <= (this._dataSet.NumTimePoints - 2)) && (localTpIdx >= 0)))
            {
                localTpIdx -= this._startIndex;
                Point ptMouse = new Point(mea.X, mea.Y);
                for (int i = 0; (i < this._dataSet.NumItems) && !flag; i++)
                {
                    localTpIdx = this._graphs.getGraph(i).moveLineWithinBounds(localTpIdx);
                    Point point = this._graphs.getGraph(i).GetPoint(localTpIdx);
                    Point point2 = this._graphs.getGraph(i).GetPoint(localTpIdx + 1);
                    if (mea.Button == MouseButtons.Left)
                    {
                        if (Utils.pointFitLine(point, point2, ptMouse, 10.0))
                        {
                            this._tsForm.ItemsListManager.SelectDataItem(i);
                            flag = true;
                        }
                    }
                    else
                    {
                        DataItemStatus status = this._dataSet.GetItem(i).Status;
                        if ((status.Enabled && !status.Selected) && Utils.pointFitLine(point, point2, ptMouse, 10.0))
                        {
                            this._dataSet.HighlightedItemIdx = i;
                            flag = true;
                        }
                    }
                }
            }
            bool flag2 = false;
            if (!(flag || (this._dataSet.HighlightedItemIdx == -1)))
            {
                this._dataSet.HighlightedItemIdx = -1;
                flag2 = true;
            }
            if (!flag)
            {
                return flag2;
            }
            return true;
        }

        protected virtual void renderGraphsAndAxesQP(Graphics grfx)
        {
            PanelPaintState state = new PanelPaintState(this._dataSet, this._graphs);
            PanelPaintStateDiff ppsDiff = this._prevPps.GetDiff(state);
            this._graphs.renderGraphs(this._bmpGrfx, ppsDiff, this._startIndex);
            this._prevPps = state;
            grfx.DrawImage(this._baseBitmap, 0, 0);
            this._hAxis.PaintSearchTriangles(grfx);
        }

        protected virtual void renderGraphsAndAxesQP_Smooth(Graphics grfx)
        {
            grfx.SmoothingMode = SmoothingMode.AntiAlias;
            grfx.FillRectangle(Configuration.appBackgroundBrush, base.ClientRectangle);
            this._graphs.renderGraphs(grfx, this._startIndex);
            if (this._tsForm.IsForecastSourceVisible)
            {
                this._incGraph.renderGraph(grfx, Configuration.riverGraphIncompletePen);
            }
            this._hAxis.Paint(grfx);
            this._vAxis.Paint(grfx);
            this._hAxis.PaintSearchTriangles(grfx);
        }

        private void selectTimeLine(int xCoord)
        {
            int timePtIdx = this._hAxis.getIndexFromCoordinate(xCoord);
            if (timePtIdx != -1)
            {
                this._tsForm.SelectTimePt(timePtIdx);
            }
        }

        public void set_warn_zone(ArrayList al)
        {
            warn_zone = al;
           // this._sx_index_start = index_start;
           // this._sx_index_end = index_end;
        }
        public void set_sx_index(int index_start, int index_end)
        {
            this._sx_index_start = index_start;
            this._sx_index_end = index_end;
        }

        public void setResults(SearchResults searchResults)
        {
            this._graphs.setResults(searchResults);
            this._hAxis.setResults(searchResults);
            this._tsForm.OVP.setResults(searchResults);
        }

        public void setStartEndIndex(int startIdx, int endIdx)
        {
            this._startIndex = startIdx;
            this._endIndex = endIdx;
            this.buildGraph();
            this.initBaseBitmapAndPps();
            this._timeBoxManager.resizeRelocateAllBoxes(this);
            base.Invalidate();
        }

        public void UpdateIncItem(DataItem incItem)
        {
            this.initIncItem(incItem);
            this.buildGraph();
            base.Invalidate();
        }

        public TimeSearcher.DataSet DataSet
        {
            get
            {
                return this._dataSet;
            }
        }

        public DataAxisH HAxis
        {
            get
            {
                return this._hAxis;
            }
        }

        public Cursor MainCursor
        {
            set
            {
                this._tsForm.Cursor = value;
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

