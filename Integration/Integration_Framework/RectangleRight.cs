using StringMath;

namespace Integration
{
    internal class RectangleRight : IIntegrationMethod
    {
        public double Integrate(string function, double a, double b, double step)
        {
            double x  = 0, result = 0;

            for (double i = a + step; i < b; i += step)
            {
                var f = function.Substitute("x", i);
                result += System.Math.Round(f.Result * step, 4);
                x = i;
            }
            result += System.Math.Round(function.Substitute("x", b).Result * (b-x), 4);

            return result;
        }
    }
}