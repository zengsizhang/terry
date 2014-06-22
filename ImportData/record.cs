namespace ImportData
{
  
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class record : Form
    {
        private Button btn_close;
        private IContainer components = null;
        private TextBox textBox1;

        public record()
        {
            this.InitializeComponent();
            this.get_reocrd();
        }

        private void btn_close_Click(object sender, EventArgs e)
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

        private void get_reocrd()
        {
            DataTable dt = DBHelper.DBHelper.GetDataSet("select * from ts_import_record order by id desc").Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.textBox1.Text = this.textBox1.Text + "\r\n";
                if (dt.Rows[i]["excel_row"].ToString() == "0")
                {
                    this.textBox1.Text = this.textBox1.Text + dt.Rows[i]["user_id_change"].ToString() + dt.Rows[i]["user_id"].ToString() + dt.Rows[i]["ct"].ToString() + "\r\n";
                }
                else
                {
                    this.textBox1.Text = this.textBox1.Text + "线路" + dt.Rows[i]["line_no"].ToString() + "：原EXCEL中第" + dt.Rows[i]["excel_row"].ToString() + "行CT值为" + dt.Rows[i]["ct"].ToString() + "的用户" + dt.Rows[i]["user_id"].ToString() + "用户编号改为" + dt.Rows[i]["user_id_change"].ToString() + "\r\n";
                }
            }
        }

        private void InitializeComponent()
        {
            this.textBox1 = new TextBox();
            this.btn_close = new Button();
            base.SuspendLayout();
            this.textBox1.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox1.Location = new Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x26d, 0x185);
            this.textBox1.TabIndex = 1;
            this.btn_close.Location = new Point(0x1f8, 0x18b);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new Size(0x4b, 0x17);
            this.btn_close.TabIndex = 0;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new EventHandler(this.btn_close_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x26e, 0x1a8);
            base.Controls.Add(this.btn_close);
            base.Controls.Add(this.textBox1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "record";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "日志记录";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

