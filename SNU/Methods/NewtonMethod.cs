using SNU.Math;

namespace SNU.Methods
{
    public class NewtonMethod
    {
        private double initialGuess;

        public double Solve(INonLinearEquation equation, double tolerance, Func<double, double> derivative, int maxIterations)
        {
            int iter = 0;

            double x0, xn, xnp1, e;
          
            e = tolerance;
            x0 = initialGuess;
            xn = x0 - equation.Solve(x0)/(derivative(x0)*x0);
            xnp1 = xn - equation.Solve(xn)/(derivative(xn)*xn);

            while (xn - xnp1 >= e && iter < maxIterations)
            {
                iter++;
                xn = xnp1;
                xnp1 = xn - equation.Solve(xn)/(derivative(xn)*xn);
            }

            return xnp1;
        }

        public NewtonMethod(double initialGuess)
        {
            this.initialGuess = initialGuess;
        }
    }
}