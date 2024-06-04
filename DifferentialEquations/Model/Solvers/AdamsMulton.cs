using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Solvers
{
    public class AdamsMulton : IODESolver
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
                $"ym3 = ym4 + ({h} * (fim3 + fim4) / 2);" +
                $"ym2 = ym3 + {h} * ((5 * fim2 / 12) + (2 * fim3 / 3) - (1  * fim4 / 12));" +
                $"ym1 = ym2 + {h} * ((3 * fim1 / 8) + (19 * fim2 / 24) - (5  * fim3 / 24) + (1 * fim4 / 24));" +
                $"y = ym1 + {h} * ((251 * fi / 720) + (646 * fim1 / 720) - (264  * fim2 / 720) + (106 * fim3 / 720) - (19  * fim4 / 720));" +
                $"Y = [Y y];" +
                $"fim4 = fim3;" +
                $"fim3 = fim2;" +
                $"fim2 = fim1;" +
                $"fim1 = fi;" +
                $"end");
        }

        private AdamsMulton() { }

        public AdamsMulton(int a, int b, double h)
        {
            A = a;
            B = b;
            H = h;
        }
    }
}
