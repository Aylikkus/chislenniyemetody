using StringMath;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Integration
{
    internal class Chebushev : IIntegrationMethod
    {
        private readonly int nodesCount;

        public Chebushev(int nodesCount)
        {
            this.nodesCount = nodesCount;
        }

        public double Integrate(string function, double a, double b, double step)
        {
            double result = 0;
            IEnumerable<double> t = GetTForNodesCount(nodesCount);
            IEnumerable<double> x = t.Select(ti => GetX(a, b, ti));
            IEnumerable<double> A = Enumerable.Repeat(2.0 / nodesCount, nodesCount);
            for (int i = 0; i < nodesCount; i++)
            {
                result += A.ElementAt(i) * function.Substitute("x", x.ElementAt(i)).Result;
            }
            result *= (b - a) / 2;
           
            return result;
        }


        // Получение x от t
        private double GetX(double a, double b, double t)
        {
            return (a + b) / 2 + (b - a) / 2 * t;
        }

        // Значение коэффициентов t в квадратурной формуле Чебышева в зависимости от значений n

        private static IEnumerable<double> GetTForNodesCount(int nodesCount)
        {
            switch (nodesCount)
            {
                case 1:
                    return new double[1] { 0 };
                case 2:
                    return new double[2] { -0.577340, 0.577460 };
                case 3:
                    return new double[3] { 0.707107, 0, -0.707107 };
                case 4:
                    return new double[4] { 0.794654, 0.187592, -0.794654, -0.187592 };
                case 5:
                    return new double[5] { 0.832498, 0.374541, 0, -0.832498, -0.374541 };
                case 6:
                    return new double[6] { 0.866247, 0.422519, 0.266635, -0.866247, -0.422519, -0.266635 };
                case 7:
                    return new double[7] { 0.883862, 0.529657, 0.323912, 0, -0.883862, -0.529657, -0.323912 };
                case 9:
                    return new double[9] { 0.911589, 0.601019, 0.528762, 0.167906, 0, -0.911589, -0.601019, -0.528762, -0.167906 };
                default:
                    throw new NotImplementedException($"Для размерности {nodesCount} метод не имеет действительных корней t");
            }
        }


    }
}