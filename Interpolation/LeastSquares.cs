using LiveCharts.Defaults;

namespace Interpolation
{
    internal class LeastSquares : IInterpolationMethod
    {
        private int degree;

        public List<ObservablePoint> Interpolate(List<ObservablePoint> values)
        {
            var xValues = values.Select(v => v.X).ToArray();
            var yValues = values.Select(v => v.Y).ToArray();

            var coefficients = CalculateCoefficients(xValues, yValues, degree);

            var interpolatedValues = new List<ObservablePoint>();

            for (double x = xValues.Min(); x <= xValues.Max(); x += 0.1)
            {
                var y = EvaluatePolynomial(coefficients, x);

                interpolatedValues.Add(new ObservablePoint(x, y));
            }

            return interpolatedValues;
        }

        public LeastSquares(int degree)
        {
            this.degree = degree;
        }

        private double[] CalculateCoefficients(double[] xValues, double[] yValues, int degree)
        {
            var matrix = new double[degree + 1, degree + 1];
            var vector = new double[degree + 1];

            for (int i = 0; i <= degree; i++)
            {
                for (int j = 0; j <= degree; j++)
                {
                    matrix[i, j] = Sum(xValues, i + j);
                }

                vector[i] = Sum(xValues, i, yValues);
            }

            var coefficients = SolveLinearSystem(matrix, vector);

            return coefficients;
        }


        private double Sum(double[] xValues, int power)
        {
            double sum = 0;

            foreach (var x in xValues)
            {
                sum += Math.Pow(x, power);
            }

            return sum;
        }

        private double Sum(double[] xValues, int power, double[] yValues)
        {
            double sum = 0;

            for (int i = 0; i < xValues.Length; i++)
            {
                sum += Math.Pow(xValues[i], power) * yValues[i];
            }

            return sum;
        }


        private double[] SolveLinearSystem(double[,] matrix, double[] vector)
        {
            var n = matrix.GetLength(0);

            var coefficients = new double[n];

            for (int i = 0; i < n; i++)
            {
                coefficients[i] = vector[i];

                for (int j = 0; j < i; j++)
                {
                    coefficients[i] -= matrix[i, j] * coefficients[j];
                }

                coefficients[i] /= matrix[i, i];
            }


            for (int i = n - 2; i >= 0; i--)
            {
                for (int j = i + 1; j < n; j++)
                {
                    coefficients[i] -= matrix[i, j] * coefficients[j];
                }

                coefficients[i] /= matrix[i, i];
            }

            return coefficients;

        }


        private double EvaluatePolynomial(double[] coefficients, double x)
        {
            double y = 0;

            for (int i = 0; i < coefficients.Length; i++)
            {
                y += coefficients[i] * Math.Pow(x, i);
            }

            return y;
        }

    }
}