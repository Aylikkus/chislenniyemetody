using LiveCharts.Defaults;
using StringMath;
using System;
using System.Collections.Generic;

namespace Differentiation
{
    internal class CentralDiff : IDifferentiationMethod
    {
        public List<ObservablePoint> Differentiate(double a, double b, double step, int degree, string function)
        {
            List<ObservablePoint> points = new List<ObservablePoint>();
            for (double x = a; x <= b; x += step)
            {
                try
                {
                    double diff = 0;
                    for (int i = 0; i <= degree; i++)
                    {
                        double C_im = Utility.GetBinCoef(degree, i);
                        double fxkh = function.Substitute("x", x + ((degree / 2) - i) * step).Result;
                        diff += Math.Pow(-1, i) * C_im * fxkh;
                    }
                    points.Add(new ObservablePoint(x, diff / Math.Pow(step, degree)));
                }
                catch { break; }
            }

            return points;
        }

        public ObservablePoint DifferentiatePoint(double x, double step, int degree, string function)
        {
            return Differentiate(x, x, step, degree, function)[0];
        }
    }
}