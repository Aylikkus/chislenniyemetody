﻿using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using StringMath;

namespace Differentiation
{
    public class DataModel
    {
        private LineSeries functionSeries;
        private LineSeries differentialSeries;
        public CartesianChart CartesianChart { get; set; }
        public IDifferentiationMethod DifferentiationMethod { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double Step { get; set; }
        public int Degree { get; set; }

        public DataModel(CartesianChart chart, IDifferentiationMethod method)
        {
            CartesianChart = chart;
            chart.Series = new SeriesCollection();
            functionSeries = new LineSeries
            {
                Fill = Brushes.Transparent,
                LineSmoothness = 0,
                StrokeThickness = 1,
                Stroke = Brushes.Red,
                Values = new ChartValues<ObservablePoint>()
            };
            CartesianChart.Series.Add(functionSeries);
            differentialSeries = new LineSeries
            {
                Fill = Brushes.Transparent,
                LineSmoothness = 0,
                StrokeThickness = 1,
                Stroke = Brushes.Black,
                Values = new ChartValues<ObservablePoint>()
            };
            CartesianChart.Series.Add(differentialSeries);
            DifferentiationMethod = method;
        }

        internal void Draw(string function)
        {
            if (Step <= 0) { return; }

            try
            {
                function.Substitute("x", 0);
            }
            catch (MathException) { return; }

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
            functionSeries.Values.Clear();
            functionSeries.Values.AddRange(points);

            List<ObservablePoint> diffs = 
                DifferentiationMethod.Differentiate(A, B, Step, Degree, function);
            differentialSeries.Values.Clear();
            differentialSeries.Values.AddRange(diffs);
        }
    }
}
