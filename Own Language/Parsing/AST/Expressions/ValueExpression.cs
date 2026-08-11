using Own_Language_Course.Lib;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Expressions
{
    public class ValueExpression : IExpression
    {
        public readonly IValue value;
        public ValueExpression(double value) 
            =>  this.value = new NumberValue(value);
        public ValueExpression(string value)
            => this.value = new StringValue(value);
        public IValue Eval() => value;
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
            => value.AsString();
    }
}
