using System.Globalization;

namespace Own_Language_Course.Lib
{
    public class StringValue : IValue
    {
        private readonly string value;

        public StringValue(string value)
        {
            this.value = value;
        }
        public double AsNumber()
        {
            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double parsePes))
                return parsePes;
            else return 0;
        }
        public string AsString() => value;
        public override string ToString() => AsString();
    }
}
