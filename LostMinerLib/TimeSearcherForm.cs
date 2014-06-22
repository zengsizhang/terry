using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using System.IO;
using DevExpress.XtraScheduler.UI;
using DevExpress.XtraEditors;
using System.Xml;
namespace TimeSearcher
{
    using ImportData;
    using LostMinerLib;
    using LostMinerLib.Util;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using System.Linq;
    using System.Linq.Expressions;
    using TimeSearcher.AttrStat;
    using TimeSearcher.Filters;
    using TimeSearcher.Panels;
    using TimeSearcher.Search;
    using TimeSearcher.Widgets;
    using TimeSearcher.Wizard;
    using System.Net;
    using System.Web.Services.Description;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Xml.Serialization;
    using System.Web.Services.Protocols;
    public partial class TimeSearcherForm : Form
    {
        public DataTable user_dt;
        private OpaqueCommand cmd;
        private TimeSearcher.DataSet _dataSet;
        private DataTable fu_dt;
        private DataTable dt_cl;
        private DataTable dt_rule_describe;
        private ArrayList time_al;
        private string woorkbookstr;
        private DataTable dt_rule_source;
        public delegate void delg_btn_sure();
        public delg_btn_sure mydelea;
        private bool is_rule = false;
        public TimeSearcherForm()
        {

            this.InitializeComponent();
            this.user_dt = new System.Data.DataTable();
            this.user_dt.Columns.Add(new DataColumn("is_check"));
            this.user_dt.Columns.Add(new DataColumn("is_computer"));
            this.user_dt.Columns.Add(new DataColumn("is_show"));
            this.user_dt.Columns.Add(new DataColumn("user_id"));
            this.user_dt.Columns.Add(new DataColumn("m1"));
            this.user_dt.Columns[4].DataType = System.Type.GetType("System.Double");
            this.user_dt.Columns.Add(new DataColumn("yaozhi"));
            this.user_dt.Columns[5].DataType = System.Type.GetType("System.Double");
            this.user_dt.Columns.Add(new DataColumn("sx_bili"));
            this.user_dt.Columns[6].DataType = System.Type.GetType("System.Double");
            this.user_dt.Columns.Add(new DataColumn("均值"));
            this.user_dt.Columns[7].DataType = System.Type.GetType("System.Double");
            this.user_dt.Columns.Add(new DataColumn("标准差"));
            this.user_dt.Columns[8].DataType = System.Type.GetType("System.Double");
            this.user_dt.Columns.Add(new DataColumn("rule"));
            this.user_dt.Columns[9].DataType = System.Type.GetType("System.String");
            this.cmd = new OpaqueCommand();
            this.strar_form();

            dt_cl = new DataTable();
            DataColumn cal_Id = new DataColumn("ID");
            DataColumn cal_EventType = new DataColumn("EventType");
            DataColumn cal_StartDate = new DataColumn("StartDate");
            DataColumn cal_EndDate = new DataColumn("EndDate");
            DataColumn cal_AllDay = new DataColumn("AllDay");
            DataColumn cal_Subject = new DataColumn("Subject");
            DataColumn cal_Location = new DataColumn("Location");
            DataColumn cal_Description = new DataColumn("Description");
            DataColumn cal_Status = new DataColumn("Status");
            DataColumn cal_Label = new DataColumn("Label");
            DataColumn cal_ResourceID = new DataColumn("ResourceID");
            DataColumn cal_Reminder = new DataColumn("Reminder");
            DataColumn cal_RecurrenceInfo = new DataColumn("RecurrenceInfo");
            dt_cl.Columns.Add(cal_Id); dt_cl.Columns.Add(cal_EventType);
            dt_cl.Columns.Add(cal_StartDate); dt_cl.Columns.Add(cal_EndDate);
            dt_cl.Columns.Add(cal_AllDay); dt_cl.Columns.Add(cal_Subject);
            dt_cl.Columns.Add(cal_Location); dt_cl.Columns.Add(cal_Description);
            dt_cl.Columns.Add(cal_Status); dt_cl.Columns.Add(cal_Label);
            dt_cl.Columns.Add(cal_ResourceID); dt_cl.Columns.Add(cal_RecurrenceInfo);
            dt_cl.Columns.Add(cal_Reminder);
            mydelea = new delg_btn_sure(btn_sure);


            INIClass iniclass = new INIClass(Application.StartupPath + @"\system.ini");
            string linkmodel = iniclass.IniReadValue("netlink", "linkmodel");
            string checkms = iniclass.IniReadValue("checkms", "msstr");
            string[] msstr = checkms.Split(';');
            comboBox1.DataSource = msstr;
            //  comboBox1.DataBindings;
            if (linkmodel == "oracle")
            {
                menuItem12.Visible = false;
                _loadAtrMenuItem.Visible = false;
                menuItem14.Visible = false;
                menuItem18.Visible = true;

            }



        }
        private void show_av_pic()
        {
            string[] dd = this._userpage.Text.Split(new char[] { ':' });
            string begin_time = string.Empty;
            string end_time = string.Empty;
            if (dd.Length > 0)
            {
                //string[] timestr = dd[1].Split('至');
                //begin_time = timestr[0];
                //end_time = timestr[1];
                //  begin_time = dd[1];
                string[] timestr = dd[1].Split(',');
                //if (timestr.Length > 1)
                //{
                DateTime dt_max = DateTime.MaxValue;
                DateTime dt_min = DateTime.MinValue;

                for (int i = 0; i < timestr.Length; i++)
                {
                    if (i == 0)
                    {
                        dt_max = DateTime.Parse(timestr[0].Split('_')[1]);
                        dt_min = DateTime.Parse(timestr[0].Split('_')[0]);
                    }
                    else
                    {
                        if (DateTime.Parse(timestr[i].Split('_')[1]) > dt_max)
                        {
                            dt_max = DateTime.Parse(timestr[i].Split('_')[1]);
                        }
                        if (DateTime.Parse(timestr[i].Split('_')[0]) < dt_min)
                        {
                            dt_min = DateTime.Parse(timestr[i].Split('_')[0]);
                        }
                    }
                }
                //}
                //else
                //{ 

                //}
                begin_time = dt_min.ToString("yyyy-MM-dd");
                end_time = dt_max.ToString("yyyy-MM-dd");
            }
            //string sql = "select * from ts_user_moniter t where t.line_no='" + line_lost + "' and t.date_time>='" + begin_time + "' and t.date_time<='" + end_time + "'    ";
            //System.Data.DataTable dt_rule = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
            ////图形展示

            //dt_rule.Columns[2].DataType=System.Type.GetType("System.DateTime");
            //#region
            ////A相电压
            //this.chart1.Series.Clear();
            //this.chart1.DataBindCrossTable(dt_rule.DefaultView, "user_id", "date_time", "A_V", "");
            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss"; 

            //for (int j = 0; j < this.chart1.Series.Count; j++)
            //{
            //    this.chart1.Series[j].ChartType = SeriesChartType.Line;
            //    this.chart1.Series[j].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;

            //    //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss"; 
            //    this.chart1.Series[j].BorderWidth = 1;
            //    this.chart1.Series[j].ToolTip = this.chart1.Series[j].Name;
            //}
            ////B相电压
            //this.chart2.Series.Clear();
            //this.chart2.DataBindCrossTable(dt_rule.DefaultView, "user_id", "date_time", "B_V", "");
            //chart2.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss"; 
            //for (int j = 0; j < this.chart2.Series.Count; j++)
            //{
            //    this.chart2.Series[j].ChartType = SeriesChartType.Line;
            //    this.chart2.Series[j].BorderWidth = 1;
            //    this.chart2.Series[j].ToolTip = this.chart2.Series[j].Name;
            //}
            ////C相电压
            //this.chart3.Series.Clear();
            //this.chart3.DataBindCrossTable(dt_rule.DefaultView, "user_id", "date_time", "C_V", "");
            //chart3.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss"; 
            //for (int j = 0; j < this.chart3.Series.Count; j++)
            //{
            //    this.chart3.Series[j].ChartType = SeriesChartType.Line;
            //    this.chart3.Series[j].BorderWidth = 1;
            //    this.chart3.Series[j].ToolTip = this.chart3.Series[j].Name;
            //}
            ////电压不平衡率
            //this.chart4.Series.Clear();
            //this.chart4.DataBindCrossTable(dt_rule.DefaultView, "user_id", "date_time", "KW_BANLACE", "");
            //chart4.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss"; 

            //for (int j = 0; j < this.chart4.Series.Count; j++)
            //{
            //    this.chart4.Series[j].ChartType = SeriesChartType.Line;
            //    this.chart4.Series[j].BorderWidth = 1;
            //    this.chart4.Series[j].ToolTip = this.chart4.Series[j].Name;
            //}
            ////电流不平衡率
            //this.chart5.Series.Clear();
            //this.chart5.DataBindCrossTable(dt_rule.DefaultView, "user_id", "date_time", "a_balance_no", "");
            //     chart5.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss"; ;
            //for (int j = 0; j < this.chart5.Series.Count; j++)
            //{
            //    this.chart5.Series[j].ChartType = SeriesChartType.Line;
            //    this.chart5.Series[j].BorderWidth = 1;
            //    this.chart5.Series[j].ToolTip = this.chart5.Series[j].Name;
            //}
            //#endregion
        }
        private void _aboutMenuItem_Click(object sender, EventArgs e)
        {
            new about().ShowDialog();
        }

        private void _analogousTsFinderMenuItem_Click(object sender, EventArgs e)
        {
            new MultiVarResSearchForm(this).Show();
        }

        public void btn_sure()
        {
            this.task_str = "读取数据";
            this.worker.RunWorkerAsync();
            this.cmd.ShowOpaqueLayer(this, 0x7d, true);
        }

        private void _ci1CombinerMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void _civToTqdConverterMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void _clearAllQueriesMenuItem_Click(object sender, EventArgs e)
        {
            new OperationRecord().Show();
        }

        private void _contentsMenuItem_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            this.setCurrentDirToBaseDir();
            process.StartInfo.FileName = "tsDirs/resources/help.html";
            process.StartInfo.UseShellExecute = true;
            process.Start();
        }

        private void _exitMenuItem_Click(object sender, EventArgs e)
        {
            new LostMinerLib.webserviceconfig().ShowDialog();
        }

        private void _expMenuItem_Click(object sender, EventArgs e)
        {
            this._expMenuItem.Checked = !this._expMenuItem.Checked;
            if (this._expMenuItem.Checked)
            {
                this._dataSet.DoExp();
            }
            else
            {
                this._dataSet.UndoExp();
            }
            this.dataSetComplete();
        }

        private void _filterAttributesMenuItem_Click(object sender, EventArgs e)
        {
            new AttrFilterForm(this).Show();
        }

        private void _filterTfnMenuItem_Click(object sender, EventArgs e)
        {
            string path = "C:/temp/filtered.tfn";
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(path);
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't open " + path + ". Ignoring...");
                return;
            }
            ArrayList filteredNames = new ArrayList();
            while (reader.Peek() > -1)
            {
                filteredNames.Add(reader.ReadLine());
            }
            reader.Close();
            this._dataSet.DisablingManager.AddEntity(new TFNEntity(filteredNames, this._dataSet.DisablingManager));
            this.refreshGraphsAndItemsList();
        }

        private void _filterUnselectedMenuItem_Click(object sender, EventArgs e)
        {
            this._dataSet.DisablingManager.AddEntity(new UnselectedEntity(this._dataSet.GetEnabledOnlyItemIdx(), this._dataSet.DisablingManager));
            this.refreshGraphsAndItemsList();
        }

        private void _forecastingMenuItem_Click(object sender, EventArgs e)
        {
            new WarnResultUser().ShowDialog();
        }

        private void _gridMenuItem_Click(object sender, EventArgs e)
        {
            new WarnUser().ShowDialog();
        }

        private void _hideFilteredMenuItem_Click(object sender, EventArgs e)
        {
            this._hideFilteredMenuItem.Checked = !this._hideFilteredMenuItem.Checked;
            SharedData.isGraphDisabledHidden = this._hideFilteredMenuItem.Checked;
        }

        private void _loadAtrMenuItem_Click(object sender, EventArgs e)
        {
            RemoveData r = new RemoveData();
            while (true)
            {
                r.ShowDialog();
                if (r.DialogResult == DialogResult.Cancel)
                {
                    this.reload();
                    return;
                }
            }
        }

        private void _logMenuItem_Click(object sender, EventArgs e)
        {
            this._logMenuItem.Checked = !this._logMenuItem.Checked;
            if (this._logMenuItem.Checked)
            {
                this._dataSet.DoLog();
            }
            else
            {
                this._dataSet.UndoLog();
            }
            this.dataSetComplete();
        }

        private void _mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._mainTabControl.SelectedTab == this._riverTabPage)
            {
                this.UpdateRiverPlotView();
            }
        }

        private void _openMenuItem_Click(object sender, EventArgs e)
        {
            this.bUpdating = true;
            if (Utils.SafeShowDialog(this._openTqdFileDlg) == DialogResult.OK)
            {
                INIClass iniclass = new INIClass(Application.StartupPath + @"\system.ini");
                string filename = this._openTqdFileDlg.FileNames[0].ToString();
                iniclass.IniWriteValue("file", "filepath", filename.Substring(0, filename.LastIndexOf(@"\")));
                DBHelper.DBHelper.ExecuteCommand("delete from ts_int_line");
                DBHelper.DBHelper.ExecuteCommand("delete from ts_int_user");
                DBHelper.DBHelper.ExecuteCommand("delete from ts_import_record");
                this.task_str = "导入数据";
                this.worker.RunWorkerAsync();
                this.cmd.ShowOpaqueLayer(this, 0x7d, true);
            }
            this.bUpdating = false;
        }

        private void _prefsMenuItem_Click(object sender, EventArgs e)
        {
            if (this._optionsForm.ShowDialog() == DialogResult.OK)
            {
                this._optionsForm.writeOptions();
            }
        }

        private void _riverTabPage_Resize(object sender, EventArgs e)
        {
            this._riverPlotView.layoutRiverPanels();
        }

        private void _searchOptsMenuItem_Click(object sender, EventArgs e)
        {
            new SearchOptsForm(this).Show();
        }

        private void _selectMatchesMenuItem_Click(object sender, EventArgs e)
        {
            LostMinerLib.Missing_ResultUser mr = new Missing_ResultUser();
            mr.ShowDialog();
        }

        private void _setSizeMenuItem_Click(object sender, EventArgs e)
        {
            new SetSizeForm(this).Show();
        }

        private void _shobt_Click(object sender, EventArgs e)
        {
            user_dt = (System.Data.DataTable)dgvSelectAll.DataSource;
            System.Data.DataTable dt = new System.Data.DataTable(); ;
            // dtyaozhi = new System.Data.DataTable();
            System.Data.DataColumn c0 = new System.Data.DataColumn("user");
            System.Data.DataColumn c1 = new System.Data.DataColumn("user_id");
            System.Data.DataColumn c2 = new System.Data.DataColumn("date_time");
            // c2.DataType = Type.GetType(" System.DateTime");           
            System.Data.DataColumn c3 = new System.Data.DataColumn("yaozhi_rate");
            dt.Columns.Add(c0);
            dt.Columns.Add(c1);
            dt.Columns.Add(c2);
            dt.Columns.Add(c3);

            System.Data.DataRow[] dr = user_dt.Select("is_show='True'");
            for (int i = 0; i < dr.Length; i++)
            {
                for (int j = 0; j < dtyaozhi.Rows.Count; j++)
                {
                    if (dtyaozhi.Rows[j]["user"].ToString() == dr[i]["user_id"].ToString())
                    {
                        System.Data.DataRow dry = dt.NewRow();
                        dry["user"] = dtyaozhi.Rows[j]["user"].ToString();
                        dry["user_id"] = dtyaozhi.Rows[j]["user_id"].ToString();
                        dry["date_time"] = dtyaozhi.Rows[j]["date_time"].ToString();
                        dry["yaozhi_rate"] = dtyaozhi.Rows[j]["yaozhi_rate"].ToString();
                        dt.Rows.Add(dry);
                    }

                }
            }
            System.Data.DataRow[] dr1 = dtyaozhi.Select("user='lost'");
            for (int y = 0; y < dr1.Length; y++)
            {
                System.Data.DataRow dry = dt.NewRow();
                dry["user"] = "线损";
                dry["user_id"] = "线损";
                dry["date_time"] = dr1[y]["date_time"].ToString();
                dry["yaozhi_rate"] = dr1[y]["yaozhi_rate"].ToString();
                dt.Rows.Add(dry);

            }
            new RateChart(dt, this.dtuserrate).Show();
        }

        private void _showForecastSourceMenuItem_Click(object sender, EventArgs e)
        {
            this._showForecastSourceMenuItem.Checked = !this._showForecastSourceMenuItem.Checked;
            this.InvalidateVisibleTabsDisplay();
        }

        private void _showSelectedItemsMenuItem_Click(object sender, EventArgs e)
        {
            this._showSelectedItemsMenuItem.Checked = !this._showSelectedItemsMenuItem.Checked;
            if (this._showSelectedItemsMenuItem.Checked)
            {
                if (this._itemsList.SelectedIndices.Count == 0)
                {
                    MessageBox.Show("Please select one or more items to first.");
                }
                else
                {
                    this._savedSelectedItemIdx = this._dataSet.GetSelectedAndEnabledItemIdx();
                    string str = this._optionsForm._lastFilename;
                    this.setCurrentDirToBaseDir();
                    this.saveFile("tsDirs/Temp/selectedItems.tqd");
                    this.openFile("tsDirs/Temp/selectedItems.tqd", true);
                    this._optionsForm._lastFilename = str;
                }
            }
            else
            {
                this.openFile(this._optionsForm._lastFilename, true);
                this._itemsListManager.SelectTheseDataItemsOnly(this._savedSelectedItemIdx);
            }
        }

        private void _showSelectionBoxesMenuItem_Click(object sender, EventArgs e)
        {
            this._showSelectionBoxesMenuItem.Checked = !this._showSelectionBoxesMenuItem.Checked;
            if (this.IsDataSetLoaded)
            {
                foreach (VariablePanel panel in this._varView.VarPanels)
                {
                    panel.showSelectionBox(this._showSelectionBoxesMenuItem.Checked);
                }
            }
        }

        private void _showTimeLineMenuItem_Click(object sender, EventArgs e)
        {
            this._showTimeLineMenuItem.Checked = !this._showTimeLineMenuItem.Checked;
            this.invalidateVisiblePanels();
        }

        private void _sxdgv_SelectionChanged(object sender, EventArgs e)
        {
            ArrayList al = new ArrayList();
            DataTable dt = this._sx_datateble.Clone();
            if (this._sxdgv.SelectedCells.Count > 0)
            {
                try
                {
                    int j;
                    for (int i = 0; i < this._sxdgv.SelectedCells.Count; i++)
                    {
                        j = 0;
                        while (j < this._sx_datateble.Rows.Count)
                        {
                            if (this._sxdgv.SelectedCells[i].Value.ToString() == this._sx_datateble.Rows[j]["user_id"].ToString())
                            {
                                DataRow dr = dt.NewRow();
                                for (int r = 0; r < dt.Columns.Count; r++)
                                {
                                    dr[r] = this._sx_datateble.Rows[j][r].ToString();
                                }
                                dt.Rows.Add(dr);
                            }
                            j++;
                        }
                    }
                    this.chart_sx.Series.Clear();
                    this.chart_sx.DataBindCrossTable(dt.DefaultView, "user_id", "weeks", "sx", "");
                    for (j = 0; j < this.chart_sx.Series.Count; j++)
                    {
                        this.chart_sx.Series[j].ChartType = SeriesChartType.Line;
                        this.chart_sx.Series[j].BorderWidth = 3;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void _syncDetailsListMenuItem_Click(object sender, EventArgs e)
        {
            this._syncDetailsListMenuItem.Checked = !this._syncDetailsListMenuItem.Checked;
            if (this.IsDetailsListSynchronized)
            {
            }
        }

        private void _syncOverviewMenuItem_Click(object sender, EventArgs e)
        {
            this._syncOverviewMenuItem.Checked = !this._syncOverviewMenuItem.Checked;
        }

        private void _testMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void _undoFilterUnselectedMenuItem_Click(object sender, EventArgs e)
        {
            new TaskOperationRecord().ShowDialog();
        }

        private void _wizardMenuItem_Click(object sender, EventArgs e)
        {
            new WizardForm(this).ShowDialog(this);
        }

        private void btn_check_all_Click(object sender, EventArgs e)
        {
            new DBHelper.A_ExcelHelper().write_excel(this._sx_table, this._sx_data);
        }

        private void btn_save_sx_Click(object sender, EventArgs e)
        {
            DataRow[] drc = ((DataTable)this.dgvSelectAll.DataSource).Select("is_show='TRUE'");
            ArrayList sql_al = new ArrayList();
            string[] dd = this._userpage.Text.Split(new char[] { ':' });
            string begin_time = string.Empty;
            string end_time = string.Empty;
            if (dd.Length > 0)
            {
                //string[] timestr = dd[1].Split('至');
                //begin_time = timestr[0];
                //end_time = timestr[1];
                //  begin_time = dd[1];
                string[] timestr = dd[1].Split(',');
                //if (timestr.Length > 1)
                //{
                DateTime dt_max = DateTime.MaxValue;
                DateTime dt_min = DateTime.MinValue;

                for (int i = 0; i < timestr.Length; i++)
                {
                    if (i == 0)
                    {
                        dt_max = DateTime.Parse(timestr[0].Split('_')[1]);
                        dt_min = DateTime.Parse(timestr[0].Split('_')[0]);
                    }
                    else
                    {
                        if (DateTime.Parse(timestr[i].Split('_')[1]) > dt_max)
                        {
                            dt_max = DateTime.Parse(timestr[i].Split('_')[1]);
                        }
                        if (DateTime.Parse(timestr[i].Split('_')[0]) < dt_min)
                        {
                            dt_min = DateTime.Parse(timestr[i].Split('_')[0]);
                        }
                    }
                }
                //}
                //else
                //{ 

                //}
                begin_time = dt_min.ToString("yyyy-MM-dd");
                end_time = dt_max.ToString("yyyy-MM-dd");
            }
            string time_spanstr = "从" + begin_time + "至" + end_time + "进行线路检查，窃电嫌疑用户为：";
            for (int i = 0; i < drc.Length; i++)
            {
                //if ((begin_time != string.Empty) && (end_time != string.Empty))
                //{
                INIClass iniclass = new INIClass(Application.StartupPath + @"\system.ini");
                string linkmodel = iniclass.IniReadValue("netlink", "linkmodel");
                string username = iniclass.IniReadValue("username", "username");
                sql_al.Add("insert into ts_warn_user(user_id,begin_time,end_time,warn_type,line_no,is_check,update_time,username,notes) values('" + drc[i]["user_id"].ToString() + "','" + begin_time + "','" + end_time + "','嫌疑','" + this._tbPanel.getLineno() + "','未检查','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" + username + "','" + comboBox1.Text + "')");

                time_spanstr = time_spanstr + drc[i]["user_id"].ToString() + ";";

                if (linkmodel == "sqlite")
                {
                    sql_al.Add("update ts_line_record set is_check=' 已检查',check_time='" + System.DateTime.Now.ToShortDateString() + "' where line_no='" + this._tbPanel.getLineno() + "' and ('" + begin_time + "' between min_time and max_time   or '" + end_time + "' between min_time and max_time ) ");
                }
                sql_al.Add("insert into ts_operation_record(operation_text,operation_type,operation_date) values('" + time_spanstr + "','窃电嫌疑扫描','" + DateTime.Now.ToString("yyyy-MM-dd") + "')");


                //this.btn_save_sx.Enabled = false;
                // this._shobt.Enabled = false;
                this.btn_check_all.Enabled = false;
            }
            DBHelper.DBHelper.ExecuteCommand(sql_al);
            MessageBox.Show("嫌疑用户已保存入库");


        }

        private void cbIndividualItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._selectedIndividualsVariable != this._cbIndividualItems.SelectedIndex)
            {
                this._selectedIndividualsVariable = this._cbIndividualItems.SelectedIndex;
                this.createItemPanels(this._selectedIndividualsVariable);
                this.layoutItemPanels();
            }
        }

        private void clearAllQueries()
        {
            this._dataSet.DisablingManager.DematerializeAllEntities();
            this.invalidateVisiblePanels();
            this.RefillItemsList();
        }

        public void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            this.cmd.HideOpaqueLayer();
            if (this.task_str == "导入功率数据")
            {
                MessageBox.Show("瞬时电量数据导入完成");
            }
            else if (this.task_str == "导入表码数据")
            {
                MessageBox.Show("主表数据导入完成");
            }
            else if (this.task_str == "导入负表数据")
            {
                MessageBox.Show("负表数据导入完成");
            }
            else if (this.task_str == "分析数据")
            {
                MessageBox.Show("分析完成");
            }
            else if (this.task_str == "读取数据")
            {
                MessageBox.Show("读取数据");
            }
            else if (this.task_str == "导出数据")
            {
                try
                {


                    // string saveFileName = System.Windows.Forms.Application.ExecutablePath + "\\" + System.DateTime.Now.ToString("yyyy-MM-dd") + "_" + System.DateTime.Now.ToString("HHmmss") + ".xls";
                    //workbook.SaveCopyAs(saveFileName);
                    string saveFileName = System.DateTime.Now.ToString("yyyy-MM-dd") + "_" + System.DateTime.Now.ToString("HHmmss") + ".xls";
                    SaveFileDialog SaveFile = new SaveFileDialog();
                    SaveFile.FileName = saveFileName;
                    SaveFile.Filter = "Miscrosoft Office Excel 97-2003 工作表|*.xls|所有文件(*.*)|*.*";
                    SaveFile.RestoreDirectory = true;
                    if (SaveFile.ShowDialog() == DialogResult.OK)
                    {
                        //  workbook.SaveCopyAs(SaveFile.FileName);
                        System.IO.File.Copy(Application.StartupPath + "\\" + woorkbookstr, SaveFile.FileName, true);
                        //  System.IO.FileStream fs = (System.IO.FileStream)SaveFile.OpenFile();
                        // System.IO.File.WriteAllText(SaveFile.FileName, woorkbookstr, System.Text.Encoding.Default);
                        // MessageBox.Show("导出数据成功!", "系统信息");
                    }

                    // workbook.Close();

                    //workbook.Close(true, saveFileName, System.Reflection.Missing.Value);
                    //  return saveFileName;
                }
                catch (Exception er) { throw er; }
            }
            else
            {
                bool is_mult = false;
                if (this._openTqdFileDlg.FileNames.Length > 1)
                {
                    is_mult = true;
                }
                wizard_first w = new wizard_first(is_mult);
                while (true)
                {
                    w.ShowDialog();
                    if (w.DialogResult == DialogResult.Cancel)
                    {
                        this.w_FormClosed();
                        return;
                    }
                }
            }
        }

        private int ComputeFibonacci(object sender, DoWorkEventArgs e)
        {
            this.worker.ReportProgress(50);
            if (this.task_str == "导入功率数据")
            {
                for (int i = 0; i < this._openTqdFileDlg.FileNames.Length; i++)
                {
                    new DBHelper.A_ExcelHelper(this._openTqdFileDlg.FileNames[i]).GetKw();
                }
            }
            else if (this.task_str == "导入表码数据")
            {
                for (int i = 0; i < this._openTqdFileDlg.FileNames.Length; i++)
                {
                    new DBHelper.A_ExcelHelper(this._openTqdFileDlg.FileNames[i]).GetMFkw(true);
                }

            }
            else if (this.task_str == "导入负表数据")
            {
                for (int i = 0; i < this._openTqdFileDlg.FileNames.Length; i++)
                {
                    new DBHelper.A_ExcelHelper(this._openTqdFileDlg.FileNames[i]).GetMFkw(false);
                }
            }
            else if (this.task_str == "分析数据")
            {
                if (this.static_array(time_al))
                {
                    this.btn_save_sx.Enabled = true;
                    if (is_rule)
                    {
                       // get_rule(time_al);

                    }
                    else
                    {

                    }
                }
            }
            else if (this.task_str == "导出数据")
            {
                woorkbookstr = new DBHelper.A_ExcelHelper().write_excel(this.source_double, this.user_dt, this.user_c, this.date_str_excel, this.source_yaozhi, fu_dt, Application.StartupPath.ToString());
            }
            else if (this.task_str == "读取数据")
            {

                //   CheckForillegalCrossThreadCalls = false;
                CheckForIllegalCrossThreadCalls = false;
                DataTable dt = DBHelper.DBHelper.GetDataSet("select * from v_ts_moniter t where t.line_no='" + line_lost + "'").Tables[0];
                set_dt(dt);
                re_read = true;
                openFile();
                fill_user_dt();
                RefillDetailsList("", false);
            }
            else
            {
                for (int i = 0; i < this._openTqdFileDlg.FileNames.Length; i++)
                {
                    string dd = new DBHelper.A_ExcelHelper(this._openTqdFileDlg.FileNames[i], true).GetContent();

                }

            }
            this.worker.ReportProgress(100);
            Thread.Sleep(10);
            return -1;
        }

        private void createItemPanels(int varIdx)
        {
            if (this.IsDataSetLoaded)
            {
                int num = 100;
                this._itemPanels = new ItemVariablePanel[this._dataSet.NumItems];
                for (int i = 0; i < this._dataSet.NumItems; i++)
                {
                    ItemPanel itemPanel = new ItemPanel(this, varIdx, i);
                    ItemVariablePanel panel2 = new ItemVariablePanel(this, itemPanel);
                    panel2.SetBounds(this._leftPanel.Left, (this._leftPanel.Top + this._cbIndividualItems.Height) + (i * num), this._bottomPanel.Width, this._bottomPanel.Height);
                    this._itemPanels[i] = panel2;
                }
            }
        }

        private void createLayout()
        {
            this._progressBar = new ProgressBar();
            this._progressBar.Parent = this._statusBar;
            this._progressBar.Dock = DockStyle.Right;
            this._progressBar.Width = this._statusBar.Width / 3;
            this._toolbar = new ToolBar();
            this._toolbar.ImageList = this._imgListButtons;
            this._toolbar.Parent = this;
            this._toolbar.Height = 20;
            this._toolbar.Dock = DockStyle.Top;
            this._tbPanel = new ToolbarPanel(this._toolbar, this);
            this._individualsTabPage.AutoScroll = true;
            this.AutoScroll = true;
            this._leftPanel = new LeftPanel();
            this._leftPanel.Parent = this._allTabPage;
            this._leftPanel.Dock = DockStyle.Fill;
            this._leftPanel.BackColor = Color.LightGray;
            this._leftPanel.Resize += new EventHandler(this.OnResizeLeftPanel);
            this._leftRiverPanel = new LeftPanel();
            this._leftRiverPanel.Parent = this._riverTabPage;
            this._leftRiverPanel.Dock = DockStyle.Fill;
            this._leftRiverPanel.BackColor = Color.LightGray;
            this._leftRiverPanel.Resize += new EventHandler(this.OnResizeLeftRiverPanel);
            this._calcAttrListManager = new CalcAttrListManager(this._calcAttrList, this._dataSet);
            this._itemsListManager = new TimeSearcher.ItemsListManager(this._itemsList, this._dataSet, this._calcAttrListManager, this);
            this._cbIndividualItems = new ComboBox();
            this._cbIndividualItems.DropDownStyle = ComboBoxStyle.DropDownList;
            this._cbIndividualItems.Width = 250;
            this._cbIndividualItems.SelectedIndexChanged += new EventHandler(this.cbIndividualItems_SelectedIndexChanged);
            this._mainTabControl.SelectedTab = this._allTabPage;
            this._varView = new VariablesView(this, this._leftPanel);
            this._riverPlotView = new RiverPlotView(this, this._leftRiverPanel);
        }

        private void createOverviewVariablePanel()
        {

            OverviewPanel[] overviewPanels = new OverviewPanel[this._dataSet.NumDynVar];
            if (this.IsDataSetLoaded)
            {
                int height = this._bottomPanel.Height;
                for (int i = 0; i < this._dataSet.NumDynVar; i++)
                {
                    overviewPanels[i] = new OverviewPanel(this, i);
                }
                OverviewVariablePanel panel2 = new OverviewVariablePanel(this, 0, overviewPanels, this._dataSet, this._logMenuItem.Checked);
                int h = this._bottomPanel.Height;
                panel2.SetBounds(this._bottomPanel.Left, this._bottomPanel.Top, this._bottomPanel.Width, this._bottomPanel.Height);
                panel2.Size = new Size(this._bottomPanel.Width, 100);
                this._overviewVariablePanel = panel2;
                this._overviewVariablePanel.Dock = DockStyle.Bottom;
            }
        }

        private void dataSetComplete()
        {
            this.VarView.initVarIndexToVpIndex(this._dataSet.NumDynVar);
            this._numDisplayedVariables = int.Parse(ConfigurationManager.AppSettings["Number of items"]);
            this._varView.createQueryPanels();
            this._varView.layoutQueryPanels();
            this.createOverviewVariablePanel();
            this.createItemPanels(0);
            this.layoutItemPanels();
            this._riverPlotView.createRiverPanels();
            this._riverPlotView.layoutRiverPanels();
            this.RefillItemsList();
            this._isControlKeyDown = false;
            this.StrMode = "select";
            this._mainTabControl.SelectedIndex = 0;
            this._selectedIndividualsVariable = 0;
            this.fillIndividualItemsComboBox();
        }

        public static void Debug(string str)
        {
        }

        private void detailsListSelectedIndexChanged(object obj, EventArgs ea)
        {
            if (this._detailsList.SelectedIndices.Count != 0)
            {
                if (this._dataSet.currTimePtIndex >= 0)
                {
                    this._detailsList.Items[this._dataSet.currTimePtIndex].BackColor = Color.White;
                    this._detailsList.Items[this._dataSet.currTimePtIndex].ForeColor = Color.Black;
                }
                this._dataSet.currTimePtIndex = this._detailsList.SelectedIndices[0];
                this._detailsList.Items[this._dataSet.currTimePtIndex].BackColor = Color.Blue;
                this._detailsList.Items[this._dataSet.currTimePtIndex].ForeColor = Color.White;
                this._detailsList.EnsureVisible(this._dataSet.currTimePtIndex);
                this.invalidateVisiblePanels();
            }
        }

        private void dgvSelectAll_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (e.RowIndex != -1))
            {
                string dd = this.dgvSelectAll.Rows[e.RowIndex].Cells[0].EditedFormattedValue.ToString();
                string dd1 = this.dgvSelectAll.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                string user_id_str = this.dgvSelectAll.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                for (int i = 0; i < this.user_dt.Rows.Count; i++)
                {
                    if (this.user_dt.Rows[i]["user_id"].ToString() == user_id_str)
                    {
                        this.user_dt.Rows[i]["is_check"] = dd;
                    }
                }
                this.re_read = false;
                this.openFile();
                this.RefillDetailsList(user_id_str, true);
            }

        }


        private void dgvSelectAll_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if ((e.RowIndex == -1) && (e.ColumnIndex == 0))
            {
                this.ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
            }
            if ((e.RowIndex == -1) && (e.ColumnIndex == 1))
            {
                this.ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
            }
        }

        private void dgvSelectAll_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
        }



        public void DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = this.ComputeFibonacci(this.worker, e);
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        public void fill_user_dt()
        {
            this.user_dt.Rows.Clear();
            int[] enabledItemIdx = this._dataSet.GetEnabledItemIdx();
            this.user_dt.Rows.Clear();
            for (int i = 0; i < enabledItemIdx.Length; i++)
            {
                if (!this._dataSet.GetItem(enabledItemIdx[i]).Name.Equals("lost") && !this._dataSet.GetItem(enabledItemIdx[i]).Name.Equals("arate"))
                {
                    DataRow dr = this.user_dt.NewRow();
                    dr["is_check"] = true;
                    dr["is_computer"] = true;
                    dr["is_show"] = false;
                    dr["user_id"] = this._dataSet.GetItem(enabledItemIdx[i]).Name;
                    double[] user_d_s = this._dataSet.GetItem(enabledItemIdx[i]).get_DataVariable[2].get_values();
                    double ave = DBHelper.MathAdd.Ave(user_d_s);
                    double var_d = DBHelper.MathAdd.Var(user_d_s);
                    double zero_rate = DBHelper.MathAdd.zero_rate(user_d_s);
                    dr["均值"] = ave.ToString("F2");
                    if (ave == 0)
                    {
                        dr["标准差"] = "0";
                    }
                    else
                    {
                        dr["标准差"] = (Math.Pow(var_d, 0.5) / ave).ToString("F2");
                    }
                    dr["yaozhi"] = zero_rate.ToString("F2");
                    this.user_dt.Rows.Add(dr);
                }
            }

            this.dgvSelectAll.DataSource = this.user_dt;
            // userid.HeaderText = userid.HeaderText + "(" + user_dt.Rows.Count + ")";
            // userid.HeaderText = "用户(" + user_dt.Rows.Count + ")";
            DataRow[] drs = user_dt.Select("yaozhi>0.8");
            label8.Text = "用户数:" + user_dt.Rows.Count.ToString() + "(零值比例大于0.8的用户数：" + drs.Length + ")";


        }

        private void fillIndividualItemsComboBox()
        {
            this._cbIndividualItems.Items.Clear();
            string[] strArray = this._dataSet.getDynVarNames(this._logMenuItem.Checked);
            for (int i = 0; i < strArray.Length; i++)
            {
                this._cbIndividualItems.Items.Add(strArray[i]);
            }
            this._cbIndividualItems.SelectedIndex = 0;
        }

        public DateTime get_curr_begin_time()
        {
            return this.curr_begin_time;
        }

        public DateTime get_curr_end_time()
        {
            return this.curr_end_time;
        }

        public bool get_g_userid(string user_id)
        {
            bool b = false;
            DataRow[] drs = this.user_dt.Select("user_id='" + user_id + "'");
            string bd = "False";
            if (drs.Length > 0)
            {
                bd = drs[0]["is_check"].ToString();
            }
            if (bd == "True")
            {
                b = true;
            }
            return b;
        }

        public string get_line_lost()
        {
            return this.line_lost;
        }

        public DateTime get_max_date_time()
        {
            return this.max_date_time;
        }

        public DateTime get_min_date_time()
        {
            return this.min_date_time;
        }

        private void HeaderCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (((CheckBox)sender).Tag.ToString() == "is_check")
            {
                this.re_read = false;
                this.openFile();
                userid.HeaderText = userid.HeaderText + "(" + user_dt.Rows.Count + ")";
                userid.HeaderText = "用户(" + user_dt.Rows.Count + ")";
            }
        }

        private void HeaderCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            this.re_read = false;
            this.HeaderCheckBoxClick((CheckBox)sender);
            this.openFile();

        }

        private void HeaderCheckBoxClick(CheckBox HCheckBox)
        {
            foreach (DataGridViewRow Row in (IEnumerable)this.dgvSelectAll.Rows)
            {
                int i;
                if (HCheckBox.Tag.ToString() == "is_check")
                {
                    ((DataGridViewCheckBoxCell)Row.Cells["chkBxSelect"]).Value = HCheckBox.Checked;
                    i = 0;
                    while (i < this.user_dt.Rows.Count)
                    {
                        this.user_dt.Rows[i]["is_check"] = HCheckBox.Checked;
                        i++;
                    }
                }
                if (HCheckBox.Tag.ToString() == "is_computer")
                {
                    ((DataGridViewCheckBoxCell)Row.Cells["chkCoSelect"]).Value = HCheckBox.Checked;
                    for (i = 0; i < this.user_dt.Rows.Count; i++)
                    {
                        this.user_dt.Rows[i]["is_computer"] = HCheckBox.Checked;
                    }
                }
            }
            this.dgvSelectAll.DataSource = this.user_dt;
            userid.HeaderText = userid.HeaderText + "(" + user_dt.Rows.Count + ")";
            this.dgvSelectAll.RefreshEdit();
        }

        private void individualsPage_Resize(object sender, EventArgs e)
        {
            this.ResizeItemPanels();
        }

        private void initAfterDataSetLoaded(bool shdKeepSettings)
        {
            this._loadAtrMenuItem.Enabled = true;
            this._filterTfnMenuItem.Enabled = true;
            this._clearAllQueriesMenuItem.Enabled = true;
            this._searchOptsMenuItem.Enabled = true;
            this._selectMatchesMenuItem.Enabled = true;
            this._logMenuItem.Checked = false;
            if (!shdKeepSettings)
            {
                this._showSelectedItemsMenuItem.Checked = false;
            }
        }
        private void int_task()
        {
            if (this.load_task)
            {
                string sql = "select line_no,tl_id,begin_time,end_time,task_id from ts_task_line\r\nwhere task_id in(select max(task_id) from ts_task) and is_static=0 ";
                DataTable dt = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    this._tbPanel.add_item(dt, false);
                    this.task_id = int.Parse(dt.Rows[0]["task_id"].ToString());
                    string task_name = DBHelper.DBHelper.GetScalar("select task_name from ts_task where task_id='" + this.task_id + "'").ToString();
                    this._allTabPage.Text = this._allTabPage.Text + ":" + task_name;
                    this.set_curr_begin_time(DateTime.Parse(dt.Rows[0]["begin_time"].ToString()));
                    this.set_curr_end_time(DateTime.Parse(dt.Rows[0]["end_time"].ToString()));
                    this.re_read = true;
                    this.openFile();
                    this.fill_user_dt();
                    this.RefillDetailsList("", false);
                    this.week_var();
                }
                else
                {
                    this._allTabPage.Text = "图形展示";
                    MessageBox.Show("此任务所有线路已检查完毕");
                    this.reload();
                }
            }
        }

        public void invalidateVisiblePanels()
        {
            this._varView.InvalidateVisibleQPs(this._numDisplayedVariables);
            this._overviewVariablePanel.invalidateOverview();
        }

        public void InvalidateVisibleTabsDisplay()
        {
            switch (this.CurrView)
            {
                case TimeSearcher.View.Var:
                    this.invalidateVisiblePanels();
                    break;

                case TimeSearcher.View.River:
                    this.RiverView.Invalidate();
                    break;
            }
        }

        private void is_check_Click(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Tag.ToString() == "is_check")
            {
                this.re_read = false;
                this.openFile();
            }
        }

        private void layoutItemPanels()
        {
            if (this.IsDataSetLoaded)
            {
                this._individualsTabPage.Controls.Clear();
                this._individualsTabPage.Controls.Add(this._cbIndividualItems);
                foreach (ItemVariablePanel panel in this._itemPanels)
                {
                    this._individualsTabPage.Controls.Add(panel);
                }
            }
        }

        public void layoutOverviewPanel()
        {
            if (this.IsDataSetLoaded)
            {
                int height = this._bottomPanel.Height;
                this._overviewVariablePanel.SetBounds(this._bottomPanel.Left, this._bottomPanel.Top, this._bottomPanel.Width, this._bottomPanel.Height);
                this._bottomPanel.Controls.Clear();
                this._bottomPanel.Controls.Add(this._overviewVariablePanel.getCombo());
                this._bottomPanel.Controls.Add(this._overviewVariablePanel);
                this._bottomPanel.Invalidate();
            }
        }

        private void MouseWheelLoad(int days)
        {
            TimeSpan time_sapn = (TimeSpan)(this.curr_end_time.AddDays((double)days) - this.curr_begin_time.AddDays((double)days));
            // double tt = time_sapn.Days;
            time_sapn = (TimeSpan)(this.max_date_time - this.curr_begin_time.AddDays((double)days));
            if (time_sapn.Days < 3.0)
            {
                this.set_curr_begin_time(DateTime.Now);
                this.set_curr_end_time(DateTime.Now);
            }
            else
            {
                time_sapn = (TimeSpan)(this.curr_end_time.AddDays((double)days) - this.min_date_time);
                if (time_sapn.Days < 3.0)
                {
                    this.set_curr_begin_time(DateTime.Now);
                    this.set_curr_end_time(DateTime.Now);
                }
                else
                {
                    this.set_curr_begin_time(this.curr_begin_time.AddDays((double)days));
                    this.set_curr_end_time(this.curr_end_time.AddDays((double)days));
                }
            }
            this.re_read = true;
            this.openFile();
            this.fill_user_dt();
            this.RefillDetailsList("", false);
            this.week_var();

        }

        private void OnKeyDown(object obj, KeyEventArgs kea)
        {
            if (kea.KeyCode == System.Windows.Forms.Keys.ControlKey)
            {
                this._isControlKeyDown = true;
            }
            if (((kea.KeyCode == System.Windows.Forms.Keys.Delete) || (kea.KeyCode == System.Windows.Forms.Keys.Cancel)) && !this.bUpdating)
            {
                this._dataSet.TimeBoxManager.removeSelectedTimeBoxes();
                this.invalidateVisiblePanels();
                if (this._dataSet.TimeBoxManager.noTimeBoxes())
                {
                    this.Cursor = Cursors.Default;
                    this.RefillItemsList();
                }
            }
            SearchBox box = this._dataSet.TimeBoxManager.getFirstSelectedSearchBox();
            if (box != null)
            {
                if ((kea.KeyCode == System.Windows.Forms.Keys.Subtract) || (kea.KeyCode == System.Windows.Forms.Keys.M))
                {
                    box.onKeyDownToChangeTolerance(-1);
                }
                else if ((kea.KeyCode == System.Windows.Forms.Keys.Add) || (kea.KeyCode == System.Windows.Forms.Keys.P))
                {
                    box.onKeyDownToChangeTolerance(1);
                }
                else if ((kea.KeyCode == System.Windows.Forms.Keys.Multiply) || (kea.KeyCode == System.Windows.Forms.Keys.S))
                {
                    //box.PerformSearch();
                    time_al = new ArrayList();
                    ArrayList s_s_box = this._dataSet.TimeBoxManager.return_selectBox();
                    string s_time = System.DateTime.Now.ToString("yyyy-MM-dd");
                    string e_time = System.DateTime.Now.ToString("yyyy-MM-dd");
                    for (int i = 0; i < s_s_box.Count; i++)
                    {

                        SearchBox s_box = (SearchBox)s_s_box[i];
                        string searchtime = s_box.searchTime();
                        if (i == 0)
                        {
                            s_time = searchtime.Split(';')[0].ToString();
                        }
                        if (i == s_s_box.Count - 1)
                        {
                            e_time = searchtime.Split(';')[1].ToString();
                        }
                        time_al.Add(s_box.searchTime());
                    }
                    // this.static_array(time_al);
                    // get_rule(time_al);
                    TimeSpan it = DateTime.Parse(e_time) - DateTime.Parse(s_time);
                    if (MessageBox.Show("是否分析辅助数据?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        is_rule = true;
                    }
                    else
                    {
                        is_rule = false;
                    }

                    DBHelper.DBiniClass iniclass = new DBHelper.DBiniClass(".\\system.ini");

                    string rule_days = iniclass.IniReadValue("rule_data_days", "rule_data_days");
                    if (it.Days >= int.Parse(rule_days))
                    {
                        MessageBox.Show("由于可能存在大量辅助数据,请不要选择超过" + rule_days + "天时间进行分析");
                        //if (MessageBox.Show("由于可能存在大量辅助数据,请不要选择超过" + rule_days + "天时间进行分析，是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //{
                        //    if (!worker.IsBusy)
                        //    {
                        //        is_rule = false;
                        //        this.task_str = "分析数据";
                        //        this.worker.RunWorkerAsync();
                        //        this.cmd.ShowOpaqueLayer(this, 0x7d, true);
                        //    }
                        //}
                    }
                    else
                    {
                        if (!worker.IsBusy)
                        {
                            this.task_str = "分析数据";
                            this.worker.RunWorkerAsync();
                            this.cmd.ShowOpaqueLayer(this, 0x7d, true);
                        }
                    }
                }


                else if (kea.KeyCode == System.Windows.Forms.Keys.Q)
                {
                    box.onKeyDownToChangeToleranceAndPerformSearch(1);
                }
                else if (kea.KeyCode == System.Windows.Forms.Keys.W)
                {
                    box.onKeyDownToChangeToleranceAndPerformSearch(-1);
                }
                else if (kea.KeyCode == System.Windows.Forms.Keys.Z)
                {
                    this.set_curr_begin_time(DateTime.Parse(this.search_b_time));
                    this.set_curr_end_time(DateTime.Parse(this.search_e_time));
                    this.re_read = true;
                    this.openFile();
                    this.fill_user_dt();
                    this.RefillDetailsList("", false);
                    this.week_var();
                    // this.show_av_pic();
                }
            }
            FilterBox f_box = this._dataSet.TimeBoxManager.getFirstSelectedFilterBox();
            if (f_box != null)
            {
                if (kea.KeyCode == System.Windows.Forms.Keys.Z)
                {
                    ArrayList time_al = new ArrayList();
                    ArrayList s_s_box = this._dataSet.TimeBoxManager.return_selectBox();
                    for (int i = 0; i < s_s_box.Count; i++)
                    {
                        FilterBox s_box = (FilterBox)s_s_box[i];
                        time_al.Add(s_box.searchTime());
                    }
                    delete_select_box(time_al);


                }
            }
        }

        private void OnKeyUp(object obj, KeyEventArgs kea)
        {
            if (kea.KeyCode == System.Windows.Forms.Keys.ControlKey)
            {
                this._isControlKeyDown = false;
            }
        }

        private void OnMouseWheel(object obj, MouseEventArgs mou)
        {
            if (Control.ModifierKeys == System.Windows.Forms.Keys.Control)
            {
                if (mou.Delta > 0)
                {
                    // this.MouseWheelLoad(7);
                }
                if (mou.Delta < 0)
                {
                    //  this.MouseWheelLoad(-7);
                }
            }
        }

        private void OnResizeBottomPanel(object obj, EventArgs ea)
        {
            if (this.IsDataSetLoaded)
            {
                this._overviewVariablePanel.Size = this._bottomPanel.Size;
            }
        }

        private void OnResizeLeftPanel(object obj, EventArgs ea)
        {
            this._varView.layoutQueryPanels();
        }

        private void OnResizeLeftRiverPanel(object obj, EventArgs ea)
        {
            this._riverPlotView.layoutRiverPanels();
        }

        public void openFile()
        {
            if (this._detailsList.SelectedIndices.Count > 0)
            {
                this._detailsList.Items[this._detailsList.SelectedIndices[0]].Selected = false;
            }
            this.bUpdating = true;
            try
            {
                this._dataSet.ReadFile();
                this.Text = this._optionsForm.TsTitle + "    " + this._dataSet.Title;
                this._isDataSetLoaded = true;
                this.dataSetComplete();
                this.initAfterDataSetLoaded(false);
                this._individualsTabPage.Text = this._dataSet.ItemTabName;
                this.bUpdating = false;
                this._itemsList.Items[0].Selected = true;
                double[] d_lost = this._dataSet.GetItem(0).GetVar(0).get_values();
                double[] d_rate = this._dataSet.GetItem(1).GetVar(1).get_values();
                if (DBHelper.MathAdd.Ave(d_lost) == 0)
                {
                    this.label7.Text = "线损统计信息\r\n线损量均值:" + DBHelper.MathAdd.Ave(d_lost).ToString("F2") + " 变异系数:0 \r\n线损率均值:" + DBHelper.MathAdd.Ave(d_rate).ToString("F2") + " 变异系数:0 ";
                }
                else
                {
                    this.label7.Text = "线损统计信息\r\n线损量均值:" + DBHelper.MathAdd.Ave(d_lost).ToString("F2") + " 变异系数:" + (Math.Pow(DBHelper.MathAdd.Var(d_lost), 0.5) / DBHelper.MathAdd.Ave(d_lost)).ToString("F2") + "\r\n线损率均值:" + DBHelper.MathAdd.Ave(d_rate).ToString("F2") + " 变异系数:" + (Math.Pow(DBHelper.MathAdd.Var(d_rate), 0.5) / DBHelper.MathAdd.Ave(d_rate)).ToString("F2") + " ";

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                MessageBox.Show("系统出错");
            }
        }

        private void openFile(string filename, bool shdKeepSettings)
        {
            if (this._detailsList.SelectedIndices.Count > 0)
            {
                this._detailsList.Items[this._detailsList.SelectedIndices[0]].Selected = false;
            }
            this.bUpdating = true;
            try
            {
                Stream fileStream = new FileStream(filename, FileMode.Open);
                if (fileStream != null)
                {
                    this._dataSet.ReadFile(fileStream);
                    this._dataSet.FilenamePath = filename;
                    this._itemsListManager.Reset(shdKeepSettings);
                    this._calcAttrListManager.Reset();
                    this._calcAttrListManager.Refill();
                    this._optionsForm._lastFilename = filename;
                }
                fileStream.Close();
                this.Text = this._optionsForm.TsTitle + "    " + this._dataSet.Title;
                this._isDataSetLoaded = true;
                this.dataSetComplete();
                this.initAfterDataSetLoaded(shdKeepSettings);
                this._individualsTabPage.Text = this._dataSet.ItemTabName;
                this.bUpdating = false;
                this._itemsList.Items[0].Selected = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                MessageBox.Show("Message: " + exception.Message + "\n(see console output for details)", "Error opening file.");
            }
        }

        private void out_put_Click(object sender, EventArgs e)
        {
            //new DBHelper.A_ExcelHelper().write_excel(this.source_double, this.user_dt, this.user_c, this.date_str_excel, this.source_yaozhi, fu_dt);
            if (!worker.IsBusy)
            {
                this.task_str = "导出数据";
                this.worker.RunWorkerAsync();
                this.cmd.ShowOpaqueLayer(this, 0x7d, true);
            }


        }

        public void ProgessChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void r_FormClosed(object sender, EventArgs e)
        {
            this.reload();
        }

        public void RefillDetailsList(string user_id_str, bool forceRefill)
        {
            int num = 0;
            if (this._detailsList.SelectedIndices.Count > 0)
            {
                num = this._detailsList.SelectedIndices[0];
            }
            this._dataSet.fillDetailsLitByName(this._detailsList, user_id_str, forceRefill);
            if (this._dataSet.currTimePtIndex != -1)
            {
                this._detailsList.EnsureVisible(this._dataSet.currTimePtIndex);
            }
            this._detailsList.Items[num].Selected = true;
        }

        public void RefillItemsList()
        {
            this._itemsListManager.fillItemsList();
        }

        public void refreshGraphsAndItemsList()
        {
            this.invalidateVisiblePanels();
            this.RefillItemsList();
        }

        private void reload()
        {
            this.set_curr_begin_time(DateTime.Now);
            this.set_curr_end_time(DateTime.Now);
            DataTable dt = DBHelper.DBHelper.GetDataSet("select distinct line_no from ts_line").Tables[0];
            this._tbPanel.add_item(dt, false);
            if (dt.Rows.Count > 0)
            {
                this.re_read = true;
                this.openFile();
                this.fill_user_dt();
                this.RefillDetailsList("", false);
                this.week_var();

            }
            else
            {
                //  Application.Restart();
            }
        }

        private void ResetHeaderCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            Rectangle oRectangle;
            Point oPoint;
            if (ColumnIndex == 0)
            {
                oRectangle = this.dgvSelectAll.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);
                oPoint = new Point();
                oPoint.X = (oRectangle.Location.X + ((oRectangle.Width - this.is_check.Width) / 2)) + 1;
                oPoint.Y = (oRectangle.Location.Y + ((oRectangle.Height - this.is_check.Height) / 2)) + 1;
                this.is_check.Location = oPoint;
            }
            if (ColumnIndex == 1)
            {
                oRectangle = this.dgvSelectAll.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);
                oPoint = new Point();
                oPoint.X = (oRectangle.Location.X + ((oRectangle.Width - this.is_computer.Width) / 2)) + 1;
                oPoint.Y = (oRectangle.Location.Y + ((oRectangle.Height - this.is_computer.Height) / 2)) + 1;
                this.is_computer.Location = oPoint;
            }
        }

        private void ResizeItemPanels()
        {
            if (this.IsDataSetLoaded)
            {
                int height = 100;
                foreach (ItemVariablePanel panel in this._itemPanels)
                {
                    panel.Size = new Size(this._individualsTabPage.Width - 20, height);
                }
            }
        }

        private void saveFile(string filename)
        {
            this.bUpdating = true;
            try
            {
                Stream stream = new FileStream(filename, FileMode.Create);
                if (stream != null)
                {
                    this._dataSet.WriteSelectedItemsAsTqd(stream);
                }
                stream.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                MessageBox.Show("Error saving file! (see console output for details)");
            }
        }

        public void SelectTimePt(int timePtIdx)
        {
            this._detailsList.Focus();
            this._detailsList.Items[timePtIdx].Selected = true;
        }

        public void set_curr_begin_time(DateTime _curr_begin_time)
        {
            this.curr_begin_time = _curr_begin_time;
        }

        public void set_curr_end_time(DateTime _curr_end_time)
        {
            this.curr_end_time = _curr_end_time;
        }

        public void set_line_lost(string _line_lost)
        {
            this.line_lost = _line_lost;
        }

        public void set_lstv_m1(Dictionary<string, string> MyDictionary, double[,] yaozhi, DataRowCollection drc, string datestr, DataTable source_dt, double[,] source_d, string[] user_dr, DataTable dt_warn_user, Dictionary<string, double> time_points)
        {
            int index;
            string[] values_str;
            int i;
            string yaozhistr;
            this.dtuserrate = new DataTable();
            this.source_yaozhi = yaozhi;
            this.date_str_excel = datestr;
            this.user_c = user_dr;
            this.dtuserrate.Columns.Add(new DataColumn("user_id"));
            DataRow userdr = this.dtuserrate.NewRow();
            userdr[0] = "线损";
            this.dtuserrate.Rows.Add(userdr);
            CheckForIllegalCrossThreadCalls = false;
            this._userpage.Text = "用户计算:" + datestr;
            this.source_double = source_d;
            this.user_dt = (DataTable)this.dgvSelectAll.DataSource;
            this.dtyaozhi = new DataTable();
            DataColumn c0 = new DataColumn("user");
            DataColumn c1 = new DataColumn("user_id");
            DataColumn c2 = new DataColumn("date_time");
            DataColumn c3 = new DataColumn("yaozhi_rate");
            this.dtyaozhi.Columns.Add(c0);
            this.dtyaozhi.Columns.Add(c1);
            this.dtyaozhi.Columns.Add(c2);
            this.dtyaozhi.Columns.Add(c3);
            int l_index = 0;
            foreach (KeyValuePair<string, double> str_dt in time_points)
            {
                DataRow userdr1 = this.dtyaozhi.NewRow();
                userdr1["user"] = "lost";
                userdr1["user_id"] = "lost";
                userdr1["date_time"] = str_dt.Key.Split(new char[] { '_' })[0];
                userdr1["yaozhi_rate"] = yaozhi[0, l_index];
                l_index++;
                this.dtyaozhi.Rows.Add(userdr1);
            }
            int indes_user = 1;
            foreach (KeyValuePair<string, string> str in MyDictionary)
            {
                index = 0;
                foreach (KeyValuePair<string, double> str_dt in time_points)
                {
                    DataRow dr_yaozhi_rate = this.dtyaozhi.NewRow();
                    values_str = str.Value.ToString().Split(new char[] { ';' });
                    dr_yaozhi_rate["user"] = str.Key;
                    dr_yaozhi_rate["user_id"] = str.Key + "(" + values_str[1].ToString() + ")";
                    dr_yaozhi_rate["date_time"] = str_dt.Key.Split(new char[] { '_' })[0];
                    dr_yaozhi_rate["yaozhi_rate"] = yaozhi[indes_user, index];
                    index++;
                    this.dtyaozhi.Rows.Add(dr_yaozhi_rate);
                }
                indes_user++;
            }
            for (i = 0; i < this.user_dt.Rows.Count; i++)
            {
                yaozhistr = string.Empty;
                if (MyDictionary.ContainsKey(this.user_dt.Rows[i]["user_id"].ToString()))
                {
                    values_str = MyDictionary[this.user_dt.Rows[i]["user_id"].ToString()].ToString().Split(new char[] { ';' });
                    this.user_dt.Rows[i]["m1"] = double.Parse(values_str[1]);
                    this.user_dt.Rows[i]["sx_bili"] = double.Parse(values_str[1]);
                    this.user_dt.Rows[i]["yaozhi"] = double.Parse(values_str[2]);
                }
                else
                {
                    this.user_dt.Rows[i]["m1"] = 0;
                    this.user_dt.Rows[i]["sx_bili"] = 0;
                    this.user_dt.Rows[i]["yaozhi"] = 0;
                }
            }
            for (i = -1; i < this.user_dt.Rows.Count; i++)
            {
                yaozhistr = string.Empty;
                if (i > -1)
                {
                    if (MyDictionary.ContainsKey(this.user_dt.Rows[i]["user_id"].ToString()))
                    {
                        values_str = MyDictionary[this.user_dt.Rows[i]["user_id"].ToString()].ToString().Split(new char[] { ';' });
                        this.user_dt.Rows[i]["m1"] = values_str[0];
                        this.user_dt.Rows[i]["sx_bili"] = double.Parse(values_str[1]);
                        this.user_dt.Rows[i]["yaozhi"] = double.Parse(values_str[2]);
                        yaozhistr = values_str[1].ToString();
                    }
                    else
                    {
                        this.user_dt.Rows[i]["m1"] = 0;
                        this.user_dt.Rows[i]["sx_bili"] = 0;
                        this.user_dt.Rows[i]["yaozhi"] = 0;
                    }
                }
                for (int j = 0; j < drc.Count; j++)
                {
                    int rr = this.user_dt.Select("is_computer='True'").Length - 1;
                    if ((i != -1) && (this.user_dt.Rows[i][1].ToString() == "True"))
                    {
                        if (MyDictionary.ContainsKey(this.user_dt.Rows[i]["user_id"].ToString()))
                        {
                            values_str = MyDictionary[this.user_dt.Rows[i]["user_id"].ToString()].ToString().Split(new char[] { ';' });
                            this.user_dt.Rows[i]["m1"] = values_str[0];
                            this.user_dt.Rows[i]["sx_bili"] = double.Parse(values_str[1]);
                            this.user_dt.Rows[i]["yaozhi"] = double.Parse(values_str[2]);
                            yaozhistr = values_str[1];
                        }
                        DataRow dr = this.dtyaozhi.NewRow();
                        dr["user_id"] = this.user_dt.Rows[i]["user_id"].ToString() + "(" + yaozhistr + ")";
                        dr["user"] = this.user_dt.Rows[i]["user_id"].ToString();
                        dr["date_time"] = drc[j][0].ToString();
                        index = 0;
                        foreach (KeyValuePair<string, double> str in time_points)
                        {
                            if (str.Key.Split(new char[] { '_' })[0] == drc[j][0].ToString())
                            {
                                dr["yaozhi_rate"] = yaozhi[i + 1, index].ToString();
                            }
                            index++;
                        }
                    }
                }
            }

            if (dt_warn_user.Rows.Count > 0)
            {
                this.dgvUserWarn.DataSource = dt_warn_user;
                this._userwarnpage.Text = "零值异常用户:" + dt_warn_user.Rows[0]["time_span"].ToString();
            }
            this.set_show_picture(source_dt, datestr);
        }

        public void set_max_date_time(DateTime _max_date_time)
        {
            this.max_date_time = _max_date_time;
        }

        public void set_min_date_time(DateTime _min_date_time)
        {
            this.min_date_time = _min_date_time;
        }

        public void set_show_picture(DataTable source_data, string date_str)
        {
            int i;
            int j;
            Dictionary<string, double> myd = new Dictionary<string, double>();
            for (i = 0; i < this.user_dt.Rows.Count; i++)
            {
                string user_id = this.user_dt.Rows[i]["user_id"].ToString();
                DataRow[] drs = source_data.Select("user_id='" + this.user_dt.Rows[i]["user_id"].ToString() + "'");
                double[] d = new double[drs.Length];
                j = 0;
                while (j < drs.Length)
                {
                    d[j] = double.Parse(drs[j][2].ToString());
                    j++;
                }
                if (d.Length > 0)
                {
                    this.user_dt.Rows[i]["均值"] = DBHelper.MathAdd.Ave(d).ToString("F2");
                    this.user_dt.Rows[i]["标准差"] = Math.Pow(DBHelper.MathAdd.Var(d), 0.5).ToString("F2");
                }
            }
            DataRow[] drs_lost = source_data.Select("user_id='lost'");
            double[] d_lost = new double[drs_lost.Length];
            double[] d_lost_rate = new double[drs_lost.Length];
            for (j = 0; j < drs_lost.Length; j++)
            {
                d_lost[j] = double.Parse(drs_lost[j][2].ToString());
                d_lost_rate[j] = double.Parse(drs_lost[j][3].ToString());
            }
            if (d_lost.Length > 0)
            {
                if (DBHelper.MathAdd.Ave(d_lost) == 0)
                {
                    this.label7.Text = "线损统计信息\r\n线损量均值:" + DBHelper.MathAdd.Ave(d_lost).ToString("F2") + " 变异系数:0 \r\n线损率均值:" + DBHelper.MathAdd.Ave(d_lost_rate).ToString("F2") + " 变异系数:0 )";
                }
                else
                {
                    this.label7.Text = "线损统计信息\r\n线损量均值:" + DBHelper.MathAdd.Ave(d_lost).ToString("F2") + " 变异系数:" + (Math.Pow(DBHelper.MathAdd.Var(d_lost), 0.5) / DBHelper.MathAdd.Ave(d_lost)).ToString("F2") + "\r\n线损率均值:" + DBHelper.MathAdd.Ave(d_lost_rate).ToString("F2") + " 变异系数:" + (Math.Pow(DBHelper.MathAdd.Var(d_lost_rate), 0.5) / DBHelper.MathAdd.Ave(d_lost_rate)).ToString("F2") + ")";

                }
                //  this.g_s.Text = date_str + "\r\n线损量均值:" + DBHelper.MathAdd.Ave(d_lost).ToString("F2") + " 标准差:" + Math.Pow(DBHelper.MathAdd.Var(d_lost), 0.5).ToString("F2") + "\r\n线损率均值:" + DBHelper.MathAdd.Ave(d_lost_rate).ToString("F2") + " 标准差:" + Math.Pow(DBHelper.MathAdd.Var(d_lost_rate), 0.5).ToString("F2");
            }
            ArrayList user_dv_select = new ArrayList();
            ArrayList user_dt_select = new ArrayList();

            DataView dv = this.user_dt.DefaultView;
            dv.Sort = "m1 asc";
            ArrayList user_dv = new ArrayList();
            ArrayList user_dv1 = new ArrayList();
            int r = 0;
            DataTable dt = new DataTable();
            //if (this.m1_c.Checked)
            //{
            dt = dv.ToTable();
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (!dt.Rows[i]["m1"].ToString().Equals("NaN"))
                {
                    if (r >= 5)
                    {
                        break;
                    }
                    if (!user_dv_select.Contains(dt.Rows[i]["user_id"].ToString()))
                    {
                        user_dv_select.Add(dt.Rows[i]["user_id"].ToString());
                        r++;
                    }
                }
            }
            // }
            for (i = 0; i < this.user_dt.Rows.Count; i++)
            {
                if (user_dv_select.Contains(this.user_dt.Rows[i]["user_id"].ToString()))
                {
                    //this.user_dt.Rows[i]["is_show"] = true;
                }
                else
                {
                    //  this.user_dt.Rows[i]["is_show"] = false;
                }
            }
            this.user_dt.AcceptChanges();
        }
        public void set_table_panel(Control[] Controls, string b_e_date_time)
        {

            tableLayoutPanel2.Controls.Clear();
            result_l.Text = "计算结果的起始时间为：" + b_e_date_time;
            tableLayoutPanel2.ColumnCount = 5;
            string rowscount = (Controls.Length / 5).ToString().Split('.')[0];
            tableLayoutPanel2.RowCount = int.Parse(rowscount) + 1;
            for (int j = 0; j < tableLayoutPanel2.RowCount; j++)
            {
                this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));

            }
            for (int i = 0; i < Controls.Length; i++)
            {
                CheckBox ck1 = (CheckBox)Controls[i];

                ck1.TabIndex = i;

                ck1.Size = new System.Drawing.Size(140, 17);
                ck1.UseVisualStyleBackColor = true;
                if (i <= 4)
                {
                    tableLayoutPanel2.Controls.Add(ck1, i, 0);
                }
                else
                {
                    string ColumnCount = (i % 5).ToString();
                    rowscount = (i / 5).ToString().Split('.')[0];
                    tableLayoutPanel2.Controls.Add(ck1, int.Parse(ColumnCount), int.Parse(rowscount));
                }
            }

        }

        public void setBottomPanelCursor(System.Windows.Forms.Cursor cursor)
        {
            this._bottomPanel.Cursor = cursor;
        }

        private void setCurrentDirToBaseDir()
        {
            Directory.SetCurrentDirectory(SharedData.BaseDir);
        }

        public void setNumDisplayedVariables(int numDisplayed)
        {
            this._numDisplayedVariables = numDisplayed;
            this._varView.layoutQueryPanels();
            this._riverPlotView.layoutRiverPanels();
        }

        public void showStatus(DataAxisH hAxis, DataAxisV vAxis, Point currMousePosition)
        {
            string str = (string)hAxis.getValueFromCoordinate(currMousePosition.X);
            double num = (double)vAxis.getValueFromCoordinate(currMousePosition.Y);
            string str_date = "";
            if (str != "")
            {
                switch (DateTime.Parse(str).DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        str_date = "星期日";
                        break;

                    case DayOfWeek.Monday:
                        str_date = "星期一";
                        break;

                    case DayOfWeek.Tuesday:
                        str_date = "星期二";
                        break;

                    case DayOfWeek.Wednesday:
                        str_date = "星期三";
                        break;

                    case DayOfWeek.Thursday:
                        str_date = "星期四";
                        break;

                    case DayOfWeek.Friday:
                        str_date = "星期五";
                        break;

                    case DayOfWeek.Saturday:
                        str_date = "星期六";
                        break;
                }
            }
            this._statusBar.Text = string.Concat(new object[] { "X:", str + " " + str_date, " Y:", Math.Round(num, 4), " - Mouse: (", currMousePosition.X, ",", currMousePosition.Y, ")" });
        }

        private void static_lost()
        {
            int r;
            DataItem[] di = this._dataSet.DataItems;
            string[] time_strs = this._dataSet.get_timePointNames();
            DateTime first_time = DateTime.Parse(time_strs[0]);
            int i_step = 5;
            DataTable dt = new DataTable();
            DataColumn cl = new DataColumn("user_id");
            dt.Columns.Add(cl);
            DataColumn cl1 = new DataColumn("begin_time");
            dt.Columns.Add(cl1);
            DataColumn cl_b = new DataColumn("begin_value");
            dt.Columns.Add(cl_b);
            int i = 1;
            while (i <= i_step)
            {
                DataColumn cl2 = new DataColumn("lag" + i + "day-rate");
                dt.Columns.Add(cl2);
                i++;
            }
            DataColumn clsum = new DataColumn("sum");
            clsum.DataType = System.Type.GetType("System.Double");
            dt.Columns.Add(clsum);
            for (r = 0; r < di.Length; r++)
            {
                i = 0;
                while (i < time_strs.Length)
                {
                    DataRow dr = dt.NewRow();
                    if (di[r].Name == "lost")
                    {
                        dr["user_id"] = "线损";
                    }
                    else if (di[r].Name == "arate")
                    {
                        dr["user_id"] = "线损率";
                    }
                    else
                    {
                        dr["user_id"] = di[r].Name;
                    }
                    dr["begin_time"] = time_strs[i];
                    dr["begin_value"] = di[r].get_DataVariable[2].get_values()[i];
                    double d_sum = 0.0;
                    int avg_count = 0;
                    if ((time_strs.Length - i) > i_step)
                    {
                        for (int l = 1; l <= i_step; l++)
                        {
                            if ((i + l) < time_strs.Length)
                            {
                                dr["lag" + l + "day-rate"] = di[r].get_DataVariable[2].get_values()[i + l] - di[r].get_DataVariable[2].get_values()[i];
                                double dd = di[r].get_DataVariable[2].get_values()[i + l] - di[r].get_DataVariable[2].get_values()[i];
                                d_sum = (d_sum + di[r].get_DataVariable[2].get_values()[i + l]) - di[r].get_DataVariable[2].get_values()[i];
                                avg_count++;
                            }
                        }
                        dr["sum"] = Math.Abs(d_sum);
                        dt.Rows.Add(dr);
                    }
                    i++;
                }
            }
            this._sx_data = dt;
            this.chanyi_dgv.DataSource = dt;
            DataView dv = new DataView(dt);
            dv.RowFilter = "user_id='线损率'";
            DataView dv1 = dv.ToTable().DefaultView;
            dv1.Sort = "sum desc";
            DataTable dt_sort = dv1.ToTable();
            ArrayList warn_zone = new ArrayList();
            if (dt_sort.Rows.Count > 0)
            {
                string start_time = dt_sort.Rows[0]["begin_time"].ToString();
                string start_time1 = dt_sort.Rows[1]["begin_time"].ToString();
                for (r = 0; r < time_strs.Length; r++)
                {
                    if (time_strs[r] == start_time)
                    {
                        warn_zone.Add(r + ";" + (r + i_step));
                        QueryPanel[] qs = this._varView.QPs;
                        for (i = 0; i < qs.Length; i++)
                        {
                            warn_zone.Add(r + ";" + (r + i_step));
                            qs[i].set_sx_index(r, r + i_step);
                        }
                        break;
                    }
                }
                //for (r = 0; r < time_strs.Length; r++)
                //{
                //    if (time_strs[r] == start_time1)
                //    {
                //        warn_zone.Add(r + ";" + (r + i_step));
                //        break;
                //    }
                //}
                //QueryPanel[] qs = this._varView.QPs;
                //for (i = 0; i < qs.Length; i++)
                //{
                //    qs[i].set_warn_zone(warn_zone);
                //}

            }
        }

        private void strar_form()
        {
            this._dataSet = new TimeSearcher.DataSet(this);
            this._isDataSetLoaded = false;
            this.createLayout();
            base.KeyPreview = true;
            base.KeyDown += new KeyEventHandler(this.OnKeyDown);
            base.KeyUp += new KeyEventHandler(this.OnKeyUp);
            this._optionsForm = new OptionsForm();
            base.MouseWheel += new MouseEventHandler(this.OnMouseWheel);
            this._numInitDisplayedVariables = this._optionsForm.NOV;
            this.curr_end_time = DateTime.Today;
            this.curr_begin_time = this.curr_end_time;
            base.WindowState = FormWindowState.Maximized;

            DataTable dt = DBHelper.DBHelper.GetDataSet("select distinct line_no from ts_line").Tables[0];

            if (dt.Rows.Count > 0)
            {

                this.openFile();
                this.fill_user_dt();

            }
            else
            {
                MessageBox.Show("无分析数据，请导入数据.");
                // Application.Restart();
            }

            dgvSelectAll.Columns[4].Frozen = true;
            //  week_var();
            base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.Selectable, true);
            this.Text = this._optionsForm.TsTitle;
            SharedData.BaseDir = Directory.GetCurrentDirectory();
        }

        private void task_form_FormClosed(object sender, EventArgs e)
        {
            this.int_task();
        }

        private void TimeSearcherForm_Load(object sender, EventArgs e)
        {
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        public void UpdateRiverPlotView()
        {
            this._riverPlotView.PrepareToDisplay(this.DataSet);
        }

        public void UpdateVisibleTabsDisplay()
        {
            switch (this.CurrView)
            {
                case TimeSearcher.View.Var:
                    this.refreshGraphsAndItemsList();
                    break;

                case TimeSearcher.View.River:
                    this.UpdateRiverPlotView();
                    break;
            }
        }

        private void w_FormClosed()
        {
            DataTable dt = DBHelper.DBHelper.GetDataSet("select distinct line_no from ts_line").Tables[0];
            this._tbPanel.add_item(dt, true);
            this.reload();
        }

        public void week_var()
        {
            try
            {
                //   show_av_pic();
                DataRow dr;
                double[] first;
                int i;
                double[] dd;
                DataItem[] di = this._dataSet.DataItems;
                string[] time_strs = this._dataSet.get_timePointNames();
                int i_week = (int)DateTime.Parse(time_strs[0]).DayOfWeek;
                int i_var_week = 7 - i_week;
                DataTable dt = new DataTable();
                DataColumn cl1 = new DataColumn("user_id");
                DataColumn cl2 = new DataColumn("weeks");
                DataColumn cl3 = new DataColumn("sx");
                dt.Columns.Add(cl1);
                dt.Columns.Add(cl2);
                dt.Columns.Add(cl3);
                int weeks = 1;
                int r = 0;
                while (r < di.Length)
                {
                    if (r != 1)
                    {
                        dr = dt.NewRow();
                        dr["user_id"] = di[r].Name;
                        dr["weeks"] = "第" + weeks.ToString() + "周";
                        first = new double[i_var_week];
                        i = 0;
                        while (i < i_var_week)
                        {
                            if (di[r].get_DataVariable[2].get_values().Length > i)
                            {
                                dd = di[r].get_DataVariable[2].get_values();
                                first[i] = di[r].get_DataVariable[2].get_values()[i];
                                i++;
                            }
                            else
                            {
                                i++;
                            }
                        }
                        double d = Math.Pow(DBHelper.MathAdd.Var(first), 0.5) / DBHelper.MathAdd.Ave(first);
                        dr["sx"] = d.ToString("F2");
                        dt.Rows.Add(dr);
                    }
                    r++;
                }
                weeks++;
                while (i_var_week < time_strs.Length)
                {
                    for (r = 0; r < di.Length; r++)
                    {
                        if (r != 1)
                        {
                            dr = dt.NewRow();
                            dr["user_id"] = di[r].Name;
                            dr["weeks"] = "第" + weeks.ToString() + "周";
                            first = new double[7];
                            int d_l = 0;
                            for (i = i_var_week; i < (i_var_week + 6); i++)
                            {
                                if (i < time_strs.Length)
                                {
                                    dd = di[r].get_DataVariable[2].get_values();
                                    first[d_l] = di[r].get_DataVariable[2].get_values()[i];
                                    d_l++;
                                }
                            }
                            dr["sx"] = (Math.Pow(DBHelper.MathAdd.Var(first), 0.5) / DBHelper.MathAdd.Ave(first)).ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                    weeks++;
                    i_var_week += 6;
                }
                this._sx_datateble = dt;
                List<string> oliw = new List<string>();
                DataTable new_dt = DBHelper.DataTableHelp.RowToColumn(dt, "weeks", "sx");
                this._sxdgv.DataSource = new_dt;
                this._sx_table = new_dt;
                this.static_lost();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private bool static_array(ArrayList time_al)
        {
            bool b = false;
            try
            {
                //获取数据
                #region
                string time_sql = string.Empty;
                string datestr = string.Empty;
                string sql_userid = "  select distinct t.user_id from static_lost t  where t.line_no='" + this.get_line_lost() + "'  ";
                string sql_static_lost = " select * from static_lost t where   t.line_no='" + this.get_line_lost() + "' ";
                string sql_timepoints = " select distinct t.date_time from static_lost t  where  t.line_no='" + this.get_line_lost() + "'  ";
                if (time_al.Count > 1)
                {
                    time_sql = " and ( ";
                }
                else
                {
                    time_sql = " and ";
                }
                for (int r = 0; r < time_al.Count; r++)
                {

                    if (r == 0)
                    {
                        time_sql = time_sql + " (t.date_time>='" + time_al[r].ToString().Split(';')[0] + "' and t.date_time<='" + time_al[r].ToString().Split(';')[1] + "')";
                    }
                    else
                    {
                        time_sql = time_sql + "  or (t.date_time>='" + time_al[r].ToString().Split(';')[0] + "' and t.date_time<='" + time_al[r].ToString().Split(';')[1] + "') ";
                    }

                    if (r == time_al.Count - 1)
                    {
                        datestr = datestr + time_al[r].ToString().Split(';')[0] + "_" + time_al[r].ToString().Split(';')[1];
                    }
                    else
                    {
                        datestr = datestr + time_al[r].ToString().Split(';')[0] + "_" + time_al[r].ToString().Split(';')[1] + ",";
                    }
                }
                if (time_al.Count > 1)
                {
                    time_sql = time_sql + " ) ";
                }
                sql_userid = sql_userid + time_sql + " order by user_id desc";
                sql_static_lost = sql_static_lost + time_sql + " order by  date_time asc ,user_id";
                sql_timepoints = sql_timepoints + time_sql + " order by date_time asc";
                DataRowCollection obj = DBHelper.DBHelper.GetDataSet(sql_userid).Tables[0].Rows;
                DataTable dt = DBHelper.DBHelper.GetDataSet(sql_static_lost).Tables[0];
                DataRowCollection timepoints = DBHelper.DBHelper.GetDataSet(sql_timepoints).Tables[0].Rows;
                #endregion
                if (timepoints.Count >= 3)
                {
                    //整理数据并算出零值比例
                    #region
                    DataRow[] drtrue = this.user_dt.Select("is_computer='True'");
                    numArraySimilar = new double[drtrue.Length + 1, timepoints.Count];
                    string[] user_c = new string[drtrue.Length + 1];
                    DataTable dt_warn = new DataTable();
                    DataColumn cl = new DataColumn("is_warn");
                    DataColumn cl1 = new DataColumn("user_id");
                    DataColumn cl2 = new DataColumn("warn_p");
                    DataColumn cl3 = new DataColumn("time_span");
                    dt_warn.Columns.Add(cl);
                    dt_warn.Columns.Add(cl1);
                    dt_warn.Columns.Add(cl2);
                    dt_warn.Columns.Add(cl3);
                    Dictionary<string, double> order_d = new Dictionary<string, double>();
                    int j;
                    int i = -1;
                    while (i < drtrue.Length)
                    {
                        DataRow dr = dt_warn.NewRow();
                        if (i > -1)
                        {
                            dr["user_id"] = drtrue[i]["user_id"].ToString();
                            // dr["time_span"] = datestr;
                        }
                        int zero_count = 0;
                        j = 0;
                        while (j < timepoints.Count)
                        {
                            DataRow[] dr11;
                            if (i == -1)
                            {
                                dr11 = dt.Select("user_id='lost' and date_time='" + timepoints[j][0].ToString() + "' ");
                                if (dr11.Length > 0)
                                {
                                    this.numArraySimilar[i + 1, j] = double.Parse(dr11[0][2].ToString());
                                    order_d.Add(timepoints[j][0].ToString() + "_" + j, double.Parse(dr11[0][2].ToString()));
                                }
                                else
                                {
                                    this.numArraySimilar[i + 1, j] = 0.0;
                                    order_d.Add(timepoints[j][0].ToString() + "_" + j, 0.0);
                                }
                                user_c[i + 1] = "lost";
                            }
                            else
                            {
                                dr11 = dt.Select("user_id='" + drtrue[i]["user_id"].ToString() + "' and date_time='" + timepoints[j][0].ToString() + "' ");
                                if (dr11.Length > 0)
                                {
                                    this.numArraySimilar[i + 1, j] = double.Parse(dr11[0][2].ToString());
                                    if (double.Parse(dr11[0][2].ToString()) == 0.0)
                                    {
                                        zero_count++;
                                    }
                                }
                                else
                                {
                                    this.numArraySimilar[i + 1, j] = 0.0;
                                    zero_count++;
                                }
                                user_c[i + 1] = drtrue[i]["user_id"].ToString();
                            }
                            j++;
                        }
                        if (i > -1)
                        {
                            double p = double.Parse(zero_count.ToString()) / double.Parse(timepoints.Count.ToString());
                            dr["warn_p"] = p.ToString("F3");
                            if (p >= 0.8)
                            {
                                dr["is_warn"] = "TRUE";
                            }
                            else
                            {
                                dr["is_warn"] = "FALSE";
                            }
                            dt_warn.Rows.Add(dr);
                        }
                        i++;
                    }
                    #endregion
                    //算出欧氏距离
                    #region
                    order_d = order_d.OrderBy<KeyValuePair<string, double>, double>(delegate(KeyValuePair<string, double> entry)
                    {
                        return entry.Value;
                    }).ToDictionary<KeyValuePair<string, double>, string, double>(delegate(KeyValuePair<string, double> pair)
                    {
                        return pair.Key;
                    }, delegate(KeyValuePair<string, double> pair)
                    {
                        return pair.Value;
                    });
                    double[,] yaozhi_rate = new double[this.numArraySimilar.GetLength(0), timepoints.Count];
                    double first = 0.0;
                    int index_p = 0;
                    int index_first = 0;
                    foreach (KeyValuePair<string, double> time_str in order_d)
                    {
                        int index_Num = int.Parse(time_str.Key.Split(new char[] { '_' })[1]);
                        if (index_p == 0)
                        {
                            first = time_str.Value;
                            index_first = index_Num;
                        }
                        i = 0;
                        while (i < this.numArraySimilar.GetLength(0))
                        {
                            if (i == 0)
                            {
                                double d_aa = time_str.Value / first;
                                yaozhi_rate[i, index_p] = double.Parse(d_aa.ToString("F2"));
                            }
                            else
                            {
                                yaozhi_rate[i, index_p] = double.Parse((this.numArraySimilar[i, index_Num] / this.numArraySimilar[i, index_first]).ToString("F2"));
                            }
                            i++;
                        }
                        index_p++;
                    }
                    double[] yaozhi = new double[yaozhi_rate.GetLength(0) - 1];
                    int i_d_index = 0;
                    for (i = 1; i < yaozhi_rate.GetLength(0); i++)
                    {
                        double d_s = 0.0;
                        j = 0;
                        while (j < yaozhi_rate.GetLength(1))
                        {
                            double dd = yaozhi_rate[0, j];
                            double dd1 = yaozhi_rate[i, j];
                            d_s += Math.Pow(yaozhi_rate[0, j] - yaozhi_rate[i, j], 2.0);
                            j++;
                        }
                        yaozhi[i_d_index] = double.Parse(Math.Pow(d_s, 0.5).ToString("F3"));
                        i_d_index++;
                    }
                    #endregion
                    //算法一
                    #region
                    ArrayList al = new ArrayList();
                    ArrayList al3 = new ArrayList();
                    string al4 = "";
                    ArrayList al1 = new ArrayList();
                    ArrayList al2 = new ArrayList();
                    for (i = 0; i < this.numArraySimilar.GetLength(1); i++)
                    {
                        if (i > 0)
                        {
                            al4 = al4 + "," + (((this.numArraySimilar[0, i] - this.numArraySimilar[0, i - 1]) / this.numArraySimilar[0, i - 1])).ToString("F2");
                            for (j = 0; j < drtrue.Length; j++)
                            {
                                int b1 = 0;
                                int b2 = 0;
                                if (i == 1)
                                {
                                    al.Add(0);
                                    al1.Add("");
                                    al2.Add(0);
                                    al3.Add("");
                                }
                                if ((this.numArraySimilar[0, i] - this.numArraySimilar[0, i - 1]) > 0.0)
                                {
                                    b1 = 1;
                                }
                                else if ((this.numArraySimilar[0, i] - this.numArraySimilar[0, i - 1]) == 0.0)
                                {
                                    b1 = 0;
                                }
                                else
                                {
                                    b1 = -1;
                                }
                                if ((this.numArraySimilar[j + 1, i] - this.numArraySimilar[j + 1, i - 1]) > 0.0)
                                {
                                    b2 = 1;
                                }
                                else if ((this.numArraySimilar[j + 1, i] - this.numArraySimilar[j + 1, i - 1]) == 0.0)
                                {
                                    b2 = 0;
                                }
                                else
                                {
                                    b2 = -1;
                                }
                                double sx_d1 = (this.numArraySimilar[0, i] - this.numArraySimilar[0, i - 1]) / this.numArraySimilar[0, i - 1];
                                double sx_d2 = (this.numArraySimilar[j + 1, i] - this.numArraySimilar[j + 1, i - 1]) / this.numArraySimilar[j + 1, i - 1];
                                al3[j] = al3[j].ToString() + "," + sx_d2.ToString("F2");
                                if ((sx_d2 / sx_d1) > 0.8)
                                {
                                    al2[j] = int.Parse(al2[j].ToString()) + 1;
                                }
                                if (b1 == b2)
                                {
                                    al[j] = int.Parse(al[j].ToString()) + 1;
                                    al1[j] = al1[j].ToString() + ",1";
                                }
                                else
                                {
                                    al1[j] = al1[j].ToString() + ",0";
                                }
                            }
                        }
                    }
                    double[] bili = new double[drtrue.Length];
                    double[] sz_bili = new double[drtrue.Length];
                    j = 0;
                    while (j < drtrue.Length)
                    {
                        bili[j] = 1.0 - (double.Parse(al[j].ToString()) / ((double)(this.numArraySimilar.GetLength(1) - 1)));
                        sz_bili[j] = double.Parse(al2[j].ToString()) / ((double)(this.numArraySimilar.GetLength(1) - 1));
                        j++;
                    }
                    #endregion
                    // double[,] a5 = new double[this.numArraySimilar.GetLength(0), timepoints.Count];
                    Dictionary<string, string> sd = new Dictionary<string, string>();
                    for (i = 0; i < drtrue.Length; i++)
                    {
                        string m1 = bili[i].ToString("F3");
                        string yaozhi_d = yaozhi[i].ToString();
                        string zero_date = dt_warn.Rows[i]["warn_p"].ToString();
                        sd.Add(drtrue[i]["user_id"].ToString(), m1 + ";" + yaozhi_d + ";" + zero_date);
                    }
                    DataView dv = dt_warn.DefaultView;
                    dv.Sort = "is_warn desc";
                    DataTable new_dt_warn = dv.ToTable();
                    this.set_lstv_m1(sd, yaozhi_rate, timepoints, datestr, dt, this.numArraySimilar, user_c, new_dt_warn, order_d);
                    b = true;
                }
                else
                {
                    b = false;
                    MessageBox.Show("选取时间不能小于三天");
                }
            }
            catch (Exception er)
            {
                b = false;
                MessageBox.Show(er.Message);
            }
            return b;
        }

        private void delete_select_box(ArrayList time_al)
        {
            string time_sql = string.Empty;
            string delete_user = "  delete from ts_user   where line_no='" + this.get_line_lost() + "'  ";
            string delete_line = "  delete from ts_line where   line_no='" + this.get_line_lost() + "' ";
            //  string sql_timepoints = " select distinct t.date_time from static_lost t  where  t.line_no='" + this.get_line_lost() + "'  ";
            if (time_al.Count > 1)
            {
                time_sql = " and ( ";
            }
            else
            {
                time_sql = " and ";
            }
            for (int r = 0; r < time_al.Count; r++)
            {

                if (r == 0)
                {
                    time_sql = time_sql + " (date_time>='" + time_al[r].ToString().Split(';')[0] + "' and date_time<='" + time_al[r].ToString().Split(';')[1] + "')";
                }
                else
                {
                    time_sql = time_sql + "  or (date_time>='" + time_al[r].ToString().Split(';')[0] + "' and date_time<='" + time_al[r].ToString().Split(';')[1] + "') ";
                }

            }
            //if (time_al.Count > 1)
            //{
            //    time_sql = time_sql + " ) ";
            //}


            delete_user = delete_user + time_sql;
            delete_line = delete_line + time_sql;
            ArrayList al = new ArrayList();
            al.Add(delete_user);
            al.Add(delete_line);
            DBHelper.DBHelper.ExecuteCommand(al);
            this.set_curr_begin_time(this.curr_begin_time);
            this.set_curr_end_time(this.curr_end_time);
            this.re_read = true;
            this.openFile();
            this.fill_user_dt();
            this.RefillDetailsList("", false);
            this.week_var();

        }

        public string BuildVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public TimeSearcher.View CurrView
        {
            get
            {
                if (this._mainTabControl.SelectedTab == this._riverTabPage)
                {
                    return TimeSearcher.View.River;
                }
                if (this._mainTabControl.SelectedTab == this._individualsTabPage)
                {
                    return TimeSearcher.View.Individuals;
                }
                if (this._mainTabControl.SelectedTab == this._allTabPage)
                {
                    return TimeSearcher.View.Var;
                }
                return TimeSearcher.View.None;
            }
        }

        public TimeSearcher.DataSet DataSet
        {
            get
            {
                return this._dataSet;
            }
        }

        public bool IsControlKeyDown
        {
            get
            {
                return this._isControlKeyDown;
            }
        }

        public bool IsDataSetLoaded
        {
            get
            {
                return this._isDataSetLoaded;
            }
        }

        public bool IsDetailsListSynchronized
        {
            get
            {
                return this._syncDetailsListMenuItem.Checked;
            }
        }

        public bool IsForecastSourceVisible
        {
            get
            {
                return this._showForecastSourceMenuItem.Checked;
            }
        }

        public bool IsOverviewSynchonized
        {
            get
            {
                return this._syncOverviewMenuItem.Checked;
            }
        }

        public TimeSearcher.ItemsListManager ItemsListManager
        {
            get
            {
                return this._itemsListManager;
            }
        }

        public bool LogMenuItemChecked
        {
            get
            {
                return this._logMenuItem.Checked;
            }
        }

        public int NumDisplayedVars
        {
            get
            {
                return this._numDisplayedVariables;
            }
        }

        public OverviewVariablePanel OVP
        {
            get
            {
                return this._overviewVariablePanel;
            }
        }

        public RiverPlotView RiverView
        {
            get
            {
                return this._riverPlotView;
            }
        }

        public bool ShdDrawTimeLine
        {
            get
            {
                return (this._showTimeLineMenuItem.Checked && (this._dataSet.currTimePtIndex != -1));
            }
        }

        public VariablesView VarView
        {
            get
            {
                return this._varView;
            }
        }

        private void _btn_rule_Click(object sender, EventArgs e)
        {

        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.bUpdating = true;
            if (Utils.SafeShowDialog(this._openTqdFileDlg) == DialogResult.OK)
            {
                INIClass iniclass = new INIClass(Application.StartupPath + @"\system.ini");
                string filename = this._openTqdFileDlg.FileNames[0].ToString();
                iniclass.IniWriteValue("file", "filepath", filename.Substring(0, filename.LastIndexOf(@"\")));
                this.task_str = "导入功率数据";
                this.worker.RunWorkerAsync();
                this.cmd.ShowOpaqueLayer(this, 0x7d, true);
            }
            this.bUpdating = false;
        }
        private void get_rule(ArrayList time_al)
        {
            //  show_av_pic();
            CheckForIllegalCrossThreadCalls = false;

            System.Data.DataTable dt_rule = rule_datatable();
            if (dt_rule != null && dt_rule.Rows.Count > 0)
            {
                //DataRow[] dr_sources = dt_rule_source.Select(time_sql);
                //System.Data.DataTable dt_rule = new DataTable();
                //dt_rule = dt_rule_source.Clone();
                //dt_rule=dr_sources.CopyToDataTable();
                System.Data.DataTable dt_rule_desc = new DataTable();
                DataColumn cl_userid = new DataColumn("user_id");
                DataColumn cl_date = new DataColumn("date");
                DataColumn cl_end_date = new DataColumn("end_date");
                DataColumn cl_rule = new DataColumn("rule");
                DataColumn cl_describe = new DataColumn("describe");
                //定义辅助变量
                #region
                INIClass iniclass = new INIClass(Application.StartupPath + @"\system.ini");


                double shiya_v = 0.9;
                double shiya_p = 0.75;
                double gaoji = 100;
                double diji = 220;
                bool ck_rule_1 = true;

                double fanjixing_p = 0.9;
                double fj_p = 0.75;
                bool ck_rule_4 = true;

                double gonglv_p = 0.9;
                double gonglv_p_2 = 0.9;
                double gonglv_p_3 = 0.9;
                double gonglv_a = 2.1;
                double gonglv_c = 1.3;
                double dushu1 = 30;
                double dushu2 = 30;
                bool ck_rule_7 = true;

                double shiliu = 0.5;
                double shiliu_p = 0.75;
                bool ck_rule_2 = true;

                double av_p = 2;
                double cv_p = 0.5;
                bool ck_rule_3 = true;

                double av_o = 0.5;
                double gonglvys = 0.9;
                double glycys_p = 0.75;
                bool ck_rule_5 = true;

                double glbyz = 0.1;
                bool ck_rule_9 = true;

                bool ck_rule_8 = true;

                double p_biaoma = 0.001;
                double biaoma = 0.0001;
                bool ck_rule_6 = true;
                try
                {
                    ck_rule_1 = bool.Parse(iniclass.IniReadValue("rule1", "is_used"));
                    shiya_v = double.Parse(iniclass.IniReadValue("rule1", "shiya_v"));
                    shiya_p = double.Parse(iniclass.IniReadValue("rule1", "shiya_p"));

                    ck_rule_4 = bool.Parse(iniclass.IniReadValue("rule4", "is_used"));
                    fanjixing_p = double.Parse(iniclass.IniReadValue("rule4", "fanjixing_p"));
                    fj_p = double.Parse(iniclass.IniReadValue("rule4", "fj_p"));


                    ck_rule_7 = bool.Parse(iniclass.IniReadValue("rule7", "is_used"));
                    gonglv_p = double.Parse(iniclass.IniReadValue("rule7", "gonglv_p"));
                    dushu1 = double.Parse(iniclass.IniReadValue("rule7", "dushu_1"));
                    dushu2 = double.Parse(iniclass.IniReadValue("rule7", "dushu_2"));
                    gonglv_p_2 = double.Parse(iniclass.IniReadValue("rule7", "gonglv_p_2"));
                    gonglv_p_3 = double.Parse(iniclass.IniReadValue("rule7", "gonglv_p_3"));
                    gonglv_a = double.Parse(iniclass.IniReadValue("rule7", "gonglv_a"));
                    gonglv_c = double.Parse(iniclass.IniReadValue("rule7", "gonglv_c"));

                    ck_rule_2 = bool.Parse(iniclass.IniReadValue("rule2", "is_used"));
                    shiliu = double.Parse(iniclass.IniReadValue("rule2", "shiliu"));
                    shiliu_p = double.Parse(iniclass.IniReadValue("rule2", "shiliu_p"));

                    ck_rule_3 = bool.Parse(iniclass.IniReadValue("rule3", "is_used"));
                    av_p = double.Parse(iniclass.IniReadValue("rule3", "av_v"));
                    cv_p = double.Parse(iniclass.IniReadValue("rule3", "av_p"));

                    ck_rule_5 = bool.Parse(iniclass.IniReadValue("rule5", "is_used"));
                    av_o = double.Parse(iniclass.IniReadValue("rule5", "av_o"));
                    gonglvys = double.Parse(iniclass.IniReadValue("rule5", "gonglvys"));
                    glycys_p = double.Parse(iniclass.IniReadValue("rule5", "glycys_p"));

                    ck_rule_9 = bool.Parse(iniclass.IniReadValue("rule9", "is_used"));
                    glbyz = double.Parse(iniclass.IniReadValue("rule9", "glbyz"));

                    ck_rule_8 = bool.Parse(iniclass.IniReadValue("rule8", "is_used"));


                    ck_rule_6 = bool.Parse(iniclass.IniReadValue("rule6", "is_used"));
                    p_biaoma = double.Parse(iniclass.IniReadValue("rule6", "p_biaoma"));
                    biaoma = double.Parse(iniclass.IniReadValue("rule6", "biaoma"));


                }
                catch (Exception er)
                {

                }
                #endregion



                dt_rule_desc.Columns.Add(cl_userid);
                dt_rule_desc.Columns.Add(cl_date);
                dt_rule_desc.Columns.Add(cl_end_date);
                dt_rule_desc.Columns.Add(cl_rule);
                dt_rule_desc.Columns.Add(cl_describe);
                ArrayList al = new ArrayList();
                ArrayList alnor = new ArrayList();
                //失压
                if (ck_rule_1)
                {
                    #region
                    //高计A相
                    var query_hav = from order_a in dt_rule.AsEnumerable()
                                    where order_a.Field<double>("B_V") == 0
                                    && order_a.Field<double>("A_V") < gaoji * shiya_v
                                    && order_a.Field<double>("A_V") != 0

                                    group order_a by new
                                    {
                                        cl1 = order_a.Field<string>("user_id"),
                                        cl2 = order_a.Field<DateTime>("date_time").ToString("yyyy-MM-dd")
                                    } into g
                                    select new
                                    {
                                        order_userid = g.Key.cl1,
                                        order_datetime = g.Key.cl2,
                                        order_acount = g.Count()
                                    };

                    //高级C相
                    var query_hcv = from order_a in dt_rule.AsEnumerable()
                                    where order_a.Field<double>("b_v") == 0
                                   && order_a.Field<double>("c_v") < gaoji * shiya_v
                                       && order_a.Field<double>("C_V") != 0
                                    group order_a by new
                                    {
                                        cl1 = order_a.Field<string>("user_id"),
                                        cl2 = order_a.Field<DateTime>("date_time").ToString("yyyy-MM-dd")
                                    } into g
                                    select new
                                    {
                                        order_userid = g.Key.cl1,
                                        order_datetime = g.Key.cl2,
                                        order_acount = g.Count()
                                    };

                    //低计A相
                    var query_lav = from order_a in dt_rule.AsEnumerable()
                                    where order_a.Field<double>("b_v") > 0
                                    && order_a.Field<double>("a_v") < diji * shiya_v
                                        && order_a.Field<double>("A_V") != 0
                                    group order_a by new
                                    {
                                        cl1 = order_a.Field<string>("user_id"),
                                        cl2 = order_a.Field<DateTime>("date_time").ToString("yyyy-MM-dd")
                                    } into g
                                    select new
                                    {
                                        order_userid = g.Key.cl1,
                                        order_datetime = g.Key.cl2,
                                        order_acount = g.Count()
                                    };
                    //低计b相
                    var query_lbv = from order_a in dt_rule.AsEnumerable()
                                    where order_a.Field<double>("b_v") > 0
                                    && order_a.Field<double>("b_v") < diji * shiya_v
                                    group order_a by new
                                    {
                                        cl1 = order_a.Field<string>("user_id"),
                                        cl2 = order_a.Field<DateTime>("date_time").ToString("yyyy-MM-dd")
                                    } into g
                                    select new
                                    {
                                        order_userid = g.Key.cl1,
                                        order_datetime = g.Key.cl2,
                                        order_acount = g.Count()
                                    };
                    //低计c相
                    var query_lcv = from order_a in dt_rule.AsEnumerable()
                                    where order_a.Field<double>("b_v") > 0
                                    && order_a.Field<double>("c_v") < diji * shiya_v
                                     && order_a.Field<double>("C_V") != 0
                                    group order_a by new
                                    {
                                        cl1 = order_a.Field<string>("user_id"),
                                        cl2 = order_a.Field<DateTime>("date_time").ToString("yyyy-MM-dd")
                                    } into g
                                    select new
                                    {
                                        order_userid = g.Key.cl1,
                                        order_datetime = g.Key.cl2,
                                        order_acount = g.Count()
                                    };
                    foreach (var order_q in query_hav)
                    {
                        if ((order_q.order_acount / 96) > shiya_p)
                        {
                            DataRow dr = dt_rule_desc.NewRow();
                            dr["user_id"] = order_q.order_userid;
                            dr["date"] = order_q.order_datetime;
                            dr["describe"] = "高计用户" + order_q.order_userid + "A相,在" + order_q.order_datetime + "符合失压条件";
                            dr["rule"] = "失压";
                            if (!al.Contains(order_q.order_userid))
                            {
                                al.Add(order_q.order_userid);
                            }
                            dt_rule_desc.Rows.Add(dr);
                        }
                        else
                        {
                            if (!alnor.Contains(order_q.order_userid))
                            {
                                alnor.Add(order_q.order_userid);
                            }
                        }
                    }
                    foreach (var order_q in query_hcv)
                    {
                        if ((order_q.order_acount / 96) > shiya_p)
                        {
                            DataRow dr = dt_rule_desc.NewRow();
                            dr["user_id"] = order_q.order_userid;
                            dr["date"] = order_q.order_datetime;
                            dr["describe"] = "高计用户" + order_q.order_userid + "C相,在" + order_q.order_datetime + "符合失压条件";
                            dr["rule"] = "失压";
                            if (!al.Contains(order_q.order_userid))
                            {
                                al.Add(order_q.order_userid);
                            }
                            dt_rule_desc.Rows.Add(dr);
                        }
                        else
                        {
                            if (!alnor.Contains(order_q.order_userid))
                            {
                                alnor.Add(order_q.order_userid);
                            }
                        }
                    }
                    foreach (var order_q in query_lav)
                    {
                        if ((order_q.order_acount / 96) > shiya_p)
                        {
                            DataRow dr = dt_rule_desc.NewRow();
                            dr["user_id"] = order_q.order_userid;
                            dr["date"] = order_q.order_datetime;
                            dr["describe"] = "低计用户" + order_q.order_userid + "A相,在" + order_q.order_datetime + "符合失压条件";
                            dr["rule"] = "失压";
                            if (!al.Contains(order_q.order_userid))
                            {
                                al.Add(order_q.order_userid);
                            }
                            dt_rule_desc.Rows.Add(dr);
                        }
                        else
                        {
                            if (!alnor.Contains(order_q.order_userid))
                            {
                                alnor.Add(order_q.order_userid);
                            }
                        }
                    }
                    foreach (var order_q in query_lbv)
                    {
                        if ((order_q.order_acount / 96) > shiya_p)
                        {
                            DataRow dr = dt_rule_desc.NewRow();
                            dr["user_id"] = order_q.order_userid;
                            dr["date"] = order_q.order_datetime;
                            dr["describe"] = "低计用户" + order_q.order_userid + "B相,在" + order_q.order_datetime + "符合失压条件";
                            dr["rule"] = "失压";
                            if (!al.Contains(order_q.order_userid))
                            {
                                al.Add(order_q.order_userid);
                            }
                            dt_rule_desc.Rows.Add(dr);
                        }
                        else
                        {
                            if (!alnor.Contains(order_q.order_userid))
                            {
                                alnor.Add(order_q.order_userid);
                            }
                        }
                    }
                    foreach (var order_q in query_lcv)
                    {
                        if ((order_q.order_acount / 96) > shiya_p)
                        {
                            DataRow dr = dt_rule_desc.NewRow();
                            dr["user_id"] = order_q.order_userid;
                            dr["date"] = order_q.order_datetime;
                            dr["describe"] = "低计用户" + order_q.order_userid + "C相,在" + order_q.order_datetime + "符合失压条件";
                            dr["rule"] = "失压";
                            if (!al.Contains(order_q.order_userid))
                            {
                                al.Add(order_q.order_userid);
                            }
                            dt_rule_desc.Rows.Add(dr);
                        }
                        else
                        {
                            if (!alnor.Contains(order_q.order_userid))
                            {
                                alnor.Add(order_q.order_userid);
                            }
                        }
                    }
                    #endregion
                }
                //反极性           
                if (ck_rule_4)
                {
                    #region
                    var query_fan = from order_a in dt_rule.AsEnumerable()
                                    where (order_a.Field<double>("C_KW") + order_a.Field<double>("B_KW") + order_a.Field<double>("A_KW")) * fanjixing_p > order_a.Field<double>("CURR_KW")
                                    group order_a by new
                                    {
                                        cl1 = order_a.Field<string>("user_id"),
                                        cl2 = order_a.Field<DateTime>("date_time").ToString("yyyy-MM-dd")
                                    } into g
                                    select new
                                    {
                                        order_userid = g.Key.cl1,
                                        order_datetime = g.Key.cl2,
                                        order_acount = g.Count()
                                    };
                    foreach (var order_q in query_fan)
                    {
                        if ((order_q.order_acount / 96) > fj_p)
                        {
                            DataRow dr = dt_rule_desc.NewRow();
                            dr["user_id"] = order_q.order_userid;
                            dr["date"] = order_q.order_datetime;
                            dr["describe"] = "用户" + order_q.order_userid + "在" + order_q.order_datetime + "符合反极性规则";
                            dr["rule"] = "反极性";
                            if (!al.Contains(order_q.order_userid))
                            {
                                al.Add(order_q.order_userid);
                            }
                            dt_rule_desc.Rows.Add(dr);
                        }
                        else
                        {
                            if (!alnor.Contains(order_q.order_userid))
                            {
                                alnor.Add(order_q.order_userid);
                            }
                        }
                    }

                    #endregion

                }
                //功率异常
                if (ck_rule_7)
                {
                    #region
                    var query_kw_l = from order_a in dt_rule.AsEnumerable()
                                     where order_a.Field<double>("B_V") > 0
                                     && (order_a.Field<double>("A_V") * order_a.Field<double>("A_V_A") * order_a.Field<double>("total_kw") * gonglv_p > order_a.Field<double>("A_KW") * 1000
                                     || order_a.Field<double>("B_V") * order_a.Field<double>("B_V_B") * order_a.Field<double>("total_kw") * gonglv_p > order_a.Field<double>("B_KW") * 1000
                                     || order_a.Field<double>("C_V") * order_a.Field<double>("C_V_C") * order_a.Field<double>("total_kw") * gonglv_p > order_a.Field<double>("C_KW") * 1000)
                                     group order_a by new
                                     {
                                         cl1 = order_a.Field<string>("user_id"),
                                         cl2 = order_a.Field<DateTime>("date_time").ToString("yyyy-MM-dd")
                                     } into g
                                     select new
                                     {
                                         order_userid = g.Key.cl1,
                                         order_datetime = g.Key.cl2,
                                         order_acount = g.Count()
                                     };
                    var query_kw_h = from order_a in dt_rule.AsEnumerable()
                                     where order_a.Field<double>("B_V") == 0
                                     && (order_a.Field<double>("A_V") * order_a.Field<double>("A_V_A") * double.Parse(Math.Cos(Math.Acos(double.Parse(order_a.Field<double>("total_kw").ToString())) + dushu1).ToString()) * gonglv_p_2 > order_a.Field<double>("A_KW") * 1000
                                     || order_a.Field<double>("C_V") * order_a.Field<double>("C_V_C") * double.Parse(Math.Cos(Math.Acos(double.Parse(order_a.Field<double>("total_kw").ToString())) + dushu2).ToString()) * gonglv_p_3 > order_a.Field<double>("C_KW") * 1000
                                     || order_a.Field<double>("A_KW") * gonglv_a != order_a.Field<double>("C_KW")
                                     )
                                     group order_a by new
                                     {
                                         cl1 = order_a.Field<string>("user_id"),
                                         cl2 = order_a.Field<DateTime>("date_time").ToString("yyyy-MM-dd")
                                     } into g
                                     select new
                                     {
                                         order_userid = g.Key.cl1,
                                         order_datetime = g.Key.cl2,
                                         order_acount = g.Count()
                                     };

                    foreach (var order_q in query_kw_l)
                    {
                        if ((order_q.order_acount / 96) > shiya_p)
                        {
                            DataRow dr = dt_rule_desc.NewRow();
                            dr["user_id"] = order_q.order_userid;
                            dr["date"] = order_q.order_datetime;
                            dr["describe"] = "用户" + order_q.order_userid + "在" + order_q.order_datetime + "功率异常规则";
                            dr["rule"] = "功率异常";
                            if (!al.Contains(order_q.order_userid))
                            {
                                al.Add(order_q.order_userid);
                            }
                            dt_rule_desc.Rows.Add(dr);
                        }
                        else
                        {
                            if (!alnor.Contains(order_q.order_userid))
                            {
                                alnor.Add(order_q.order_userid);
                            }
                        }
                    }
                    foreach (var order_q in query_kw_h)
                    {
                        if ((order_q.order_acount / 96) > shiya_p)
                        {
                            DataRow dr = dt_rule_desc.NewRow();
                            dr["user_id"] = order_q.order_userid;
                            dr["date"] = order_q.order_datetime;
                            dr["describe"] = "用户" + order_q.order_userid + "在" + order_q.order_datetime + "功率异常规则";
                            dr["rule"] = "功率异常";
                            if (!al.Contains(order_q.order_userid))
                            {
                                al.Add(order_q.order_userid);
                            }
                            dt_rule_desc.Rows.Add(dr);
                        }
                        else
                        {
                            if (!alnor.Contains(order_q.order_userid))
                            {
                                alnor.Add(order_q.order_userid);
                            }
                        }
                    }
                    #endregion
                }
                //失流
                if (ck_rule_2)
                {
                    #region
                    var query_av_l = from order_a in dt_rule.AsEnumerable()
                                     where order_a.Field<double>("B_V") > 0
                                     && (order_a.Field<double>("A_V_A") < shiliu || order_a.Field<double>("B_V_B") < shiliu || order_a.Field<double>("C_V_C") < shiliu)
                                     group order_a by new
                                   {
                                       cl1 = order_a.Field<string>("user_id"),
                                       cl2 = order_a.Field<DateTime>("date_time").ToString("yyyy-MM-dd")
                                   } into g
                                     select new
                                     {
                                         order_userid = g.Key.cl1,
                                         order_datetime = g.Key.cl2,
                                         order_acount = g.Count()
                                     };
                    var query_av_h = from order_a in dt_rule.AsEnumerable()
                                     where order_a.Field<double>("B_V") == 0
                                     && (order_a.Field<double>("A_V_A") < shiliu || order_a.Field<double>("C_V_C") < shiliu)
                                     group order_a by new
                                     {
                                         cl1 = order_a.Field<string>("user_id"),
                                         cl2 = order_a.Field<DateTime>("date_time").ToString("yyyy-MM-dd")
                                     } into g
                                     select new
                                     {
                                         order_userid = g.Key.cl1,
                                         order_datetime = g.Key.cl2,
                                         order_acount = g.Count()
                                     };
                    foreach (var order_q in query_av_l)
                    {
                        if ((order_q.order_acount / 96) > shiliu_p)
                        {
                            DataRow dr = dt_rule_desc.NewRow();
                            dr["user_id"] = order_q.order_userid;
                            dr["date"] = order_q.order_datetime;
                            dr["describe"] = "用户" + order_q.order_userid + "在" + order_q.order_datetime + "失流规则";
                            dr["rule"] = "失流";
                            if (!al.Contains(order_q.order_userid))
                            {
                                al.Add(order_q.order_userid);
                            }
                            dt_rule_desc.Rows.Add(dr);
                        }
                        else
                        {
                            if (!alnor.Contains(order_q.order_userid))
                            {
                                alnor.Add(order_q.order_userid);
                            }
                        }
                    }
                    foreach (var order_q in query_av_h)
                    {
                        if ((order_q.order_acount / 96) > shiliu_p)
                        {
                            DataRow dr = dt_rule_desc.NewRow();
                            dr["user_id"] = order_q.order_userid;
                            dr["date"] = order_q.order_datetime;
                            dr["describe"] = "用户" + order_q.order_userid + "在" + order_q.order_datetime + "失流规则";
                            dr["rule"] = "失流";
                            if (!al.Contains(order_q.order_userid))
                            {
                                al.Add(order_q.order_userid);
                            }
                            dt_rule_desc.Rows.Add(dr);
                        }
                        else
                        {
                            if (!alnor.Contains(order_q.order_userid))
                            {
                                alnor.Add(order_q.order_userid);
                            }
                        }
                    }
                    #endregion

                }
                //电流不平衡
                if (ck_rule_3)
                {
                    #region
                    var query_av_b_h = from order_a in dt_rule.AsEnumerable()
                                       where order_a.Field<double>("B_V") == 0
                                       && order_a.Field<double>("C_V_C") > 0
                                       && order_a.Field<double>("A_V_A") > 0
                                       && (order_a.Field<double>("A_V_A") / order_a.Field<double>("C_V_C") > av_p || order_a.Field<double>("A_V_A") / order_a.Field<double>("C_V_C") < cv_p)
                                       group order_a by new
                                       {
                                           cl1 = order_a.Field<string>("user_id"),
                                           cl2 = order_a.Field<DateTime>("date_time").ToString("yyyy-MM-dd")
                                       } into g
                                       select new
                                       {
                                           order_userid = g.Key.cl1,
                                           order_datetime = g.Key.cl2,
                                           order_acount = g.Count()
                                       };
                    foreach (var order_q in query_av_b_h)
                    {
                        if ((order_q.order_acount / 96) > shiya_p)
                        {
                            DataRow dr = dt_rule_desc.NewRow();
                            dr["user_id"] = order_q.order_userid;
                            dr["date"] = order_q.order_datetime;
                            dr["describe"] = "用户" + order_q.order_userid + "在" + order_q.order_datetime + "电流不平衡规则";
                            dr["rule"] = "电流不平衡";
                            if (!al.Contains(order_q.order_userid))
                            {
                                al.Add(order_q.order_userid);
                            }
                            dt_rule_desc.Rows.Add(dr);
                        }
                        else
                        {
                            if (!alnor.Contains(order_q.order_userid))
                            {
                                alnor.Add(order_q.order_userid);
                            }
                        }
                    }
                    #endregion
                }
                //功率因素异常
                if (ck_rule_5)
                {
                    #region
                    var query_kw = from order_a in dt_rule.AsEnumerable()
                                   where Math.Cos(double.Parse(order_a.Field<double>("total_kw").ToString())) > gonglvys
                                   &&
                                   (order_a.Field<double>("A_V_A") > av_o || order_a.Field<double>("B_V_B") > av_o || order_a.Field<double>("C_V_C") > av_o)
                                   group order_a by new
                                   {
                                       cl1 = order_a.Field<string>("user_id"),
                                       cl2 = order_a.Field<DateTime>("date_time").ToString("yyyy-MM-dd")
                                   } into g
                                   select new
                                   {
                                       order_userid = g.Key.cl1,
                                       order_datetime = g.Key.cl2,
                                       order_acount = g.Count()
                                   };
                    foreach (var order_q in query_kw)
                    {
                        if ((order_q.order_acount / 96) > glycys_p)
                        {
                            DataRow dr = dt_rule_desc.NewRow();
                            dr["user_id"] = order_q.order_userid;
                            dr["date"] = order_q.order_datetime;
                            dr["describe"] = "用户" + order_q.order_userid + "在" + order_q.order_datetime + "功率因素异常规则";
                            dr["rule"] = "功率因素异常";
                            if (!al.Contains(order_q.order_userid))
                            {
                                al.Add(order_q.order_userid);
                            }
                            dt_rule_desc.Rows.Add(dr);
                        }
                        else
                        {
                            if (!alnor.Contains(order_q.order_userid))
                            {
                                alnor.Add(order_q.order_userid);
                            }
                        }
                    }

                    #endregion

                }
                //功率不一致


                if (ck_rule_9)
                {
                    #region
                    var query_kwbyz = from order_a in dt_rule.AsEnumerable()
                                      where order_a.Field<double>("m_zyb") > 0
                                      &&
                                     (order_a.Field<double>("m_zyb") - order_a.Field<double>("f_zyb")) / order_a.Field<double>("m_zyb") > glbyz
                                      group order_a by new
                                       {
                                           cl1 = order_a.Field<string>("user_id"),
                                           cl2 = order_a.Field<DateTime>("date_time").ToString("yyyy-MM-dd")
                                       } into g
                                      select new
                                      {
                                          order_userid = g.Key.cl1,
                                          order_datetime = g.Key.cl2,
                                          order_acount = g.Count()
                                      };
                    foreach (var order_q in query_kwbyz)
                    {
                        if ((order_q.order_acount / 24) > shiya_p)
                        {
                            DataRow dr = dt_rule_desc.NewRow();
                            dr["user_id"] = order_q.order_userid;
                            dr["date"] = order_q.order_datetime;
                            dr["describe"] = "用户" + order_q.order_userid + "在" + order_q.order_datetime + "功率不一致规则";
                            dr["rule"] = "功率不一致";
                            if (!al.Contains(order_q.order_userid))
                            {
                                al.Add(order_q.order_userid);
                            }
                            dt_rule_desc.Rows.Add(dr);
                        }
                        else
                        {
                            if (!alnor.Contains(order_q.order_userid))
                            {
                                alnor.Add(order_q.order_userid);
                            }
                        }
                    }

                    #endregion
                }
                //不完全星形接线中线中断



                if (ck_rule_8)
                {
                    #region
                    var query_star = from order_a in dt_rule.AsEnumerable()
                                     where order_a.Field<double>("B_V") == 0
                                     && (order_a.Field<double>("A_V") * order_a.Field<double>("A_V_A") * double.Parse(Math.Cos(Math.Acos(double.Parse(order_a.Field<double>("total_kw").ToString())) + dushu1).ToString()) * gonglv_p_2 > order_a.Field<double>("A_KW")
                                     || order_a.Field<double>("C_V") * order_a.Field<double>("C_V_C") * double.Parse(Math.Cos(Math.Acos(double.Parse(order_a.Field<double>("total_kw").ToString())) + dushu2).ToString()) * gonglv_p_3 > order_a.Field<double>("C_KW")
                                     || order_a.Field<double>("A_KW") * gonglv_a < order_a.Field<double>("C_KW")
                                     || order_a.Field<double>("A_KW") * gonglv_c > order_a.Field<double>("C_KW")
                                     )
                                     && order_a.Field<double>("A_V_A") == order_a.Field<double>("C_V_C")
                                     group order_a by new
                                     {
                                         cl1 = order_a.Field<string>("user_id"),
                                         cl2 = order_a.Field<DateTime>("date_time").ToString("yyyy-MM-dd")
                                     } into g
                                     select new
                                     {
                                         order_userid = g.Key.cl1,
                                         order_datetime = g.Key.cl2,
                                         order_acount = g.Count()
                                     };
                    foreach (var order_q in query_star)
                    {
                        if ((order_q.order_acount / 96) > shiya_p)
                        {
                            DataRow dr = dt_rule_desc.NewRow();
                            dr["user_id"] = order_q.order_userid;
                            dr["date"] = order_q.order_datetime;
                            dr["describe"] = "用户" + order_q.order_userid + "在" + order_q.order_datetime + "不完全星形接线中线中断规则";
                            dr["rule"] = "不完全星形接线中线中断";
                            if (!al.Contains(order_q.order_userid))
                            {
                                al.Add(order_q.order_userid);
                            }
                            dt_rule_desc.Rows.Add(dr);
                        }
                        else
                        {
                            if (!alnor.Contains(order_q.order_userid))
                            {
                                alnor.Add(order_q.order_userid);
                            }
                        }
                    }
                    #endregion
                }



                DataView dv_dis = dt_rule_desc.DefaultView;
                DataTable dt_distinct = dv_dis.ToTable(true, "user_id", "date", "rule");
                DataTable dt_rule_distinct = dt_rule_desc.Clone();
                DataRow dr_dis_new = dt_rule_distinct.NewRow();
                // DataRow dr_dis_new;
                ArrayList al_dis = new ArrayList();
                for (int i = 0; i < dt_distinct.Rows.Count; i++)
                {
                    if (al_dis.Contains(dt_distinct.Rows[i]["user_id"].ToString() + "@" + dt_distinct.Rows[i]["rule"].ToString()))
                    {
                        if (DateTime.Parse(dt_distinct.Rows[i]["date"].ToString()) == DateTime.Parse(dt_distinct.Rows[i - 1]["date"].ToString()).AddDays(1))
                        {
                            dr_dis_new["user_id"] = dt_distinct.Rows[i]["user_id"].ToString();
                            dr_dis_new["rule"] = dt_distinct.Rows[i]["rule"].ToString();
                            if (i == dt_distinct.Rows.Count - 1)
                            {
                                dr_dis_new["end_date"] = dt_distinct.Rows[i]["date"].ToString();
                                dt_rule_distinct.Rows.Add(dr_dis_new);
                            }
                        }
                        else
                        {
                            dr_dis_new["end_date"] = dt_distinct.Rows[i - 1]["date"].ToString();
                            dt_rule_distinct.Rows.Add(dr_dis_new);
                            dr_dis_new = dt_rule_distinct.NewRow();
                            dr_dis_new["user_id"] = dt_distinct.Rows[i]["user_id"].ToString();
                            dr_dis_new["rule"] = dt_distinct.Rows[i]["rule"].ToString();
                            dr_dis_new["date"] = dt_distinct.Rows[i]["date"].ToString();
                        }

                    }
                    else
                    {
                        if (dr_dis_new["user_id"].ToString() != "")
                        {
                            dr_dis_new["end_date"] = dt_distinct.Rows[i - 1]["date"].ToString();
                            dt_rule_distinct.Rows.Add(dr_dis_new);
                            dr_dis_new = dt_rule_distinct.NewRow();
                            dr_dis_new["user_id"] = dt_distinct.Rows[i]["user_id"].ToString();
                            dr_dis_new["date"] = dt_distinct.Rows[i]["date"].ToString();
                            dr_dis_new["rule"] = dt_distinct.Rows[i]["rule"].ToString();
                            al_dis.Add(dt_distinct.Rows[i]["user_id"].ToString() + "@" + dt_distinct.Rows[i]["rule"].ToString());
                        }
                        else
                        {
                            dr_dis_new = dt_rule_distinct.NewRow();
                            dr_dis_new["user_id"] = dt_distinct.Rows[i]["user_id"].ToString();
                            dr_dis_new["date"] = dt_distinct.Rows[i]["date"].ToString();
                            dr_dis_new["rule"] = dt_distinct.Rows[i]["rule"].ToString();
                            al_dis.Add(dt_distinct.Rows[i]["user_id"].ToString() + "@" + dt_distinct.Rows[i]["rule"].ToString());
                        }
                    }

                }
                //电量差异常



                if (ck_rule_6)
                {
                    #region
                    for (int i = 0; i < user_dt.Rows.Count; i++)
                    {
                        DataRow[] drs = dt_rule.Select("user_id='" + user_dt.Rows[i]["user_id"].ToString() + "' and m_zyb>0 ");
                        if (drs.Length > 2)
                        {
                            double m_used = double.Parse(drs[drs.Length - 1]["m_zyb"].ToString()) - double.Parse(drs[0]["m_zyb"].ToString());
                            double f_used = double.Parse(drs[drs.Length - 1]["f_zyb"].ToString()) - double.Parse(drs[0]["f_zyb"].ToString());
                            double pp = Math.Abs((m_used - f_used) / m_used);
                            double pv = Math.Abs(m_used - f_used);
                            if (Math.Abs((m_used - f_used) / m_used) > p_biaoma && Math.Abs(m_used - f_used) > biaoma)
                            {
                                DataRow dr = dt_rule_distinct.NewRow();
                                dr["user_id"] = user_dt.Rows[i]["user_id"].ToString();
                                dr["date"] = drs[0]["date_time"].ToString();
                                dr["rule"] = "电量差异常";
                                dr["end_date"] = drs[drs.Length - 1]["date_time"].ToString();
                                dt_rule_distinct.Rows.Add(dr);
                            }
                        }

                    }
                    #endregion
                }
                //统计计算规则数据
                #region
                var q_distinct = dt_rule_desc.AsEnumerable()
                              .Select(q_row => new
                              {
                                  q_user_id = q_row.Field<string>("user_id"),
                                  q_date = q_row.Field<string>("date"),
                                  r_rule = q_row.Field<string>("rule"),
                              }).Distinct();
                var q_distinct_count = from q_count in q_distinct.AsEnumerable()
                                       group q_count by new
                                       {
                                           c1 = q_count.q_user_id,
                                           c2 = q_count.r_rule
                                       } into g
                                       select new
                                       {
                                           count_user_id = g.Key.c1,
                                           count_rule = g.Key.c2,
                                           cont_group = g.Count()
                                       };
                DataTable dt_rule_count = new DataTable();
                DataColumn rule_cl1 = new DataColumn("用户");
                DataColumn rule_cl2 = new DataColumn("百分比");
                DataColumn rule_cl3 = new DataColumn("规则");


                dt_rule_count.Columns.Add(rule_cl1);
                dt_rule_count.Columns.Add(rule_cl2);
                dt_rule_count.Columns.Add(rule_cl3);

                int date_count = 0;
                for (int i = 0; i < time_al.Count; i++)
                {
                    TimeSpan ts = DateTime.Parse(time_al[i].ToString().Split(';')[1]) - DateTime.Parse(time_al[i].ToString().Split(';')[0]);
                    date_count += ts.Days;
                }

                foreach (var q_d in q_distinct_count)
                {
                    DataRow dr_rule_count = dt_rule_count.NewRow();
                    dr_rule_count[0] = q_d.count_user_id;
                    dr_rule_count[2] = q_d.count_rule;
                    dr_rule_count[1] = ((double)q_d.cont_group / (double)date_count).ToString("F2");
                    dt_rule_count.Rows.Add(dr_rule_count);
                }
                DataTable dt_rule_rc = DBHelper.DataTableHelp.RowToColumn(dt_rule_count, "规则", "百分比");
                DataColumn rule_cl4 = new DataColumn("算法A");
                dt_rule_rc.Columns.Add(rule_cl4);
                for (int i = 0; i < user_dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt_rule_rc.Rows.Count; j++)
                    {
                        if (user_dt.Rows[i]["user_id"].ToString() == dt_rule_rc.Rows[j]["用户"].ToString())
                        {
                            dt_rule_rc.Rows[j]["算法A"] = user_dt.Rows[i]["m1"].ToString();
                        }
                    }

                }

                dgv_rule_p.DataSource = dt_rule_rc;
                if (dgv_rule_p.ColumnCount > 1)
                {
                    dgv_rule_p.Columns[1].Frozen = true;
                }
                #endregion
                //缺失数据统计
                #region
                DataTable dt_rule_no = new DataTable();
                dt_rule_no.Columns.Add("用户");
                dt_rule_no.Columns.Add("缺失比例");
                dt_rule_no.Columns.Add("A相电压");
                dt_rule_no.Columns.Add("B相电压");
                dt_rule_no.Columns.Add("C相电压");
                dt_rule_no.Columns.Add("A相电流");
                dt_rule_no.Columns.Add("B相电流");
                dt_rule_no.Columns.Add("C相电流");
                dt_rule_no.Columns.Add("A相功率");
                dt_rule_no.Columns.Add("B相功率");
                dt_rule_no.Columns.Add("C相功率");
                dt_rule_no.Columns.Add("当前有功功率");
                dt_rule_no.Columns.Add("总功率因素");


                var query_queshi = from order_a in dt_rule.AsEnumerable()
                                   where order_a.Field<DateTime>("date_time") > DateTime.Parse(time_al[0].ToString().Split(';')[0])
                                   && order_a.Field<DateTime>("date_time") < DateTime.Parse(time_al[time_al.Count - 1].ToString().Split(';')[1])
                                   group order_a by new
                                   {
                                       cl1 = order_a.Field<string>("user_id")
                                   } into g
                                   select new
                                   {
                                       order_userid = g.Key.cl1,
                                       order_acount = g.Count()
                                   };
                foreach (var q_queshi in query_queshi)
                {
                    DataRow dr = dt_rule_no.NewRow();
                    dr["用户"] = q_queshi.order_userid;
                    dr["缺失比例"] = (((double)(date_count * 96) - (double)q_queshi.order_acount) / (double)(date_count * 96)).ToString("F2");
                    dr["A相电压"] = 0;
                    dr["C相电压"] = 0;
                    dr["B相电压"] = 0;
                    dr["A相电流"] = 0;
                    dr["B相电流"] = 0;
                    dr["C相电流"] = 0;
                    dr["A相功率"] = 0;
                    dr["C相功率"] = 0;
                    dr["B相功率"] = 0;
                    dr["总功率因素"] = 0;
                    dr["当前有功功率"] = 0;
                    dt_rule_no.Rows.Add(dr);
                }
                var q_z_av = from order_a in dt_rule.AsEnumerable()
                             where order_a.Field<double>("a_v") == 0
                             group order_a by new
                             {
                                 cl1 = order_a.Field<string>("user_id")
                             } into g
                             select new
                             {
                                 order_userid = g.Key.cl1,
                                 order_acount = g.Count()
                             };
                foreach (var q_queshi in q_z_av)
                {
                    for (int i = 0; i < dt_rule_no.Rows.Count; i++)
                    {
                        if (dt_rule_no.Rows[i]["用户"].ToString() == q_queshi.order_userid)
                        {
                            dt_rule_no.Rows[i]["A相电压"] = ((double)q_queshi.order_acount / (double)(date_count * 96)).ToString("F2");
                        }
                    }

                }
                var q_z_bv = from order_a in dt_rule.AsEnumerable()
                             where order_a.Field<double>("b_v") == 0
                             group order_a by new
                             {
                                 cl1 = order_a.Field<string>("user_id")
                             } into g
                             select new
                             {
                                 order_userid = g.Key.cl1,
                                 order_acount = g.Count()
                             };
                foreach (var q_queshi in q_z_bv)
                {
                    for (int i = 0; i < dt_rule_no.Rows.Count; i++)
                    {
                        if (dt_rule_no.Rows[i]["用户"].ToString() == q_queshi.order_userid)
                        {
                            dt_rule_no.Rows[i]["B相电压"] = ((double)q_queshi.order_acount / (double)(date_count * 96)).ToString("F2");
                        }
                    }

                }
                var q_z_cv = from order_a in dt_rule.AsEnumerable()
                             where order_a.Field<double>("c_v") == 0
                             group order_a by new
                             {
                                 cl1 = order_a.Field<string>("user_id")
                             } into g
                             select new
                             {
                                 order_userid = g.Key.cl1,
                                 order_acount = g.Count()
                             };
                foreach (var q_queshi in q_z_cv)
                {
                    for (int i = 0; i < dt_rule_no.Rows.Count; i++)
                    {
                        if (dt_rule_no.Rows[i]["用户"].ToString() == q_queshi.order_userid)
                        {
                            dt_rule_no.Rows[i]["C相电压"] = ((double)q_queshi.order_acount / (double)(date_count * 96)).ToString("F2");
                        }
                    }

                }
                var q_z_ava = from order_a in dt_rule.AsEnumerable()
                              where order_a.Field<double>("a_v_a") == 0
                              group order_a by new
                              {
                                  cl1 = order_a.Field<string>("user_id")
                              } into g
                              select new
                              {
                                  order_userid = g.Key.cl1,
                                  order_acount = g.Count()
                              };
                foreach (var q_queshi in q_z_ava)
                {
                    for (int i = 0; i < dt_rule_no.Rows.Count; i++)
                    {
                        if (dt_rule_no.Rows[i]["用户"].ToString() == q_queshi.order_userid)
                        {
                            dt_rule_no.Rows[i]["A相电流"] = ((double)q_queshi.order_acount / (double)(date_count * 96)).ToString("F2");
                        }
                    }

                }
                var q_z_bvb = from order_a in dt_rule.AsEnumerable()
                              where order_a.Field<double>("b_v_b") == 0
                              group order_a by new
                              {
                                  cl1 = order_a.Field<string>("user_id")
                              } into g
                              select new
                              {
                                  order_userid = g.Key.cl1,
                                  order_acount = g.Count()
                              };
                foreach (var q_queshi in q_z_bvb)
                {
                    for (int i = 0; i < dt_rule_no.Rows.Count; i++)
                    {
                        if (dt_rule_no.Rows[i]["用户"].ToString() == q_queshi.order_userid)
                        {
                            dt_rule_no.Rows[i]["B相电流"] = ((double)q_queshi.order_acount / (double)(date_count * 96)).ToString("F2");
                        }
                    }

                }
                var q_z_cvc = from order_a in dt_rule.AsEnumerable()
                              where order_a.Field<double>("c_v_c") == 0
                              group order_a by new
                              {
                                  cl1 = order_a.Field<string>("user_id")
                              } into g
                              select new
                              {
                                  order_userid = g.Key.cl1,
                                  order_acount = g.Count()
                              };
                foreach (var q_queshi in q_z_cvc)
                {
                    for (int i = 0; i < dt_rule_no.Rows.Count; i++)
                    {
                        if (dt_rule_no.Rows[i]["用户"].ToString() == q_queshi.order_userid)
                        {
                            dt_rule_no.Rows[i]["C相电流"] = ((double)q_queshi.order_acount / (double)(date_count * 96)).ToString("F2");
                        }
                    }

                }
                var q_z_akw = from order_a in dt_rule.AsEnumerable()
                              where order_a.Field<double>("a_kw") == 0
                              group order_a by new
                              {
                                  cl1 = order_a.Field<string>("user_id")
                              } into g
                              select new
                              {
                                  order_userid = g.Key.cl1,
                                  order_acount = g.Count()
                              };
                foreach (var q_queshi in q_z_akw)
                {
                    for (int i = 0; i < dt_rule_no.Rows.Count; i++)
                    {
                        if (dt_rule_no.Rows[i]["用户"].ToString() == q_queshi.order_userid)
                        {
                            dt_rule_no.Rows[i]["A相功率"] = ((double)q_queshi.order_acount / (double)(date_count * 96)).ToString("F2");
                        }
                    }

                }
                var q_z_bkw = from order_a in dt_rule.AsEnumerable()
                              where order_a.Field<double>("b_kw") == 0
                              group order_a by new
                              {
                                  cl1 = order_a.Field<string>("user_id")
                              } into g
                              select new
                              {
                                  order_userid = g.Key.cl1,
                                  order_acount = g.Count()
                              };
                foreach (var q_queshi in q_z_bkw)
                {
                    for (int i = 0; i < dt_rule_no.Rows.Count; i++)
                    {
                        if (dt_rule_no.Rows[i]["用户"].ToString() == q_queshi.order_userid)
                        {
                            dt_rule_no.Rows[i]["B相功率"] = ((double)q_queshi.order_acount / (double)(date_count * 96)).ToString("F2");
                        }
                    }

                }
                var q_z_ckw = from order_a in dt_rule.AsEnumerable()
                              where order_a.Field<double>("c_kw") == 0
                              group order_a by new
                              {
                                  cl1 = order_a.Field<string>("user_id")
                              } into g
                              select new
                              {
                                  order_userid = g.Key.cl1,
                                  order_acount = g.Count()
                              };
                foreach (var q_queshi in q_z_ckw)
                {
                    for (int i = 0; i < dt_rule_no.Rows.Count; i++)
                    {
                        if (dt_rule_no.Rows[i]["用户"].ToString() == q_queshi.order_userid)
                        {
                            dt_rule_no.Rows[i]["C相功率"] = ((double)q_queshi.order_acount / (double)(date_count * 96)).ToString("F2");
                        }
                    }

                }
                var q_z_totalkw = from order_a in dt_rule.AsEnumerable()
                                  where order_a.Field<double>("total_kw") == 0
                                  group order_a by new
                                  {
                                      cl1 = order_a.Field<string>("user_id")
                                  } into g
                                  select new
                                  {
                                      order_userid = g.Key.cl1,
                                      order_acount = g.Count()
                                  };
                foreach (var q_queshi in q_z_totalkw)
                {
                    for (int i = 0; i < dt_rule_no.Rows.Count; i++)
                    {
                        if (dt_rule_no.Rows[i]["用户"].ToString() == q_queshi.order_userid)
                        {
                            dt_rule_no.Rows[i]["总功率因素"] = ((double)q_queshi.order_acount / (double)(date_count * 96)).ToString("F2");
                        }
                    }

                }
                var q_z_currkw = from order_a in dt_rule.AsEnumerable()
                                 where order_a.Field<double>("curr_kw") == 0
                                 group order_a by new
                                 {
                                     cl1 = order_a.Field<string>("user_id")
                                 } into g
                                 select new
                                 {
                                     order_userid = g.Key.cl1,
                                     order_acount = g.Count()
                                 };
                foreach (var q_queshi in q_z_currkw)
                {
                    for (int i = 0; i < dt_rule_no.Rows.Count; i++)
                    {
                        if (dt_rule_no.Rows[i]["用户"].ToString() == q_queshi.order_userid)
                        {
                            dt_rule_no.Rows[i]["当前有功功率"] = ((double)q_queshi.order_acount / (double)(date_count * 96)).ToString("F2");
                        }
                    }

                }
                dgv_fuzhu_static.DataSource = dt_rule_no;
                if (dgv_fuzhu_static.Columns.Count > 1)
                {
                    dgv_fuzhu_static.Columns[1].Frozen = true;
                }
                #endregion

                for (int i = 0; i < user_dt.Rows.Count; i++)
                {
                    var q = from q_ord in dt_rule_rc.AsEnumerable()
                            where q_ord.Field<string>("用户") == user_dt.Rows[i]["user_id"].ToString()
                            select q_ord.Field<string>("用户");
                    int ic = q.Count();
                    if (ic == 0)
                    {
                        DataRow dr = dt_rule_rc.NewRow();
                        dr["用户"] = user_dt.Rows[i]["user_id"].ToString();
                        dr["算法A"] = user_dt.Rows[i]["m1"].ToString();
                        dt_rule_rc.Rows.Add(dr);
                    }
                    var q1 = from q_ord in dt_rule_no.AsEnumerable()
                             where q_ord.Field<string>("用户") == user_dt.Rows[i]["user_id"].ToString()
                             select q_ord.Field<string>("用户");
                    int ic1 = q1.Count();
                    if (ic1 == 0)
                    {
                        DataRow dr = dt_rule_no.NewRow();
                        dr["用户"] = user_dt.Rows[i]["user_id"].ToString();
                        dt_rule_no.Rows.Add(dr);
                    }
                }


                for (int i = 0; i < user_dt.Rows.Count; i++)
                {

                    if (al.Contains(user_dt.Rows[i]["user_id"].ToString()))
                    {
                        user_dt.Rows[i]["rule"] = "2-异常";
                        if (user_dt.Rows[i]["is_show"].ToString() == "True")
                        {
                            user_dt.Rows[i]["is_show"] = "true";
                        }
                        else
                        {
                            user_dt.Rows[i]["is_show"] = "False";
                        }
                    }
                    else if (alnor.Contains(user_dt.Rows[i]["user_id"].ToString()))
                    {
                        user_dt.Rows[i]["rule"] = "0-正常";
                        user_dt.Rows[i]["is_show"] = "False";
                    }
                    else
                    {
                        user_dt.Rows[i]["rule"] = "1-无数据";
                        user_dt.Rows[i]["is_show"] = "False";
                    }


                }
                // this._shobt.Enabled = true;
                this.out_put.Enabled = true;
                this.btn_save_sx.Enabled = true;

                dt_rule_describe = dt_rule_distinct;
                fu_dt = dt_rule;
            }
            else
            {
                for (int i = 0; i < user_dt.Rows.Count; i++)
                {

                    user_dt.Rows[i]["rule"] = "1-无数据";


                }
                this.btn_save_sx.Enabled = true;
            }
            this.btn_save_sx.Enabled = true;
        }
        private DataTable rule_datatable()
        {

            DBHelper.DBiniClass iniclass = new DBHelper.DBiniClass(".\\system.ini");
            DataTable rule_dt = new DataTable();
            string linkmodel = iniclass.IniReadValue("netlink", "linkmodel");
            if (linkmodel == "sqlite")
            {
                string sql = "select * from v_ts_moniter t where t.line_no='" + line_lost + "' ";
                string time_sql = string.Empty;
                if (time_al.Count > 1)
                {
                    time_sql = " and ( ";
                }
                else
                {
                    time_sql = " and ";
                }
                for (int r = 0; r < time_al.Count; r++)
                {

                    if (r == 0)
                    {
                        time_sql = time_sql + " (date_time>='" + time_al[r].ToString().Split(';')[0] + "' and date_time<='" + time_al[r].ToString().Split(';')[1] + "')";
                    }

                    else
                    {
                        time_sql = time_sql + "  or (date_time>='" + time_al[r].ToString().Split(';')[0] + "' and date_time<='" + time_al[r].ToString().Split(';')[1] + "') ";
                    }
                }
                if (time_al.Count > 1)
                {
                    time_sql = time_sql + " ) ";
                }
                sql = sql + time_sql;
                rule_dt = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
            }
            else
            {
                rule_dt = gettHistoryValue();
                ArrayList al = new ArrayList();
                for (int i = 0; i < rule_dt.Rows.Count; i++)
                {
                    string sql = "insert into ts_user_moniter(user_id ,jl_point ,date_time ,A_V ,B_V ,C_V ,A_V_A ,B_V_B ,C_V_C ,A_BALANCE_NO,CURR_KW ,CURR_KVAR ,A_KW ,B_KW ,C_KW ,TOTAL_KW ,A_KW_S ,B_KW_S ,C_KW_S ,KW_BANLACE ,M_NO,line_no,m_zyb,f_zyb ) values ";
                    sql = sql + "('" + rule_dt.Rows[i]["user_id"].ToString() + "','" + rule_dt.Rows[i]["jl_point"].ToString() + "','" + rule_dt.Rows[i]["date_time"].ToString() + "','" + rule_dt.Rows[i]["A_V"].ToString() + "','" + rule_dt.Rows[i]["B_V "].ToString() + "','" + rule_dt.Rows[i]["C_V"].ToString() + "','" + rule_dt.Rows[i]["A_V_A"].ToString() + "','" + rule_dt.Rows[i]["B_V_B"].ToString() + "','" + rule_dt.Rows[i]["C_V_C"].ToString() + "','0','" + rule_dt.Rows[i]["CURR_KW"].ToString() + "','0','" + rule_dt.Rows[i]["A_KW"].ToString() + "','" + rule_dt.Rows[i]["B_KW"].ToString() + "','" + rule_dt.Rows[i]["C_KW"].ToString() + "','0.9','0','0','0','0','0','" + rule_dt.Rows[i]["line_no"].ToString() + "'，'" + rule_dt.Rows[i]["m_zyb"].ToString() + "','" + rule_dt.Rows[i]["f_zyb"].ToString() + "' )";
                    al.Add(sql);

                }
                DBHelper.DBHelper.ExecuteCommand(al);
            }
            return rule_dt;

        }
        private DataTable gettHistoryValue()
        {

            DataTable dt = new DataTable();
            string beg_time = "";
            string end_time = "";
            string Interval = "15m";
            for (int i = 0; i < time_al.Count; i++)
            {
                if (i == 0)
                {
                    beg_time = time_al[i].ToString().Split(';')[0];
                }
                if (i == time_al.Count - 1)
                {
                    end_time = time_al[i].ToString().Split(';')[1];
                }
            }
            // beg_time="2014-03-01T00:00:00";
            // end_time="2014-03-04T00:00:00";
            beg_time = beg_time + "T00:00:00";
            end_time = end_time + "T23:59:59";
            List<LostMinerLib.Util.HistoryValue> lh = new List<HistoryValue>();
           
            #region
            try
            {

                DataTable dt_moniter_c = new DataTable();
                dt_moniter_c.Columns.Add("user_id");
                dt_moniter_c.Columns.Add("cl_name");
                dt_moniter_c.Columns.Add("datatime");
                dt_moniter_c.Columns.Add("datavalue");
                DateTime dtb = System.DateTime.Now;
                string userids = "";


                for (int i = 0; i < user_dt.Rows.Count; i++)
                {
                    string webdtbeg = System.DateTime.Now.ToString();
                    try
                    {
                        string xmlstr = "<RequestHistoryValue><StartTime>" + beg_time + "</StartTime>" +
                                   "<EndTime>" + end_time + "</EndTime><Interval>900</Interval><PointNames>";
                        xmlstr = xmlstr + "<PointName>MV-B611-0-05-03-03-" + user_dt.Rows[i]["user_id"].ToString().Split('/')[0] + "</PointName>" +
                              "<PointName>MV-B612-0-05-03-03-" + user_dt.Rows[i]["user_id"].ToString().Split('/')[0] + "</PointName>" +
                               "<PointName>MV-B613-0-05-03-03-" + user_dt.Rows[i]["user_id"].ToString().Split('/')[0] + "</PointName>" +
                                "<PointName>MV-B621-0-05-03-03-" + user_dt.Rows[i]["user_id"].ToString().Split('/')[0] + "</PointName>" +
                                 "<PointName>MV-B622-0-05-03-03-" + user_dt.Rows[i]["user_id"].ToString().Split('/')[0] + "</PointName>" +
                                  "<PointName>MV-B623-0-05-03-03-" + user_dt.Rows[i]["user_id"].ToString().Split('/')[0] + "</PointName>" +
                                   "<PointName>MV-B630-0-05-03-03-" + user_dt.Rows[i]["user_id"].ToString().Split('/')[0] + "</PointName>" +
                                    "<PointName>MV-B631-0-05-03-03-" + user_dt.Rows[i]["user_id"].ToString().Split('/')[0] + "</PointName>" +
                                     "<PointName>MV-B632-0-05-03-03-" + user_dt.Rows[i]["user_id"].ToString().Split('/')[0] + "</PointName>" +
                                      "<PointName>MV-B633-0-05-03-03-" + user_dt.Rows[i]["user_id"].ToString().Split('/')[0] + "</PointName>";



                        userids = user_dt.Rows[i]["user_id"].ToString().Split('/')[0];
                        string xmlstr2 = "</PointNames></RequestHistoryValue>";
                        xmlstr = xmlstr + xmlstr2;

                        //FileStream myFs1 = new FileStream(userids+"moniterr.xml", FileMode.Create);

                        //StreamWriter mySw1 = new StreamWriter(myFs1, System.Text.Encoding.GetEncoding("gb2312"));
                        //mySw1.Write(xmlstr);
                        //mySw1.Close();
                        //myFs1.Close();

                        //moniterws.realdataqueryservice rl = new moniterws.realdataqueryservice();
                        //string xmlres = "<?xml version=\"1.0\" encoding=\"gb2312\"?>";

                       // string ddd = rl.QueryHistoryData(xmlstr);
                        string webdtend = System.DateTime.Now.ToString();
                       // xmlres = xmlres + ddd;
                        //FileStream myFs = new FileStream(userids + "moniter.xml", FileMode.Create);

                        //StreamWriter mySw = new StreamWriter(myFs, System.Text.Encoding.GetEncoding("gb2312"));
                        //mySw.Write(xmlres);
                        //mySw.Close();
                        //myFs.Close();



                        XmlDocument xd = new XmlDocument();
                        xd.Load(userids+"moniter.xml");
                        XmlNodeList nodelist = xd.SelectNodes("HistoryValueResponse");
                        foreach (XmlNode xmlnode in nodelist)
                        {
                            XmlNodeList nlhf = xmlnode.ChildNodes;
                            foreach (XmlNode nlh in nlhf) // HistoryValue
                            {
                                XmlNodeList nlp = nlh.ChildNodes;

                                string pointname = "";
                                for (int n = 0; n < nlp.Count; n++)
                                {
                                    if (n == 0)
                                    {
                                        pointname = nlp[n].InnerText;
                                    }
                                    else if (n >= 2)
                                    {
                                        XmlNodeList nlv = nlp[n].ChildNodes;
                                        DataRow dr = dt_moniter_c.NewRow();
                                        LostMinerLib.Util.HistoryValue hv = new HistoryValue();
                                        //dr["user_id"] = user_dt.Rows[i]["user_id"].ToString();

                                        dr["cl_name"] = conver_point(pointname);
                                        DataRow[] drs = user_dt.Select("user_id like '" + pointname.Split('-')[6] + "/%'");
                                        // dr["user_id"] = drs[0]["user_id"].ToString();
                                        hv.username = drs[0]["user_id"].ToString();
                                        hv.pointName = pointname;
                                        for (int nv = 0; nv < nlv.Count; nv++)
                                        {

                                            if (nv == 0)
                                            {
                                                //   dr["datavalue"] = nlv[nv].InnerText;
                                                hv.datavalue = nlv[nv].InnerText;
                                            }
                                            if (nv == 1)
                                            {
                                                //   dr["datatime"] = nlv[nv].InnerText; ;
                                                hv.datatime = DateTime.Parse(nlv[nv].InnerText).ToString("yyyy-MM-dd hh:mm:ss");
                                            }
                                        }
                                        lh.Add(hv);
                                        // dt_moniter_c.Rows.Add(dr);
                                    }
                                }
                            }
                        }
            #endregion
                       
                      
                        StreamReader sr = new StreamReader("webi.log", System.Text.Encoding.UTF8);
                        string txt = sr.ReadToEnd();
                        sr.Close();
                        StreamWriter sw = new StreamWriter("webi.log");

                        sw.WriteLine("用户" + user_dt.Rows[i]["user_id"].ToString() + "读取接口数据从:" + webdtbeg + " 至" + webdtend);
                       // sw.WriteLine("用户" + user_dt.Rows[i]["user_id"].ToString() + "数据转换从:" + datecbeg + "至" + datecend);
                        sw.WriteLine("-------------------");
                        sw.WriteLine(txt);
                        sw.Close();
                    }
                    catch (Exception er)
                    {
                        StreamReader sr = new StreamReader("webi.log", System.Text.Encoding.UTF8);
                        string txt = sr.ReadToEnd();
                        sr.Close();
                        string webdtend = System.DateTime.Now.ToString();
                        StreamWriter sw = new StreamWriter("webi.log");

                        sw.WriteLine("用户" + userids + "读取接口数据" + er.Message.ToString() + "时间从:" + webdtbeg + " 至" + webdtend);

                        sw.WriteLine("-------------------");
                        sw.WriteLine(txt);
                        sw.Close();
                    }
                    string datecbeg = System.DateTime.Now.ToString();
                    dt = convert_hv_dt(lh);
                    string datecend = System.DateTime.Now.ToString();
                    StreamReader sr1 = new StreamReader("webi.log", System.Text.Encoding.UTF8);
                    string txt1 = sr1.ReadToEnd();
                    sr1.Close();
                    StreamWriter sw1 = new StreamWriter("webi.log");

                  //  sw.WriteLine("用户" + user_dt.Rows[i]["user_id"].ToString() + "读取接口数据从:" + webdtbeg + " 至" + webdtend);
                    sw1.WriteLine("用户" + user_dt.Rows[i]["user_id"].ToString() + "数据转换从:" + datecbeg + "至" + datecend);
                    sw1.WriteLine("-------------------");
                    sw1.WriteLine(txt1);
                    sw1.Close();
                }

                DateTime dte = System.DateTime.Now;
                string timespan = dtb + "--" + dte;

            }
            catch (Exception er)
            {
                string webdtend = System.DateTime.Now.ToString();

                DBHelper.DBiniClass iniclass = new DBHelper.DBiniClass(".\\system.ini");

                iniclass.IniWriteValue("erromessate", "message3", er.Message.ToString());
                //StreamReader sr = new StreamReader("webi.log", System.Text.Encoding.UTF8);
                //string txt = sr.ReadToEnd();
                //sr.Close();
                //StreamWriter sw = new StreamWriter("webi.log");

                //sw.WriteLine("读取接口数据" + er.Message.ToString() + "时间从:" + webdtbeg + " 至" + webdtend);

                //sw.WriteLine("-------------------");
                //sw.WriteLine(txt);

                //// return_str = er.Message.ToString();
            }


            return dt;
        }
        private string conver_point(string pointname)
        {
            string str = "";
            if (pointname.Split('-')[1].ToString() == "B611") //A_V  
            {
                str = "a_v";
            }
            else if (pointname.Split('-')[1].ToString() == "B612")//B_V
            {
                str = "b_v";



            }
            else if (pointname.Split('-')[1].ToString() == "B613")//C_V  
            {
                str = "c_v";

            }
            else if (pointname.Split('-')[1].ToString() == "B621")//A_V_A 
            {
                str = "a_v_a";

            }
            else if (pointname.Split('-')[1].ToString() == "B622")//B_V_B
            {
                str = "b_v_b";

            }
            else if (pointname.Split('-')[1].ToString() == "B623")//C_V_C  
            {
                str = "c_v_c";

            }
            else if (pointname.Split('-')[1].ToString() == "B630")//CURR_KW
            {
                str = "curr_kw";

            }
            else if (pointname.Split('-')[1].ToString() == "B631")//A_KW 
            {
                str = "a_kw";

            }
            else if (pointname.Split('-')[1].ToString() == "B632")//B_KW  
            {
                str = "b_kw";

            }
            else if (pointname.Split('-')[1].ToString() == "B633")//C_KW 
            {
                str = "c_kw";

            }
            else if (pointname.Split('-')[1].ToString() == "m_zyb")//m_zyb 
            {
                str = "m_zyb";


            }
            else if (pointname.Split('-')[1].ToString() == "f_zyb")//f_zyb  
            {
                str = "f_zyb";
            }
            else
            { }
            return str;
        }
        private DataTable convert_hv_dt(List<LostMinerLib.Util.HistoryValue> lhvs)
        {
            DataTable dt_hv = new DataTable();
            dt_hv.Columns.Add("user_id");
            dt_hv.Columns.Add("date_time");
            dt_hv.Columns.Add("a_v");
            dt_hv.Columns.Add("b_v");
            dt_hv.Columns.Add("c_v");
            dt_hv.Columns.Add("a_v_a");
            dt_hv.Columns.Add("b_v_b");
            dt_hv.Columns.Add("c_v_c");
            dt_hv.Columns.Add("a_kw");
            dt_hv.Columns.Add("b_kw");
            dt_hv.Columns.Add("c_kw");
            dt_hv.Columns.Add("total_kw");
            dt_hv.Columns.Add("curr_kw");
            dt_hv.Columns.Add("m_zyb");
            dt_hv.Columns.Add("f_zyb");
            dt_hv.Columns.Add("line_no");
            dt_hv.Columns.Add("jl_point");
            var disnticthv = lhvs.AsEnumerable()
                            .Select(q_row => new
                            {
                                hv_datatime = q_row.datatime,
                                hv_username = q_row.username
                            }).Distinct();
            foreach (var hv in disnticthv)
            {

                var hvvalue = from lhv in lhvs.AsEnumerable()
                              where lhv.username == hv.hv_username && lhv.datatime == hv.hv_datatime
                              select lhv;

                foreach (var h in hvvalue)
                {
                    DataRow[] drs = dt_hv.Select("user_id='" + hv.hv_username + "' and date_time='" + hv.hv_datatime + "'");
                    DataRow dr;
                    if (drs.Length > 0)
                    {
                        dr = drs[0];

                    }
                    else
                    {
                        dr = dt_hv.NewRow();
                    }

                    dr["user_id"] = hv.hv_username;
                    dr["date_time"] = hv.hv_datatime;
                    dr["line_no"] = line_lost;
                    dr["jl_point"] = hv.hv_username;
                    if (h.pointName.Split('-')[1].ToString() == "B611") //A_V  
                    {
                        dr["a_v"] = h.datavalue;
                    }
                    else if (h.pointName.Split('-')[1].ToString() == "B612")//B_V
                    {

                        dr["b_v"] = h.datavalue;


                    }
                    else if (h.pointName.Split('-')[1].ToString() == "B613")//C_V  
                    {
                        dr["c_v"] = h.datavalue;

                    }
                    else if (h.pointName.Split('-')[1].ToString() == "B621")//A_V_A 
                    {
                        dr["a_v_a"] = h.datavalue;

                    }
                    else if (h.pointName.Split('-')[1].ToString() == "B622")//B_V_B
                    {
                        dr["b_v_b"] = h.datavalue;

                    }
                    else if (h.pointName.Split('-')[1].ToString() == "B623")//C_V_C  
                    {

                        dr["c_v_c"] = h.datavalue;

                    }
                    else if (h.pointName.Split('-')[1].ToString() == "B630")//CURR_KW
                    {
                        dr["curr_kw"] = h.datavalue;

                    }
                    else if (h.pointName.Split('-')[1].ToString() == "B631")//A_KW 
                    {
                        dr["a_kw"] = h.datavalue;

                    }
                    else if (h.pointName.Split('-')[1].ToString() == "B632")//B_KW  
                    {
                        dr["b_kw"] = h.datavalue;

                    }
                    else if (h.pointName.Split('-')[1].ToString() == "B633")//C_KW 
                    {
                        dr["c_kw"] = h.datavalue;
                    }
                    else if (h.pointName.Split('-')[1].ToString() == "total_kw")//total_kw
                    {
                        dr["total_kw"] = h.datavalue;

                    }
                    else if (h.pointName.Split('-')[1].ToString() == "m_zyb")//m_zyb 
                    {
                        dr["m_zyb"] = h.datavalue;

                    }
                    else if (h.pointName.Split('-')[1].ToString() == "f_zyb")//f_zyb  
                    {
                        dr["f_zyb"] = h.datavalue;

                    }
                    else
                    {
                        dr["m_zyb"] = 0;
                        dr["f_zyb"] = 0;
                        dr["total_kw"] = 0.9;
                    }
                    if (drs.Length == 0)
                    {
                        dt_hv.Rows.Add(dr);
                    }
                }


            }

            return dt_hv;
        }

        public object[] get_data_webservice(string method_name, object[] objs)
        {
            INIClass iniclass = new INIClass(Application.StartupPath + @"\system.ini");
            object[] dd = null;
            WebClient wc = new WebClient();
            string url_str = iniclass.IniReadValue("webservice", "webservice");
            Stream st = wc.OpenRead(url_str);
            ServiceDescription sd = ServiceDescription.Read(st);
            ServiceDescriptionImporter si = new ServiceDescriptionImporter();
            si.ProtocolName = "Soap";
            si.Style = ServiceDescriptionImportStyle.Client;
            si.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;
            si.AddServiceDescription(sd, null, null);

            CodeNamespace nmspace = new CodeNamespace();
            CodeCompileUnit unit = new CodeCompileUnit();
            unit.Namespaces.Add(nmspace);

            ServiceDescriptionImportWarnings warning = si.Import(nmspace, unit);
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameter = new CompilerParameters();
            parameter.GenerateExecutable = false;
            parameter.GenerateInMemory = true;
            parameter.ReferencedAssemblies.Add("System.dll");
            parameter.ReferencedAssemblies.Add("System.XML.dll");
            parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
            parameter.ReferencedAssemblies.Add("System.Data.dll");
            CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
            if (!result.Errors.HasErrors)
            {
                Assembly asm = result.CompiledAssembly;
                Type t = asm.GetType("IComplexUserServiceService");
                object o = Activator.CreateInstance(t);
                MethodInfo method = t.GetMethod(method_name);
                try
                {
                    //method.Invoke(o, new object[] { "" });

                    dd = (object[])method.Invoke(o, objs);

                }
                catch (Exception er)
                {
                    throw new Exception(er.Message.ToString());
                }
            }
            return dd;
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            LostMinerLib.Wizard wd = new LostMinerLib.Wizard();
            this.Hide();
            wd.Show();
            //   DBHelper.DBHelper.dbParaFactory = null;
        }

        private void TimeSearcherForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            Application.ExitThread();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否全部删除数据?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ArrayList al = new ArrayList();
                string sql = "delete from ts_user;";
                al.Add(sql);
                sql = "delete from ts_line;";
                al.Add(sql);
                sql = "delete from ts_user_moniter;";
                al.Add(sql);
                DBHelper.DBHelper.ExecuteCommand(al);
                MessageBox.Show("数据删除完成");
                reload();
            }
        }

        private void menuItem10_Click(object sender, EventArgs e)
        {
            this.bUpdating = true;
            if (Utils.SafeShowDialog(this._openTqdFileDlg) == DialogResult.OK)
            {
                INIClass iniclass = new INIClass(Application.StartupPath + @"\system.ini");
                string filename = this._openTqdFileDlg.FileNames[0].ToString();
                iniclass.IniWriteValue("file", "filepath", filename.Substring(0, filename.LastIndexOf(@"\")));
                this.task_str = "导入表码数据";
                this.worker.RunWorkerAsync();
                this.cmd.ShowOpaqueLayer(this, 0x7d, true);
            }
            this.bUpdating = false;

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }



        private void schedulerControl1_Click(object sender, EventArgs e)
        {
            AppointmentBaseCollection apl = ((SchedulerControl)sender).SelectedAppointments;
            if (apl.Count > 0)
            {
                Appointment a = apl[0];
                //string dd = a.Id.ToString().Split('@')[0];
                //fu_dt
                DataTable dt = fu_dt.Clone();
                DataRow[] drs = fu_dt.Select("user_id='" + a.Subject.ToString().Split('-')[0] + "' and date_time>='" + a.Start.ToString("yyyy-MM-dd") + "' and  date_time<='" + a.Start.AddDays(1).ToString("yyyy-MM-dd") + "'");
                for (int i = 0; i < drs.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int r = 0; r < fu_dt.Columns.Count; r++)
                    {

                        dr[r] = drs[i][r].ToString();

                    }
                    dt.Rows.Add(dr);
                }
                LostMinerLib.moniter mo = new moniter(dt);
                mo.ShowDialog();
            }

        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            this.bUpdating = true;
            if (Utils.SafeShowDialog(this._openTqdFileDlg) == DialogResult.OK)
            {
                INIClass iniclass = new INIClass(Application.StartupPath + @"\system.ini");
                string filename = this._openTqdFileDlg.FileNames[0].ToString();
                iniclass.IniWriteValue("file", "filepath", filename.Substring(0, filename.LastIndexOf(@"\")));
                this.task_str = "导入负表数据";
                this.worker.RunWorkerAsync();
                this.cmd.ShowOpaqueLayer(this, 0x7d, true);
            }
            this.bUpdating = false;
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            LostMinerLib.fm_rule fmr = new fm_rule();
            fmr.ShowDialog();

        }
        public void set_dt(DataTable dt)
        {
            dt_rule_source = dt;
        }

        private void dgvSelectAll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 9) && (e.RowIndex != -1))
            {
                if (!worker.IsBusy)
                {
                    #region
                    string user_id_str = this.dgvSelectAll.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                    if (fu_dt != null)
                    {
                        if (fu_dt.Rows.Count > 0)
                        {
                            DateTime mindate = System.DateTime.Now;
                            DataRow[] drs = dt_rule_describe.Select("user_id='" + user_id_str + "'");
                            dt_cl.Rows.Clear();
                            for (int i = 0; i < drs.Length; i++)
                            {
                                DataRow dr = dt_cl.NewRow();
                                if (mindate > DateTime.Parse(drs[i]["date"].ToString()))
                                {
                                    mindate = DateTime.Parse(drs[i]["date"].ToString());

                                }
                                dr["id"] = i;
                                dr["EventType"] = 0;
                                dr["StartDate"] = drs[i]["date"].ToString();
                                dr["EndDate"] = drs[i]["end_date"].ToString() + " 23:59:59";
                                dr["AllDay"] = true;
                                dr["Subject"] = drs[i]["user_id"].ToString() + "-" + drs[i]["rule"].ToString();
                                dr["Status"] = 0;
                                dr["Reminder"] = drs[i]["user_id"].ToString();
                                if (drs[i]["rule"].ToString() == "失压")
                                {
                                    dr["Label"] = "1";
                                }
                                else if (drs[i]["rule"].ToString() == "反极性")
                                {
                                    dr["Label"] = "2";
                                }
                                else if (drs[i]["rule"].ToString() == "功率异常")
                                {
                                    dr["Label"] = "3";
                                }
                                else if (drs[i]["rule"].ToString() == "功率因素异常")
                                {
                                    dr["Label"] = "4";
                                }
                                else if (drs[i]["rule"].ToString() == "失流")
                                {
                                    dr["Label"] = "5";
                                }
                                else if (drs[i]["rule"].ToString() == "电流不平衡")
                                {
                                    dr["Label"] = "6";
                                }
                                else if (drs[i]["rule"].ToString() == "不完全星形接线中线中断")
                                {
                                    dr["Label"] = "7";
                                }
                                else if (drs[i]["rule"].ToString() == "电量差异常")
                                {
                                    dr["Label"] = "8";
                                }
                                else
                                {
                                    dr["Label"] = "9";
                                }
                                dt_cl.Rows.Add(dr);
                            }
                            if (dgv_fuzhu_static.Rows.Count > 1)
                            {

                                dgv_fuzhu_static.ClearSelection();

                                for (int i = 0; i < dgv_fuzhu_static.Rows.Count; i++)
                                {
                                    string dgv_user_id = dgv_fuzhu_static.Rows[i].Cells[0].FormattedValue.ToString();
                                    if (dgv_user_id == user_id_str)
                                    {
                                        dgv_fuzhu_static.Rows[i].Selected = true;
                                        dgv_fuzhu_static.CurrentCell = dgv_fuzhu_static.Rows[i].Cells[0];

                                    }

                                }

                            }
                            if (dgv_rule_p.Rows.Count > 1)
                            {
                                dgv_rule_p.ClearSelection();

                                for (int i = 0; i < dgv_rule_p.Rows.Count; i++)
                                {
                                    string dgv_user_id = dgv_rule_p.Rows[i].Cells[0].FormattedValue.ToString();
                                    if (dgv_user_id == user_id_str)
                                    {
                                        dgv_rule_p.Rows[i].Selected = true;
                                        dgv_rule_p.CurrentCell = dgv_rule_p.Rows[i].Cells[0];

                                    }

                                }

                            }



                            schedulerStorage1.Appointments.DataSource = dt_cl;
                            schedulerControl1.Start = mindate;
                            this._mainTabControl.SelectedTab = _tab_fuzhu;
                        }
                    }
                    #endregion
                }
            }

        }

        private void dgv_fuzhu_static_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 0)
            {

                string user_id_str = this.dgv_fuzhu_static.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();


                if (dgv_fuzhu_static.Rows.Count > 1)
                {

                    dgvSelectAll.ClearSelection();

                    for (int i = 0; i < dgvSelectAll.Rows.Count; i++)
                    {
                        string dgv_user_id = dgvSelectAll.Rows[i].Cells[3].FormattedValue.ToString();
                        if (dgv_user_id == user_id_str)
                        {
                            dgvSelectAll.Rows[i].Selected = true;
                            dgvSelectAll.CurrentCell = dgvSelectAll.Rows[i].Cells[0];

                        }

                    }
                    for (int i = 0; i < dgv_rule_p.Rows.Count; i++)
                    {
                        string dgv_user_id = dgv_rule_p.Rows[i].Cells[0].FormattedValue.ToString();
                        if (dgv_user_id == user_id_str)
                        {
                            dgv_rule_p.Rows[i].Selected = true;
                            dgv_rule_p.CurrentCell = dgv_rule_p.Rows[i].Cells[0];

                        }

                    }

                }


            }
        }
        public DataTable getdgvdt()
        {
            return (DataTable)dgvSelectAll.DataSource;
        }
        private void dgv_rule_p_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 0)
            {

                string user_id_str = this.dgv_rule_p.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                if (dgv_rule_p.Rows.Count > 1)
                {

                    dgvSelectAll.ClearSelection();
                    dgv_rule_p.ClearSelection();

                    for (int i = 0; i < dgvSelectAll.Rows.Count; i++)
                    {
                        string dgv_user_id = dgvSelectAll.Rows[i].Cells[3].FormattedValue.ToString();
                        if (dgv_user_id == user_id_str)
                        {
                            dgvSelectAll.Rows[i].Selected = true;
                            dgvSelectAll.CurrentCell = dgvSelectAll.Rows[i].Cells[0];

                        }

                    }
                    for (int i = 0; i < dgv_fuzhu_static.Rows.Count; i++)
                    {
                        string dgv_user_id = dgv_fuzhu_static.Rows[i].Cells[0].FormattedValue.ToString();
                        if (dgv_user_id == user_id_str)
                        {
                            dgv_fuzhu_static.Rows[i].Selected = true;
                            dgv_fuzhu_static.CurrentCell = dgv_fuzhu_static.Rows[i].Cells[0];

                        }

                    }

                }



            }
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            LostMinerLib.task_manager tskm = new task_manager(this);
            tskm.ShowDialog();
        }

        private void menuItem5_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dgv_rule_p_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 0)
            {
                string time_sql = string.Empty;
                if (time_al.Count > 1)
                {
                    time_sql = " and ( ";
                }
                else
                {
                    time_sql = " and ";
                }
                for (int r = 0; r < time_al.Count; r++)
                {

                    if (r == 0)
                    {
                        time_sql = time_sql + " (date_time>='" + time_al[r].ToString().Split(';')[0] + "' and date_time<='" + time_al[r].ToString().Split(';')[1] + "')";
                    }
                    else
                    {
                        time_sql = time_sql + "  or (date_time>='" + time_al[r].ToString().Split(';')[0] + "' and date_time<='" + time_al[r].ToString().Split(';')[1] + "') ";
                    }
                }
                if (time_al.Count > 1)
                {
                    time_sql = time_sql + " ) ";
                }


                string user_id_str = this.dgv_rule_p.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                DataTable dt = fu_dt.Clone();
                DataRow[] drs = fu_dt.Select("user_id='" + user_id_str + "'  " + time_sql + "");
                for (int i = 0; i < drs.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int r = 0; r < fu_dt.Columns.Count; r++)
                    {

                        dr[r] = drs[i][r].ToString();

                    }
                    dt.Rows.Add(dr);
                }
                LostMinerLib.moniter mo = new moniter(dt);
                mo.ShowDialog();
            }
        }

        private void dgv_fuzhu_static_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 0)
            {
                string time_sql = string.Empty;
                if (time_al.Count > 1)
                {
                    time_sql = " and ( ";
                }
                else
                {
                    time_sql = " and ";
                }
                for (int r = 0; r < time_al.Count; r++)
                {

                    if (r == 0)
                    {
                        time_sql = time_sql + " (date_time>='" + time_al[r].ToString().Split(';')[0] + "' and date_time<='" + time_al[r].ToString().Split(';')[1] + "')";
                    }
                    else
                    {
                        time_sql = time_sql + "  or (date_time>='" + time_al[r].ToString().Split(';')[0] + "' and date_time<='" + time_al[r].ToString().Split(';')[1] + "') ";
                    }
                }

                string user_id_str = this.dgv_fuzhu_static.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                DataTable dt = fu_dt.Clone();
                DataRow[] drs = fu_dt.Select("user_id='" + user_id_str + "'  " + time_sql + "");
                for (int i = 0; i < drs.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int r = 0; r < fu_dt.Columns.Count; r++)
                    {

                        dr[r] = drs[i][r].ToString();

                    }
                    dt.Rows.Add(dr);
                }


                if (dgv_fuzhu_static.Rows.Count > 1)
                {

                    dgvSelectAll.ClearSelection();

                    for (int i = 0; i < dgvSelectAll.Rows.Count; i++)
                    {
                        string dgv_user_id = dgvSelectAll.Rows[i].Cells[3].FormattedValue.ToString();
                        if (dgv_user_id == user_id_str)
                        {
                            dgvSelectAll.Rows[i].Selected = true;
                            dgvSelectAll.CurrentCell = dgvSelectAll.Rows[i].Cells[0];

                        }

                    }
                    for (int i = 0; i < dgv_rule_p.Rows.Count; i++)
                    {
                        string dgv_user_id = dgv_rule_p.Rows[i].Cells[0].FormattedValue.ToString();
                        if (dgv_user_id == user_id_str)
                        {
                            dgv_rule_p.Rows[i].Selected = true;
                            dgv_rule_p.CurrentCell = dgv_rule_p.Rows[i].Cells[0];

                        }

                    }

                }

                LostMinerLib.moniter mo = new moniter(dt);
                mo.ShowDialog();
            }
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            //LostMinerLib.DataBrowse db = new DataBrowse();
            //db.ShowDialog();
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = Application.StartupPath + "\\深圳供电局窃电漏计预警系统V2.0用户手册.doc";
            p.Start();
        }

        private void menuItem16_Click(object sender, EventArgs e)
        {
            DBHelper.DBiniClass iniclass = new DBHelper.DBiniClass(".\\system.ini");

            iniclass.IniWriteValue("netlink", "linkmodel", "oracle");
            LostMinerLib.NetLine nl = new NetLine();
            this.Hide();
            nl.ShowDialog();
        }
        private ManualResetEvent manulrest = new ManualResetEvent(true);
        private void button1_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy)
            {

                //worker.WorkerSupportsCancellation = true;
                worker.CancelAsync();
                // worker.Dispose();
                //worker = null;
                // worker = new BackgroundWorker();
                this.cmd.HideOpaqueLayer();
            }
        }

        private void menuItem17_Click(object sender, EventArgs e)
        {
            LostMinerLib.DataBrowse db = new DataBrowse();
            db.ShowDialog();
        }

        private void menuItem18_Click(object sender, EventArgs e)
        {
            LostMinerLib.task_look db = new task_look();
            db.ShowDialog();
        }

    }
}

