namespace LostMinerLib
{
    partial class task_manager
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
            this.线路 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数据开始时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数据结束时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数据导入时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.检查情况 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sel_btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.line_no = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.线路,
            this.数据开始时间,
            this.数据结束时间,
            this.数据导入时间,
            this.检查情况});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(843, 339);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // 线路
            // 
            this.线路.DataPropertyName = "line_no";
            this.线路.HeaderText = "线路";
            this.线路.Name = "线路";
            // 
            // 数据开始时间
            // 
            this.数据开始时间.DataPropertyName = "min_time";
            this.数据开始时间.HeaderText = "数据开始时间";
            this.数据开始时间.Name = "数据开始时间";
            // 
            // 数据结束时间
            // 
            this.数据结束时间.DataPropertyName = "max_time";
            this.数据结束时间.HeaderText = "数据结束时间";
            this.数据结束时间.Name = "数据结束时间";
            // 
            // 数据导入时间
            // 
            this.数据导入时间.DataPropertyName = "import_time";
            this.数据导入时间.HeaderText = "数据导入时间";
            this.数据导入时间.Name = "数据导入时间";
            // 
            // 检查情况
            // 
            this.检查情况.DataPropertyName = "is_check";
            this.检查情况.HeaderText = "检查情况";
            this.检查情况.Name = "检查情况";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sel_btn);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.line_no);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 100);
            this.panel1.TabIndex = 1;
            // 
            // sel_btn
            // 
            this.sel_btn.Location = new System.Drawing.Point(643, 26);
            this.sel_btn.Name = "sel_btn";
            this.sel_btn.Size = new System.Drawing.Size(75, 23);
            this.sel_btn.TabIndex = 6;
            this.sel_btn.Text = "查询";
            this.sel_btn.UseVisualStyleBackColor = true;
            this.sel_btn.Click += new System.EventHandler(this.sel_btn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(411, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "_____";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(464, 26);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(150, 21);
            this.dateTimePicker2.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(254, 26);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(140, 21);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "导入时间段";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "线路";
            // 
            // line_no
            // 
            this.line_no.Location = new System.Drawing.Point(60, 26);
            this.line_no.Name = "line_no";
            this.line_no.Size = new System.Drawing.Size(100, 21);
            this.line_no.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(843, 339);
            this.panel2.TabIndex = 2;
            // 
            // task_manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 439);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "task_manager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "任务管理";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox line_no;
        private System.Windows.Forms.Button sel_btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 线路;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数据开始时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数据结束时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数据导入时间;
        private System.Windows.Forms.DataGridViewButtonColumn 检查情况;
    }
}