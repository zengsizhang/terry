namespace TimeSearcher.Widgets
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using TimeSearcher;

    public class ToleranceHandle
    {
        private bool bHandleHighlighted;
        private bool bindicatorHighlighted;
        private Font fontRegular = new Font(FontFamily.GenericSansSerif, 8f);
        public bool isVisible = true;
        private Size size = new Size(12, 12);
        private int tolerance;
        private Rectangle toleranceBounds;
        private Rectangle toleranceIndicator;

        public ToleranceHandle(Point pt, Size size)
        {
            this.toleranceBounds = new Rectangle(pt, size);
            this.tolerance = 0;
            this.toleranceIndicator = new Rectangle(pt.X, pt.Y + this.tolerance, size.Width, size.Height);
        }

        public void changeToleranceBy(int percChange)
        {
            this.setTolerance(this.tolerance + percChange);
            this.toleranceIndicator.Y = this.getYCoordFromPercTolerance(this.tolerance);
        }

        public bool containsPoint(Point pt)
        {
            this.bindicatorHighlighted = this.toleranceIndicator.Contains(pt);
            this.bHandleHighlighted = this.toleranceBounds.Contains(pt);
            return this.bHandleHighlighted;
        }

        public int getPercTolerance()
        {
            return this.tolerance;
        }

        private int getPercToleranceFromYCoord(int yCoord)
        {
            double num = (((double) (yCoord - this.toleranceBounds.Y)) / ((double) this.toleranceBounds.Height)) * 100.0;
            return (int) num;
        }

        private int getYCoordFromPercTolerance(int percTolerance)
        {
            double num = (((double) (percTolerance * this.toleranceBounds.Height)) / 100.0) + this.toleranceBounds.Y;
            return (int) num;
        }

        public void move(int pt_X, int pt_Y, int width, int height)
        {
            this.toleranceBounds.X = pt_X;
            this.toleranceBounds.Y = pt_Y;
            this.toleranceBounds.Width = width;
            this.toleranceBounds.Height = height;
            this.toleranceIndicator = new Rectangle(pt_X, pt_Y + ((this.tolerance * height) / 100), this.size.Width, 4);
        }

        private int moveYCoordWithinBounds(int yCoord)
        {
            if (yCoord < this.toleranceBounds.Y)
            {
                return this.toleranceBounds.Y;
            }
            if (yCoord > this.toleranceBounds.Bottom)
            {
                return this.toleranceBounds.Bottom;
            }
            return yCoord;
        }

        public void onMouseDownAndMove(MouseEventArgs mea)
        {
            new Rectangle(this.toleranceIndicator.Location, this.toleranceIndicator.Size).Inflate(2, 5);
            this.toleranceIndicator.X = this.toleranceBounds.X;
            this.toleranceIndicator.Y = this.moveYCoordWithinBounds(mea.Y);
            this.setTolerance(this.getPercToleranceFromYCoord(this.toleranceIndicator.Y));
        }

        public void Paint(Graphics g)
        {
            if (this.isVisible)
            {
                Pen pen;
                Point point = new Point(this.toleranceBounds.X, this.toleranceBounds.Y);
                Point point2 = new Point(this.toleranceBounds.X, this.toleranceBounds.Y + this.toleranceBounds.Height);
                Point point3 = new Point(this.toleranceBounds.X + this.toleranceBounds.Width, this.toleranceBounds.Y + this.toleranceBounds.Height);
                Point[] points = new Point[] { point, point2, point3 };
                FillMode winding = FillMode.Winding;
                Brush brush = new LinearGradientBrush(this.toleranceBounds, Pens.LightGreen.Color, Pens.DarkGreen.Color, 90f);
                g.FillPolygon(brush, points, winding);
                if (this.bHandleHighlighted)
                {
                    g.DrawPolygon(Pens.DarkGreen, points);
                }
                if (this.bindicatorHighlighted)
                {
                    pen = new Pen(Color.FromArgb(0xe8, Configuration.toleranceIndicatorBorderSelected.Color));
                }
                else
                {
                    pen = new Pen(Color.FromArgb(0xe8, Color.Black));
                }
                SolidBrush brush2 = new SolidBrush(pen.Color);
                g.CompositingMode = CompositingMode.SourceOver;
                g.FillRectangle(brush2, this.toleranceIndicator);
                g.DrawRectangle(pen, this.toleranceIndicator);
                float x = this.toleranceIndicator.Right + 2;
                float y = this.toleranceIndicator.Y;
                new RectangleF((PointF) this.toleranceIndicator.Location, (SizeF) this.toleranceIndicator.Size);
                string s = this.tolerance.ToString() + "%";
                g.DrawString(s, this.fontRegular, Brushes.Black, x, y);
            }
        }

        private void setTolerance(int toler)
        {
            if (toler < 0)
            {
                this.tolerance = 0;
            }
            else if (toler > 100)
            {
                this.tolerance = 100;
            }
            else
            {
                this.tolerance = toler;
            }
        }
    }
}

