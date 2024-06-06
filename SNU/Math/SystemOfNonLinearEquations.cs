using SNU.Methods;

namespace SNU.Math
{
    public class SystemOfNonLinearEquations
    {
        private List<INonLinearEquation> equations;

        public double[] SolveFPI(FixedPointIteration method)
        {
            List<double> results = new List<double>(equations.Count);

            foreach (var equation in equations)
                results.Add(method.Solve(equation));

            return results.ToArray();
        }

        public double[][] SolveGS(GaussSeidelMethod method)
        {
            List<double[]> results = new List<double[]>(equations.Count);

            foreach (var equation in equations)
                results.Add(method.Solve(equation));

            return results.ToArray();
        }

        public double[] SolveN(NewtonMethod method, Func<double, double> derivative)
        {
            List<double> results = new List<double>(equations.Count);

            foreach (var equation in equations)
                results.Add(method.Solve(equation, 0.01, derivative, 1000000));

            return results.ToArray();
        }

        public double[] SolveCC(CuttingChords method)
        {
            List<double> results = new List<double>(equations.Count);

            foreach (var equation in equations)
                results.Add(method.Solve(equation, 0.01, 1000000));

            return results.ToArray();
        }

        public SystemOfNonLinearEquations(List<INonLinearEquation> equations)
        {
            int n = equations[0].Coefficients.Count;

            foreach (var equation in equations)
            {
                if (n != equation.Coefficients.Count)
                    throw new Exception("Количество коэфициентов должно быть одинаковым");
            }

            this.equations = equations;
        }
    }
}