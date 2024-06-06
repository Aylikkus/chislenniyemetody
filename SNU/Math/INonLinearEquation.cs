namespace SNU.Math
{
    public interface INonLinearEquation
    {
        List<Coefficient> Coefficients { get; }

        double Solve(double x);
    }
}