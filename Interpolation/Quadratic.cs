using LiveCharts.Defaults;

namespace Interpolation
{
    internal class Quadratic : IInterpolationMethod
    {
        public List<ObservablePoint> Interpolate(List<ObservablePoint> values)
        {
            List<ObservablePoint> interpolatedPoints = new List<ObservablePoint>();


            if (values.Count < 3)
            {
                return interpolatedPoints;
            }

            for (int i = 0; i < values.Count - 2; i++)
            {
                ObservablePoint p0 = values[i];
                ObservablePoint p1 = values[i + 1];
                ObservablePoint p2 = values[i + 2];

                double x0 = p0.X;
                double x1 = p1.X;
                double x2 = p2.X;


                double y0 = p0.Y;
                double y1 = p1.Y;
                double y2 = p2.Y;

                double denominator = (x0 - x1) * (x0 - x2) * (x1 - x2);

                double a = (x2 * (y0 - y1) + x1 * (y2 - y0) + x0 * (y1 - y2)) / denominator;
                double b = (x2 * x2 * (y1 - y0) + x1 * x1 * (y0 - y2) + x0 * x0 * (y2 - y1)) / denominator;
                double c = (x1 * x2 * (x1 - x2) * y0 + x2 * x0 * (x2 - x0) * y1 + x0 * x1 * (x0 - x1) * y2) / denominator;


                for (double x = x0; x <= x2; x += 0.1)
                {
                    double y = a * x * x + b * x + c;
                    interpolatedPoints.Add(new ObservablePoint(x, y));
                }
            }

            return interpolatedPoints;
        }
    }
}