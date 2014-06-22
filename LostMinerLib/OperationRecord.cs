namespace LostMinerLib
{
    
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class OperationRecord : Form
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
        private DataGridViewTextBoxColumn 编号;
        private DataGridViewTextBoxColumn 操作记录;
        private DataGridViewTextBoxColumn 操作时间;
        private DataGridViewTextBoxColumn 操作类型;
        private TextBox textBox1;

        public OperationRecord()
        {
            this.InitializeComponent();
            this.bingdata();
        }

        private void bingdata()
        {
            string sql = "select id , operation_text ,operation_date ,operation_type   from ts_operation_record ";
            DataTable dt = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
            this.dataGridView1.DataSource = dt;
            sql = "select distinct operation_type from ts_operation_record";
            DataTable dt1 = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.Add("全部");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.comboBox1.Items.Add(dt1.Rows[i][0].ToString());
            }
            if (this.comboBox1.Items.Count > 0)
            {
                this.comboBox1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select id '编号', operation_text '操作记录',operation_date '操作时间',operation_type '操作类型'  from ts_operation_record where 1=1 ";
            if (this.textBox1.Text != "")
            {
                sql = sql + " and operation_text like '%" + this.textBox1.Text + "%' ";
            }
            if (this.comboBox1.SelectedItem.ToString() != "全部")
            {
                sql = sql + " and operation_type='" + this.comboBox1.SelectedItem.ToString() + "'";
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
                DataTable dt = (DataTable) this.dataGridView1.DataSource;
              
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.操作记录 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.操作时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.操作类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(722, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(250, 40);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(141, 20);
            this.comboBox1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(190, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "操作类型";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(490, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "关闭";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(84, 40);
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
            this.label1.Text = "操作记录:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(409, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(722, 357);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.编号,
            this.操作记录,
            this.操作时间,
            this.操作类型});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(716, 337);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // 编号
            // 
            this.编号.DataPropertyName = "id";
            this.编号.HeaderText = "编号";
            this.编号.Name = "编号";
            this.编号.Width = 60;
            // 
            // 操作记录
            // 
            this.操作记录.DataPropertyName = "OPERATION_TEXT";
            this.操作记录.HeaderText = "操作记录";
            this.操作记录.Name = "操作记录";
            this.操作记录.Width = 400;
            // 
            // 操作时间
            // 
            this.操作时间.DataPropertyName = "OPERATION_DATE";
            this.操作时间.HeaderText = "操作时间";
            this.操作时间.Name = "操作时间";
            // 
            // 操作类型
            // 
            this.操作类型.DataPropertyName = "OPERATION_TYPE";
            this.操作类型.HeaderText = "操作类型";
            this.操作类型.Name = "操作类型";
            this.操作类型.Width = 80;
            // 
            // OperationRecord
            // 
            this.ClientSize = new System.Drawing.Size(722, 457);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OperationRecord";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "操作记录";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        private void xy_FormClosed(object sender, EventArgs e)
        {
            this.bingdata();
        }
    }
}

