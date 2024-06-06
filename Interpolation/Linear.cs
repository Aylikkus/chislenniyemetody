using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpolation
{
    public class Linear : IInterpolationMethod
    {
        public List<ObservablePoint> Interpolate(List<ObservablePoint> values)
        {
            return values;
        }
    }
}
