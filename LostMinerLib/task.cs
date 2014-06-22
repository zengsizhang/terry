namespace LostMinerLib
{
   
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher;

    public class task : Form
    {
        private TimeSearcherForm _tsform;
        private DateTimePicker begin_time;
        private Button button1;
        private Button button2;
        private IContainer components = null;
        private DataGridView dgv_result;
        private DataGridView dgv_select;
        private DataRowCollection dr_result;
        private DataRowCollection dr_select;
        private DateTimePicker end_time;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private DataGridViewCheckBoxColumn is_select;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox line_no;
        private DataTable resurt_dt;
        private DataTable select_dt;
        private TextBox task_name;
        private ComboBox task_type;

        public task(TimeSearcherForm tsform)
        {
            this.InitializeComponent();
            this._tsform = tsform;
            this._tsform.load_task = false;
            this.int_value();
            this.select_data();
        }

        private void addrowtoresult(string line_no)
        {
            int i;
            this.select_dt = (DataTable) this.dgv_select.DataSource;
            DataRow[] dr = this.select_dt.Select("线路='" + line_no + "'");
            bool b = false;
            for (i = 0; i < this.resurt_dt.Rows.Count; i++)
            {
                if (this.resurt_dt.Rows[i]["线路"].ToString() == dr[0]["线路"].ToString())
                {
                    b = true;
                    break;
                }
            }
            DataRow dr_Result = this.resurt_dt.NewRow();
            for (i = 0; i < this.resurt_dt.Columns.Count; i++)
            {
                dr_Result[i] = dr[0][i];
            }
            this.select_dt.Rows.Remove(dr[0]);
            this.resurt_dt.Rows.Add(dr_Result);
            this.dgv_select.DataSource = this.select_dt;
            this.dgv_result.DataSource = this.resurt_dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.task_name.Text == "")
            {
                MessageBox.Show("任务名不能为空");
            }
            else if (this.resurt_dt.Rows.Count <= 0)
            {
                MessageBox.Show("请选择检查线路");
            }
            else
            {
                ArrayList al = new ArrayList();
                al.Add("insert into ts_task(task_name,task_type,create_time) values('" + this.task_name.Text + "','" + this.task_type.SelectedItem.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')");
                for (int i = 0; i < this.resurt_dt.Rows.Count; i++)
                {
                    al.Add("insert into ts_task_line(task_id,line_no,begin_time,end_time,var_value,avg_value,max_value,sd_value) values\r\n                ((select max(task_id) from ts_task),'" + this.resurt_dt.Rows[i]["线路"].ToString() + "','" + this.begin_time.Value.ToString("yyyy-MM-dd") + "','" + this.end_time.Value.ToString("yyyy-MM-dd") + "','" + this.resurt_dt.Rows[i]["方差"].ToString() + "','" + this.resurt_dt.Rows[i]["线损率均值"].ToString() + "','" + this.resurt_dt.Rows[i]["最大线损"].ToString() + "','" + this.resurt_dt.Rows[i]["标准差"].ToString() + "' )");
                }
                DBHelper.DBHelper.ExecuteCommand(al);
                this._tsform.load_task = true;
                base.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.select_data();
        }

        private void dgv_result_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string line_no = this.dgv_result.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                this.removerowfromresult(line_no);
            }
        }

        private void dgv_select_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string line_no = this.dgv_select.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                this.addrowtoresult(line_no);
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
            this.groupBox1 = new GroupBox();
            this.groupBox2 = new GroupBox();
            this.button1 = new Button();
            this.task_name = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.task_type = new ComboBox();
            this.dgv_result = new DataGridView();
            this.line_no = new TextBox();
            this.label3 = new Label();
            this.begin_time = new DateTimePicker();
            this.label4 = new Label();
            this.label5 = new Label();
            this.end_time = new DateTimePicker();
            this.button2 = new Button();
            this.dgv_select = new DataGridView();
            this.is_select = new DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.dgv_result).BeginInit();
            ((ISupportInitialize) this.dgv_select).BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.dgv_result);
            this.groupBox1.Controls.Add(this.task_type);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.task_name);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x2e1, 0x10b);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "任务";
            this.groupBox2.Controls.Add(this.dgv_select);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.end_time);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.begin_time);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.line_no);
            this.groupBox2.Dock = DockStyle.Bottom;
            this.groupBox2.Location = new Point(0, 0x111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x2e1, 0x11c);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "线路";
            this.button1.Location = new Point(0x1c3, 20);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.task_name.Location = new Point(60, 0x12);
            this.task_name.Name = "task_name";
            this.task_name.Size = new Size(0xa2, 0x15);
            this.task_name.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 0x15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "任务名";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xf2, 0x19);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "任务类型";
            this.task_type.FormattingEnabled = true;
            this.task_type.Items.AddRange(new object[] { "常规检查", "专项检查" });
            this.task_type.Location = new Point(0x131, 0x16);
            this.task_type.Name = "task_type";
            this.task_type.Size = new Size(0x79, 20);
            this.task_type.TabIndex = 4;
            this.task_type.Text = "常规检查";
            this.dgv_result.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_result.Location = new Point(7, 50);
            this.dgv_result.Name = "dgv_result";
            this.dgv_result.RowTemplate.Height = 0x17;
            this.dgv_result.Size = new Size(730, 0xd3);
            this.dgv_result.TabIndex = 5;
            this.dgv_result.CellContentDoubleClick += new DataGridViewCellEventHandler(this.dgv_result_CellContentDoubleClick);
            this.line_no.Location = new Point(0x36, 0x15);
            this.line_no.Name = "line_no";
            this.line_no.Size = new Size(100, 0x15);
            this.line_no.TabIndex = 0;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(13, 0x18);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "线路";
            this.begin_time.Location = new Point(0xda, 0x15);
            this.begin_time.Name = "begin_time";
            this.begin_time.Size = new Size(0x75, 0x15);
            this.begin_time.TabIndex = 2;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xa1, 0x1a);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "开始时间";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x156, 0x19);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "结束时间";
            this.end_time.Location = new Point(0x199, 20);
            this.end_time.Name = "end_time";
            this.end_time.Size = new Size(0x75, 0x15);
            this.end_time.TabIndex = 5;
            this.button2.Location = new Point(0x22a, 0x12);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 6;
            this.button2.Text = "查询";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.dgv_select.AllowUserToAddRows = false;
            this.dgv_select.AllowUserToDeleteRows = false;
            this.dgv_select.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_select.Columns.AddRange(new DataGridViewColumn[] { this.is_select });
            this.dgv_select.Location = new Point(3, 0x35);
            this.dgv_select.Name = "dgv_select";
            this.dgv_select.RowTemplate.Height = 0x17;
            this.dgv_select.Size = new Size(0x2de, 0xe7);
            this.dgv_select.TabIndex = 7;
            this.dgv_select.CellContentDoubleClick += new DataGridViewCellEventHandler(this.dgv_select_CellContentDoubleClick);
            this.is_select.DataPropertyName = "is_select";
            this.is_select.HeaderText = "选中";
            this.is_select.Name = "is_select";
            this.is_select.Resizable = DataGridViewTriState.True;
            this.is_select.SortMode = DataGridViewColumnSortMode.Automatic;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2e1, 0x22d);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "task";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "任务";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((ISupportInitialize) this.dgv_result).EndInit();
            ((ISupportInitialize) this.dgv_select).EndInit();
            base.ResumeLayout(false);
        }

        private void int_value()
        {
            DateTime now = DateTime.Now;
            DateTime last_d = new DateTime(now.Year, now.Month, 1).AddDays(-1.0);
            DateTime first_d = new DateTime(now.Year, now.AddMonths(-1).Month, 1);
            this.begin_time.Value = first_d;
            this.end_time.Value = last_d;
        }

        private void removerowfromresult(string line_no)
        {
            this.resurt_dt = (DataTable) this.dgv_result.DataSource;
            DataRow[] dr = this.resurt_dt.Select("线路='" + line_no + "'");
            DataRow dr_Result = this.select_dt.NewRow();
            for (int i = 0; i < this.select_dt.Columns.Count; i++)
            {
                dr_Result[i] = dr[0][i];
            }
            this.select_dt.Rows.Add(dr_Result);
            this.resurt_dt.Rows.Remove(dr[0]);
            this.dgv_select.DataSource = this.select_dt;
            this.dgv_result.DataSource = this.resurt_dt;
        }

        private void select_data()
        {
            string sql = "select case  when round(avg(rate),2)>5 then 'TRUE' ELSE 'FALSE' END 'is_select', line_no 线路,round(avg(lost),2) 线损均值,round(avg(rate),2) 线损率均值,max(lost) 最大线损,\r\n                    ''  '方差' ,'' '标准差'  from ts_line\r\n                   where  date_time>='" + this.begin_time.Value.ToString("yyyy-MM-dd") + "' and date_time <='" + this.end_time.Value.ToString("yyyy-MM-dd") + "'";
            if (this.line_no.Text != "")
            {
                sql = string.Concat(new object[] { sql, " and line_no like '%", this.line_no, "%' " });
            }
            sql = sql + " group by line_no order by avg(rate) desc";
            this.select_dt = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
            this.dgv_select.DataSource = this.select_dt;
            this.resurt_dt = this.select_dt.Copy();
            this.resurt_dt.Clear();
        }
    }
}

