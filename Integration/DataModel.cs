using LiveCharts.Defaults;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using StringMath;

namespace Integration
{
    public class DataModel
    {
        private LineSeries lineSeries;
        public CartesianChart CartesianChart { get; set; }
        public IIntegrationMethod IntegrationMethod { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double Step { get; set; }

        public DataModel(CartesianChart chart, IIntegrationMethod method)
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
            IntegrationMethod = method;
        }

        public void Draw(string function)
        {
            if (Step <= 0) { return; }

            List<ObservablePoint> points = new List<ObservablePoint>();
            for (double x = A; x <= B; x += Step)
            {
                try
                {
                    double y = function.Substitute("x", x).Result;
                    points.Add(new ObservablePoint(x, y));
                }
                catch { break; }
            }
            lineSeries.Values.Clear();
            lineSeries.Values.AddRange(points);
        }

        public double Integrate(string function)
        {
            return IntegrationMethod.Integrate(function, A, B, Step);
        }
    }

}