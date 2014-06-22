namespace TimeSearcher.Widgets
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Panels;

    public class SearchBox : TimeBox
    {
        private TimeSearcherForm _tsForm;
        private Graphics gs;
        public double[,] numArraySimilar;
        protected SearchButton searchButton;
        private Label show_lab;
        protected ToleranceHandle toleranceHandle;

        public SearchBox(Point pt, QueryPanel qp, Form tsForm) : base(pt, qp, tsForm)
        {
            this.searchButton = new SearchButton(0, 0, 0);
            this._tsForm = (TimeSearcherForm) tsForm;
        }

        protected override void afterCommitMove()
        {
           // this.showSearchButton();
        }

        protected override void afterMove()
        {
           // this.showToleranceHandle();
        }

        public override bool containsPoint(Point pt)
        {
            if (!(base.overTimebox(pt) || this.overSearchButton(pt)))
            {
                return this.overToleranceHandle(pt);
            }
            return true;
        }

        public override Color getBoxInteriorColor()
        {
            return Color.FromArgb(0xe8, Configuration.searchBoxPen.Color);
        }

        public override TimeBox.CURSOR_LOC getCursorLoc(Point mousePosition)
        {
            TimeBox.CURSOR_LOC cursor_loc = base.getCursorLoc(mousePosition);
            if (cursor_loc != TimeBox.CURSOR_LOC.NONE)
            {
                return cursor_loc;
            }
            if (this.overSearchButton(mousePosition) && this.searchButton.isVisible)
            {
                return TimeBox.CURSOR_LOC.SEARCHBUTTON;
            }
            if (this.overToleranceHandle(mousePosition))
            {
                return TimeBox.CURSOR_LOC.TOLERANCE;
            }
            return TimeBox.CURSOR_LOC.NONE;
        }

        public int[] GetItemIdxHavingResults()
        {
            return base._queryPanel.GetItemIdxHavingResults();
        }

        protected override void onCommit()
        {
            Point pt = new Point((this.rectBox.X + this.rectBox.Width) + 4, this.rectBox.Y);
            Size size = new Size(8, this.rectBox.Height);
            this.toleranceHandle = new ToleranceHandle(pt, size);
            this.showAllHandles();
          //  this.showSearchButton();
            this.show_lab = new Label();
        }

        public void onKeyDownToChangeTolerance(int percChange)
        {
            this.toleranceHandle.changeToleranceBy(percChange);
            base._queryPanel.Invalidate();
        }

        public void onKeyDownToChangeToleranceAndPerformSearch(int percChange)
        {
            this.onKeyDownToChangeTolerance(percChange);
            this.PerformSearch();
        }

        public override void onMouseDownAndMove(MouseEventArgs mea, TimeBox.CURSOR_LOC prevCursorLoc)
        {
            base.onMouseDownAndMove(mea, prevCursorLoc);
            if (prevCursorLoc == TimeBox.CURSOR_LOC.TOLERANCE)
            {
                this.toleranceHandle.onMouseDownAndMove(mea);
            }
        }

        public override void onMouseUp(MouseEventArgs mea)
        {
            this.searchButton.isVisible = true;
            this.toleranceHandle.isVisible = true;
            if (this.searchButton.containsPoint(new Point(mea.X, mea.Y)))
            {
                Console.WriteLine("starting search...");
                this.PerformSearch();
            }
        }

        private bool overSearchButton(Point pt)
        {
            return this.searchButton.containsPoint(pt);
        }

        private bool overToleranceHandle(Point pt)
        {
            return this.toleranceHandle.containsPoint(pt);
        }

        public override void Paint(Graphics g, QueryPanel qp)
        {
            if (base._queryPanel == qp)
            {
               
                base.PaintBoxAndHandles(g, qp);
                if (base.isSelected)
                {
                  //  this.toleranceHandle.Paint(g);
                   // this.showSearchButton();
                    //this.searchButton.Paint(g);
                }
                Font font1 = new Font("Arial", 10f, GraphicsUnit.Point);
                string x_start_value = qp.DataSet.getTimePointName(base.hStartIndex);
                string x_end_value = qp.DataSet.getTimePointName(base.hEndIndex);
                int b_bit = 0;
                int e_bit = 0;
                for (int i = 0; i < qp.DataSet.get_timePointNames().Length; i++)
                {
                    if (x_start_value == qp.DataSet.get_timePointNames()[i])
                    {
                        b_bit = i;
                    }
                    if (x_end_value == qp.DataSet.get_timePointNames()[i])
                    {
                        e_bit = i;
                    }
                }
                if ((e_bit - b_bit) > -2)
                {
                    double[] d_lost = new double[(e_bit - b_bit) + 1];
                    double[] d_rate = new double[(e_bit - b_bit) + 1];
                    int for_i = 0;
                    for (int i = b_bit; i <= e_bit; i++)
                    {
                        try
                        {
                            d_lost[for_i] = qp.DataSet.GetItem(0).GetVar(0).get_values()[i];
                            d_rate[for_i] = qp.DataSet.GetItem(1).GetVar(1).get_values()[i];
                            for_i++;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    this._tsForm._tbPanel.b_e_time.Text = "  选择时间范围为:" + x_start_value + "-" + x_end_value;
                    g.DrawString(x_start_value + "-" + x_end_value+"\r\n"+DBHelper.MathAdd.Ave(d_lost).ToString("F2") + "/" + Math.Pow(DBHelper.MathAdd.Var(d_lost), 0.5).ToString("F2") + "\r\n" + DBHelper.MathAdd.Ave(d_rate).ToString("F2") + "/" + Math.Pow(DBHelper.MathAdd.Var(d_rate), 0.5).ToString("F2"), font1, Brushes.Black, base.rectBox);
                }
            }
        }

        public void PerformSearch()
        {
            this.searchPattern();
        }

        public override void resizeUsingHandle(int handle, Point mouseDiff)
        {
            this.searchButton.isVisible = false;
            this.toleranceHandle.isVisible = false;
            base.resize(handle, mouseDiff.X, mouseDiff.Y);
        }
        public string searchTime()
        {
             TimeSearcher.DataSet dataSet = base._queryPanel.DataSet;
            string x_start_value = dataSet.getTimePointName(base.hStartIndex);
            string x_end_value = dataSet.getTimePointName(base.hEndIndex);
            return x_start_value + ";" + x_end_value;
        }
        private void searchPattern()
        {
            string min = DBHelper.DBHelper.GetScalar("select max(static_time) from ts_static").ToString();
            //if (DateTime.Now < DateTime.Parse(min))
            //{
            //    MessageBox.Show("系统故障，请与管理员联系");
            //}
            //else if (DateTime.Now > DateTime.Parse("2013-12-15"))
            //{
            //    MessageBox.Show("系统故障，请与管理员联系");
            //}
            //else
            //{
                DBHelper.DBHelper.ExecuteCommand("insert into ts_static(static_time) values('" + DateTime.Now.ToString("yyyy-MM-dd") + "')");
                int varIndex = base._queryPanel.getVarIndex();
                TimeSearcher.DataSet dataSet = base._queryPanel.DataSet;
                DataVariable variable = dataSet.getSelectedDataVar(varIndex);
                if (variable == null)
                {
                    variable = dataSet.getSelectedDataVar(0);
                }
                double[] patternC = variable.getValues(base.hStartIndex, base.hEndIndex);
                string x_start_value = dataSet.getTimePointName(base.hStartIndex);
                string x_end_value = dataSet.getTimePointName(base.hEndIndex);
                TimeSpan time_span = (TimeSpan) (DateTime.Parse(x_end_value) - DateTime.Parse(x_start_value));
                if (time_span.Days <= 2)
                {
                    MessageBox.Show("选取时间段不能少于三天");
                }
                else
                {
                    int j;
                    string datestr = "从" + x_start_value + "至" + x_end_value;
                    DataRowCollection obj = DBHelper.DBHelper.GetDataSet("select distinct t.user_id from static_lost t  where t.date_time>='" + x_start_value + "' and t.date_time<='" + x_end_value + "' and t.line_no='" + this._tsForm.get_line_lost() + "' order by user_id desc").Tables[0].Rows;
                    DataTable dt = DBHelper.DBHelper.GetDataSet("select * from static_lost t where t.date_time>='" + x_start_value + "' and t.date_time<='" + x_end_value + "' and t.line_no='" + this._tsForm.get_line_lost() + "'  order by  date_time asc ,user_id").Tables[0];
                    DataRowCollection timepoints = DBHelper.DBHelper.GetDataSet("select distinct t.date_time from static_lost t  where t.date_time>='" + x_start_value + "' and t.date_time<='" + x_end_value + "' and t.line_no='" + this._tsForm.get_line_lost() + "' order by user_id desc,date_time asc").Tables[0].Rows;
                    DataRow[] drtrue = this._tsForm.user_dt.Select("is_computer='True'");
                    this.numArraySimilar = new double[drtrue.Length + 1, timepoints.Count];
                    string[] user_c = new string[drtrue.Length + 1];
                    DataTable dt_warn = new DataTable();
                    DataColumn cl = new DataColumn("is_warn");
                    DataColumn cl1 = new DataColumn("user_id");
                    DataColumn cl2 = new DataColumn("warn_p");
                    DataColumn cl3 = new DataColumn("time_span");
                    dt_warn.Columns.Add(cl);
                    dt_warn.Columns.Add(cl1);
                    dt_warn.Columns.Add(cl2);
                    dt_warn.Columns.Add(cl3);
                    Dictionary<string, double> order_d = new Dictionary<string, double>();
                    int i = -1;
                    while (i < drtrue.Length)
                    {
                        DataRow dr = dt_warn.NewRow();
                        if (i > -1)
                        {
                            dr["user_id"] = drtrue[i]["user_id"].ToString();
                            dr["time_span"] = datestr;
                        }
                        int zero_count = 0;
                        j = 0;
                        while (j < timepoints.Count)
                        {
                            DataRow[] dr11;
                            if (i == -1)
                            {
                                dr11 = dt.Select("user_id='lost' and date_time='" + timepoints[j][0].ToString() + "' ");
                                if (dr11.Length > 0)
                                {
                                    this.numArraySimilar[i + 1, j] = double.Parse(dr11[0][2].ToString());
                                    order_d.Add(timepoints[j][0].ToString() + "_" + j, double.Parse(dr11[0][2].ToString()));
                                }
                                else
                                {
                                    this.numArraySimilar[i + 1, j] = 0.0;
                                    order_d.Add(timepoints[j][0].ToString() + "_" + j, 0.0);
                                }
                                user_c[i + 1] = "lost";
                            }
                            else
                            {
                                dr11 = dt.Select("user_id='" + drtrue[i]["user_id"].ToString() + "' and date_time='" + timepoints[j][0].ToString() + "' ");
                                if (dr11.Length > 0)
                                {
                                    this.numArraySimilar[i + 1, j] = double.Parse(dr11[0][2].ToString());
                                    if (double.Parse(dr11[0][2].ToString()) == 0.0)
                                    {
                                        zero_count++;
                                    }
                                }
                                else
                                {
                                    this.numArraySimilar[i + 1, j] = 0.0;
                                    zero_count++;
                                }
                                user_c[i + 1] = drtrue[i]["user_id"].ToString();
                            }
                            j++;
                        }
                        if (i > -1)
                        {
                            double p = double.Parse(zero_count.ToString()) / double.Parse(timepoints.Count.ToString());
                            dr["warn_p"] = p.ToString("F2");
                            if (p >= 0.8)
                            {
                                dr["is_warn"] = "TRUE";
                            }
                            else
                            {
                                dr["is_warn"] = "FALSE";
                            }
                            dt_warn.Rows.Add(dr);
                        }
                        i++;
                    }
                    order_d = order_d.OrderBy<KeyValuePair<string, double>, double>(delegate (KeyValuePair<string, double> entry) {
                        return entry.Value;
                    }).ToDictionary<KeyValuePair<string, double>, string, double>(delegate (KeyValuePair<string, double> pair) {
                        return pair.Key;
                    }, delegate (KeyValuePair<string, double> pair) {
                        return pair.Value;
                    });
                    double[,] yaozhi_rate = new double[this.numArraySimilar.GetLength(0), timepoints.Count];
                    double first = 0.0;
                    int index_p = 0;
                    int index_first = 0;
                    foreach (KeyValuePair<string, double> time_str in order_d)
                    {
                        int index_Num = int.Parse(time_str.Key.Split(new char[] { '_' })[1]);
                        if (index_p == 0)
                        {
                            first = time_str.Value;
                            index_first = index_Num;
                        }
                        i = 0;
                        while (i < this.numArraySimilar.GetLength(0))
                        {
                            if (i == 0)
                            {
                                double d_aa = time_str.Value / first;
                                yaozhi_rate[i, index_p] = double.Parse(d_aa.ToString("F2"));
                            }
                            else
                            {
                                yaozhi_rate[i, index_p] = double.Parse((this.numArraySimilar[i, index_Num] / this.numArraySimilar[i, index_first]).ToString("F2"));
                            }
                            i++;
                        }
                        index_p++;
                    }
                    double[] yaozhi = new double[yaozhi_rate.GetLength(0) - 1];
                    int i_d_index = 0;
                    for (i = 1; i < yaozhi_rate.GetLength(0); i++)
                    {
                        double d_s = 0.0;
                        j = 0;
                        while (j < yaozhi_rate.GetLength(1))
                        {
                            double dd = yaozhi_rate[0, j];
                            double dd1 = yaozhi_rate[i, j];
                            d_s += Math.Pow(yaozhi_rate[0, j] - yaozhi_rate[i, j], 2.0);
                            j++;
                        }
                        yaozhi[i_d_index] = double.Parse(Math.Pow(d_s, 0.5).ToString("F2"));
                        i_d_index++;
                    }
                    ArrayList al = new ArrayList();
                    ArrayList al3 = new ArrayList();
                    string al4 = "";
                    ArrayList al1 = new ArrayList();
                    ArrayList al2 = new ArrayList();
                    for (i = 0; i < this.numArraySimilar.GetLength(1); i++)
                    {
                        if (i > 0)
                        {
                            al4 = al4 + "," + (((this.numArraySimilar[0, i] - this.numArraySimilar[0, i - 1]) / this.numArraySimilar[0, i - 1])).ToString("F2");
                            for (j = 0; j < drtrue.Length; j++)
                            {
                                int b1 = 0;
                                int b2 = 0;
                                if (i == 1)
                                {
                                    al.Add(0);
                                    al1.Add("");
                                    al2.Add(0);
                                    al3.Add("");
                                }
                                if ((this.numArraySimilar[0, i] - this.numArraySimilar[0, i - 1]) > 0.0)
                                {
                                    b1 = 1;
                                }
                                else if ((this.numArraySimilar[0, i] - this.numArraySimilar[0, i - 1]) == 0.0)
                                {
                                    b1 = 0;
                                }
                                else
                                {
                                    b1 = -1;
                                }
                                if ((this.numArraySimilar[j + 1, i] - this.numArraySimilar[j + 1, i - 1]) > 0.0)
                                {
                                    b2 = 1;
                                }
                                else if ((this.numArraySimilar[j + 1, i] - this.numArraySimilar[j + 1, i - 1]) == 0.0)
                                {
                                    b2 = 0;
                                }
                                else
                                {
                                    b2 = -1;
                                }
                                double sx_d1 = (this.numArraySimilar[0, i] - this.numArraySimilar[0, i - 1]) / this.numArraySimilar[0, i - 1];
                                double sx_d2 = (this.numArraySimilar[j + 1, i] - this.numArraySimilar[j + 1, i - 1]) / this.numArraySimilar[j + 1, i - 1];
                                al3[j] = al3[j].ToString() + "," + sx_d2.ToString("F2");
                                if ((sx_d2 / sx_d1) > 0.8)
                                {
                                    al2[j] = int.Parse(al2[j].ToString()) + 1;
                                }
                                if (b1 == b2)
                                {
                                    al[j] = int.Parse(al[j].ToString()) + 1;
                                    al1[j] = al1[j].ToString() + ",1";
                                }
                                else
                                {
                                    al1[j] = al1[j].ToString() + ",0";
                                }
                            }
                        }
                    }
                    double[] bili = new double[drtrue.Length];
                    double[] sz_bili = new double[drtrue.Length];
                    j = 0;
                    while (j < drtrue.Length)
                    {
                        bili[j] = 1.0 - (double.Parse(al[j].ToString()) / ((double) (this.numArraySimilar.GetLength(1) - 1)));
                        sz_bili[j] = double.Parse(al2[j].ToString()) / ((double) (this.numArraySimilar.GetLength(1) - 1));
                        j++;
                    }
                    
                    double[,] a5 = new double[this.numArraySimilar.GetLength(0), timepoints.Count];
                    Dictionary<string, string> sd = new Dictionary<string, string>();
                    for (i = 0; i < drtrue.Length; i++)
                    {
                        string m1 = bili[i].ToString("F2");
                        string yaozhi_d = yaozhi[i].ToString();
                        string zero_date = dt_warn.Rows[i]["warn_p"].ToString();
                        sd.Add(drtrue[i]["user_id"].ToString(), m1 + ";" + yaozhi_d + ";" + zero_date);
                    }
                    DataView dv = dt_warn.DefaultView;
                    dv.Sort = "is_warn desc";
                    DataTable new_dt_warn = dv.ToTable();
                    this._tsForm.set_lstv_m1(sd, yaozhi_rate, timepoints, datestr, dt, this.numArraySimilar, user_c, new_dt_warn, order_d);
                //}
            }
        }

        public override void showAllHandles()
        {
            base.showAllHandles();
            this.showToleranceHandle();
        }

        protected void showSearchButton()
        {
            int dimension = Math.Min(this.rectBox.Width, this.rectBox.Height) / 2;
            if (dimension > Configuration.maxWidthSearchButton)
            {
                dimension = Configuration.maxWidthSearchButton;
            }
            this.searchButton.setSearchButton((this.rectBox.Right - dimension) - 4, this.rectBox.Bottom + 4, dimension);
        }

        protected void showToleranceHandle()
        {
            this.toleranceHandle.move(((this.rectBox.X + this.rectBox.Width) + 3) + 2, this.rectBox.Y, 8, this.rectBox.Height);
        }

        public Dictionary<string, double> Sort(Dictionary<string, double> dic)
        {
            List<string> keys = new List<string>();
            List<double> values = new List<double>();
            int i = 0;
            foreach (string item in dic.Keys)
            {
                keys.Add(item);
                if (!values.Contains(dic[item]))
                {
                    values.Add(dic[item]);
                }
                i++;
            }
            values.Sort();
            while (keys.Count != values.Count)
            {
                values.Add(values[values.Count - 1] + 1.0);
            }
            for (int j = 0; j < i; j++)
            {
                dic[keys[j]] = values[j];
            }
            return dic;
        }

        public void undoEffects()
        {
            base._queryPanel.clearResults();
        }
    }
}

