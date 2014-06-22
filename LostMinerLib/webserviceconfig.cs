using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LostMinerLib.Util;

namespace LostMinerLib
{
    public partial class webserviceconfig : Form
    {
        public webserviceconfig()
        {
            InitializeComponent();

            INIClass iniclass = new INIClass(Application.StartupPath + @"\system.ini");
            //string filename = this._openTqdFileDlg.FileNames[0].ToString();
            string url_str = iniclass.IniReadValue("webservice", "webservice");

            string line_interface = iniclass.IniReadValue("webservice", "line_interface");
            string user_interface = iniclass.IniReadValue("webservice", "user_interface");
            string user_moniter_interface = iniclass.IniReadValue("webservice", "user_moniter_interface");
            webservice_url.Text = url_str;
            line.Text = line_interface;
            user.Text = user_interface;
            user_moniter.Text = user_moniter_interface;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            INIClass iniclass = new INIClass(Application.StartupPath + @"\system.ini");
            iniclass.IniWriteValue("webservice", "webservice", webservice_url.Text);
            iniclass.IniWriteValue("webservice", "line_interface", line.Text);
            iniclass.IniWriteValue("webservice", "user_interface", user.Text);
            iniclass.IniWriteValue("webservice", "user_moniter_interface", user_moniter.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
