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

            if (degree > 1)
            {
                int curDegree = 1;
                while (curDegree != degree)
                {
                    points = Differentiate(points, step);
                    curDegree++;
                }
            }

            return points;
        }

        private List<ObservablePoint> Differentiate(List<ObservablePoint> points, double step)
        {
            List<ObservablePoint> newPoints = new List<ObservablePoint>();

            if (points.Count < 4) return newPoints;

            for (int i = 1; i < points.Count - 2; i++)
            {
                try
                {
                    double y_0 = points[i - 1].Y;
                    double y_1 = points[i].Y;
                    double y_2 = points[i + 1].Y;
                    double y_3 = points[i + 2].Y; ;
                    double diff = (-2 * y_0) - (-3 * y_1) + (6 * y_2) - y_3;
                    diff *= 1 / (6 * step);
                    newPoints.Add(new ObservablePoint(points[i].X, diff));
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