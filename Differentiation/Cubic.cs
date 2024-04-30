using LiveCharts.Defaults;
using StringMath;
using System;
using System.Collections.Generic;

namespace Differentiation
{
    internal class Cubic : IDifferentiationMethod
    {
        public List<ObservablePoint> Differentiate(double a, double b, double step, int degree, string function)
        {
            List<ObservablePoint> points = new List<ObservablePoint>();
            double x_0 = a;
            for (double x = a; x <= b; x += step)
            {
                try
                {
                    double q = (x - x_0) / step;
                    double d_y = RightDiff.FiniteDiff(x, step, 1, function);
                    double d2_y = RightDiff.FiniteDiff(x, step, 2, function);
                    double d3_y = RightDiff.FiniteDiff(x, step, 3, function);
                    double diff = 1 / step * (d_y + ((2 * q - 1) / 2 * d2_y) + 
                        (((3 * q * q) - (6 * q) + 2) / 6 * d3_y));
                    points.Add(new ObservablePoint(x, diff));
                }
                catch { break; }
            }

            return points;
        }
    }
}