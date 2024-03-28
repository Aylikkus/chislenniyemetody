using Integration_Framework;
using StringMath;
using System;
using System.Windows;

namespace Integration
{
    internal class MonteCarlo : IIntegrationMethod
    {
        private readonly int _points;

        public MonteCarlo(int points) 
        {
            _points = points;
        }            
        
        public double Integrate(string function, double a, double b, double step)
        {

            double result = 0, x = a, sumValuesFunctions = 0;
            Random r = new Random();
            int countOfPoints = _points;            
            for (int i = 0; i < countOfPoints; i++)
            {
                while (x <= a || x > b)
                    x = r.Next((int)a, (int)b) + r.NextDouble();
                sumValuesFunctions += function.Substitute("x", x).Result;
                x = a;
            }
            result = (b - a) / countOfPoints * sumValuesFunctions;


            return System.Math.Round(result, 4);
        }
                
    }
}