using SNU.Math;

namespace SNU.Methods
{
    struct Point
    {
        public double X {get; set;}
        public double Y {get; set;}

        public Point(double x, double y)
        {
            X = y;
            Y = y;
        }
    }

    public class CuttingChords
    {
        private double initialGuess;

        public double Solve(INonLinearEquation equation, double tolerance, int maxIterations)
        {
            double X = initialGuess;
            Point X1, X0; 
            double? X2;
            X0 = new Point(X, equation.Solve(X)); // Начальная точка итерации
            if (X0.Y == 0) return X0.X;
            X1 = new Point(X * .5 + 1, 0); // Начальная точка смещения итерации
            do
            {
                X1.Y = equation.Solve(X1.X); // Значение функции в точка смещения
                if (X1.Y == 0) return X1.X; // Проверка на случайно найденное решение
                X2 = Suppression(X0, X1); // Следующая точка смещения - пересечение оси Х прямой, проходящей через последнии две точки
                if (X2 == null) return X1.X; // Проверка на ошибку, если нет пересечения
                X0.Y = X1.Y; X0.X = X1.X; // Смещение данных для следующего шага итерации
                X1.X = (double)X2;
            } while (System.Math.Abs(X0.X - X1.X) >= tolerance); // Проверка на найденное решение
            return X1.X;
        }

        static double? Suppression(Point A, Point B)
        {
            if (A.Y == B.Y) return null;
            if (A.X == B.X) return A.X;
            ///  За основу взята формула прямой проходящей через 2 точки
            ///   Y - A.Y       X - A.X           Y - A.Y
            ///  ---------  = ---------- <=> X = ---------- * (B.X - A.X) + A.X
            ///  B.Y - A.Y     B.X - A.X         B.Y - A.Y
            return -A.Y * (B.X - A.X) / (B.Y - A.Y) + A.X;
        }

        static double? Suppression(Point A, double K)
        {
            if (K == 0) return null;
            ///  За основу взята формула прямой проходящей через точку
            ///                                          A.Y - Y
            ///  Y = K * (X - A.X) + A.Y  <=> X = A.X - --------- 
            ///                                              K       
            return A.X - A.Y / K;
        }

        public double Solve(INonLinearEquation equation)
        {
            return Solve(equation, 0.01, 1000000);
        }

        public CuttingChords(double initialGuess)
        {
            this.initialGuess = initialGuess;
        }
    }
}