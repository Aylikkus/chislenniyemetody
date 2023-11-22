using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLAULIB.SLAU;

namespace SLAULIB.SolveMethods
{
    public class LUDecompositionMethod : ISolveMethod
    {
        public List<double> Solve(SystemOfLinearEquations system)
        {            
            double[,] LU = FactorizeTheMatrix(system.Matrix, 0, 0);
            double[,] L;
            double[,] U;
            int size = LU.GetLength(0);

            L = LU.Clone() as double[,];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (j > i)
                    {
                        L[i, j] = 0;
                    }
                }
            }

            U = LU.Clone() as double[,];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (j == i)
                    {
                        U[i, j] = 1;
                    }
                    else if (i > j)
                    {
                        U[i, j] = 0;
                    }
                }
            }

            double[,] Y = FindY(L, system.B);
            List <double> solution = FindX(U, Y);
            return solution;

        }

        public static List<double> FindX(double[,] matrix, double[,] vector)
        {
            int n = vector.Length;
            double[,] solution = new double[n, 1];

            for (int i = 0; i < n; i++)
            {
                solution[i, 0] = 1;
            }

            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = n - 1; j >= 0; j--)
                {
                    if (i != j)
                    {
                        sum -= matrix[i, j] * solution[j, 0];
                    }
                }
                solution[i, 0] = (vector[i, 0] + sum) / matrix[i, i];
            }

            List<double> solutions = new List<double>();

            for (int i = 0; i < n; i++)
            {
                solutions.Add(solution[i, 0]);
            }

            return solutions;
        }

        public static double[,] FindY(double[,] matrix, double[,] vector)
        {
            int n = vector.Length;
            double[,] solution = new double[n, 1];

            for (int i = 0; i < n; i++)
            {
                solution[i, 0] = 1;
            }

            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        sum -= matrix[i, j] * solution[j, 0];
                    }
                }
                solution[i, 0] = (vector[i, 0] + sum) / matrix[i, i];
            }

            return solution;
        }

        public static double[,] FactorizeTheMatrix(double[,] A, int i, int j)
        {
            double result = 0;
            int length = A.GetLength(0);

            if (i != A.GetLength(0) - 1 && j != A.GetLength(0) - 1)
            {
                if (i == 0 && j == 0)
                {
                    for (int step = i; step < length; step++)
                    {
                        A[step, i] = A[step, i];
                    }
                    for (int step = j + 1; step < length; step++)
                    {
                        A[i, step] = A[i, step] / A[i, j];
                    }
                }
                else
                {
                    for (int step = i; step < length; step++)
                    {
                        for (int s = 0; s <= j - 1; s++)
                        {
                            result -= A[step, s] * A[s, j];
                        }
                        A[step, j] = A[step, j] + result;
                        result = 0;
                    }
                    result = 0;
                    for (int step = j + 1; step < length; step++)
                    {
                        for (int s = 0; s <= i - 1; s++)
                        {
                            result -= A[i, s] * A[s, step];
                        }
                        A[i, step] = (A[i, step] + result) / A[i, i];
                        result = 0;
                    }
                }

                if (i != A.GetLength(0) && j != A.GetLength(0))
                {
                    FactorizeTheMatrix(A, i + 1, j + 1);
                }
            }
            else
            {
                for (int s = 0; s <= j - 1; s++)
                {
                    result -= A[i, s] * A[s, j];
                }
                A[i, j] = A[i, j] + result;
            }
            return A;

        }
    }
}

