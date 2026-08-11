using Own_Language_Course.Parsing.AST.Statements;

namespace Own_Language_Course.Parsing.Visitors
{
    public class FunctionAdder :AbstractVisitor
    {
        public override void Visit(FunctionDefineStatement s)
        {
            base.Visit(s);
            s.Execute();
        }
    }
}
