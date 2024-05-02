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

        public ObservablePoint DifferentiatePoint(double x, double step, int degree, string function)
        {
            return Differentiate(x, x, step, degree, function)[0];
        }
    }
}