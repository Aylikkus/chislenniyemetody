using LiveCharts.Defaults;

namespace Interpolation
{
    internal class Lagranj : IInterpolationMethod
    {
        public List<ObservablePoint> Interpolate(List<ObservablePoint> values)
        {
            List<ObservablePoint> result = new List<ObservablePoint>();

            int n = values.Count - 1;
            double[] x = values.Select(v => v.X).ToArray();
            double[] y = values.Select(v => v.Y).ToArray();

            for (double xi = x[0]; xi <= x[n]; xi += 0.1)
            {
                double yi = LagrangeInterpolate(xi, x, y);
                result.Add(new ObservablePoint(xi, yi));
            }

            return result;
        }

        private double LagrangeInterpolate(double x, double[] xValues, double[] yValues)
        {
            double result = 0;
            for (int i = 0; i < xValues.Length; i++)
            {
                double basis = 1;

                for (int j = 0; j < xValues.Length; j++)
                {
                    if (i != j)
                    {
                        basis *= (x - xValues[j]) / (xValues[i] - xValues[j]);
                    }
                }
                result += basis * yValues[i];
            }

            return result;
        }
    }
}