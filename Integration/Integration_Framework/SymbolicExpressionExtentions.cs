using MathNet.Numerics;
using MathNet.Symbolics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration_Framework
{
    internal static class SymbolicExpressionExtentions
    {
        // Производная
        public static double Derivative(this SymbolicExpression function, double valueX, int order)
        {
            return Differentiate.Derivative(x => function.EvaluateX(x), valueX, order);
        }

        // Вычисление по X
        public static double EvaluateX(this SymbolicExpression function, double valueX)
        {
            return function.Evaluate(new Dictionary<string, FloatingPoint>() { { "x", valueX } }).RealValue;
        }
    }
}
