using LiveCharts.Defaults;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace Differentiation
{
    public interface IDifferentiationMethod
    {
        List<ObservablePoint> Differentiate(double a, double b, double step, int degree, string function);
    }
}