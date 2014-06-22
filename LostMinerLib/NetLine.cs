using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml.Serialization;
using System.Web.Services.Protocols;
using System.IO;
using System.Reflection;
using System.Collections;
using LostMinerLib.Util;
using System.Threading;
using System.Diagnostics;
namespace LostMinerLib
{
    public partial class NetLine : Form
    {
        private OpaqueCommand cmd;
        private DataTable all_dt = new DataTable();
        private DataTable usermoniter;
        private string close_str;
        private string task_str;
        private string sql;
        private DataTable dt_down = new DataTable();
        INIClass iniclass = new INIClass(Application.StartupPath + @"\system.ini");
        public NetLine()
        {
            InitializeComponent();
            this.cmd = new OpaqueCommand();
            dateTimePicker1.Value = System.DateTime.Now.AddMonths(-1);
            z.DataSource = all_dt;
          //  InitDataSet();

            DBHelper.DBiniClass iniclass = new DBHelper.DBiniClass(".\\system.ini");

            string username = iniclass.IniReadValue("username", "username");
            string defaultmonth = iniclass.IniReadValue("defaultmoth", "month");

//            [dataselcon]
//rate_agv_beg=1
//rate_avg_end=100
//lost_avg_beg=3000
//lost_avg_end=1000
//data_year_month=2014-05

             string datayearmonth = iniclass.IniReadValue("dataselcon", "data_year_month");
            string rate_agv_beg=iniclass.IniReadValue("dataselcon", "rate_agv_beg");
            string rate_avg_end=iniclass.IniReadValue("dataselcon", "rate_avg_end");
             string lost_avg_beg=iniclass.IniReadValue("dataselcon", "lost_avg_beg");
             string lost_avg_end=iniclass.IniReadValue("dataselcon", "lost_avg_end");

             dateTimePicker1.Value = DateTime.Parse(datayearmonth + "-01");
             textBox2.Text = rate_agv_beg;
             textBox5.Text = rate_avg_end;
             textBox6.Text = lost_avg_beg;
             textBox7.Text = lost_avg_end;


            textBox4.Text = defaultmonth;
            z.AutoGenerateColumns = false;

            dt_down.Columns.Add("线路");
            dt_down.Columns.Add("开始年月");
            dt_down.Columns.Add("结束年月");


            textBox3.Text = username;

            // get_data_from_webservice();
        }
        public void get_data_from_webservice()
        {
            string sql = "select * from line_data";
            DataTable dtss = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
            try
            {
                DataTable dt = new System.Data.DataTable();
                DataColumn cl1 = new System.Data.DataColumn();
                cl1.ColumnName = "date_time";
                DataColumn cl2 = new System.Data.DataColumn();
                cl2.ColumnName = "line_no";
                DataColumn cl3 = new System.Data.DataColumn();
                cl3.ColumnName = "total";
                DataColumn cl4 = new System.Data.DataColumn();
                cl4.ColumnName = "localstate";
                dt.Columns.Add(cl1);
                dt.Columns.Add(cl2);
                dt.Columns.Add(cl3);
                dt.Columns.Add(cl4);

                string line_month = iniclass.IniReadValue("webservice", "line_month");
                object[] dd = get_data_webservice(line_month, new object[] { });
                for (int i = 0; i < dd.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    Type obejct_t = dd[i].GetType();
                    PropertyInfo[] pi = obejct_t.GetProperties();
                    for (int r = 0; r < pi.Length; r++)
                    {
                        string ddd = pi[r].GetValue(dd[i], null).ToString();
                        if (pi[r].Name == "date_time")
                        {
                            dr["date_time"] = ddd;
                        }
                        else if (pi[r].Name == "line_no")
                        {
                            dr["line_no"] = ddd;
                        }
                        else if (pi[r].Name == "total")
                        {
                            dr["total"] = ddd;
                        }
                    }
                    DataRow[] drs = dtss.Select("line_no='" + dr["line_no"].ToString() + "' and date_time='" + dr["date_time"].ToString() + "'");
                    if (drs.Length > 0)
                    {
                        dr["localstate"] = "已存在";
                    }
                    else
                    {
                        dr["localstate"] = "未存在";
                    }
                    dt.Rows.Add(dr);
                }
                all_dt = dt;
                z.DataSource = dt;
                label2.Text = "服务器连接性：连接正常";
            }
            catch (Exception er)
            {
                if (er.Message.Contains("500"))
                {
                    label2.Text = "服务器连接性：连接异常";
                }
                else
                {
                    MessageBox.Show("获取线路的接口不存在或错误");
                    label2.Text = "服务器连接性：连接异常";
                }
            }
        }
        public object[] get_data_webservice(string method_name, object[] objs)
        {
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 3) && (e.RowIndex != -1))
            {
                //获取线路数据
                #region
                DataTable dt = new System.Data.DataTable();
                DataColumn cl1 = new System.Data.DataColumn();
                cl1.ColumnName = "ct";
                DataColumn cl2 = new System.Data.DataColumn();
                cl2.ColumnName = "date_time";
                DataColumn cl3 = new System.Data.DataColumn();
                cl3.ColumnName = "line_no";
                DataColumn cl4 = new System.Data.DataColumn();
                cl4.ColumnName = "rate";
                DataColumn cl5 = new System.Data.DataColumn();
                cl5.ColumnName = "TOTAL_Q";
                DataColumn cl6 = new System.Data.DataColumn();
                cl6.ColumnName = "USER_Q";
                string line_no = this.z.Rows[e.RowIndex].Cells[1].Value.ToString();
                string data_month = this.z.Rows[e.RowIndex].Cells[0].Value.ToString();
                object[] dd = get_data_webservice("getUsersss", new object[] { line_no, data_month });
                for (int i = 0; i < dd.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    Type obejct_t = dd[i].GetType();
                    PropertyInfo[] pi = obejct_t.GetProperties();
                    // IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
                    for (int r = 0; r < pi.Length; r++)
                    {
                        string ddd = pi[r].GetValue(dd[i], null).ToString();
                        if (pi[r].Name == "date_time")
                        {
                            dr["date_time"] = ddd;
                        }
                        else if (pi[r].Name == "line_no")
                        {
                            dr["line_no"] = ddd;
                        }
                        else if (pi[r].Name == "total")
                        {
                            dr["total"] = ddd;
                        }
                        else if (pi[r].Name == "rate")
                        {
                            dr["rate"] = ddd;
                        }
                        else if (pi[r].Name == "user_q")
                        {
                            dr["user_q"] = ddd;
                        }
                        else if (pi[r].Name == "ct")
                        {
                            dr["ct"] = ddd;
                        }
                    }
                    dt.Rows.Add(dr);
                }
                #endregion
                //获取用户数据
                #region
                DataTable dt_user = new System.Data.DataTable();
                cl1 = new System.Data.DataColumn();
                cl1.ColumnName = "ct";
                cl2 = new System.Data.DataColumn();
                cl2.ColumnName = "date_time";
                cl3 = new System.Data.DataColumn();
                cl3.ColumnName = "line_no";
                cl4 = new System.Data.DataColumn();
                cl4.ColumnName = "used_q";
                cl5 = new System.Data.DataColumn();
                cl5.ColumnName = "user_id";

                dd = get_data_webservice("getUser", new object[] { line_no, data_month });
                for (int i = 0; i < dd.Length; i++)
                {
                    DataRow dr = dt_user.NewRow();
                    Type obejct_t = dd[i].GetType();
                    PropertyInfo[] pi = obejct_t.GetProperties();
                    // IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
                    for (int r = 0; r < pi.Length; r++)
                    {
                        string ddd = pi[r].GetValue(dd[i], null).ToString();
                        if (pi[r].Name == "date_time")
                        {
                            dr["date_time"] = ddd;
                        }
                        else if (pi[r].Name == "line_no")
                        {
                            dr["line_no"] = ddd;
                        }
                        else if (pi[r].Name == "used_q")
                        {
                            dr["used_q"] = ddd;
                        }
                        else if (pi[r].Name == "user_id")
                        {
                            dr["user_id"] = ddd;
                        }

                    }
                    dt_user.Rows.Add(dr);
                }
                #endregion
                StringBuilder sp = new StringBuilder();
                ArrayList al = new ArrayList();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sql = "insert into ts_line (LINE_NO,DATE_TIME ,TOTAL_Q,USER_Q ,lost ,rate,ct) values('" + dt.Rows[i]["line_no"].ToString() + "','" + dt.Rows[i]["date_time"].ToString() + "','" + dt.Rows[i]["total_q"].ToString() + "','" + dt.Rows[i]["user_q"].ToString() + "','" + dt.Rows[i]["lost"].ToString() + "','" + dt.Rows[i]["rate"].ToString() + "','" + dt.Rows[i]["ct"].ToString() + "')";
                    al.Add(sql);
                }
                for (int i = 0; i < dt_user.Rows.Count; i++)
                {
                    string sql = "insert into ts_user (user_id,DATE_TIME ,USED_Q ,line_no,ct) values('" + dt_user.Rows[i]["user_id"].ToString() + "','" + dt_user.Rows[i]["date_time"].ToString() + "','" + dt_user.Rows[i]["user_q"].ToString() + "','" + dt_user.Rows[i]["line_no"].ToString() + "','" + dt_user.Rows[i]["ct"].ToString() + "')";
                    al.Add(sql);
                }
                string resturt_str = DBHelper.DBHelper.ExecuteCommand(al);
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            if ((e.ColumnIndex == 3) && (e.RowIndex != -1))
            {
                //获取线路数据
                #region
                DataTable dt = new System.Data.DataTable();
                DataColumn cl1 = new System.Data.DataColumn();
                cl1.ColumnName = "ct";
                DataColumn cl2 = new System.Data.DataColumn();
                cl2.ColumnName = "date_time";
                DataColumn cl3 = new System.Data.DataColumn();
                cl3.ColumnName = "line_no";
                DataColumn cl4 = new System.Data.DataColumn();
                cl4.ColumnName = "rate";
                DataColumn cl5 = new System.Data.DataColumn();
                cl5.ColumnName = "TOTAL_Q";
                DataColumn cl6 = new System.Data.DataColumn();
                cl6.ColumnName = "USER_Q";
                string line_no = this.z.Rows[e.RowIndex].Cells[1].Value.ToString();
                string data_month = this.z.Rows[e.RowIndex].Cells[0].Value.ToString();
                object[] dd = get_data_webservice("getUsersss", new object[] { line_no, data_month });
                for (int i = 0; i < dd.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    Type obejct_t = dd[i].GetType();
                    PropertyInfo[] pi = obejct_t.GetProperties();
                    // IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
                    for (int r = 0; r < pi.Length; r++)
                    {
                        string ddd = pi[r].GetValue(dd[i], null).ToString();
                        if (pi[r].Name == "date_time")
                        {
                            dr["date_time"] = ddd;
                        }
                        else if (pi[r].Name == "line_no")
                        {
                            dr["line_no"] = ddd;
                        }
                        else if (pi[r].Name == "total")
                        {
                            dr["total"] = ddd;
                        }
                        else if (pi[r].Name == "rate")
                        {
                            dr["rate"] = ddd;
                        }
                        else if (pi[r].Name == "user_q")
                        {
                            dr["user_q"] = ddd;
                        }
                        else if (pi[r].Name == "ct")
                        {
                            dr["ct"] = ddd;
                        }
                    }
                    dt.Rows.Add(dr);
                }
                #endregion
                //获取用户数据
                #region
                DataTable dt_user = new System.Data.DataTable();
                cl1 = new System.Data.DataColumn();
                cl1.ColumnName = "ct";
                cl2 = new System.Data.DataColumn();
                cl2.ColumnName = "date_time";
                cl3 = new System.Data.DataColumn();
                cl3.ColumnName = "line_no";
                cl4 = new System.Data.DataColumn();
                cl4.ColumnName = "used_q";
                cl5 = new System.Data.DataColumn();
                cl5.ColumnName = "user_id";

                dd = get_data_webservice("getUser", new object[] { line_no, data_month });
                for (int i = 0; i < dd.Length; i++)
                {
                    DataRow dr = dt_user.NewRow();
                    Type obejct_t = dd[i].GetType();
                    PropertyInfo[] pi = obejct_t.GetProperties();
                    // IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
                    for (int r = 0; r < pi.Length; r++)
                    {
                        string ddd = pi[r].GetValue(dd[i], null).ToString();
                        if (pi[r].Name == "date_time")
                        {
                            dr["date_time"] = ddd;
                        }
                        else if (pi[r].Name == "line_no")
                        {
                            dr["line_no"] = ddd;
                        }
                        else if (pi[r].Name == "used_q")
                        {
                            dr["used_q"] = ddd;
                        }
                        else if (pi[r].Name == "user_id")
                        {
                            dr["user_id"] = ddd;
                        }

                    }
                    dt_user.Rows.Add(dr);
                }
                #endregion
                StringBuilder sp = new StringBuilder();
                ArrayList al = new ArrayList();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sql = "insert into ts_line (LINE_NO,DATE_TIME ,TOTAL_Q,USER_Q ,lost ,rate,ct) values('" + dt.Rows[i]["line_no"].ToString() + "','" + dt.Rows[i]["date_time"].ToString() + "','" + dt.Rows[i]["total_q"].ToString() + "','" + dt.Rows[i]["user_q"].ToString() + "','" + dt.Rows[i]["lost"].ToString() + "','" + dt.Rows[i]["rate"].ToString() + "','" + dt.Rows[i]["ct"].ToString() + "')";
                    al.Add(sql);
                }
                for (int i = 0; i < dt_user.Rows.Count; i++)
                {
                    string sql = "insert into ts_user (user_id,DATE_TIME ,USED_Q ,line_no,ct) values('" + dt_user.Rows[i]["user_id"].ToString() + "','" + dt_user.Rows[i]["date_time"].ToString() + "','" + dt_user.Rows[i]["user_q"].ToString() + "','" + dt_user.Rows[i]["line_no"].ToString() + "','" + dt_user.Rows[i]["ct"].ToString() + "')";
                    al.Add(sql);
                }
                string resturt_str = DBHelper.DBHelper.ExecuteCommand(al);
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private string RunCmd()
        {


            string command = "";
            int milliseconds = 2;
            Process p = new Process();
            string res = string.Empty;

            p.StartInfo.FileName = "spooldata.bat";
            // p.StartInfo.Arguments = "/c " + command;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;

            try
            {
                if (p.Start())       //开始进程
                {
                    if (milliseconds == 0)
                        p.WaitForExit();     //这里无限等待进程结束
                    else
                        p.WaitForExit(milliseconds);  //这里等待进程结束，等待时间为指定的毫秒     

                    res = p.StandardOutput.ReadToEnd();
                }
            }
            catch
            {
            }
            finally
            {
                if (p != null)
                    p.Close();

                //KillProcess(p);
            }

            return res;
        }
        private string RunDelCmd()
        {


            string command = "";
            int milliseconds = 2;
            Process p = new Process();
            string res = string.Empty;

            p.StartInfo.FileName = "rmmkdir.bat";
            // p.StartInfo.Arguments = "/c " + command;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;

            try
            {
                if (p.Start())       //开始进程
                {
                    if (milliseconds == 0)
                        p.WaitForExit();     //这里无限等待进程结束
                    else
                        p.WaitForExit(milliseconds);  //这里等待进程结束，等待时间为指定的毫秒     

                    res = p.StandardOutput.ReadToEnd();
                }
            }
            catch
            {
            }
            finally
            {
                if (p != null)
                    p.Close();

                //KillProcess(p);
            }

            return res;
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int rowcount = 0;
        //        foreach (DataGridViewRow Row in (IEnumerable)this.dataGridView1.Rows)
        //        {
        //            object ob = Row.Cells[0].FormattedValue;
        //            bool b = (bool)Row.Cells[0].FormattedValue;
        //            //获取线路数据
        //            if (b)
        //            {
        //                rowcount++;
        //                try
        //                {
        //                    #region

        //                    DataTable dt = new System.Data.DataTable();
        //                    DataColumn cl1 = new System.Data.DataColumn();
        //                    cl1.ColumnName = "ct";
        //                    DataColumn cl2 = new System.Data.DataColumn();
        //                    cl2.ColumnName = "date_time";
        //                    DataColumn cl3 = new System.Data.DataColumn();
        //                    cl3.ColumnName = "line_no";
        //                    DataColumn cl4 = new System.Data.DataColumn();
        //                    cl4.ColumnName = "rate";
        //                    DataColumn cl5 = new System.Data.DataColumn();
        //                    cl5.ColumnName = "TOTAL_Q";
        //                    DataColumn cl6 = new System.Data.DataColumn();
        //                    cl6.ColumnName = "USER_Q";
        //                    dt.Columns.Add(cl1);
        //                    dt.Columns.Add(cl2);
        //                    dt.Columns.Add(cl3);
        //                    dt.Columns.Add(cl4);
        //                    dt.Columns.Add(cl5);
        //                    dt.Columns.Add(cl6);
        //                    string line_no = Row.Cells[4].Value.ToString();
        //                    string data_month = Row.Cells[3].Value.ToString();
        //                    string line_interface = iniclass.IniReadValue("webservice", "line_interface");
        //                    object[] dd = get_data_webservice(line_interface, new object[] { line_no, data_month });
        //                    for (int i = 0; i < dd.Length; i++)
        //                    {
        //                        DataRow dr = dt.NewRow();
        //                        Type obejct_t = dd[i].GetType();
        //                        PropertyInfo[] pi = obejct_t.GetProperties();
        //                        // IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
        //                        for (int r = 0; r < pi.Length; r++)
        //                        {
        //                            string ddd = pi[r].GetValue(dd[i], null).ToString();
        //                            if (pi[r].Name == "date_time")
        //                            {
        //                                dr["date_time"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "line_no")
        //                            {
        //                                dr["line_no"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "TOTAL_Q")
        //                            {
        //                                dr["TOTAL_Q"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "rate")
        //                            {
        //                                dr["rate"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "USER_Q")
        //                            {
        //                                dr["user_q"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "ct")
        //                            {
        //                                dr["ct"] = ddd;
        //                            }
        //                        }
        //                        dt.Rows.Add(dr);
        //                    }
        //                    #endregion
        //                    //获取用户数据
        //                    #region
        //                    DataTable dt_user = new System.Data.DataTable();
        //                    cl1 = new System.Data.DataColumn();
        //                    cl1.ColumnName = "ct";
        //                    cl2 = new System.Data.DataColumn();
        //                    cl2.ColumnName = "date_time";
        //                    cl3 = new System.Data.DataColumn();
        //                    cl3.ColumnName = "line_no";
        //                    cl4 = new System.Data.DataColumn();
        //                    cl4.ColumnName = "used_q";
        //                    cl5 = new System.Data.DataColumn();
        //                    cl5.ColumnName = "user_id";
        //                    dt_user.Columns.Add(cl1);
        //                    dt_user.Columns.Add(cl2);
        //                    dt_user.Columns.Add(cl3);
        //                    dt_user.Columns.Add(cl4);
        //                    dt_user.Columns.Add(cl5);
        //                    string user_interface = iniclass.IniReadValue("webservice", "user_interface");
        //                    dd = get_data_webservice(user_interface, new object[] { line_no, data_month });
        //                    for (int i = 0; i < dd.Length; i++)
        //                    {
        //                        DataRow dr = dt_user.NewRow();
        //                        Type obejct_t = dd[i].GetType();
        //                        PropertyInfo[] pi = obejct_t.GetProperties();
        //                        // IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
        //                        for (int r = 0; r < pi.Length; r++)
        //                        {
        //                            string ddd = pi[r].GetValue(dd[i], null).ToString();
        //                            if (pi[r].Name == "date_time")
        //                            {
        //                                dr["date_time"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "line_no")
        //                            {
        //                                dr["line_no"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "used_q")
        //                            {
        //                                dr["used_q"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "user_id")
        //                            {
        //                                dr["user_id"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "ct")
        //                            {
        //                                dr["ct"] = ddd;
        //                            }

        //                        }
        //                        dt_user.Rows.Add(dr);
        //                    }
        //                    #endregion
        //                    //获取辅助数据
        //                    #region
        //                    usermoniter = new System.Data.DataTable();
        //                    DataColumn mcl1 = new System.Data.DataColumn();
        //                    mcl1.ColumnName = "a_BALANCE_NO";
        //                    DataColumn mcl2 = new System.Data.DataColumn();
        //                    mcl2.ColumnName = "a_KW";
        //                    DataColumn mcl3 = new System.Data.DataColumn();
        //                    mcl3.ColumnName = "a_KW_S";
        //                    DataColumn mcl4 = new System.Data.DataColumn();
        //                    mcl4.ColumnName = "a_V";
        //                    DataColumn mcl5 = new System.Data.DataColumn();
        //                    mcl5.ColumnName = "a_V_A";
        //                    DataColumn mcl6 = new System.Data.DataColumn();
        //                    mcl6.ColumnName = "b_KW";
        //                    DataColumn mcl7 = new System.Data.DataColumn();
        //                    mcl7.ColumnName = "b_KW_S";
        //                    DataColumn mcl8 = new System.Data.DataColumn();
        //                    mcl8.ColumnName = "b_V";
        //                    DataColumn mcl9 = new System.Data.DataColumn();
        //                    mcl9.ColumnName = "CURR_KVAR";
        //                    DataColumn mcl10 = new System.Data.DataColumn();
        //                    mcl10.ColumnName = "CURR_KW";
        //                    DataColumn mcl11 = new System.Data.DataColumn();
        //                    mcl11.ColumnName = "b_V_B";
        //                    DataColumn mcl12 = new System.Data.DataColumn();
        //                    mcl12.ColumnName = "c_KW";
        //                    DataColumn mcl13 = new System.Data.DataColumn();
        //                    mcl13.ColumnName = "c_KW_S";
        //                    DataColumn mcl14 = new System.Data.DataColumn();
        //                    mcl14.ColumnName = "c_V";
        //                    DataColumn mcl15 = new System.Data.DataColumn();
        //                    mcl15.ColumnName = "c_V_C";
        //                    DataColumn mcl16 = new System.Data.DataColumn();
        //                    mcl16.ColumnName = "date_time";
        //                    DataColumn mcl17 = new System.Data.DataColumn();
        //                    mcl17.ColumnName = "jl_point";
        //                    DataColumn mcl18 = new System.Data.DataColumn();
        //                    mcl18.ColumnName = "m_NO";
        //                    DataColumn mcl19 = new System.Data.DataColumn();
        //                    mcl19.ColumnName = "TOTAL_KW";
        //                    //DataColumn cl20 = new System.Data.DataColumn();
        //                    //cl20.ColumnName = "user_idd";
        //                    DataColumn mcl21 = new System.Data.DataColumn();
        //                    mcl21.ColumnName = "KW_BANLACE";
        //                    DataColumn mcl22 = new System.Data.DataColumn();
        //                    mcl22.ColumnName = "line_no";
        //                    DataColumn mcl23 = new System.Data.DataColumn();
        //                    mcl23.ColumnName = "user_id";
        //                    usermoniter.Columns.Add(mcl1);
        //                    usermoniter.Columns.Add(mcl2);
        //                    usermoniter.Columns.Add(mcl3);
        //                    usermoniter.Columns.Add(mcl4);
        //                    usermoniter.Columns.Add(mcl5);
        //                    usermoniter.Columns.Add(mcl6);
        //                    usermoniter.Columns.Add(mcl7);
        //                    usermoniter.Columns.Add(mcl8);
        //                    usermoniter.Columns.Add(mcl9);
        //                    usermoniter.Columns.Add(mcl10);
        //                    usermoniter.Columns.Add(mcl11);
        //                    usermoniter.Columns.Add(mcl12);
        //                    usermoniter.Columns.Add(mcl13);
        //                    usermoniter.Columns.Add(mcl14);
        //                    usermoniter.Columns.Add(mcl15);
        //                    usermoniter.Columns.Add(mcl16);
        //                    usermoniter.Columns.Add(mcl17);
        //                    usermoniter.Columns.Add(mcl18);
        //                    usermoniter.Columns.Add(mcl19);
        //                    //usermoniter.Columns.Add(cl20);
        //                    usermoniter.Columns.Add(mcl21);
        //                    usermoniter.Columns.Add(mcl22);
        //                    usermoniter.Columns.Add(mcl23);
        //                    //  string line_no = this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        //                    // string data_month = this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        //                    string user_moniter_interface = iniclass.IniReadValue("webservice", "user_moniter_interface");
        //                    dd = get_data_webservice(user_moniter_interface, new object[] { line_no, data_month });
        //                    for (int i = 0; i < dd.Length; i++)
        //                    {
        //                        DataRow dr = usermoniter.NewRow();
        //                        Type obejct_t = dd[i].GetType();
        //                        PropertyInfo[] pi = obejct_t.GetProperties();
        //                        // IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
        //                        for (int r = 0; r < pi.Length; r++)
        //                        {
        //                            string ddd = pi[r].GetValue(dd[i], null).ToString();
        //                            if (pi[r].Name == "a_BALANCE_NO")
        //                            {
        //                                dr["a_BALANCE_NO"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "a_KW")
        //                            {
        //                                dr["a_KW"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "a_KW_S")
        //                            {
        //                                dr["a_KW_S"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "a_V")
        //                            {
        //                                dr["a_V"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "a_V_A")
        //                            {
        //                                dr["a_V_A"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "b_KW")
        //                            {
        //                                dr["b_KW"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "b_KW_S")
        //                            {
        //                                dr["b_KW_S"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "b_V_B")
        //                            {
        //                                dr["b_V_B"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "CURR_KVAR")
        //                            {
        //                                dr["CURR_KVAR"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "CURR_KW")
        //                            {
        //                                dr["CURR_KW"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "c_KW")
        //                            {
        //                                dr["c_KW"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "c_KW_S")
        //                            {
        //                                dr["c_KW_S"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "c_V")
        //                            {
        //                                dr["c_V"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "c_V_C")
        //                            {
        //                                dr["c_V_C"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "jl_point")
        //                            {
        //                                dr["jl_point"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "date_time")
        //                            {
        //                                dr["date_time"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "KW_BANLACE")
        //                            {
        //                                dr["KW_BANLACE"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "line_no")
        //                            {
        //                                dr["line_no"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "m_NO")
        //                            {
        //                                dr["m_NO"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "TOTAL_KW")
        //                            {
        //                                dr["TOTAL_KW"] = ddd;
        //                            }
        //                            else if (pi[r].Name == "user_id")
        //                            {
        //                                dr["user_id"] = ddd;
        //                            }

        //                        }
        //                        usermoniter.Rows.Add(dr);
        //                    }
        //                    #endregion
        //                    #region
        //                    StringBuilder sp = new StringBuilder();
        //                    ArrayList al = new ArrayList();

        //                    for (int i = 0; i < dt.Rows.Count; i++)
        //                    {
        //                        decimal lost = decimal.Parse(dt.Rows[i]["total_q"].ToString()) - decimal.Parse(dt.Rows[i]["user_q"].ToString());
        //                        string sql = "insert into ts_line (LINE_NO,DATE_TIME ,TOTAL_Q,USER_Q ,lost ,rate,ct) values('" + dt.Rows[i]["line_no"].ToString() + "','" + dt.Rows[i]["date_time"].ToString() + "','" + dt.Rows[i]["total_q"].ToString() + "','" + dt.Rows[i]["user_q"].ToString() + "','" + lost.ToString() + "','" + dt.Rows[i]["rate"].ToString() + "','" + dt.Rows[i]["ct"].ToString() + "')";
        //                        al.Add(sql);
        //                    }
        //                    for (int i = 0; i < dt_user.Rows.Count; i++)
        //                    {
        //                        string sql = "insert into ts_user (user_id,DATE_TIME ,USED_Q ,line_no,ct) values('" + dt_user.Rows[i]["user_id"].ToString() + "','" + dt_user.Rows[i]["date_time"].ToString() + "','" + dt_user.Rows[i]["used_q"].ToString() + "','" + dt_user.Rows[i]["line_no"].ToString() + "','" + dt_user.Rows[i]["ct"].ToString() + "')";
        //                        al.Add(sql);
        //                    }
        //                    for (int i = 0; i < usermoniter.Rows.Count; i++)
        //                    {
        //                        string sql = "insert into ts_user_moniter(user_id ,jl_point ,date_time ,A_V ,B_V ,C_V ,A_V_A ,B_V_B ,C_V_C ,A_BALANCE_NO,CURR_KW ,CURR_KVAR ,A_KW ,B_KW ,C_KW ,TOTAL_KW ,A_KW_S ,B_KW_S ,C_KW_S ,KW_BANLACE ,M_NO,line_no ) values ";
        //                        sql = sql + "('" + usermoniter.Rows[i]["user_id"].ToString() + "','" + usermoniter.Rows[i]["jl_point"].ToString() + "','" + usermoniter.Rows[i]["date_time"].ToString() + "','" + usermoniter.Rows[i]["A_V"].ToString() + "','" + usermoniter.Rows[i]["B_V"].ToString() + "','" + usermoniter.Rows[i]["C_V"].ToString() + "','" + usermoniter.Rows[i]["A_V_A"].ToString() + "','" + usermoniter.Rows[i]["B_V_B"].ToString() + "','" + usermoniter.Rows[i]["C_V_C"].ToString() + "','" + usermoniter.Rows[i]["A_BALANCE_NO"].ToString() + "','" + usermoniter.Rows[i]["CURR_KW"].ToString() + "','" + usermoniter.Rows[i]["CURR_KVAR"].ToString() + "','" + usermoniter.Rows[i]["A_KW"].ToString() + "','" + usermoniter.Rows[i]["B_KW"].ToString() + "','" + usermoniter.Rows[i]["C_KW"].ToString() + "','" + usermoniter.Rows[i]["TOTAL_KW"].ToString() + "','" + usermoniter.Rows[i]["A_KW_S"].ToString() + "','" + usermoniter.Rows[i]["B_KW_S"].ToString() + "','" + usermoniter.Rows[i]["C_KW_S"].ToString() + "','" + usermoniter.Rows[i]["KW_BANLACE"].ToString() + "','" + usermoniter.Rows[i]["M_NO"].ToString() + "','" + usermoniter.Rows[i]["line_no"].ToString() + "' )";
        //                        al.Add(sql);
        //                    }
        //                    string resturt_str = DBHelper.DBHelper.ExecuteCommand(al);
        //                    #endregion
        //                }
        //                catch (Exception er)
        //                {
        //                    if (er.Message.Contains("500"))
        //                    {
        //                        label2.Text = "服务器连接性：连接异常";
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("获取数据的接口不存在或错误");
        //                        label2.Text = "服务器连接性：连接异常";
        //                    }
        //                }
        //            }
        //            }
        //        if (rowcount == 0)
        //        {
        //            MessageBox.Show("请选择数据下载");
        //        }
        //        else
        //        {
        //            MessageBox.Show("下载完成");
        //            get_data_from_webservice();
        //        }
        //        }
        //    catch
        //    { 

        //    }

        //}



        private void button4_Click(object sender, EventArgs e)
        {
            DBHelper.DBiniClass iniclass = new DBHelper.DBiniClass(".\\system.ini");
             iniclass.IniWriteValue("dataselcon", "data_year_month", dateTimePicker1.Value.ToString("yyyy-MM"));
             iniclass.IniWriteValue("dataselcon", "rate_agv_beg",textBox2.Text);
             iniclass.IniWriteValue("dataselcon", "rate_avg_end",  textBox5.Text);
             iniclass.IniWriteValue("dataselcon", "lost_avg_beg", textBox6.Text);
             iniclass.IniWriteValue("dataselcon", "lost_avg_end", textBox7.Text);

            InitDataSet();
        }

        private void NetLine_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close_str == "YES")
            {

            }
            else
            {
                Application.ExitThread();
            }
        }

        private void NetLine_FormClosed(object sender, FormClosedEventArgs e)
        {
            // System.Environment.Exit(0);
            // this.Parent.Show();

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in (IEnumerable)this.z.Rows)
            {

                ((DataGridViewCheckBoxCell)Row.Cells[0]).Value = checkBox1.Checked;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            webserviceconfig w = new webserviceconfig();
            w.ShowDialog();
        }



        private void databind()
        {
            z.SuspendLayout();
            CheckForIllegalCrossThreadCalls = false;
            //CheckForIllegalCrossThreadCalls = false;
            sql = @"select  *  from line_avg where 1=1";
            sql += " and date_time='" + dateTimePicker1.Value.ToString("yyyy-MM") + "' ";
            sql += "  and rate_avg>=" + textBox2.Text + " and  rate_avg<="+textBox5.Text+"";
            if (textBox2.Text != "")
            {
                sql += "  and rate_avg >= " + textBox2.Text + " ";
            }
            if (textBox5.Text != "")
            {
                sql += "  and rate_avg <= " + textBox5.Text + " ";
            }
            if (textBox1.Text != "")
            {
                sql += "  and upper(line_no) like upper('%" + textBox1.Text + "%') ";
            }
            if (textBox6.Text != "")
            {
                sql += "  and lost_avg >= " + textBox6.Text + " ";
            }
            if (textBox7.Text != "")
            {
                sql += "  and lost_avg <= " + textBox7.Text + " ";
            }
            all_dt = DBHelper.DBHelper.GetDataSet(sql).Tables[0];
            // z.DataSource = all_dt;

        }
        private void InitDataSet()
        {
            if (!worker.IsBusy)
            {
                task_str = "查询数据";
                this.cmd.ShowOpaqueLayer(this, 0x7d, true);
                this.worker.RunWorkerAsync();

            }

        }


        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.ComputeFibonacci(this.worker, e);
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            cmd.HideOpaqueLayer();
            if (task_str == "查询数据")
            {
                z.DataSource = all_dt;
            }
            else if (task_str == "下载数据")
            {
                close_str = "YES";
                TimeSearcher.TimeSearcherForm tf = new TimeSearcher.TimeSearcherForm();
                tf.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("数据下载完成");
            }
        }
        private int ComputeFibonacci(object sender, DoWorkEventArgs e)
        {
            this.worker.ReportProgress(50);
            if (task_str == "查询数据")
            {
                databind();
            }
            else if (task_str == "下载数据")
            {
                ArrayList al_line = new ArrayList();
                ArrayList al_lile_del = new ArrayList();
                string username = textBox3.Text;
                foreach (DataGridViewRow gr in z.Rows)
                {
                    if (bool.Parse(gr.Cells[0].EditedFormattedValue.ToString()))
                    {
                        string line_no = gr.Cells[1].EditedFormattedValue.ToString();
                        string data_month = gr.Cells[2].EditedFormattedValue.ToString();
                        string data_month_beg = data_month;
                        data_month = DateTime.Parse(data_month + "-1").AddMonths(-int.Parse(textBox4.Text)).ToString("yyyy-MM");
                        al_line.Add("delete from  ts_line  where substr(date_time,1,7)>='" + data_month + "' and substr(date_time,1,7)<='" + data_month_beg + "' and line_no='" + line_no + "'   ");
                        al_line.Add("delete from  ts_user where  substr(date_time,1,7)>='" + data_month + "' and substr(date_time,1,7)<='" + data_month_beg + "'  and line_no='" + line_no + "'   ");
                        al_line.Add("insert into  ts_line select * from v_ts_line where  substr(date_time,1,7)>='" + data_month + "' and substr(date_time,1,7)<='" + data_month_beg + "'  and line_no='" + line_no + "'   ");
                        al_line.Add("insert into  ts_user select * from v_ts_user where  substr(date_time,1,7)>='" + data_month + "' and substr(date_time,1,7)<='" + data_month_beg + "'  and line_no='" + line_no + "'   ");
                        al_line.Add("insert into ts_line_select(line_no,year_mon,username) values('" + line_no + "','" + data_month + "','" + username + "') ");
                        //   DataTable dt=DBHelper.DBHelper.GetDataSet("select * from v_ts_user where substr(date_time,1,7)='" + data_month + "' and line_no='" + line_no + "' order by user_id ,date_time "  ).Tables[0];
                    }
                }
                if (al_line.Count > 0)
                {
                    // DBHelper.DBHelper.ExecuteCommand(al_lile_del);
                    DBHelper.DBHelper.ExecuteCommand(al_line);
                }
            }
            else
            {
                foreach (DataGridViewRow gr in dataGridView1.Rows)
                {
                    
                        string line_no = gr.Cells[1].EditedFormattedValue.ToString();
                        string  data_month_beg= gr.Cells[3].EditedFormattedValue.ToString();
                        string data_month = gr.Cells[2].EditedFormattedValue.ToString();                       
                        string sql_line = "select * from   v_ts_line where substr(date_time,1,7)>='" + data_month + "' and substr(date_time,1,7)<='" + data_month_beg + "' and line_no='" + line_no + "' ";
                        string sql_user = "select * from  v_ts_user where substr(date_time,1,7)>='" + data_month + "' and substr(date_time,1,7)<='" + data_month_beg + "' and line_no='" + line_no + "' ";
                        DataTable dt_line = DBHelper.DBHelper.GetDataSet(sql_line).Tables[0];
                        DataTable dt_user = DBHelper.DBHelper.GetDataSet(sql_user).Tables[0];

                        DBHelper.DBiniClass iniclass = new DBHelper.DBiniClass(".\\system.ini");

                        iniclass.IniWriteValue("netlink", "linkmodel", "sqlite");
                        ArrayList al = new ArrayList();
                        al.Add("delete from  ts_line  where substr(date_time,1,7)>='" + data_month + "' and substr(date_time,1,7)<='" + data_month_beg + "' and line_no='" + line_no + "'   ");
                        al.Add("delete from  ts_user where  substr(date_time,1,7)>='" + data_month + "' and substr(date_time,1,7)<='" + data_month_beg + "'  and line_no='" + line_no + "'   ");
                       
                        for (int i = 0; i < dt_line.Rows.Count; i++)
                        {
                            string sql = "insert into ts_line (LINE_NO,DATE_TIME ,TOTAL_Q,USER_Q ,lost ,rate,ct) values('" + dt_line.Rows[i]["line_no"].ToString() + "','" + dt_line.Rows[i]["date_time"].ToString() + "','" + dt_line.Rows[i]["total_q"].ToString() + "','" + dt_line.Rows[i]["user_q"].ToString() + "','" + dt_line.Rows[i]["lost"].ToString() + "','" + dt_line.Rows[i]["rate"].ToString() + "','" + dt_line.Rows[i]["ct"].ToString() + "')";
                            al.Add(sql);
                        }
                        for (int i = 0; i < dt_user.Rows.Count; i++)
                        {
                            string sql = "insert into ts_user (user_id,DATE_TIME ,USED_Q ,line_no,ct) values('" + dt_user.Rows[i]["user_id"].ToString() + "','" + dt_user.Rows[i]["date_time"].ToString() + "','" + dt_user.Rows[i]["used_q"].ToString() + "','" + dt_user.Rows[i]["line_no"].ToString() + "','" + dt_user.Rows[i]["ct"].ToString() + "')";
                            al.Add(sql);
                        }
                        DBHelper.DBHelper.ExecuteCommand(al);
                        GC.Collect();
                        iniclass.IniWriteValue("netlink", "linkmodel", "oracle");
                    
                }

            }
            this.worker.ReportProgress(100);
            return -1;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("请输入用户名");
            }
            else
            {
                DBHelper.DBiniClass iniclass = new DBHelper.DBiniClass(".\\system.ini");

                iniclass.IniWriteValue("username", "username", textBox3.Text);
                iniclass.IniWriteValue("defaultmoth", "month", textBox4.Text);
                try
                {
                    int.Parse(textBox4.Text);
                    if (!worker.IsBusy)
                    {
                        task_str = "下载数据";
                        this.cmd.ShowOpaqueLayer(this, 0x7d, true);
                        this.worker.RunWorkerAsync();
                        //this.Hide();
                        //TimeSearcher.TimeSearcherForm tf = new TimeSearcher.TimeSearcherForm();
                        //tf.ShowDialog();
                       
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("数据时间跨度应为数字");
                }

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            close_str = "YES";
            LostMinerLib.Wizard w = new LostMinerLib.Wizard();
            w.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!worker.IsBusy)
            {
               // RunDelCmd();
                task_str = "卸载数据";
                this.cmd.ShowOpaqueLayer(this, 0x7d, true);
                this.worker.RunWorkerAsync();

            }
        }
        private void createtxt(string line_no, string name, string month)
        {

            string data_month_beg = month;
            month = DateTime.Parse(month + "-1").AddMonths(-int.Parse( textBox4.Text)).ToString("yyyy-MM");
            ArrayList al_line = new ArrayList();
            al_line.Add("delete from  ts_line  where substr(date_time,1,7)>='" + month + "' and substr(date_time,1,7)<='" + data_month_beg + "' and line_no='" + line_no + "'   ");
            al_line.Add("delete from  ts_user where  substr(date_time,1,7)>='" + month + "' and substr(date_time,1,7)<='" + data_month_beg + "'  and line_no='" + line_no + "'   ");
            al_line.Add("insert into  ts_line select * from v_ts_line where  substr(date_time,1,7)>='" + month + "' and substr(date_time,1,7)<='" + data_month_beg + "'  and line_no='" + line_no + "'   ");
            al_line.Add("insert into  ts_user select * from v_ts_user where  substr(date_time,1,7)>='" + month + "' and substr(date_time,1,7)<='" + data_month_beg + "'  and line_no='" + line_no + "'   ");
          //  al_line.Add("insert into ts_line_select(line_no,year_mon,username) values('" + line_no + "','" + month + "','" + username + "') ");
            DBHelper.DBHelper.ExecuteCommand(al_line);



            string content = "set feed off \r\n";
            content = content + "set feed off \r\n";
            content = content + "set heading off \r\n";
            content = content + "set newpage none \r\n";
            content = content + "set trimout off \r\n";
            content = content + "set long 10000 \r\n";
            // and line_no='" + line_no + "';
            content = content + "spool ./spooldata/" + line_no + name + data_month_beg + ".txt \r\n";
            if (name == "line")
            {

                content = content + "select LINE_NO||' '||DATE_TIME||' '||TOTAL_Q||' '||USER_Q||' '||LOST||' '||RATE   from ts_line where substr(date_time,1,7)>='" + month + "' and substr(date_time,1,7)<='" + data_month_beg + "' and line_no='" + line_no + "' ;\r\n";
            }
            if (name == "user")
            {
                content = content + "select USER_ID||'  '||DATE_TIME||' '||USED_Q||'  '||LINE_NO||' '||CT   from ts_user where substr(date_time,1,7)>='" + month + "' and substr(date_time,1,7)<='" + data_month_beg + "' and line_no='" + line_no + "'; \r\n ";
            }
            if (name == "moniter")
            {
                content = content + @"select     USER_ID||'  '||JL_POINT||'  '||DATE_TIME||'  '||A_V||'  '||B_V||'  '||C_V||'  '||A_V_A||'  '||B_V_B||'  '||C_V_C||'  '||CURR_KW||'  '||CURR_KVAR||'  '||A_KW||'  '||B_KW||'  '||C_KW||'  '||TOTAL_KW||'  '||A_KW_S||'  '||B_KW_S||'  '||C_KW_S||'  '||KW_BANLACE||'  '||M_NO||'  '||A_BALANCE_NO||'  '||LINE_NO||'  '||M_ZYB||'  '||M_FYB||'  '||M_ZWB||'  '||M_FWB||'  '||F_ZYB||'  '||F_FYB||'  '||F_ZWB||'  '||F_FWB
                from V_TS_MONITER 
                 where substr(date_time,1,7)>='" + month + "' and substr(date_time,1,7)<='" + data_month_beg + "'  and line_no='" + line_no + "';\r\n ";
            }
            content = content + " spool off  \r\n  ";
            content = content + " exit ";
            if (!File.Exists("spool.txt") == true)
            {
                File.Delete("spool.sql");
                FileStream myFs = new FileStream("spool.sql", FileMode.Create);

                StreamWriter mySw = new StreamWriter(myFs, System.Text.Encoding.GetEncoding("gb2312"));
                mySw.Write(content);
                mySw.Close();
                myFs.Close();
                //  MessageBox.Show("写入成功");
            }
            else
            {
                File.Delete("spool.sql");
                FileStream myFs = new FileStream("spool.sql", FileMode.Create);
                StreamWriter mySw = new StreamWriter(myFs, System.Text.Encoding.GetEncoding("gb2312"));
                mySw.Write(content);
                mySw.Close();
                myFs.Close();
                //MessageBox.Show("写入成功");
            }
            RunCmd();

        }

        private void z_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv != null) 
            {
                Rectangle rect = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgv.RowHeadersWidth - 4, e.RowBounds.Height);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgv.RowHeadersDefaultCellStyle.Font, rect, dgv.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in z.Rows)
            {

                DataRow dr = dt_down.NewRow();
                if (bool.Parse(gr.Cells[0].EditedFormattedValue.ToString()))
                {
                    string line_no = gr.Cells[1].EditedFormattedValue.ToString();
                    string data_month = gr.Cells[2].EditedFormattedValue.ToString();
                    string data_month_beg = data_month;
                     data_month = DateTime.Parse(data_month + "-1").AddMonths(-int.Parse(textBox4.Text)).ToString("yyyy-MM");
                   DataRow[] drs=  dt_down.Select("线路='" + line_no + "' and 开始年月='" + data_month_beg + "' and 结束年月='" + data_month + "' ");
                   if (drs.Length > 0)
                   {

                   }
                   else
                   {
                       dr["线路"] = line_no;
                       dr["结束年月"] = data_month_beg;

                       dr["开始年月"] = data_month;
                       dt_down.Rows.Add(dr);
                   }
                 
                }
            }
            dataGridView1.DataSource = dt_down;
            MessageBox.Show("加入下载列表完成");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int i = 0;
            
           List<DataRow> dr_list = new List<DataRow>();
            foreach (DataGridViewRow gr in dataGridView1.Rows)
            {
               
                if (bool.Parse(gr.Cells[0].EditedFormattedValue.ToString()))
                {
                    string line_no = gr.Cells[1].EditedFormattedValue.ToString();
                    string data_month_beg = gr.Cells[2].EditedFormattedValue.ToString();
                    string data_month_end = gr.Cells[3].EditedFormattedValue.ToString();
                    dr_list.Add(dt_down.Rows[i]);                   
                }

                i++;
            }
            for (int j = 0; j < dr_list.Count; j++)
            {
                dt_down.Rows.Remove(dr_list[j]);
            }
                dataGridView1.DataSource = dt_down;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dt_down.Rows.Clear();
            dataGridView1.DataSource = dt_down;
        }

        private void z_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
              if(e.RowIndex>=0)
              {
                DataRow dr = dt_down.NewRow();
                
                    string line_no =z.Rows[e.RowIndex].Cells[2].EditedFormattedValue.ToString();
                    string data_month = z.Rows[e.RowIndex].Cells[3].EditedFormattedValue.ToString();
                    string data_month_beg = data_month;
                    data_month = DateTime.Parse(data_month + "-1").AddMonths(-int.Parse(textBox4.Text)).ToString("yyyy-MM");
                    DataRow[] drs = dt_down.Select("线路='" + line_no + "' and 开始年月='" + data_month_beg + "' and 结束年月='" + data_month + "' ");
                    if (drs.Length > 0)
                    {

                    }
                    else
                    {
                        dr["线路"] = line_no;
                        dr["结束年月"] = data_month_beg;

                        dr["开始年月"] = data_month;
                        dt_down.Rows.Add(dr);
                    }

               
            }
            dataGridView1.DataSource = dt_down;
            MessageBox.Show("加入下载列表完成");
        }
    }
}
