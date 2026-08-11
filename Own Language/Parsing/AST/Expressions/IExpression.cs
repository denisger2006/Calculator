using Own_Language_Course.Lib;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Expressions
{
    public interface IExpression : INode
    {
        IValue Eval();
    }
}
