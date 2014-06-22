namespace TimeSearcher
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;

    public class RateChart : Form
    {
        private Chart chart1;
        private Chart chart2;
        private IContainer components = null;

        public RateChart(DataTable dtrate, DataTable userrate)
        {
            this.InitializeComponent();
            DataView dv = dtrate.DefaultView;
            this.chart2.DataBindCrossTable(dtrate.DefaultView, "user_id", "date_time", "yaozhi_rate", "");
            for (int j = 0; j < this.chart2.Series.Count; j++)
            {
                this.chart2.Series[j].ChartType = SeriesChartType.Line;
                this.chart2.Series[j].BorderWidth = 3;
                string dd = dtrate.Rows[j]["user_id"].ToString();
                this.chart2.Series[j].ToolTip = this.chart2.Series[j].Name;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();
            this.chart2 = new Chart();
            this.chart2.BeginInit();
            base.SuspendLayout();
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            this.chart2.Dock = DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new Point(0, 0);
            this.chart2.Name = "chart2";
            this.chart2.Size = new Size(0x1e4, 0x1ce);
            this.chart2.TabIndex = 0;
            this.chart2.Text = "chart2";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScroll = true;
            base.AutoScrollMargin = new Size(10, 10);
            base.ClientSize = new Size(0x1e4, 0x1ce);
            base.Controls.Add(this.chart2);
            base.Name = "RateChart";
            this.Text = "趋势分析";
            this.chart2.EndInit();
            base.ResumeLayout(false);
        }
    }
}

