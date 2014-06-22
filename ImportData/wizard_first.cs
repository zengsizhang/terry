namespace ImportData
{

    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Linq;

    public class wizard_first : Form
    {
        private string _filename;
        private bool _is_mult;
        private Button button1;
        private Button button2;
        private Button button3;
        private IContainer components = null;
        private DataGridView dgv_line;
        private DataGridView dgv_mult;
        private DataGridView dgv_user;
        private GroupBox groupBox1;
        private GroupBox groupBox2;

        public wizard_first(bool is_mult)
        {
            this.InitializeComponent();
            this._is_mult = is_mult;
            this.binddata();
        }

        private void binddata()
        {
            
            string sql = "select a.line_no '线路', min(a.date_time)||'-'|| max(a.date_time) '时间',count(distinct b.user_id) '用户数' from ts_int_line as a,ts_int_user as b\r\nwhere a.line_no=b.line_no\r\ngroup by a.line_no";
            DataTable dt_mult = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
            this.dgv_mult.DataSource = dt_mult;
            DataTable dt_line = DBHelper.DBHelper.GetDataSet("select line_no '线路',date_time '日期',total_q '供电',user_q '售电', lost '线损',rate '线损率',ct from ts_int_line").Tables[0];
            DataTable dt_user = DBHelper.DBHelper.GetDataSet("select  user_id '用户',date_time '日期',used_q '售电',line_no '线路',ct from ts_int_user order by user_id,date_time").Tables[0];
            this.dgv_line.DataSource = dt_line;
            this.dgv_user.DataSource = dt_user;
            if (this._is_mult)
            {
                this.dgv_mult.Visible = true;
                this.dgv_user.Visible = false;
                this.dgv_line.Visible = false;
                this.groupBox1.Visible = false;
                this.groupBox2.Visible = false;
            }
            else
            {
                this.dgv_mult.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.Parse(DBHelper.DBHelper.GetScalar("select count(distinct line_no) from ts_int_line ").ToString()) > 1)
            {
                if (MessageBox.Show("由于批量导入数据 若导入数据与历史数据存在冲突，则覆盖，是否继续导入?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.mult_import();
                }
            }
            else
            {
                this.sing_import();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new record().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DBHelper.DBHelper.ExecuteCommand("delete from ts_int_line");
            DBHelper.DBHelper.ExecuteCommand("delete from ts_int_user");
            DBHelper.DBHelper.ExecuteCommand("delete from ts_import_record");
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
            this.groupBox1 = new GroupBox();
            this.dgv_line = new DataGridView();
            this.groupBox2 = new GroupBox();
            this.dgv_user = new DataGridView();
            this.button1 = new Button();
            this.button2 = new Button();
            this.button3 = new Button();
            this.dgv_mult = new DataGridView();
            this.groupBox1.SuspendLayout();
            ((ISupportInitialize) this.dgv_line).BeginInit();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.dgv_user).BeginInit();
            ((ISupportInitialize) this.dgv_mult).BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.dgv_line);
            this.groupBox1.Location = new Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x35f, 210);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "线路数据";
            this.dgv_line.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_line.Dock = DockStyle.Fill;
            this.dgv_line.Location = new Point(3, 0x11);
            this.dgv_line.Name = "dgv_line";
            this.dgv_line.RowTemplate.Height = 0x17;
            this.dgv_line.Size = new Size(0x359, 190);
            this.dgv_line.TabIndex = 0;
            this.groupBox2.Controls.Add(this.dgv_user);
            this.groupBox2.Location = new Point(10, 0xdb);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x359, 0xdf);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "用户数据";
            this.dgv_user.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_user.Dock = DockStyle.Fill;
            this.dgv_user.Location = new Point(3, 0x11);
            this.dgv_user.Name = "dgv_user";
            this.dgv_user.RowTemplate.Height = 0x17;
            this.dgv_user.Size = new Size(0x353, 0xcb);
            this.dgv_user.TabIndex = 0;
            this.button1.Location = new Point(0x28b, 0x1c0);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 2;
            this.button1.Text = "导入";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Location = new Point(0x209, 0x1c0);
            this.button2.Name = "button2";
            this.button2.Size = new Size(110, 0x17);
            this.button2.TabIndex = 3;
            this.button2.Text = "日志";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.button3.Location = new Point(0x30b, 0x1c0);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 4;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new EventHandler(this.button3_Click);
            this.dgv_mult.AllowUserToAddRows = false;
            this.dgv_mult.AllowUserToDeleteRows = false;
            this.dgv_mult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_mult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_mult.Location = new Point(0, 0);
            this.dgv_mult.Name = "dgv_mult";
            this.dgv_mult.RowTemplate.Height = 0x17;
            this.dgv_mult.Size = new Size(0x367, 0x1b4);
            this.dgv_mult.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x371, 0x1de);
            base.Controls.Add(this.dgv_mult);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "wizard_first";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "数据预览";
            this.groupBox1.ResumeLayout(false);
            ((ISupportInitialize) this.dgv_line).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((ISupportInitialize) this.dgv_user).EndInit();
            ((ISupportInitialize) this.dgv_mult).EndInit();
            base.ResumeLayout(false);
        }

        private void mult_import()
        {
            int i;
            string sql;
            DataTable dt_line = (DataTable) this.dgv_line.DataSource;
            DataTable dt_user = (DataTable) this.dgv_user.DataSource;
            ArrayList al = new ArrayList();
            var q = from line_q in dt_line.AsEnumerable()
                    group line_q by new
                    {
                        c1 = line_q.Field<string>("线路"),


                    } into g
                    select new
                    {
                        line_no = g.Key.c1,
                        max_time = g.Max(n => n.Field<string>("日期")),
                        min_time = g.Min(n => n.Field<string>("日期")),
                    };

            foreach (var qq in q)
            {

                al.Add("delete from ts_line where line_no='" + qq.line_no + "' and date_time>='" + qq.min_time + "' and date_time<='" + qq.max_time + "' ");
                al.Add("delete from ts_user where line_no='" + qq.line_no + "' and date_time>='" + qq.min_time + "' and date_time<='" + qq.max_time + "' ");

            }
            for (i = 0; i < dt_line.Rows.Count; i++)
            {
                sql = "insert into ts_line (LINE_NO,DATE_TIME ,TOTAL_Q,USER_Q ,lost ,rate,ct) values('" + dt_line.Rows[i][0].ToString() + "','" + dt_line.Rows[i][1].ToString() + "','" + dt_line.Rows[i][2].ToString() + "','" + dt_line.Rows[i][3].ToString() + "','" + dt_line.Rows[i][4].ToString() + "','" + dt_line.Rows[i][5].ToString() + "','" + dt_line.Rows[i][6].ToString() + "')";
                al.Add(sql);
            }
            for (i = 0; i < dt_user.Rows.Count; i++)
            {
                string USER_ID = dt_user.Rows[i][0].ToString();
                sql = "insert into ts_user (user_id,DATE_TIME ,USED_Q ,line_no,ct) values('" + USER_ID + "','" + dt_user.Rows[i][1].ToString() + "','" + dt_user.Rows[i][2].ToString() + "','" + dt_user.Rows[i][3].ToString() + "','" + dt_user.Rows[i][4].ToString() + "')";
                al.Add(sql);
            }
            al.Add("delete from ts_int_line");
            al.Add("delete from ts_int_user");
            al.Add("delete from ts_import_record");
            al.Add("insert into ts_import_file(file_name) values('" + this._filename + "')");
            string result_str = string.Empty;
            result_str = DBHelper.DBHelper.ExecuteCommand(al);
            if (result_str == "导入成功")
            {
                base.Close();
            }
            else if (result_str != string.Empty)
            {
                MessageBox.Show("导入出错,错误信息为:" + result_str + "请重新核实数据后并修改完后点击完成");
            }
        }

        private void sing_import()
        {
            int i;
            string sql;
            DataTable dt_line = (DataTable) this.dgv_line.DataSource;
            DataTable dt_user = (DataTable) this.dgv_user.DataSource;
            ArrayList al = new ArrayList();
            bool execute_b = false;
            string max_line_time = DBHelper.DBHelper.GetScalar("select max(date_time) from ts_line where line_no='" + dt_line.Rows[0][0].ToString() + "'").ToString();
            var q = from line_q in dt_line.AsEnumerable()
                    group line_q by new
                    {
                        c1 = line_q.Field<string>("线路"),
                     

                    } into g
                    select new
                    {
                        line_no = g.Key.c1,
                        max_time = g.Max(n=>n.Field<string>("日期")),
                        min_time = g.Min(n => n.Field<string>("日期")),
                    };

            foreach (var qq in q)
            {

                al.Add("delete from ts_line where line_no='" + qq.line_no + "' and date_time>='" + qq.min_time + "' and date_time<='" + qq.max_time + "' ");
                al.Add("delete from ts_user where line_no='" + qq.line_no + "' and date_time>='" + qq.min_time + "' and date_time<='" + qq.max_time + "' ");

            }
            for (i = 0; i < dt_line.Rows.Count; i++)
            {
                sql = "insert into ts_line (LINE_NO,DATE_TIME ,TOTAL_Q,USER_Q ,lost ,rate,ct) values('" + dt_line.Rows[i][0].ToString() + "','" + dt_line.Rows[i][1].ToString() + "','" + dt_line.Rows[i][2].ToString() + "','" + dt_line.Rows[i][3].ToString() + "','" + dt_line.Rows[i][4].ToString() + "','" + dt_line.Rows[i][5].ToString() + "','" + dt_line.Rows[i][6].ToString() + "')";
                if ((max_line_time.Trim().Length > 0) && (DateTime.Parse(max_line_time) >= DateTime.Parse(dt_line.Rows[i][1].ToString())))
                {
                    execute_b = true;
                }
                al.Add(sql);
            }
            for (i = 0; i < dt_user.Rows.Count; i++)
            {
                string USER_ID = dt_user.Rows[i][0].ToString();
                sql = "insert into ts_user (user_id,DATE_TIME ,USED_Q ,line_no,ct) values('" + USER_ID + "','" + dt_user.Rows[i][1].ToString() + "','" + dt_user.Rows[i][2].ToString() + "','" + dt_user.Rows[i][3].ToString() + "','" + dt_user.Rows[i][4].ToString() + "')";
                al.Add(sql);
            }
            al.Add("delete from ts_int_line");
            al.Add("delete from ts_int_user");
            al.Add("delete from ts_import_record");
            al.Add("insert into ts_import_file(file_name) values('" + this._filename + "')");
            string result_str = string.Empty;
            if (execute_b)
            {
                if (MessageBox.Show("线路" + dt_line.Rows[0][0].ToString() + "存在重复数据，继续导入则覆盖原先数据，是否继续导入?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    result_str = DBHelper.DBHelper.ExecuteCommand(al);
                }
            }
            else
            {
                result_str = DBHelper.DBHelper.ExecuteCommand(al);
            }
            if (result_str == "导入成功")
            {
                base.Close();
            }
            else if (result_str != string.Empty)
            {
                MessageBox.Show("导入出错,错误信息为:" + result_str + "请重新核实数据后并修改完后点击完成");
            }
        }
    }
}

