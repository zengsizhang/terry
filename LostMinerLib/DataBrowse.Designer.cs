namespace LostMinerLib
{
    partial class DataBrowse
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.线路 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.供电量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.售电量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.用户 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.用电量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.用户号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.采集时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 105);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(832, 480);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(824, 454);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "线路数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(818, 448);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.线路,
            this.时间,
            this.供电量,
            this.售电量});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(818, 448);
            this.dataGridView1.TabIndex = 0;
            // 
            // 线路
            // 
            this.线路.DataPropertyName = "line_no";
            this.线路.HeaderText = "线路";
            this.线路.Name = "线路";
            // 
            // 时间
            // 
            this.时间.DataPropertyName = "date_time";
            this.时间.HeaderText = "时间";
            this.时间.Name = "时间";
            // 
            // 供电量
            // 
            this.供电量.DataPropertyName = "total_q";
            this.供电量.HeaderText = "供电量";
            this.供电量.Name = "供电量";
            // 
            // 售电量
            // 
            this.售电量.DataPropertyName = "user_q";
            this.售电量.HeaderText = "售电量";
            this.售电量.Name = "售电量";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(505, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(364, 21);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(115, 21);
            this.dateTimePicker2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(326, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "至";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(200, 21);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(115, 21);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "线路";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(78, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(824, 454);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "用户数据";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dataGridView2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(818, 448);
            this.panel5.TabIndex = 2;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.用户,
            this.用电量,
            this.dataGridViewTextBoxColumn2});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(818, 448);
            this.dataGridView2.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "line_no";
            this.dataGridViewTextBoxColumn1.HeaderText = "线路";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // 用户
            // 
            this.用户.DataPropertyName = "user_id";
            this.用户.HeaderText = "用户";
            this.用户.Name = "用户";
            // 
            // 用电量
            // 
            this.用电量.DataPropertyName = "used_q";
            this.用电量.HeaderText = "用电量";
            this.用电量.Name = "用电量";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "date_time";
            this.dataGridViewTextBoxColumn2.HeaderText = "时间";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel6);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(824, 454);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "辅助变量数据";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dataGridView3);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(824, 454);
            this.panel6.TabIndex = 2;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.用户号,
            this.采集时间,
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
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(0, 0);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 23;
            this.dataGridView3.Size = new System.Drawing.Size(824, 454);
            this.dataGridView3.TabIndex = 2;
            // 
            // 用户号
            // 
            this.用户号.DataPropertyName = "user_id";
            this.用户号.HeaderText = "用户号";
            this.用户号.Name = "用户号";
            // 
            // 采集时间
            // 
            this.采集时间.DataPropertyName = "date_time";
            this.采集时间.HeaderText = "时间";
            this.采集时间.Name = "采集时间";
            // 
            // A相电压
            // 
            this.A相电压.DataPropertyName = "a_v";
            this.A相电压.HeaderText = "A相电压";
            this.A相电压.Name = "A相电压";
            // 
            // B相电压
            // 
            this.B相电压.DataPropertyName = "b_v";
            this.B相电压.HeaderText = "B相电压";
            this.B相电压.Name = "B相电压";
            // 
            // C相电压
            // 
            this.C相电压.DataPropertyName = "c_v";
            this.C相电压.HeaderText = "C相电压";
            this.C相电压.Name = "C相电压";
            // 
            // A相电流
            // 
            this.A相电流.DataPropertyName = "a_v_a";
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
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(78, 58);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "线路";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(141, 58);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "用户";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(202, 58);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 16);
            this.checkBox3.TabIndex = 9;
            this.checkBox3.Text = "辅助数据";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // DataBrowse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 585);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "DataBrowse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据浏览";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 线路;
        private System.Windows.Forms.DataGridViewTextBoxColumn 时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 供电量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 售电量;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 用户;
        private System.Windows.Forms.DataGridViewTextBoxColumn 用电量;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridViewTextBoxColumn 用户号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 采集时间;
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
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
    }
}