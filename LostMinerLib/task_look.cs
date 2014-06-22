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
    public partial class task_look : Form
    {
        public task_look()
        {
            InitializeComponent();
            databind();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            databind();
        }
        private void databind()
        {
            string sql = "select　* from v_warn_user where 1=1 ";
                if(textBox1.Text!="")
                {
                    sql += " and upper(line_no) like upper('%" + textBox1.Text + "%')";
                }
                if (dateTimePicker1.Value.ToString() != "")
                {
                    sql += " and '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' >=begin_time and  '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' <=end_time ";
                }
                DataTable dt = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
                dataGridView1.DataSource = dt;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in this.dataGridView1.Rows)
            {
                ((DataGridViewCheckBoxCell)Row.Cells[0]).Value = checkBox1.Checked;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();          
            dt.Columns.Add("线路");
            dt.Columns.Add("检查开始时间");
            dt.Columns.Add("检查结束时间");
            dt.Columns.Add("检查说明");
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
                  //  dr["用户"] = dts.Rows[i]["user_id"].ToString();
                    dr["线路"] = dts.Rows[i]["line_no"].ToString();
                    dr["检查开始时间"] = dts.Rows[i]["BEGIN_TIME"].ToString();
                    dr["检查结束时间"] = dts.Rows[i]["END_TIME"].ToString();
                    dr["检查说明"] = dts.Rows[i]["NOTES"].ToString();
                    //dr["发现时间"] = dts.Rows[i]["UPDATE_TIME"].ToString();
                    //dr["类型"] = dts.Rows[i]["WARN_TYPE"].ToString();
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
