namespace TimeSearcher.Widgets
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Filters;
    using TimeSearcher.Panels;

    public class FilterBox : TimeBox, DisablingEntity
    {
        public FilterBox(Point pt, QueryPanel qp, Form tsForm) : base(pt, qp, tsForm)
        {
        }

        protected override void afterCommitMove()
        {
        }

        protected override void afterMove()
        {
        }

        public void Dematerialize()
        {
            base._queryPanel.DataSet.TimeBoxManager.removeTimeBox(this);
        }

        public bool Filters(DataItem di)
        {
            double[] numArray = di.GetVar(base.getVarIndex()).getValues(base.hStartIndex, base.hEndIndex + 1);
            for (int i = 0; i < numArray.Length; i++)
            {
                if (!((numArray[i] == SharedData.missingValue) || base.coversValue(numArray[i])))
                {
                    return true;
                }
            }
            return false;
        }

        public override Color getBoxInteriorColor()
        {
            return Color.FromArgb(0xe8, Configuration.timeBoxPen.Color);
        }

        protected override void onCommit()
        {
            this.showAllHandles();
        }

        public override void Paint(Graphics g, QueryPanel qp)
        {
            if (base._queryPanel == qp)
            {
                base.PaintBoxAndHandles(g, qp);
            }
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
            Font font1 = new Font("Arial", 10f, GraphicsUnit.Point);
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
               // this._tsForm._tbPanel.b_e_time.Text = "  选择时间范围为:" + x_start_value + "-" + x_end_value;
                g.DrawString(x_start_value + "-" + x_end_value, font1, Brushes.Black, base.rectBox);
            }
        }
        public string searchTime()
        {
            TimeSearcher.DataSet dataSet = base._queryPanel.DataSet;
            string x_start_value = dataSet.getTimePointName(base.hStartIndex);
            string x_end_value = dataSet.getTimePointName(base.hEndIndex);
            return x_start_value + ";" + x_end_value;
        }
        public override void resizeUsingHandle(int handle, Point mouseDiff)
        {
            base.resize(handle, mouseDiff.X, mouseDiff.Y);
        }
    }
}

