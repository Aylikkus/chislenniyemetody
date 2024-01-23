using LiveCharts.Defaults;

namespace Interpolation
{
    public interface IInterpolationMethod
    {
        public List<ObservablePoint> Interpolate(List<ObservablePoint> values);
    }
}