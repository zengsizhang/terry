using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LostMinerLib
{
    public partial class DataBrowse : Form
    {
        public DataBrowse()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView3.AutoGenerateColumns = false;
            this.AcceptButton = button1;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                string sql = "select * from ts_line where 1=1 ";
                if (textBox1.Text != "")
                {
                    sql += " and line_no like '%" + textBox1.Text + "%'";
                }
                if (dateTimePicker1.Value.ToString("yyyy-MM-dd") != dateTimePicker2.Value.ToString("yyyy-MM-dd"))
                {

                    sql += "  and date_time >='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and date_time <='" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ";
                }
                dataGridView1.DataSource = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
            }
            if (checkBox2.Checked)
            {
                user_Click();
            }
            if (checkBox3.Checked)
            {
                monitor_Click();
            }
            
        }

        private void user_Click()
        {
            string sql1 = "select * from ts_user where 1=1 ";
            if (textBox1.Text != "")
            {
                sql1 += " and line_no like '%" + textBox1.Text + "%'";
            }
            if (dateTimePicker2.Value.ToString("yyyy-MM-dd") != dateTimePicker1.Value.ToString("yyyy-MM-dd"))
            {

                sql1 += "  and date_time >='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and date_time <='" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ";
            }
            dataGridView2.DataSource = DBHelper.DBHelper.GetDataSet(sql1).Tables[0];

        }

        private void monitor_Click()
        {
            string sql2 = "select * from v_ts_moniter where 1=1 ";
            if (textBox1.Text != "")
            {
                sql2 += " and line_no like '%" + textBox1.Text + "%'";
            }
            if (dateTimePicker1.Value.ToString("yyyy-MM-dd") != dateTimePicker2.Value.ToString("yyyy-MM-dd"))
            {
                if ((dateTimePicker2.Value - dateTimePicker1.Value).Days > 30)
                {
                    MessageBox.Show("由于辅助数据量较大，请选择一个月时间范围进行查询");
                }
                else
                {
                    sql2 += "  and date_time >='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and date_time <='" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ";
                    dataGridView3.DataSource = DBHelper.DBHelper.GetDataSet(sql2).Tables[0];
                }
            }
            else
            {
                MessageBox.Show("由于辅助数据量较大，请选择一个月时间范围进行查询");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                this.AcceptButton = button1;
             }
            
        }
    }
}
