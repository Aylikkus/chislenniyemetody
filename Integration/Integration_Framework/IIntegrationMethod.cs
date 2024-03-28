

using System.Windows.Media.Animation;

namespace Integration
{
    public interface IIntegrationMethod
    {
        double Integrate(string function, double a, double b, double step);
        
    }

}