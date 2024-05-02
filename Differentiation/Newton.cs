using LiveCharts.Defaults;
using StringMath;
using System.Collections.Generic;

namespace Differentiation
{
    internal class Newton : IDifferentiationMethod
    {
        const int POLYNOMIALS = 5;

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
                    for (int i = 0; i < POLYNOMIALS; i++) 
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

            if (degree > 1)
            {
                int curDegree = 1;
                while (curDegree != degree)
                {
                    points = Differentiate(points, step, curDegree, function);
                    curDegree++;
                }
            }

            return points;
        }

        private List<ObservablePoint> Differentiate(List<ObservablePoint> points, double step, int curDegree, string function)
        {
            List<ObservablePoint> newPoints = new List<ObservablePoint>();
            double x_0 = points[0].X;
            for (int i = 0; i < points.Count; i++)
            {
                try
                {
                    double x = points[i].X;
                    double q = (x - x_0) / step;
                    double y = points[i].Y;
                    double diff = y;
                    for (int j = 0; j < POLYNOMIALS; j++)
                    {
                        double numerator = RightDiff.FiniteDiff(x, step, curDegree + j + 2, function);
                        for (int k = 0; k < j + 1; k++)
                        {
                            numerator *= q - k;
                        }
                        double denominator = Utility.GetFactorial(j + 1);
                        diff += numerator / denominator;
                    }
                    newPoints.Add(new ObservablePoint(x, diff));
                }
                catch { break; }
            }

            return newPoints;
        }

        public ObservablePoint DifferentiatePoint(double x, double step, int degree, string function)
        {
            return Differentiate(x, x, step, degree, function)[0];
        }
    }
}