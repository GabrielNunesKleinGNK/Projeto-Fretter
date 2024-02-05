namespace System
{
    public class Percent
    {
        public float Value { get; set; }

        public Percent(float value)
        {
            Value = value;
        }

        public static float operator -(float x, Percent y) => x - CalcPercent(x, y);
        public static float operator +(float x, Percent y) => x + CalcPercent(x, y);
        private static float CalcPercent(float x, Percent y) => (x * (y.Value / 100));
    }
}