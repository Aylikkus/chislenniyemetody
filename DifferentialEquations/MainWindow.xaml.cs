using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Model;
using Model.Solvers;

namespace DifferentialEquations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Context context = new Context();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void solveBtn_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(aTb.Text, out int a);
            int.TryParse(bTb.Text, out int b);
            double.TryParse(hTb.Text, out double h);

            IODESolver solver;

            switch (solverCb.Text)
            {
                case "Эйлер (пересчёт)":
                    solver = new EulerRecount(a, b, h);
                    break;
                case "Эйлер (итерационный)":
                    solver = new EulerIterational(a, b, h);
                    break;
                case "Эйлер (улучшенный)":
                    solver = new EulerAdvanced(a, b, h);
                    break;
                case "Рунге-Кутта (третий порядок)":
                    solver = new RungeKuttThird(a, b, h);
                    break;
                case "Рунге-Кутта (четвёртый порядок)":
                    solver = new RungeKuttFourth(a, b, h);
                    break;
                case "Адамс (четыре узла)":
                    solver = new Adams(a, b, h);
                    break;
                case "Адамс-Башфорт":
                    solver = new AdamsBashforth(a, b, h);
                    break;
                case "Адамс-Мультон":
                    solver = new AdamsMulton(a, b, h);
                    break;
                case "Эйлер":
                default:
                    solver = new Euler(a, b, h);
                    break;
            }

            double.TryParse(fxTb.Text, out double y0);

            context.SetFunc(fxTb.Text);
            context.Solver = solver;
            context.SolveODE(y0);
        }
    }
}