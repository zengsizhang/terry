namespace TimeSearcher
{
    using System;
    using System.Collections;
    using System.Drawing;
    using TimeSearcher.Domain;
    using TimeSearcher.Properties;
    using TimeSearcher.Search;

    public class Graph
    {
        private bool _endsWithMissing;
        private int _iclolr = -1;
        private readonly TimeSearcher.DataVariable _myDataVar;
        private Pen _myPen;
        private Point[][] _pointGroups;
        private Point[] _points;
        private TimeSearcher.Search.SearchResult _searchResult = TimeSearcher.Search.SearchResult.Empty;
        private UniformDomain _staticVarDomain;
        private DataAxisV _vAxis;
        private const int MISSING_POINT = -1;

        public Graph(TimeSearcher.DataVariable dataVar, UniformDomain staticVarDomain, DataAxisV vAxis)
        {
            this._vAxis = vAxis;
            this._myDataVar = dataVar;
            this._staticVarDomain = staticVarDomain;
            if (dataVar.get_strName == "arate_1")
            {
                this._myPen = Configuration.wran_pen;
            }
            else
            {
                this._myPen = Configuration.defaultDataVariablePen;
            }
        }

        public void BuildGraph(int s, int e)
        {
            double[] numArray = this._myDataVar.getValues();
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            this._points = new Point[(e - s) + 1];
            int num = this._staticVarDomain.getCoordinateFromIndex(s);
            int index = s;
            while ((index <= e) && (numArray[index] == SharedData.missingValue))
            {
                this._points[index - s].Y = -1;
                num = this._staticVarDomain.getCoordinateFromIndex(index);
                this._points[index - s].X = num;
                index++;
            }
            if (index <= e)
            {
                list.Add(index - s);
            }
            while (index <= e)
            {
                try
                {
                    num = this._staticVarDomain.getCoordinateFromIndex(index);
                    this._points[index - s].X = num;
                    if (numArray[index] == SharedData.missingValue)
                    {
                        list2.Add((index - s) - 1);
                        while ((index <= e) && (numArray[index] == SharedData.missingValue))
                        {
                            this._points[index - s].Y = -1;
                            index++;
                        }
                        if (index <= e)
                        {
                            list.Add(index - s);
                        }
                        index--;
                    }
                    else
                    {
                        this._points[index - s].Y = this._vAxis.getCoordinateFromValue(numArray[index]);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(string.Concat(new object[] { "DV.buildGraph(): problem displaying data: i=", index, " values[i]= ", numArray[index] }));
                    Console.WriteLine(exception.Message);
                }
                index++;
            }
            if (!(numArray[e] == SharedData.missingValue))
            {
                list2.Add(e - s);
            }
            int[] startIndices = (int[]) list.ToArray(typeof(int));
            int[] endIndices = (int[]) list2.ToArray(typeof(int));
            this.initPointGroups(startIndices, endIndices);
        }

        public void clearResults()
        {
            this._searchResult = TimeSearcher.Search.SearchResult.Empty;
        }

        public Point GetPoint(int index)
        {
            return this._points[index];
        }

        private void initPointGroups(int[] startIndices, int[] endIndices)
        {
            this._pointGroups = new Point[startIndices.Length][];
            for (int i = 0; i < startIndices.Length; i++)
            {
                int num = (endIndices[i] - startIndices[i]) + 1;
                if (num == 1)
                {
                    this._pointGroups[i] = new Point[2];
                    this._pointGroups[i][0] = this._pointGroups[i][1] = this._points[startIndices[i]];
                }
                else
                {
                    this._pointGroups[i] = new Point[num];
                    Array.Copy(this._points, startIndices[i], this._pointGroups[i], 0, this._pointGroups[i].Length);
                }
            }
            if (endIndices.Length < 1)
            {
                this._endsWithMissing = true;
            }
            else
            {
                this._endsWithMissing = endIndices[endIndices.Length - 1] < (this._points.Length - 1);
            }
        }

        public int moveLineWithinBounds(int localTpIdx)
        {
            if (localTpIdx >= (this.NumPoints - 1))
            {
                return (this.NumPoints - 2);
            }
            if (localTpIdx < 0)
            {
                return 0;
            }
            return localTpIdx;
        }

        public void renderGraph(Graphics grfx, Pen graphPen)
        {
            int index = 0;
            while (index < (this._pointGroups.Length - 1))
            {
                grfx.DrawLines(graphPen, this._pointGroups[index]);
                Point point = this._pointGroups[index][this._pointGroups[index].Length - 1];
                grfx.DrawEllipse(Configuration.missingValuePen, point.X - 1, point.Y - 1, 3, 3);
                index++;
            }
            if (index < this._pointGroups.Length)
            {
                grfx.DrawLines(graphPen, this._pointGroups[index]);
                if (this._endsWithMissing)
                {
                    Point point2 = this._pointGroups[index][this._pointGroups[index].Length - 1];
                    grfx.DrawEllipse(Configuration.missingValuePen, point2.X - 1, point2.Y - 1, 3, 3);
                }
            }
        }

        public void RenderGraphWithMatches(Graphics grfx, int global_s, bool smooth)
        {
            Pen graphPen = smooth ? this.selectGraphPen_Smooth() : this.selectGraphPen();
            this.renderGraph(grfx, graphPen);
            if (this.SearchResult.HasResults)
            {
                int minLocalIndex = this.MinLocalIndex;
                int maxLocalIndex = this.MaxLocalIndex;
                this.renderMatchedSegments(grfx, global_s, minLocalIndex, maxLocalIndex, smooth);
            }
        }

        public void RenderGraphWithMatches(Graphics grfx, int global_s, bool smooth, int linenum)
        {
            Pen graphPen = smooth ? this.selectGraphPen_Smooth(linenum) : this.selectGraphPen(linenum);
            if (linenum == -1)
            {
                graphPen = Configuration.wran_pen;
            }
            this.renderGraph(grfx, graphPen);
            if (this.SearchResult.HasResults)
            {
                int minLocalIndex = this.MinLocalIndex;
                int maxLocalIndex = this.MaxLocalIndex;
                this.renderMatchedSegments(grfx, global_s, minLocalIndex, maxLocalIndex, smooth);
            }
        }

        private void renderMatchedSegments(Graphics grfx, int global_s, int local_s, int local_e, bool smooth)
        {
            int[] timePtIndices = this._searchResult.TimePtIndices;
            Pen graphPen = smooth ? Configuration.graphMatchedPen_Smooth : Configuration.graphMatchedPen;
            int num3 = global_s + local_e;
            for (int i = 0; i < timePtIndices.Length; i++)
            {
                if (timePtIndices[i] >= (global_s - this._searchResult.PatLen))
                {
                    if (timePtIndices[i] > num3)
                    {
                        break;
                    }
                    int num = timePtIndices[i] - global_s;
                    int num2 = num + this._searchResult.PatLen;
                    if (num < local_s)
                    {
                        num = local_s;
                    }
                    if (num2 > local_e)
                    {
                        num2 = local_e;
                    }
                    this.renderSubGraph(grfx, num, num2, graphPen);
                }
            }
        }

        private void renderSubGraph(Graphics grfx, int local_s, int local_e, Pen graphPen)
        {
            Point[] destinationArray = new Point[(local_e - local_s) + 1];
            Array.Copy(this._points, local_s, destinationArray, 0, destinationArray.Length);
            foreach (Point point in destinationArray)
            {
                if (point.Y == -1)
                {
                    return;
                }
            }
            grfx.DrawLines(graphPen, destinationArray);
        }

        public void RenderSubGraph(Graphics grfx, int global_min, int global_s, int global_e, Pen graphPen)
        {
            int num = global_s - global_min;
            int num2 = global_e - global_min;
            if (num < 0)
            {
                num = 0;
            }
            if ((num < this.NumPoints) && (num2 >= 0))
            {
                if (num2 >= this.NumPoints)
                {
                    num2 = this.NumPoints - 1;
                }
                if (num < num2)
                {
                    this.renderSubGraph(grfx, num, num2, graphPen);
                }
            }
        }

        private Pen selectGraphPen()
        {
            DataItemStatus status = this._myDataVar.Status;
            if (status.Enabled)
            {
                if (status.Highlighted)
                {
                    return Configuration.graphHighlightedPen;
                }
                if (status.Selected)
                {
                    return Configuration.graphSelectedPen;
                }
                return Configuration.graphEnabledPen;
            }
            if (SharedData.isGraphDisabledHidden)
            {
                return Configuration.graphEraseDisabledPen;
            }
            return Configuration.graphDisabledPen;
        }

        private Pen selectGraphPen(int linenum)
        {
            DataItemStatus status = this._myDataVar.Status;
            if (status.Enabled)
            {
                if (status.Highlighted)
                {
                    return Configuration.graphHighlightedPen;
                }
                if (status.Selected)
                {
                    return Configuration.graphSelectedPen;
                }
                return Configuration.graphEnabledPen;
            }
            if (SharedData.isGraphDisabledHidden)
            {
                return Configuration.graphEraseDisabledPen;
            }
            return Configuration.graphDisabledPen;
        }

        private Pen selectGraphPen_Smooth()
        {
            DataItemStatus status = this._myDataVar.Status;
            if (status.Enabled)
            {
                if (status.Highlighted)
                {
                    return Configuration.graphHighlightedPen_Smooth;
                }
                if (status.Selected)
                {
                    return Configuration.graphSelectedPen_Smooth;
                }
                return Configuration.graphEnabledPen_Smooth;
            }
            if (SharedData.isGraphDisabledHidden)
            {
                return Configuration.graphEraseDisabledPen_Smooth;
            }
            return Configuration.graphDisabledPen_Smooth;
        }

        private Pen selectGraphPen_Smooth(int linenum)
        {
            DataItemStatus status = this._myDataVar.Status;
            if (status.Enabled)
            {
                if (status.Highlighted)
                {
                    return Configuration.graphHighlightedPen_Smooth;
                }
                if (status.Selected)
                {
                    return Configuration.graphSelectedPen_Smooth;
                }
                return LineColor.getlinecolor(linenum);
            }
            if (SharedData.isGraphDisabledHidden)
            {
                return Configuration.graphEraseDisabledPen_Smooth;
            }
            return LineColor.getlinecolor(linenum);
        }

        public TimeSearcher.DataVariable DataVariable
        {
            get
            {
                return this._myDataVar;
            }
        }

        private int MaxLocalIndex
        {
            get
            {
                return (this._points.Length - 1);
            }
        }

        private int MinLocalIndex
        {
            get
            {
                return 0;
            }
        }

        private int NumPoints
        {
            get
            {
                return this._points.Length;
            }
        }

        public Point[] Points
        {
            get
            {
                return this._points;
            }
        }

        public TimeSearcher.Search.SearchResult SearchResult
        {
            get
            {
                return this._searchResult;
            }
            set
            {
                this._searchResult = value;
            }
        }
    }
}

