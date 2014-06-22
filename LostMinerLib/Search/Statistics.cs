namespace TimeSearcher.Search
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using TimeSearcher;

    public class Statistics
    {
        public double maximum;
        private double mean;
        public double minimum;
        private int n;
        private double regA;
        private double regB;
        private double stDev;
        private double sumX;
        private double sumX2;
        private double sumXY;
        private double sumY;
        private double sumY2;
        private double[] values;

        public Statistics()
        {
        }

        public Statistics(double[] vals)
        {
            this.values = vals;
            this.n = this.values.Length;
            this.minimum = double.MaxValue;
            this.maximum = double.MinValue;
            for (int i = 0; i < this.n; i++)
            {
                double num = this.values[i];
                this.minimum = Math.Min(this.minimum, num);
                this.maximum = Math.Max(this.maximum, num);
                this.sumX2 += Math.Pow((double) i, 2.0);
                this.sumX += i;
                this.sumY2 += Math.Pow(num, 2.0);
                this.sumY += num;
                this.sumXY += i * num;
            }
            this.mean = this.sumY / ((double) this.n);
            this.stDev = Math.Sqrt((this.sumY2 / ((double) (this.n - 1))) - (Math.Pow(this.sumY, 2.0) / ((double) (this.n * (this.n - 1)))));
            this.regA = ((this.n * this.sumXY) - (this.sumX * this.sumY)) / ((this.n * this.sumX2) - Math.Pow(this.sumX, 2.0));
            this.regB = ((this.sumY * this.sumX2) - (this.sumX * this.sumXY)) / ((this.n * this.sumX2) - Math.Pow(this.sumX, 2.0));
        }

        public static ArrayList APCA2series(ArrayList APCAvalues)
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < (APCAvalues.Count - 1); i++)
            {
                double meanValue = ((Couple) APCAvalues[i]).meanValue;
                int num3 = 0;
                if (i != 0)
                {
                    num3 = ((Couple) APCAvalues[i - 1]).index + 1;
                }
                for (int j = num3; j < ((Couple) APCAvalues[i]).index; j++)
                {
                    list.Add(meanValue);
                }
                list.Add(((Couple) APCAvalues[i]).meanValue);
            }
            return list;
        }

        private void doStats()
        {
            double num5 = 0.0;
            string str = "";
            for (int i = 0; i < (this.n - 1); i++)
            {
                double num7 = this.values[i];
                double num = (this.regA * i) + this.regB;
                double num4 = num7 - num;
                double num2 = num4 - this.mean;
                double num3 = num2 / this.stDev;
                if (i == 0)
                {
                    num5 = num3;
                }
                else if ((i == (this.n - 1)) && (this.n > 1))
                {
                    num5 = (num3 - num5) / 2.0;
                }
                else
                {
                    num5 = ((num5 + this.values[i + 1]) - ((this.regA * (i + 1)) + this.regB)) / 2.0;
                }
                object obj2 = str;
                str = string.Concat(new object[] { obj2, i + 1, ";", num7, ";", num, ";", num2, ";", num3, ";", num4, ";", num5, "\n" });
            }
            Console.WriteLine(str);
        }

        public static double[] getAmplitudeSeries(double[] values)
        {
            double[] numArray = new double[values.Length];
            double num = getStandarDeviation(values);
            if (num == 0.0)
            {
                num = 1.0;
            }
            for (int i = 0; i < values.Length; i++)
            {
                numArray[i] = values[i] / num;
            }
            return numArray;
        }

        public static ArrayList getAPCA(double[] values)
        {
            double num = 0.2;
            ArrayList list = new ArrayList();
            double num2 = 0.0;
            Couple couple = new Couple();
            couple.index = 0;
            couple.meanValue = values[0];
            list.Add(couple);
            int num4 = 0;
            int index = 1;
            while (index < values.Length)
            {
                if (Math.Abs((double) (((Couple) list[list.Count - 1]).meanValue - values[index])) < num)
                {
                    num2 += values[index];
                    num4++;
                }
                else
                {
                    couple.index = index;
                    couple.meanValue = num2 / ((num4 == 0) ? 1.0 : ((double) num4));
                    list.Add(couple);
                    num2 = values[index];
                    num4 = 1;
                }
                index++;
            }
            if (num4 != 0)
            {
                couple.index = index;
                couple.meanValue = num2 / ((double) num4);
                list.Add(couple);
            }
            Console.WriteLine("the series has been reduced to {0} elements", list.Count);
            return list;
        }

        public static double[] getFullNormalized(double[] values, SearchOptionsView soView)
        {
            double[] array = new double[values.Length];
            values.CopyTo(array, 0);
            if (soView.hasLinearT())
            {
                array = getLinearTrendSeries(array);
            }
            if (soView.hasOffsetT())
            {
                array = getOffsetSeries(array);
            }
            if (soView.hasAmplitudeS())
            {
                array = getAmplitudeSeries(array);
            }
            if (soView.hasNoiseR())
            {
                array = getSmoothedSeries(array);
            }
            return array;
        }

        private static Formula getLinearTrendCoeff(double[] values)
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double x = 0.0;
            double num5 = 0.0;
            int length = values.Length;
            Formula formula = new Formula();
            for (int i = 0; i < length; i++)
            {
                double num8 = values[i];
                num2 += Math.Pow((double) i, 2.0);
                x += i;
                num += Math.Pow(num8, 2.0);
                num3 += num8;
                num5 += i * num8;
            }
            formula.a = ((length * num5) - (x * num3)) / ((length * num2) - Math.Pow(x, 2.0));
            formula.b = ((num3 * num2) - (x * num5)) / ((length * num2) - Math.Pow(x, 2.0));
            return formula;
        }

        public static double[] getLinearTrendSeries(double[] values)
        {
            double[] numArray = new double[values.Length];
            Formula formula = new Formula();
            formula = getLinearTrendCoeff(values);
            double a = formula.a;
            double b = formula.b;
            for (int i = 0; i < values.Length; i++)
            {
                double num4 = (a * i) + b;
                numArray[i] = values[i] - num4;
            }
            return numArray;
        }

        private static double getMean(double[] values)
        {
            double num = 0.0;
            for (int i = 0; i < values.Length; i++)
            {
                num += values[i];
            }
            return (num / ((double) values.Length));
        }

        public static double[] getOffsetSeries(double[] values)
        {
            double[] numArray = new double[values.Length];
            double num = getMean(values);
            for (int i = 0; i < values.Length; i++)
            {
                double num3 = values[i];
                numArray[i] = num3 - num;
            }
            return numArray;
        }

        public static double getRange(double[] values)
        {
            double maxValue = double.MaxValue;
            double minValue = double.MinValue;
            foreach (double num3 in values)
            {
                if (num3 > minValue)
                {
                    minValue = num3;
                }
                if (num3 < maxValue)
                {
                    maxValue = num3;
                }
            }
            return (minValue - maxValue);
        }

        public static double[] getSmoothedSeries(double[] values)
        {
            int length = values.Length;
            double[] destinationArray = new double[values.Length];
            Array.Copy(values, 0, destinationArray, 0, values.Length);
            for (int i = 1; i < (length - 1); i++)
            {
                destinationArray[i] = (values[i - 1] + values[i + 1]) / 2.0;
            }
            return destinationArray;
        }

        private static double getStandarDeviation(double[] values)
        {
            int length = values.Length;
            double num3 = 0.0;
            double x = 0.0;
            for (int i = 0; i < length; i++)
            {
                num3 += Math.Pow(values[i], 2.0);
                x += values[i];
            }
            return Math.Sqrt((num3 / ((double) (length - 1))) - (Math.Pow(x, 2.0) / ((double) (length * (length - 1)))));
        }

        public void printAll(double[] values)
        {
            double[] numArray;
            this.printSeries(numArray = getLinearTrendSeries(values));
            this.printSeries(numArray = getOffsetSeries(numArray));
            this.printSeries(numArray = getAmplitudeSeries(numArray));
            this.printSeries(numArray = getSmoothedSeries(numArray));
        }

        private void printSeries(double[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine(values[i]);
            }
            Console.WriteLine("-----EOS----");
        }

        private void printStats()
        {
            int length = this.values.Length;
            if (length <= 1)
            {
                this.stDev = 0.0;
                this.mean = 0.0;
            }
            else
            {
                double num2 = 0.0;
                double num3 = 0.0;
                double x = 0.0;
                double num5 = 0.0;
                double num6 = 0.0;
                for (int i = 0; i < length; i++)
                {
                    double num8 = this.values[i];
                    num3 += Math.Pow((double) i, 2.0);
                    num5 += i;
                    num2 += Math.Pow(num8, 2.0);
                    x += num8;
                    num6 += i * num8;
                }
                this.mean = x / ((double) length);
                this.stDev = Math.Sqrt((num2 / ((double) (length - 1))) - (Math.Pow(x, 2.0) / ((double) (length * (length - 1)))));
                double num9 = ((length * num6) - (num5 * x)) / ((length * num3) - Math.Pow(num5, 2.0));
                double num10 = ((x * num3) - (num5 * num6)) / ((length * num3) - Math.Pow(num5, 2.0));
                double num15 = 0.0;
                string str = "";
                for (int j = 0; j < (length - 1); j++)
                {
                    double num17 = this.values[j];
                    double num12 = num17 - this.mean;
                    double num13 = num12 / this.stDev;
                    double num11 = (num9 * j) + num10;
                    double num14 = num17 - num11;
                    if (j == 0)
                    {
                        num15 = num14;
                    }
                    else if ((j == (length - 1)) && (length > 1))
                    {
                        num15 = (num14 - num15) / 2.0;
                    }
                    else
                    {
                        num15 = ((num15 + this.values[j + 1]) - ((num9 * (j + 1)) + num10)) / 2.0;
                    }
                    object obj2 = str;
                    str = string.Concat(new object[] { obj2, j + 1, ";", num17, ";", num11, ";", num12, ";", num13, ";", num14, ";", num15, "\n" });
                }
                Console.WriteLine(str);
                TimeSearcherForm.Debug(str);
                TimeSearcherForm.Debug(string.Concat(new object[] { "Regression parameters for the function y=mx+b: m: ", num9, ", b: ", num10 }));
                TimeSearcherForm.Debug("Standard deviation: " + this.stDev);
                TimeSearcherForm.Debug("Mean: " + this.mean);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Couple
        {
            public int index;
            public double meanValue;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Formula
        {
            public double a;
            public double b;
        }
    }
}

