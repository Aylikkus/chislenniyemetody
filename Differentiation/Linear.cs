using LiveCharts.Defaults;
using System.Collections.Generic;

namespace Differentiation
{
    internal class Linear : IDifferentiationMethod
    {
        public List<ObservablePoint> Differentiate(double a, double b, double step, int degree, string function)
        {
            return new RightDiff().Differentiate(a, b, step, degree, function);
        }
    }
}