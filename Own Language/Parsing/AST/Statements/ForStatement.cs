using Own_Language_Course.Parsing.AST.Expressions;
using Own_Language_Course.Parsing.Visitors;


namespace Own_Language_Course.Parsing.AST.Statements
{
    public class ForStatement : IStatement
    {
        public readonly IStatement initialization;
        public readonly IExpression termination;
        public readonly IStatement increment;
        public readonly IStatement statement;

        public ForStatement(IStatement initialization, IExpression termination, IStatement increment, IStatement statement)
        {
            this.initialization = initialization;
            this.termination = termination;
            this.increment = increment;
            this.statement = statement;
        }
        public void Execute()
        {
            for (initialization.Execute(); termination.Eval().AsNumber() != 0; increment.Execute())
            {
                try
                {
                    statement.Execute();
                }
                catch(BreakStatement) { break; }
                catch (ContinueStatement) { continue; }
            }
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
            => $"for {initialization}, {termination}, {increment}, {statement}";
    }
}
