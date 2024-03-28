using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration
{
    public class SplineCore
    {
        private double[] c; // Коэффициенты c для каждого отрезка сплайна

        public SplineCore(double[] x, double[] y)
        {
            // Проверка, что массивы x и y имеют одинаковую длину
            if (x.Length != y.Length)
                throw new ArgumentException("Массивы x и y должны иметь одинаковую длину.");

            int n = x.Length - 1;

            // Инициализация массива c
            c = new double[n];

            // Вычисление разностей h и delta
            double[] h = new double[n];
            double[] delta = new double[n];
            for (int i = 0; i < n; i++)
            {
                h[i] = x[i + 1] - x[i];
                delta[i] = (y[i + 1] - y[i]) / h[i];
            }

            // Создание и решение системы уравнений для коэффициентов c
            double[] b = new double[n - 2];
            double[,] A = new double[n - 2, n - 2];
            for (int i = 0; i < n - 2; i++)
            {
                A[i, i] = 2 * (h[i] + h[i + 1]);
                if (i > 0)
                    A[i, i - 1] = h[i];
                if (i < n - 3)
                    A[i, i + 1] = h[i + 1];
                b[i] = 6 * (delta[i + 1] - delta[i]);
            }

            SolveSystem(A, c, b);
        }

        private void SolveSystem(double[,] A, double[] x, double[] b)
        {
            //int n = x.Length;

            //// Прямой ход метода Гаусса
            //for (int i = 0; i < n - 1; i++)
            //{
            //    double pivot = A[i, i];
            //    for (int j = i + 1; j < n; j++)
            //    {
            //        double ratio = A[j, i] / pivot;
            //        for (int k = i; k < n; k++)
            //        {
            //            A[j, k] -= ratio * A[i, k];
            //        }
            //        b[j] -= ratio * b[i];
            //    }
            //}

            //// Обратный ход метода Гаусса
            //x[n - 1] = b[n - 1] / A[n - 1, n - 1];
            //for (int i = n - 2; i >= 0; i--)
            //{
            //    double sum = b[i];
            //    for (int j = i + 1; j < n; j++)
            //    {
            //        sum -= A[i, j] * x[j];
            //    }
            //    x[i] = sum / A[i, i];
            //}
        }

        public double[] GetCoefficients()
        {
            return c;
        }
    }
}
