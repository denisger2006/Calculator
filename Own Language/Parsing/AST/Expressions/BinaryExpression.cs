using System.Text;
using Own_Language_Course.Lib;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Expressions
{
    public class BinaryExpression : IExpression
    {
        public readonly char operation;
        public readonly IExpression expr1, expr2;

        public BinaryExpression(char operation, IExpression expr1, IExpression expr2)
        {
            this.operation = operation;
            this.expr1 = expr1;
            this.expr2 = expr2;
        }
        public IValue Eval()
        {
            var value1 = expr1.Eval();
            var value2 = expr2.Eval();
            if (value1 is StringValue || value1 is ArrayValue)
            {
                var string1 = value1.AsString();
                var string2 = value2.AsString();
                switch (operation)
                {
                    case '+':
                        return new StringValue(string1 + string2);
                    case '*':
                        int iters = (int)value2.AsNumber();
                        var buffer = new StringBuilder();
                        for (int i = 0; i < iters; i++)
                        {
                            buffer.Append(string1);
                        }
                        return new StringValue(buffer.ToString());
                    default:
                        ErrorHandler.ThrowRuntimeError($"Неизвестный оператор '{operation}' для строк/массивов.");
                        return null!;
                }
            }
            var number1 = expr1.Eval().AsNumber();
            var number2 = expr2.Eval().AsNumber();

            if (operation == '/' && number2 == 0)
            {
                ErrorHandler.ThrowRuntimeError("Деление на ноль.");
            }

            var result = operation switch
            {
                '+' => new NumberValue(number1 + number2),
                '-' => new NumberValue(number1 - number2),
                '*' => new NumberValue(number1 * number2),
                '/' => new NumberValue(number1 / number2),
                '^' => new NumberValue(Math.Pow(number1, number2)),
                _ => null
            }; 

            if (result == null)
            {
                ErrorHandler.ThrowRuntimeError($"Неизвестный оператор '{operation}'.");
            }
            return result;
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
            => $"({expr1} {operation} {expr2})";
    }
}



