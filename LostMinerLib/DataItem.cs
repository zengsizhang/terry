namespace TimeSearcher
{
    using System;
    using System.Collections;
    using System.Windows.Forms;
    using TimeSearcher.Attribute;
    using TimeSearcher.Filters;

    public class DataItem
    {
        private AttrValue[] _attributes;
        private readonly DataVariable[] _dataVariables;
        private Hashtable _disablingEntities;
        private readonly int _myIndex;
        private DataItemStatus _status;
        private readonly string _strName;

        public DataItem(string itemName, int itemIndex, double[][] dataVariables, string[] varNames)
        {
            this._strName = itemName;
            this._myIndex = itemIndex;
            this._attributes = new AttrValue[] { new AttrString(this._strName) };
            this._dataVariables = new DataVariable[varNames.Length];
            for (int i = 0; i < varNames.Length; i++)
            {
                this._dataVariables[i] = new DataVariable(this, varNames[i], dataVariables[i]);
            }
            this._status = DataItemStatus.NewDefaultStatus;
            this._disablingEntities = new Hashtable();
        }

        public void attachDisablingEntity(DisablingEntity dEntity)
        {
            if (!this._disablingEntities.Contains(dEntity))
            {
                this._disablingEntities.Add(dEntity, "");
                this.disable();
            }
        }

        public void detachDisablingEntity(DisablingEntity dEntity)
        {
            this._disablingEntities.Remove(dEntity);
            if (this._disablingEntities.Count == 0)
            {
                this.unDisable();
            }
        }

        public void disable()
        {
            this._status.Enabled = false;
        }

        public void DoExp()
        {
            this._dataVariables[0].DoExp();
        }

        public void DoLog()
        {
            foreach (DataVariable variable in this._dataVariables)
            {
                variable.DoLog();
            }
        }

        private void enable()
        {
            this._status.Enabled = true;
        }

        public AttrValue GetAttr(int attrIndex)
        {
            return this._attributes[attrIndex];
        }

        public static DataItem GetEmptyItem(int numTimePoints, int numVars)
        {
            double[][] dataVariables = new double[numVars][];
            for (int i = 0; i < dataVariables.Length; i++)
            {
                dataVariables[i] = new double[numTimePoints];
                for (int k = 0; k < numTimePoints; k++)
                {
                    dataVariables[i][k] = SharedData.missingValue;
                }
            }
            string[] varNames = new string[numVars];
            for (int j = 0; j < numVars; j++)
            {
                varNames[j] = "empty variable";
            }
            return new DataItem("empty item", -1, dataVariables, varNames);
        }

        public DataVariable GetVar(int i)
        {
            return this._dataVariables[i];
        }

        public DataVariable[] getVariables()
        {
            return this._dataVariables;
        }

        public void highlight()
        {
            this._status.Highlighted = true;
        }

        public bool IsEnabled()
        {
            return this._status.Enabled;
        }

        public bool IsEnabledOnly()
        {
            return ((this._status.Enabled && !this._status.Selected) && !this._status.Highlighted);
        }

        public bool IsHighlighted()
        {
            return this._status.Highlighted;
        }

        public bool IsSelectedAndEnabled()
        {
            return (this._status.Enabled && this._status.Selected);
        }

        public void selectEnabled()
        {
            this._status.Selected = true;
            this._status.Highlighted = false;
        }

        public static double[][] ToDoubleArrays(string[] fields, int numVar, int numTimePoints)
        {
            double[][] numArray = new double[numVar][];
            for (int i = 0; i < numVar; i++)
            {
                numArray[i] = new double[numTimePoints];
                int index = i;
                int num3 = 0;
                while (num3 < numTimePoints)
                {
                    try
                    {
                        numArray[i][num3] = double.Parse(fields[index]);
                    }
                    catch (FormatException exception)
                    {
                        Console.WriteLine("DI.ToDoubleArrays(): CAUGHT: " + exception);
                        FileFormatException exception2 = new FileFormatException(fields[index] + " is not a double. ");
                        throw exception2;
                    }
                    if (double.IsNaN(numArray[i][num3]))
                    {
                        numArray[i][num3] = SharedData.missingValue;
                    }
                    num3++;
                    index += numVar;
                }
            }
            return numArray;
        }

        private void unDisable()
        {
            this._status.Enabled = true;
        }

        public void UndoExp()
        {
            this._dataVariables[0].UndoExp();
        }

        public void UndoLog()
        {
            foreach (DataVariable variable in this._dataVariables)
            {
                variable.UndoLog();
            }
        }

        public void unHighlight()
        {
            this._status.Highlighted = false;
        }

        public void unSelectEnabled()
        {
            this._status.Selected = false;
        }

        public AttrValue[] Attributes
        {
            get
            {
                return this._attributes;
            }
            set
            {
                this._attributes = value;
            }
        }

        public DataVariable[] get_DataVariable
        {
            get
            {
                return this._dataVariables;
            }
        }

        public int ItemIdx
        {
            get
            {
                return this._myIndex;
            }
        }

        public System.Windows.Forms.ListViewItem ListViewItem
        {
            get
            {
                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(AttrValue.GetStrValues(this._attributes));
                item.Tag = this.ItemIdx;
                return item;
            }
        }

        public string Name
        {
            get
            {
                return this._strName;
            }
        }

        public DataItemStatus Status
        {
            get
            {
                return this._status;
            }
        }
    }
}

