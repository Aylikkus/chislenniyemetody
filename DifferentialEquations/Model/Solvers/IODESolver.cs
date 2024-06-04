using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Solvers
{
    public interface IODESolver
    {
        void Solve();

        MLApp.MLApp? MatLab { get; set; }
    }
}
