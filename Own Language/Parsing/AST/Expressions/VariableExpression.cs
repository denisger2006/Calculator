using Own_Language_Course.Lib;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Expressions
{
    public class VariableExpression : IExpression
    {
        public readonly string name;

        public VariableExpression(string name)
        {
            this.name = name;
        }
        public IValue Eval()
        {
            if (!Variable.IsExists(name))
            {
                ErrorHandler.ThrowRuntimeError($"Переменной с именем '{name}' не существует.");
            }
            return Variable.GetVariable(name);
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString() => name;
    }
}
