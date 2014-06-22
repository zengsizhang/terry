namespace TimeSearcher
{
    using System;
    using System.Drawing;
    using TimeSearcher.Domain;

    public class StaticVariable
    {
        private UniformDomain _domain;
        private readonly string _name;
        private Color[] _tempDensity;
        private readonly Type _type;

        public StaticVariable(string line, string[] strDomainValues)
        {
            string[] strArray = line.Split(new char[] { ',' });
            this._name = strArray[0];
            this._type = this.getTypeFromString(strArray[1]);
            if (this._type == Type.BipolarDateTime)
            {
                this._domain = new UniformBipolarDateTimeDomain(strDomainValues, this.str2TimeSpan(strArray[2]), this.str2TimeSpan(strArray[3]), this.str2TimeSpan(strArray[4]));
            }
            else
            {
                this._domain = new UniformLinearDomain(0, strDomainValues.Length - 1);
            }
        }

        private Color[] calcTempDensity(string[] strDomainValues)
        {
            DateTime[] timeArray = new DateTime[strDomainValues.Length];
            for (int i = 0; i < timeArray.Length; i++)
            {
                timeArray[i] = Convert.ToDateTime(strDomainValues[i]);
            }
            TimeSpan[] spanArray = new TimeSpan[timeArray.Length - 1];
            for (int j = 0; j < spanArray.Length; j++)
            {
                spanArray[j] = timeArray[j + 1].Subtract(timeArray[j]);
            }
            long[] numArray = new long[spanArray.Length];
            for (int k = 0; k < numArray.Length; k++)
            {
                numArray[k] = (long) spanArray[k].TotalSeconds;
            }
            long num4 = numArray[0];
            for (int m = 1; m < numArray.Length; m++)
            {
                if (numArray[m] < num4)
                {
                    num4 = numArray[m];
                }
            }
            for (int n = 0; n < numArray.Length; n++)
            {
                numArray[n] -= num4;
            }
            long num7 = numArray[0];
            for (int num8 = 1; num8 < numArray.Length; num8++)
            {
                if (numArray[num8] > num7)
                {
                    num7 = numArray[num8];
                }
            }
            double[] numArray2 = new double[numArray.Length];
            if (num7 == 0L)
            {
                for (int num9 = 0; num9 < numArray.Length; num9++)
                {
                    numArray2[num9] = 1.0;
                }
            }
            else
            {
                for (int num10 = 0; num10 < numArray.Length; num10++)
                {
                    numArray2[num10] = ((double) numArray[num10]) / ((double) num7);
                }
            }
            int[] numArray3 = new int[numArray2.Length];
            for (int num11 = 0; num11 < numArray2.Length; num11++)
            {
                numArray3[num11] = (int) (numArray2[num11] * 255.0);
            }
            Color[] colorArray = new Color[numArray3.Length];
            for (int num13 = 0; num13 < numArray3.Length; num13++)
            {
                int red = 0xff - numArray3[num13];
                colorArray[num13] = Color.FromArgb(red, red, red);
            }
            return colorArray;
        }

        private string getStringFromType(Type type)
        {
            switch (type)
            {
                case Type.String:
                    return "String";

                case Type.BipolarDateTime:
                    return "BipolarDateTime";
            }
            return "None";
        }

        private Type getTypeFromString(string typeName)
        {
            switch (typeName)
            {
                case "String":
                    return Type.String;

                case "BipolarDateTime":
                    return Type.BipolarDateTime;
            }
            return Type.None;
        }

        private TimeSpan str2TimeSpan(string str)
        {
            string[] strArray = str.Trim().Split(new char[] { ' ' });
            for (int i = 0; i < strArray.Length; i++)
            {
                strArray[i] = strArray[i].Trim();
            }
            if (strArray[1].Equals("min"))
            {
                return TimeSpan.FromMinutes(Convert.ToDouble(strArray[0]));
            }
            if (strArray[1].Equals("sec"))
            {
                return TimeSpan.FromSeconds(Convert.ToDouble(strArray[0]));
            }
            Console.WriteLine("SV.str2TimeSpan: couldn't parse! <" + str + ">");
            return TimeSpan.FromSeconds(0.0);
        }

        public UniformDomain Domain
        {
            get
            {
                return this._domain;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public Color[] TempDensity
        {
            get
            {
                return this._tempDensity;
            }
        }

        public string TypeStr
        {
            get
            {
                return this.getStringFromType(this.TypeVal);
            }
        }

        public Type TypeVal
        {
            get
            {
                return this._type;
            }
        }

        public enum Type
        {
            String,
            BipolarDateTime,
            None
        }
    }
}

