using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLAULIB.SLAU;

namespace SLAULIB.SolveMethods
{
    public class TridiagonalMatrixMethod : ISolveMethod
    {
        double[] Ai;
        double[] Bi;
    
        public List<double> Solve(SystemOfLinearEquations system)
        {
            int n = system.Matrix.GetLength(0);
            List<double> solution = new List<double>();
            double[,] A = system.Matrix;
            double[,] d = system.B;
            Ai = new double[n];
            Bi = new double[n];

            if (TridiagonalMatrixIsCorrect(A))
            {
                TridiagonalSolveRecursive(A, d, 0);                               
            }
            else
            {
                return solution;
                throw new ArgumentException("Matrix is not Tridiagonal");                
            }

            for (int i = Bi.Length - 1; i >= 0; i--)
            {
                if (i == Bi.Length - 1)
                {
                    solution.Add(Bi[i]);
                }
                else
                {                 
                    solution.Add(Ai[i] * solution[(solution.Count) - 1] + Bi[i]);
                }

            }

            solution.Reverse();

            return solution;
        }

        public void TridiagonalSolveRecursive(double[,] A, double[,] d, int i)
        {
            double n = A.GetLength(0);
            double ai = 1;
            double bi = 1;
            double ci = 1;
            if (i == 0)
            {
                bi = A[i, i];
                ci = A[i, i + 1];
                ai = 0;
            }
            else if (i == n - 1)
            {
                ai = A[i, i - 1];
                bi = A[i, i];
                ci = 0;
            }
            else
            {
                ai = A[i, i - 1];
                bi = A[i, i];
                ci = A[i, i + 1];
            }

            double di = d[i, 0];

            if (i == 0)
            {
                Ai[i] = -ci / bi;
                Bi[i] = di / bi;

                TridiagonalSolveRecursive(A, d, i + 1);
            }
            else
            {
                if (i == n - 1)
                {
                    Bi[i] = (di - ai * Bi[i - 1]) / (bi + ai * Ai[i - 1]);
                    return;
                }

                Ai[i] = -ci / (bi + ai * Ai[i - 1]);
                Bi[i] = (di - ai * Bi[i - 1]) / (bi + ai * Ai[i - 1]);

                TridiagonalSolveRecursive(A, d, i + 1);
            }
        }


        public static bool TridiagonalMatrixIsCorrect(double[,] matrix)
        {
            int n = matrix.GetLength(0);

            // Проверка на отсутствие ненулевых элементов в нетрёхдиагональной области

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (!(j == i || j == i - 1 || j == i + 1))
                    {
                        if (matrix[i, j] != 0) return false;
                    }
                }
            }

            // Проверка на наличие нулевых элементов в трёхдиагональной области
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == i || j == i - 1 || j == i + 1)
                    {
                        if (matrix[i, j] == 0) return false;
                    }
                }
            }
            return true;
        }


    }
}
