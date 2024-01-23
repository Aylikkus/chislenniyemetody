using LiveCharts;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace Interpolation
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataModel dataModel;
        private bool isMouseDragging = false;
        private int radius = 10;

        private double func(double x)
        {
            return Math.Sin(x);
        }

        public MainWindow()
        {
            InitializeComponent();
            chart1.AxisX[0].MaxValue = radius;
            chart1.AxisY[0].MaxValue = radius;
            chart1.AxisX[0].MinValue = -radius;
            chart1.AxisY[0].MinValue = -radius;
            dataModel = new DataModel(chart1, new Chebushev(0));
            dataModel.Function = new Function(func);
            dataModel.Draw();
            chart1.AnimationsSpeed = new TimeSpan(1000);

            chart1.MouseDown += Chart1_MouseDown;
            chart1.MouseMove += Chart1_MouseMove;
            chart1.MouseUp += Chart1_MouseUp;
        }

        private void Chart1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDragging = true;
            dataModel.Draw();
        }

        private void Chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDragging)
            {
                dataModel.Draw();
            }
        }

        private void Chart1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDragging = false;
            dataModel.Draw();
        }

        private void chart1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            dataModel.Draw();
        }

        private IInterpolationMethod getMethod(string text, int degree)
        {
            IInterpolationMethod method = null;

            switch (text)
            {
                case "Линейная":
                    method = new Linear();
                    break;
                case "Квадратичная":
                    method = new Quadratic();
                    break;
                case "Кубический сплайн":
                    method = new QubicSpline();
                    break;
                case "Многочлен Лангранжа":
                    method = new Lagranj();
                    break;
                case "Многочлен Ньютона":
                    method = new Newton();
                    break;
                case "Метод наименьших квадратов":
                    method = new LeastSquares(degree);
                    break;
                case "Многочлены Чебышева":
                default:
                    method = new Chebushev(degree);
                    break;
            }

            return method;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int.TryParse(textBox.Text, out int number);
            dataModel.InterpolationMethod = getMethod(
                (string)((ComboBoxItem)e.AddedItems[0]).Content, 
                number);
            dataModel.Draw();
        }

        private static readonly Regex _nums = new Regex("[^0-9]+");

        private static bool isNumbersText(string text)
        {
            return _nums.IsMatch(text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (isNumbersText(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(textBox.Text, out int number);
            dataModel.InterpolationMethod = getMethod(
                (string)((ComboBoxItem)comboBox.SelectedValue).Content,
                number);
            dataModel.Draw();
        }
    }
}
