namespace LostMinerLib
{

    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class RemoveData : Form
    {
        private DateTimePicker b_time;
        private Button button1;
        private Button button2;
        private ComboBox comboBox1;
        private IContainer components = null;
        private DateTimePicker e_time;
        private Label label1;
        private Label label2;
        private Button button3;
        private Label label3;

        public RemoveData()
        {
            this.InitializeComponent();
            this.binddata();
        }

        public void add_item(DataTable dt)
        {
            this.comboBox1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.comboBox1.Items.Add(dt.Rows[i][0].ToString());
            }
            if (this.comboBox1.Items.Count > 0)
            {
                this.comboBox1.SelectedIndex = 0;
                string date_time = DBHelper.DBHelper.GetScalar("select max(date_time)||';'||min(date_time) from ts_line where line_no='" + this.comboBox1.SelectedItem.ToString() + "'").ToString();
                this.b_time.Value = DateTime.Parse(date_time.Split(new char[] { ';' })[1]);
                this.e_time.Value = DateTime.Parse(date_time.Split(new char[] { ';' })[0]);
            }
            else
            {
                this.comboBox1.Items.Clear();
                this.comboBox1.Items.Add("无线路数据可删除");
                this.comboBox1.SelectedIndex = 0;
            }
        }

        private void binddata()
        {
            DataTable dt = DBHelper.DBHelper.GetDataSet("select distinct line_no from ts_line").Tables[0];
            this.add_item(dt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedItem.ToString() != "无线路数据可删除")
            {
                if (this.comboBox1.SelectedItem.ToString() == "F04新一线")
                {
                    MessageBox.Show("F04新一线做为示例数据不可删除");
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add("delete from  ts_line where line_no='" + this.comboBox1.SelectedItem.ToString() + "' and date_time>='" + this.b_time.Value.ToString("yyyy-MM-dd") + "' and date_time<='" + this.e_time.Value.ToString("yyyy-MM-dd") + "'");
                    al.Add("delete from ts_user where line_no='" + this.comboBox1.SelectedItem.ToString() + "' and date_time>='" + this.b_time.Value.ToString("yyyy-MM-dd") + "' and date_time<='" + this.e_time.Value.ToString("yyyy-MM-dd") + "'");
                    al.Add("delete from ts_user_moniter where line_no='" + this.comboBox1.SelectedItem.ToString() + "' and date_time>='" + this.b_time.Value.ToString("yyyy-MM-dd") + "' and date_time<='" + this.e_time.Value.ToString("yyyy-MM-dd") + "'");
                    al.Add("delete from ts_user_kw where line_no='" + this.comboBox1.SelectedItem.ToString() + "' and date_time>='" + this.b_time.Value.ToString("yyyy-MM-dd") + "' and date_time<='" + this.e_time.Value.ToString("yyyy-MM-dd") + "'");
                    al.Add("delete from ts_user_kw_main where line_no='" + this.comboBox1.SelectedItem.ToString() + "' and date_time>='" + this.b_time.Value.ToString("yyyy-MM-dd") + "' and date_time<='" + this.e_time.Value.ToString("yyyy-MM-dd") + "'");
                    string time_spanstr = "删除线路" + this.comboBox1.SelectedItem.ToString() + "数据，及该线路下所有用户数据";
                    al.Add("insert into ts_operation_record(operation_text,operation_type,operation_date) values('" + time_spanstr + "','删除数据','" + DateTime.Now.ToString("yyyy-MM-dd") + "')");
                    DBHelper.DBHelper.ExecuteCommand(al);
                    MessageBox.Show("删除成功");
                    this.binddata();
                }
            }
            else
            {
                MessageBox.Show("无线路数据可删除");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedItem.ToString() != "无线路数据可删除")
            {
                string date_time = DBHelper.DBHelper.GetScalar("select max(date_time)||';'||min(date_time) from ts_line where line_no='" + this.comboBox1.SelectedItem.ToString() + "'").ToString();
                this.b_time.Value = DateTime.Parse(date_time.Split(new char[] { ';' })[1]);
                this.e_time.Value = DateTime.Parse(date_time.Split(new char[] { ';' })[0]);
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.b_time = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.e_time = new System.Windows.Forms.DateTimePicker();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "线路:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(53, 27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(384, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(481, 69);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // b_time
            // 
            this.b_time.Location = new System.Drawing.Point(244, 28);
            this.b_time.Name = "b_time";
            this.b_time.Size = new System.Drawing.Size(113, 21);
            this.b_time.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "开始时间:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(365, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "结束时间:";
            // 
            // e_time
            // 
            this.e_time.Location = new System.Drawing.Point(425, 28);
            this.e_time.Name = "e_time";
            this.e_time.Size = new System.Drawing.Size(113, 21);
            this.e_time.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(274, 68);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "全部清除数据";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // RemoveData
            // 
            this.ClientSize = new System.Drawing.Size(595, 104);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.e_time);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.b_time);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RemoveData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "删除数据";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否全部删除数据?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ArrayList al = new ArrayList();
                string sql = "delete from ts_user where line_no<>'F04新一线';";
                al.Add(sql);
                sql = "delete from ts_line where line_no<>'F04新一线';";
                al.Add(sql);
                sql = "delete from ts_user_moniter where line_no<>'F04新一线';";
                al.Add(sql);
                DBHelper.DBHelper.ExecuteCommand(al);
                MessageBox.Show("数据删除完成");
              //  Application.Restart();
            }
        }
    }
}

