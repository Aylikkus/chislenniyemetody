using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Solvers
{
    public class EulerAdvanced : IODESolver
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
                $"yhalf = y + ({h} / 2)*d(x, y);" +
                $"y = y + {h}*d(x + ({h} / 2), yhalf);" +
                $"Y = [Y y];" +
                $"end");
        }

        private EulerAdvanced() { }

        public EulerAdvanced(int a, int b, double h)
        {
            A = a;
            B = b;
            H = h;
        }
    }
}
