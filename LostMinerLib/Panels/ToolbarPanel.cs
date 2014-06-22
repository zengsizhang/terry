namespace TimeSearcher.Panels
{
   
    using System;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher;
    using System.IO;
    using LostMinerLib;

    public class ToolbarPanel : Panel
    {
        private DateTimePicker _b_date_time_pick;
        private Button _btn_sure;
        private Button _btnSearchbox;
        private Button _btnSelect;
        private readonly int _btnSide = 20;
        private Button _btnTimebox;
        private ComboBox _cbNumVariables;
        private DateTimePicker _e_date_time_pick;
        private Label _lblNumDynVar;
        private ToolBar _toolbar;
        private ToolTip _toolTip1;
        private TimeSearcherForm _tsForm;
        public Label b_e_time;
        private Label b_lable;
        private Button btn_m_l;
        private Button btn_m_r;
        private Label e_lab;

        public ToolbarPanel(ToolBar tb, TimeSearcherForm tsForm)
        {
            this._toolbar = tb;
            this._tsForm = tsForm;
            this._btnSide = tb.Height;
            this._toolTip1 = new ToolTip();
            this._toolTip1.AutoPopDelay = 0x1388;
            this._toolTip1.InitialDelay = 0x3e8;
            this._toolTip1.ReshowDelay = 500;
            this._toolTip1.ShowAlways = true;
            this.AutoScroll = true;
            string dir = Path.GetDirectoryName(Application.ExecutablePath);
            Image ii = Image.FromFile(dir+"\\"+"tsDirs\\images\\searchbox.gif");

            this.AddButton(ref this._btnSearchbox, Image.FromFile(dir + "\\" + "tsDirs\\images\\searchbox.gif"), "Draw Searchbox", null);
           // this.AddButton(ref this._btnTimebox, Image.FromFile(dir + "\\" + "tsDirs\\images\\hand.gif"), "Select Searchbox", null);
            this.btn_m_r = new Button();
            this.btn_m_r.Height = this._btnSide;
            this.btn_m_r.Width = 100;
            this.btn_m_r.Dock = DockStyle.Left;
            this.btn_m_r.Text = "右移10天";
            this.btn_m_r.Click += new EventHandler(this.btn_m_r_Click);
            this._toolbar.Controls.Add(this.btn_m_r);
            this.btn_m_l = new Button();
            this.btn_m_l.Height = this._btnSide;
            this.btn_m_l.Width = 100;
            this.btn_m_l.Dock = DockStyle.Left;
            this.btn_m_l.Text = "左移10天";
            this.btn_m_l.Click += new EventHandler(this.btn_m_l_Click);
            this._toolbar.Controls.Add(this.btn_m_l);
            this.b_e_time = new Label();
            this.b_e_time.Height = this._btnSide;
            b_e_time.Visible = false;
            this.b_e_time.Width = 300;
            this.b_e_time.Text = "";
            this.b_e_time.TextAlign = ContentAlignment.MiddleCenter;
            this.b_e_time.Dock = DockStyle.Left;
            this._toolbar.Controls.Add(this.b_e_time);
            this._lblNumDynVar = new Label();
            this._lblNumDynVar.Height = this._btnSide;
            this._lblNumDynVar.Text = "";
            this._lblNumDynVar.Visible = false;
            this._lblNumDynVar.TextAlign = ContentAlignment.MiddleLeft;
            this._lblNumDynVar.Dock = DockStyle.Left;
            this._lblNumDynVar.BackColor = Color.LightGray;
            this._toolbar.Controls.Add(this._lblNumDynVar);
            this._btn_sure = new Button();
            this._btn_sure.Height = this._btnSide;
            this._btn_sure.Width = 80;
            this._btn_sure.Dock = DockStyle.Left;
            this._btn_sure.Text = "确定";
           // this._btn_sure.Image = Image.FromFile("tsDirs/images/rectlock.gif");
            this._btn_sure.Click += new EventHandler(this._btn_sure_Click);
        
            this._e_date_time_pick = new DateTimePicker();
            this._e_date_time_pick.Height = this._btnSide;
            this._e_date_time_pick.Width = 110;
            this._e_date_time_pick.Dock = DockStyle.Left;
            this._toolbar.Controls.Add(this._e_date_time_pick);
            this.e_lab = new Label();
            this.e_lab.Height = this._btnSide;
            this.e_lab.Width = 20;
            this.e_lab.Text = "TO";
            this.e_lab.TextAlign = ContentAlignment.MiddleRight;
            this.e_lab.Dock = DockStyle.Left;
            this.e_lab.BackColor = Color.LightGray;
            this._toolbar.Controls.Add(this.e_lab);
            this._b_date_time_pick = new DateTimePicker();
            this._b_date_time_pick.Height = this._btnSide;
            this._b_date_time_pick.Width = 110;
            this._b_date_time_pick.Dock = DockStyle.Left;
            this._toolbar.Controls.Add(this._b_date_time_pick);
            this.b_lable = new Label();
            this.b_lable.Height = this._btnSide;
            this.b_lable.Width = 30;
            this.b_lable.Text = "FROM";
            this.b_lable.TextAlign = ContentAlignment.MiddleRight;
            this.b_lable.Dock = DockStyle.Left;
            this.b_lable.BackColor = Color.LightGray;
            this._toolbar.Controls.Add(this.b_lable);
            this._toolbar.Controls.Add(this._btn_sure);
            this._cbNumVariables = new ComboBox();
            this._cbNumVariables.DropDownStyle = ComboBoxStyle.DropDownList;
            this._cbNumVariables.Width = 150;
            this._cbNumVariables.Dock = DockStyle.Left;
            DBHelper.DBiniClass iniclass = new DBHelper.DBiniClass(".\\system.ini");

            string linkmodel = iniclass.IniReadValue("netlink", "linkmodel");
            string username = iniclass.IniReadValue("username", "username");
            this._cbNumVariables.Items.Clear();
             DataTable dt ;
            if (linkmodel == "oracle")
            {
                dt = DBHelper.DBHelper.GetDataSet("select  line_no,max(id) as  id  from ts_line_select where username='" + username + "'  and rownum<=20 group by line_no order by id desc ").Tables[0];
            }
            else
            {
                dt= DBHelper.DBHelper.GetDataSet("select distinct line_no from ts_line").Tables[0];
            }
            this.add_item(dt);
            if (this._cbNumVariables.Items.Count > 0)
            {
                tsForm.set_line_lost(this._cbNumVariables.SelectedItem.ToString());
            }
            this._toolbar.Controls.Add(this._cbNumVariables);
            Label label = new Label();
            label.Text = "线路";
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Dock = DockStyle.Left;
            label.BackColor = Color.LightGray;
            this._toolbar.Controls.Add(label);
            ToolBarButton button = new ToolBarButton();
            button.Style = ToolBarButtonStyle.Separator;
            this._toolbar.Buttons.Add(button);
            this._btnSearchbox.Enabled = true;
        }

        private void _btn_sure_Click(object obj, EventArgs ea)
        {
            this._tsForm.set_curr_begin_time(this._b_date_time_pick.Value);
            this._tsForm.set_curr_end_time(this._e_date_time_pick.Value);
            this._tsForm.set_line_lost(this._cbNumVariables.SelectedItem.ToString());
           // _tsForm.Invoke(_tsForm.mydelea);
            //  _tsForm.btn_sure();
            //DataTable dt = DBHelper.DBHelper.GetDataSet("select * from v_ts_moniter t where t.line_no='" + this._cbNumVariables.SelectedItem.ToString() + "'").Tables[0];
            //_tsForm.set_dt(dt);
            this._tsForm.re_read = true;
            this._tsForm.openFile();
            this._tsForm.fill_user_dt();
            this._tsForm.RefillDetailsList("", false);
            this._tsForm.week_var();
        }

        public void add_item(DataTable dt)
        {
            
               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this._cbNumVariables.Items.Add(dt.Rows[i][0].ToString());
                }
                if (this._cbNumVariables.Items.Count > 0)
                {
                    this._cbNumVariables.SelectedIndex = 0;
               }
            
        }

        public void add_item(DataTable dt, bool reload)
        {
            string old_select = string.Empty;
            if (this._cbNumVariables.Items.Count > 0)
            {
                old_select = this._cbNumVariables.SelectedItem.ToString();
            }
            this._cbNumVariables.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this._cbNumVariables.Items.Add(dt.Rows[i][0].ToString());
                if (old_select == dt.Rows[i][0].ToString())
                {
                    this._cbNumVariables.SelectedIndex = i;
                }
                else
                {
                    this._cbNumVariables.SelectedIndex = 0;
                }
                this._tsForm.set_line_lost(this._cbNumVariables.SelectedItem.ToString());
            }
            if (!reload && (this._cbNumVariables.Items.Count > 0))
            {
                this._cbNumVariables.SelectedIndex = 0;
                this._tsForm.set_line_lost(this._cbNumVariables.SelectedItem.ToString());
            }
        }

        public void AddButton(ref Button btn, Image img, string tooltip, string txt)
        {
            btn = new Button();
            btn.BackColor = Color.LightGray;
            if (img != null)
            {
                btn.Width = this._btnSide;
                btn.Height = this._btnSide;
                btn.Image = img;
            }
            if (txt != null)
            {
                btn.Text = txt;
                btn.TextAlign = ContentAlignment.MiddleCenter;
            }
            this._toolTip1.SetToolTip(btn, new string(tooltip.ToCharArray()));
            btn.Dock = DockStyle.Left;
            btn.Click += new EventHandler(this.OnToolbarButtonClicked);
            btn.Enabled = true;
            this._toolbar.Controls.Add(btn);
        }

        private void btn_m_l_Click(object obj, EventArgs ea)
        {
            this._tsForm.set_curr_begin_time(this._tsForm.get_curr_begin_time().AddDays(-10.0));
            this._tsForm.set_curr_end_time(this._tsForm.get_curr_end_time());
            this._tsForm.set_line_lost(this._cbNumVariables.SelectedItem.ToString());
            this._tsForm.re_read = true;
            this._tsForm.openFile();
            this._tsForm.fill_user_dt();
            this._tsForm.RefillDetailsList("", false);
            this._tsForm.week_var();
        }

        private void btn_m_r_Click(object obj, EventArgs ea)
        {
            this._tsForm.set_curr_begin_time(this._tsForm.get_curr_begin_time());
            this._tsForm.set_curr_end_time(this._tsForm.get_curr_end_time().AddDays(10.0));
            this._tsForm.set_line_lost(this._cbNumVariables.SelectedItem.ToString());
            this._tsForm.re_read = true;
            this._tsForm.openFile();
            this._tsForm.fill_user_dt();
            this._tsForm.RefillDetailsList("", false);
            this._tsForm.week_var();
        }

        public string getLineno()
        {
            return this._cbNumVariables.SelectedItem.ToString();
        }
        public void SetLineno(string line_no)
        {
            if (_cbNumVariables.Items.Contains(line_no))
            {
                for (int i = 0; i < this._cbNumVariables.Items.Count; i++)
                {
                    if (this._cbNumVariables.Items[i].ToString() == line_no)
                    {
                        this._cbNumVariables.SelectedItem = this._cbNumVariables.Items[i];
                    }
                }
            }
        }

        private void numVariablesSelectionChanged(object obj, EventArgs ea)
        {
            this._tsForm.setNumDisplayedVariables(this._cbNumVariables.SelectedIndex + 1);
            this._b_date_time_pick.Value = DateTime.Now;
            this._e_date_time_pick.Value = DateTime.Now;
        }

        protected void OnToolbarButtonClicked(object sender, EventArgs ea)
        {
            TimeSearcher.DataSet dataSet = this._tsForm.DataSet;
            if (((Button) sender) == this._btnSelect)
            {
                this._tsForm.StrMode = "select";
            }
            else if (((Button) sender) == this._btnTimebox)
            {
                this._tsForm.StrMode = "timebox";
            }
            else if (((Button) sender) == this._btnSearchbox)
            {
                this._tsForm.StrMode = "searchbox";
            }
        }

        public void selectNumVariables(int i)
        {
            this._cbNumVariables.SelectedIndex = i - 1;
        }

        public void set_pic_time(DateTime b_time, DateTime e_time)
        {
            this._b_date_time_pick.Value = b_time;
            this._e_date_time_pick.Value = e_time;
        }

        public int NumDynVar
        {
            set
            {
                int num = value;
                this._cbNumVariables.Items.Clear();
             
           
                for (int i = 0; i <= num; i++)
                {
                    this._cbNumVariables.Items.Add(i);
                }
                this._lblNumDynVar.Text = " of " + num;
                this._lblNumDynVar.Width = this._lblNumDynVar.PreferredWidth;
          
            }
        }
    }
}

