using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DBHelper;
using System.Diagnostics;
using System.Xml;

namespace LostMinerLib
{
    public partial class Wizard : Form
    {

        public Wizard()
        {
            InitializeComponent();
            //StreamReader sr = new StreamReader("webi.log", System.Text.Encoding.UTF8);
            //string txt = sr.ReadToEnd();
            //sr.Close();
            //StreamWriter sw = new StreamWriter("webi.log");

            //sw.WriteLine("读取接口数据从  至");
            //sw.WriteLine("数据转换从至" );
            //sw.WriteLine("-------------------");
            //sw.WriteLine(txt);
            //sw.Close();
           // RunCmd();
            //  SetupPath();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBiniClass iniclass = new DBiniClass(".\\system.ini");

            iniclass.IniWriteValue("netlink", "linkmodel", "sqlite");

            this.Hide();
            TimeSearcher.TimeSearcherForm tf = new TimeSearcher.TimeSearcherForm();
            tf.ShowDialog();

            //Application.ExitThread();
            // this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DBiniClass iniclass = new DBiniClass(".\\system.ini");

            iniclass.IniWriteValue("netlink", "linkmodel", "oracle");
            //RunCmd();
            this.Hide();
            LostMinerLib.NetLine nf = new NetLine();
            nf.ShowDialog();

        }

        private void Wizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
        public void SetupPath()
        {
            //            string Rversion = "R-3.0.2";
            //            var oldPath = System.Environment.GetEnvironmentVariable("PATH");
            //            var rPath = System.Environment.Is64BitProcess ?
            //                                   string.Format(@"C:\Program Files\R\{0}\bin\x64", Rversion) :
            //                                   string.Format(@"C:\Program Files\R\{0}\bin\i386", Rversion);
            //            if (!Directory.Exists(rPath))
            //                throw new DirectoryNotFoundException(
            //                  string.Format(" R.dll not found in : {0}", rPath));
            //            var newPath = string.Format("{0}{1}{2}", rPath,
            //                                         System.IO.Path.PathSeparator, oldPath);
            //            System.Environment.SetEnvironmentVariable("PATH", newPath);

            //            REngine rengine;
            //          // rengine.
            //       //   RDotNet.lo
            //            //  RGraphAppHook cbt;
            //            try
            //            {
            //                rengine = REngine.CreateInstance("RDotNet");
            //               // return;
            //            }
            //            catch (Exception e)
            //            {


            //               LabelMessage.Text = e.Message.ToString();
            //                return;
            //            }

            //            // Initializes settings.
            //           rengine.Initialize();

            //            RGraphAppHook cbt = new RGraphAppHook { GraphControl = panel1 };
            //            // .NET Framework array to R vector.
            //            NumericVector group1 = rengine.CreateNumericVector(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            //            rengine.SetSymbol("group1", group1);
            //            // Direct parsing from R script.
            //            NumericVector group2 = rengine.Evaluate("group2 <- c(1,2,3,4,5,6,7,8,9,10)").AsNumeric();
            //           //  string SQLITECON = "library(\"RSQLite\");drv <- dbDriver(\"SQLite\");con <- dbConnect(drv, \"C:/project/vs/lostminer/bin/Debug/lostminer.s3db\");TSLINE = dbGetQuery(con,\"select  *  from ts_line\");plot(TSLINE$DATE_TIME,TSLINE$TOTAL_Q)";
            //            string SQLITECON = "library(\"RSQLite\");drv <- dbDriver(\"SQLite\");" +
            //                            "con <- dbConnect(drv, \"C:/project/vs/lostminer/bin/Debug/lostminer.s3db\");" +
            //            "TSLINE = dbGetQuery(con,\"SELECT * from ts_line \");" +
            //           "TSLINE;";
            //            // "plot(TSLINE$USER_Q,TSLINE$TOTAL_Q);";

            //            try
            //             {
            //                 cbt.Install();
            //            DataFrame df  =rengine.Evaluate(SQLITECON).AsDataFrame() ;
            ////LabelMessage.Text = CV.ToString();
            //                 cbt.Uninstall();
            //             }
            //             catch(Exception er)
            //             {  
            //           //   throw
            //             }

            //            // Test difference of mean and get the P-value.
            //            //GenericVector testResult = rengine.Evaluate("t.test(group1, group2)").AsList();
            //            //double p = testResult["p.value"].AsNumeric().First();

            //            //LabelMessage.Text += "Group1: " + string.Join(", ", group1) + "<br>";
            //            //LabelMessage.Text += "Group2: " + string.Join(", ", group2) + "<br>";
            //            //LabelMessage.Text += "P-value =" + p + "<br>";
            //            //LabelMessage.Text += DateTime.Now.ToLongTimeString();
            //            string rid = rengine.ID;
            //            rengine.ForceGarbageCollection();
            //            //rengine.Close();
            //            //rengine.Dispose();
            //            cbt = null;
            //            rengine = null;

            //           string  ddd= System.Environment.GetEnvironmentVariable("PATH");
            //           System.Environment.SetEnvironmentVariable("PATH", oldPath);
            //            ddd = System.Environment.GetEnvironmentVariable("PATH");
        }

        private void RunCmd()
        {
            DateTime db = System.DateTime.Now;
            List<LostMinerLib.Util.HistoryValue> lh = new List<LostMinerLib.Util.HistoryValue>();
            XmlDocument xd = new XmlDocument();
            xd.Load("moniter.xml");
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
                            //  DataRow dr = dt_moniter_c.NewRow();
                            LostMinerLib.Util.HistoryValue hv = new LostMinerLib.Util.HistoryValue();
                            //dr["user_id"] = user_dt.Rows[i]["user_id"].ToString();

                            //   dr["cl_name"] = conver_point(pointname);
                            //  DataRow[] drs = user_dt.Select("user_id like '" + pointname.Split('-')[6] + "/%'");
                            // dr["user_id"] = drs[0]["user_id"].ToString();
                            hv.username = pointname.Split('-')[6];
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
                                    hv.datatime =DateTime.Parse( nlv[nv].InnerText).ToString("yyyy-MM-dd hh:mm:ss");
                                }
                            }
                            lh.Add(hv);
                            // dt_moniter_c.Rows.Add(dr);
                        }
                    }
                }
            }
         DataTable dt=   convert_hv_dt(lh);
            System.DateTime de = System.DateTime.Now;
            DataView dv = dt.DefaultView;
            dv.Sort = "date_time asc";
            DataTable dts = dv.ToTable();
            string dd = db + "-" + de;
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
                     DataRow dr ;
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
                    { }
                    if (drs.Length == 0)
                    {
                        dt_hv.Rows.Add(dr);
                    }
                }


            }

            return dt_hv;
        }

    }
}
