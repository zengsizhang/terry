namespace TimeSearcher.Panels
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Search;

    public class OverviewPanel : VarIndexedPanel
    {
        private Bitmap _baseBitmap;
        private Graphics _bmpGrfx;
        private DataSet _dataSet;
        private EDIT_MODE _editMode;
        private int _endIndex;
        private Point _firstPoint;
        private GraphCollection _graphs;
        private DataAxisH _hAxis;
        private bool _insideOverviewRect1;
        private Point _lastPoint;
        private Point _mousePoint;
        private static Point _oldMouseMovePosition;
        private const int _overviewMargin = 0x19;
        private PanelPaintState _prevPps;
        private Rectangle _rectOverview;
        private const int _rectOverviewLabelInset = 4;
        private int _startIndex;
        private TimeSearcherForm _tsForm;
        private DataAxisV _vAxis;

        public OverviewPanel(TimeSearcherForm frm, int varIndex) : base(varIndex)
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.Selectable, true);
            this._tsForm = frm;
            this._dataSet = this._tsForm.DataSet;
            this._mousePoint = new Point(-1, -1);
            this._startIndex = 0;
            this._endIndex = this._dataSet.NumTimePoints - 1;
            this.initAxesAndGraphs();
            this.initBaseBitmapAndPps();
            base.ResizeRedraw = true;
            base.Paint += new PaintEventHandler(this.OnPaint);
            base.Resize += new EventHandler(this.OnResize);
            base.MouseMove += new MouseEventHandler(this.OnMouseMove);
            base.MouseDown += new MouseEventHandler(this.OnMouseDown);
            base.MouseUp += new MouseEventHandler(this.OnMouseUp);
        }

        private void buildGraph()
        {
            if (this._tsForm.WindowState != FormWindowState.Minimized)
            {
                if (this._tsForm.IsDataSetLoaded)
                {
                    this._hAxis.setSize(base.ClientRectangle);
                    this._vAxis.setSize(base.ClientRectangle);
                    int s = this._startIndex;
                    int e = this._endIndex;
                    this._hAxis.setScale(s, e);
                    this._vAxis.setScale();
                    this._graphs.BuildVarGraphs(s, e);
                }
                int x = this._hAxis.getCoordinateFromIndex(this._tsForm.VarView.getQP(base._varIndex).getStartIndex());
                int num4 = this._hAxis.getCoordinateFromIndex(this._tsForm.VarView.getQP(base._varIndex).getEndIndex());
                this._rectOverview = new Rectangle(x, base.ClientRectangle.Top + 0x19, num4 - x, base.ClientRectangle.Bottom - 50);
                base.Invalidate();
            }
        }

        public void clearResults()
        {
            this._graphs.clearResults();
            this._hAxis.clearResults();
        }

        public DataSet getDataSet()
        {
            return this._dataSet;
        }

        private void initAxesAndGraphs()
        {
            base._hMargin = 0x19;
            base._vMargin = 0x19;
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

        private bool isTimeLineWithinView()
        {
            return ((this._startIndex <= this._dataSet.currTimePtIndex) && (this._dataSet.currTimePtIndex <= this._endIndex));
        }

        private void OnMouseDown(object obj, MouseEventArgs mea)
        {
            this._mousePoint = new Point(mea.X, mea.Y);
            this._firstPoint = this._mousePoint;
        }

        private void OnMouseMove(object obj, MouseEventArgs mea)
        {
            if (_oldMouseMovePosition != new Point(mea.X, mea.Y))
            {
                _oldMouseMovePosition = new Point(mea.X, mea.Y);
                if (!this._tsForm.bUpdating)
                {
                    this._mousePoint = new Point(mea.X, mea.Y);
                    new Point(this._mousePoint.X - this._firstPoint.X, this._mousePoint.Y - this._firstPoint.Y);
                    this._tsForm.showStatus(this._hAxis, this._vAxis, this._mousePoint);
                    if (this._tsForm.StrMode.Equals("select"))
                    {
                        if (mea.Button == MouseButtons.None)
                        {
                            if (this._rectOverview.Contains(this._mousePoint))
                            {
                                int num = 5;
                                Rectangle rectangle = new Rectangle(this._rectOverview.Left - num, this._rectOverview.Top, num * 2, this._rectOverview.Height);
                                Rectangle rectangle2 = new Rectangle(this._rectOverview.Right - num, this._rectOverview.Top, num * 2, this._rectOverview.Height);
                                new Rectangle(this._rectOverview.Left - num, this._rectOverview.Top, num * 2, this._rectOverview.Height);
                                this._insideOverviewRect1 = true;
                                if (rectangle.Contains(this._mousePoint))
                                {
                                    this._editMode = EDIT_MODE.RESIZE_LEFT;
                                    this._tsForm.setBottomPanelCursor(Cursors.SizeWE);
                                }
                                else if (rectangle2.Contains(this._mousePoint))
                                {
                                    this._editMode = EDIT_MODE.RESIZE_RIGHT;
                                    this._tsForm.setBottomPanelCursor(Cursors.SizeWE);
                                }
                                else
                                {
                                    this._editMode = EDIT_MODE.MOVE;
                                    this._tsForm.setBottomPanelCursor(Cursors.SizeAll);
                                }
                            }
                            else
                            {
                                this._tsForm.setBottomPanelCursor(Cursors.Default);
                                this._insideOverviewRect1 = false;
                            }
                        }
                        else
                        {
                            Rectangle rect = this._rectOverview;
                            switch (this._editMode)
                            {
                                case EDIT_MODE.MOVE:
                                    rect.Offset(this._mousePoint.X - this._lastPoint.X, 0);
                                    break;

                                case EDIT_MODE.RESIZE_LEFT:
                                    rect.X += this._mousePoint.X - this._lastPoint.X;
                                    rect.Width -= this._mousePoint.X - this._lastPoint.X;
                                    break;

                                case EDIT_MODE.RESIZE_RIGHT:
                                    rect.Width += this._mousePoint.X - this._lastPoint.X;
                                    break;
                            }
                            if (this._hAxis.coversValidRect(rect))
                            {
                                this._rectOverview = rect;
                                base.Invalidate();
                            }
                        }
                    }
                    this._lastPoint = this._mousePoint;
                }
            }
        }

        private void OnMouseUp(object obj, MouseEventArgs mea)
        {
            if (!this._tsForm.bUpdating)
            {
                this._mousePoint = new Point(mea.X, mea.Y);
                if (this._insideOverviewRect1)
                {
                    int startIdx = this._hAxis.getIndexFromCoordinate(this._rectOverview.Left);
                    int endIdx = this._hAxis.getIndexFromCoordinate(this._rectOverview.Right);
                    this.setStartEndIndexOfDetail(startIdx, endIdx);
                }
            }
        }

        private void OnPaint(object obj, PaintEventArgs pea)
        {
            if (this._tsForm.IsDataSetLoaded)
            {
                Graphics grfx = pea.Graphics;
                this.renderGraphsAndAxesOP_Smooth(grfx);
                if (this._tsForm.ShdDrawTimeLine && this.isTimeLineWithinView())
                {
                    int num = 0;
                    num += this._hAxis.getCoordinateFromIndex(this._dataSet.currTimePtIndex);
                    Pen p = new Pen(Color.Black, 1f);
                    grfx.DrawLine(Configuration.timeLinePen, num, base.Bounds.Top, num, base.Bounds.Bottom);
                }
                grfx.DrawRectangle(Configuration.overviewBoxPen, this._rectOverview);
                this.paintRectOverviewTimePoints(grfx);
            }
        }

        private void OnResize(object obj, EventArgs ea)
        {
            if (this._tsForm.WindowState != FormWindowState.Minimized)
            {
                this.buildGraph();
                this.initBaseBitmapAndPps();
            }
        }

        private void paintCenteredTimePointName(Graphics grfx, int midTimePtXCoord)
        {
            int y = this._rectOverview.Top - 0x10;
            int index = this._hAxis.getIndexFromCoordinate(midTimePtXCoord);
            string text = this._dataSet.getTimePointName(index);
            SizeF ef = grfx.MeasureString(text, this.Font);
            Point location = new Point(midTimePtXCoord - ((int) (ef.Width / 2f)), y);
            Rectangle rect = new Rectangle(location, ef.ToSize());
            rect.Inflate(4, 0);
            grfx.FillRectangle(Brushes.White, rect);
            grfx.DrawRectangle(Pens.Black, rect);
            grfx.DrawString(text, this.Font, Configuration.timePointBrush, (PointF) location);
        }

        private void paintRectOverviewTimePoints(Graphics grfx)
        {
            int coord = (this._rectOverview.Left + this._rectOverview.Right) / 2;
            int bottom = this._rectOverview.Bottom;
            int index = this._hAxis.getIndexFromCoordinate(coord);
            string text = this._dataSet.getTimePointName(index);
            if ((grfx.MeasureString(text, this.Font).Width + 8f) > this._rectOverview.Width)
            {
                this.paintCenteredTimePointName(grfx, coord);
            }
            else
            {
                this.paintCenteredTimePointName(grfx, this._rectOverview.Left);
                this.paintCenteredTimePointName(grfx, this._rectOverview.Right);
            }
        }

        private void renderGraphsAndAxesOP(Graphics grfx)
        {
            PanelPaintState state = new PanelPaintState(this._dataSet, this._graphs);
            PanelPaintStateDiff ppsDiff = this._prevPps.GetDiff(state);
            this._graphs.renderGraphs(this._bmpGrfx, ppsDiff, this._startIndex);
            this._prevPps = state;
            grfx.DrawImage(this._baseBitmap, 0, 0);
            this._hAxis.PaintSearchTriangles(grfx);
        }

        private void renderGraphsAndAxesOP_Smooth(Graphics grfx)
        {
            grfx.SmoothingMode = SmoothingMode.AntiAlias;
            grfx.FillRectangle(Configuration.appBackgroundBrush, base.ClientRectangle);
            this._graphs.renderGraphs(grfx, this._startIndex);
            this._hAxis.Paint(grfx);
            this._vAxis.Paint(grfx);
            this._hAxis.PaintSearchTriangles(grfx);
        }

        public void setResults(SearchResults searchResults)
        {
            this._graphs.setResults(searchResults);
            this._hAxis.setResults(searchResults);
        }

        private void setStartEndIndexOfDetail(int startIdx, int endIdx)
        {
            this.setStartEndIndexOfRect(startIdx, endIdx);
            this.setStartEndIndexOfQPs(startIdx, endIdx);
            this.setStartEndIndexOfRPs(startIdx, endIdx);
            base.Invalidate();
        }

        public void setStartEndIndexOfOverview(int startIdx, int endIdx)
        {
            this._startIndex = startIdx;
            this._endIndex = endIdx;
            this.buildGraph();
            this.setStartEndIndexOfDetail(startIdx, endIdx);
        }

        private void setStartEndIndexOfQPs(int startIdx, int endIdx)
        {
            if (this._tsForm.IsOverviewSynchonized)
            {
                QueryPanel[] qPs = this._tsForm.VarView.QPs;
                for (int i = 0; i < qPs.Length; i++)
                {
                    qPs[i].setStartEndIndex(startIdx, endIdx);
                }
            }
            else
            {
                this._tsForm.VarView.getQP(base._varIndex).setStartEndIndex(startIdx, endIdx);
            }
        }

        private bool setStartEndIndexOfRect(int startIdx, int endIdx)
        {
            int num = this._hAxis.getCoordinateFromIndex(startIdx);
            int num2 = this._hAxis.getCoordinateFromIndex(endIdx);
            if ((this._rectOverview.X == num) && (this._rectOverview.Width == (num2 - num)))
            {
                return false;
            }
            this._rectOverview.X = num;
            this._rectOverview.Width = num2 - num;
            return true;
        }

        private void setStartEndIndexOfRPs(int startIdx, int endIdx)
        {
            if (this._tsForm.IsOverviewSynchonized)
            {
                RiverPanel[] rPs = this._tsForm.RiverView.RPs;
                for (int i = 0; i < rPs.Length; i++)
                {
                    rPs[i].SetStartEndIndex(startIdx, endIdx);
                }
            }
            else
            {
                this._tsForm.RiverView.GetRP(base._varIndex).SetStartEndIndex(startIdx, endIdx);
            }
        }

        private enum EDIT_MODE
        {
            MOVE,
            RESIZE_LEFT,
            RESIZE_RIGHT
        }
    }
}

