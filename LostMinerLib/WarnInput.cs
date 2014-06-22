namespace LostMinerLib
{
   
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class WarnInput : Form
    {
        private DateTimePicker begin_time;
        private Button btn_save;
        private ComboBox comboBox1;
        private IContainer components = null;
        private DateTimePicker end_time;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox user_id;
        private ComboBox warn_type;

        public WarnInput()
        {
            this.InitializeComponent();
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
            }
        }

        private void binddata()
        {
            DataTable dt = DBHelper.DBHelper.GetDataSet("select distinct line_no from ts_line").Tables[0];
            this.add_item(dt);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if ((this.user_id.Text == "") || (this.user_id.Text == string.Empty))
            {
                MessageBox.Show("用户编号不能为空");
            }
            else
            {
                DBHelper.DBHelper.ExecuteCommand("insert into ts_warn_user(user_id,begin_time,end_time,warn_type) values('" + this.user_id.Text + "','" + DateTime.Parse(this.begin_time.Text).ToString("yyyy-MM-dd") + "','" + DateTime.Parse(this.end_time.Text).ToString("yyyy-MM-dd") + "','" + this.warn_type.SelectedItem.ToString() + "')");
                string time_spanstr = "增加" + this.user_id.Text + "用户" + this.warn_type.SelectedItem.ToString() + "数据";
                DBHelper.DBHelper.ExecuteCommand("insert into ts_operation_record(operation_text,operation_type,operation_date) values('" + time_spanstr + "','增加窃电嫌疑数据','" + DateTime.Now.ToString("yyyy-MM-dd") + "')");
            }
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
            this.label1 = new Label();
            this.user_id = new TextBox();
            this.label2 = new Label();
            this.begin_time = new DateTimePicker();
            this.end_time = new DateTimePicker();
            this.label3 = new Label();
            this.label4 = new Label();
            this.warn_type = new ComboBox();
            this.btn_save = new Button();
            this.label5 = new Label();
            this.comboBox1 = new ComboBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x1a, 0x21);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户";
            this.user_id.Location = new Point(0x4a, 0x20);
            this.user_id.Name = "user_id";
            this.user_id.Size = new Size(100, 0x15);
            this.user_id.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(2, 0x45);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "开始时间";
            this.begin_time.Location = new Point(0x4a, 0x43);
            this.begin_time.Name = "begin_time";
            this.begin_time.Size = new Size(200, 0x15);
            this.begin_time.TabIndex = 3;
            this.end_time.Location = new Point(0x4b, 0x69);
            this.end_time.Name = "end_time";
            this.end_time.Size = new Size(200, 0x15);
            this.end_time.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(4, 0x6b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "结束时间";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x1a, 0x8d);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "类型";
            this.warn_type.FormattingEnabled = true;
            this.warn_type.Items.AddRange(new object[] { "窃电", "嫌疑", "正常" });
            this.warn_type.Location = new Point(0x4b, 0x85);
            this.warn_type.Name = "warn_type";
            this.warn_type.Size = new Size(0x79, 20);
            this.warn_type.TabIndex = 7;
            this.warn_type.Text = "窃电";
            this.btn_save.Location = new Point(190, 0xd7);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new Size(0x4b, 0x17);
            this.btn_save.TabIndex = 8;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new EventHandler(this.btn_save_Click);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x1c, 0xa8);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "线路";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "窃电", "嫌疑" });
            this.comboBox1.Location = new Point(0x4b, 0xa8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x79, 20);
            this.comboBox1.TabIndex = 10;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x11c, 0x106);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.btn_save);
            base.Controls.Add(this.warn_type);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.end_time);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.begin_time);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.user_id);
            base.Controls.Add(this.label1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "WarnInput";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "信息录入";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

