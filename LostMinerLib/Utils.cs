namespace TimeSearcher
{
    using LostMinerLib.Util;
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class Utils
    {
        public static bool CanOpenFile(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            FileStream stream = null;
            try
            {
                stream = File.OpenRead(path);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Utils.CanOpenFile(): " + exception);
                if (stream != null)
                {
                    stream.Close();
                }
                return false;
            }
            stream.Close();
            return true;
        }

        public static double DoExp(double val)
        {
            return Math.Exp(val);
        }

        public static double DoLog(double val)
        {
            return Math.Log(1.0 + val);
        }

        public static bool EqualArrays(int[] array1, int[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static int GetCeilingIntWidthForLvCol(SizeF sizeF)
        {
            return (int) Math.Ceiling((double) (sizeF.Width + 9f));
        }

        public static int GetCeilingIntWidthForLvColHdr(SizeF sizeF)
        {
            return (int) Math.Ceiling((double) (sizeF.Width + 5f));
        }

        public static string GetFileNameOnly(string path)
        {
            string fullName = Directory.GetParent(path).FullName;
            return path.Substring(fullName.Length + 1);
        }

        public static double getMax(double[] dblValues)
        {
            double minValue = double.MinValue;
            for (int i = 0; i < dblValues.Length; i++)
            {
                if (dblValues[i] > minValue)
                {
                    minValue = dblValues[i];
                }
            }
            return minValue;
        }

        public static double getMaxExcept(double[] dblValues, double excludedValue)
        {
            double minValue = double.MinValue;
            for (int i = 0; i < dblValues.Length; i++)
            {
                if ((dblValues[i] != excludedValue) && (dblValues[i] > minValue))
                {
                    minValue = dblValues[i];
                }
            }
            return minValue;
        }

        public static double getMin(double[] dblValues)
        {
            double maxValue = double.MaxValue;
            for (int i = 0; i < dblValues.Length; i++)
            {
                if (dblValues[i] < maxValue)
                {
                    maxValue = dblValues[i];
                }
            }
            return maxValue;
        }

        public static double getMinExcept(double[] dblValues, double excludedValue)
        {
            double maxValue = double.MaxValue;
            for (int i = 0; i < dblValues.Length; i++)
            {
                if ((dblValues[i] != excludedValue) && (dblValues[i] < maxValue))
                {
                    maxValue = dblValues[i];
                }
            }
            return maxValue;
        }

        public static string GetMRUPath()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\OpenSaveMRU\*");
                string name = key.GetValue("MRUList").ToString()[0].ToString();
                return Path.GetDirectoryName(key.GetValue(name).ToString());
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static bool inBox(Point pt1, Point pt2, Point ptMouse)
        {
            return ((((Math.Min(pt1.Y, pt2.Y) <= ptMouse.Y) && (ptMouse.Y <= Math.Max(pt1.Y, pt2.Y))) && (Math.Min(pt1.X, pt2.X) <= ptMouse.X)) && (ptMouse.X <= Math.Max(pt1.X, pt2.X)));
        }

        public static string intArrayListToString(ArrayList intList)
        {
            int[] numArray = (int[]) intList.ToArray(typeof(int));
            string str = "";
            for (int i = 0; i < numArray.Length; i++)
            {
                str = str + numArray[i] + " ";
            }
            return str;
        }

        public static Point mouseCoorDiff(MouseEventArgs mea, Point oldMouseMovePosition)
        {
            return new Point(mea.X - oldMouseMovePosition.X, mea.Y - oldMouseMovePosition.Y);
        }

        public static int NearestSegIndex(Point point, Point[] pt1s, Point[] pt2s, out double nearestDist)
        {
            int num = 0;
            double maxValue = double.MaxValue;
            for (int i = 0; i < pt1s.Length; i++)
            {
                double num2 = SqDistToLineSeg(point, pt1s[i], pt2s[i]);
                if (num2 < maxValue)
                {
                    maxValue = num2;
                    num = i;
                }
            }
            nearestDist = maxValue;
            return num;
        }

        public static bool pointFitLine(Point pt1, Point pt2, Point ptMouse, double tolerance)
        {
            bool flag = false;
            if (inBox(pt1, pt2, ptMouse))
            {
                double num = pt2.Y - pt1.Y;
                double num2 = pt2.X - pt1.X;
                if (Math.Abs(num) < tolerance)
                {
                    return ((Math.Min(pt2.X, pt1.X) <= ptMouse.X) && (ptMouse.X <= Math.Max(pt2.X, pt1.X)));
                }
                double num3 = num / num2;
                double num4 = pt2.Y - (num3 * pt2.X);
                double num5 = Math.Abs((double) ((ptMouse.Y - (num3 * pt1.X)) - num4));
                double num6 = Math.Abs((double) (ptMouse.X - ((ptMouse.Y - num4) / num3)));
                if (Math.Min(num5, num6) <= tolerance)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static double pointLineDistance(Point pt1, Point pt2, Point pt3)
        {
            double num = Math.Pow((double) (pt1.X - pt3.X), 2.0) + Math.Pow((double) (pt1.Y - pt3.Y), 2.0);
            double num2 = Math.Pow((double) (pt2.X - pt3.X), 2.0) + Math.Pow((double) (pt2.Y - pt3.Y), 2.0);
            return Math.Min(num, num2);
        }

        public static void removeDuplicatesInSorted(ArrayList sortedList)
        {
            for (int i = 0; i < (sortedList.Count - 1); i++)
            {
                if (sortedList[i].Equals(sortedList[i + 1]))
                {
                    sortedList.RemoveAt(i);
                }
            }
        }

        public static string[] removeDuplicatesInSorted(string[] sortedArray)
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < (sortedArray.Length - 1); i++)
            {
                if (!sortedArray[i].Equals(sortedArray[i + 1]))
                {
                    list.Add(sortedArray[i]);
                }
            }
            list.Add(sortedArray[sortedArray.Length - 1]);
            return (string[]) list.ToArray(typeof(string));
        }

        public static double roundDownOneLeftmostDigit(double val)
        {
            int num2 = 1;
            while (((int) Math.Abs((double) (val / ((double) num2)))) >= 10)
            {
                num2 *= 10;
            }
            return (Math.Floor((double) (val / ((double) num2))) * num2);
        }

        public static double roundUpTwoLeftmostDigits(double val)
        {
            if (val < 100.0)
            {
                return val;
            }
            int num = 1;
            while (true)
            {
                if (((int) Math.Abs((double) (val / ((double) num)))) < 10)
                {
                    double num2 = Math.Ceiling((double) (val / ((double) num))) * num;
                    num /= 10;
                    while (num2 > val)
                    {
                        num2 -= num;
                    }
                    return (num2 + num);
                }
                num *= 10;
            }
        }

        public static DialogResult SafeShowDialog(OpenFileDialog fileDlg)
        {
            try
            {
                string dd1 = Application.StartupPath;
                INIClass iniclass = new INIClass(Application.StartupPath + @"\system.ini");
                if (iniclass.ExistINIFile())
                {
                    fileDlg.InitialDirectory = iniclass.IniReadValue("file", "filepath");
                }
            }
            catch (Exception)
            {
                fileDlg.InitialDirectory = @"c:\";
            }
            return fileDlg.ShowDialog();
        }

        public static string[] SplitCsvLine(string line)
        {
            return new string[5];
        }

        public static double SqDistToLineSeg(Point point, Point pt1, Point pt2)
        {
            double num = ((point.X - pt1.X) * (pt2.X - pt1.X)) + ((point.Y - pt1.Y) * (pt2.Y - pt1.Y));
            num /= Math.Pow((double) (pt2.Y - pt1.Y), 2.0) + Math.Pow((double) (pt2.X - pt1.X), 2.0);
            double num2 = pt1.X + (num * (pt2.X - pt1.X));
            double num3 = pt1.Y + (num * (pt2.Y - pt1.Y));
            return (Math.Pow(point.X - num2, 2.0) + Math.Pow(point.Y - num3, 2.0));
        }

        public static DateTime[] strArr2DateTime(string[] strValues)
        {
            DateTime[] timeArray = new DateTime[strValues.Length];
            for (int i = 0; i < strValues.Length; i++)
            {
                timeArray[i] = Convert.ToDateTime(strValues[i]);
            }
            return timeArray;
        }

        public static double UndoExp(double val)
        {
            return Math.Log(val);
        }

        public static double UndoLog(double val)
        {
            return (Math.Exp(val) - 1.0);
        }
    }
}

