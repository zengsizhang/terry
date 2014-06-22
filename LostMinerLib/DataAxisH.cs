namespace TimeSearcher
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using TimeSearcher.Domain;
    using TimeSearcher.Search;

    public class DataAxisH : DataAxis
    {
        private int _endIndex;
        private int _hMargin;
        private const int _nMarkers = 5;
        private Point _ptEnd;
        private Point _ptStart;
        private int _startIndex;
        private StaticVariable _staticVar;
        private UniformDomain _staticVarDomain;
        private string[] _timePointNames;
        private int _vMargin;
        private ArrayList results = new ArrayList();
        private int searchPatternStartIndex;

        public DataAxisH(Rectangle clientRect, DataSet dataSet, int hMargin, int vMargin)
        {
            this._timePointNames = dataSet.TimePointNames;
            this._staticVarDomain = dataSet.StaticVar.Domain.GetIndependentCopy();
            this._staticVar = dataSet.StaticVar;
            this._hMargin = hMargin;
            this._vMargin = vMargin;
            this.setSize(clientRect);
            this._startIndex = 0;
            this._endIndex = 0;
        }

        public void clearResults()
        {
            this.results.Clear();
        }

        public override bool contains(Point point)
        {
            return this._staticVarDomain.containsPixel(point.X);
        }

        public bool coversValidRect(Rectangle rect)
        {
            return this._staticVarDomain.coversValidRect(rect);
        }

        public override void drawGrid(Graphics g)
        {
            if (Configuration.isGridVisible)
            {
                for (int i = this._startIndex; i < this._endIndex; i++)
                {
                    int num2 = this.getCoordinateFromIndex(i);
                    g.DrawLine(Configuration.gridPen, num2, this._ptStart.Y - this._ptEnd.Y, num2, this._ptStart.Y);
                }
            }
        }

        public int getCoordinateFromIndex(int timePtIndex)
        {
            return this._staticVarDomain.getCoordinateFromIndex(timePtIndex);
        }

        private int getCorrectedIndex(int index)
        {
            if (index >= this._endIndex)
            {
                index = this._endIndex;
            }
            if (index < this._startIndex)
            {
                index = this._startIndex;
            }
            return index;
        }

        public int getFloorIndexFromCoordinate(int coord)
        {
            int index = this._staticVarDomain.getFloorIndexFromCoordinate(coord);
            return this.getCorrectedIndex(index);
        }

        public int getIndexFromCoordinate(int coord)
        {
            int index = this._staticVarDomain.getIndexFromCoordinate(coord);
            return this.getCorrectedIndex(index);
        }

        private Pen getTriangleBorderPen(int resultIndex)
        {
            if (resultIndex == this.searchPatternStartIndex)
            {
                return Configuration.searchPatternTriangleBorderPen;
            }
            return Configuration.searchResultTriangleBorderPen;
        }

        private Brush getTriangleBrush(int resultIndex, Rectangle rectOfTriangle)
        {
            Color searchPatternTriangleDarkColor;
            Color searchPatternTriangleLiteColor;
            if (resultIndex == this.searchPatternStartIndex)
            {
                searchPatternTriangleDarkColor = Configuration.searchPatternTriangleDarkColor;
                searchPatternTriangleLiteColor = Configuration.searchPatternTriangleLiteColor;
            }
            else
            {
                searchPatternTriangleDarkColor = Configuration.searchResultTriangleDarkColor;
                searchPatternTriangleLiteColor = Configuration.searchResultTriangleLiteColor;
            }
            return new LinearGradientBrush(rectOfTriangle, searchPatternTriangleDarkColor, searchPatternTriangleLiteColor, 30f);
        }

        public override object getValueFromCoordinate(int coord)
        {
            int index = this._staticVarDomain.getIndexFromCoordinate(coord);
            if ((index > -1) && (index < this._timePointNames.Length))
            {
                return this._timePointNames[index];
            }
            return "";
        }

        public override void Paint(Graphics g)
        {
            new Font(FontFamily.GenericSansSerif, 8f);
            Font font = new Font(FontFamily.GenericSansSerif, 8f);
            new Point(0, 0);
            new Point(0, 0);
            if (Configuration.isGridVisible)
            {
                this.drawGrid(g);
            }
            g.DrawLine(Pens.Black, this._ptStart, this._ptEnd);
            string[] strArray = this._timePointNames;
            int num = (int) Math.Ceiling((double) (((double) (this._endIndex - this._startIndex)) / 5.0));
            for (int i = this._startIndex; i < this._endIndex; i++)
            {
                string s = strArray[i];
                g.DrawLine(Pens.Black, this.getCoordinateFromIndex(i), this._ptStart.Y - 4, this.getCoordinateFromIndex(i), this._ptStart.Y);
                if (((i - this._startIndex) % num) == 0)
                {
                    g.DrawString(s, font, Configuration.timePointBrush, (float) this.getCoordinateFromIndex(i), (float) (this._ptStart.Y + 10));
                    g.DrawLine(Pens.Black, this.getCoordinateFromIndex(i), this._ptStart.Y - 6, this.getCoordinateFromIndex(i), this._ptStart.Y);
                }
                else
                {
                    g.DrawLine(Configuration.timePointBarPen, this.getCoordinateFromIndex(i), this._ptStart.Y - 4, this.getCoordinateFromIndex(i), this._ptStart.Y);
                }
            }
        }

        private void paintDensity(Graphics grfx)
        {
            Point point = this._ptStart;
            point.Offset(0, 6);
            Color[] tempDensity = this._staticVar.TempDensity;
            for (int i = this._startIndex; i < this._endIndex; i++)
            {
                Pen pen = new Pen(tempDensity[i], 8f);
                grfx.DrawLine(pen, this.getCoordinateFromIndex(i), point.Y, this.getCoordinateFromIndex(i + 1), point.Y);
            }
        }

        public void PaintSearchTriangles(Graphics g)
        {
            int triangleDim = Configuration.triangleDim;
            foreach (int num2 in this.results)
            {
                int x = this.getCoordinateFromIndex(num2);
                int y = this._ptStart.Y;
                Point point = new Point(x, y);
                Point point2 = new Point(x - (triangleDim / 2), y + triangleDim);
                Point point3 = new Point(x + (triangleDim / 2), y + triangleDim);
                Point[] points = new Point[] { point, point2, point3 };
                Brush brush = this.getTriangleBrush(num2, new Rectangle(x - 3, y - 6, 6, 6));
                g.FillPolygon(brush, points, FillMode.Alternate);
                g.DrawPolygon(this.getTriangleBorderPen(num2), points);
            }
        }

        public void setResults(SearchResults searchResults)
        {
            this.results.AddRange(searchResults.getAllResults());
            this.results.Sort();
            Utils.removeDuplicatesInSorted(this.results);
            this.searchPatternStartIndex = searchResults.getPatStartIndex();
        }

        public void setScale(int s, int e)
        {
            this._startIndex = s;
            this._endIndex = e;
            this._staticVarDomain.SetStartEndIndex(this._startIndex, this._endIndex);
        }

        public override void setSize(Rectangle rect)
        {
            this._ptStart.X = rect.Left + this._hMargin;
            this._ptStart.Y = rect.Bottom - this._vMargin;
            this._ptEnd.X = rect.Right - (this._hMargin / 4);
            this._ptEnd.Y = rect.Bottom - this._vMargin;
            this._staticVarDomain.SetStartEndPixel(this._ptStart.X, this._ptEnd.X);
        }

        public UniformDomain Domain
        {
            get
            {
                return this._staticVarDomain;
            }
        }
    }
}

