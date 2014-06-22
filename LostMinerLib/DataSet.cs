namespace TimeSearcher
{
   
    using System;
    using System.Collections;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using TimeSearcher.Attribute;
    using TimeSearcher.Widgets;
    using Util.Stat;

    public class DataSet
    {
        private AttributeOracle _attrOracle;
        private DataItem[] _dataItems;
        private readonly TimeSearcher.DisablingManager _disablingManager;
        private string[] _dynamicVariableNames;
        private string _filenamePath;
        private int _highlightedItemIdx;
        private string _itemTabName;
        private string _lastAttrPath;
        private int _nDimensions;
        private int _numItems;
        private int _numTimePoints;
        private int _prevSelectedItemIdx;
        private StaticVariable _staticVariable;
        private string _strTitle;
        private readonly TimeSearcher.Widgets.TimeBoxManager _timeBoxManager;
        private string[] _timePointNames;
        public TimeSearcherForm _tsform;
        private double[] _variablesMaximums;
        private double[] _variablesMinimums;
        private const char COMMA = ',';
        public int currTimePtIndex;
        public DataType[] dataType;
        public DataTable dt;
        public const int ITEM_IDX_NONE = -1;
       // private ArrayList time_al = new ArrayList();

        public DataSet()
        {
            this._highlightedItemIdx = -1;
            this._prevSelectedItemIdx = -1;
            this.currTimePtIndex = -1;
            this._disablingManager = new TimeSearcher.DisablingManager(this);
            this._timeBoxManager = new TimeSearcher.Widgets.TimeBoxManager(this);
            this.reset();
        }

        public DataSet(TimeSearcherForm tsform)
        {
            this._highlightedItemIdx = -1;
            this._prevSelectedItemIdx = -1;
            this.currTimePtIndex = -1;
            this._disablingManager = new TimeSearcher.DisablingManager(this);
            this._timeBoxManager = new TimeSearcher.Widgets.TimeBoxManager(this);
            this._tsform = tsform;
            this.dt = tsform.user_dt;
            this.reset();
        }

        public int calcEnabledNumItems()
        {
            int num = 0;
            foreach (DataItem item in this._dataItems)
            {
                if (item.IsEnabled())
                {
                    num++;
                }
            }
            return num;
        }

        private double calculateVarMax(int varIdx)
        {
            double minValue = double.MinValue;
            foreach (DataItem item in this._dataItems)
            {
                double num2 = item.GetVar(varIdx).getMaxValue();
                if (num2 > minValue)
                {
                    minValue = num2;
                }
            }
            return minValue;
        }

        private double calculateVarMin(int varIdx)
        {
            double maxValue = double.MaxValue;
            foreach (DataItem item in this._dataItems)
            {
                double num2 = item.GetVar(varIdx).getMinValue();
                if (num2 < maxValue)
                {
                    maxValue = num2;
                }
            }
            return maxValue;
        }

        public static TimeSearcher.DataSet CloneDataSet(TimeSearcher.DataSet dataSet)
        {
            FileStream fileStream = new FileStream(dataSet._filenamePath, FileMode.Open);
            TimeSearcher.DataSet set = new TimeSearcher.DataSet();
            set.ReadFile(fileStream);
            return set;
        }

        public static DataTable Col2Row(DataTable src, int columnHead)
        {
            DataTable result = new DataTable();
            DataColumn myHead = src.Columns[columnHead];
            result.Columns.Add(myHead.ColumnName);
            int i = 0;
            while (i < src.Rows.Count)
            {
                result.Columns.Add(src.Rows[i][myHead].ToString());
                i++;
            }
            foreach (DataColumn col in src.Columns)
            {
                if (col != myHead)
                {
                    object[] newRow = new object[src.Rows.Count + 1];
                    newRow[0] = col.ColumnName;
                    for (i = 0; i < src.Rows.Count; i++)
                    {
                        newRow[i + 1] = src.Rows[i][col];
                    }
                    result.Rows.Add(newRow);
                }
            }
            return result;
        }

        public void DisableItems(int[] indices)
        {
            for (int i = 0; i < indices.Length; i++)
            {
                this._dataItems[i].disable();
            }
        }

        public void DoExp()
        {
            foreach (DataItem item in this._dataItems)
            {
                item.DoExp();
            }
            this.fillAndProcessVarMinMax();
        }

        public void DoLog()
        {
            foreach (DataItem item in this._dataItems)
            {
                item.DoLog();
            }
            this.fillAndProcessVarMinMax();
        }

        private void fillAndProcessVarMinMax()
        {
            for (int i = 0; i < this._nDimensions; i++)
            {
                this._variablesMinimums[i] = this.calculateVarMin(i);
                this._variablesMaximums[i] = this.calculateVarMax(i);
            }
            for (int j = 0; j < this._variablesMaximums.Length; j++)
            {
                this._variablesMaximums[j] = Utils.roundUpTwoLeftmostDigits(this._variablesMaximums[j]);
            }
            for (int k = 0; k < this._variablesMinimums.Length; k++)
            {
                this._variablesMinimums[k] = Utils.roundDownOneLeftmostDigit(this._variablesMinimums[k]);
                if ((this._variablesMinimums[k] > 0.0) && (this._variablesMinimums[k] < 10.0))
                {
                    this._variablesMinimums[k] = Math.Floor(this._variablesMinimums[k]);
                }
            }
            for (int m = 0; m < this._variablesMaximums.Length; m++)
            {
                if (this._variablesMaximums[m].Equals(this._variablesMinimums[m]))
                {
                    this._variablesMaximums[m] = this._variablesMinimums[m] + 1.0;
                }
            }
        }

        public void fillDetailsList(ListView list, int itemIndex, bool forceRefill)
        {
            if (((itemIndex != this._prevSelectedItemIdx) || forceRefill) && (itemIndex >= 0))
            {
                int i;
                this._prevSelectedItemIdx = itemIndex;
                list.SuspendLayout();
                list.Items.Clear();
                list.Columns.Clear();
                int width = 80;
                list.Columns.Add(this.TimePtLabelForDL, width, HorizontalAlignment.Right);
                int c = 0;
                foreach (DataVariable variable in this._dataItems[itemIndex].getVariables())
                {
                    if (c == 0)
                    {
                        width = 80;
                    }
                    list.Columns.Add(variable.getName(), width, HorizontalAlignment.Right);
                }
                ListViewItem[] items = new ListViewItem[this.NumTimePoints];
                for (i = 0; i < this.NumTimePoints; i++)
                {
                    ListViewItem item = new ListViewItem(this._timePointNames[i]);
                    string[] strArray = new string[this.NumDynVar];
                    for (int j = 0; j < this.NumDynVar; j++)
                    {
                        strArray[j] = this._dataItems[itemIndex].GetVar(j).getStringValue(i);
                    }
                    item.SubItems.AddRange(strArray);
                    items[i] = item;
                }
                list.Items.AddRange(items);
                for (i = 0; i < list.Columns.Count; i++)
                {
                    if (list.Columns[i].Text.Contains("Date"))
                    {
                        list.Columns[i].Text = "时间(" + list.Items.Count + ")";
                    }
                    else if (list.Columns[i].Text.Contains("lost"))
                    {
                        list.Columns[i].Text = "线损";
                    }
                    else if (list.Columns[i].Text.Contains("arate"))
                    {
                        list.Columns[i].Text = "线损率";
                    }
                    else if (list.Columns[i].Text.Contains("used"))
                    {
                        list.Columns[i].Text = "用电量";
                    }
                }
                list.ResumeLayout();
            }
        }

        public void fillDetailsLitByName(ListView list, string user_id_str, bool forceRefill)
        {
            int i_index = 0;
            if (!user_id_str.Equals(""))
            {
                for (int i = 0; i < this._dataItems.Length; i++)
                {
                    if (this._dataItems[i].Name == user_id_str)
                    {
                        i_index = i;
                        break;
                    }
                }
            }
            this.fillDetailsList(list, i_index, forceRefill);
        }

        public bool get_g_userid(string user_id)
        {
            bool b = false;
            DataRow[] drs = this.dt.Select("user_id='" + user_id + "'");
            string bd = "False";
            if (drs.Length > 0)
            {
                bd = drs[0]["is_check"].ToString();
            }
            if (bd == "True")
            {
                b = true;
            }
            return b;
        }

        public string[] get_timePointNames()
        {
            return this._timePointNames;
        }

        public void GetData()
        {
            try
            {
                DateTime d11 = this._tsform.get_curr_begin_time();
                DateTime d12 = this._tsform.get_curr_end_time();
                string[] date_Array = DBHelper.DBHelper.GetScalar("select max(date_time)||'@'||min(date_time)  from static_lost where line_no='" + this._tsform.get_line_lost() + "' ").ToString().Split(new char[] { '@' });
                this._tsform.set_max_date_time(DateTime.Parse(date_Array[0]));
                this._tsform.set_min_date_time(DateTime.Parse(date_Array[1]));
                if (this._tsform.get_curr_end_time().CompareTo(DateTime.Parse(date_Array[0])) >= 0)
                {
                    this._tsform.set_curr_end_time(DateTime.Parse(date_Array[0]));
                }
                if (this._tsform.get_curr_begin_time().CompareTo(DateTime.Parse(date_Array[1])) <= 0)
                {
                    this._tsform.set_curr_begin_time(DateTime.Parse(date_Array[1]));
                }
                if (this._tsform.get_curr_begin_time().CompareTo(DateTime.Parse(date_Array[0])) >= 0)
                {
                    this._tsform.set_curr_end_time(DateTime.Parse(date_Array[0]));
                    this._tsform.set_curr_begin_time(DateTime.Parse(date_Array[1]));
                }
                if (d11.ToShortDateString() == d12.ToShortDateString())
                { 
                    this._tsform.set_curr_end_time(DateTime.Parse(date_Array[0]));
                    this._tsform.set_curr_begin_time(DateTime.Parse(date_Array[1]));
                }
                this._strTitle = "";
                string nextLine = "Date,String";
                string str = "lost,Float;arate,Float;used,Float";
                this._nDimensions = this.ParseDynamicVariables(str);
                this._nDimensions = 3;
                this.initializeMinMax(this._nDimensions);
                this._numTimePoints = int.Parse(DBHelper.DBHelper.GetScalar("select count(distinct t.date_time) from static_lost t  where t.date_time>='" + this._tsform.get_curr_begin_time().ToString("yyyy-MM-dd") + "' and t.date_time<='" + this._tsform.get_curr_end_time().ToString("yyyy-MM-dd") + "' and t.line_no='" + this._tsform.get_line_lost() + "' ").ToString());
                if (this._numTimePoints < 2)
                {
                    throw new FileFormatException("There must be at least 2 time points.");
                }
                this._numItems = Convert.ToInt16(DBHelper.DBHelper.GetScalar("select count(distinct t.user_id) from static_lost t  where t.date_time>='" + this._tsform.get_curr_begin_time().ToString("yyyy-MM-dd") + "' and t.date_time<='" + this._tsform.get_curr_end_time().ToString("yyyy-MM-dd") + "' and t.line_no='" + this._tsform.get_line_lost() + "'   "));
                SharedData.missingValue = -1.0;
                DataRowCollection obj = DBHelper.DBHelper.GetDataSet("select distinct t.date_time from static_lost t where t.date_time>='" + this._tsform.get_curr_begin_time().ToString("yyyy-MM-dd") + "' and t.date_time<='" + this._tsform.get_curr_end_time().ToString("yyyy-MM-dd") + "' and t.line_no='" + this._tsform.get_line_lost() + "'  order by date_time asc").Tables[0].Rows;
                this._timePointNames = new string[obj.Count];
                for (int i = 0; i < obj.Count; i++)
                {
                    this._timePointNames[i] = obj[i][0].ToString();
                }
                if (this._numTimePoints != this._timePointNames.Length)
                {
                    Console.WriteLine("DataSet.ReadHeader(): ASSERTION FAILED with numTimePoints!");
                }
                this._staticVariable = new StaticVariable(nextLine, this._timePointNames);
            }
            catch (FileFormatException exception)
            {
                Console.WriteLine("DataSet: CAUGHT FileFormatException " + exception);
                throw new FileFormatException("Error reading header: " + exception);
            }
            catch (Exception exception2)
            {
                Console.WriteLine("DataSet: CAUGHT Exception " + exception2);
                Console.WriteLine(exception2.ToString());
            }
        }

        public string getDynVarName(int index)
        {
            return this._dynamicVariableNames[index];
        }

        public string[] getDynVarNames(bool logTaken)
        {
            if (!logTaken)
            {
                return this._dynamicVariableNames;
            }
            string[] strArray = new string[this._dynamicVariableNames.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                strArray[i] = "Log(1+" + this._dynamicVariableNames[i] + ")";
            }
            return strArray;
        }

        public int[] GetEnabledItemIdx()
        {
            return (int[]) this.GetEnabledItemIdxList().ToArray(typeof(int));
        }

        public ArrayList GetEnabledItemIdxList()
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < this._dataItems.Length; i++)
            {
                if (this._dataItems[i].IsEnabled())
                {
                    list.Add(i);
                }
            }
            return list;
        }

        public int[] GetEnabledOnlyItemIdx()
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < this._dataItems.Length; i++)
            {
                if (this._dataItems[i].IsEnabledOnly())
                {
                    list.Add(i);
                }
            }
            return (int[]) list.ToArray(typeof(int));
        }

        public DataItem GetItem(int i)
        {
            return this._dataItems[i];
        }

        private int getItemIndex(string itemName)
        {
            for (int i = 0; i < this.NumItems; i++)
            {
                if (this._dataItems[i].Name == itemName)
                {
                    return i;
                }
            }
            return -1;
        }

        private string GetNextLine(StreamReader myReader)
        {
            while (myReader.Peek() > -1)
            {
                string str = myReader.ReadLine();
                TimeSearcherForm.Debug("Reading---" + str);
                if (!(str.StartsWith("#") && !str.StartsWith("##")))
                {
                    return str;
                }
            }
            throw new FileFormatException();
        }

        public int[] GetSelectedAndEnabledItemIdx()
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < this._dataItems.Length; i++)
            {
                if (this._dataItems[i].IsSelectedAndEnabled())
                {
                    list.Add(i);
                }
            }
            return (int[]) list.ToArray(typeof(int));
        }

        public DataVariable getSelectedDataVar(int varIndex)
        {
            foreach (DataItem item in this._dataItems)
            {
                if (item.Name == "lost")
                {
                    return item.GetVar(varIndex);
                }
            }
            return null;
        }

        public TimeSearcher.DataSet GetStatDataSet()
        {
            TimeSearcher.DataSet set = new TimeSearcher.DataSet();
            set._numItems = 5;
            set.dataType = this.dataType;
            set._dynamicVariableNames = this._dynamicVariableNames;
            set._nDimensions = set._dynamicVariableNames.Length;
            set.initializeMinMax(set._nDimensions);
            set._numTimePoints = this._numTimePoints;
            set._itemTabName = this._itemTabName;
            set._timePointNames = this._timePointNames;
            set._staticVariable = this._staticVariable;
            StatType[] statTypes = new StatType[] { StatType.MIN, StatType.Q1, StatType.MED, StatType.Q3, StatType.MAX };
            Configuration.riverGraphMedianIndex = 2;
            int[] enabledItemIdx = this.GetEnabledItemIdx();
            double[][][] numArray3 = new double[5][][];
            for (int i = 0; i < 5; i++)
            {
                numArray3[i] = new double[this.NumDynVar][];
                for (int m = 0; m < this.NumDynVar; m++)
                {
                    numArray3[i][m] = new double[this.NumTimePoints];
                }
            }
            for (int j = 0; j < this.NumTimePoints; j++)
            {
                for (int n = 0; n < this.NumDynVar; n++)
                {
                    double[] dblValues = new double[enabledItemIdx.Length];
                    for (int num5 = 0; num5 < enabledItemIdx.Length; num5++)
                    {
                        dblValues[num5] = this._dataItems[enabledItemIdx[num5]].GetVar(n).getValue(j);
                    }
                    double[] doubleStats = new EfficientStatComputer(dblValues, statTypes).GetDoubleStats(statTypes);
                    for (int num6 = 0; num6 < 5; num6++)
                    {
                        numArray3[num6][n][j] = doubleStats[num6];
                    }
                }
            }
            set._dataItems = new DataItem[5];
            for (int k = 0; k < 5; k++)
            {
                set._dataItems[k] = new DataItem(statTypes[k].Name, k, numArray3[k], this._dynamicVariableNames);
            }
            set.ImportVarMinMax(this);
            set._prevSelectedItemIdx = -1;
            return set;
        }

        public string getTimePointName(int index)
        {
            return this._timePointNames[index];
        }

        public double getVarMax(int varIdx)
        {
            return this._variablesMaximums[varIdx];
        }

        public double getVarMin(int varIdx)
        {
            return this._variablesMinimums[varIdx];
        }

        public double getVarRange(int varIdx)
        {
            return (this._variablesMaximums[varIdx] - this._variablesMinimums[varIdx]);
        }

        public void ImportVarMinMax(TimeSearcher.DataSet dataSet)
        {
            this._variablesMinimums = new double[dataSet._variablesMinimums.Length];
            this._variablesMaximums = new double[dataSet._variablesMaximums.Length];
            Array.Copy(dataSet._variablesMinimums, 0, this._variablesMinimums, 0, this._variablesMinimums.Length);
            Array.Copy(dataSet._variablesMaximums, 0, this._variablesMaximums, 0, this._variablesMaximums.Length);
        }

        private void initializeMinMax(int size)
        {
            this._variablesMinimums = new double[size];
            this._variablesMaximums = new double[size];
            for (int i = 0; i < size; i++)
            {
                this._variablesMinimums[i] = double.MaxValue;
                this._variablesMaximums[i] = double.MinValue;
            }
        }

        private int ParseDynamicVariables(string str)
        {
            string[] strArray = str.Split(new char[] { ';' });
            this.dataType = new DataType[strArray.Length];
            this._dynamicVariableNames = new string[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { ',' });
                this._dynamicVariableNames[i] = strArray2[0];
                if (strArray2[1].Equals("Binary"))
                {
                    this.dataType[i] = DataType.BINARY;
                }
                else
                {
                    this.dataType[i] = DataType.FLOAT;
                }
            }
            return this._dynamicVariableNames.Length;
        }

        public AttrType[] ReadAttributes(string path)
        {
            StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open));
            int num = 0;
            string[] attrNames = null;
            AttrType[] attrTypes = null;
            try
            {
                num++;
                attrNames = Utils.SplitCsvLine(reader.ReadLine());
                num++;
                attrTypes = AttrType.ParseTypeArray(Utils.SplitCsvLine(reader.ReadLine()));
                while (reader.Peek() > -1)
                {
                    num++;
                    string[] strValues = Utils.SplitCsvLine(reader.ReadLine());
                    AttrValue[] attributes = AttrType.ParseAttrValues(attrTypes, strValues);
                    this.setItemAttribute(strValues[0], attributes);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Concat(new object[] { "Error on line ", num, "\n", exception.Message }));
            }
            finally
            {
                reader.Close();
            }
            this._lastAttrPath = path;
            this._attrOracle = new AttributeOracle(this, attrNames, attrTypes);
            return attrTypes;
        }

        public bool ReadFile()
        {
            if (this._tsform.re_read)
            {
                Cursor.Current = Cursors.WaitCursor;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                new Regex(",", RegexOptions.IgnoreCase);
                this.reset();
                DataTable dt = new DataTable();
                this.GetData();
                DateTime d11 = this._tsform.get_curr_begin_time();
                DateTime d12 = this._tsform.get_curr_end_time();
                DataRowCollection obj = DBHelper.DBHelper.GetDataSet("select distinct t.user_id from static_lost t  where t.date_time>='" + this._tsform.get_curr_begin_time().ToString("yyyy-MM-dd") + "' and t.date_time<='" + this._tsform.get_curr_end_time().ToString("yyyy-MM-dd") + "' and t.line_no='" + this._tsform.get_line_lost() + "' order by user_id desc").Tables[0].Rows;
                dt = DBHelper.DBHelper.GetDataSet("select * from static_lost t where t.date_time>='" + this._tsform.get_curr_begin_time().ToString("yyyy-MM-dd") + "' and t.date_time<='" + this._tsform.get_curr_end_time().ToString("yyyy-MM-dd") + "' and t.line_no='" + this._tsform.get_line_lost() + "'  order by  date_time asc ,user_id").Tables[0];
                this._dataItems = new DataItem[this._numItems];
                for (int i = 0; i < obj.Count; i++)
                {
                    double[][] numArray = new double[][] { new double[this._timePointNames.Length], new double[this._timePointNames.Length], new double[this._timePointNames.Length] };
                    for (int j = 0; j < this._timePointNames.Length; j++)
                    {
                        string userid = obj[i][0].ToString();
                        string datetime = this._timePointNames[j];
                        DataRow[] dr = dt.Select("user_id='" + obj[i][0].ToString() + "' and date_time='" + this._timePointNames[j] + "'");
                        if (dr.Length > 0)
                        {
                            if (dr[0][1].ToString().Trim() == "NaN")
                            {
                                numArray[0][j] = 0.0;
                            }
                            else
                            {
                                try
                                {
                                    numArray[0][j] = double.Parse(dr[0][1].ToString());
                                }
                                catch
                                {
                                    numArray[0][j] = 0;
                                }
                               
                            }
                            if (dr[0][3].ToString().Trim() == "NaN")
                            {
                                numArray[1][j] = 0.0;
                            }
                            else
                            {
                                try
                                {
                                    numArray[1][j] = double.Parse(dr[0][3].ToString());
                                }
                                catch
                                {
                                    numArray[1][j] = 0;
                                }
                              
                            }
                            if (dr[0][2].ToString().Trim() == "NaN")
                            {
                                numArray[2][j] = 0.0;
                            }
                            else
                            {
                                try
                                {
                                    numArray[2][j] = double.Parse(dr[0][2].ToString());
                                }
                                catch 
                                {
                                    numArray[2][j] = 0;
                                }
                            }
                        }
                    }
                    this._dataItems[i] = new DataItem(obj[i][0].ToString(), i, numArray, this._dynamicVariableNames);
                }
                this.fillAndProcessVarMinMax();
                this._prevSelectedItemIdx = -1;
            }
            else
            {
                this.fillAndProcessVarMinMax();
                this._prevSelectedItemIdx = -1;
            }
            return true;
        }

        public bool ReadFile(Stream fileStream)
        {
            Cursor.Current = Cursors.WaitCursor;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            new Regex(",", RegexOptions.IgnoreCase);
            this.reset();
            DataRowCollection obj = DBHelper.DBHelper.GetDataSet("select distinct t.user_id from static_lost t order by user_id asc").Tables[0].Rows;
            DataTable dt = new DataTable();
            this.GetData();
            dt = DBHelper.DBHelper.GetDataSet("select * from static_lost  order by  date_time asc ,user_id").Tables[0];
            this._dataItems = new DataItem[this._numItems];
            for (int i = 0; i < obj.Count; i++)
            {
                double[][] numArray = new double[][] { new double[this._timePointNames.Length], new double[this._timePointNames.Length] };
                for (int j = 0; j < this._timePointNames.Length; j++)
                {
                    string userid = obj[i][0].ToString();
                    string datetime = this._timePointNames[j];
                    DataRow[] dr = dt.Select("user_id='" + obj[i][0].ToString() + "' and date_time='" + this._timePointNames[j] + "'");
                    if (dr.Length > 0)
                    {
                        double d1 = double.Parse(dr[0][1].ToString());
                        double d2 = double.Parse(dr[0][2].ToString());
                        numArray[0][j] = double.Parse(dr[0][1].ToString());
                        numArray[1][j] = double.Parse(dr[0][2].ToString());
                    }
                }
                this._dataItems[i] = new DataItem(obj[i][0].ToString(), i, numArray, this._dynamicVariableNames);
            }
            this.fillAndProcessVarMinMax();
            this._prevSelectedItemIdx = -1;
            return true;
        }

        private bool ReadHeader(StreamReader myReader)
        {
            try
            {
                this._strTitle = this.GetNextLine(myReader);
                string nextLine = this.GetNextLine(myReader);
                string str = this.GetNextLine(myReader);
                this._nDimensions = this.ParseDynamicVariables(str);
                this.initializeMinMax(this._nDimensions);
                this._numTimePoints = Convert.ToInt16(this.GetNextLine(myReader));
                if (this._numTimePoints < 2)
                {
                    throw new FileFormatException("There must be at least 2 time points.");
                }
                this._numItems = Convert.ToInt16(this.GetNextLine(myReader));
                str = this.GetNextLine(myReader);
                if (str.StartsWith("##tabName"))
                {
                    this._itemTabName = str.Substring(str.IndexOf(' '));
                    str = this.GetNextLine(myReader);
                }
                if (str.StartsWith("##missingValue"))
                {
                    SharedData.missingValue = double.Parse(str.Substring(str.IndexOf(' ')));
                    str = this.GetNextLine(myReader);
                }
                else
                {
                    SharedData.missingValue = -1.0;
                }
                if (str.EndsWith(","))
                {
                    Console.WriteLine("DataSet.ReadHeader(): Please remove the COMMA at the end of the time point labels!");
                    str = str.TrimEnd(new char[] { ',' });
                }
                this._timePointNames = str.Split(new char[] { ',' });
                if (this._numTimePoints != this._timePointNames.Length)
                {
                    Console.WriteLine("DataSet.ReadHeader(): ASSERTION FAILED with numTimePoints!");
                }
                this._staticVariable = new StaticVariable(nextLine, this._timePointNames);
            }
            catch (FileFormatException exception)
            {
                Console.WriteLine("DataSet: CAUGHT FileFormatException " + exception);
                throw new FileFormatException("Error reading header: " + exception);
            }
            catch (Exception exception2)
            {
                Console.WriteLine("DataSet: CAUGHT Exception " + exception2);
                Console.WriteLine(exception2.ToString());
            }
            return true;
        }

        private void reset()
        {
            this._timeBoxManager.reset();
            this.currTimePtIndex = -1;
            this._attrOracle = new AttributeOracle(this, "Name", AttrType.STRING);
            this._itemTabName = "item";
        }

        private void setItemAttribute(string itemName, AttrValue[] attributes)
        {
            int index = this.getItemIndex(itemName);
            if (index >= 0)
            {
                this._dataItems[index].Attributes = attributes;
            }
        }

        public void UndoExp()
        {
            foreach (DataItem item in this._dataItems)
            {
                item.UndoExp();
            }
            this.fillAndProcessVarMinMax();
        }

        public void UndoLog()
        {
            foreach (DataItem item in this._dataItems)
            {
                item.UndoLog();
            }
            this.fillAndProcessVarMinMax();
        }

        public void WriteSelectedItemsAsTqd(Stream stream)
        {
            int[] selectedAndEnabledItemIdx = this.GetSelectedAndEnabledItemIdx();
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("#title");
            writer.WriteLine("temporary file for selected items");
            writer.WriteLine("#static attributes");
            writer.WriteLine(this._staticVariable.Name + "," + this._staticVariable.TypeStr);
            writer.WriteLine("#Dynamic attributes");
            int index = 0;
            while (index < (this._dynamicVariableNames.Length - 1))
            {
                writer.Write(this._dynamicVariableNames[index] + ",Float;");
                index++;
            }
            writer.WriteLine(this._dynamicVariableNames[index] + ",Float");
            writer.WriteLine("#number of time points\n" + this.NumTimePoints);
            writer.WriteLine("#number of items\n" + selectedAndEnabledItemIdx.Length);
            writer.WriteLine("##tabName " + this._itemTabName);
            for (index = 0; index < this._timePointNames.Length; index++)
            {
                writer.Write(this._timePointNames[index] + ",");
            }
            writer.WriteLine();
            for (index = 0; index < selectedAndEnabledItemIdx.Length; index++)
            {
                DataItem item = this._dataItems[selectedAndEnabledItemIdx[index]];
                writer.Write(item.Name);
                for (int i = 0; i < this.NumTimePoints; i++)
                {
                    for (int j = 0; j < this.NumDynVar; j++)
                    {
                        writer.Write("," + Convert.ToString(item.GetVar(j).getValue(i)));
                    }
                }
                writer.WriteLine();
            }
            writer.Close();
        }

        public AttributeOracle AttrOracle
        {
            get
            {
                return this._attrOracle;
            }
        }

        public DataItem[] DataItems
        {
            get
            {
                return this._dataItems;
            }
        }

        public TimeSearcher.DisablingManager DisablingManager
        {
            get
            {
                return this._disablingManager;
            }
        }

        public string FilenamePath
        {
            get
            {
                return this._filenamePath;
            }
            set
            {
                this._filenamePath = value;
            }
        }

        public bool HighlightedItemExists
        {
            get
            {
                return (this._highlightedItemIdx != -1);
            }
        }

        public int HighlightedItemIdx
        {
            get
            {
                return this._highlightedItemIdx;
            }
            set
            {
                if (this.HighlightedItemExists)
                {
                    this._dataItems[this._highlightedItemIdx].unHighlight();
                    this._highlightedItemIdx = -1;
                }
                if (value != -1)
                {
                    this._highlightedItemIdx = value;
                    this._dataItems[this._highlightedItemIdx].highlight();
                }
            }
        }

        public string ItemTabName
        {
            get
            {
                return this._itemTabName;
            }
        }

        public string LastAttrPath
        {
            get
            {
                return this._lastAttrPath;
            }
        }

        public int NumDynVar
        {
            get
            {
                return this._nDimensions;
            }
        }

        public int NumItems
        {
            get
            {
                return this._numItems;
            }
        }

        public int NumTimePoints
        {
            get
            {
                return this._numTimePoints;
            }
        }

        public StaticVariable StaticVar
        {
            get
            {
                return this._staticVariable;
            }
        }

        public TimeSearcher.Widgets.TimeBoxManager TimeBoxManager
        {
            get
            {
                return this._timeBoxManager;
            }
        }

        public string[] TimePointNames
        {
            get
            {
                return this._timePointNames;
            }
        }

        private string TimePtLabelForDL
        {
            get
            {
                return string.Concat(new object[] { this._staticVariable.Name, " (", this.NumTimePoints, ")" });
            }
        }

        public string Title
        {
            get
            {
                return this._strTitle;
            }
        }

        public enum DataType
        {
            UNKNOWN,
            FLOAT,
            BINARY
        }
    }
}

