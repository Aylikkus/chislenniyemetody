using LiveCharts.Defaults;

namespace Interpolation
{
    internal class LeastSquares : IInterpolationMethod
    {
        private int degree;

        public List<ObservablePoint> Interpolate(List<ObservablePoint> values)
        {
            return new List<ObservablePoint>();
        }

        public LeastSquares(int degree)
        {
            this.degree = degree;
        }
    }
}