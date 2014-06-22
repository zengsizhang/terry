namespace TimeSearcher
{
    using System;
    using System.Drawing;

    public class GraphGap
    {
        private readonly Brush _gapBrush;
        private readonly Point[] _polygonPoints;

        public GraphGap(Graph lowGraph, Graph highGraph, Brush gapBrush)
        {
            this._gapBrush = gapBrush;
            this._polygonPoints = new Point[lowGraph.Points.Length + highGraph.Points.Length];
            Array.Copy(lowGraph.Points, 0, this._polygonPoints, 0, lowGraph.Points.Length);
            int length = lowGraph.Points.Length;
            for (int i = highGraph.Points.Length - 1; length < this._polygonPoints.Length; i--)
            {
                this._polygonPoints[length] = highGraph.Points[i];
                length++;
            }
        }

        public void FillGap(Graphics grfx)
        {
            grfx.FillPolygon(this._gapBrush, this._polygonPoints);
        }
    }
}

