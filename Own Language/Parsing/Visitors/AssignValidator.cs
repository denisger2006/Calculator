using Own_Language_Course.Lib;
using Own_Language_Course.Parsing.AST.Statements;

namespace Own_Language_Course.Parsing.Visitors
{
    public class AssignValidator : AbstractVisitor
    {
        public override void Visit(AssignmentStatement s)
        {
            base.Visit(s);
            if (Variable.IsExists(s.variable))
            {
                ErrorHandler.ThrowRuntimeError($"Нельзя переопределить константу '{s.variable}'.");
            }
        }
    }
}