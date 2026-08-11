using Own_Language_Course.Parsing.AST.Expressions;
using Own_Language_Course.Parsing.AST.Statements;

namespace Own_Language_Course.Parsing.Visitors
{
    public abstract class AbstractVisitor : IVisitor
    {
        public void Visit(ArrayAccessExpression s)
        {
            foreach (var index in s.indexes)
            {
                index.Accept(this);
            }
        }
        public void Visit(ArrayAssignmentStatement s)
        {
            s.array.Accept(this);
            s.expression.Accept(this);
        }
        public void Visit(ArrayExpression s)
        {
            foreach (var index in s.elements)
            {
                index.Accept(this);
            }
        }
        public virtual void Visit(AssignmentStatement s)
        {
            s.expression.Accept(this);
        }
        public void Visit(BinaryExpression s)
        {
            s.expr1.Accept(this);
            s.expr2.Accept(this);
        }
        public void Visit(BlockStatement s)
        {
            foreach (var statement in s.statements)
            {
                statement.Accept(this);
            }
        }
        public void Visit(BreakStatement s) { }
        public void Visit(ConditionalExpression s)
        {
            s.expr1.Accept(this);
            s.expr2.Accept(this);
        }
        public void Visit(ContinueStatement s) { }
        public void Visit(DoWhileStatement s)
        {
            s.condition.Accept(this);
            s.statement.Accept(this);
        }
        public void Visit(ForStatement s)
        {
            s.initialization.Accept(this);
            s.increment.Accept(this);
            s.statement.Accept(this);
            s.termination.Accept(this);
        }
        public virtual void Visit(FunctionDefineStatement s)
        {
            s.body.Accept(this);
        }
        public void Visit(FunctionStatement s)
        {
            s.function.Accept(this);
        }
        public void Visit(FunctionalExpression s)
        {
            foreach (var argument in s.arguments)
            {
                argument.Accept(this);
            }
        }
        public void Visit(IfStatement s)
        {
            s.expression.Accept(this);
            s.ifStatement.Accept(this);
            s.elseStatement?.Accept(this);

        }
        public void Visit(PrintStatement s)
        {
            s.expression.Accept(this);
        }
        public void Visit(ReturnStatement s) { }
        public void Visit(UnaryExpression s)
        {
            s.expr.Accept(this);
        }
        public void Visit(ValueExpression s) { }
        public virtual void Visit(VariableExpression s) { }
        public void Visit(WhileStatement st)
        {
            st.condition.Accept(this);
            st.statement.Accept(this);
        }
    }
}

