using Own_Language_Course.Lib;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Expressions
{
    public class FunctionalExpression : IExpression
    {
        public readonly string name;
        public readonly List<IExpression> arguments;

        public FunctionalExpression(string name)
        {
            this.name = name;
            arguments = [];
        }
        public FunctionalExpression(string name, List<IExpression> arguments)
        {
            this.name = name;
            this.arguments = arguments;
        }
        public void AddArgument(IExpression arg)
        {
            arguments.Add(arg);
        }

        public IValue Eval()
        {
            int size = arguments.Count;
            var values = new IValue[size];
            for (int i = 0; i < size; i++)
            {
                values[i] = arguments[i].Eval();
            }

            if (!Function.IsExists(name))
            {
                ErrorHandler.ThrowRuntimeError($"Вызов несуществующей функции '{name}'.");
                return null!;
            }

            var function = Function.GetFunction(name); 
            if (function is UserDefineFunction userFunction)
            {
                if (size != userFunction.ArgsCount)
                {
                    ErrorHandler.ThrowRuntimeError($"Неправильное количество аргументов для функции '{name}'. Ожидалось: {userFunction.ArgsCount}, получено: {size}.");
                }
                Variable.Push();
                for (int i = 0; i < size; i++)
                {
                    Variable.Set(userFunction.GetArgsName(i), values[i]);
                }
                var result = userFunction.Execute(values);
                Variable.Pop();
                return result;
            }
            return function.Execute(values);
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
        {
            return $"{name}({arguments})";
        }
    }
}
