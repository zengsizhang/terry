namespace LostMinerLib
{
    partial class moniter
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
            this.dgv_fuzhu = new System.Windows.Forms.DataGridView();
            this.用户号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A相电压 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B相电压 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C相电压 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A相电流 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B相电流 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C相电流 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A相功率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B相功率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C相功率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.有功功率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.总功率因素 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.主表表码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.负控表码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_fuzhu)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_fuzhu
            // 
            this.dgv_fuzhu.AllowUserToAddRows = false;
            this.dgv_fuzhu.AllowUserToDeleteRows = false;
            this.dgv_fuzhu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_fuzhu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.用户号,
            this.时间,
            this.A相电压,
            this.B相电压,
            this.C相电压,
            this.A相电流,
            this.B相电流,
            this.C相电流,
            this.A相功率,
            this.B相功率,
            this.C相功率,
            this.有功功率,
            this.总功率因素,
            this.主表表码,
            this.负控表码});
            this.dgv_fuzhu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_fuzhu.Location = new System.Drawing.Point(0, 0);
            this.dgv_fuzhu.Margin = new System.Windows.Forms.Padding(3, 100, 3, 3);
            this.dgv_fuzhu.Name = "dgv_fuzhu";
            this.dgv_fuzhu.RowTemplate.Height = 23;
            this.dgv_fuzhu.Size = new System.Drawing.Size(1336, 470);
            this.dgv_fuzhu.TabIndex = 18;
            // 
            // 用户号
            // 
            this.用户号.DataPropertyName = "user_id";
            this.用户号.HeaderText = "用户号";
            this.用户号.Name = "用户号";
            // 
            // 时间
            // 
            this.时间.DataPropertyName = "DATE_TIME";
            this.时间.HeaderText = "时间";
            this.时间.Name = "时间";
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
            // A相电流
            // 
            this.A相电流.DataPropertyName = "A_V_A";
            this.A相电流.HeaderText = "A相电流";
            this.A相电流.Name = "A相电流";
            // 
            // B相电流
            // 
            this.B相电流.DataPropertyName = "B_V_B";
            this.B相电流.HeaderText = "B相电流";
            this.B相电流.Name = "B相电流";
            // 
            // C相电流
            // 
            this.C相电流.DataPropertyName = "C_V_C";
            this.C相电流.HeaderText = "C相电流";
            this.C相电流.Name = "C相电流";
            // 
            // A相功率
            // 
            this.A相功率.DataPropertyName = "A_KW";
            this.A相功率.HeaderText = "A相功率";
            this.A相功率.Name = "A相功率";
            // 
            // B相功率
            // 
            this.B相功率.DataPropertyName = "B_KW";
            this.B相功率.HeaderText = "B相功率";
            this.B相功率.Name = "B相功率";
            // 
            // C相功率
            // 
            this.C相功率.DataPropertyName = "C_KW";
            this.C相功率.HeaderText = "C相功率";
            this.C相功率.Name = "C相功率";
            // 
            // 有功功率
            // 
            this.有功功率.DataPropertyName = "CURR_KW";
            this.有功功率.HeaderText = "有功功率";
            this.有功功率.Name = "有功功率";
            // 
            // 总功率因素
            // 
            this.总功率因素.DataPropertyName = "TOTAL_KW";
            this.总功率因素.HeaderText = "总功率因素";
            this.总功率因素.Name = "总功率因素";
            // 
            // 主表表码
            // 
            this.主表表码.DataPropertyName = "M_ZYB";
            this.主表表码.HeaderText = "主表表码";
            this.主表表码.Name = "主表表码";
            // 
            // 负控表码
            // 
            this.负控表码.DataPropertyName = "F_ZYB";
            this.负控表码.HeaderText = "负控表码";
            this.负控表码.Name = "负控表码";
            // 
            // moniter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 470);
            this.Controls.Add(this.dgv_fuzhu);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "moniter";
            this.Text = "辅助数据";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_fuzhu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_fuzhu;
        private System.Windows.Forms.DataGridViewTextBoxColumn 用户号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn A相电压;
        private System.Windows.Forms.DataGridViewTextBoxColumn B相电压;
        private System.Windows.Forms.DataGridViewTextBoxColumn C相电压;
        private System.Windows.Forms.DataGridViewTextBoxColumn A相电流;
        private System.Windows.Forms.DataGridViewTextBoxColumn B相电流;
        private System.Windows.Forms.DataGridViewTextBoxColumn C相电流;
        private System.Windows.Forms.DataGridViewTextBoxColumn A相功率;
        private System.Windows.Forms.DataGridViewTextBoxColumn B相功率;
        private System.Windows.Forms.DataGridViewTextBoxColumn C相功率;
        private System.Windows.Forms.DataGridViewTextBoxColumn 有功功率;
        private System.Windows.Forms.DataGridViewTextBoxColumn 总功率因素;
        private System.Windows.Forms.DataGridViewTextBoxColumn 主表表码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 负控表码;
    }
}