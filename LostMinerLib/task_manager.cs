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
    public partial class task_manager : Form
    {
        private TimeSearcher.TimeSearcherForm _tsForm;
        public task_manager(TimeSearcher.TimeSearcherForm ts)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            _tsForm = ts;
            bind_data();
        }
        private void bind_data()
        {
            string sql = "select * from ts_line_record where 1=1 ";
            if (line_no.Text != "")
            {
                sql += " and line_no like '%" + line_no.Text + "%'";
            }
            if (dateTimePicker1.Value.ToString("yyyy-MM-dd") != dateTimePicker2.Value.ToString("yyyy-MM-dd"))
            {
                sql += " and min_time>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and max_time<='" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            }
            sql += " order by import_time desc";
            dataGridView1.DataSource = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
        }
        private void sel_btn_Click(object sender, EventArgs e)
        {
            bind_data();
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // if(e.)
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 4)
            {
                if (this.dataGridView1.Rows[e.RowIndex].Cells[4].FormattedValue.ToString() == "未检查")
                {
                    this._tsForm.set_curr_begin_time(DateTime.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString()));
                    this._tsForm.set_curr_end_time(DateTime.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString()));
                    this._tsForm.set_line_lost(this.dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
                    // _tsForm.Invoke(_tsForm.mydelea);
                    //  _tsForm.btn_sure();
                    int di = int.Parse(DBHelper.DBHelper.GetScalar("select count(1) from ts_line where line_no='" + this.dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString() + "' and date_time between '" + this.dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString() + "' and '" + this.dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString() + "' ").ToString());
                    //_tsForm.set_dt(dt);
                    if (di > 0)
                    {
                        this._tsForm.re_read = true;
                        this._tsForm.openFile();
                        this._tsForm.fill_user_dt();
                        this._tsForm.RefillDetailsList("", false);
                        this._tsForm._tbPanel.SetLineno(this.dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("线路" + this.dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString() + "在时间段" + this.dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString() + "-" + this.dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString() + "无数据");
                    }
                    //this._tsForm.week_var();
                }
                else
                {
                    if (MessageBox.Show("已检查过是否继续检查?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this._tsForm.set_curr_begin_time(DateTime.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString()));
                        this._tsForm.set_curr_end_time(DateTime.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString()));
                        this._tsForm.set_line_lost(this.dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
                        // _tsForm.Invoke(_tsForm.mydelea);
                        //  _tsForm.btn_sure();
                        int di = int.Parse(DBHelper.DBHelper.GetScalar("select count(1) from ts_line where line_no='" + this.dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString() + "' and date_time between '" + this.dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString() + "' and '" + this.dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString() + "' ").ToString());
                        //_tsForm.set_dt(dt);
                        if (di > 0)
                        {
                            this._tsForm.re_read = true;
                            this._tsForm.openFile();
                            this._tsForm.fill_user_dt();
                            this._tsForm.RefillDetailsList("", false);
                            this._tsForm._tbPanel.SetLineno(this.dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("线路" + this.dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString() + "在时间段" + this.dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString() + "-" + this.dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString() + "无数据");
                        }
                    }
                }
            }
        }
    }
}
