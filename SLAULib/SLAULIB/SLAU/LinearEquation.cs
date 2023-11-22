using System;
using System.Collections.Generic;

namespace SLAULIB.SLAU
{
    public class LinearEquation
    {
        public List<double> Coefficients { get; set; }
        public double B { get; set; }

        // new LinearEquation(5, 1, 2, 3) -- 1x + 2x + 3x = 5
        public LinearEquation(double b, params double[] coefficients)
        {
            Coefficients = new List<double>();
            Coefficients.AddRange(coefficients);
            B = b;
        }
    }
}
