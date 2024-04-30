using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Differentiation
{
    /// <summary>
    /// Логика взаимодействия для ReferenceWindow.xaml
    /// </summary>
    public partial class ReferenceWindow : Window
    {
        struct Entry
        {
            public string name { get; set; }
            public double first { get; set; }
            public double firstDiff { get; set; }
            public double second { get; set; }
            public double secondDiff { get; set; }
            public double third { get; set; }
            public double thirdDiff { get; set; }

            public Entry(string name, 
                double first, double firstDiff, 
                double second, double secondDiff,
                double third, double thirdDiff)
            {
                this.name = name;
                this.first = first;
                this.firstDiff = firstDiff;
                this.second = second;
                this.secondDiff = secondDiff;
                this.third = third;
                this.thirdDiff = thirdDiff;
            }
        }
        public ReferenceWindow()
        {
            InitializeComponent();
            table.Items.Add(getAnalytical());
            table.Items.Add(getFromMethod("Левой границы", new LeftDiff()));
            table.Items.Add(getFromMethod("Правой границы", new RightDiff()));
            table.Items.Add(getFromMethod("Центральной границы", new CentralDiff()));
            table.Items.Add(getFromMethod("Линейной интерполяции", new Linear()));
            table.Items.Add(getFromMethod("Квадратической интерполяции", new Square()));
            table.Items.Add(getFromMethod("Кубической интерполяции", new Cubic()));
            table.Items.Add(getFromMethod("Многочлены Ньютона", new Newton()));
        }

        Entry getAnalytical()
        {
            return new Entry("Аналитический", 1.92, 0, 2.43, 0, 3, 0);
        }

        Entry getFromMethod(string name, IDifferentiationMethod method)
        {
            List<ObservablePoint> res = method.Differentiate(0.8, 1, 0.1, 1, "{x}^3");

            if (res.Count != 3) return new Entry();

            return new Entry(name, 
                res[0].Y, res[0].Y - 1.92, 
                res[1].Y, res[1].Y - 2.43, 
                res[2].Y, res[2].Y - 3);
        }
    }
}
