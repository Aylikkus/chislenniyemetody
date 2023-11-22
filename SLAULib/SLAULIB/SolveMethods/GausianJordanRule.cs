using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLAULIB.SLAU;

namespace SLAULIB.SolveMethods
{
    public class GausianJordanRule : ISolveMethod
    {
        public List<double> Solve(SystemOfLinearEquations system)
        {
            double[,] A = system.Matrix.Clone() as double[,];
            double[,] B = system.B.Clone() as double[,];
            int rowsCount = A.GetLength(0);
            int columnsCount = A.GetLength(1);
            double[] solution = new double[rowsCount];

            double m;

            // Forward elimination
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < rowsCount; j++)
                {
                    if (j != i)
                    {
                        m = A[j, i] / A[i, i];

                        for (int k = 0; k < columnsCount; k++)
                        {
                            A[j, k] = A[j, k] - m * A[i, k];
                        }

                        B[j, 0] = B[j, 0] - m * B[i, 0];
                    }
                }
            }

            // Back substitution
            for (int i = 0; i < rowsCount; i++)
            {
                solution[i] = B[i, 0] / A[i, i];
            }

            return new List<double>(solution);
        }
    }
}
