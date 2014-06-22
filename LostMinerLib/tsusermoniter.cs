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
    public partial class tsusermoniter : Form
    {
        public tsusermoniter()
        {
            InitializeComponent();
        }
        public tsusermoniter(DataTable dt)
        {
            InitializeComponent();
            dataGridView1.DataSource = dt;
        }
    }
}
