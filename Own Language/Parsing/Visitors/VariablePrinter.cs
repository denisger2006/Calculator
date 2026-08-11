using Own_Language_Course.Parsing.AST.Expressions;
using Own_Language_Course.Parsing.AST.Statements;

namespace Own_Language_Course.Parsing.Visitors
{ 
    public class VariablePrinter : AbstractVisitor
    {
        public override void Visit(AssignmentStatement s)
        {
            base.Visit(s);
            Console.WriteLine(s.variable);
        }
        public override void Visit(VariableExpression s) 
        {
            Console.WriteLine(s.name);
        }
    }
}
