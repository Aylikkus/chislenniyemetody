using LiveCharts.Defaults;
using StringMath;
using System.Collections.Generic;

namespace Differentiation
{
    internal class Square : IDifferentiationMethod
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
                    double diff = 1 / step * (d_y + ((2 * q - 1) / 2 * d2_y));
                    points.Add(new ObservablePoint(x, diff));
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
                    double d_y = RightDiff.FiniteDiff(x, step, curDegree + 1, function);
                    double d2_y = RightDiff.FiniteDiff(x, step, curDegree + 2, function);
                    double diff = 1 / step * (d_y + ((2 * q - 1) / 2 * d2_y));
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