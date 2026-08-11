using Own_Language_Course.Parsing.AST.Expressions;
using VR = Own_Language_Course.Lib.Variable;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Statements
{
    public class AssignmentStatement : IStatement
    {
        public readonly string variable;
        public readonly IExpression expression;

        public AssignmentStatement(string variable, IExpression expression)
        {
            this.variable = variable;
            this.expression = expression;
        }
        public void Execute()
        {
            var result = expression.Eval();
            VR.Set(variable, result);
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
            => $"{variable} = {expression}";
    }
}
