using Own_Language_Course.Parsing.AST.Expressions;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Statements
{
    public class FunctionStatement : IStatement
    {
        public readonly FunctionalExpression function;

        public FunctionStatement(FunctionalExpression function)
        {
            this.function = function;
        }
        public void Execute()
        {
            function.Eval();
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
        {
            return function.ToString();
        }
    }
}
