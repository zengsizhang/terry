using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBHelper
{
    public class DataTableHelp
    {        

        public static DataTable RowToColumn(DataTable sourseDT, string column, string ValueCol)
        {
            string sortCol = ""; string[] sortCols = new string[sourseDT.Columns.Count];
            DataTable result = new DataTable(); DataTable dtTemp = new DataTable();
            string groupCol = ""; int groupColNum = 0;
            foreach (DataColumn item in sourseDT.Columns)//添加除column、ValueCol外原来的列
            {
                if (item.Caption == column || item.Caption == ValueCol) continue;
                result.Columns.Add(item.Caption);
                if (groupCol.Length > 0) groupCol += ",";
                groupCol += item.Caption; groupColNum++;//标记除column、ValueCol外原来的列
            }
            sortCol = groupCol + "," + column + "," + ValueCol;
            sortCols = sortCol.Split(',');
            sourseDT = sourseDT.DefaultView.ToTable(false, sortCols);//重新排序sourseDT：除column、ValueCol外原来的列排在前面
            dtTemp = sourseDT.DefaultView.ToTable(true, column);//获取column列无重复数据
            foreach (DataRow item in dtTemp.Rows)//column列无重复列值全转为列
            {
                result.Columns.Add(item[column].ToString());
            }
            dtTemp = sourseDT.DefaultView.ToTable(true, groupCol.Split(','));//依据groupCol，得到分组分组依据行
            DataTable dtTemp1 = new DataTable();
            foreach (DataRow item in dtTemp.Rows)
            {
                object[] newRow = new object[result.Columns.Count];
                string tempfilter = "";
                foreach (DataColumn item1 in dtTemp.Columns)//根据dtTemp中的一条记录，从sourseDT筛选出一组数据
                {
                    if (tempfilter.Length > 0) tempfilter += " and ";
                    tempfilter += item1.ColumnName + "='" + item[item1].ToString() + "'";
                }
                sourseDT.DefaultView.RowFilter = tempfilter;
                dtTemp1 = sourseDT.DefaultView.ToTable();//筛选结果，新表的一行记录将从这个dtTemp1里取
                foreach (DataRow row in dtTemp1.Rows)
                {
                    for (int i = 0; i < result.Columns.Count; i++)
                    {
                        if (i < groupColNum)
                        {
                            newRow[i] = row[i];
                        }
                        else
                            if (row[column].ToString() == result.Columns[i].ColumnName)
                            {
                                newRow[i] = row[ValueCol].ToString(); break;
                            }
                    }
                }
                result.Rows.Add(newRow);
            }
            return result;
        }

    }
}
