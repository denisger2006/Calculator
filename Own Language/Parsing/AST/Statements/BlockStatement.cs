using System.Text;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course.Parsing.AST.Statements
{
    public class BlockStatement : IStatement
    {
        public readonly List<IStatement> statements = [];

        public void Add(IStatement statement)
        {
            statements.Add(statement);
        }
        public void Execute()
        {
            foreach (var statement in statements)
            {
                statement.Execute();
            }
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var statement in statements)
            {
                result.Append(statement.ToString())
                    .AppendLine();
            }

            return result.ToString();
        }
    }
}
