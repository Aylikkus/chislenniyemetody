using LiveCharts.Defaults;

namespace Interpolation
{
    internal class Newton : IInterpolationMethod
    {
        public List<ObservablePoint> Interpolate(List<ObservablePoint> values)
        {
            List<ObservablePoint> result = new List<ObservablePoint>();

            double[] x = values.Select(v => v.X).ToArray();
            double[] y = values.Select(v => v.Y).ToArray();

            // Рассчитываем конечные разности
            double[,] coef = DividedDiff(x, y);

            // Расчитать для каждого x
            for (int i = 0; i < x.Length; i++)
            {
                double p = NewtonPoly(coef, x, x[i]);
                result.Add(new ObservablePoint(x[i], p));
            }

            return result;
        }

        private double[,] DividedDiff(double[] x, double[] y)
        {
            int n = y.Length;
            double[,] coef = new double[n, n];

            // Первая колонока - y
            for (int i = 0; i < n; i++)
            {
                coef[i, 0] = y[i];
            }

            for (int j = 1; j < n; j++)
            {
                for (int i = 0; i < n - j; i++)
                {
                    coef[i, j] = (coef[i + 1, j - 1] - coef[i, j - 1]) / (x[i + j] - x[i]);
                }
            }

            return coef;
        }

        private double NewtonPoly(double[,] coef, double[] x, double xVal)
        {
            int n = x.Length - 1;
            double p = coef[n, n];

            for (int k = 1; k <= n; k++)
            {
                p = coef[n - k, n - k] + (xVal - x[n - k]) * p;
            }

            return p;
        }
    }
}