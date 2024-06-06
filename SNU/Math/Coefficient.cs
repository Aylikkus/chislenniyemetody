namespace SNU.Math
{
    public struct Coefficient
    {
        public int Degree { get; set; }
        public double Value { get; set; }

        public Coefficient(int degree, double value)
        {
            Degree = degree;
            Value = value;
        }
    }
}