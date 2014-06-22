namespace TimeSearcher
{
    using System;
    using System.Data;
    using System.Drawing;
    using TimeSearcher.Domain;

    public class DataAxisV : DataAxis
    {
        private readonly TimeSearcher.DataSet _dataSet;
        private Point _end_ptEnd;
        private Point _end_ptStart;
        private int _endValue;
        private readonly int _hMargin;
        private const int _nMarkers = 5;
        private Point _ptEnd;
        private Point _ptStart;
        private int _startValue;
        private UniformDomain _staticVarDomain;
        private float _unitPerPixel;
        private readonly int _varIndex;
        private readonly int _vMargin;

        public DataAxisV(Rectangle clientRect, TimeSearcher.DataSet dataSet, int varIndex, int hMargin, int vMargin)
        {
            this._hMargin = hMargin;
            this._vMargin = vMargin;
            this._dataSet = dataSet;
            this._varIndex = varIndex;
            this._staticVarDomain = dataSet.StaticVar.Domain.GetIndependentCopy();
            this.setSize(clientRect);
            this._ptEnd = this._ptEnd;
            this._ptStart = this._ptStart;
            this._startValue = 0;
            this._endValue = 0;
        }

        public override bool contains(Point point)
        {
            return ((this._ptEnd.Y < point.Y) && (point.Y < this._ptStart.Y));
        }

        public override void drawGrid(Graphics g)
        {
            Point point = new Point(0, 0);
            Point point2 = new Point(0, 0);
            double num = (int)Math.Ceiling((double)(((double)(this._endValue - this._startValue)) / 5.0));
            for (double i = this._startValue; (i - this._endValue) <= 0.0001; i += num)
            {
                point.X = this._ptStart.X - 2;
                point.Y = this.getCoordinateFromValue(i);
                if (Configuration.isGridVisible)
                {
                    point2.X = (this._endValue - this._startValue) + this._ptStart.X;
                    point2.Y = this.getCoordinateFromValue(i);
                }
                else
                {
                    point2.X = this._ptStart.X + 2;
                    point2.Y = this.getCoordinateFromValue(i);
                }
                g.DrawLine(Configuration.gridPen, point, point2);
            }
        }

        public int getCoordinateFromValue(double yValue)
        {
            return (this._ptStart.Y - ((int)((yValue - this._startValue) / ((double)this._unitPerPixel))));
        }

        public override object getValueFromCoordinate(int coord)
        {
            return (double)(this._startValue + ((this._ptStart.Y - coord) * this._unitPerPixel));
        }

        public override void Paint(Graphics g)
        {
            Font font = new Font(FontFamily.GenericSansSerif, 8f);
            Font font2 = new Font(FontFamily.GenericSansSerif, 8f);
            Point point = new Point(0, 0);
            Point point2 = new Point(0, 0);
            Point point3 = new Point(0, 0);
            Point point4 = new Point(0, 0);
            if (Configuration.isGridVisible)
            {
                this.drawGrid(g);
            }
            g.DrawLine(Pens.Black, this._ptStart, this._ptEnd);
            double num = (int)Math.Ceiling((double)(((double)(this._endValue - this._startValue)) / 5.0));
            for (double i = this._startValue; i < this._endValue; i += num)
            {
                point.X = this._ptStart.X - 2;
                point.Y = this.getCoordinateFromValue(i);
                if (Configuration.isGridVisible)
                {
                    point2.X = (this._endValue - this._startValue) + this._ptStart.X;
                    point2.Y = this.getCoordinateFromValue(i);
                }
                else
                {
                    point2.X = this._ptStart.X + 2;
                    point2.Y = this.getCoordinateFromValue(i);
                }
                g.DrawLine(Configuration.yValueBarPen, point, point2);
                string text = Convert.ToString((int)Math.Round(i));
                SizeF ef = g.MeasureString(text, font);
                g.DrawString(text, font2, Brushes.Black, (float)((point.X - ef.Width) - 2f), (float)(point.Y - (ef.Height / 2f)));
            }
        }

        public void setScale()
        {
            if (this._dataSet._tsform != null)
            {
                double max;
                double min;
                int j;
                DataVariable v;
                double[] d;
                double tempmin;
                double tempmax;
                if (this._varIndex == 0)
                {
                    max = 0.0;
                    min = 0.0;
                    for (j = 0; j < this._dataSet.DataItems.Length; j++)
                    {
                        if ("lost" == this._dataSet.DataItems[j].Name)
                        {
                            v = this._dataSet.DataItems[j].getVariables()[0];
                            d = this._dataSet.DataItems[j].getVariables()[0].get_values();
                            tempmin = Utils.getMinExcept(d, SharedData.missingValue);
                            tempmax = Utils.getMaxExcept(d, SharedData.missingValue);
                            if (max <= tempmax)
                            {
                                max = tempmax;
                            }
                            if (min >= tempmin)
                            {
                                min = tempmin;
                            }
                        }
                    }
                    this._startValue = (int)Math.Floor(min);
                    this._endValue = (int)Math.Floor(max);
                    this._unitPerPixel = ((float)(this._endValue - this._startValue)) / ((float)(this._ptStart.Y - this._ptEnd.Y));
                }
                else if (this._varIndex == 1)
                {
                    max = 0.0;
                    min = 0.0;
                    j = 0;
                    while (j < this._dataSet.DataItems.Length)
                    {
                        if ("arate" == this._dataSet.DataItems[j].Name)
                        {
                            v = this._dataSet.DataItems[j].getVariables()[0];
                            d = this._dataSet.DataItems[j].getVariables()[0].get_values();
                            tempmin = Utils.getMinExcept(d, SharedData.missingValue);
                            tempmax = Utils.getMaxExcept(d, SharedData.missingValue);
                            if (max <= tempmax)
                            {
                                max = tempmax;
                            }
                            if (min >= tempmin)
                            {
                                min = tempmin;
                            }
                        }
                        j++;
                    }
                    if (max < 5.0)
                    {
                        max = 6.0;
                    }
                    this._startValue = (int)Math.Floor(min);
                    this._endValue = (int)Math.Floor(max);
                    this._unitPerPixel = ((float)(this._endValue - this._startValue)) / ((float)(this._ptStart.Y - this._ptEnd.Y));
                }
                else
                {
                    if (!this._dataSet._tsform.re_read)
                    {
                        max = 0.0;
                        min = 0.0;
                      //  DataTable dtg = this._dataSet._tsform.getdgvdt();
                       // DataRow[] drs1 = dtg.Select("is_check='True'");
                        DataRow[] drs = this._dataSet._tsform.user_dt.Select("is_check<>'False'");
                        for (int i = 0; i < this._dataSet._tsform.user_dt.Rows.Count; i++)
                        {
                            if (this._dataSet._tsform.user_dt.Rows[i]["is_check"].ToString() == "True")
                            {
                                j = 0;
                                while (j < this._dataSet.DataItems.Length)
                                {
                                    if (this._dataSet._tsform.user_dt.Rows[i]["user_id"].ToString() == this._dataSet.DataItems[j].Name)
                                    {
                                        v = this._dataSet.DataItems[j].getVariables()[2];
                                        d = this._dataSet.DataItems[j].getVariables()[2].get_values();
                                        tempmin = Utils.getMinExcept(d, SharedData.missingValue);
                                        tempmax = Utils.getMaxExcept(d, SharedData.missingValue);
                                        if (max <= tempmax)
                                        {
                                            max = tempmax;
                                        }
                                        if (min >= tempmin)
                                        {
                                            min = tempmin;
                                        }
                                    }
                                    j++;
                                }
                            }
                        }
                        if ((max == min) && (max == 0.0))
                        {
                            max = 100.0;
                            min = -100.0;
                        }
                        this._startValue = (int)Math.Floor(min);
                        this._endValue = (int)Math.Ceiling(max);
                        this._unitPerPixel = ((float)(this._endValue - this._startValue)) / ((float)(this._ptStart.Y - this._ptEnd.Y));
                    }
                    else
                    {
                        max = double.MinValue;
                        min = double.MaxValue;
                        int ic = 0;
                        for (j = 0; j < this._dataSet.DataItems.Length; j++)
                        {
                            if (!this._dataSet.DataItems[j].Name.Equals("arate") && !this._dataSet.DataItems[j].Name.Equals("lost"))
                            {
                                v = this._dataSet.DataItems[j].getVariables()[2];
                                d = this._dataSet.DataItems[j].getVariables()[2].get_values();
                                tempmin = Utils.getMinExcept(d, SharedData.missingValue);
                                tempmax = Utils.getMaxExcept(d, SharedData.missingValue);
                                if (max <= tempmax)
                                {
                                    max = tempmax;
                                }
                                if (min >= tempmin)
                                {
                                    min = tempmin;
                                }
                                ic++;
                            }
                        }
                        this._startValue = (int)Math.Floor(min);
                        this._endValue = (int)Math.Floor(max);
                        this._unitPerPixel = ((float)(this._endValue - this._startValue)) / ((float)(this._ptStart.Y - this._ptEnd.Y));
                    }
                }

            }
        }

        public override void setSize(Rectangle rect)
        {
            this._ptStart.X = rect.Left + this._hMargin;
            this._ptStart.Y = rect.Bottom - this._vMargin;
            this._ptStart = this._ptStart;
            this._ptEnd.X = rect.Left + this._hMargin;
            this._ptEnd.Y = rect.Top + this._vMargin;
            if (this._varIndex == 0)
            {
                this._end_ptStart.Y = rect.Bottom - this._vMargin;
                this._end_ptStart.X = rect.Right - (this._hMargin / 4);
                this._end_ptStart.X = rect.Right - (this._hMargin / 4);
                this._end_ptStart.Y = rect.Top + this._vMargin;
            }
        }
    }
}

