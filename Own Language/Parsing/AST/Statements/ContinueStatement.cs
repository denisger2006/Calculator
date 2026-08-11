using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Statements
{
    public class ContinueStatement : Exception, IStatement
    {
        public void Execute() => throw this;
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString() => "continue";
    }
}
