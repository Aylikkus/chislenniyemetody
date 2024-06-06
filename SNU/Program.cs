using System.Security.Cryptography.X509Certificates;
using SNU.Math;
using SNU.Methods;

namespace SNU
{
    public class Program
    {
        public static void Print(string text, double[] res)
        {
            Console.WriteLine(text);
            for (int i = 1; i <= res.Length; i++)
            {
                Console.WriteLine($"1: {res[i-1]}");
            }
        }

        public static void Print(string text, double[][] res)
        {
            Console.WriteLine(text);
            for (int i = 1; i <= res.Length; i++)
            {
                Console.WriteLine($"1:");

                for (int j = 1; j <= res[i - 1].Length; j++)
                {
                    Console.WriteLine($"x{j} = {res[i - 1][j - 1]}");
                }
            }
        }

        public static void Main()
        {
            SystemOfNonLinearEquations system = new SystemOfNonLinearEquations(
                new List<INonLinearEquation> {
                    new PolynomEquation([-5, -2, 3])
                }
            );

            var FPIresults = system.SolveFPI(new FixedPointIteration(1));
            var GSresults = system.SolveGS(new GaussSeidelMethod([1, 1, 1]));
            var Nresults = system.SolveN(new NewtonMethod(1), (x) => (-10 * x) - 2);
            var CCresults = system.SolveCC(new CuttingChords(1));

            Print("Метод простых итераций", FPIresults);
            Print("Метод Зейделя", GSresults);
            Print("Метод Ньютона", Nresults);
            Print("Метод Секущих", CCresults);
        }
    }
}