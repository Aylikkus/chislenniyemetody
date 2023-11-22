using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAULIB
{
    public static class MatrixUtility
    {
        public static double[,] TransPositioned(double[,] matrix)
        {
            int rowsCount = matrix.GetLength(0);
            int columnsCount = matrix.GetLength(1);
            double[,] transpositioned = new double[columnsCount, rowsCount];

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    transpositioned[j, i] = matrix[i, j];
                }
            }

            return transpositioned;
        }

        public static double[,] CreateSubMatrix(double[,] matrix, int excludeRow, int excludeCol)
        {
            int size = matrix.GetLength(0);
            double[,] subMatrix = new double[size - 1, size - 1];

            int r = 0;

            for (int i = 0; i < size; i++)
            {
                if (i == excludeRow)
                {
                    continue;
                }

                int c = 0;

                for (int j = 0; j < size; j++)
                {
                    if (j == excludeCol)
                    {
                        continue;
                    }

                    subMatrix[r, c] = matrix[i, j];
                    c++;
                }

                r++;
            }

            return subMatrix;
        }

        public static double FindDeterminant(double[,] matrix)
        {
            int size = matrix.GetLength(0);

            if (size == 1)
            {
                return matrix[0, 0];
            }

            double determinant = 0;

            for (int i = 0; i < size; i++)
            {
                double[,] subMatrix = CreateSubMatrix(matrix, 0, i);
                determinant += Math.Pow(-1, i) * matrix[0, i] * FindDeterminant(subMatrix);
            }

            return determinant;
        }

        public static double[,] FindInverse(double[,] matrix)
        {
            double determinant = FindDeterminant(matrix);
            if (determinant == 0) throw new ArgumentException("Matrix determinant is zero");
            double invertDeterminant = 1.0 / determinant;

            int rowsCount = matrix.GetLength(0);
            int columnsCount = matrix.GetLength(1);
            double[,] transposed = TransPositioned(matrix);

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    double[,] subMatrix = CreateSubMatrix(transposed, i, j);
                    double sign = Math.Pow(-1, i + j + 2);
                    matrix[i, j] = sign * invertDeterminant * FindDeterminant(subMatrix);
                }
            }

            return matrix;
        }

        public static double[,] MatrixMultiply(double[,] A, double[,] B)
        {
            int rowsA = A.GetLength(0);
            int columnsA = A.GetLength(1);
            int rowsB = B.GetLength(0);
            int columnsB = B.GetLength(1);
            if (columnsA != rowsB) throw new ArgumentException("Matrixes can't be multiplied");
            else
            {
                double temp = 0;
                double[,] result = new double[rowsA, columnsB];
                for (int i = 0; i < rowsA; i++)
                {
                    for (int j = 0; j < columnsB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < columnsA; k++)
                        {
                            temp += A[i, k] * B[k, j];
                        }
                        result[i, j] = temp;
                    }
                }
                return result;
            }
        }
    }
}
