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
    public partial class ReadWord : Form
    {
        public ReadWord()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
          //  webBrowser1.Navigate(Application.StartupPath+"\\帮助文档.doc");
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = Application.StartupPath + "\\帮助文档.doc";
            p.Start();

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
           
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            
        }
    }
}
