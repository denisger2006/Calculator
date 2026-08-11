using Own_Language_Course.Lib;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Statements
{
    public class FunctionDefineStatement : IStatement
    {
        public readonly string name;
        public readonly List<string> argNames;
        public readonly IStatement body;

        public FunctionDefineStatement(string name, List<string> argNames, IStatement body)
        {
            this.name = name;
            this.argNames = argNames;
            this.body = body;
        }

        public void Execute()
        {
            Function.Set(name, new UserDefineFunction(argNames, body));
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
            => $"def ({argNames}) {body}";
    }
}
