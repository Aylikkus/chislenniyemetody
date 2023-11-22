using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLAULIB.SLAU;

namespace SLAULIB.SolveMethods
{
    public class CramersRule : ISolveMethod
    {
        private double[,] changeColumn(double[,] A, double[,] B, int column)
        {
            int rowsCount = A.GetLength(0);
            int columnsCount = A.GetLength(1);
            if (column > columnsCount) return A;

            double[,] result = A.Clone() as double[,];
            
            for (int i = 0; i < rowsCount; i++)
            {
                result[i, column - 1] = B[i, 0];
            }

            return result;
        }

        public List<double> Solve(SystemOfLinearEquations system)
        {
            List<double> solution = new List<double>();
            double[,] A = system.Matrix.Clone() as double[,];
            double[,] B = system.B.Clone() as double[,];
            int rowsCount = A.GetLength(0);
            int columnsCount = A.GetLength(1);

            double d = MatrixUtility.FindDeterminant(A);

            for (int i = 0; i < rowsCount; i++)
            {
                double di = MatrixUtility.FindDeterminant(changeColumn(A, B, i + 1));
                double x = di / d;
                solution.Add(x);
            }

            return solution;
        }
    }
}
