using StringMath;


namespace Integration
{
    internal class RectangleLeft : IIntegrationMethod
    {

        public double Integrate(string function, double a, double b, double step)
        {
            double result = 0;

            for (double i = a; i < b; i+= step)
            {
                var f = function.Substitute("x",System.Math.Round(i, 6));
                result += System.Math.Round(step * f.Result, 4);
            }

            return result;
        }
    }
}