using LiveCharts.Defaults;
using StringMath;
using System.Collections.Generic;

namespace Differentiation
{
    internal class Newton : IDifferentiationMethod
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
                    double y = function.Substitute("x", x).Result;
                    double diff = y;
                    for (int i = 0; i < degree; i++) 
                    {
                        double numerator = RightDiff.FiniteDiff(x, step, i + 1, function);
                        for (int j = 0; j < i + 1; j++) 
                        {
                            numerator *= q - j;
                        }
                        double denominator = Utility.GetFactorial(i + 1);
                        diff += numerator / denominator;
                    }
                    points.Add(new ObservablePoint(x, diff * (1 / step)));
                }
                catch { break; }
            }

            return points;
        }
    }
}