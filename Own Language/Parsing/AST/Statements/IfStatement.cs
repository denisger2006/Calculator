using Own_Language_Course.Parsing.AST.Expressions;
using Own_Language_Course.Parsing.Visitors;
using System.Text;

namespace Own_Language_Course.Parsing.AST.Statements
{
    public class IfStatement : IStatement
    {
        public readonly IExpression expression;
        public readonly IStatement ifStatement, elseStatement;

        public IfStatement(IExpression expression, IStatement ifStatement, IStatement elseStatement)
        {
            this.expression = expression;
            this.ifStatement = ifStatement;
            this.elseStatement = elseStatement;
        }
        public void Execute()
        {
            double result = expression.Eval().AsNumber();

            if (result != 0)
                ifStatement.Execute();
            else elseStatement?.Execute();
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("if ").Append(expression).Append(ifStatement);
            if (elseStatement != null)
                result.Append("\nelse").Append(elseStatement);
            return result.ToString();
        }
    }
}
