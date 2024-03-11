using System.Reflection.Metadata.Ecma335;

namespace Integration
{
    public interface IIntegrationMethod
    {
        double Integrate(string function, double a, double b, double step);
    }
}