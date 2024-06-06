using LiveCharts.Defaults;

namespace Interpolation
{
    internal class QubicSpline : IInterpolationMethod
    {
        public List<ObservablePoint> Interpolate(List<ObservablePoint> values)
        {
            List<ObservablePoint> points = values.Select(v => new ObservablePoint { X = v.X, Y = v.Y }).ToList();

            int n = points.Count;
            float[] x = points.Select(p => (float)p.X).ToArray();
            float[] y = points.Select(p => (float)p.Y).ToArray();

            float[] a = new float[n];
            float[] b = new float[n];
            float[] c = new float[n];
            float[] d = new float[n];


            // Рассчитываем коэффициенты
            for (int i = 1; i < n - 1; i++)
            {
                float h = x[i] - x[i - 1];
                float lambda = h / (x[i + 1] - x[i - 1]);
                float mu = h / (x[i + 1] - x[i - 1]);


                a[i] = lambda;
                b[i] = 2;
                c[i] = mu;
                d[i] = 3 * ((y[i + 1] - y[i]) / (x[i + 1] - x[i]) - (y[i] - y[i - 1]) / (x[i] - x[i - 1]));

            }


            // Решаем трёхдиагональную матрицу

            float[] cPrime = new float[n];
            float[] dPrime = new float[n];

            cPrime[0] = c[0] / b[0];
            dPrime[0] = d[0] / b[0];

            for (int i = 1; i < n; i++)
            {
                float denominator = b[i] - a[i] * cPrime[i - 1];
                cPrime[i] = c[i] / denominator;
                dPrime[i] = (d[i] - a[i] * dPrime[i - 1]) / denominator;
            }

            float[] xSpline = new float[n];
            xSpline[n - 1] = dPrime[n - 1];

            for (int i = n - 2; i >= 0; i--)
            {
                xSpline[i] = dPrime[i] - cPrime[i] * xSpline[i + 1];
            }


            float stepSize = 0.1f;
            List<ObservablePoint> splinePoints = new List<ObservablePoint>();

            for (int i = 0; i < points.Count; i++)
            {
                float t = x[0] + i * stepSize;
                int j = GetNextXIndex(t, x);

                float h = t - x[j];
                float ySpline = (1 - h) * y[j] + h * y[j + 1] + h * (1 - h) * (a[j] * (1 - h) + b[j] * h);

                splinePoints.Add(new ObservablePoint(t, ySpline));
            }

            return splinePoints;
        }


        private int GetNextXIndex(float x, float[] xArray)
        {
            for (int i = 0; i < xArray.Length - 1; i++)
            {
                if (x <= xArray[i + 1])
                    return i;
            }

            return xArray.Length - 2;
        }
    }
}