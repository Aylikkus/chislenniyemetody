namespace SNU.Math
{
    public class CustomEquation : INonLinearEquation
    {
        private Coefficient[] coefficients;

        public List<Coefficient> Coefficients
        {
            get
            {
                return coefficients.ToList();
            }
        }

        public double Solve(double x) 
        {
            double result = 0;
            for (int i = coefficients.Length - 1; i >= 0; i--)
            {
                result += System.Math.Pow(coefficients[i].Value, coefficients[i].Degree);
            }

            return result;
        }

        public CustomEquation(Coefficient[] coefs)
        {
            coefficients = coefs;
        }
    }
}