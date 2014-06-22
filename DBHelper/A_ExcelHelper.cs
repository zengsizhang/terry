using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop;
using System.Collections;
using System.Windows.Forms;
using System.Data;
namespace DBHelper
{
    public class A_ExcelHelper
    {


        private Microsoft.Office.Interop.Excel._Application excelApp;
        private string fileName = string.Empty;
        private Microsoft.Office.Interop.Excel.WorkbookClass wbclass;
        private bool _islarge;
        public A_ExcelHelper(string _filename, bool is_large)
        {
            try
            {
                _islarge = is_large;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                object objOpt = System.Reflection.Missing.Value;
                fileName = _filename;
                wbclass = (Microsoft.Office.Interop.Excel.WorkbookClass)excelApp.Workbooks.Open(_filename, objOpt, false, objOpt, objOpt, objOpt, true, objOpt, objOpt, true, objOpt, objOpt, objOpt, objOpt, objOpt);
            }
            catch (Exception er)
            {
                DBiniClass iniclass = new DBiniClass(".\\system.ini");

                iniclass.IniWriteValue("erromessate", "message1", er.ToString());

            }
        }
        public A_ExcelHelper(string _filename)
        {
            fileName = _filename;
        }
        public A_ExcelHelper()
        {

        }
        /**/
        /// <summary>
        /// 所有sheet的名称列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetSheetNames()
        {
            List<string> list = new List<string>();
            Microsoft.Office.Interop.Excel.Sheets sheets = wbclass.Worksheets;
            string sheetNams = string.Empty;
            foreach (Microsoft.Office.Interop.Excel.Worksheet sheet in sheets)
            {
                list.Add(sheet.Name);
            }
            return list;
        }

        public Microsoft.Office.Interop.Excel.Worksheet GetWorksheetByName(string name)
        {
            Microsoft.Office.Interop.Excel.Worksheet sheet = null;
            Microsoft.Office.Interop.Excel.Sheets sheets = wbclass.Worksheets;
            foreach (Microsoft.Office.Interop.Excel.Worksheet s in sheets)
            {
                if (s.Name == name)
                {
                    sheet = s;
                    break;
                }
            }
            return sheet;
        }
        /**/
        /// <summary>
        /// 读取线损数据和用户用电数据
        /// </summary>
        /// <param name="sheetName">sheet名称</param>
        /// <returns></returns>
        public string GetContent()
        {
            string return_str = string.Empty;
            try
            {
                List<string> sheetNames = GetSheetNames();
                Microsoft.Office.Interop.Excel.Worksheet sheet = GetWorksheetByName(sheetNames[0].ToString());
                //获取A1 到AM24范围的单元格
                int c_count = sheet.UsedRange.Columns.Count;
                int r_count = sheet.UsedRange.Rows.Count;
                object c = sheet.UsedRange.Rows.get_Item(1, Type.Missing);
                Microsoft.Office.Interop.Excel.Range rang = sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[r_count, c_count]);

                //读一个单元格内容
                //sheet.get_Range("A1", Type.Missing);
                //不为空的区域,列,行数目
                //   int l = sheet.UsedRange.Columns.Count;
                // int w = sheet.UsedRange.Rows.Count;
                //  object[,] dell = sheet.UsedRange.get_Value(Missing.Value) as object[,];
                int dd = 41452;
                //  DateTime dtt = DateTime.Parse(dd.ToString());
                object[,] strs = (object[,])rang.Cells.Value2;
                //string _value=  strs[1,2].ToString();
                string[] year_strs = sheetNames[0].ToString().Split('日');
                string year = year_strs[1].Substring(1, 2);
                int int_year = 2000;
                try
                {
                    int_year = 2000 + int.Parse(year);
                }
                catch
                {
                    int_year = System.DateTime.Now.Year;
                }
                //_tsform.get_curr_begin_time().ToString("yyyy-MM-dd")
                string line_no;
                string date_time;
                string TOTAL_Q;
                string USER_Q;
                string lost;
                string rate;
                string ct;
                string min_date = string.Empty;
                string max_date = string.Empty;
                string USER_ID;
        

                line_no = strs[2, 1].ToString();
                ct = strs[3, 3].ToString();
                ArrayList al = new ArrayList();
                if (!_islarge)
                {
                    DBHelper.ExecuteCommand("delete from ts_int_line");
                    DBHelper.ExecuteCommand("delete from ts_int_user");
                    DBHelper.ExecuteCommand("delete from ts_import_record");
                }
                // DBHelper.ExecuteCommand("delete from ts_int_line"");
                for (int r = 4; r <= c_count; r++)
                {
                    if (r >4)
                    {
                        if (strs[1, r - 1].ToString().Split('月')[0] + "-" + strs[1, r - 1].ToString().Split('月')[1].Split('日')[0] == "12-31")
                        {
                            int_year += 1;
                        }
                    }
                    TOTAL_Q = strs[3, r].ToString();
                    USER_Q = strs[2, r].ToString();
                    lost = strs[4, r].ToString();
                    if (TOTAL_Q.Trim() != "" && USER_Q.Trim() != "")
                    {
                        double rate_d = (double.Parse(lost) / double.Parse(TOTAL_Q)) * 100;
                        rate = rate_d.ToString("F2");
                        if (double.Parse(TOTAL_Q) == 0)
                        {
                            rate = "0";
                        }
                        try
                        {
                            string dd_d = strs[1, r].ToString().Split('月')[0];
                            date_time = int_year.ToString() + "-" + strs[1, r].ToString().Split('月')[0] + "-" + strs[1, r].ToString().Split('月')[1].Split('日')[0];
                        }
                        catch
                        {
                            string dd_d = strs[1, r].ToString();
                            date_time = DateTime.FromOADate(double.Parse(dd_d)).ToString("yyyy-MM-dd");
                        }
                        if (r == 4)
                        {
                            min_date = date_time;
                        }
                        if (r == c_count)
                        {
                            max_date = date_time;
                        }
                        string sql = "insert into ts_int_line (LINE_NO,DATE_TIME ,TOTAL_Q,USER_Q ,lost ,rate,ct) values" +
                                      "('" + line_no + "','" + date_time + "','" + TOTAL_Q + "','" + USER_Q + "','" + lost + "','" + rate + "','" + ct + "')";

                        string dddd = strs[1, r].ToString().Split('月')[0] + "-" + strs[1, r].ToString().Split('月')[1].Split('日')[0];
                       
                        al.Add(sql);
                        
                    }
                }

               
                bool b = true;
                ArrayList al_user_id = new ArrayList();
                Dictionary<string, int> user_d = new Dictionary<string, int>();
                for (int i = 5; i <= r_count; i++)
                {
                    int rn = 0;

                    USER_ID = strs[i, 1].ToString();
                    if (USER_ID.Trim() == "")
                    {

                    }
                    else
                    {
                        if (user_d.ContainsKey(USER_ID))
                        {

                            USER_ID = USER_ID + "_" + (user_d[USER_ID] + 1);
                            user_d[USER_ID.Split('_')[0]] = user_d[USER_ID.Split('_')[0]] + 1;
                            //al_user_id.Add(USER_ID);
                            string sql_record = "insert into ts_import_record(user_id,user_id_change,ct,excel_row,line_no) values ('" + USER_ID.Split('_')[0] + "','" + USER_ID + "','" + ct + "','" + i.ToString() + "','" + line_no + "')";
                            al.Add(sql_record);
                        }
                        else
                        {

                            user_d.Add(USER_ID, 0);
                        }

                        bool b1 = false;
                        // USER_ID = strs[i, 2].ToString();
                        ct = strs[i, 3].ToString();
                        try
                        {
                            int_year = 2000 + int.Parse(year);
                        }
                        catch
                        {
                            int_year = System.DateTime.Now.Year;
                        }
                        for (int r = 4; r <= c_count; r++)
                        {
                           
                            if (r > 4)
                            {
                                if (strs[1, r - 1].ToString().Split('月')[0] + "-" + strs[1, r - 1].ToString().Split('月')[1].Split('日')[0] == "12-31")
                                {
                                    int_year += 1;
                                }
                            }
                            
                            USER_Q = strs[i, r].ToString();
                            // date_time = "2013-" + strs[1, r].ToString().Split('月')[0] + "-" + strs[1, r].ToString().Split('月')[1].Split('日')[0];
                            try
                            {
                                string dd_d = strs[1, r].ToString().Split('月')[0];
                                date_time = int_year.ToString() + "-" + strs[1, r].ToString().Split('月')[0] + "-" + strs[1, r].ToString().Split('月')[1].Split('日')[0];
                            }
                            catch
                            {
                                string dd_d = strs[1, r].ToString();
                                date_time = DateTime.FromOADate(double.Parse(dd_d)).ToString("yyyy-MM-dd");
                            }
                            string sql = "insert into ts_int_user (user_id,DATE_TIME ,USED_Q ,line_no,ct) values" +
                                   "('" + USER_ID + "','" + date_time + "','" + USER_Q + "','" + line_no + "','" + ct + "')";

                            
                            al.Add(sql);

                        }
                    }
                }
                al.Add("insert into ts_line_record(user_id,user_id_change,ct,excel_row,line_no,max_time,min_time,is_check,import_time) values ('导入用户：" + (r_count - 5).ToString() + "个','线路:" + line_no + "','导入时间段为" + min_date + "至" + max_date + "','0','" + line_no + "','" + max_date + "','" + min_date + "','未检查','" + System.DateTime.Now.ToString() + "')");
                try
                {
                    DBHelper.ExecuteCommand(al);
                    return_str = "导入成功";
                }
                catch (Exception er)
                {
                    DBiniClass iniclass = new DBiniClass(".\\system.ini");

                    iniclass.IniWriteValue("erromessate", "message3", er.ToString());
                    return_str = er.Message.ToString();
                }
            }
            catch (Exception er)
            {
                DBiniClass iniclass = new DBiniClass(".\\system.ini");

                iniclass.IniWriteValue("erromessate", "message3", er.ToString());
                return_str = er.Message.ToString();
            }
            excelApp.Quit();
            excelApp = null;
            if (excelApp != null)
            {
                excelApp.Workbooks.Close();
                excelApp.Quit();
                int generation = GC.GetGeneration(excelApp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                excelApp = null;
                GC.Collect(generation);
            }
            GC.Collect(); //强行销毁
            System.Diagnostics.Process[] excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            DateTime startTime = new DateTime();
            int m, killId = 0;
            for (m = 0; m < excelProc.Length; m++)
            {
                if (startTime < excelProc[m].StartTime)
                {
                    startTime = excelProc[m].StartTime;
                    killId = m;
                }
            }
            if (excelProc[killId].HasExited == false)
            {
                excelProc[killId].Kill();
            }
            return return_str;
        }

        /**/
        /// <summary>
        /// 读取用户功率数据
        /// </summary>
        /// <param name="sheetName">sheet名称</param>
        /// <returns></returns>

        public string GetKw()
        {
            string return_str = "";

            try
            {
                //    List<string> sheetNames = GetSheetNames();
                string dt1 = System.DateTime.Now.ToString();
                MyExcelReader myex = new MyExcelReader(fileName, "Export Workbook");
                System.Data.DataSet dss = myex.DataSet;
                string dt2 = System.DateTime.Now.ToString();
                //Microsoft.Office.Interop.Excel.Worksheet sheet = GetWorksheetByName(sheetNames[0].ToString());                
                //int c_count = sheet.UsedRange.Columns.Count;
                //int r_count = sheet.UsedRange.Rows.Count;
                //object c = sheet.UsedRange.Rows.get_Item(1, Type.Missing);
                //Microsoft.Office.Interop.Excel.Range rang = sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[r_count, c_count]);
                //object[,] strs = (object[,])rang.Cells.Value2;


                string user_id = string.Empty;
                string jl_point = string.Empty;
                string date_time = string.Empty;
                string A_V = string.Empty;
                string B_V = string.Empty;
                string C_V = string.Empty;
                string A_V_A = string.Empty;
                string B_V_B = string.Empty;
                string C_V_C = string.Empty;
                string A_BALANCE_NO = string.Empty;
                string CURR_KW = string.Empty;
                string CURR_KVAR = string.Empty;
                string A_KW = string.Empty;
                string B_KW = string.Empty;
                string C_KW = string.Empty;
                string TOTAL_KW = string.Empty;
                string A_KW_S = string.Empty;
                string B_KW_S = string.Empty;
                string C_KW_S = string.Empty;
                string KW_BANLACE = string.Empty;
                string M_NO = string.Empty;
                string line_no = string.Empty;
                ArrayList al = new ArrayList();
                if (dss.Tables[0].Rows.Count > 0 && dss.Tables[0].Columns.Count>23)
                {
                    //var q_max = dss.Tables[0].AsEnumerable()
                    //           .Select(q_row => new
                    //           {
                    //               q_date = q_row.Field<string>("date")
                    //           }).Max();
                    //var q_min = dss.Tables[0].AsEnumerable()
                    //           .Select(q_row => new
                    //           {
                    //               q_date = q_row.Field<string>("date")
                    //           }).Min();

                    user_id = dss.Tables[0].Rows[0][1].ToString();
                    string date_max = dss.Tables[0].Rows[0][4].ToString();
                    string date_min = dss.Tables[0].Rows[dss.Tables[0].Rows.Count-1][4].ToString();
                    if (dss.Tables[0].Rows.Count >= 24)
                    {
                        line_no = dss.Tables[0].Rows[0][24].ToString();
                    }
                    else
                    {
                        //   line_no = dss.Tables[0].Rows[i][23].ToString();
                        line_no = "F04新一线";
                    }
                    al.Add("delete from ts_user_moniter where user_id='" + user_id + "' and date_time>='" + date_max + "' and date_time<='" + date_min + "' and line_no='" + line_no + "'");

                    for (int i = 1; i < dss.Tables[0].Rows.Count; i++)
                    {
                        user_id = dss.Tables[0].Rows[i][1].ToString();
                        jl_point = dss.Tables[0].Rows[i][3].ToString();
                        date_time = dss.Tables[0].Rows[i][4].ToString();
                        A_V = dss.Tables[0].Rows[i][5].ToString();
                        B_V = dss.Tables[0].Rows[i][6].ToString();
                        C_V = dss.Tables[0].Rows[i][7].ToString();
                        A_V_A = dss.Tables[0].Rows[i][8].ToString();
                        B_V_B = dss.Tables[0].Rows[i][9].ToString();
                        C_V_C = dss.Tables[0].Rows[i][10].ToString();
                        A_BALANCE_NO = "0";
                    //    A_BALANCE_NO = dss.Tables[0].Rows[i][11].ToString();
                        CURR_KW = dss.Tables[0].Rows[i][12].ToString();
                        CURR_KVAR = dss.Tables[0].Rows[i][13].ToString();
                        A_KW = dss.Tables[0].Rows[i][14].ToString();
                        B_KW = dss.Tables[0].Rows[i][15].ToString();
                        C_KW = dss.Tables[0].Rows[i][16].ToString();
                        TOTAL_KW = dss.Tables[0].Rows[i][17].ToString();
                        A_KW_S = dss.Tables[0].Rows[i][18].ToString();
                        B_KW_S = dss.Tables[0].Rows[i][19].ToString();
                        C_KW_S = dss.Tables[0].Rows[i][20].ToString();
                        KW_BANLACE = "0";
                     //   KW_BANLACE = dss.Tables[0].Rows[i][21].ToString();
                        M_NO = dss.Tables[0].Rows[i][23].ToString();
                     
                        //  al.Add("delete from ts_user_moniter where user_id ='" + user_id + "' and date_time='" + date_time + "' ");
                        string sql = "insert into ts_user_moniter(user_id ,jl_point ,date_time ,A_V ,B_V ,C_V ,A_V_A ,B_V_B ,C_V_C ,A_BALANCE_NO,CURR_KW ,CURR_KVAR ,A_KW ,B_KW ,C_KW ,TOTAL_KW ,A_KW_S ,B_KW_S ,C_KW_S ,KW_BANLACE ,M_NO,line_no ) values ";
                        sql = sql + "('" + user_id + "','" + jl_point + "','" + date_time + "','" + A_V + "','" + B_V + "','" + C_V + "','" + A_V_A + "','" + B_V_B + "','" + C_V_C + "','" + A_BALANCE_NO + "','" + CURR_KW + "','" + CURR_KVAR + "','" + A_KW + "','" + B_KW + "','" + C_KW + "','" + TOTAL_KW + "','" + A_KW_S + "','" + B_KW_S + "','" + C_KW_S + "','" + KW_BANLACE + "','" + M_NO + "','" + line_no + "' )";
                        al.Add(sql);

                    }
                    string dt3 = System.DateTime.Now.ToString();
                    DBHelper.ExecuteCommand(al);
                }
            }
            catch
            {

            }

            return return_str;
        }

        /// <summary>
        /// 导入码表数据
        /// </summary>
        /// 
        public string GetMFkw(bool main_b)
        {
            string return_str = "";

            try
            {
                //    List<string> sheetNames = GetSheetNames();
                string dt1 = System.DateTime.Now.ToString();
                MyExcelReader myex = new MyExcelReader(fileName, "Export Workbook");
                System.Data.DataSet dss = myex.DataSet;
                string dt2 = System.DateTime.Now.ToString();
                string user_id = string.Empty;
                string M_NO = string.Empty;
                string date_time = string.Empty;
                string M_ZYB = string.Empty;
                string M_FYB = string.Empty;
                string M_ZWB = string.Empty;
                string M_FWB = string.Empty;
                string F_ZYB = string.Empty;
                string F_FYB = string.Empty;
                string F_ZWB = string.Empty;
                string F_FWB = string.Empty;
                string LINE_NO = string.Empty;
                ArrayList al = new ArrayList();
                if (dss.Tables[0].Rows.Count > 0)
                {
                    string date_max = dss.Tables[0].Rows[0][3].ToString();
                    string date_min = dss.Tables[0].Rows[dss.Tables[0].Rows.Count - 1][3].ToString();
                    LINE_NO = dss.Tables[0].Rows[0][13].ToString();
                   // line_no = "F02岗共二线";
                   // al.Add("delete from ts_user_moniter where user_id='" + date_min + "' and date_time>='" + date_max + "' and date_time<='" + date_min + "' and line_no='" + line_no + "'");


                    if (main_b)
                    {
                        al.Add("delete from ts_user_kw_main where user_id='" + dss.Tables[0].Rows[0][1].ToString() + "' and date_time>='" + date_max + "' and date_time<='" + date_min + "' and line_no='" + LINE_NO + "'");
                    }
                    else
                    {
                        al.Add("delete from ts_user_kw where user_id='" + dss.Tables[0].Rows[0][1].ToString() + "' and date_time>='" + date_max + "' and date_time<='" + date_min + "' and line_no='" + LINE_NO + "'");

                    }
                    for (int i = 1; i < dss.Tables[0].Rows.Count; i++)
                    {
                        user_id = dss.Tables[0].Rows[i][1].ToString();
                        M_NO = dss.Tables[0].Rows[i][4].ToString();
                        date_time = dss.Tables[0].Rows[i][3].ToString();
                        M_ZYB = dss.Tables[0].Rows[i][7].ToString();
                        M_FYB = dss.Tables[0].Rows[i][8].ToString();
                        M_ZWB = dss.Tables[0].Rows[i][9].ToString();
                        M_FWB = dss.Tables[0].Rows[i][10].ToString();
                        //F_ZYB = dss.Tables[0].Rows[i][7].ToString();
                        //F_ZWB = dss.Tables[0].Rows[i][8].ToString();
                        //F_FYB = dss.Tables[0].Rows[i][9].ToString();
                        //F_FWB = dss.Tables[0].Rows[i][10].ToString();
                        LINE_NO = dss.Tables[0].Rows[i][13].ToString();
                        if (main_b)
                        {
                            //  al.Add("delete from ts_user_moniter where user_id ='" + user_id + "' and date_time='" + date_time + "' ");
                            string sql = "insert into ts_user_kw_main(user_id ,date_time,M_NO ,M_ZYB ,M_FYB ,M_ZWB ,M_FWB,LINE_NO ) values ";
                            sql = sql + "('" + user_id + "','" + DateTime.Parse(date_time).ToString("yyyy-MM-dd HH:mm:ss") + "','" + M_NO + "' ,'" + M_ZYB + "' ,'" + M_FYB + "' ,'" + M_ZWB + "' ,'" + M_FWB + "' ,'" + LINE_NO + "')";
                            al.Add(sql);
                        }
                        else
                        {
                            string sql = "insert into ts_user_kw(user_id ,date_time,M_NO ,M_ZYB ,M_FYB ,M_ZWB ,M_FWB,LINE_NO ) values ";
                            sql = sql + "('" + user_id + "','" + DateTime.Parse(date_time).ToString("yyyy-MM-dd HH:mm:ss") + "','" + M_NO + "' ,'" + M_ZYB + "' ,'" + M_FYB + "' ,'" + M_ZWB + "' ,'" + M_FWB + "' ,'" + LINE_NO + "')";
                            al.Add(sql);
                        }

                    }
                    string dt3 = System.DateTime.Now.ToString();
                    DBHelper.ExecuteCommand(al);
                }
            }
            catch (Exception er)
            {

            }

            return return_str;
        }
        public void Close()
        {
            excelApp.Quit();
            excelApp = null;
        }
        public string write_excel(double[,] source_d, System.Data.DataTable user_dt, string[] user_c, string date_str, double[,] source_yaozhi, DataTable fu_dt,string path)
        {
            string saveFileName = string.Empty;
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);

            Microsoft.Office.Interop.Excel.Worksheet lastWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(workbook.Worksheets.Count);
            Microsoft.Office.Interop.Excel.Worksheet newSheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, lastWorksheet, Type.Missing, Type.Missing);
            newSheet.Name = "用户数据";
            for (int i = 0; i < source_d.GetLength(0); i++)
            {
                for (int j = 0; j < source_d.GetLength(1); j++)
                {
                    newSheet.Cells[i + 1, j + 2] = source_d[i, j];
                }
                newSheet.Cells[i + 1, 1] = user_c[i];
            }

            Microsoft.Office.Interop.Excel.Worksheet newSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, lastWorksheet, Type.Missing, Type.Missing);
            newSheet1.Name = "分析数据";
            newSheet1.Cells[1, 1] = "用户";
            newSheet1.Cells[1, 2] = "算法一";
            newSheet1.Cells[1, 3] = "算法二";
            newSheet1.Cells[1, 4] = "零值比例";
            newSheet1.Cells[1, 5] = "均值";
            newSheet1.Cells[1, 6] = "标准差";

            for (int i = 0; i < user_dt.Rows.Count; i++)
            {

                newSheet1.Cells[i + 2, 1] = user_dt.Rows[i]["user_id"].ToString();
                newSheet1.Cells[i + 2, 2] = user_dt.Rows[i]["m1"].ToString();
                newSheet1.Cells[i + 2, 3] = user_dt.Rows[i]["sx_bili"].ToString();
                newSheet1.Cells[i + 2, 4] = user_dt.Rows[i]["yaozhi"].ToString();
                newSheet1.Cells[i + 2, 5] = user_dt.Rows[i]["均值"].ToString();
                newSheet1.Cells[i + 2, 6] = user_dt.Rows[i]["标准差"].ToString();
            }

            //  Microsoft.Office.Interop.Excel.Worksheet lastWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(workbook.Worksheets.Count);
            Microsoft.Office.Interop.Excel.Worksheet newSheet2 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, lastWorksheet, Type.Missing, Type.Missing);
            newSheet2.Name = "幺值";
            for (int i = 0; i < source_yaozhi.GetLength(0); i++)
            {
                for (int j = 0; j < source_yaozhi.GetLength(1); j++)
                {
                    newSheet2.Cells[i + 1, j + 2] = source_yaozhi[i, j].ToString();
                }
                newSheet2.Cells[i + 1, 1] = user_c[i];
            }

            Microsoft.Office.Interop.Excel.Worksheet newSheet3 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, lastWorksheet, Type.Missing, Type.Missing);
            string[,] StringArray = new string[fu_dt.Rows.Count+1, fu_dt.Columns.Count];
            try
            {
                if (fu_dt.Rows.Count > 0)
                {
                    newSheet3.Name = "辅助数据";
                    StringArray[0, 0] = "用户";
                    StringArray[0, 1] = "采集时间";
                    StringArray[0, 2] = "A相电压";
                    StringArray[0, 3] = "B相电压";
                    StringArray[0, 4] = "C相电压";
                    StringArray[0, 5] = "A相电流";
                    StringArray[0, 6] = "B相电流";
                    StringArray[0, 7] = "C相电流";
                    StringArray[0, 8] = "A相功率";
                    StringArray[0, 9] = "B相功率";
                    StringArray[0, 10] = "C相功率";
                    StringArray[0, 11] = "总功率因素";
                    StringArray[0, 12] = "有功功率";
                    StringArray[0, 13] = "线路";
                    StringArray[0, 14] = "主表表码正向有功表码";
                    StringArray[0, 15] = "主表表码方向有功表码";
                    StringArray[0, 16] = "主表表码反向有功表码";
                    StringArray[0, 17] = "主表表码反向有功表码";
                    StringArray[0, 18] = "负表表码正向有功表码";
                    StringArray[0, 19] = "负表表码方向有功表码";
                    StringArray[0, 20] = "负表表码反向有功表码";
                    StringArray[0, 21] = "负表表码反向有功表码";

                    for (int i = 1; i <= fu_dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < fu_dt.Columns.Count; j++)
                        {
                            StringArray[i, j] = fu_dt.Rows[i - 1][j].ToString();
                        }
                    }
                    newSheet3.get_Range(newSheet3.Cells[1, 1], newSheet3.Cells[fu_dt.Rows.Count, fu_dt.Columns.Count]).Value = StringArray;
                }
            }
            catch(Exception er)
            {
                er.Message.ToString();
            }


            ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1)).Delete();
            ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1)).Delete();
            ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1)).Delete();
            ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1)).Activate();
            workbook.Saved = true;
            try
            {

               string   fileName_end = System.DateTime.Now.ToString("yyyy-MM-dd") + "_" + System.DateTime.Now.ToString("HHmmss") + ".xls";
               saveFileName = path + "\\" + fileName_end;
                
                // saveFileName = System.Windows.Forms.Application.StartupPath+".\\"+ System.DateTime.Now.ToString("yyyy-MM-dd") + "_" + System.DateTime.Now.ToString("HHmmss") + ".xls";
                 workbook.SaveCopyAs(saveFileName);
                 saveFileName = fileName_end;
                //string saveFileName = System.DateTime.Now.ToString("yyyy-MM-dd") + "_" + System.DateTime.Now.ToString("HHmmss") + ".xls";
                //SaveFileDialog SaveFile = new SaveFileDialog();
                //SaveFile.FileName = saveFileName;
                //SaveFile.Filter = "Miscrosoft Office Excel 97-2003 工作表|*.xls|所有文件(*.*)|*.*";
                //SaveFile.RestoreDirectory = true;
                //if (SaveFile.ShowDialog() == DialogResult.OK)
                //{
                //    workbook.SaveCopyAs(SaveFile.FileName);


                //    MessageBox.Show("导出数据成功!", "系统信息");
                //}

                workbook.Close();

                //workbook.Close(true, saveFileName, System.Reflection.Missing.Value);
                //  return saveFileName;
            }
            catch (Exception e) { throw e; }
            excel.Quit();


            if (excel != null)
            {
                excel.Workbooks.Close();


                excel.Quit();


                int generation = GC.GetGeneration(excel);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);


                excel = null;
                GC.Collect(generation);
            }
            GC.Collect(); //强行销毁





            System.Diagnostics.Process[] excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            DateTime startTime = new DateTime();
            int m, killId = 0;
            for (m = 0; m < excelProc.Length; m++)
            {
                if (startTime < excelProc[m].StartTime)
                {
                    startTime = excelProc[m].StartTime;
                    killId = m;
                }
            }
            if (excelProc[killId].HasExited == false)
            {
                excelProc[killId].Kill();
            }
            return saveFileName;

        }
        public void write_excel(System.Data.DataTable sx_datatable, System.Data.DataTable sx_data)
        {

            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);

            Microsoft.Office.Interop.Excel.Worksheet lastWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(workbook.Worksheets.Count);
            Microsoft.Office.Interop.Excel.Worksheet newSheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, lastWorksheet, Type.Missing, Type.Missing);
            newSheet.Name = "变异数据";
            for (int i = 0; i < sx_datatable.Columns.Count; i++)
            {
                newSheet.Cells[1, i + 1] = sx_datatable.Columns[i].ColumnName;
            }
            for (int i = 0; i < sx_datatable.Rows.Count; i++)
            {
                for (int l = 0; l < sx_datatable.Columns.Count; l++)
                {
                    newSheet.Cells[i + 2, l + 1] = sx_datatable.Rows[i][l].ToString();
                }
            }


            Microsoft.Office.Interop.Excel.Worksheet newSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, lastWorksheet, Type.Missing, Type.Missing);
            newSheet1.Name = "差异数据";
            for (int i = 0; i < sx_data.Columns.Count; i++)
            {
                newSheet1.Cells[1, i + 1] = sx_data.Columns[i].ColumnName;
            }
            for (int i = 0; i < sx_data.Rows.Count; i++)
            {
                for (int l = 0; l < sx_data.Columns.Count; l++)
                {
                    newSheet1.Cells[i + 2, l + 1] = sx_data.Rows[i][l].ToString();
                }
            }


            ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1)).Delete();
            ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1)).Delete();
            ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1)).Delete();
            ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1)).Activate();
            try
            {



                workbook.Saved = true;
                //   string saveFileName = System.Windows.Forms.Application.ExecutablePath+"\\"+ System.DateTime.Now.ToString("yyyy-MM-dd") + "_" + System.DateTime.Now.ToString("HHmmss") + ".xls";
                string saveFileName = System.DateTime.Now.ToString("yyyy-MM-dd") + "_" + System.DateTime.Now.ToString("HHmmss") + ".xls";
                SaveFileDialog SaveFile = new SaveFileDialog();
                SaveFile.FileName = saveFileName;
                SaveFile.Filter = "Miscrosoft Office Excel 97-2003 工作表|*.xls|所有文件(*.*)|*.*";
                SaveFile.RestoreDirectory = true;
                if (SaveFile.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveCopyAs(SaveFile.FileName);


                    MessageBox.Show("导出数据成功!", "系统信息");
                }

                workbook.Close();

                //workbook.Close(true, saveFileName, System.Reflection.Missing.Value);
                //  return saveFileName;
            }
            catch (Exception e) { throw e; }
            excel.Quit();


            if (excel != null)
            {
                excel.Workbooks.Close();


                excel.Quit();


                int generation = GC.GetGeneration(excel);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);


                excel = null;
                GC.Collect(generation);
            }
            GC.Collect(); //强行销毁





            System.Diagnostics.Process[] excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            DateTime startTime = new DateTime();
            int m, killId = 0;
            for (m = 0; m < excelProc.Length; m++)
            {
                if (startTime < excelProc[m].StartTime)
                {
                    startTime = excelProc[m].StartTime;
                    killId = m;
                }
            }
            if (excelProc[killId].HasExited == false)
            {
                excelProc[killId].Kill();
            }

        }
        public void write_excel(DataTable dt)
        {
            
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);

            Microsoft.Office.Interop.Excel.Worksheet lastWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(workbook.Worksheets.Count);
            Microsoft.Office.Interop.Excel.Worksheet newSheet3 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, lastWorksheet, Type.Missing, Type.Missing);
            string[,] StringArray = new string[dt.Rows.Count + 1, dt.Columns.Count];
            try
            {
                if (dt.Rows.Count > 0)
                {
                    newSheet3.Name = "数据导出";
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        StringArray[0, i] = dt.Columns[i].ColumnName; 
                    }

                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            StringArray[i, j] = dt.Rows[i - 1][j].ToString();
                        }
                    }
                    newSheet3.get_Range(newSheet3.Cells[1, 1], newSheet3.Cells[dt.Rows.Count, dt.Columns.Count]).Value = StringArray;
                }
            }
            catch (Exception er)
            {
                er.Message.ToString();
            }


            ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1)).Delete();
            ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1)).Delete();
            ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1)).Delete();
            ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1)).Activate();
            workbook.Saved = true;
          
            string saveFileName = System.DateTime.Now.ToString("yyyy-MM-dd") + "_" + System.DateTime.Now.ToString("HHmmss") + ".xls";
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.FileName = saveFileName;
            SaveFile.Filter = "Miscrosoft Office Excel 97-2003 工作表|*.xls|所有文件(*.*)|*.*";
            SaveFile.RestoreDirectory = true;
            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveCopyAs(SaveFile.FileName);


                MessageBox.Show("导出数据成功!", "系统信息");
            }

            workbook.Close();
        }
    }
}
