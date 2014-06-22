namespace TimeSearcher.Wizard
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class RectangleWidget
    {
        private Rectangle area;
        private Pen borderPen;
        private bool dragLeftTriangleMode;
        private bool dragRightTriangleMode;
        private SolidBrush fillBrush;
        private SolidBrush innerBrush;
        private Point leftTriangleVertex;
        private int maxX;
        private float minPixels;
        private int minX;
        private CurrentRiverPanel owner;
        private Point rightTriangleVertex;
        private int triHeight = 10;

        public event EventHandler ValueChanged;

        public RectangleWidget(CurrentRiverPanel owner, Rectangle area, Color borderColor, Color innerColor)
        {
            this.area = area;
            this.borderPen = new Pen(borderColor, 2f);
            this.innerBrush = new SolidBrush(Color.FromArgb(200, innerColor));
            this.fillBrush = new SolidBrush(borderColor);
            this.leftTriangleVertex = new Point(area.X, area.Height + 5);
            this.rightTriangleVertex = new Point(area.X + area.Width, area.Height + 5);
            this.owner = owner;
        }

        public void Draw(Graphics grfx)
        {
            grfx.FillRectangle(this.innerBrush, this.area);
            grfx.DrawRectangle(this.borderPen, this.area);
            this.DrawMarkerTriangle(grfx, this.leftTriangleVertex);
            this.DrawMarkerTriangle(grfx, this.rightTriangleVertex);
        }

        private void DrawMarkerTriangle(Graphics grfx, Point vertex)
        {
            int x = vertex.X;
            int y = vertex.Y;
            Point[] points = new Point[] { new Point(x, y), new Point(x - (this.triHeight / 2), y + this.triHeight), new Point(x + (this.triHeight / 2), y + this.triHeight) };
            grfx.FillPolygon(this.fillBrush, points);
        }

        private bool IntersectTriangle(Point center, Point point)
        {
            int x = center.X;
            int y = center.Y;
            int num3 = point.X;
            int num4 = point.Y;
            return ((Math.Pow((double) (num3 - x), 2.0) + Math.Pow((double) (num4 - y), 2.0)) < Math.Pow((double) this.triHeight, 2.0));
        }

        public bool MouseDown(Point location)
        {
            if (this.IntersectTriangle(this.leftTriangleVertex, location))
            {
                this.dragLeftTriangleMode = true;
            }
            else if (this.IntersectTriangle(this.rightTriangleVertex, location))
            {
                this.dragRightTriangleMode = true;
            }
            if (!this.dragLeftTriangleMode)
            {
                return this.dragRightTriangleMode;
            }
            return true;
        }

        public void MouseMove(Point location)
        {
            if (this.dragLeftTriangleMode)
            {
                int width = (this.area.X + this.area.Width) - location.X;
                if (((width >= this.minPixels) && (location.X >= this.minX)) && (location.X <= this.maxX))
                {
                    this.leftTriangleVertex.X = location.X;
                    this.area = new Rectangle(location.X, 0, width, this.area.Height);
                    this.owner.Invalidate();
                    this.OnValueChanged(this, EventArgs.Empty);
                }
            }
            if (this.dragRightTriangleMode)
            {
                int num2 = location.X - this.area.X;
                if (((num2 >= this.minPixels) && (location.X >= this.minX)) && (location.X <= this.maxX))
                {
                    this.rightTriangleVertex.X = location.X;
                    this.area = new Rectangle(this.area.X, 0, num2, this.area.Height);
                    this.owner.Invalidate();
                    this.OnValueChanged(this, EventArgs.Empty);
                }
            }
        }

        public void MouseUp()
        {
            this.dragLeftTriangleMode = false;
            this.dragRightTriangleMode = false;
        }

        protected void OnValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(sender, e);
            }
        }

        public void SetArea(int min, int max)
        {
            int coordinateFromTimepoint = this.owner.GetCoordinateFromTimepoint(min);
            int num2 = this.owner.GetCoordinateFromTimepoint(max);
            Rectangle rectangle = new Rectangle(coordinateFromTimepoint, 0, num2 - coordinateFromTimepoint, this.area.Height);
            this.leftTriangleVertex.X = rectangle.X;
            this.rightTriangleVertex.X = rectangle.X + rectangle.Width;
            this.area = rectangle;
            this.ValueChanged(this, EventArgs.Empty);
        }

        public void SetExtents(int min, int max, float minPixels)
        {
            this.minX = min;
            this.maxX = max;
            this.minPixels = minPixels;
        }

        public Rectangle Area
        {
            get
            {
                return this.area;
            }
            set
            {
                this.area = value;
            }
        }

        public int EndPointIndex
        {
            get
            {
                int x = this.area.X + this.area.Width;
                return this.owner.GetTimepointFromCoordinate(x);
            }
        }

        public int StartPointIndex
        {
            get
            {
                return this.owner.GetTimepointFromCoordinate(this.area.X);
            }
        }
    }
}

