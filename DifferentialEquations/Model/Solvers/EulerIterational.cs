using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Solvers
{
    public class EulerIterational : IODESolver
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
                $"yk0 = y + {h}*d(x, y);" +
                $"yk1 = y + (({h} * d(x, y) + d(x + {h}, yk0)) / 2);" +
                $"yk2 = y + (({h} * d(x, y) + d(x + {h}, yk1)) / 2);" +
                $"yk3 = y + (({h} * d(x, y) + d(x + {h}, yk2)) / 2);" +
                $"Y = [Y yk3];" +
                $"end");
        }

        private EulerIterational() { }

        public EulerIterational(int a, int b, double h)
        {
            A = a;
            B = b;
            H = h;
        }
    }
}
