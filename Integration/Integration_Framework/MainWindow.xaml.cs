using LiveCharts.Wpf.Charts.Base;
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
using Integration;

namespace Integration_Framework
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
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
            dataModel = new DataModel(chart1, new Chebushev(0));
            chart1.AnimationsSpeed = new TimeSpan(1000);

            chart1.MouseDown += Chart1_MouseDown;
            chart1.MouseMove += Chart1_MouseMove;
            chart1.MouseUp += Chart1_MouseUp;
            additionalBlock.IsEnabled = false;
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
            additionalText.Text = "";
            additionalBlock.IsEnabled = false;
            switch (text)
            {
                case "Прямоугольный (левая граница)":
                    method = new RectangleLeft();
                    break;
                case "Прямоугольный (средняя граница)":
                    method = new RectangleMiddle();
                    break;
                case "Прямоугольный (правая граница)":
                    method = new RectangleRight();
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
                    method = new MonteCarlo(int.TryParse(additionalBlock.Text, out int result) ? result : 0);       
                    additionalBlock.IsEnabled = true;
                    additionalText.Text = "Точки";
                    break;
                case "Гаусса":
                    method = new Gauss(int.TryParse(additionalBlock.Text, out int gaussNodesCount) ? gaussNodesCount : 0);
                    additionalBlock.IsEnabled = true;
                    additionalText.Text = "Узлы";
                    break;
                case "Чебышёва":
                    method = new Chebushev(int.TryParse(additionalBlock.Text, out int chebushevNodesCount) ? chebushevNodesCount : 0);
                    additionalBlock.IsEnabled = true;
                    additionalText.Text = "Узлы";
                    break;
                default:
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
            dataModel.IntegrationMethod = getMethod((string)comboBox.Text);
            MessageBox.Show(dataModel.Integrate(getFunction()).ToString());
        }

        private void validateRange()
        {
            double.TryParse(a.Text, out double resA);
            double.TryParse(b.Text, out double resB);
            double.TryParse(step.Text, out double resStep);
            int.TryParse(additionalBlock.Text, out int resAdditionalField);

            if (resA < resB)
            {
                dataModel.A = resA;
                dataModel.B = resB;
                dataModel.Step = resStep;
                dataModel.AdditionalField = resAdditionalField;

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

        private void additionalBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateRange();
        }
    }
}
