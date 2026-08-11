using Own_Language_Course.Parsing.AST.Expressions;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Statements
{
    public class WhileStatement : IStatement
    {
        public readonly IExpression condition;
        public readonly IStatement statement;

        public WhileStatement(IExpression condition, IStatement statement)
        {
            this.condition = condition;
            this.statement = statement;
        }
        public void Execute()
        {
            while(condition.Eval().AsNumber() != 0)
            {
                try
                {
                    statement.Execute();
                }
                catch (BreakStatement) { break; }
                catch (ContinueStatement) { continue; }
            }
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
            => $"while {condition} {statement}";
    }
}
