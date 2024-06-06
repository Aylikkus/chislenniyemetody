namespace SNU.Math
{
    public class PolynomEquation : INonLinearEquation
    {
        private double[] coefficients;

        public List<Coefficient> Coefficients
        {
            get
            {
                int degree = coefficients.Length - 1;
                List<Coefficient> result = new List<Coefficient>(coefficients.Length);
                for (int i = 0; i < coefficients.Length; i++)
                {
                    result.Add(new Coefficient(degree--, coefficients[i]));
                }

                return result;
            }
        }

        public double Solve(double x) 
        {
            int degree = 0;
            double result = 0;
            for (int i = coefficients.Length - 1; i >= 0; i--)
            {
                result += System.Math.Pow(coefficients[i], degree++);
            }

            return result;
        }

        // Like: 2x^2 - x + 2 = 0
        public PolynomEquation(double[] coefs)
        {
            coefficients = coefs;
        }
    }
}