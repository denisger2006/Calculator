using System;

namespace Own_Language_Course.Lib
{
    public class ArrayValue : IValue
    {
        private readonly IValue[] elements;
        public IValue this[int index]
        {
            get => elements[index];
            set => elements[index] = value;
        }
        public ArrayValue(int size)
        {
            elements = new IValue[size];
        }
        public ArrayValue(IValue[] elements)
        {
            this.elements = new IValue[elements.Length];
            Array.Copy(elements, 0, this.elements, 0, elements.Length);
        }
        public ArrayValue(ArrayValue array) :
            this(array.elements)
        { }
        public double AsNumber()
        {
            ErrorHandler.ThrowRuntimeError("Нельзя привести массив к числу.");
            return 0; 
        }
        public string AsString()
        {
            return "[" + string.Join(", ", elements) + "]";
        }
        public override string ToString()
            => AsString();
    }
}