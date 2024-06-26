﻿using LiveCharts.Defaults;
using StringMath;
using System;
using System.Collections.Generic;

namespace Differentiation
{
    internal class RightDiff : IDifferentiationMethod
    {
        public List<ObservablePoint> Differentiate(double a, double b, double step, int degree, string function)
        {
            List<ObservablePoint> points = new List<ObservablePoint>();
            for (double x = a; x <= b; x += step)
            {
                try
                {
                    double diff = 0;
                    for (int i = 0; i <= degree; i++)
                    {
                        double C_im = Utility.GetBinCoef(degree, i);
                        double fxkh = function.Substitute("x", x + (i * step)).Result;
                        diff += Math.Pow(-1, degree - i) * C_im * fxkh;
                    }
                    points.Add(new ObservablePoint(x, diff / Math.Pow(step, degree)));
                }
                catch { break; }
            }

            return points;
        }

        public static double FiniteDiff(double x, double step, int degree, string function)
        {
            double diff = 0;
            for (int i = 0; i <= degree; i++)
            {
                double C_im = Utility.GetBinCoef(degree, i);
                double fxkh = function.Substitute("x", x + (i * step)).Result;
                diff += Math.Pow(-1, degree - i) * C_im * fxkh;
            }
            return diff;
        }

        public ObservablePoint DifferentiatePoint(double x, double step, int degree, string function)
        {
            return Differentiate(x, x, step, degree, function)[0];
        }
    }
}