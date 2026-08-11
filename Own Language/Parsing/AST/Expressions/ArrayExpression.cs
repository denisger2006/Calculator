using Own_Language_Course.Lib;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Expressions
{
    public class ArrayExpression : IExpression
    {
        public readonly List<IExpression> elements;

        public ArrayExpression(List<IExpression> elements)
        {
            this.elements = elements;
        }
        public IValue Eval()
        {
            int size = elements.Count;
            var array = new ArrayValue(size);

            for (int i = 0; i < size; i++) 
                array[i] = elements[i].Eval();

            return array;
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
        {
            return $"[{elements}]";
        }
    }
}
