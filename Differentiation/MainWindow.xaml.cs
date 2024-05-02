using LiveCharts.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Differentiation
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataModel dataModel; 
        private bool isMouseDragging = false;

        public MainWindow()
        {
            InitializeComponent();
            dataModel = new DataModel(chart1, new LeftDiff());
            chart1.AnimationsSpeed = new TimeSpan(1000);

            chart1.MouseDown += Chart1_MouseDown;
            chart1.MouseMove += Chart1_MouseMove;
            chart1.MouseUp += Chart1_MouseUp;
        }

        private IDifferentiationMethod getMethod(string text)
        {
            IDifferentiationMethod method = null;
            switch (text)
            {
                case "Левая разность":
                    method = new LeftDiff();
                    degree.IsEnabled = true;
                    break;
                case "Правая разность":
                    method = new RightDiff();
                    degree.IsEnabled = true;
                    break;
                case "Центральная разность":
                    method = new CentralDiff();
                    degree.IsEnabled = true;
                    break;
                case "Линейная интерполяция":
                    method = new Linear();
                    degree.IsEnabled = true;
                    break;
                case "Квадратичная интерполяция":
                    method = new Square();
                    degree.IsEnabled = true;
                    break;
                case "Кубическая интерполяция":
                    method = new Cubic();
                    degree.IsEnabled = true;
                    break;
                case "Многочлен Ньютона":
                    method = new Newton();
                    degree.IsEnabled = true;
                    break;
                case "Метод неопределённых коэффициентов":
                    method = new MNK();
                    degree.IsEnabled = true;
                    break;
                default:
                    break;
            }

            return method;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (degree == null) return;

            dataModel.DifferentiationMethod = getMethod(
                (string)((ComboBoxItem)e.AddedItems[0]).Content);
            dataModel.Draw(getFunction());
        }

        private void validateRange()
        {
            double.TryParse(a.Text, out double resA);
            double.TryParse(b.Text, out double resB);
            double.TryParse(step.Text, out double resStep);
            int.TryParse(degree.Text, out int resDegree);

            if (resDegree < 1) resDegree = 1;

            if (resA <= resB)
            {
                dataModel.A = resA;
                dataModel.B = resB;
                dataModel.Step = resStep;
                dataModel.Degree = resDegree;

                dataModel.Draw(getFunction());
            }
        }

        private void step_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateRange();
        }

        private void degree_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateRange();
        }

        private void a_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateRange();
        }

        private void b_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateRange();
        }

        private void Function_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataModel.Draw(getFunction());
        }

        private void chart1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            dataModel.Draw(getFunction());
        }

        private string getFunction()
        {
            return function.Text.Replace("x", "{x}");
        }

        private void Chart1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDragging = true;
            dataModel.Draw(getFunction());
        }

        private void Chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDragging)
            {
                dataModel.Draw(getFunction());
            }
        }

        private void Chart1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDragging = false;
            dataModel.Draw(getFunction());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReferenceWindow referenceWindow = new ReferenceWindow(dataModel, getFunction());
            referenceWindow.Show();
        }

        private void Point_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(point.Text, out double x))
                dataModel.X = x;
            else
                dataModel.X = null;
            dataModel.Draw(getFunction());
        }
    }
}
