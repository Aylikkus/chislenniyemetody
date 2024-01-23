using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Windows.Media;

namespace Interpolation
{
    public class DataModel
    {
        private LineSeries lineSeries;
        public CartesianChart CartesianChart { get; set; }
        public Function Function { get; set; }
        public IInterpolationMethod InterpolationMethod { get; set; }

        public DataModel(CartesianChart chart, IInterpolationMethod method)
        {
            CartesianChart = chart;
            chart.Series = new SeriesCollection();
            lineSeries = new LineSeries
            {
                Fill = Brushes.Transparent,
                LineSmoothness = 0,
                StrokeThickness = 1,
                Stroke = Brushes.Red,
                Values = new ChartValues<ObservablePoint>()
            };
            CartesianChart.Series.Add(lineSeries);
            InterpolationMethod = method;
        }

        public void Draw()
        {
            /*double x1 = CartesianChart.AxisX[0].MinValue;
            double x2 = CartesianChart.AxisX[0].MaxValue;*/
            double x1 = -5;
            double x2 = 5;
            double step = 0.05;
            List<ObservablePoint> interpolationPoints = Function.Interpolate(
                InterpolationMethod, x1, x2, step);
            lineSeries.Values.Clear();
            lineSeries.Values.AddRange(interpolationPoints);
        }
    }
}
