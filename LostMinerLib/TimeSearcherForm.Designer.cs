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
    using System.Drawing;
    partial class TimeSearcherForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private MenuItem _aboutMenuItem;
        private MenuItem _analogousTsFinderMenuItem;
        private Panel _bottom_right_panel;
        private Panel _bottomPanel;
        private ListView _calcAttrList;
        private CalcAttrListManager _calcAttrListManager;
        private TabPage _calculatedAttrTabPage;
        private ComboBox _cbIndividualItems;
        private TabPage _chayiTabpage;
        private MenuItem _ci1CombinerMenuItem;
        private MenuItem _civToTqdConverterMenuItem;
        private MenuItem _clearAllQueriesMenuItem;
        
        private TabPage _detailpage;
        private ListView _detailsList;
        private TabControl _detailTabControl;
        private MenuItem _exitMenuItem;
        private MenuItem _expMenuItem;
        private MenuItem _fileMenu;
        private MenuItem _filterAttributesMenuItem;
        private MenuItem _filterTfnMenuItem;
        private MenuItem _filterUnselectedMenuItem;
        private MenuItem _forecastingMenuItem;
        private MenuItem _gridMenuItem;
        private MenuItem _helpMenu;
        private MenuItem _hideFilteredMenuItem;
        private Splitter _hSplitter;
        private ImageList _imgListButtons;
        private TabPage _individualsTabPage;
        private bool _isControlKeyDown;
        private bool _isDataSetLoaded;
        private ItemVariablePanel[] _itemPanels;
        private ListView _itemsList;
        private TimeSearcher.ItemsListManager _itemsListManager;
        private TabPage _itemsListTabPage;
        private TabControl _itemsTabControl;
        private LeftPanel _leftPanel;
        private LeftPanel _leftRiverPanel;
        private MenuItem _loadAtrMenuItem;
        private MenuItem _logMenuItem;
        private MainMenu _mainMenu;
        private Splitter _mainSplitter1;
        private TabControl _mainTabControl;
        private int _numDisplayedVariables;
        private int _numInitDisplayedVariables;
        private OpenFileDialog _openAtrFileDlg;
        private MenuItem _openMenuItem;
        private OpenFileDialog _openTqdFileDlg;
        private OptionsForm _optionsForm;
        private OverviewVariablePanel _overviewVariablePanel;
        private MenuItem _prefsMenuItem;
        private ProgressBar _progressBar;
        private ContextMenu _qpContextMenu;
        private MenuItem _quickRefMenuItem;
        private Panel _rightPanel;
        private RiverPlotView _riverPlotView;
        private TabPage _riverTabPage;
        private int[] _savedSelectedItemIdx;
        private MenuItem _searchOptsMenuItem;
        private int _selectedIndividualsVariable;
        private MenuItem _selectMatchesMenuItem;
        private MenuItem _separator1MenuItem;
        private MenuItem _separator2MenuItem;
        private MenuItem _setSizeMenuItem;
        private MenuItem _showForecastSourceMenuItem;
        private MenuItem _showSelectedItemsMenuItem;
        private MenuItem _showSelectionBoxesMenuItem;
        private MenuItem _showTimeLineMenuItem;
        private MenuItem _missingMenuItem;
        private MenuItem _missingDetail;
        private StatusBar _statusBar;
        private DataTable _sx_data;
        private DataTable _sx_datateble;
        private DataTable _sx_table;
        private DataGridView _sxdgv;
        private TabPage _sxTabPage;
        private MenuItem _syncDetailsListMenuItem;
        private MenuItem _syncOverviewMenuItem;
        public ToolbarPanel _tbPanel;
        private MenuItem _testMenuItem;
        private ToolBar _toolbar;
        private MenuItem _toolsMenu;
        private Panel _topPanel;
        private MenuItem _undoFilterUnselectedMenuItem;
        private TabPage _userpage;
        private TabControl _userTabControl;
        private TabPage _userwarnpage;
        private VariablesView _varView;
        private MenuItem _viewMenu;
        private MenuItem _wizardMenuItem;
        private double[,] _yaozhi;
        private DataGridViewTextBoxColumn avg;
        private CheckBox avg_c;
        private TextBox avg_t_f;
        private TextBox avg_t_t;
        private Button btn_chayi;
        private Button btn_check_all;
        private Button btn_save_sx;
        public bool bUpdating;
        private DataGridView chanyi_dgv;
        private Chart chart_chayi;
        private Chart chart_sx;
        private DataGridViewCheckBoxColumn chkBxSelect ;
        private DataGridViewCheckBoxColumn chkCoSelect;
        private DataGridViewCheckBoxColumn chkUserWarn ;
    
        
        private DateTime curr_begin_time;
        private DateTime curr_end_time;
        private string date_str_excel;
        private DataGridView dgvSelectAll;
        private DataGridView dgvUserWarn;
        private DataTable dtuserrate;
        private DataTable dtyaozhi;
        private GroupBox g_o;
        private GroupBox g_s;
        private CheckBox HeaderCheckBox;
        private ImageList imglst_genre = new ImageList();
        private ImageList imglst_rate = new ImageList();
        private CheckBox is_check;
        private CheckBox is_computer;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private string line_lost;
        public bool load_task;
        private DataGridViewTextBoxColumn m1;
        private DateTime max_date_time;
        private MenuItem menuItem1;
        private MenuItem menuItem3;
        private DateTime min_date_time;
        private Button out_put;
        private DataGridViewTextBoxColumn pUserWarn ;
        public bool re_read = true;
        private Label result_l;
        public string search_b_time = string.Empty;
        public string search_e_time = string.Empty;
        private importdataproc showproc;
        private double[,] source_double;
        private double[,] source_yaozhi;
        private Splitter splitter1;
        public string StrMode;
        private DataGridViewTextBoxColumn sx_bili;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private int task_id;
        private string task_str;
        private ToolBar tbBtn;
        private ToolBarButton tbrBtnAngularQuery;
        private ToolBarButton tbrBtnClear;
        private ToolBarButton tbrBtnFlip;
        private ToolBarButton tbrBtnLeadLag;
        private ToolBarButton tbrBtnSearch;
        private ToolBarButton tbrBtnSearchbox;
        private ToolBarButton tbrBtnSelect;
        private ToolBarButton tbrBtnTimebox;
        private ToolBarButton tbrBtnVTTimebox;
        private ToolBarButton toolBarSeparator;
        private string[] user_c;
        private DataGridViewTextBoxColumn userid ;
        private DataGridViewTextBoxColumn c_rule;
        private DataGridViewTextBoxColumn useridWarn ;
        private CheckBox var_c;
        private DataGridViewTextBoxColumn var_p;
        private TextBox var_t_f;
        private TextBox var_t_t;
        private BackgroundWorker worker;
        private DataGridViewCheckBoxColumn xyCoSelect;
        private DataGridViewTextBoxColumn yaozhi;
        private double[,] numArraySimilar;
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            DevExpress.XtraScheduler.TimeRuler timeRuler1 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler2 = new DevExpress.XtraScheduler.TimeRuler();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeSearcherForm));
            this.chkBxSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chkCoSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chkUserWarn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.userid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_rule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.useridWarn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.var_p = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xyCoSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.yaozhi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pUserWarn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sx_bili = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this._fileMenu = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this._loadAtrMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this._openMenuItem = new System.Windows.Forms.MenuItem();
            this._exitMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this._viewMenu = new System.Windows.Forms.MenuItem();
            this._gridMenuItem = new System.Windows.Forms.MenuItem();
            this._toolsMenu = new System.Windows.Forms.MenuItem();
            this._forecastingMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this._selectMatchesMenuItem = new System.Windows.Forms.MenuItem();
            this._helpMenu = new System.Windows.Forms.MenuItem();
            this._clearAllQueriesMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this._aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this._undoFilterUnselectedMenuItem = new System.Windows.Forms.MenuItem();
            this._separator1MenuItem = new System.Windows.Forms.MenuItem();
            this._searchOptsMenuItem = new System.Windows.Forms.MenuItem();
            this._separator2MenuItem = new System.Windows.Forms.MenuItem();
            this._setSizeMenuItem = new System.Windows.Forms.MenuItem();
            this._prefsMenuItem = new System.Windows.Forms.MenuItem();
            this._showTimeLineMenuItem = new System.Windows.Forms.MenuItem();
            this._showForecastSourceMenuItem = new System.Windows.Forms.MenuItem();
            this._syncDetailsListMenuItem = new System.Windows.Forms.MenuItem();
            this._syncOverviewMenuItem = new System.Windows.Forms.MenuItem();
            this._showSelectedItemsMenuItem = new System.Windows.Forms.MenuItem();
            this._hideFilteredMenuItem = new System.Windows.Forms.MenuItem();
            this._showSelectionBoxesMenuItem = new System.Windows.Forms.MenuItem();
            this._logMenuItem = new System.Windows.Forms.MenuItem();
            this._expMenuItem = new System.Windows.Forms.MenuItem();
            this._filterUnselectedMenuItem = new System.Windows.Forms.MenuItem();
            this._filterAttributesMenuItem = new System.Windows.Forms.MenuItem();
            this._filterTfnMenuItem = new System.Windows.Forms.MenuItem();
            this._analogousTsFinderMenuItem = new System.Windows.Forms.MenuItem();
            this._wizardMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this._civToTqdConverterMenuItem = new System.Windows.Forms.MenuItem();
            this._ci1CombinerMenuItem = new System.Windows.Forms.MenuItem();
            this._quickRefMenuItem = new System.Windows.Forms.MenuItem();
            this._testMenuItem = new System.Windows.Forms.MenuItem();
            this._imgListButtons = new System.Windows.Forms.ImageList(this.components);
            this.tbBtn = new System.Windows.Forms.ToolBar();
            this.toolBarSeparator = new System.Windows.Forms.ToolBarButton();
            this.tbrBtnSelect = new System.Windows.Forms.ToolBarButton();
            this.tbrBtnTimebox = new System.Windows.Forms.ToolBarButton();
            this.tbrBtnSearchbox = new System.Windows.Forms.ToolBarButton();
            this.tbrBtnVTTimebox = new System.Windows.Forms.ToolBarButton();
            this.tbrBtnAngularQuery = new System.Windows.Forms.ToolBarButton();
            this.tbrBtnLeadLag = new System.Windows.Forms.ToolBarButton();
            this.tbrBtnFlip = new System.Windows.Forms.ToolBarButton();
            this.tbrBtnSearch = new System.Windows.Forms.ToolBarButton();
            this.tbrBtnClear = new System.Windows.Forms.ToolBarButton();
            this._qpContextMenu = new System.Windows.Forms.ContextMenu();
            this._openAtrFileDlg = new System.Windows.Forms.OpenFileDialog();
            this._openTqdFileDlg = new System.Windows.Forms.OpenFileDialog();
            this._statusBar = new System.Windows.Forms.StatusBar();
            this._bottomPanel = new System.Windows.Forms.Panel();
            this._bottom_right_panel = new System.Windows.Forms.Panel();
            this.g_s = new System.Windows.Forms.GroupBox();
            this.检查信息 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.out_put = new System.Windows.Forms.Button();
            this.btn_save_sx = new System.Windows.Forms.Button();
            this._detailTabControl = new System.Windows.Forms.TabControl();
            this._detailpage = new System.Windows.Forms.TabPage();
            this._detailsList = new System.Windows.Forms.ListView();
            this._userTabControl = new System.Windows.Forms.TabControl();
            this._userpage = new System.Windows.Forms.TabPage();
            this.dgvSelectAll = new System.Windows.Forms.DataGridView();
            this.is_check = new System.Windows.Forms.CheckBox();
            this.is_computer = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.result_l = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._hSplitter = new System.Windows.Forms.Splitter();
            this._topPanel = new System.Windows.Forms.Panel();
            this._mainTabControl = new System.Windows.Forms.TabControl();
            this._allTabPage = new System.Windows.Forms.TabPage();
            this._tab_fuzhu = new System.Windows.Forms.TabPage();
            this._tab = new System.Windows.Forms.TabControl();
            this._tab_a = new System.Windows.Forms.TabPage();
            this.schedulerControl1 = new DevExpress.XtraScheduler.SchedulerControl();
            this.schedulerStorage1 = new DevExpress.XtraScheduler.SchedulerStorage(this.components);
            this.tab_rule_static = new System.Windows.Forms.TabPage();
            this.dgv_rule_p = new System.Windows.Forms.DataGridView();
            this.tab_rule_fuzhu = new System.Windows.Forms.TabPage();
            this.dgv_fuzhu_static = new System.Windows.Forms.DataGridView();
            this._mainSplitter1 = new System.Windows.Forms.Splitter();
            this._sxTabPage = new System.Windows.Forms.TabPage();
            this.btn_check_all = new System.Windows.Forms.Button();
            this.chart_sx = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._sxdgv = new System.Windows.Forms.DataGridView();
            this._chayiTabpage = new System.Windows.Forms.TabPage();
            this.chanyi_dgv = new System.Windows.Forms.DataGridView();
            this._individualsTabPage = new System.Windows.Forms.TabPage();
            this._riverTabPage = new System.Windows.Forms.TabPage();
            this._rightPanel = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this._itemsTabControl = new System.Windows.Forms.TabControl();
            this._itemsListTabPage = new System.Windows.Forms.TabPage();
            this._itemsList = new System.Windows.Forms.ListView();
            this._calculatedAttrTabPage = new System.Windows.Forms.TabPage();
            this._calcAttrList = new System.Windows.Forms.ListView();
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.g_o = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.avg_t_f = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.avg_t_t = new System.Windows.Forms.TextBox();
            this.avg_c = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.var_t_f = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.var_t_t = new System.Windows.Forms.TextBox();
            this.var_c = new System.Windows.Forms.CheckBox();
            this.chart_chayi = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btn_chayi = new System.Windows.Forms.Button();
            this.dgvUserWarn = new System.Windows.Forms.DataGridView();
            this._userwarnpage = new System.Windows.Forms.TabPage();
            this._bottomPanel.SuspendLayout();
            this._bottom_right_panel.SuspendLayout();
            this.g_s.SuspendLayout();
            this._detailTabControl.SuspendLayout();
            this._detailpage.SuspendLayout();
            this._userTabControl.SuspendLayout();
            this._userpage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectAll)).BeginInit();
            this.dgvSelectAll.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this._topPanel.SuspendLayout();
            this._mainTabControl.SuspendLayout();
            this._tab_fuzhu.SuspendLayout();
            this._tab.SuspendLayout();
            this._tab_a.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerStorage1)).BeginInit();
            this.tab_rule_static.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rule_p)).BeginInit();
            this.tab_rule_fuzhu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_fuzhu_static)).BeginInit();
            this._sxTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_sx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._sxdgv)).BeginInit();
            this._chayiTabpage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chanyi_dgv)).BeginInit();
            this._itemsTabControl.SuspendLayout();
            this._itemsListTabPage.SuspendLayout();
            this._calculatedAttrTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_chayi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserWarn)).BeginInit();
            this._userwarnpage.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkBxSelect
            // 
            this.chkBxSelect.DataPropertyName = "is_check";
            this.chkBxSelect.HeaderText = "显示";
            this.chkBxSelect.Name = "chkBxSelect";
            this.chkBxSelect.Width = 80;
            // 
            // chkCoSelect
            // 
            this.chkCoSelect.DataPropertyName = "is_computer";
            this.chkCoSelect.HeaderText = "计算";
            this.chkCoSelect.Name = "chkCoSelect";
            this.chkCoSelect.Width = 80;
            // 
            // chkUserWarn
            // 
            this.chkUserWarn.DataPropertyName = "is_warn";
            this.chkUserWarn.HeaderText = "是否异常";
            this.chkUserWarn.Name = "chkUserWarn";
            this.chkUserWarn.Width = 80;
            // 
            // userid
            // 
            this.userid.DataPropertyName = "user_id";
            this.userid.HeaderText = "用户";
            this.userid.Name = "userid";
            this.userid.ReadOnly = true;
            this.userid.Width = 200;
            // 
            // c_rule
            // 
            this.c_rule.DataPropertyName = "rule";
            this.c_rule.HeaderText = "规则";
            this.c_rule.Name = "c_rule";
            this.c_rule.ReadOnly = true;
            this.c_rule.Width = 80;
            // 
            // useridWarn
            // 
            this.useridWarn.DataPropertyName = "user_id";
            this.useridWarn.HeaderText = "用户编号";
            this.useridWarn.Name = "useridWarn";
            this.useridWarn.Width = 80;
            // 
            // var_p
            // 
            this.var_p.DataPropertyName = "标准差";
            this.var_p.HeaderText = "变异系数";
            this.var_p.Name = "var_p";
            this.var_p.ReadOnly = true;
            this.var_p.Width = 90;
            // 
            // xyCoSelect
            // 
            this.xyCoSelect.DataPropertyName = "is_show";
            this.xyCoSelect.HeaderText = "嫌疑";
            this.xyCoSelect.Name = "xyCoSelect";
            this.xyCoSelect.Width = 80;
            // 
            // yaozhi
            // 
            this.yaozhi.DataPropertyName = "yaozhi";
            this.yaozhi.HeaderText = "零值比例";
            this.yaozhi.Name = "yaozhi";
            this.yaozhi.ReadOnly = true;
            this.yaozhi.Width = 80;
            // 
            // pUserWarn
            // 
            this.pUserWarn.DataPropertyName = "warn_p";
            this.pUserWarn.HeaderText = "零值比率";
            this.pUserWarn.Name = "pUserWarn";
            this.pUserWarn.Width = 80;
            // 
            // avg
            // 
            this.avg.DataPropertyName = "均值";
            this.avg.HeaderText = "均值";
            this.avg.Name = "avg";
            this.avg.ReadOnly = true;
            this.avg.Width = 80;
            // 
            // m1
            // 
            this.m1.DataPropertyName = "M1";
            this.m1.HeaderText = "算法A";
            this.m1.Name = "m1";
            this.m1.ReadOnly = true;
            this.m1.Width = 80;
            // 
            // sx_bili
            // 
            this.sx_bili.DataPropertyName = "sx_bili";
            this.sx_bili.HeaderText = "算法B";
            this.sx_bili.Name = "sx_bili";
            this.sx_bili.ReadOnly = true;
            this.sx_bili.Visible = false;
            this.sx_bili.Width = 80;
            // 
            // _mainMenu
            // 
            this._mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this._fileMenu,
            this.menuItem14,
            this.menuItem9,
            this.menuItem6,
            this._viewMenu,
            this._toolsMenu,
            this.menuItem1,
            this._helpMenu});
            // 
            // _fileMenu
            // 
            this._fileMenu.Index = 0;
            this._fileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem12,
            this._loadAtrMenuItem,
            this.menuItem4,
            this.menuItem16,
            this.menuItem5});
            this._fileMenu.Text = "&文件";
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 0;
            this.menuItem12.Text = "任务管理";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // _loadAtrMenuItem
            // 
            this._loadAtrMenuItem.Index = 1;
            this._loadAtrMenuItem.Text = "清除数据";
            this._loadAtrMenuItem.Click += new System.EventHandler(this._loadAtrMenuItem_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "返回导航";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 3;
            this.menuItem16.Text = "返回网络接口";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 4;
            this.menuItem5.Text = "退出";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click_1);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 1;
            this.menuItem14.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this._openMenuItem,
            this._exitMenuItem,
            this.menuItem2,
            this.menuItem10,
            this.menuItem11});
            this.menuItem14.Text = "导入数据";
            // 
            // _openMenuItem
            // 
            this._openMenuItem.Index = 0;
            this._openMenuItem.Text = "&导入馈线线损数据";
            this._openMenuItem.Click += new System.EventHandler(this._openMenuItem_Click);
            // 
            // _exitMenuItem
            // 
            this._exitMenuItem.Index = 1;
            this._exitMenuItem.Text = "&自动导入";
            this._exitMenuItem.Visible = false;
            this._exitMenuItem.Click += new System.EventHandler(this._exitMenuItem_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 2;
            this.menuItem2.Text = "导入瞬时主表数据";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 3;
            this.menuItem10.Text = "导入表码主表数据";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 4;
            this.menuItem11.Text = "导入表码负表数据";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 2;
            this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem17,
            this.menuItem18});
            this.menuItem9.Text = "数据浏览";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 0;
            this.menuItem17.Text = "数据查看";
            this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 1;
            this.menuItem18.Text = "任务查看";
            this.menuItem18.Visible = false;
            this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 3;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7,
            this.menuItem8});
            this.menuItem6.Text = "系统配置";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 0;
            this.menuItem7.Text = "网络接口配置";
            this.menuItem7.Click += new System.EventHandler(this._exitMenuItem_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            this.menuItem8.Text = "规则参数配置";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // _viewMenu
            // 
            this._viewMenu.Index = 4;
            this._viewMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this._gridMenuItem});
            this._viewMenu.Text = "嫌疑库";
            // 
            // _gridMenuItem
            // 
            this._gridMenuItem.Index = 0;
            this._gridMenuItem.Text = "嫌疑用户库";
            this._gridMenuItem.Click += new System.EventHandler(this._gridMenuItem_Click);
            // 
            // _toolsMenu
            // 
            this._toolsMenu.Index = 5;
            this._toolsMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this._forecastingMenuItem});
            this._toolsMenu.Text = "&窃电库";
            this._toolsMenu.Visible = false;
            // 
            // _forecastingMenuItem
            // 
            this._forecastingMenuItem.Index = 0;
            this._forecastingMenuItem.Text = "窃电用户库";
            this._forecastingMenuItem.Click += new System.EventHandler(this._forecastingMenuItem_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 6;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this._selectMatchesMenuItem});
            this.menuItem1.Text = "&漏计库";
            this.menuItem1.Visible = false;
            // 
            // _selectMatchesMenuItem
            // 
            this._selectMatchesMenuItem.Enabled = false;
            this._selectMatchesMenuItem.Index = 0;
            this._selectMatchesMenuItem.Text = "漏计用户库";
            this._selectMatchesMenuItem.Click += new System.EventHandler(this._selectMatchesMenuItem_Click);
            // 
            // _helpMenu
            // 
            this._helpMenu.Index = 7;
            this._helpMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this._clearAllQueriesMenuItem,
            this.menuItem13,
            this._aboutMenuItem});
            this._helpMenu.Text = "&帮助";
            // 
            // _clearAllQueriesMenuItem
            // 
            this._clearAllQueriesMenuItem.Index = 0;
            this._clearAllQueriesMenuItem.Text = "操作记录";
            this._clearAllQueriesMenuItem.Click += new System.EventHandler(this._clearAllQueriesMenuItem_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 1;
            this.menuItem13.Text = "帮助文档";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // _aboutMenuItem
            // 
            this._aboutMenuItem.Index = 2;
            this._aboutMenuItem.Text = "关于";
            this._aboutMenuItem.Click += new System.EventHandler(this._aboutMenuItem_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = -1;
            this.menuItem15.Text = "d";
            // 
            // _undoFilterUnselectedMenuItem
            // 
            this._undoFilterUnselectedMenuItem.Index = -1;
            this._undoFilterUnselectedMenuItem.Text = "任务查询";
            this._undoFilterUnselectedMenuItem.Click += new System.EventHandler(this._undoFilterUnselectedMenuItem_Click);
            // 
            // _separator1MenuItem
            // 
            this._separator1MenuItem.Index = -1;
            this._separator1MenuItem.Text = "-";
            // 
            // _searchOptsMenuItem
            // 
            this._searchOptsMenuItem.Enabled = false;
            this._searchOptsMenuItem.Index = -1;
            this._searchOptsMenuItem.Text = "Search Options";
            this._searchOptsMenuItem.Click += new System.EventHandler(this._searchOptsMenuItem_Click);
            // 
            // _separator2MenuItem
            // 
            this._separator2MenuItem.Index = -1;
            this._separator2MenuItem.Text = "-";
            // 
            // _setSizeMenuItem
            // 
            this._setSizeMenuItem.Index = -1;
            this._setSizeMenuItem.Text = "Set Size";
            this._setSizeMenuItem.Click += new System.EventHandler(this._setSizeMenuItem_Click);
            // 
            // _prefsMenuItem
            // 
            this._prefsMenuItem.Index = -1;
            this._prefsMenuItem.Text = "Preferences";
            this._prefsMenuItem.Click += new System.EventHandler(this._prefsMenuItem_Click);
            // 
            // _showTimeLineMenuItem
            // 
            this._showTimeLineMenuItem.Checked = true;
            this._showTimeLineMenuItem.Index = -1;
            this._showTimeLineMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
            this._showTimeLineMenuItem.Text = "47038904__F12伟达线_日_13-02-18到13-03-10";
            this._showTimeLineMenuItem.Click += new System.EventHandler(this._showTimeLineMenuItem_Click);
            // 
            // _showForecastSourceMenuItem
            // 
            this._showForecastSourceMenuItem.Index = -1;
            this._showForecastSourceMenuItem.Text = "47211170__F15茶山一线_日_12-12-20到13-01-05";
            this._showForecastSourceMenuItem.Click += new System.EventHandler(this._showForecastSourceMenuItem_Click);
            // 
            // _syncDetailsListMenuItem
            // 
            this._syncDetailsListMenuItem.Checked = true;
            this._syncDetailsListMenuItem.Index = -1;
            this._syncDetailsListMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlD;
            this._syncDetailsListMenuItem.Text = "47248778__F20塘林线_日_13-02-15到13-03-06";
            this._syncDetailsListMenuItem.Click += new System.EventHandler(this._syncDetailsListMenuItem_Click);
            // 
            // _syncOverviewMenuItem
            // 
            this._syncOverviewMenuItem.Checked = true;
            this._syncOverviewMenuItem.Index = -1;
            this._syncOverviewMenuItem.Text = "47438283__F10钟岗二线_日_13-02-23到13-03-13";
            this._syncOverviewMenuItem.Click += new System.EventHandler(this._syncOverviewMenuItem_Click);
            // 
            // _showSelectedItemsMenuItem
            // 
            this._showSelectedItemsMenuItem.Index = -1;
            this._showSelectedItemsMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this._showSelectedItemsMenuItem.Text = "47833764__F41楼工二线_日_13-01-01到13-01-21";
            this._showSelectedItemsMenuItem.Click += new System.EventHandler(this._showSelectedItemsMenuItem_Click);
            // 
            // _hideFilteredMenuItem
            // 
            this._hideFilteredMenuItem.Index = -1;
            this._hideFilteredMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this._hideFilteredMenuItem.Text = "65537335__F11达琦线_日_13-04-01到13-04-26";
            this._hideFilteredMenuItem.Click += new System.EventHandler(this._hideFilteredMenuItem_Click);
            // 
            // _showSelectionBoxesMenuItem
            // 
            this._showSelectionBoxesMenuItem.Checked = true;
            this._showSelectionBoxesMenuItem.Index = -1;
            this._showSelectionBoxesMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlB;
            this._showSelectionBoxesMenuItem.Text = "65824198__F09浪贝一线_日_13-01-01到13-01-28";
            this._showSelectionBoxesMenuItem.Click += new System.EventHandler(this._showSelectionBoxesMenuItem_Click);
            // 
            // _logMenuItem
            // 
            this._logMenuItem.Index = -1;
            this._logMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
            this._logMenuItem.Text = "79501314__F05报岗线_日_13-02-01到13-02-24";
            this._logMenuItem.Click += new System.EventHandler(this._logMenuItem_Click);
            // 
            // _expMenuItem
            // 
            this._expMenuItem.Index = -1;
            this._expMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
            this._expMenuItem.Text = "79897737__F06潭头线_日_12-10-01到12-10-22";
            this._expMenuItem.Click += new System.EventHandler(this._expMenuItem_Click);
            // 
            // _filterUnselectedMenuItem
            // 
            this._filterUnselectedMenuItem.Index = -1;
            this._filterUnselectedMenuItem.Text = "Filter unselected";
            this._filterUnselectedMenuItem.Click += new System.EventHandler(this._filterUnselectedMenuItem_Click);
            // 
            // _filterAttributesMenuItem
            // 
            this._filterAttributesMenuItem.Index = -1;
            this._filterAttributesMenuItem.Text = "Filter attributes";
            this._filterAttributesMenuItem.Click += new System.EventHandler(this._filterAttributesMenuItem_Click);
            // 
            // _filterTfnMenuItem
            // 
            this._filterTfnMenuItem.Enabled = false;
            this._filterTfnMenuItem.Index = -1;
            this._filterTfnMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
            this._filterTfnMenuItem.Text = "&Filter items (.tfn)";
            this._filterTfnMenuItem.Visible = false;
            this._filterTfnMenuItem.Click += new System.EventHandler(this._filterTfnMenuItem_Click);
            // 
            // _analogousTsFinderMenuItem
            // 
            this._analogousTsFinderMenuItem.Index = -1;
            this._analogousTsFinderMenuItem.Text = "Analogous time series finder";
            this._analogousTsFinderMenuItem.Click += new System.EventHandler(this._analogousTsFinderMenuItem_Click);
            // 
            // _wizardMenuItem
            // 
            this._wizardMenuItem.Index = -1;
            this._wizardMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
            this._wizardMenuItem.Text = "Wizard";
            this._wizardMenuItem.Click += new System.EventHandler(this._wizardMenuItem_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = -1;
            this.menuItem3.Text = "-";
            // 
            // _civToTqdConverterMenuItem
            // 
            this._civToTqdConverterMenuItem.Index = -1;
            this._civToTqdConverterMenuItem.Text = ".civ to .tqd Converter";
            this._civToTqdConverterMenuItem.Click += new System.EventHandler(this._civToTqdConverterMenuItem_Click);
            // 
            // _ci1CombinerMenuItem
            // 
            this._ci1CombinerMenuItem.Index = -1;
            this._ci1CombinerMenuItem.Text = ".ci1 Combiner";
            this._ci1CombinerMenuItem.Click += new System.EventHandler(this._ci1CombinerMenuItem_Click);
            // 
            // _quickRefMenuItem
            // 
            this._quickRefMenuItem.Index = -1;
            this._quickRefMenuItem.Text = "关于";
            // 
            // _testMenuItem
            // 
            this._testMenuItem.Index = -1;
            this._testMenuItem.Text = "Test!";
            this._testMenuItem.Visible = false;
            this._testMenuItem.Click += new System.EventHandler(this._testMenuItem_Click);
            // 
            // _imgListButtons
            // 
            this._imgListButtons.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this._imgListButtons.ImageSize = new System.Drawing.Size(16, 16);
            this._imgListButtons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tbBtn
            // 
            this.tbBtn.AutoSize = false;
            this.tbBtn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBtn.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarSeparator,
            this.tbrBtnSelect,
            this.tbrBtnTimebox,
            this.tbrBtnSearchbox,
            this.tbrBtnVTTimebox,
            this.tbrBtnAngularQuery,
            this.tbrBtnLeadLag,
            this.tbrBtnFlip,
            this.tbrBtnSearch,
            this.tbrBtnClear});
            this.tbBtn.DropDownArrows = true;
            this.tbBtn.ImageList = this._imgListButtons;
            this.tbBtn.Location = new System.Drawing.Point(0, 0);
            this.tbBtn.Name = "tbBtn";
            this.tbBtn.ShowToolTips = true;
            this.tbBtn.Size = new System.Drawing.Size(784, 42);
            this.tbBtn.TabIndex = 0;
            this.tbBtn.Visible = false;
            // 
            // toolBarSeparator
            // 
            this.toolBarSeparator.Name = "toolBarSeparator";
            this.toolBarSeparator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbrBtnSelect
            // 
            this.tbrBtnSelect.ImageIndex = 6;
            this.tbrBtnSelect.Name = "tbrBtnSelect";
            this.tbrBtnSelect.Text = "Select";
            this.tbrBtnSelect.ToolTipText = "Select";
            // 
            // tbrBtnTimebox
            // 
            this.tbrBtnTimebox.ImageIndex = 13;
            this.tbrBtnTimebox.Name = "tbrBtnTimebox";
            this.tbrBtnTimebox.Text = "Timebox";
            // 
            // tbrBtnSearchbox
            // 
            this.tbrBtnSearchbox.ImageIndex = 18;
            this.tbrBtnSearchbox.Name = "tbrBtnSearchbox";
            // 
            // tbrBtnVTTimebox
            // 
            this.tbrBtnVTTimebox.ImageIndex = 16;
            this.tbrBtnVTTimebox.Name = "tbrBtnVTTimebox";
            this.tbrBtnVTTimebox.Text = "VTT";
            // 
            // tbrBtnAngularQuery
            // 
            this.tbrBtnAngularQuery.ImageIndex = 1;
            this.tbrBtnAngularQuery.Name = "tbrBtnAngularQuery";
            this.tbrBtnAngularQuery.Text = "Angular Query";
            // 
            // tbrBtnLeadLag
            // 
            this.tbrBtnLeadLag.ImageIndex = 10;
            this.tbrBtnLeadLag.Name = "tbrBtnLeadLag";
            this.tbrBtnLeadLag.Text = "Leaders and Laggars";
            // 
            // tbrBtnFlip
            // 
            this.tbrBtnFlip.ImageIndex = 4;
            this.tbrBtnFlip.Name = "tbrBtnFlip";
            this.tbrBtnFlip.Text = "Flip";
            // 
            // tbrBtnSearch
            // 
            this.tbrBtnSearch.Name = "tbrBtnSearch";
            this.tbrBtnSearch.Text = "Search";
            // 
            // tbrBtnClear
            // 
            this.tbrBtnClear.ImageIndex = 3;
            this.tbrBtnClear.Name = "tbrBtnClear";
            this.tbrBtnClear.Text = "Clear";
            // 
            // _openAtrFileDlg
            // 
            this._openAtrFileDlg.Filter = "Attribute files (*.atr)|*.atr|.csv files (*.csv)|*.csv|All files (*.*)|*.*";
            this._openAtrFileDlg.FilterIndex = 2;
            this._openAtrFileDlg.Title = "Open Attribute File";
            // 
            // _openTqdFileDlg
            // 
            this._openTqdFileDlg.Filter = "xlsx files |*.*";
            this._openTqdFileDlg.Multiselect = true;
            this._openTqdFileDlg.Title = "Open .tqd File";
            // 
            // _statusBar
            // 
            this._statusBar.Location = new System.Drawing.Point(0, 524);
            this._statusBar.Name = "_statusBar";
            this._statusBar.Size = new System.Drawing.Size(784, 17);
            this._statusBar.TabIndex = 1;
            // 
            // _bottomPanel
            // 
            this._bottomPanel.AutoScroll = true;
            this._bottomPanel.BackColor = System.Drawing.Color.LightGray;
            this._bottomPanel.Controls.Add(this._bottom_right_panel);
            this._bottomPanel.Controls.Add(this._detailTabControl);
            this._bottomPanel.Controls.Add(this._userTabControl);
            this._bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._bottomPanel.Location = new System.Drawing.Point(0, 324);
            this._bottomPanel.Name = "_bottomPanel";
            this._bottomPanel.Size = new System.Drawing.Size(784, 200);
            this._bottomPanel.TabIndex = 2;
            this._bottomPanel.Resize += new System.EventHandler(this.OnResizeBottomPanel);
            // 
            // _bottom_right_panel
            // 
            this._bottom_right_panel.AutoScroll = true;
            this._bottom_right_panel.Controls.Add(this.g_s);
            this._bottom_right_panel.Dock = System.Windows.Forms.DockStyle.Left;
            this._bottom_right_panel.Location = new System.Drawing.Point(1120, 0);
            this._bottom_right_panel.Name = "_bottom_right_panel";
            this._bottom_right_panel.Size = new System.Drawing.Size(260, 183);
            this._bottom_right_panel.TabIndex = 0;
            // 
            // g_s
            // 
            this.g_s.Controls.Add(this.检查信息);
            this.g_s.Controls.Add(this.comboBox1);
            this.g_s.Controls.Add(this.label8);
            this.g_s.Controls.Add(this.button1);
            this.g_s.Controls.Add(this.label7);
            this.g_s.Controls.Add(this.out_put);
            this.g_s.Controls.Add(this.btn_save_sx);
            this.g_s.Dock = System.Windows.Forms.DockStyle.Top;
            this.g_s.Location = new System.Drawing.Point(0, 0);
            this.g_s.Name = "g_s";
            this.g_s.Size = new System.Drawing.Size(260, 180);
            this.g_s.TabIndex = 0;
            this.g_s.TabStop = false;
            // 
            // 检查信息
            // 
            this.检查信息.AutoSize = true;
            this.检查信息.Location = new System.Drawing.Point(9, 89);
            this.检查信息.Name = "检查信息";
            this.检查信息.Size = new System.Drawing.Size(53, 12);
            this.检查信息.TabIndex = 25;
            this.检查信息.Text = "检查信息";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(64, 82);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(192, 20);
            this.comboBox1.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "label8";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(34, 129);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 20);
            this.button1.TabIndex = 22;
            this.button1.Text = "暂停分析";
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "线损统计信息";
            // 
            // out_put
            // 
            this.out_put.Enabled = false;
            this.out_put.Location = new System.Drawing.Point(159, 154);
            this.out_put.Name = "out_put";
            this.out_put.Size = new System.Drawing.Size(80, 20);
            this.out_put.TabIndex = 1;
            this.out_put.Text = "数据导出";
            this.out_put.Visible = false;
            this.out_put.Click += new System.EventHandler(this.out_put_Click);
            // 
            // btn_save_sx
            // 
            this.btn_save_sx.Enabled = false;
            this.btn_save_sx.Location = new System.Drawing.Point(120, 129);
            this.btn_save_sx.Name = "btn_save_sx";
            this.btn_save_sx.Size = new System.Drawing.Size(80, 23);
            this.btn_save_sx.TabIndex = 19;
            this.btn_save_sx.Text = "嫌疑入库";
            this.btn_save_sx.Click += new System.EventHandler(this.btn_save_sx_Click);
            // 
            // _detailTabControl
            // 
            this._detailTabControl.Controls.Add(this._detailpage);
            this._detailTabControl.Dock = System.Windows.Forms.DockStyle.Left;
            this._detailTabControl.Location = new System.Drawing.Point(760, 0);
            this._detailTabControl.Name = "_detailTabControl";
            this._detailTabControl.SelectedIndex = 0;
            this._detailTabControl.Size = new System.Drawing.Size(360, 183);
            this._detailTabControl.TabIndex = 1;
            // 
            // _detailpage
            // 
            this._detailpage.Controls.Add(this._detailsList);
            this._detailpage.Location = new System.Drawing.Point(4, 22);
            this._detailpage.Name = "_detailpage";
            this._detailpage.Size = new System.Drawing.Size(352, 157);
            this._detailpage.TabIndex = 0;
            this._detailpage.Text = "用户详细数据";
            // 
            // _detailsList
            // 
            this._detailsList.AllowColumnReorder = true;
            this._detailsList.BackColor = System.Drawing.Color.White;
            this._detailsList.Dock = System.Windows.Forms.DockStyle.Left;
            this._detailsList.ForeColor = System.Drawing.Color.Black;
            this._detailsList.FullRowSelect = true;
            this._detailsList.GridLines = true;
            this._detailsList.Location = new System.Drawing.Point(0, 0);
            this._detailsList.MultiSelect = false;
            this._detailsList.Name = "_detailsList";
            this._detailsList.Size = new System.Drawing.Size(360, 157);
            this._detailsList.TabIndex = 2;
            this._detailsList.UseCompatibleStateImageBehavior = false;
            this._detailsList.View = System.Windows.Forms.View.Details;
            this._detailsList.SelectedIndexChanged += new System.EventHandler(this.detailsListSelectedIndexChanged);
            // 
            // _userTabControl
            // 
            this._userTabControl.Controls.Add(this._userpage);
            this._userTabControl.Dock = System.Windows.Forms.DockStyle.Left;
            this._userTabControl.Location = new System.Drawing.Point(0, 0);
            this._userTabControl.Name = "_userTabControl";
            this._userTabControl.SelectedIndex = 0;
            this._userTabControl.Size = new System.Drawing.Size(760, 183);
            this._userTabControl.TabIndex = 2;
            // 
            // _userpage
            // 
            this._userpage.Controls.Add(this.dgvSelectAll);
            this._userpage.Location = new System.Drawing.Point(4, 22);
            this._userpage.Name = "_userpage";
            this._userpage.Size = new System.Drawing.Size(752, 157);
            this._userpage.TabIndex = 0;
            this._userpage.Text = "用户计算";
            // 
            // dgvSelectAll
            // 
            this.dgvSelectAll.AllowUserToAddRows = false;
            this.dgvSelectAll.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSelectAll.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSelectAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelectAll.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkBxSelect,
            this.chkCoSelect,
            this.xyCoSelect,
            this.userid,
            this.yaozhi,
            this.m1,
            this.sx_bili,
            this.c_rule,
            this.avg,
            this.var_p});
            this.dgvSelectAll.Controls.Add(this.is_check);
            this.dgvSelectAll.Controls.Add(this.is_computer);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSelectAll.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSelectAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvSelectAll.Location = new System.Drawing.Point(0, 0);
            this.dgvSelectAll.Name = "dgvSelectAll";
            this.dgvSelectAll.RowTemplate.Height = 23;
            this.dgvSelectAll.Size = new System.Drawing.Size(760, 157);
            this.dgvSelectAll.TabIndex = 0;
            this.dgvSelectAll.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSelectAll_CellClick);
            this.dgvSelectAll.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSelectAll_CellContentClick);
            this.dgvSelectAll.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvSelectAll_CellPainting);
            this.dgvSelectAll.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvSelectAll_CurrentCellDirtyStateChanged);
            // 
            // is_check
            // 
            this.is_check.Checked = true;
            this.is_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.is_check.Location = new System.Drawing.Point(0, 0);
            this.is_check.Name = "is_check";
            this.is_check.Size = new System.Drawing.Size(15, 15);
            this.is_check.TabIndex = 2;
            this.is_check.Tag = "is_check";
            this.is_check.Click += new System.EventHandler(this.is_check_Click);
            this.is_check.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HeaderCheckBox_KeyUp);
            this.is_check.MouseClick += new System.Windows.Forms.MouseEventHandler(this.HeaderCheckBox_MouseClick);
            // 
            // is_computer
            // 
            this.is_computer.Checked = true;
            this.is_computer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.is_computer.Location = new System.Drawing.Point(0, 0);
            this.is_computer.Name = "is_computer";
            this.is_computer.Size = new System.Drawing.Size(15, 15);
            this.is_computer.TabIndex = 3;
            this.is_computer.Tag = "is_computer";
            this.is_computer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HeaderCheckBox_KeyUp);
            this.is_computer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.HeaderCheckBox_MouseClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.Controls.Add(this.result_l, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 200);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // result_l
            // 
            this.result_l.AutoSize = true;
            this.result_l.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.result_l.Location = new System.Drawing.Point(96, 21);
            this.result_l.Margin = new System.Windows.Forms.Padding(15, 20, 15, 15);
            this.result_l.Name = "result_l";
            this.result_l.Size = new System.Drawing.Size(0, 20);
            this.result_l.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(15, 20, 15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "算法";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(16, 77);
            this.label2.Margin = new System.Windows.Forms.Padding(15, 20, 15, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "M1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(84, 60);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1055, 200);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // _hSplitter
            // 
            this._hSplitter.BackColor = System.Drawing.Color.DarkGray;
            this._hSplitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._hSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._hSplitter.Location = new System.Drawing.Point(0, 320);
            this._hSplitter.Name = "_hSplitter";
            this._hSplitter.Size = new System.Drawing.Size(784, 4);
            this._hSplitter.TabIndex = 3;
            this._hSplitter.TabStop = false;
            // 
            // _topPanel
            // 
            this._topPanel.BackColor = System.Drawing.Color.LightGray;
            this._topPanel.Controls.Add(this._mainTabControl);
            this._topPanel.Controls.Add(this._mainSplitter1);
            this._topPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._topPanel.Location = new System.Drawing.Point(0, 42);
            this._topPanel.Name = "_topPanel";
            this._topPanel.Size = new System.Drawing.Size(784, 278);
            this._topPanel.TabIndex = 4;
            // 
            // _mainTabControl
            // 
            this._mainTabControl.Controls.Add(this._allTabPage);
            this._mainTabControl.Controls.Add(this._tab_fuzhu);
            this._mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainTabControl.Location = new System.Drawing.Point(0, 0);
            this._mainTabControl.Name = "_mainTabControl";
            this._mainTabControl.SelectedIndex = 0;
            this._mainTabControl.Size = new System.Drawing.Size(780, 278);
            this._mainTabControl.TabIndex = 2;
            this._mainTabControl.SelectedIndexChanged += new System.EventHandler(this._mainTabControl_SelectedIndexChanged);
            // 
            // _allTabPage
            // 
            this._allTabPage.Location = new System.Drawing.Point(4, 22);
            this._allTabPage.Name = "_allTabPage";
            this._allTabPage.Size = new System.Drawing.Size(772, 252);
            this._allTabPage.TabIndex = 0;
            this._allTabPage.Text = "图形展示";
            // 
            // _tab_fuzhu
            // 
            this._tab_fuzhu.Controls.Add(this._tab);
            this._tab_fuzhu.Location = new System.Drawing.Point(4, 22);
            this._tab_fuzhu.Name = "_tab_fuzhu";
            this._tab_fuzhu.Size = new System.Drawing.Size(772, 252);
            this._tab_fuzhu.TabIndex = 1;
            this._tab_fuzhu.Text = "规则分析";
            this._tab_fuzhu.UseVisualStyleBackColor = true;
            // 
            // _tab
            // 
            this._tab.Controls.Add(this._tab_a);
            this._tab.Controls.Add(this.tab_rule_static);
            this._tab.Controls.Add(this.tab_rule_fuzhu);
            this._tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tab.Location = new System.Drawing.Point(0, 0);
            this._tab.Name = "_tab";
            this._tab.SelectedIndex = 0;
            this._tab.Size = new System.Drawing.Size(772, 252);
            this._tab.TabIndex = 0;
            // 
            // _tab_a
            // 
            this._tab_a.AutoScroll = true;
            this._tab_a.Controls.Add(this.schedulerControl1);
            this._tab_a.Location = new System.Drawing.Point(4, 22);
            this._tab_a.Name = "_tab_a";
            this._tab_a.Padding = new System.Windows.Forms.Padding(3);
            this._tab_a.Size = new System.Drawing.Size(764, 226);
            this._tab_a.TabIndex = 0;
            this._tab_a.Text = "规则图形";
            this._tab_a.UseVisualStyleBackColor = true;
            // 
            // schedulerControl1
            // 
            this.schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Timeline;
            this.schedulerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schedulerControl1.Location = new System.Drawing.Point(3, 3);
            this.schedulerControl1.Name = "schedulerControl1";
            this.schedulerControl1.OptionsBehavior.RecurrentAppointmentEditAction = DevExpress.XtraScheduler.RecurrentAppointmentAction.Ask;
            this.schedulerControl1.OptionsCustomization.AllowAppointmentConflicts = DevExpress.XtraScheduler.AppointmentConflictsMode.Forbidden;
            this.schedulerControl1.OptionsCustomization.AllowAppointmentCopy = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.schedulerControl1.OptionsCustomization.AllowAppointmentCreate = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.schedulerControl1.OptionsCustomization.AllowAppointmentDelete = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.schedulerControl1.OptionsCustomization.AllowAppointmentDrag = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.schedulerControl1.OptionsCustomization.AllowAppointmentEdit = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.schedulerControl1.OptionsCustomization.AllowAppointmentMultiSelect = false;
            this.schedulerControl1.OptionsCustomization.AllowAppointmentResize = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.schedulerControl1.OptionsCustomization.AllowDisplayAppointmentDependencyForm = DevExpress.XtraScheduler.AllowDisplayAppointmentDependencyForm.Never;
            this.schedulerControl1.OptionsCustomization.AllowDisplayAppointmentForm = DevExpress.XtraScheduler.AllowDisplayAppointmentForm.Never;
            this.schedulerControl1.OptionsRangeControl.RangeMaximum = new System.DateTime(2014, 5, 1, 0, 0, 0, 0);
            this.schedulerControl1.OptionsRangeControl.RangeMinimum = new System.DateTime(2014, 2, 1, 0, 0, 0, 0);
            this.schedulerControl1.Size = new System.Drawing.Size(758, 220);
            this.schedulerControl1.Start = new System.DateTime(2014, 3, 16, 0, 0, 0, 0);
            this.schedulerControl1.Storage = this.schedulerStorage1;
            this.schedulerControl1.TabIndex = 1;
            this.schedulerControl1.Text = "schedulerControl1";
            this.schedulerControl1.Views.DayView.TimeRulers.Add(timeRuler1);
            this.schedulerControl1.Views.WorkWeekView.TimeRulers.Add(timeRuler2);
            this.schedulerControl1.Click += new System.EventHandler(this.schedulerControl1_Click);
            // 
            // schedulerStorage1
            // 
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new DevExpress.XtraScheduler.AppointmentCustomFieldMapping("ContactInfo", "ContactInfo", DevExpress.XtraScheduler.FieldValueType.String));
            this.schedulerStorage1.Appointments.Mappings.AllDay = "AllDay";
            this.schedulerStorage1.Appointments.Mappings.Description = "Description";
            this.schedulerStorage1.Appointments.Mappings.End = "EndDate";
            this.schedulerStorage1.Appointments.Mappings.Label = "Label";
            this.schedulerStorage1.Appointments.Mappings.Location = "Location";
            this.schedulerStorage1.Appointments.Mappings.ResourceId = "ResourceID";
            this.schedulerStorage1.Appointments.Mappings.Start = "StartDate";
            this.schedulerStorage1.Appointments.Mappings.Status = "Status";
            this.schedulerStorage1.Appointments.Mappings.Subject = "Subject";
            this.schedulerStorage1.Appointments.Mappings.Type = "EventType";
            // 
            // tab_rule_static
            // 
            this.tab_rule_static.Controls.Add(this.dgv_rule_p);
            this.tab_rule_static.Location = new System.Drawing.Point(4, 22);
            this.tab_rule_static.Name = "tab_rule_static";
            this.tab_rule_static.Size = new System.Drawing.Size(764, 226);
            this.tab_rule_static.TabIndex = 1;
            this.tab_rule_static.Text = "规则违背率统计";
            this.tab_rule_static.UseVisualStyleBackColor = true;
            // 
            // dgv_rule_p
            // 
            this.dgv_rule_p.AllowUserToAddRows = false;
            this.dgv_rule_p.AllowUserToDeleteRows = false;
            this.dgv_rule_p.AllowUserToOrderColumns = true;
            this.dgv_rule_p.AllowUserToResizeColumns = false;
            this.dgv_rule_p.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgv_rule_p.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_rule_p.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_rule_p.Location = new System.Drawing.Point(0, 0);
            this.dgv_rule_p.MultiSelect = false;
            this.dgv_rule_p.Name = "dgv_rule_p";
            this.dgv_rule_p.RowTemplate.Height = 23;
            this.dgv_rule_p.Size = new System.Drawing.Size(764, 226);
            this.dgv_rule_p.TabIndex = 0;
            this.dgv_rule_p.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_rule_p_CellClick);
            this.dgv_rule_p.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_rule_p_CellDoubleClick);
            // 
            // tab_rule_fuzhu
            // 
            this.tab_rule_fuzhu.Controls.Add(this.dgv_fuzhu_static);
            this.tab_rule_fuzhu.Location = new System.Drawing.Point(4, 22);
            this.tab_rule_fuzhu.Name = "tab_rule_fuzhu";
            this.tab_rule_fuzhu.Size = new System.Drawing.Size(764, 226);
            this.tab_rule_fuzhu.TabIndex = 2;
            this.tab_rule_fuzhu.Text = "辅助变量缺失与零值比例";
            this.tab_rule_fuzhu.UseVisualStyleBackColor = true;
            // 
            // dgv_fuzhu_static
            // 
            this.dgv_fuzhu_static.AllowUserToAddRows = false;
            this.dgv_fuzhu_static.AllowUserToDeleteRows = false;
            this.dgv_fuzhu_static.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_fuzhu_static.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_fuzhu_static.Location = new System.Drawing.Point(0, 0);
            this.dgv_fuzhu_static.MultiSelect = false;
            this.dgv_fuzhu_static.Name = "dgv_fuzhu_static";
            this.dgv_fuzhu_static.RowTemplate.Height = 23;
            this.dgv_fuzhu_static.Size = new System.Drawing.Size(764, 226);
            this.dgv_fuzhu_static.TabIndex = 0;
            this.dgv_fuzhu_static.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_fuzhu_static_CellClick);
            this.dgv_fuzhu_static.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_fuzhu_static_CellDoubleClick);
            // 
            // _mainSplitter1
            // 
            this._mainSplitter1.BackColor = System.Drawing.Color.DarkGray;
            this._mainSplitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._mainSplitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this._mainSplitter1.Location = new System.Drawing.Point(780, 0);
            this._mainSplitter1.Name = "_mainSplitter1";
            this._mainSplitter1.Size = new System.Drawing.Size(4, 278);
            this._mainSplitter1.TabIndex = 1;
            this._mainSplitter1.TabStop = false;
            // 
            // _sxTabPage
            // 
            this._sxTabPage.Controls.Add(this.btn_check_all);
            this._sxTabPage.Controls.Add(this.chart_sx);
            this._sxTabPage.Controls.Add(this._sxdgv);
            this._sxTabPage.Location = new System.Drawing.Point(0, 0);
            this._sxTabPage.Name = "_sxTabPage";
            this._sxTabPage.Size = new System.Drawing.Size(542, 252);
            this._sxTabPage.TabIndex = 1;
            this._sxTabPage.Text = "变异分析";
            // 
            // btn_check_all
            // 
            this.btn_check_all.Location = new System.Drawing.Point(8, 230);
            this.btn_check_all.Name = "btn_check_all";
            this.btn_check_all.Size = new System.Drawing.Size(75, 23);
            this.btn_check_all.TabIndex = 2;
            this.btn_check_all.Text = "导出结果";
            this.btn_check_all.UseVisualStyleBackColor = true;
            this.btn_check_all.Click += new System.EventHandler(this.btn_check_all_Click);
            // 
            // chart_sx
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_sx.ChartAreas.Add(chartArea1);
            this.chart_sx.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend1.Name = "Legend1";
            this.chart_sx.Legends.Add(legend1);
            this.chart_sx.Location = new System.Drawing.Point(0, 132);
            this.chart_sx.Name = "chart_sx";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_sx.Series.Add(series1);
            this.chart_sx.Size = new System.Drawing.Size(542, 120);
            this.chart_sx.TabIndex = 1;
            this.chart_sx.Text = "chart1";
            // 
            // _sxdgv
            // 
            this._sxdgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._sxdgv.Dock = System.Windows.Forms.DockStyle.Top;
            this._sxdgv.Location = new System.Drawing.Point(0, 0);
            this._sxdgv.Name = "_sxdgv";
            this._sxdgv.RowTemplate.Height = 23;
            this._sxdgv.Size = new System.Drawing.Size(542, 200);
            this._sxdgv.TabIndex = 0;
            this._sxdgv.SelectionChanged += new System.EventHandler(this._sxdgv_SelectionChanged);
            // 
            // _chayiTabpage
            // 
            this._chayiTabpage.Controls.Add(this.chanyi_dgv);
            this._chayiTabpage.Location = new System.Drawing.Point(0, 0);
            this._chayiTabpage.Name = "_chayiTabpage";
            this._chayiTabpage.Size = new System.Drawing.Size(542, 252);
            this._chayiTabpage.TabIndex = 2;
            this._chayiTabpage.Text = "差分分析";
            // 
            // chanyi_dgv
            // 
            this.chanyi_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.chanyi_dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chanyi_dgv.Location = new System.Drawing.Point(0, 0);
            this.chanyi_dgv.Name = "chanyi_dgv";
            this.chanyi_dgv.RowTemplate.Height = 23;
            this.chanyi_dgv.Size = new System.Drawing.Size(542, 252);
            this.chanyi_dgv.TabIndex = 0;
            // 
            // _individualsTabPage
            // 
            this._individualsTabPage.Location = new System.Drawing.Point(4, 22);
            this._individualsTabPage.Name = "_individualsTabPage";
            this._individualsTabPage.Size = new System.Drawing.Size(542, 252);
            this._individualsTabPage.TabIndex = 1;
            this._individualsTabPage.Text = "Individual items";
            this._individualsTabPage.Resize += new System.EventHandler(this.individualsPage_Resize);
            // 
            // _riverTabPage
            // 
            this._riverTabPage.Location = new System.Drawing.Point(4, 22);
            this._riverTabPage.Name = "_riverTabPage";
            this._riverTabPage.Size = new System.Drawing.Size(542, 252);
            this._riverTabPage.TabIndex = 2;
            this._riverTabPage.Text = "River Plot";
            this._riverTabPage.Resize += new System.EventHandler(this._riverTabPage_Resize);
            // 
            // _rightPanel
            // 
            this._rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this._rightPanel.Location = new System.Drawing.Point(554, 0);
            this._rightPanel.Name = "_rightPanel";
            this._rightPanel.Size = new System.Drawing.Size(230, 278);
            this._rightPanel.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.DarkGray;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 26);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(230, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // _itemsTabControl
            // 
            this._itemsTabControl.Controls.Add(this._itemsListTabPage);
            this._itemsTabControl.Controls.Add(this._calculatedAttrTabPage);
            this._itemsTabControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._itemsTabControl.Location = new System.Drawing.Point(0, 29);
            this._itemsTabControl.Name = "_itemsTabControl";
            this._itemsTabControl.SelectedIndex = 0;
            this._itemsTabControl.Size = new System.Drawing.Size(230, 249);
            this._itemsTabControl.TabIndex = 0;
            // 
            // _itemsListTabPage
            // 
            this._itemsListTabPage.Controls.Add(this._itemsList);
            this._itemsListTabPage.Location = new System.Drawing.Point(4, 22);
            this._itemsListTabPage.Name = "_itemsListTabPage";
            this._itemsListTabPage.Size = new System.Drawing.Size(222, 223);
            this._itemsListTabPage.TabIndex = 0;
            this._itemsListTabPage.Text = "Items";
            // 
            // _itemsList
            // 
            this._itemsList.AllowColumnReorder = true;
            this._itemsList.BackColor = System.Drawing.Color.White;
            this._itemsList.CheckBoxes = true;
            this._itemsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemsList.ForeColor = System.Drawing.Color.Black;
            this._itemsList.FullRowSelect = true;
            this._itemsList.Location = new System.Drawing.Point(0, 0);
            this._itemsList.Name = "_itemsList";
            this._itemsList.Size = new System.Drawing.Size(222, 223);
            this._itemsList.TabIndex = 0;
            this._itemsList.UseCompatibleStateImageBehavior = false;
            this._itemsList.View = System.Windows.Forms.View.Details;
            // 
            // _calculatedAttrTabPage
            // 
            this._calculatedAttrTabPage.Controls.Add(this._calcAttrList);
            this._calculatedAttrTabPage.Location = new System.Drawing.Point(4, 22);
            this._calculatedAttrTabPage.Name = "_calculatedAttrTabPage";
            this._calculatedAttrTabPage.Size = new System.Drawing.Size(222, 223);
            this._calculatedAttrTabPage.TabIndex = 1;
            this._calculatedAttrTabPage.Text = "Attribute Statistics";
            // 
            // _calcAttrList
            // 
            this._calcAttrList.AllowColumnReorder = true;
            this._calcAttrList.BackColor = System.Drawing.Color.White;
            this._calcAttrList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._calcAttrList.ForeColor = System.Drawing.Color.Black;
            this._calcAttrList.FullRowSelect = true;
            this._calcAttrList.Location = new System.Drawing.Point(0, 0);
            this._calcAttrList.MultiSelect = false;
            this._calcAttrList.Name = "_calcAttrList";
            this._calcAttrList.Size = new System.Drawing.Size(222, 223);
            this._calcAttrList.TabIndex = 0;
            this._calcAttrList.UseCompatibleStateImageBehavior = false;
            this._calcAttrList.View = System.Windows.Forms.View.Details;
            // 
            // worker
            // 
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DoWork);
            this.worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgessChanged);
            this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CompleteWork);
            // 
            // g_o
            // 
            this.g_o.Location = new System.Drawing.Point(0, 0);
            this.g_o.Name = "g_o";
            this.g_o.Size = new System.Drawing.Size(200, 100);
            this.g_o.TabIndex = 0;
            this.g_o.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "均值:从";
            // 
            // avg_t_f
            // 
            this.avg_t_f.Location = new System.Drawing.Point(69, 27);
            this.avg_t_f.Name = "avg_t_f";
            this.avg_t_f.Size = new System.Drawing.Size(35, 21);
            this.avg_t_f.TabIndex = 2;
            this.avg_t_f.Text = "4000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "至";
            // 
            // avg_t_t
            // 
            this.avg_t_t.Location = new System.Drawing.Point(135, 28);
            this.avg_t_t.Name = "avg_t_t";
            this.avg_t_t.Size = new System.Drawing.Size(32, 21);
            this.avg_t_t.TabIndex = 4;
            this.avg_t_t.Text = "7000";
            // 
            // avg_c
            // 
            this.avg_c.Checked = true;
            this.avg_c.CheckState = System.Windows.Forms.CheckState.Checked;
            this.avg_c.Location = new System.Drawing.Point(177, 31);
            this.avg_c.Name = "avg_c";
            this.avg_c.Size = new System.Drawing.Size(15, 14);
            this.avg_c.TabIndex = 5;
            this.avg_c.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "标准差:从";
            // 
            // var_t_f
            // 
            this.var_t_f.Location = new System.Drawing.Point(69, 57);
            this.var_t_f.Name = "var_t_f";
            this.var_t_f.Size = new System.Drawing.Size(35, 21);
            this.var_t_f.TabIndex = 7;
            this.var_t_f.Text = "300";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(111, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "至";
            // 
            // var_t_t
            // 
            this.var_t_t.Location = new System.Drawing.Point(134, 17);
            this.var_t_t.Name = "var_t_t";
            this.var_t_t.Size = new System.Drawing.Size(33, 21);
            this.var_t_t.TabIndex = 9;
            this.var_t_t.Text = "400";
            // 
            // var_c
            // 
            this.var_c.Checked = true;
            this.var_c.CheckState = System.Windows.Forms.CheckState.Checked;
            this.var_c.Location = new System.Drawing.Point(176, 21);
            this.var_c.Name = "var_c";
            this.var_c.Size = new System.Drawing.Size(15, 14);
            this.var_c.TabIndex = 10;
            this.var_c.UseVisualStyleBackColor = true;
            // 
            // chart_chayi
            // 
            chartArea2.Name = "ChartArea2";
            this.chart_chayi.ChartAreas.Add(chartArea2);
            this.chart_chayi.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend2.Name = "Legend2";
            this.chart_chayi.Legends.Add(legend2);
            this.chart_chayi.Location = new System.Drawing.Point(0, 129);
            this.chart_chayi.Name = "chart_chayi";
            this.chart_chayi.Size = new System.Drawing.Size(768, 120);
            this.chart_chayi.TabIndex = 1;
            this.chart_chayi.Text = "chart2";
            // 
            // btn_chayi
            // 
            this.btn_chayi.Location = new System.Drawing.Point(8, 102);
            this.btn_chayi.Name = "btn_chayi";
            this.btn_chayi.Size = new System.Drawing.Size(75, 23);
            this.btn_chayi.TabIndex = 2;
            this.btn_chayi.Text = "全选";
            this.btn_chayi.UseVisualStyleBackColor = true;
            // 
            // dgvUserWarn
            // 
            this.dgvUserWarn.AllowUserToAddRows = false;
            this.dgvUserWarn.AllowUserToDeleteRows = false;
            this.dgvUserWarn.AutoGenerateColumns = false;
            this.dgvUserWarn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserWarn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkUserWarn,
            this.useridWarn,
            this.pUserWarn});
            this.dgvUserWarn.Location = new System.Drawing.Point(0, 0);
            this.dgvUserWarn.Name = "dgvUserWarn";
            this.dgvUserWarn.RowTemplate.Height = 23;
            this.dgvUserWarn.Size = new System.Drawing.Size(700, 200);
            this.dgvUserWarn.TabIndex = 0;
            // 
            // _userwarnpage
            // 
            this._userwarnpage.Controls.Add(this.dgvUserWarn);
            this._userwarnpage.Location = new System.Drawing.Point(0, 0);
            this._userwarnpage.Name = "_userwarnpage";
            this._userwarnpage.Size = new System.Drawing.Size(200, 100);
            this._userwarnpage.TabIndex = 0;
            this._userwarnpage.Text = "零值异常用户";
            // 
            // TimeSearcherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 541);
            this.Controls.Add(this._topPanel);
            this.Controls.Add(this._hSplitter);
            this.Controls.Add(this._bottomPanel);
            this.Controls.Add(this._statusBar);
            this.Controls.Add(this.tbBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this._mainMenu;
            this.Name = "TimeSearcherForm";
            this.Text = "TimeSearcher";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TimeSearcherForm_FormClosed);
            this.Load += new System.EventHandler(this.TimeSearcherForm_Load);
            this._bottomPanel.ResumeLayout(false);
            this._bottom_right_panel.ResumeLayout(false);
            this.g_s.ResumeLayout(false);
            this.g_s.PerformLayout();
            this._detailTabControl.ResumeLayout(false);
            this._detailpage.ResumeLayout(false);
            this._userTabControl.ResumeLayout(false);
            this._userpage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectAll)).EndInit();
            this.dgvSelectAll.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this._topPanel.ResumeLayout(false);
            this._mainTabControl.ResumeLayout(false);
            this._tab_fuzhu.ResumeLayout(false);
            this._tab.ResumeLayout(false);
            this._tab_a.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.schedulerControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerStorage1)).EndInit();
            this.tab_rule_static.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rule_p)).EndInit();
            this.tab_rule_fuzhu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_fuzhu_static)).EndInit();
            this._sxTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_sx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._sxdgv)).EndInit();
            this._chayiTabpage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chanyi_dgv)).EndInit();
            this._itemsTabControl.ResumeLayout(false);
            this._itemsListTabPage.ResumeLayout(false);
            this._calculatedAttrTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_chayi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserWarn)).EndInit();
            this._userwarnpage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridViewTextBoxColumn USERID;
        private MenuItem menuItem2;
        private TabPage _tab_fuzhu;
        private TabControl _tab;
        private TabPage _tab_a;
        private MenuItem menuItem4;
        private MenuItem menuItem6;
        private MenuItem menuItem7;
        private MenuItem menuItem8;
        private MenuItem menuItem9;
        private MenuItem menuItem10;
        private TabPage _allTabPage;
        private DevExpress.XtraScheduler.SchedulerControl schedulerControl1;
        private DevExpress.XtraScheduler.SchedulerStorage schedulerStorage1;
        private MenuItem menuItem11;
        private TabPage tab_rule_static;
        private DataGridView dgv_rule_p;
        private TabPage tab_rule_fuzhu;
        private DataGridView dgv_fuzhu_static;
        private MenuItem menuItem12;
        private MenuItem menuItem14;
        private MenuItem menuItem15;
        private MenuItem menuItem5;
        private MenuItem menuItem13;
        private Label label7;
        private MenuItem menuItem16;
        private Button button1;
        private Label label8;
        private Label 检查信息;
        private ComboBox comboBox1;
        private MenuItem menuItem17;
        private MenuItem menuItem18;
    }
}