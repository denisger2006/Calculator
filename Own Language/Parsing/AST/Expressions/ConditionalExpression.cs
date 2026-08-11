using System.ComponentModel;
using System.Reflection;
using Own_Language_Course.Lib;
using static Own_Language_Course.Parsing.AST.Expressions.ConditionalExpression;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Expressions
{
    public class ConditionalExpression : IExpression
    {
        public readonly Operator operation;
        public readonly IExpression expr1, expr2;
        public enum Operator
        {
            [Description("+")] PLUS,
            [Description("-")] MINUS,
            [Description("*")] MULTIPLY,
            [Description("/")] DIVIDE,

            [Description("==")] EQUALS,
            [Description("!=")] NOT_EQUALS,

            [Description("<")] LT,
            [Description("<=")] LTEQU,
            [Description(">")] GT,
            [Description(">=")] GTEQU,

            [Description("&&")] AND,
            [Description("||")] OR
        };

        public ConditionalExpression(Operator operation, IExpression left, IExpression right)
        {
            this.operation = operation;
            expr1 = left;
            expr2 = right;
        }
        public IValue Eval()
        {
            var value1 = expr1.Eval();
            var value2 = expr2.Eval();

            double number1, number2;
            if (value1 is StringValue)
            {
                number1 = value1.AsString().CompareTo(value2.AsString());
                number2 = 0;
            }
            else
            {
                number1 = value1.AsNumber();
                number2 = value2.AsNumber();
            }

            var result = operation switch
            {
                Operator.LT => number1 < number2,
                Operator.LTEQU => number1 <= number2,
                Operator.GT => number1 > number2,
                Operator.GTEQU => number1 >= number2,
                Operator.EQUALS => number1 == number2,
                Operator.NOT_EQUALS => number1 != number2,
                Operator.AND => (number1 != 0) && (number2 != 0),
                Operator.OR => (number1 != 0) || (number2 != 0),
                _ => throw new Exception("Нет такового оператора"),
            };

            if (result == null)
            {
                ErrorHandler.ThrowRuntimeError("Нет такого логического оператора");
            }
            return new NumberValue(result);
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
            => $"({expr1} {operation.GetName()} {expr2})";
    }

    public static class OperatorExtensions
    {
        public static string GetName(this Operator op)
        {
            FieldInfo field = op.GetType().GetField(op.ToString());
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? op.ToString() : attribute.Description;
        }
    }
}
