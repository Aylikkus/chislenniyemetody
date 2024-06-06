using SNU.Math;

namespace SNU.Methods
{
    public class GaussSeidelMethod
    {
        private double[] initialValues;
        
        public GaussSeidelMethod(double[] initialValues)
        {
            this.initialValues = initialValues;
        }

        public double[] Solve(INonLinearEquation equation, double tolerance = 0.01, int maxIterations = 1000000)
        {
            double[] x = new double[initialValues.Length];

            Array.Copy(initialValues, x, initialValues.Length);

            double[] newX = new double[x.Length];

            for (int i = 0; i < maxIterations; i++)
            {
                newX = new double[x.Length];

                for (int j = 0; j < x.Length; j++)
                {
                    double sum = 0;

                    for (int k = 0; k < j; k++)
                    {
                        sum += equation.Coefficients[k].Value * System.Math.Pow(newX[k], equation.Coefficients[k].Degree);
                    }

                    for (int k = j + 1; k < x.Length; k++)
                    {
                        sum += equation.Coefficients[k].Value * System.Math.Pow(x[k], equation.Coefficients[k].Degree);
                    }

                    newX[j] = System.Math.Pow(-sum / equation.Coefficients[j].Value, 1.0 / equation.Coefficients[j].Degree);
                }

                bool converged = true;

                for (int j = 0; j < x.Length; j++)
                {
                    if (System.Math.Abs(newX[j] - x[j]) > tolerance)
                    {
                        converged = false;
                        break;
                    }
                }

                if (converged)
                {
                    return newX;
                }

                Array.Copy(newX, x, newX.Length);
            }

            return newX;
        }
    }
}