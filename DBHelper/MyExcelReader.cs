using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;  

namespace DBHelper
{
    public class MyExcelReader
    {
        /// <summary>  
        /// Access 数据库连接字符串  
        /// </summary>  
        private string strConnectionn;
        /// <summary>  
        /// 要操作的Excel文件路径  
        /// </summary>  
        private string filePath;
        /// <summary>  
        /// Excel文件表名  
        /// </summary>  
        private string tableName;
        /// <summary>  
        /// 文件行数  
        /// </summary>  
        private int rowCount;
        /// <summary>  
        /// 列数  
        /// </summary>  
        private int columnCount;
        /// <summary>  
        /// 读取Excel文件到DataSet  
        /// </summary>  
        private DataSet data;


        #region 属性
        public DataSet DataSet
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }
        public string FilePath
        {
            get
            {
                return this.filePath;
            }
            set
            {
                this.filePath = value;
            }
        }
        public string TableName
        {
            get
            {
                return this.tableName;
            }
            set
            {
                this.tableName = value;
            }
        }
        public string StrConnection
        {
            get
            {
                return this.strConnectionn;
            }
            set
            {
                this.strConnectionn = value;
            }
        }
        public int RowsCount
        {
            get
            {
                return this.rowCount;
            }
        }
        public int ColumnCount
        {
            get
            {
                return this.columnCount;
            }
        }
        #endregion
        ~MyExcelReader()
        {
        }
        public MyExcelReader(string path, string table)
        {
            
            this.FilePath = path;
            this.TableName = table;
            Init();
            System.Diagnostics.Process[] excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            DateTime startTime = new DateTime();
            int m, killId = 0;
            for (m = 0; m < excelProc.Length; m++)
            {
                if (startTime <= excelProc[m].StartTime)
                {
                    startTime = excelProc[m].StartTime;
                    killId = m;
                }
            }
            if (killId!=0)
            {
            if (excelProc[killId].HasExited == false)
            {
                excelProc[killId].Kill();
            }
            }
            System.Threading.Thread.Sleep(1000);
        }
        public MyExcelReader(string path)
        {
            this.FilePath = path;
            this.TableName = "Sheet1";
            Init();

        }
        private void Init()
        {
            this.StrConnection = @"Provider=Microsoft.Ace.OleDb.12.0;Extended Properties='Excel 12.0; HDR=Yes; IMEX=1';Data Source= ";
            //strConnectionn = @"Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 12.0 Xml;HDR=YES;IMEX=1;Data Source= ";  
            try
            {
                this.DataSet = CreateDateSet();
            }
            catch (Exception ex)
            {
                this.DataSet = null;
            }
        }
        #region DataSet
        /// <summary>  
        /// 读取Excel到DataSet，必须在构造函数中，输入文件路径，标明为默认的Sheet1  
        /// </summary>  
        /// <returns></returns>  
        public DataSet ReadExcelToDataSet()
        {
            return this.DataSet;
        }
        /// <summary>  
        /// 生成DataSet  
        /// </summary>  
        /// <param name="strConn">链接字符串</param>  
        /// <param name="tableName">表名</param>  
        /// <returns></returns>  
        private DataSet CreateDateSet()
        {
            this.StrConnection = "Provider=Microsoft.Ace.OleDb.12.0;Extended Properties=\"Excel 8.0\";Data Source= ";
           // string strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'", this.FilePath);

            string strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'", this.FilePath); 
            
            string sqlConn = "SELECT * FROM [" + this.TableName + "$]";
           // string strConn = this.StrConnection + this.FilePath;
            OleDbConnection conn = new OleDbConnection(strConn);
            // string sqlConn = "SELECT * FROM [sheet1$]";  
            OleDbDataAdapter myCommand = new OleDbDataAdapter(sqlConn, strConn);
            DataSet myDataSet = new DataSet();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
               strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'", this.FilePath);
               conn = new OleDbConnection(strConn);
               conn.Open();
                if (conn != null)
                {
                    conn.Close();
                }
                throw new Exception("该Excel文件的工作表的名字不正确," + FilePath + ex.Message);
            }
                myCommand.Fill(myDataSet);
                int row = myDataSet.Tables[0].Rows.Count;
                int col = myDataSet.Tables[0].Columns.Count;
                this.rowCount = (row > 0) ? row : 0;
                this.columnCount = (col > 0) ? col : 0;
                conn.Close();
          
            return myDataSet;
        }
        #endregion
        #region RowsCount
        /// <summary>  
        ///   
        /// </summary>  
        /// <returns></returns>  
        public int GetExcelRowsCount()
        {
            int val = -1;
            val = this.RowsCount;
            return val;
        }
        #endregion
        #region ColumnsCount
        /// <summary>  
        ///   
        /// </summary>  
        /// <returns></returns>  
        public int GetExcelColumnsCount()
        {
            int val = -1;
            val = this.ColumnCount;
            return val;
        }
        #endregion
        #region GetColumnsByArray
        /// <summary>  
        /// 以数组形式返回Excel中的一列  
        /// </summary>  
        /// <param name="columns">列下表，从0开始，为第一列</param>  
        /// <returns></returns>  
        public string[] GetColumnsByArray(int columns)
        {
            string[] val;
            if ((columns < 0) || (columns >= this.ColumnCount))
            {
                val = null;
                return val;
            }
            val = new string[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                val[i] = this.DataSet.Tables[0].Rows[i][columns].ToString();
            }
            return val;
        }
        public string[] GetColumnsByArray(int columns, int rowStart, int rowEnd)
        {
            string[] val;
            // DataSet data;  
            int Count;
            if ((columns < 0) || (columns >= this.ColumnCount)
                || rowStart < 0 || rowStart >= rowEnd
                || rowEnd >= this.ColumnCount)
            {
                val = null;
                return val;
            }
            Count = rowEnd - rowStart + 1;
            if (Count > 0)
            {
                val = new string[Count];
                int len = this.RowsCount;
                for (int i = 0; i < len; i++)
                {
                    if ((i >= rowStart) && (i <= rowEnd))
                    {
                        val[i] = this.DataSet.Tables[0].Rows[i][columns].ToString();
                    }
                }
            }
            else
            {
                val = null;
            }
            return val;
        }
        #endregion
        #region getRowValue
        /// <summary>  
        /// 获取一行得值  
        /// </summary>  
        /// <param name="row">行值</param>  
        /// <returns></returns>  
        public string[] GetRowByArray(int row)
        {
            int length = this.ColumnCount;
            string[] val;
            if ((row < 0) || (row >= this.RowsCount) || (length < 0))
            {
                return null;
            }
            val = new string[length];
            for (int i = 0; i < length; i++)
            {
                val[i] = this.DataSet.Tables[0].Rows[row][i].ToString();
            }
            return val;
        }
        #endregion
        #region  去读单元格
        /// <summary>  
        /// 或者某个单元格的值  
        /// </summary>  
        /// <param name="row">行坐标</param>  
        /// <param name="col">列坐标</param>  
        /// <returns></returns>  
        public string GetCellValue(int row, int col)
        {
            string val;
            if ((row < 0) || (col < 0) || (row >= this.RowsCount) || (col >= this.ColumnCount))
            {
                val = null;
                return val;
            }
            val = this.DataSet.Tables[0].Rows[row][col].ToString();
            return val;
        }
        #endregion
    }  
}
