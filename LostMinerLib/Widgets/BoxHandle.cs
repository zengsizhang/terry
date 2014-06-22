namespace TimeSearcher.Widgets
{
    using System;
    using System.Drawing;
    using TimeSearcher;

    public class BoxHandle
    {
        private Pen _borderPen;
        private const int _fullSide = 6;
        private const int _halfSide = 3;
        private bool _isSelected;
        private Rectangle _rectBounds;

        public BoxHandle(int x, int y)
        {
            this._rectBounds = new Rectangle(x - 3, y - 3, 6, 6);
        }

        public bool containsPoint(Point pt)
        {
            Rectangle rectangle = this._rectBounds;
            rectangle.Inflate(4, 4);
            return rectangle.Contains(pt);
        }

        public void onMouseLeft()
        {
            this._isSelected = false;
        }

        public void onMouseMove(Point currMousePosition)
        {
            this._isSelected = this.containsPoint(currMousePosition);
        }

        public void Paint(Graphics g)
        {
            if (this._isSelected)
            {
                this._borderPen = Configuration.selectedBorderBoxHandlePen;
            }
            else
            {
                this._borderPen = Configuration.unselectedBorderBoxHandlePen;
            }
            g.FillRectangle(new SolidBrush(Configuration.boxHandleBackgroundPen.Color), this._rectBounds);
            g.DrawRectangle(this._borderPen, this._rectBounds);
        }
    }
}

