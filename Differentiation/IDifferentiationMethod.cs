using LiveCharts.Defaults;
using System.Text;

namespace Differentiation
{
    public interface IDifferentiationMethod
    {
        ObservablePoint[] Differentiate(int a, int b, double step, int degree, string function);
    }
}