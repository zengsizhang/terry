namespace LostMinerLib
{

    using LostMinerLib.Util;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class TaskOperationRecord : Form
    {
        private Button button1;
        private Button button3;
        private ComboBox comboBox1;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private TextBox textBox1;

        public TaskOperationRecord()
        {
            this.InitializeComponent();
            this.bingdata();
        }

        private void bingdata()
        {
            string sql = "select task_name '任务名',line_no '线路',user_id '用户号',create_time '创建时间'  from task_view ";
            DataTable dt = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
            this.dataGridView1.DataSource = dt;
            sql = "select distinct task_name,task_id from ts_task order by create_time desc";
            DataTable dt1 = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.Add("全部");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.comboBox1.Items.Add(new LostMinerLib.Util.ListItem(dt1.Rows[i]["task_id"].ToString(), dt1.Rows[i]["task_name"].ToString()));
            }
            if (this.comboBox1.Items.Count > 0)
            {
                this.comboBox1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select task_name '任务名',line_no '线路',user_id '用户号',create_time '创建时间'  from task_view where 1=1 ";
            if (this.textBox1.Text != "")
            {
                sql = sql + " and line_no like '%" + this.textBox1.Text + "%' ";
            }
            if (this.comboBox1.SelectedItem.ToString() != "全部")
            {
                sql = sql + " and task_id='" + ((LostMinerLib.Util.ListItem) this.comboBox1.SelectedItem).Key + "'";
            }
            DataTable dt = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
            this.dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WarnInput i = new WarnInput();
            i.Show();
            i.FormClosed += new FormClosedEventHandler(this.i_FormClosed);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (e.RowIndex != -1))
            {
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
            this.groupBox1 = new GroupBox();
            this.comboBox1 = new ComboBox();
            this.label2 = new Label();
            this.button3 = new Button();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.button1 = new Button();
            this.groupBox2 = new GroupBox();
            this.dataGridView1 = new DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x2d2, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(0xe1, 40);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x8d, 20);
            this.comboBox1.TabIndex = 6;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(190, 0x2e);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "任务";
            this.button3.Location = new Point(490, 0x29);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 4;
            this.button3.Text = "关闭";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new EventHandler(this.button3_Click);
            this.textBox1.Location = new Point(0x3d, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(100, 0x15);
            this.textBox1.TabIndex = 2;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x13, 0x2b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "线路:";
            this.button1.Location = new Point(0x199, 0x2b);
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
            this.groupBox2.Size = new Size(0x2d2, 0x165);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据";
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.Location = new Point(3, 0x11);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.Size = new Size(0x2cc, 0x151);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2d2, 0x1c9);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "TaskOperationRecord";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "操作记录";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            base.ResumeLayout(false);
        }

        private void xy_FormClosed(object sender, EventArgs e)
        {
        }
    }
}

