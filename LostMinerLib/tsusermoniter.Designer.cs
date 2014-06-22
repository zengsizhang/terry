namespace LostMinerLib
{
    partial class tsusermoniter
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.用户号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.计量点 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.采集时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A相电压 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B相电压 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C相电压 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A相电流有效值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B相电流有效值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C相电流有效值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.当前有功功率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.当前无功功率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A相有功功率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B相有功功率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C相有功功率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.总功率因数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A相功率因数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B相功率因数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C相功率因数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.功率不平衡率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.电表资产号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.电流不平衡率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.用户号,
            this.计量点,
            this.采集时间,
            this.A相电压,
            this.B相电压,
            this.C相电压,
            this.A相电流有效值,
            this.B相电流有效值,
            this.C相电流有效值,
            this.当前有功功率,
            this.当前无功功率,
            this.A相有功功率,
            this.B相有功功率,
            this.C相有功功率,
            this.总功率因数,
            this.A相功率因数,
            this.B相功率因数,
            this.C相功率因数,
            this.功率不平衡率,
            this.电表资产号,
            this.电流不平衡率});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(756, 481);
            this.dataGridView1.TabIndex = 0;
            // 
            // 用户号
            // 
            this.用户号.DataPropertyName = "user_id";
            this.用户号.HeaderText = "用户号";
            this.用户号.Name = "用户号";
            // 
            // 计量点
            // 
            this.计量点.DataPropertyName = "jl_point";
            this.计量点.HeaderText = "计量点";
            this.计量点.Name = "计量点";
            // 
            // 采集时间
            // 
            this.采集时间.DataPropertyName = "date_time";
            this.采集时间.HeaderText = "采集时间";
            this.采集时间.Name = "采集时间";
            // 
            // A相电压
            // 
            this.A相电压.DataPropertyName = "A_V";
            this.A相电压.HeaderText = "A相电压";
            this.A相电压.Name = "A相电压";
            // 
            // B相电压
            // 
            this.B相电压.DataPropertyName = "B_V";
            this.B相电压.HeaderText = "B相电压";
            this.B相电压.Name = "B相电压";
            // 
            // C相电压
            // 
            this.C相电压.DataPropertyName = "C_V";
            this.C相电压.HeaderText = "C相电压";
            this.C相电压.Name = "C相电压";
            // 
            // A相电流有效值
            // 
            this.A相电流有效值.DataPropertyName = "A_V_A";
            this.A相电流有效值.HeaderText = "A相电流有效值";
            this.A相电流有效值.Name = "A相电流有效值";
            // 
            // B相电流有效值
            // 
            this.B相电流有效值.DataPropertyName = "B_V_B";
            this.B相电流有效值.HeaderText = "B相电流有效值";
            this.B相电流有效值.Name = "B相电流有效值";
            // 
            // C相电流有效值
            // 
            this.C相电流有效值.DataPropertyName = "C_V_C";
            this.C相电流有效值.HeaderText = "C相电流有效值";
            this.C相电流有效值.Name = "C相电流有效值";
            // 
            // 当前有功功率
            // 
            this.当前有功功率.DataPropertyName = "CURR_KW";
            this.当前有功功率.HeaderText = "当前有功功率";
            this.当前有功功率.Name = "当前有功功率";
            // 
            // 当前无功功率
            // 
            this.当前无功功率.DataPropertyName = "CURR_KVAR";
            this.当前无功功率.HeaderText = "当前无功功率";
            this.当前无功功率.Name = "当前无功功率";
            // 
            // A相有功功率
            // 
            this.A相有功功率.DataPropertyName = "A_KW";
            this.A相有功功率.HeaderText = "A相有功功率";
            this.A相有功功率.Name = "A相有功功率";
            // 
            // B相有功功率
            // 
            this.B相有功功率.DataPropertyName = "B_KW";
            this.B相有功功率.HeaderText = "B相有功功率";
            this.B相有功功率.Name = "B相有功功率";
            // 
            // C相有功功率
            // 
            this.C相有功功率.DataPropertyName = "C_KW";
            this.C相有功功率.HeaderText = "C相有功功率";
            this.C相有功功率.Name = "C相有功功率";
            // 
            // 总功率因数
            // 
            this.总功率因数.DataPropertyName = "TOTAL_KW";
            this.总功率因数.HeaderText = "总功率因数";
            this.总功率因数.Name = "总功率因数";
            // 
            // A相功率因数
            // 
            this.A相功率因数.DataPropertyName = "A_KW_S";
            this.A相功率因数.HeaderText = "A相功率因数";
            this.A相功率因数.Name = "A相功率因数";
            // 
            // B相功率因数
            // 
            this.B相功率因数.DataPropertyName = "B_KW_S";
            this.B相功率因数.HeaderText = "B相功率因数";
            this.B相功率因数.Name = "B相功率因数";
            // 
            // C相功率因数
            // 
            this.C相功率因数.DataPropertyName = "C_KW_S";
            this.C相功率因数.HeaderText = "C相功率因数";
            this.C相功率因数.Name = "C相功率因数";
            // 
            // 功率不平衡率
            // 
            this.功率不平衡率.DataPropertyName = "KW_BANLACE";
            this.功率不平衡率.HeaderText = "功率不平衡率";
            this.功率不平衡率.Name = "功率不平衡率";
            // 
            // 电表资产号
            // 
            this.电表资产号.DataPropertyName = "M_NO";
            this.电表资产号.HeaderText = "电表资产号";
            this.电表资产号.Name = "电表资产号";
            // 
            // 电流不平衡率
            // 
            this.电流不平衡率.DataPropertyName = "A_BALANCE_NO";
            this.电流不平衡率.HeaderText = "电流不平衡率";
            this.电流不平衡率.Name = "电流不平衡率";
            // 
            // tsusermoniter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 481);
            this.Controls.Add(this.dataGridView1);
            this.Name = "tsusermoniter";
            this.Text = "tsusermoniter";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 用户号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 计量点;
        private System.Windows.Forms.DataGridViewTextBoxColumn 采集时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn A相电压;
        private System.Windows.Forms.DataGridViewTextBoxColumn B相电压;
        private System.Windows.Forms.DataGridViewTextBoxColumn C相电压;
        private System.Windows.Forms.DataGridViewTextBoxColumn A相电流有效值;
        private System.Windows.Forms.DataGridViewTextBoxColumn B相电流有效值;
        private System.Windows.Forms.DataGridViewTextBoxColumn C相电流有效值;
        private System.Windows.Forms.DataGridViewTextBoxColumn 当前有功功率;
        private System.Windows.Forms.DataGridViewTextBoxColumn 当前无功功率;
        private System.Windows.Forms.DataGridViewTextBoxColumn A相有功功率;
        private System.Windows.Forms.DataGridViewTextBoxColumn B相有功功率;
        private System.Windows.Forms.DataGridViewTextBoxColumn C相有功功率;
        private System.Windows.Forms.DataGridViewTextBoxColumn 总功率因数;
        private System.Windows.Forms.DataGridViewTextBoxColumn A相功率因数;
        private System.Windows.Forms.DataGridViewTextBoxColumn B相功率因数;
        private System.Windows.Forms.DataGridViewTextBoxColumn C相功率因数;
        private System.Windows.Forms.DataGridViewTextBoxColumn 功率不平衡率;
        private System.Windows.Forms.DataGridViewTextBoxColumn 电表资产号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 电流不平衡率;
    }
}