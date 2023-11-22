using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLAULIB.SLAU;

namespace SLAULIB.SolveMethods
{
    public class MatrixMethod : ISolveMethod
    {
        public List<double> Solve(SystemOfLinearEquations system)
        {
            double[,] A = system.Matrix;
            double[,] B = system.B;
            int rowsCount = A.GetLength(0);
            int columnsCount = A.GetLength(1);

            List<double> solution = new List<double>();
            if (rowsCount != columnsCount) return solution;

            foreach (double x in MatrixUtility.MatrixMultiply(
                MatrixUtility.FindInverse(A), B))
            {
                solution.Add(x);
            }

            return solution;
        }
    }
}
