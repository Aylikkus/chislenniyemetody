using LiveCharts.Defaults;
using Rychusoft.NumericalLibraries.Calculator;
using Rychusoft.NumericalLibraries.Derivative;
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
        DataModel dataModel;
        string function;
        double[] analyticalVals;

        struct Entry
        {
            public string name { get; set; }
            public double[] vals { get; set; }
            public double[] diffs { get; set; }

            public Entry(string name, double[] vals, double[] diffs)
            {
                this.name = name;
                this.vals = vals;
                this.diffs = diffs;
            }
        }
        public ReferenceWindow(DataModel dataModel, string function)
        {
            InitializeComponent();

            if (dataModel.Step == 0) return;

            this.dataModel = dataModel;
            this.function = function;

            initializeDataGrid();

            table.Items.Add(getAnalytical());
            table.Items.Add(getFromMethod("Левой границы", new LeftDiff()));
            table.Items.Add(getFromMethod("Правой границы", new RightDiff()));
            table.Items.Add(getFromMethod("Центральной границы", new CentralDiff()));
            table.Items.Add(getFromMethod("Линейной интерполяции", new Linear()));
            table.Items.Add(getFromMethod("Квадратической интерполяции", new Square()));
            table.Items.Add(getFromMethod("Кубической интерполяции", new Cubic()));
            table.Items.Add(getFromMethod("Многочлены Ньютона", new Newton()));
            table.Items.Add(getFromMethod("Метод Неопределённых Коэффициентов", new MNK()));
        }

        void initializeDataGrid()
        {
            DataGridTextColumn name = new DataGridTextColumn();
            name.Header = "Название";
            name.Binding = new Binding("name");
            table.Columns.Add(name);
            double x = dataModel.A;
            for (int i = 0; x <= dataModel.B; x += dataModel.Step, i++)
            {
                var num = new DataGridTextColumn();
                num.Header = x.ToString();
                Binding numBinding = new Binding($"vals[{i}]");
                numBinding.StringFormat = "{0:F}";
                num.Binding = numBinding;
                table.Columns.Add(num);

                var diff = new DataGridTextColumn();
                Binding diffBinding = new Binding($"diffs[{i}]");
                diffBinding.StringFormat = "{0:F}";
                diff.Binding = diffBinding;
                table.Columns.Add(diff);
            }
        }

        int getNumCount()
        {
            return (int)(Math.Abs(dataModel.A - dataModel.B) / dataModel.Step) + 1;
        }

        Entry getAnalytical()
        {
            var d = new Derivative(function.Replace('{', ' ').Replace('}', ' '));
            double[] vals = new double[getNumCount()];
            double[] diffs = new double[getNumCount()];
            double x = dataModel.A;
            for (int i = 0; x <= dataModel.B; x += dataModel.Step, i++)
            {
                vals[i] = d.ComputeDerivative(x);
            }
            analyticalVals = vals;
            return new Entry("Rychusoft.NumericalLibraries.Derivative", vals, diffs);
        }

        Entry getFromMethod(string name, IDifferentiationMethod method)
        {
            var points = method.Differentiate(dataModel.A, dataModel.B, dataModel.Step, 1, function);
            double[] vals = new double[getNumCount()];
            double[] diffs = new double[getNumCount()];
            double x = dataModel.A;
            for (int i = 0; x <= dataModel.B; x += dataModel.Step, i++)
            {
                vals[i] = points[i].Y;
                diffs[i] = analyticalVals[i] - points[i].Y;
            }
            return new Entry(name, vals, diffs);
        }
    }
}
