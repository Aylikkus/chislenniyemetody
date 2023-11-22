using System.Collections.Generic;
using SLAULIB.SLAU;

namespace SLAULIB.SolveMethods
{
    public interface ISolveMethod
    {
        List<double> Solve(SystemOfLinearEquations system);
    }
}
