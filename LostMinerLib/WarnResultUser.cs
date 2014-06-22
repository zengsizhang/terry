namespace LostMinerLib
{
   
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class WarnResultUser : Form
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private DataGridViewButtonColumn Column1;
        private ComboBox comboBox1;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private ComboBox line_no;
        private TextBox textBox1;
        private DataGridViewButtonColumn 删除;

        public WarnResultUser()
        {
            this.InitializeComponent();
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
            string sql = "select id '编号', user_id '用户编号',begin_time '检查开始时间',end_time '检查结束时间', warn_type '用户类型',line_no '线路',update_time '确认时间' from ts_warn_user where   warn_type='窃电'  order by id desc ";
            DataTable dt = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
            this.dataGridView1.DataSource = dt;
            DataTable dt_line = DBHelper.DBHelper.GetDataSet("select distinct line_no from ts_line").Tables[0];
            this.add_item(dt_line);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select id '编号', user_id '用户编号',begin_time '检查开始时间',end_time '检查结束时间', warn_type '用户类型',line_no '线路',update_time '确认时间' from ts_warn_user WHERE warn_type='窃电'  ";
            if (this.textBox1.Text != "")
            {
                sql = sql + " and user_id like '%" + this.textBox1.Text + "%' ";
            }
            if (this.line_no.SelectedItem.ToString() != "请选择线路")
            {
                sql = sql + " and line_no='" + this.line_no.SelectedItem.ToString() + "' ";
            }
            DataTable dt = DBHelper.DBHelper.GetDataSet(sql + " order by id desc").Tables[0];
            this.dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new WarnInput().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (e.RowIndex != -1))
            {
                new Result_XY_Select(this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(), this.dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString(), this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(), this.dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString(), this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), this.dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()).ShowDialog();
            }
            if ((e.ColumnIndex == 1) && (e.RowIndex != -1))
            {
                DBHelper.DBHelper.ExecuteCommand("delete from ts_warn_user where id='" + this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'");
                this.bingdata();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (e.RowIndex != -1))
            {
                DataTable dt = (DataTable) this.dataGridView1.DataSource;
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            this.groupBox1 = new GroupBox();
            this.line_no = new ComboBox();
            this.label3 = new Label();
            this.comboBox1 = new ComboBox();
            this.label2 = new Label();
            this.button3 = new Button();
            this.button2 = new Button();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.button1 = new Button();
            this.groupBox2 = new GroupBox();
            this.dataGridView1 = new DataGridView();
            this.Column1 = new DataGridViewButtonColumn();
            this.删除 = new DataGridViewButtonColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.line_no);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x3fd, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询";
            this.line_no.FormattingEnabled = true;
            this.line_no.Items.AddRange(new object[] { "未检查", "已检查" });
            this.line_no.Location = new Point(0x100, 0x2a);
            this.line_no.Name = "line_no";
            this.line_no.Size = new Size(0x79, 20);
            this.line_no.TabIndex = 8;
            this.line_no.Text = "请选择线路";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xce, 0x2b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "线路";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "未检查", "已检查" });
            this.comboBox1.Location = new Point(0x1f2, 0x23);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x79, 20);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.Text = "未检查";
            this.comboBox1.Visible = false;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x1a2, 0x27);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "检查状态";
            this.label2.Visible = false;
            this.button3.Location = new Point(0x1f2, 0x47);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 4;
            this.button3.Text = "关闭";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new EventHandler(this.button3_Click);
            this.button2.Location = new Point(0x25b, 0x47);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 3;
            this.button2.Text = "录入";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.textBox1.Location = new Point(0x54, 0x29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(100, 0x15);
            this.textBox1.TabIndex = 2;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x13, 0x2b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3b, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户编号:";
            this.button1.Location = new Point(0x18c, 0x47);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Dock = DockStyle.Fill;
            this.groupBox2.Location = new Point(0, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x3fd, 0x165);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据";
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.Column1, this.删除 });
            this.dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.Location = new Point(3, 0x11);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.Size = new Size(0x3f7, 0x151);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "查看";
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.FillWeight = 50f;
            this.Column1.HeaderText = "查看";
            this.Column1.Name = "Column1";
            this.Column1.Text = "查看";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "删除";
            this.删除.DefaultCellStyle = dataGridViewCellStyle2;
            this.删除.HeaderText = "删除";
            this.删除.Name = "删除";
            this.删除.Resizable = DataGridViewTriState.True;
            this.删除.SortMode = DataGridViewColumnSortMode.Automatic;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3fd, 0x1c9);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "WarnResultUser";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "窃电用户";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            base.ResumeLayout(false);
        }

        private void xy_FormClosed(object sender, EventArgs e)
        {
            this.bingdata();
        }
    }
}

