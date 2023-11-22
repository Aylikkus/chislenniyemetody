using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLAULIB.SLAU;
using SLAULIB.SolveMethods;

namespace SLAULibTest
{
    class Program
    {
        static void Main(string[] args)
        {
            LinearEquation e1 = new LinearEquation(-10, 2, 1, 0, 0, 0);
            LinearEquation e2 = new LinearEquation(-26, 2, 9, 2, 0, 0);
            LinearEquation e3 = new LinearEquation(-16, 0, 4, 15, 8, 0);
            LinearEquation e4 = new LinearEquation(-2, 0, 0, 4, 15, -8);
            LinearEquation e5 = new LinearEquation(16, 0, 0, 0, 2, 3);


            SystemOfLinearEquations soe = new SystemOfLinearEquations();
            soe.AddEquation(e1);
            soe.AddEquation(e2);
            soe.AddEquation(e3);
            soe.AddEquation(e4);
            soe.AddEquation(e5);

            
            List<double> solution = soe.Solve(new TridiagonalMatrixMethod());


            int x = 0;
            Console.ReadLine();
        }
    }
}
