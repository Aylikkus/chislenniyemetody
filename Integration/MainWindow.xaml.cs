using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Integration
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
            dataModel = new DataModel(chart1, new Chebushev());
            chart1.AnimationsSpeed = new TimeSpan(1000);

            chart1.MouseDown += Chart1_MouseDown;
            chart1.MouseMove += Chart1_MouseMove;
            chart1.MouseUp += Chart1_MouseUp;
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

        private void chart1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            dataModel.Draw(getFunction());
        }

        private IIntegrationMethod getMethod(string text)
        {
            IIntegrationMethod method = null;

            switch (text)
            {
                case "Прямоугольный (нижняя граница)":
                    method = new RectangleLower();
                    break;
                case "Прямоугольный (средняя граница)":
                    method = new RectangleMiddle();
                    break;
                case "Прямоугольный (верхняя граница)":
                    method = new RectangleUpper();
                    break;
                case "Трапеций":
                    method = new Trapezoid();
                    break;
                case "Парабол":
                    method = new Parabola();
                    break;
                case "Сплайнов":
                    method = new Splines();
                    break;
                case "Монте-Карло":
                    method = new MonteCarlo();
                    break;
                case "Гаусса":
                    method = new Gauss();
                    break;
                case "Чебышёва":
                default:
                    method = new Chebushev();
                    break;
            }

            return method;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataModel.IntegrationMethod = getMethod(
                (string)((ComboBoxItem)e.AddedItems[0]).Content);
            dataModel.Draw(getFunction());
        }

        private void Function_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataModel.Draw(getFunction());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(dataModel.Integrate(getFunction()).ToString());
        }

        private void validateRange()
        {
            double.TryParse(a.Text, out double resA);
            double.TryParse(b.Text, out double resB);
            double.TryParse(step.Text, out double resStep);

            if (resA < resB)
            {
                dataModel.A = resA;
                dataModel.B = resB;
                dataModel.Step = resStep;

                dataModel.Draw(getFunction());
            }
        }

        private void a_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateRange();
        }

        private void b_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateRange();
        }

        private void step_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateRange();
        }
    }
}
