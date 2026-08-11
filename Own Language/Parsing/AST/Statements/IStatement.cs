using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Statements
{
    public interface IStatement : INode
    {
        void Execute();
    }
}
