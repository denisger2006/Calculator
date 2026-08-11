using Own_Language_Course.Lib;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Expressions
{
    public class ArrayAccessExpression : IExpression
    {
        public readonly string variable;
        public readonly List<IExpression> indexes;
        public int LastIndex => Index(indexes.Count - 1);
        public ArrayAccessExpression(string variable, List<IExpression> indexes)
        {
            this.variable = variable;
            this.indexes = indexes;
        }
        public IValue Eval()
        {
            return GetArray()[LastIndex];
        }
        public ArrayValue GetArray()
        {
            var array = ConsumeArray(Variable.GetVariable(variable));
            int last = indexes.Count - 1;
            for (int i = 0; i < last; i++)
            {
                array = ConsumeArray(array[Index(i)]);
            }
            return array;
        }
        private int Index(int index)
            => (int)indexes[index].Eval().AsNumber();

        private ArrayValue? ConsumeArray(IValue value)
        {
            if (value is ArrayValue arrayValue)
            {
                return arrayValue;
            }
            else
            {
                ErrorHandler.ThrowRuntimeError("Ожидается массив.");
                return null;
            }
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
            => $"{variable}{indexes}";
    }
}