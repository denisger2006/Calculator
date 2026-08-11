using Own_Language_Course.Parsing.AST.Expressions;
using Own_Language_Course.Parsing.AST.Statements;
using System.Globalization;
using Own_Language_Course.Lib; 

namespace Own_Language_Course.Parsing
{
    public class Parser
    {
        private static readonly Token EOF = new(TokenType.EOF, "");
        private readonly List<Token> Tokens;
        private int Size => Tokens.Count;
        private int Pos;
        public Parser(List<Token> tokens)
            => Tokens = tokens;
        public IStatement Parse()
        {
            var result = new BlockStatement();
            while (!Match(TokenType.EOF))
            {
                result.Add(Statement());
            }
            return result;
        }
        private IStatement Block()
        {
            var block = new BlockStatement();
            Consume(TokenType.LBRACE);
            while (!Match(TokenType.RBRACE))
            {
                block.Add(Statement());
            }
            return block;
        }
        private IStatement StatementOrBlock()
        {
            if (LookMatch(0, TokenType.LBRACE)) return Block();
            return Statement();
        }
        private IStatement Statement()
        {
            if (Match(TokenType.PRINT))
                return new PrintStatement(Expression());
            if (Match(TokenType.PRINTLN)) 
                return new PrintStatement(Expression(), addNewLine: true);
            if (Match(TokenType.IF))
                return IfElse();
            if (Match(TokenType.WHILE))
                return WhileStatement();
            if (Match(TokenType.DO))
                return DoWhileStatement();
            if (Match(TokenType.BREAK))
                return new BreakStatement();
            if (Match(TokenType.CONTINUE))
                return new ContinueStatement();
            if (Match(TokenType.RETURN))
                return new ReturnStatement(Expression());
            if (Match(TokenType.FOR))
                return ForStatement();
            if (Match(TokenType.DEF))
                return FunctionDefine();
            if (LookMatch(0, TokenType.WORD) && LookMatch(1, TokenType.LPAREN))
                return new FunctionStatement(Function());
            return AssigmentStatement();
        }
        private IStatement AssigmentStatement()
        {
            if (LookMatch(0, TokenType.WORD) && LookMatch(1, TokenType.EQU))
            {
                var variable = Consume(TokenType.WORD).Text;
                Consume(TokenType.EQU);
                return new AssignmentStatement(variable, Expression());
            }
            if (LookMatch(0, TokenType.WORD) && LookMatch(1, TokenType.LBRACKET))
            {
                var array = Element();
                Consume(TokenType.EQU);
                return new ArrayAssignmentStatement(array, Expression());
            }

            ErrorHandler.ThrowSyntaxError("Неизвестный оператор.");
            return null!;
        }
        private IStatement IfElse()
        {
            var condition = Expression();
            var ifStatement = StatementOrBlock();
            var elseStatement = Match(TokenType.ELSE) ? StatementOrBlock() : null;
            return new IfStatement(condition, ifStatement, elseStatement);
        }
        private IStatement WhileStatement()
        {
            var condition = Expression();
            var statement = StatementOrBlock();
            return new WhileStatement(condition, statement);
        }
        private IStatement DoWhileStatement()
        {
            var statement = StatementOrBlock();
            Consume(TokenType.WHILE);
            var condition = Expression();
            return new DoWhileStatement(condition, statement);
        }
        private IStatement ForStatement()
        {
            var initialization = AssigmentStatement();
            Consume(TokenType.COMMA);
            var termination = Expression();
            Consume(TokenType.COMMA);
            var increment = AssigmentStatement();
            var statement = StatementOrBlock();
            return new ForStatement(initialization, termination, increment, statement);
        }
        private FunctionDefineStatement FunctionDefine()
        {
            string name = Consume(TokenType.WORD).Text;
            Consume(TokenType.LPAREN);
            var argNames = new List<string>();
            while (!Match(TokenType.RPAREN))
            {
                argNames.Add(Consume(TokenType.WORD).Text);
                Match(TokenType.COMMA);
            }
            var body = StatementOrBlock();
            return new FunctionDefineStatement(name, argNames, body);
        }
        private FunctionalExpression Function()
        {
            string name = Consume(TokenType.WORD).Text;
            Consume(TokenType.LPAREN);
            var function = new FunctionalExpression(name);
            while (!Match(TokenType.RPAREN))
            {
                function.AddArgument(Expression());
                Match(TokenType.COMMA);
            }
            return function;
        }
        private IExpression Array()
        {
            Consume(TokenType.LBRACKET);
            var elements = new List<IExpression>();
            while (!Match(TokenType.RBRACKET))
            {
                elements.Add(Expression());
                Match(TokenType.COMMA);
            }
            return new ArrayExpression(elements);
        }
        private ArrayAccessExpression Element()
        {
            var variable = Consume(TokenType.WORD).Text;
            var indexes = new List<IExpression>();
            do
            {
                Consume(TokenType.LBRACKET);
                indexes.Add(Expression());
                Consume(TokenType.RBRACKET);
            }
            while (LookMatch(0, TokenType.LBRACKET));
            return new ArrayAccessExpression(variable, indexes);
        }
        private IExpression Expression()
        {
            return LogicalOr();
        }
        private IExpression LogicalOr()
        {
            var result = LogicalAnd();
            while (true)
            {
                if (Match(TokenType.BARBAR))
                {
                    result = new ConditionalExpression
                         (ConditionalExpression.Operator.OR, result, LogicalAnd());
                    continue;
                }
                break;
            }
            return result;
        }
        private IExpression LogicalAnd()
        {
            var result = Equality();
            while (true)
            {
                if (Match(TokenType.AMPAMP))
                {
                    result = new ConditionalExpression
                        (ConditionalExpression.Operator.AND, result, Equality());
                }
                break;
            }
            return result;
        }
        private IExpression Equality()
        {
            var result = Conditional();
            if (Match(TokenType.EQUEQU))
            {
                return new ConditionalExpression
                    (ConditionalExpression.Operator.EQUALS, result, Conditional());
            }
            if (Match(TokenType.EXCLEQU))
            {
                return new ConditionalExpression
                    (ConditionalExpression.Operator.NOT_EQUALS, result, Conditional());
            }
            return result;
        }
        private IExpression Conditional()
        {
            var result = Additive();
            while (true)
            {
                if (Match(TokenType.LT))
                {
                    result = new ConditionalExpression
                        (ConditionalExpression.Operator.LT, result, Additive());
                    continue;
                }
                if (Match(TokenType.LTEQU))
                {
                    result = new ConditionalExpression
                        (ConditionalExpression.Operator.LTEQU, result, Additive());
                    continue;
                }
                if (Match(TokenType.GT))
                {
                    result = new ConditionalExpression
                        (ConditionalExpression.Operator.GT, result, Additive());
                    continue;
                }
                if (Match(TokenType.GTEQU))
                {
                    result = new ConditionalExpression
                        (ConditionalExpression.Operator.GTEQU, result, Additive());
                    continue;
                }
                break;
            }
            return result;
        }
        private IExpression Additive()
        {
            var result = Multiplicative();
            while (true)
            {
                if (Match(TokenType.PLUS))
                {
                    result = new BinaryExpression('+', result, Multiplicative());
                    continue;
                }
                if (Match(TokenType.MINUS))
                {
                    result = new BinaryExpression('-', result, Multiplicative());
                    continue;
                }
                break;
            }
            return result;
        }
        private IExpression Multiplicative()
        {
            var result = Power();
            while (true)
            {
                if (Match(TokenType.STAR))
                {
                    result = new BinaryExpression('*', result, Power());
                    continue;
                }
                if (Match(TokenType.SLASH))
                {
                    result = new BinaryExpression('/', result, Power());
                    continue;
                }
                break;
            }
            return result;
        }
        private IExpression Power()
        {
            var result = Unary();
            while (true)
            {
                if (Match(TokenType.CARET))
                {
                    result = new BinaryExpression('^', result, Unary());
                    continue;
                }
                break;
            }
            return result;
        }
        private IExpression Unary()
        {
            if (Match(TokenType.MINUS))
                return new UnaryExpression('-', Primary());
            if (Match(TokenType.PLUS))
                return Primary();
            return Primary();
        }
        private IExpression Primary()
        {
            var current = GetToken(0);
            if (Match(TokenType.NUMBER))
            {
                if (double.TryParse(current.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double intResult))
                    return new ValueExpression(intResult);
                else
                {
                    ErrorHandler.ThrowSyntaxError($"{current.Text} не является корректным числом.");
                    return null!;
                }
            }
            if (Match(TokenType.HEX_NUMBER))
                return new ValueExpression(long.Parse(current.Text, NumberStyles.HexNumber));
            if (LookMatch(0, TokenType.WORD) && LookMatch(1, TokenType.LPAREN))
                return Function();
            if (LookMatch(0, TokenType.WORD) && LookMatch(1, TokenType.LBRACKET))
                return Element();
            if (LookMatch(0, TokenType.LBRACKET))
                return Array();
            if (Match(TokenType.WORD))
                return new VariableExpression(current.Text);
            if (Match(TokenType.TEXT))
                return new ValueExpression(current.Text);
            if (Match(TokenType.LPAREN))
            {
                var result = Expression();
                Match(TokenType.RPAREN);
                return result;
            }

            ErrorHandler.ThrowSyntaxError("Неизвестное выражение.");
            return null!;
        }
        private Token Consume(TokenType type)
        {
            var current = GetToken(0);
            if (type != current.Type)
            {
                ErrorHandler.ThrowSyntaxError($"Токен {current} не соответствует токену {type}.");
            }
            Pos++;
            return current;
        }
        private bool Match(TokenType type)
        {
            var current = GetToken(0);
            if (type != current.Type)
                return false;
            Pos++;
            return true;
        }
        private bool LookMatch(int position, TokenType type)
        {
            return GetToken(position).Type == type;
        }
        private Token GetToken(int relativePosition)
        {
            int position = Pos + relativePosition;
            if (position >= Size) return EOF;
            return Tokens[position];
        }

        //для калькулятора
        public double GetValue(string name)
            => Variable.GetVariable("result").AsNumber();
        
    }
}