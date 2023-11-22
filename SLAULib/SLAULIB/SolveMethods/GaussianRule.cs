using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLAULIB.SLAU;

namespace SLAULIB.SolveMethods
{
    public class GaussianRule : ISolveMethod
    {
        public List<double> Solve(SystemOfLinearEquations system)
        {
            double[,] A = system.Matrix.Clone() as double[,];
            double[,] B = system.B.Clone() as double[,];
            int rowsCount = A.GetLength(0);
            int columnsCount = A.GetLength(1);
            double[] solution = new double[rowsCount];

            double m1, m2;

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = i + 1; j < columnsCount; j++)
                {
                    m1 = A[j, i] / A[i, i];
                    for (int k = i; k < columnsCount; k++)
                    {
                        A[j, k] = A[j, k] - m1 * A[i, k];
                    }
                    B[j, 0] = B[j, 0] - m1 * B[i, 0];
                }
            }

            for (int i = rowsCount - 1; i >= 0; i--)
            {
                m1 = 0;
                for (int j = i; j < columnsCount; j++)
                {
                    m2 = A[i, j] * solution[j];
                    m1 += m2;
                }
                solution[i] = (B[i, 0] - m1) / A[i, i];
            }

            return new List<double>(solution);
        }
    }
}
