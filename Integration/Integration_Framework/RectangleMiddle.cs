using StringMath;

namespace Integration
{
    internal class RectangleMiddle : IIntegrationMethod
    {
        public double Integrate(string function, double a, double b, double step)
        {
            double x = 0, h = 0, result = 0;
            
            for (double i = a; i < b; i += step)
            {
                x = (i + step + i) / 2;
                h = step;
                var f = function.Substitute("x", x);
                result += System.Math.Round(f.Result * h, 4);
            }
            result += System.Math.Round(function.Substitute("x", (b + x) / 2).Result * (b - x), 4);


            return result;
        }
    }
}