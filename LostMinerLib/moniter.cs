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
    public partial class moniter : Form
    {
        public moniter(DataTable dt)
        {
            InitializeComponent();
            dgv_fuzhu.AutoGenerateColumns = false;
            dgv_fuzhu.DataSource = dt;
        }
    }
}
