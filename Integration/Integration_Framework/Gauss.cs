using MathNet.Symbolics;
using StringMath;

namespace Integration
{
    internal class Gauss : IIntegrationMethod
    {
        private readonly int nodesCount;

        public Gauss(int nodesCount)
        {
            this.nodesCount = nodesCount;
        }

        public double Integrate(string function, double a, double b, double step)
        {
            double result = 0;
            double A, x = 0;
            
            for (int i = 1; i <= nodesCount; i++)
            {
                // Вычисление значения i-го узла, вычисление значения производной полинома Лежандра
                double ti = GetT(nodesCount - 1, i, nodesCount), pd = GetPd(nodesCount, ti);
                A = 2 / ((1 - System.Math.Pow(ti,2)) * System.Math.Pow(pd, 2));
                x = GetX(a, b, ti);
                result += A * function.Substitute("x", x).Result;
            }
            result *= (b - a) / 2;

            return System.Math.Round(result, 4);
        }

        // Вычисление значения x
        private double GetX(double a, double b, double t)
        {
            return (a + b) / 2 + (b - a) / 2 * t;
        }

        // Вычисление значений k-го корня полинома Лежанжра
        private double GetT(int k, int i, int n)
        {
            if (k == 0)
            {
                double t1 = System.Math.PI * (4 * i - 1), t2 = 4 * n + 2;
                return System.Math.Cos(t1 / t2);
            }
            double tprev = GetT(k - 1, i, n);
            return tprev - GetP(n, tprev) / GetPd(n, tprev);
        }
        
        // Вычисление полинома Лежандра степени n
        private double GetP(double n, double t)
        {
            switch (n)
            {
                case 0:
                    return 1;
                case 1:
                    return t;
                default:
                    return (2 * (n - 1) + 1) / ((n - 1) + 1) * t * GetP(n - 1, t)
                     - (n - 1) / ((n - 1) + 1) * GetP(n - 2, t);

            }
        }

        // Вычисление производной полинома Лежандра
        private double GetPd(double n, double t)
        {
            return (n / (1 - t * t )) * (GetP(n-1, t) - t * GetP(n, t));
        }
    }
}