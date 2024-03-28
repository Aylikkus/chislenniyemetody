using StringMath;

namespace Integration
{
    internal class Trapezoid : IIntegrationMethod
    {
        public double Integrate(string function, double a, double b, double step)
        {
            double result = 0, h = 0, x = 0;
            MathExpr f0, f1;


            for (double i = a + step; i < b; i += step)
            {
                f0 = function.Substitute("x", i - step);
                f1 = function.Substitute("x", i);
                h = step;
                result += System.Math.Round(h * (f0.Result + f1.Result), 4);
                x = i;
            }
            result += System.Math.Round((b - x) * (function.Substitute("x", x).Result + function.Substitute("x", b)), 4); 

            return result/2;
        }
    }
}