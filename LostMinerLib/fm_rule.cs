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
    public partial class fm_rule : Form
    {
        public fm_rule()
        {
            InitializeComponent();
            INIClass iniclass = new INIClass(Application.StartupPath + @"\system.ini");
            ck_rule_1.Checked = bool.Parse(iniclass.IniReadValue("rule1", "is_used"));
            tx_shiya_v.Text = iniclass.IniReadValue("rule1", "shiya_v");
            tx_shiya_p.Text=iniclass.IniReadValue("rule1", "shiya_p");

            ck_rule_4.Checked = bool.Parse(iniclass.IniReadValue("rule4", "is_used"));
            tx_fanjixing_p.Text = iniclass.IniReadValue("rule4", "fanjixing_p");
            tx_fj_p.Text = iniclass.IniReadValue("rule4", "fj_p");

            ck_rule_6.Checked = bool.Parse(iniclass.IniReadValue("rule6", "is_used"));
            tx_p_biaoma.Text = iniclass.IniReadValue("rule6", "p_biaoma");
            tx_biaoma.Text = iniclass.IniReadValue("rule6", "biaoma");

            ck_rule_7.Checked = bool.Parse(iniclass.IniReadValue("rule7", "is_used"));
            tx_gonglv_p.Text = iniclass.IniReadValue("rule7", "gonglv_p");
            tx_dushu_1.Text = iniclass.IniReadValue("rule7", "dushu_1");
            tx_dushu_2.Text = iniclass.IniReadValue("rule7", "dushu_2");
            tx_gonglv_p_2.Text = iniclass.IniReadValue("rule7", "gonglv_p_2");
            tx_gonglv_p_3.Text = iniclass.IniReadValue("rule7", "gonglv_p_3");
            tx_gonglv_a.Text = iniclass.IniReadValue("rule7", "gonglv_a");
            tx_gonglv_c.Text = iniclass.IniReadValue("rule7", "gonglv_c");

            ck_rule_8.Checked = bool.Parse(iniclass.IniReadValue("rule8", "is_used"));


            ck_rule_3.Checked = bool.Parse(iniclass.IniReadValue("rule3", "is_used"));
            tx_av_v.Text = iniclass.IniReadValue("rule3", "av_v");
            tx_av_p.Text = iniclass.IniReadValue("rule3", "av_p");

            ck_rule_5.Checked = bool.Parse(iniclass.IniReadValue("rule5", "is_used"));
            tx_av_o.Text = iniclass.IniReadValue("rule5", "av_o");
            tx_gonglvys.Text = iniclass.IniReadValue("rule5", "gonglvys");
            tx_glycys_p.Text = iniclass.IniReadValue("rule5", "glycys_p");

            ck_rule_9.Checked = bool.Parse(iniclass.IniReadValue("rule9", "is_used"));
            tx_glbyz.Text = iniclass.IniReadValue("rule9", "glbyz");
           

            ck_rule_2.Checked = bool.Parse(iniclass.IniReadValue("rule2", "is_used"));
            tx_shiliu.Text = iniclass.IniReadValue("rule2", "shiliu");
            tx_shiliu_p.Text = iniclass.IniReadValue("rule2", "shiliu_p");



        }

        private void button1_Click(object sender, EventArgs e)
        {

             INIClass iniclass = new INIClass(Application.StartupPath + @"\system.ini");
            //失压
            iniclass.IniWriteValue("rule1", "is_used",ck_rule_1.Checked.ToString());
            iniclass.IniWriteValue("rule1", "shiya_v", tx_shiya_v.Text);
            iniclass.IniWriteValue("rule1", "shiya_p", tx_shiya_p.Text);
            //反极性
            iniclass.IniWriteValue("rule4", "is_used", ck_rule_4.Checked.ToString());
            iniclass.IniWriteValue("rule4", "fanjixing_p", tx_fanjixing_p.Text);
            iniclass.IniWriteValue("rule4", "shiya_p", tx_fj_p.Text);
            //电量差
            iniclass.IniWriteValue("rule6", "is_used", ck_rule_6.Checked.ToString());
            iniclass.IniWriteValue("rule6", "fanjixing_p", tx_p_biaoma.Text);
            iniclass.IniWriteValue("rule6", "shiya_p", tx_biaoma.Text);
            //功率异常
            iniclass.IniWriteValue("rule7", "is_used", ck_rule_7.Checked.ToString());
            iniclass.IniWriteValue("rule7", "gonglv_p", tx_gonglv_p.Text);
            iniclass.IniWriteValue("rule7", "dushu_1", tx_dushu_1.Text);
            iniclass.IniWriteValue("rule7", "dushu_2", tx_dushu_2.Text);
            iniclass.IniWriteValue("rule7", "gonglv_p_2", tx_gonglv_p_2.Text);
            iniclass.IniWriteValue("rule7", "gonglv_p_3", tx_gonglv_p_3.Text);
            iniclass.IniWriteValue("rule7", "gonglv_a", tx_gonglv_a.Text);
            iniclass.IniWriteValue("rule7", "gonglv_c", tx_gonglv_c.Text);
            //不完全星形接线中线断线
            iniclass.IniWriteValue("rule8", "is_used", ck_rule_8.Checked.ToString());
            //电流不平衡
            iniclass.IniWriteValue("rule3", "is_used", ck_rule_3.Checked.ToString());
            iniclass.IniWriteValue("rule3", "av_v", tx_av_v.Text);
            iniclass.IniWriteValue("rule3", "av_p", tx_av_p.Text);
            //功率因数异常
            iniclass.IniWriteValue("rule5", "is_used", ck_rule_5.Checked.ToString());
            iniclass.IniWriteValue("rule5", "av_o", tx_av_o.Text);
            iniclass.IniWriteValue("rule5", "gonglvys", tx_gonglvys.Text);
            iniclass.IniWriteValue("rule5", "glycys_p", tx_glycys_p.Text);
            //功率不一致
            iniclass.IniWriteValue("rule9", "is_used", ck_rule_9.Checked.ToString());
            iniclass.IniWriteValue("rule9", "glbyz", tx_glbyz.Text);
            //失流
            iniclass.IniWriteValue("rule2", "is_used", ck_rule_2.Checked.ToString());
            iniclass.IniWriteValue("rule2", "shiliu", tx_shiliu.Text);
            iniclass.IniWriteValue("rule2", "shiliu_p", tx_shiliu_p.Text);

            MessageBox.Show("保存成功");
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ck_rule_1.Checked = checkBox1.Checked;
            ck_rule_2.Checked = checkBox1.Checked;
            ck_rule_3.Checked = checkBox1.Checked;
            ck_rule_4.Checked = checkBox1.Checked;
            ck_rule_5.Checked = checkBox1.Checked;
            ck_rule_6.Checked = checkBox1.Checked;
            ck_rule_7.Checked = checkBox1.Checked;
            ck_rule_8.Checked = checkBox1.Checked;
            ck_rule_9.Checked = checkBox1.Checked;


        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //ck_rule_1.Checked = checkBox2.Checked;
            //ck_rule_2.Checked = checkBox2.Checked;
            //ck_rule_3.Checked = checkBox2.Checked;
            //ck_rule_4.Checked = checkBox2.Checked;
            //ck_rule_5.Checked = checkBox2.Checked;
            //ck_rule_6.Checked = checkBox2.Checked;
            //ck_rule_7.Checked = checkBox2.Checked;
            //ck_rule_8.Checked = checkBox2.Checked;
            //ck_rule_9.Checked = checkBox2.Checked;
        }
    }
}
