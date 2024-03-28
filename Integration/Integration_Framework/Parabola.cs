using StringMath;
using System.IO;

namespace Integration
{
    internal class Parabola : IIntegrationMethod
    {
        public double Integrate(string function, double a, double b, double step)
        {
            double result = 0;
            int numberOfIteration = 0;

            for (double i = a; i < b; i += step)
            {
                if (numberOfIteration == 0)
                {
                    result += System.Math.Round(function.Substitute("x", i).Result, 4);
                }
                else if (numberOfIteration % 2 == 0)
                {
                    result += 2 * System.Math.Round(function.Substitute("x", i).Result, 4);
                }
                else if (numberOfIteration % 2 != 0)
                {
                    result += 4 * System.Math.Round(function.Substitute("x", i).Result, 4);
                }
      
                numberOfIteration++;
            }

            result += System.Math.Round(function.Substitute("x", b).Result, 4);

            return (step / 3) * result;
        }
    }
}