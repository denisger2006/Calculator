using Own_Language_Course.Parsing.Visitors;
using Own_Language_Course.Parsing.AST.Expressions;

namespace Own_Language_Course.Parsing.AST.Statements
{
    public class DoWhileStatement : IStatement
    {
        public readonly IExpression condition;
        public readonly IStatement statement;

        public DoWhileStatement(IExpression condition, IStatement statement)
        {
            this.condition = condition;
            this.statement = statement;
        }
        public void Execute()
        {
            do
            {
                try
                {
                    statement.Execute();
                }
                catch (BreakStatement) { break; }
                catch (ContinueStatement) { continue; }
            }
            while (condition.Eval().AsNumber() != 0);

        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
            => $"do {statement} while {condition}";
    }
}