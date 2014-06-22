namespace LostMinerLib
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using TimeSearcher;

    public class XY_Select : Form
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
        private ComboBox comboBox1;
        private IContainer components = null;
        private DateTimePicker end_time_t;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label time_span;
        private OpenFileDialog upfileDoalog;
        private Label label3;
        private Label warn_user;
        private TextBox textBox1;
        private Label label4;
        private TextBox textBox2;
        private ComboBox comboBox2;
        private string model;

        public XY_Select( string id, string warn_type)
        {
            this.InitializeComponent();
           // this._user_id = user_id;
            //this._line_no = line_no;
            //t/his._begin_time = begin_time;
            //this._end_time = end_time;
            this._id = id;
            this.binddata();
            //this.begin_time_t.Value = DateTime.Parse(this._begin_time);
            //this.end_time_t.Value = DateTime.Parse(this._end_time);
            this.warn_user.Text = this.warn_user.Text + ";" + this._user_id;
            this.comboBox1.SelectedItem = warn_type;
            
            model = "update";
           
        }
        public XY_Select()
        {
            this.InitializeComponent();
            model = "insert";
            textBox1.Visible = true;
            DBHelper.DBiniClass iniclass = new DBHelper.DBiniClass(".\\system.ini");
            string checkms = iniclass.IniReadValue("checkms", "msstr");
            string[] msstr = checkms.Split(';');
            comboBox2.DataSource = msstr;
            //this.warn_user.Text = this.warn_user.Text + ";" + this._user_id;
            //this._user_id = user_id;
            //this._line_no = line_no;
            //t/his._begin_time = begin_time;
            //this._end_time = end_time;
       
        }

        private void binddata()
        {
            int j;
            DataTable war_dt = DBHelper.DBHelper.GetDataSet("select * from ts_warn_user where id='" + this._id + "'").Tables[0];
            DBHelper.DBiniClass iniclass = new DBHelper.DBiniClass(".\\system.ini");
            string checkms = iniclass.IniReadValue("checkms", "msstr");
            string[] msstr = checkms.Split(';');
            comboBox2.DataSource = msstr;
            if (checkms.Contains(war_dt.Rows[0]["notes"].ToString()))
            {
                comboBox2.Text = war_dt.Rows[0]["notes"].ToString();
            }
            else
            { 
            
            }
            this._user_id = war_dt.Rows[0]["user_id"].ToString();
            this.textBox2.Text = war_dt.Rows[0]["line_no"].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (model == "update")
                {
                    int i = DBHelper.DBHelper.ExecuteCommand("update ts_warn_user set notes='" + this.comboBox2.Text + "' , begin_time='" + this.begin_time_t.Value.ToString("yyyy-MM-dd") + "' ,end_time='" + this.end_time_t.Value.ToString("yyyy-MM-dd") + "' , warn_type='" + this.comboBox1.SelectedItem.ToString() + "' where id='" + this._id + "'");
                    MessageBox.Show("保存成功");
                }
                else
                {
                    DBHelper.DBiniClass iniclass = new DBHelper.DBiniClass(".\\system.ini");

                    string username = iniclass.IniReadValue("username", "username");
                    string sql = @"insert into ts_warn_user(USER_ID,WARN_TYPE,BEGIN_TIME,END_TIME,LINE_NO,IS_CHECK,NOTES,UPDATE_TIME,USERNAME) "+
                          "values('" + textBox1.Text + "','" + comboBox1.Text + "','" + begin_time_t.Value.ToString("yyyy-MM-dd") + "','" + end_time_t.Value.ToString("yyyy-MM-dd") + "','" + textBox2.Text + "','已检查','"+comboBox2.Text+"','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','" + username + "')";
                  int i=  DBHelper.DBHelper.ExecuteCommand(sql);
                  MessageBox.Show("保存成功");
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
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
            this.button1 = new System.Windows.Forms.Button();
            this.warn_user = new System.Windows.Forms.Label();
            this.time_span = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.end_time_t = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.begin_time_t = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.upfileDoalog = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(109, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // warn_user
            // 
            this.warn_user.AutoSize = true;
            this.warn_user.Location = new System.Drawing.Point(30, 26);
            this.warn_user.Name = "warn_user";
            this.warn_user.Size = new System.Drawing.Size(53, 12);
            this.warn_user.TabIndex = 2;
            this.warn_user.Text = "用户编号";
            // 
            // time_span
            // 
            this.time_span.AutoSize = true;
            this.time_span.Location = new System.Drawing.Point(30, 51);
            this.time_span.Name = "time_span";
            this.time_span.Size = new System.Drawing.Size(53, 12);
            this.time_span.TabIndex = 3;
            this.time_span.Text = "开始时间";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "窃电",
            "嫌疑",
            "漏计",
            "正常"});
            this.comboBox1.Location = new System.Drawing.Point(512, 44);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Text = "嫌疑";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.end_time_t);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.begin_time_t);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.warn_user);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.time_span);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(932, 285);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "嫌疑 窃电用户显示";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(445, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "用户类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "分析说明";
            // 
            // end_time_t
            // 
            this.end_time_t.Location = new System.Drawing.Point(291, 44);
            this.end_time_t.Name = "end_time_t";
            this.end_time_t.Size = new System.Drawing.Size(121, 21);
            this.end_time_t.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(223, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "结束时间";
            // 
            // begin_time_t
            // 
            this.begin_time_t.Location = new System.Drawing.Point(95, 45);
            this.begin_time_t.Name = "begin_time_t";
            this.begin_time_t.Size = new System.Drawing.Size(121, 21);
            this.begin_time_t.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(243, 227);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // upfileDoalog
            // 
            this.upfileDoalog.FileName = "openFileDialog1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(95, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(254, 21);
            this.textBox1.TabIndex = 12;
            this.textBox1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(379, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "线路";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(425, 16);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(157, 21);
            this.textBox2.TabIndex = 14;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(94, 88);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(192, 20);
            this.comboBox2.TabIndex = 25;
            // 
            // XY_Select
            // 
            this.ClientSize = new System.Drawing.Size(932, 285);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XY_Select";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "嫌疑用户详细";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void upfile_Click(object sender, EventArgs e)
        {
            if (Utils.SafeShowDialog(this.upfileDoalog) == DialogResult.OK)
            {
                for (int i = 0; i < this.upfileDoalog.FileNames.Length; i++)
                {
                }
            }
        }
    }
}

