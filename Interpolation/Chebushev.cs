using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpolation
{
    public class Chebushev : IInterpolationMethod
    {
        private int degree;

        public List<ObservablePoint> Interpolate(List<ObservablePoint> values)
        {
            var points = new List<ObservablePoint>();
            foreach (var p in values)
            {
                double x = p.X;
                double y = Math.Cos(degree * Math.Acos(x));
                points.Add(new ObservablePoint(x, y));
            }
            return points;
        }

        public Chebushev(int degree)
        {
            this.degree = degree;
        }
    }
}
