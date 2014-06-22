namespace LostMinerLib
{

    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Collections;

    public class WarnUser : Form
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private ComboBox comboBox1;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private ComboBox line_no;
        private CheckBox checkBox1;
        private Button button4;
        private DataGridViewCheckBoxColumn 全选;
        private DataGridViewButtonColumn 删除;
        private DataGridViewButtonColumn 编辑;
        private DataGridViewTextBoxColumn 线路;
        private DataGridViewTextBoxColumn 用户;
        private DataGridViewTextBoxColumn 类型;
        private DataGridViewTextBoxColumn 检查开始时间;
        private DataGridViewTextBoxColumn 检查结束时间;
        private DataGridViewTextBoxColumn 发现时间;
        private DataGridViewTextBoxColumn 检查人;
        private TextBox textBox1;

        public WarnUser()
        {
            this.InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            this.bingdata();

        }

        public void add_item(DataTable dt)
        {
            this.line_no.Items.Clear();
            this.line_no.Items.Add("请选择线路");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.line_no.Items.Add(dt.Rows[i][0].ToString());
            }
            if (this.line_no.Items.Count > 0)
            {
                this.line_no.SelectedIndex = 0;
            }
        }

        private void bingdata()
        {

            string sql = "select id , user_id,begin_time ,end_time , warn_type ,line_no ,update_time,username  from ts_warn_user  order by id desc ";
            DataTable dt = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
            this.dataGridView1.DataSource = dt;
            DataTable dt_line = DBHelper.DBHelper.GetDataSet("select distinct line_no from ts_line").Tables[0];
            this.add_item(dt_line);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select id , user_id,begin_time ,end_time , warn_type ,line_no ,update_time,username  from ts_warn_user WHERE  1=1 ";
            if (this.textBox1.Text != "")
            {
                sql = sql + " and user_id like '%" + this.textBox1.Text + "%' ";
            }
            if (this.line_no.SelectedItem.ToString() != "请选择线路")
            {
                sql = sql + " and line_no='" + this.line_no.SelectedItem.ToString() + "' ";
            }
            DataTable dt = DBHelper.DBHelper.GetDataSet((sql + " and is_check='" + this.comboBox1.SelectedItem.ToString() + "'") + " order by id desc").Tables[0];
            this.dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XY_Select xy = new XY_Select();
            xy.Show();
            xy.FormClosed += new FormClosedEventHandler(this.xy_FormClosed);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            DBHelper.DBiniClass iniclass = new DBHelper.DBiniClass(".\\system.ini");
            string username = iniclass.IniReadValue("username", "username");
            string linkmodel = iniclass.IniReadValue("netlink", "linkmodel");
            if (linkmodel == "oracle")
            {
                if ((e.ColumnIndex == 1) && (e.RowIndex != -1))
                {
                    DataTable dt = (DataTable)this.dataGridView1.DataSource;
                    DBHelper.DBHelper.ExecuteCommand("delete from ts_warn_user where id='" + dt.Rows[e.RowIndex][0].ToString() + "'");
                    this.bingdata();

                }
                if ((e.ColumnIndex == 2) && (e.RowIndex != -1))
                {

                    //if (username == "admin")
                    //{
                    DataTable dt = (DataTable)this.dataGridView1.DataSource;
                    XY_Select xy = new XY_Select(dt.Rows[e.RowIndex][0].ToString(), dt.Rows[e.RowIndex][4].ToString());
                    xy.Show();
                    xy.FormClosed += new FormClosedEventHandler(this.xy_FormClosed);
                    //}
                    //else
                    //{
                    //    MessageBox.Show(username + "无权限进行编辑");
                    //}
                }
            }
            else
            {
                MessageBox.Show("单机模式无法进行操作");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (e.RowIndex != -1))
            {
                //DataTable dt = (DataTable) this.dataGridView1.DataSource;
                //XY_Select xy = new XY_Select(dt.Rows[e.RowIndex][1].ToString(), dt.Rows[e.RowIndex][5].ToString(), dt.Rows[e.RowIndex][2].ToString(), dt.Rows[e.RowIndex][3].ToString(), dt.Rows[e.RowIndex][0].ToString(), dt.Rows[e.RowIndex][4].ToString());
                //xy.Show();
                //xy.FormClosed += new FormClosedEventHandler(this.xy_FormClosed);
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

        private void i_FormClosed(object sender, EventArgs e)
        {
            this.bingdata();
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.line_no = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.全选 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.删除 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.编辑 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.线路 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.用户 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.检查开始时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.检查结束时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.发现时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.检查人 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.line_no);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1066, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(714, 41);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(103, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "导出数据";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // line_no
            // 
            this.line_no.FormattingEnabled = true;
            this.line_no.Items.AddRange(new object[] {
            "未检查",
            "已检查"});
            this.line_no.Location = new System.Drawing.Point(266, 42);
            this.line_no.Name = "line_no";
            this.line_no.Size = new System.Drawing.Size(121, 20);
            this.line_no.TabIndex = 8;
            this.line_no.Text = "请选择线路";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "线路";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "未检查",
            "已检查"});
            this.comboBox1.Location = new System.Drawing.Point(487, 68);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.Text = "未检查";
            this.comboBox1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(402, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "检查状态";
            this.label2.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(503, 40);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "关闭";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(595, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "添加窃电用户";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(84, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户编号:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(404, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1066, 357);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(84, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.全选,
            this.删除,
            this.编辑,
            this.线路,
            this.用户,
            this.类型,
            this.检查开始时间,
            this.检查结束时间,
            this.发现时间,
            this.检查人});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1060, 337);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // 全选
            // 
            this.全选.HeaderText = "选择";
            this.全选.Name = "全选";
            // 
            // 删除
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.NullValue = "删除";
            this.删除.DefaultCellStyle = dataGridViewCellStyle5;
            this.删除.HeaderText = "删除";
            this.删除.Name = "删除";
            this.删除.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.删除.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // 编辑
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.NullValue = "编辑";
            this.编辑.DefaultCellStyle = dataGridViewCellStyle6;
            this.编辑.HeaderText = "编辑";
            this.编辑.Name = "编辑";
            this.编辑.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.编辑.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.编辑.Text = "编辑";
            // 
            // 线路
            // 
            this.线路.DataPropertyName = "line_no";
            this.线路.HeaderText = "线路";
            this.线路.Name = "线路";
            // 
            // 用户
            // 
            this.用户.DataPropertyName = "user_id";
            this.用户.HeaderText = "用户";
            this.用户.Name = "用户";
            // 
            // 类型
            // 
            this.类型.DataPropertyName = "warn_type";
            dataGridViewCellStyle7.NullValue = "类型";
            this.类型.DefaultCellStyle = dataGridViewCellStyle7;
            this.类型.HeaderText = "类型";
            this.类型.Name = "类型";
            // 
            // 检查开始时间
            // 
            this.检查开始时间.DataPropertyName = "begin_time";
            this.检查开始时间.HeaderText = "检查开始时间";
            this.检查开始时间.Name = "检查开始时间";
            // 
            // 检查结束时间
            // 
            this.检查结束时间.DataPropertyName = "end_time";
            this.检查结束时间.HeaderText = "检查结束时间";
            this.检查结束时间.Name = "检查结束时间";
            // 
            // 发现时间
            // 
            this.发现时间.DataPropertyName = "update_time";
            this.发现时间.HeaderText = "发现时间";
            this.发现时间.Name = "发现时间";
            // 
            // 检查人
            // 
            this.检查人.DataPropertyName = "username";
            dataGridViewCellStyle8.NullValue = "检查人";
            this.检查人.DefaultCellStyle = dataGridViewCellStyle8;
            this.检查人.HeaderText = "检查人";
            this.检查人.Name = "检查人";
            // 
            // WarnUser
            // 
            this.ClientSize = new System.Drawing.Size(1066, 457);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WarnUser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "嫌疑用户";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        private void xy_FormClosed(object sender, EventArgs e)
        {
            this.bingdata();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in this.dataGridView1.Rows)
            {
                ((DataGridViewCheckBoxCell)Row.Cells[0]).Value = checkBox1.Checked;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ArrayList al = new ArrayList();
            DataTable dt = new DataTable();
            dt.Columns.Add("用户");
            dt.Columns.Add("线路");
            dt.Columns.Add("检查开始时间");
            dt.Columns.Add("检查结束时间");
            dt.Columns.Add("发现时间");
            dt.Columns.Add("类型");
            dt.Columns.Add("录入人");
            int i = 0;
            DataTable dts = (DataTable)dataGridView1.DataSource;
            foreach (DataGridViewRow Row in this.dataGridView1.Rows)
            {
                //dts.Rows[i][""].ToString()
                //((DataGridViewCheckBoxCell)Row.Cells[0]).Value = checkBox1.Checked;
                if (bool.Parse(Row.Cells[0].FormattedValue.ToString()))
                {
                    DataRow dr = dt.NewRow();
                    dr["用户"] = dts.Rows[i]["user_id"].ToString();
                    dr["线路"] = dts.Rows[i]["line_no"].ToString();
                    dr["检查开始时间"] = dts.Rows[i]["BEGIN_TIME"].ToString();
                    dr["检查结束时间"] = dts.Rows[i]["END_TIME"].ToString();
                    dr["发现时间"] = dts.Rows[i]["UPDATE_TIME"].ToString();
                    dr["类型"] = dts.Rows[i]["WARN_TYPE"].ToString();
                    dr["录入人"] = dts.Rows[i]["USERNAME"].ToString();
                    // al.Add("delete from ts_warn_user where id='" + Row.Cells[2].FormattedValue.ToString() + "'");
                    dt.Rows.Add(dr);
                }

                i++;
            }
            if (dt.Rows.Count > 0)
            {
                DBHelper.A_ExcelHelper ae = new DBHelper.A_ExcelHelper();
                ae.write_excel(dt); 
            }
            else
            {
                MessageBox.Show("无数据导出");
            }
        }
    }
}

