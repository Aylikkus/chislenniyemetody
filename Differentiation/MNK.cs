using LiveCharts.Defaults;
using StringMath;
using System;
using System.Collections.Generic;

namespace Differentiation
{
    internal class MNK : IDifferentiationMethod
    {
        public List<ObservablePoint> Differentiate(double a, double b, double step, int degree, string function)
        {
            List<ObservablePoint> points = new List<ObservablePoint>();
            for (double x = a; x <= b; x += step)
            {
                try
                {
                    double y_0 = function.Substitute("x", x - step).Result;
                    double y_1 = function.Substitute("x", x).Result;
                    double y_2 = function.Substitute("x", x + step).Result;
                    double y_3 = function.Substitute("x", x + (2 * step)).Result;
                    double diff = (-2 * y_0) - (-3 * y_1) + (6 * y_2) - y_3;
                    diff *= 1 / (6 * step);
                    points.Add(new ObservablePoint(x, diff));
                }
                catch { break; }
            }

            return points;
        }
    }
}