using System;
using System.Collections.Generic;
using System.Linq;
using SLAULIB.SolveMethods;

namespace SLAULIB.SLAU
{
    public class SystemOfLinearEquations
    {
        private List<LinearEquation> linearEquations;

        private int getMaxCoefficients()
        {
            int max = 0;
            foreach (LinearEquation e in linearEquations)
            {
                int coefficientsCount = e.Coefficients.Count();
                max = coefficientsCount > max ? coefficientsCount : max;
            }
            return max;
        }

        public double[,] Matrix
        {
            get
            {
                double[,] matrix = new double[linearEquations.Count(), getMaxCoefficients()];
                for (int i = 0; i < linearEquations.Count(); i++)
                {
                    LinearEquation e = linearEquations[i];
                    for (int j = 0; j < e.Coefficients.Count(); j++)
                    {
                        matrix[i, j] = e.Coefficients[j];
                    }
                }
                return matrix;
            }
        }

        public double[,] B
        {
            get
            {
                int count = linearEquations.Count();
                double[,] b = new double[count, 1];
                for (int i = 0; i < count; i++)
                {
                    b[i, 0] = linearEquations[i].B;
                }
                return b;
            }
        }

        public SystemOfLinearEquations()
        {
            linearEquations = new List<LinearEquation>();
        }

        public void AddEquation(LinearEquation equation)
        {
            linearEquations.Add(equation);
        }

        public List<double> Solve(ISolveMethod method)
        {
            int n = linearEquations.Count();
            int m = getMaxCoefficients();
            if (n != m) return new List<double>();

            return method.Solve(this);
        }
    }
}
