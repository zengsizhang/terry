namespace TimeSearcher
{
    using System;

    public class DataVariable
    {
        private double _maxValue;
        private double _minValue;
        private readonly DataItem _myDataItem;
        private readonly string _strName;
        private readonly double[] _values;

        public DataVariable(DataItem myItem, string name, double[] tempValues)
        {
            this._myDataItem = myItem;
            this._strName = name;
            this._values = tempValues;
            this._minValue = Utils.getMinExcept(tempValues, SharedData.missingValue);
            this._maxValue = Utils.getMaxExcept(tempValues, SharedData.missingValue);
        }

        public void DoExp()
        {
            for (int i = 0; i < this._values.Length; i++)
            {
                this._values[i] = Utils.DoExp(this._values[i]);
            }
            this._minValue = Utils.DoExp(this._minValue);
            this._maxValue = Utils.DoExp(this._maxValue);
        }

        public void DoLog()
        {
            for (int i = 0; i < this._values.Length; i++)
            {
                this._values[i] = Utils.DoLog(this._values[i]);
            }
            this._minValue = Utils.DoLog(this._minValue);
            this._maxValue = Utils.DoLog(this._maxValue);
        }

        public double[] get_values()
        {
            return this._values;
        }

        public double getMaxValue()
        {
            return this._maxValue;
        }

        public double getMinValue()
        {
            return this._minValue;
        }

        public string getName()
        {
            return this._strName;
        }

        public int getNumValues()
        {
            return this._values.Length;
        }

        public double getRange()
        {
            return (this._maxValue - this._minValue);
        }

        public string getStringValue(int i)
        {
            return string.Format("{0:f}", Math.Round(this._values[i], 2));
        }

        public double getValue(int index)
        {
            return this._values[index];
        }

        public double[] getValues()
        {
            return this._values;
        }

        public double[] getValues(int fromIndex, int toIndex)
        {
            double[] numArray = new double[toIndex - fromIndex];
            for (int i = fromIndex; i < toIndex; i++)
            {
                numArray[i - fromIndex] = this._values[i];
            }
            return numArray;
        }

        public void UndoExp()
        {
            for (int i = 0; i < this._values.Length; i++)
            {
                this._values[i] = Utils.UndoExp(this._values[i]);
            }
            this._minValue = Utils.UndoExp(this._minValue);
            this._maxValue = Utils.UndoExp(this._maxValue);
        }

        public void UndoLog()
        {
            for (int i = 0; i < this._values.Length; i++)
            {
                this._values[i] = Utils.UndoLog(this._values[i]);
            }
            this._minValue = Utils.UndoLog(this._minValue);
            this._maxValue = Utils.UndoLog(this._maxValue);
        }

        public string get_strName
        {
            get
            {
                return this._strName;
            }
        }

        public int ItemIdx
        {
            get
            {
                return this._myDataItem.ItemIdx;
            }
        }

        public DataItemStatus Status
        {
            get
            {
                return this._myDataItem.Status;
            }
        }
    }
}

