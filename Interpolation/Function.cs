using LiveCharts.Defaults;
using System;
using System.Collections.Generic;

namespace Interpolation
{
    public class Function
    {
        public Func<double, double> Func;

        public double F(double x)
        {
            return Func(x);
        }

        // a - начало (x), b - конец (x), step - шаг
        public List<ObservablePoint> Interpolate(IInterpolationMethod method, double a, double b, double step)
        {
            List<ObservablePoint> points = new List<ObservablePoint>();
            for (double x = a; x <= b; x += step)
            {
                double y = F(x);
                ObservablePoint point = new ObservablePoint(x, y);
                points.Add(point);
            }
            return method.Interpolate(points);
        }

        public Function(Func<double, double> func)
        {
            Func = func;
        }

        private Function() { }
    }
}
