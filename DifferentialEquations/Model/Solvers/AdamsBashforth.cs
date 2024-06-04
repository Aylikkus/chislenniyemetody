using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Solvers
{
    public class AdamsBashforth : IODESolver
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
                $"fi = d(x, y);" +
                $"if exist('fim1', 'var') == 0 fim1 = fi; end;" +
                $"if exist('fim2', 'var') == 0 fim2 = fim1; end;" +
                $"if exist('fim3', 'var') == 0 fim3 = fim2; end;" +
                $"if exist('fim4', 'var') == 0 fim4 = fim3; end;" +
                $"ym4 = d(x, y) + {h} * fim4;" +
                $"ym3 = ym4 + {h} * ((3 * fim3 / 2) - (fim4 / 2));" +
                $"ym2 = ym3 + {h} * ((23 * fim2 / 12) - (4 * fim3 / 2) + (5  * fim4 / 12));" +
                $"ym1 = ym2 + {h} * ((55 * fim1 / 24) - (59 * fim2 / 24) + (37  * fim3 / 24) - (3 * fim4 / 8));" +
                $"y = ym1 + {h} * ((1901 * fi / 720) - (1387 * fim1 / 360) + (109  * fim2 / 30) - (637 * fim3 / 360) + (251  * fim4 / 720));" +
                $"Y = [Y y];" +
                $"fim4 = fim3;" +
                $"fim3 = fim2;" +
                $"fim2 = fim1;" +
                $"fim1 = fi;" +
                $"end");
        }

        private AdamsBashforth() { }

        public AdamsBashforth(int a, int b, double h)
        {
            A = a;
            B = b;
            H = h;
        }
    }
}
