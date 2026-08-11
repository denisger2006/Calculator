using Own_Language_Course.Lib;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Expressions
{
    public class UnaryExpression : IExpression
    {
        public readonly char operation;
        public readonly IExpression expr;

        public UnaryExpression(char operation, IExpression expr)
        {
            this.operation = operation;
            this.expr = expr;
        }
        public IValue Eval() => operation switch
        {
            '-' => new NumberValue(-expr.Eval().AsNumber()),
            '+' => expr.Eval(),
            _ => throw new Exception("Неверный унарный оператор")
        };
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
            => $"{operation} {expr}";
    }
}
