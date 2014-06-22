namespace LostMinerLib
{
   
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;

    public class Result_XY_Select : Form
    {
        private string _begin_time;
        private string _end_time;
        private string _id;
        private string _line_no;
        private string _user_id;
        private string _warn_type;
        private DateTimePicker begin_time_t;
        private Button button1;
        private Button button2;
        private Chart chart1;
        private Chart chart2;
        private Chart chart3;
        private IContainer components = null;
        private DateTimePicker end_time_t;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private TextBox notes;
        private Label time_span;
        private OpenFileDialog upfileDoalog;
        private Label warn_user;

        public Result_XY_Select(string user_id, string line_no, string begin_time, string end_time, string id, string warn_type)
        {
            this.InitializeComponent();
            this._user_id = user_id;
            this._line_no = line_no;
            this._begin_time = begin_time;
            this._end_time = end_time;
            this._id = id;
            this.begin_time_t.Value = DateTime.Parse(this._begin_time);
            this.end_time_t.Value = DateTime.Parse(this._end_time);
            this.warn_user.Text = this.warn_user.Text + ";" + this._user_id;
            this.binddata();
        }

        private void binddata()
        {
            int j;
            DataTable war_dt = DBHelper.DBHelper.GetDataSet("select * from ts_warn_user where id='" + this._id + "'").Tables[0];
            this.notes.Text = war_dt.Rows[0]["notes"].ToString();
            DataTable dt_lost = DBHelper.DBHelper.GetDataSet("select * from ts_line where line_no='" + this._line_no + "' and date_time>='" + this._begin_time + "' and date_time <='" + this._end_time + "' ").Tables[0];
            DataTable dt_user = DBHelper.DBHelper.GetDataSet("select * from ts_user where user_id='" + this._user_id + "' and line_no='" + this._line_no + "' and date_time>='" + this._begin_time + "' and date_time <='" + this._end_time + "' ").Tables[0];
            DataView dv = dt_lost.DefaultView;
            this.chart1.DataBindCrossTable(dv, "line_no", "date_time", "lost", "");
            this.chart1.Legends[0].Title = "线损曲线";
            for (j = 0; j < this.chart1.Series.Count; j++)
            {
                this.chart1.Series[j].ChartType = SeriesChartType.Line;
                this.chart1.Series[j].BorderWidth = 3;
            }
            this.chart2.Legends[0].Title = "线损率曲线";
            this.chart2.DataBindCrossTable(dv, "line_no", "date_time", "rate", "");
            for (j = 0; j < this.chart2.Series.Count; j++)
            {
                this.chart2.Series[j].ChartType = SeriesChartType.Line;
                this.chart2.Series[j].BorderWidth = 3;
            }
            this.chart3.Legends[0].Title = "用户用电量曲线";
            DataView dv1 = dt_user.DefaultView;
            this.chart3.DataBindCrossTable(dv1, "user_id", "date_time", "used_q", "");
            for (j = 0; j < this.chart2.Series.Count; j++)
            {
                this.chart3.Series[j].ChartType = SeriesChartType.Line;
                this.chart3.Series[j].BorderWidth = 3;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
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
            ChartArea chartArea7 = new ChartArea();
            Legend legend7 = new Legend();
            ChartArea chartArea8 = new ChartArea();
            Legend legend8 = new Legend();
            ChartArea chartArea9 = new ChartArea();
            Legend legend9 = new Legend();
            this.button1 = new Button();
            this.warn_user = new Label();
            this.time_span = new Label();
            this.groupBox1 = new GroupBox();
            this.label2 = new Label();
            this.notes = new TextBox();
            this.end_time_t = new DateTimePicker();
            this.label1 = new Label();
            this.begin_time_t = new DateTimePicker();
            this.button2 = new Button();
            this.groupBox2 = new GroupBox();
            this.chart3 = new Chart();
            this.chart2 = new Chart();
            this.chart1 = new Chart();
            this.upfileDoalog = new OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.chart3.BeginInit();
            this.chart2.BeginInit();
            this.chart1.BeginInit();
            base.SuspendLayout();
            this.button1.Location = new Point(0x1e1, 0xb1);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.warn_user.AutoSize = true;
            this.warn_user.Location = new Point(0x1c, 0x33);
            this.warn_user.Name = "warn_user";
            this.warn_user.Size = new Size(0x35, 12);
            this.warn_user.TabIndex = 2;
            this.warn_user.Text = "用户编号";
            this.time_span.AutoSize = true;
            this.time_span.Location = new Point(0xa8, 0x33);
            this.time_span.Name = "time_span";
            this.time_span.Size = new Size(0x35, 12);
            this.time_span.TabIndex = 3;
            this.time_span.Text = "开始时间";
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.notes);
            this.groupBox1.Controls.Add(this.end_time_t);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.begin_time_t);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.warn_user);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.time_span);
            this.groupBox1.Dock = DockStyle.Fill;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x3a4, 0x245);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "窃电用户显示";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(30, 0x60);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "备注";
            this.notes.Location = new Point(0x56, 0x60);
            this.notes.Multiline = true;
            this.notes.Name = "notes";
            this.notes.Size = new Size(0x17b, 0x68);
            this.notes.TabIndex = 9;
            this.end_time_t.Location = new Point(0x1a8, 0x2c);
            this.end_time_t.Name = "end_time_t";
            this.end_time_t.Size = new Size(0x79, 0x15);
            this.end_time_t.TabIndex = 8;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x164, 0x31);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "结束时间";
            this.begin_time_t.Location = new Point(0xe4, 0x2d);
            this.begin_time_t.Name = "begin_time_t";
            this.begin_time_t.Size = new Size(0x79, 0x15);
            this.begin_time_t.TabIndex = 6;
            this.button2.Location = new Point(590, 0xb1);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 5;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.groupBox2.Controls.Add(this.chart3);
            this.groupBox2.Controls.Add(this.chart2);
            this.groupBox2.Controls.Add(this.chart1);
            this.groupBox2.Dock = DockStyle.Bottom;
            this.groupBox2.Location = new Point(0, 0xce);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x3a4, 0x177);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图形显示";
            chartArea7.Name = "ChartArea1";
            this.chart3.ChartAreas.Add(chartArea7);
            this.chart3.Dock = DockStyle.Bottom;
            legend7.Name = "Legend1";
            this.chart3.Legends.Add(legend7);
            this.chart3.Location = new Point(3, 0x10a);
            this.chart3.Name = "chart3";
            this.chart3.Size = new Size(0x39e, 0x6a);
            this.chart3.TabIndex = 2;
            this.chart3.Text = "chart3";
            chartArea8.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea8);
            legend8.Name = "Legend1";
            this.chart2.Legends.Add(legend8);
            this.chart2.Location = new Point(0, 0x8b);
            this.chart2.Name = "chart2";
            this.chart2.Size = new Size(0x389, 0x6a);
            this.chart2.TabIndex = 1;
            this.chart2.Text = "chart2";
            chartArea9.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea9);
            this.chart1.Dock = DockStyle.Top;
            legend9.Name = "Legend1";
            this.chart1.Legends.Add(legend9);
            this.chart1.Location = new Point(3, 0x11);
            this.chart1.Name = "chart1";
            this.chart1.Size = new Size(0x39e, 0x6a);
            this.chart1.TabIndex = 0;
            this.upfileDoalog.FileName = "openFileDialog1";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3a4, 0x245);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Result_XY_Select";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "窃电用户详细";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.chart3.EndInit();
            this.chart2.EndInit();
            this.chart1.EndInit();
            base.ResumeLayout(false);
        }
    }
}

