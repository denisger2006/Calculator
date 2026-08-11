namespace Own_Language_Course.Lib
{
    public class NumberValue : IValue
    {
        public static readonly NumberValue ZERO = new(0);
        private readonly double value;
        public NumberValue(bool value)
        {
            this.value = value ? 1 : 0;
        }
        public NumberValue(double value)
        {
            this.value = value;
        }
        public double AsNumber() => value;
        public string AsString() => value.ToString();
        public override string ToString() => AsString();
    }
}
