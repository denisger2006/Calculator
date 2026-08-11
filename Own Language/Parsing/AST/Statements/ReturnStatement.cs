using Own_Language_Course.Lib;
using Own_Language_Course.Parsing.AST.Expressions;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Statements
{
    public class ReturnStatement : Exception, IStatement
    {
        private readonly IExpression expression;
        private IValue result;
        public IValue Result => result;

        public ReturnStatement(IExpression expression)
        {
            this.expression = expression;
        }
        public void Execute()
        {
            result = expression.Eval();
            throw this;
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
            => "return";
    }
}
