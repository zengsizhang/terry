using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBHelper
{
	public class MathAdd
	{
        //求随机数平均值方法
        public static double Ave(double[] a)
        {
            double sum = 0;
            foreach (double d in a)
            {

                sum = sum + d;
            }
            double ave = sum / a.Length;

            return ave;
        }
        public static double zero_rate(double[] a)
        {
            double sum = 0;
            foreach (double d in a)
            {

                if (d == 0)
                {
                    sum++;
                }
            }
            double ave = sum / a.Length;

            return ave;
        }
        //求随机数方差方法
        public static double Var(double[] v)
        {
            //    double tt = 2;
            //double mm = tt ^ 2;

            double sum1 = 0;
            for (int i = 0; i < v.Length; i++)
            {
                double temp = v[i] * v[i];
                sum1 = sum1 + temp;

            }

            double sum = 0;
            foreach (double d in v)
            {
                sum = sum + d;
            }

            double var = sum1 / v.Length - (sum / v.Length) * (sum / v.Length);
            return var;
        }

        //求正态分布的随机数
        public static void Fenbu(double[] f)
        {
            //double fenbu=new double[f.Length ];
            for (int i = 0; i < f.Length; i++)
            {
                double a = 0, b = 0;
                a = Math.Sqrt((-2) * Math.Log(f[i], Math.E));
                b = Math.Cos(2 * Math.PI * f[i]);
                f[i] = a * b * 0.3 + 1;

            }

        }

	}
}
