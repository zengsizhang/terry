namespace TimeSearcher.Widgets
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using TimeSearcher;

    public class SearchButton
    {
        private Pen borderBoxPen;
        private bool isSelected;
        public bool isVisible;
        private int width;
        private int x;
        private int y;

        public SearchButton(int X, int Y)
        {
            this.setSearchButton(X, Y, Configuration.searchButtonDefaultWidth);
        }

        public SearchButton(int X, int Y, int dimension)
        {
            this.setSearchButton(X, Y, dimension);
        }

        public bool containsPoint(Point pt)
        {
            Rectangle rectangle = new Rectangle(this.x, this.y, this.width, this.width);
            rectangle.Inflate(4, 4);
            this.isSelected = rectangle.Contains(pt);
            return this.isSelected;
        }

        public int getDimension()
        {
            return this.width;
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            try
            {
                int width = 2 * radius;
                Rectangle rectangle = new Rectangle(rect.Location, new Size(width, width));
                GraphicsPath path = new GraphicsPath();
                path.AddArc(rectangle, 180f, 90f);
                rectangle.X = rect.Right - width;
                path.AddArc(rectangle, 270f, 90f);
                rectangle.Y = rect.Bottom - width;
                path.AddArc(rectangle, 0f, 90f);
                rectangle.X = rect.Left;
                path.AddArc(rectangle, 90f, 90f);
                path.CloseFigure();
                return path;
            }
            catch
            {
                return null;
            }
        }

        public void Paint(Graphics g)
        {
            if (this.isVisible)
            {
                Rectangle rect = new Rectangle(this.x, this.y, this.width, this.width);
                if (this.isSelected)
                {
                    this.borderBoxPen = Configuration.selectedBborderSearchBoxPen;
                }
                else
                {
                    this.borderBoxPen = Configuration.unSelectedBorderSearchBoxPen;
                }
                Pen borderSearchBoxPen = Configuration.borderSearchBoxPen;
                Brush brush = new LinearGradientBrush(rect, Pens.LightGray.Color, Pens.LightCyan.Color, 90f);
                Brush searchButtonArrowBrush = Configuration.searchButtonArrowBrush;
                Pen myArrowBorderPen = Configuration.myArrowBorderPen;
                Point point = new Point(this.x + (rect.Width / 3), this.y + (rect.Height / 4));
                Point point2 = new Point(this.x + (rect.Width / 3), (this.y + rect.Height) - (rect.Height / 4));
                Point point3 = new Point((this.x + rect.Width) - (rect.Width / 3), this.y + (rect.Height / 2));
                Point[] points = new Point[] { point, point2, point3 };
                using (GraphicsPath path = this.GetRoundedRectPath(rect, this.width / 4))
                {
                    g.FillPath(brush, path);
                    g.DrawPath(borderSearchBoxPen, path);
                    g.DrawPath(this.borderBoxPen, path);
                }
                g.FillPolygon(searchButtonArrowBrush, points, FillMode.Alternate);
                g.DrawPolygon(myArrowBorderPen, points);
            }
        }

        public void setSearchButton(int X, int Y, int dimension)
        {
            this.x = X;
            this.y = Y;
            this.width = dimension;
        }
    }
}

