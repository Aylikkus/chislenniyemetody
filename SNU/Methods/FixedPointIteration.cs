using SNU.Math;

namespace SNU.Methods
{
    public class FixedPointIteration
    {
        private double initialGuess;

        public double Solve(INonLinearEquation equation, double tolerance, int maxIterations)
        {
            int iter = 0;
            double x0 = initialGuess;
            double x1;

            do
            {
                x1 = equation.Solve(x0);
                iter++;
                if (System.Math.Abs(x1 - x0) >= tolerance && iter >= maxIterations)
                {
                    break;
                }
                x0 = x1;
            } while (System.Math.Abs(x0 - equation.Solve(x0)) > tolerance);

            return x1;
        }

        public double Solve(INonLinearEquation equation)
        {
            return Solve(equation, 0.01, 1000000);
        }

        public FixedPointIteration(double initialGuess)
        {
            this.initialGuess = initialGuess;
        }
    }
}