using Own_Language_Course.Parsing.AST.Expressions;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Statements
{
    public class PrintStatement : IStatement
    {
        public readonly IExpression expression;
        public readonly bool addNewLine; 

        public PrintStatement(IExpression expression, bool addNewLine = false)
        {
            this.expression = expression;
            this.addNewLine = addNewLine;
        }

        public void Execute()
        {
            if (addNewLine)
                Console.WriteLine(expression.Eval());
            else
                Console.Write(expression.Eval());
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
            => $"{(addNewLine ? "println" : "print")} {expression}";
    }
}