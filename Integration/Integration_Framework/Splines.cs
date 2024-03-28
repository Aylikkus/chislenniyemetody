using StringMath;
using MathNet.Numerics.Interpolation;
using MathNet.Numerics;
using MathNet;
using System;
using System.Linq;
using System.Windows;
using MathNet.Symbolics;
using System.Collections.Generic;
using Integration_Framework;

namespace Integration
{
    internal class Splines : IIntegrationMethod
    {
        public double Integrate(string function, double a, double b, double step)
        {
            SymbolicExpression func = SymbolicExpression.Parse(function.Replace("}", "").Replace("{", ""));
            double result = 0;
            Trapezoid t = new Trapezoid();
            result += t.Integrate(function, a, b, step);          
            
            for (double x = a + step; x < b; x += step)
            {
                result -= (Math.Pow(step, 3) * func.Derivative(x, 2))/12; 
            }

            return System.Math.Round(result, 4);
        }

       
    }
}