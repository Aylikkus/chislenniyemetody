using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Solvers
{
    public class Adams : IODESolver
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
                $"dfi = fi - fim1;" +
                $"d2fi = fi - (2 * fim1) + fim2;" +
                $"d3fi = fi - (3 * fim1) + (3 * fim2) - fim3;" +
                $"y = y + ({h} * fi) + (({h}^2 / 2) * dfi) + ((5 * {h}^3 / 12) * d2fi) + ((3 * {h}^4 / 8) * d3fi);" +
                $"Y = [Y y];" +
                $"fim3 = fim2;" +
                $"fim2 = fim1;" +
                $"fim1 = fi;" +
                $"end");
        }

        private Adams() { }

        public Adams(int a, int b, double h)
        {
            A = a;
            B = b;
            H = h;
        }
    }
}
