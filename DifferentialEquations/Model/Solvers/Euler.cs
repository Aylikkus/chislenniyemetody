using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Solvers
{
    public class Euler : IODESolver
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
                $"y = y + {h}*d(x, y);" +
                $"Y = [Y y];" +
                $"end");
        }

        private Euler() { }

        public Euler(int a, int b, double h)
        {
            A = a;
            B = b;
            H = h;
        }
    }
}
