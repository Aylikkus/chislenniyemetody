using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Solvers
{
    public class RungeKuttThird : IODESolver
    {
        public MLApp.MLApp? MatLab { get; set; } = null;

        public int A { get; set; }
        public int B { get; set; }
        public double H { get; set; }

        public void Solve()
        {
            if (MatLab == null) return;

            string h = H.ToString(System.Globalization.CultureInfo.InvariantCulture);

            MatLab.Execute($"X = {A}:{h}:{B}");
            MatLab.Execute($"for x = {A}:{h}:{B};" +
                $"k1 = {h}*d(x, y);" +
                $"k2 = {h}*d(x + (1 * {h} / 3), y + (1 * k1 / 3));" +
                $"k3 = {h}*d(x + (2 * {h} / 3), y + (2 * k2 / 3));" +
                $"dy = 1 * (k1 + (3 * k3)) / 4;" +
                $"y = y + dy;" +
                $"Y = [Y y];" +
                $"end");
        }

        private RungeKuttThird() { }

        public RungeKuttThird(int a, int b, double h)
        {
            A = a;
            B = b;
            H = h;
        }
    }
}
