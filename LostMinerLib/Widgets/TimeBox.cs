namespace TimeSearcher.Widgets
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Panels;

    public abstract class TimeBox
    {
        protected QueryPanel _queryPanel;
        protected Form _tsForm;
        protected BoxHandle[] handles;
        public int hEndIndex;
        public int hStartIndex;
        private bool isHighlighted;
        public bool isSelected;
        public Point ptEnd;
        public Point ptStart;
        protected Rectangle rectBox;
        protected double vEnd;
        protected double vStart;

        public TimeBox(Point pt, QueryPanel qp, Form tsForm)
        {
            this.InitBox(pt, qp, tsForm);
        }

        protected abstract void afterCommitMove();
        protected abstract void afterMove();
        public bool commit()
        {
            this.initRectBoxWith(this.ptStart, this.ptEnd);
            this.init_hvStartEnd();
            if ((this.rectBox.Width == 0) || (this.rectBox.Height == 0))
            {
                return false;
            }
            this.onCommit();
            return true;
        }

        public void commitMove()
        {
            this.init_hvStartEnd();
            this.showAllHandles();
            this.afterCommitMove();
        }

        public virtual bool containsPoint(Point pt)
        {
            return this.overTimebox(pt);
        }

        public bool coversValue(double yValue)
        {
            return ((Math.Min(this.vStart, this.vEnd) <= yValue) && (yValue <= Math.Max(this.vStart, this.vEnd)));
        }

        public abstract Color getBoxInteriorColor();
        protected Cursor getCursor(CURSOR_LOC cursorLoc)
        {
            switch (cursorLoc)
            {
                case CURSOR_LOC.BOX:
                    return Cursors.SizeAll;

                case CURSOR_LOC.HANDLE:
                    return Cursors.Cross;

                case CURSOR_LOC.TOLERANCE:
                    return Cursors.UpArrow;

                case CURSOR_LOC.SEARCHBUTTON:
                    return Cursors.Hand;
            }
            return Cursors.Default;
        }

        public virtual CURSOR_LOC getCursorLoc(Point mousePosition)
        {
            if (this.overHandle(mousePosition))
            {
                return CURSOR_LOC.HANDLE;
            }
            if (this.overTimebox(mousePosition))
            {
                return CURSOR_LOC.BOX;
            }
            return CURSOR_LOC.NONE;
        }

        public int getHandle(Point pt)
        {
            for (int i = 0; i < 8; i++)
            {
                if (this.handles[i].containsPoint(pt))
                {
                    return i;
                }
            }
            return -1;
        }

        protected int getVarIndex()
        {
            return this._queryPanel.getVarIndex();
        }

        private void init_hvStartEnd()
        {
            this.hStartIndex = this._queryPanel.HAxis.getIndexFromCoordinate(this.rectBox.Left);
            this.hEndIndex = this._queryPanel.HAxis.getIndexFromCoordinate(this.rectBox.Right);
            this.vStart = (double) this._queryPanel.VAxis.getValueFromCoordinate(this.rectBox.Bottom);
            this.vEnd = (double) this._queryPanel.VAxis.getValueFromCoordinate(this.rectBox.Top);
        }

        protected void InitBox(Point pt, QueryPanel qp, Form tsForm)
        {
            this.ptStart = pt;
            this._queryPanel = qp;
            this._tsForm = tsForm;
            this.isHighlighted = false;
            this.isSelected = false;
            this.handles = new BoxHandle[0];
        }

        private void initBoxHandles()
        {
            this.handles = new BoxHandle[] { new BoxHandle(this.rectBox.X, this.rectBox.Y), new BoxHandle(this.rectBox.X, this.rectBox.Y + (this.rectBox.Height / 2)), new BoxHandle(this.rectBox.X, this.rectBox.Y + this.rectBox.Height), new BoxHandle(this.rectBox.X + (this.rectBox.Width / 2), this.rectBox.Y + this.rectBox.Height), new BoxHandle(this.rectBox.X + this.rectBox.Width, this.rectBox.Y + this.rectBox.Height), new BoxHandle(this.rectBox.X + this.rectBox.Width, this.rectBox.Y + (this.rectBox.Height / 2)), new BoxHandle(this.rectBox.X + this.rectBox.Width, this.rectBox.Y), new BoxHandle(this.rectBox.X + (this.rectBox.Width / 2), this.rectBox.Y) };
        }

        private void initRectBoxWith(Point point1, Point point2)
        {
            this.rectBox.X = Math.Min(point1.X, point2.X);
            this.rectBox.Y = Math.Min(point1.Y, point2.Y);
            this.rectBox.Width = Math.Abs((int) (point1.X - point2.X));
            this.rectBox.Height = Math.Abs((int) (point1.Y - point2.Y));
        }

        public void move(Point mouseDiff, Rectangle fieldBounds)
        {
            this.handles = new BoxHandle[0];
            this.rectBox.Offset(mouseDiff);
            this.rectBox = this.pushRectangleWithinBounds(this.rectBox, fieldBounds);
            this.afterMove();
        }

        protected abstract void onCommit();
        public virtual void onMouseDownAndMove(MouseEventArgs mea, CURSOR_LOC cursorLoc)
        {
        }

        public void onMouseLeft()
        {
            this.setHighlighted(false);
            foreach (BoxHandle handle in this.handles)
            {
                handle.onMouseLeft();
            }
        }

        public void onMouseMoveOnly(MouseEventArgs mea, Point prevMousePosition, Rectangle bounds)
        {
            Point currMousePosition = new Point(mea.X, mea.Y);
            foreach (BoxHandle handle in this.handles)
            {
                handle.onMouseMove(currMousePosition);
            }
            this.setHighlighted(this.containsPoint(currMousePosition));
            this.getCursorLoc(currMousePosition);
            this._queryPanel.MainCursor = this.getCursor(this.getCursorLoc(currMousePosition));
        }

        public virtual void onMouseUp(MouseEventArgs mea)
        {
        }

        public bool onQP(QueryPanel qPanel)
        {
            return (qPanel == this._queryPanel);
        }

        public void onResizeRelocate()
        {
            this.recoverRectBoxFrom_hvStartEnd();
            this.showAllHandles();
        }

        protected bool overHandle(Point pt)
        {
            for (int i = 0; i < this.handles.Length; i++)
            {
                if (this.handles[i].containsPoint(pt))
                {
                    return true;
                }
            }
            return false;
        }

        protected bool overTimebox(Point pt)
        {
            return this.rectBox.Contains(pt);
        }

        public abstract void Paint(Graphics g, QueryPanel qp);
        protected void PaintBoxAndHandles(Graphics g, QueryPanel qp)
        {
            try
            {
                Pen pen = this.isHighlighted ? Configuration.highlightedTimeBoxPen : Configuration.enabledTimeBoxPen;
                SolidBrush brush = new SolidBrush(this.getBoxInteriorColor());
                g.CompositingMode = CompositingMode.SourceOver;
                g.FillRectangle(brush, this.rectBox);
                g.DrawRectangle(pen, this.rectBox);
                if (this.isSelected)
                {
                    foreach (BoxHandle handle in this.handles)
                    {
                        handle.Paint(g);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void paintIntermediate(Point lastPoint, Graphics g, QueryPanel qp)
        {
            if (this._queryPanel == qp)
            {
                this.ptEnd = lastPoint;
                this.initRectBoxWith(this.ptStart, this.ptEnd);
                SolidBrush brush = new SolidBrush(Configuration.incompleteTimeBoxColor);
                g.CompositingMode = CompositingMode.SourceOver;
                g.FillRectangle(brush, this.rectBox);
                g.DrawRectangle(Configuration.timeBoxPen, this.rectBox);
            }
        }

        private Rectangle pushRectangleWithinBounds(Rectangle newRect, Rectangle bounds)
        {
            if (newRect.Left < bounds.Left)
            {
                newRect.X = bounds.Left;
            }
            if (newRect.Right > bounds.Right)
            {
                newRect.X = bounds.Right - newRect.Width;
            }
            if (newRect.Top < bounds.Top)
            {
                newRect.Y = bounds.Top;
            }
            if (newRect.Bottom > bounds.Bottom)
            {
                newRect.Y = bounds.Bottom - newRect.Height;
            }
            return newRect;
        }

        private void recoverRectBoxFrom_hvStartEnd()
        {
            this.rectBox.X = this._queryPanel.HAxis.getCoordinateFromIndex(this.hStartIndex);
            this.rectBox.Y = this._queryPanel.VAxis.getCoordinateFromValue(this.vEnd);
            this.rectBox.Width = this._queryPanel.HAxis.getCoordinateFromIndex(this.hEndIndex) - this.rectBox.X;
            this.rectBox.Height = this._queryPanel.VAxis.getCoordinateFromValue(this.vStart) - this.rectBox.Y;
        }

        protected void resize(int handle, int dx, int dy)
        {
            switch (handle)
            {
                case 0:
                    this.rectBox.X += dx;
                    this.rectBox.Y += dy;
                    this.rectBox.Width -= dx;
                    this.rectBox.Height -= dy;
                    break;

                case 1:
                    this.rectBox.X += dx;
                    this.rectBox.Width -= dx;
                    break;

                case 2:
                    this.rectBox.X += dx;
                    this.rectBox.Width -= dx;
                    this.rectBox.Height += dy;
                    break;

                case 3:
                    this.rectBox.Height += dy;
                    break;

                case 4:
                    this.rectBox.Width += dx;
                    this.rectBox.Height += dy;
                    break;

                case 5:
                    this.rectBox.Width += dx;
                    break;

                case 6:
                    this.rectBox.Y += dy;
                    this.rectBox.Width += dx;
                    this.rectBox.Height -= dy;
                    break;

                case 7:
                    this.rectBox.Y += dy;
                    this.rectBox.Height -= dy;
                    break;
            }
            this.showAllHandles();
        }

        public abstract void resizeUsingHandle(int handle, Point mouseDiff);
        private void setHighlighted(bool shdHighlight)
        {
            if (this.isHighlighted != shdHighlight)
            {
                this.isHighlighted = shdHighlight;
                this._queryPanel.Invalidate();
            }
        }

        public virtual void showAllHandles()
        {
            this.initBoxHandles();
        }

        public enum CURSOR_LOC
        {
            BOX,
            HANDLE,
            TOLERANCE,
            SEARCHBUTTON,
            NONE
        }
    }
}

