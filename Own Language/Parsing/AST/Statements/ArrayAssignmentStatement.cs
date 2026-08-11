using Own_Language_Course.Parsing.AST.Expressions;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Statements
{
    public class ArrayAssignmentStatement : IStatement
    {
        public readonly ArrayAccessExpression array;
        public readonly IExpression expression;

        public ArrayAssignmentStatement(ArrayAccessExpression array, IExpression expression)
        {
            this.array = array;
            this.expression = expression;
        }
        public void Execute()
        {
            array.GetArray()[array.LastIndex] = expression.Eval();
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
            => $"{array} = {expression}";
    }
}
