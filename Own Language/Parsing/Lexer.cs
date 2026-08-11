using System.Text;
using Own_Language_Course.Lib; 

namespace Own_Language_Course.Parsing
{
    public class Lexer
    {
        public static readonly Dictionary<string, TokenType> OPERATORS = new()
        {
            ["+"] = TokenType.PLUS,
            ["-"] = TokenType.MINUS,
            ["*"] = TokenType.STAR,
            ["/"] = TokenType.SLASH,
            ["^"] = TokenType.CARET,
            ["("] = TokenType.LPAREN,
            [")"] = TokenType.RPAREN,
            ["["] = TokenType.LBRACKET,
            ["]"] = TokenType.RBRACKET,
            ["{"] = TokenType.LBRACE,
            ["}"] = TokenType.RBRACE,
            ["="] = TokenType.EQU,
            ["<"] = TokenType.LT,
            [">"] = TokenType.GT,
            [","] = TokenType.COMMA,
            ["!"] = TokenType.EXCL,
            ["&"] = TokenType.AMP,
            ["|"] = TokenType.BAR,
            ["=="] = TokenType.EQUEQU,
            ["!="] = TokenType.EXCLEQU,
            ["<="] = TokenType.LTEQU,
            [">="] = TokenType.GTEQU,
            ["&&"] = TokenType.AMPAMP,
            ["||"] = TokenType.BARBAR
        };
        public string Input { get; private set; }
        public int Length => Input.Length;
        public List<Token> Tokens { get; private set; } = [];
        public int Pos { get; private set; }
        public Lexer(string input) => Input = input;
        public List<Token> Tokenize()
        {
            while (Pos < Length)
            {
                char current = Peek(0);
                if (char.IsDigit(current))
                    TokenizeNumber();
                else if (char.IsLetter(current))
                    TokenizeWord();
                else if (current == '#')
                {
                    Next();
                    TokenizeHexNumber();
                }
                else if (current == '"')
                {
                    TokenizeText();
                }
                else if (OPERATORS.ContainsKey(current.ToString()))
                    TokenizeOperator();
                else Next();
            }
            return Tokens;
        }
        private void TokenizeNumber()
        {
            var buffer = new StringBuilder();
            char current = Peek(0);
            while (true)
            {
                if (current == '.')
                {
                    if (buffer.ToString().Contains('.'))
                    {
                        ErrorHandler.ThrowLexicalError("Неправильное вещественное число.");
                    }
                }
                else if (!char.IsDigit(current)) break;
                buffer.Append(current);
                current = Next();
            }
            AddToken(TokenType.NUMBER, buffer.ToString());
        }
        private void TokenizeHexNumber()
        {
            var buffer = new StringBuilder();
            char current = Peek(0);
            while (char.IsDigit(current) || IsHexNumber(current))
            {
                buffer.Append(current);
                current = Next();
            }
            AddToken(TokenType.HEX_NUMBER, buffer.ToString());
        }
        private static bool IsHexNumber(char current)
        {
            return "abcdef".Contains(char.ToLower(current));
        }
        private void TokenizeOperator()
        {
            char current = Peek(0);
            if (current == '/')
            {
                if (Peek(1) == '/')
                {
                    Next();
                    Next();
                    TokenizeComment();
                    return;
                }
                else if (Peek(1) == '*')
                {
                    Next();
                    Next();
                    TokenizeMultilineComment();
                    return;
                }
            }
            var buffer = new StringBuilder();
            while (true)
            {
                var text = buffer.ToString();
                if (!OPERATORS.ContainsKey(text + current) && text.Length > 0)
                {
                    AddToken(OPERATORS[text]);
                    return;
                }
                buffer.Append(current);
                current = Next();
            }
        }
        private void TokenizeWord()
        {
            var buffer = new StringBuilder();
            char current = Peek(0);
            while (true)
            {
                if (char.IsLetterOrDigit(current) || current == '_' || current == '$')
                {
                    buffer.Append(current);
                    current = Next();
                }
                else break;
            }
            var word = buffer.ToString();
            switch (word)
            {
                case "print": AddToken(TokenType.PRINT); break;
                case "println": AddToken(TokenType.PRINTLN); break; 
                case "if": AddToken(TokenType.IF); break;
                case "else": AddToken(TokenType.ELSE); break;
                case "while": AddToken(TokenType.WHILE); break;
                case "for": AddToken(TokenType.FOR); break;
                case "do": AddToken(TokenType.DO); break;
                case "break": AddToken(TokenType.BREAK); break;
                case "continue": AddToken(TokenType.CONTINUE); break;
                case "def": AddToken(TokenType.DEF); break;
                case "return": AddToken(TokenType.RETURN); break;
                default: AddToken(TokenType.WORD, word); break;
            }
        }
        private void TokenizeText()
        {
            Next();
            var buffer = new StringBuilder();
            char current = Peek(0);
            while (true)
            {
                if (current == '\\')
                {
                    current = Next();
                    switch (current)
                    {
                        case '"': current = Next(); buffer.Append('"'); continue;
                        case 'n': current = Next(); buffer.Append('\n'); continue;
                        case 't': current = Next(); buffer.Append('\t'); continue;
                    }
                    buffer.Append('\\');
                    continue;
                }
                if (current != '"')
                {
                    buffer.Append(current);
                    current = Next();
                }
                else break;
            }
            Next(); 
            AddToken(TokenType.TEXT, buffer.ToString());
        }
        private void TokenizeComment()
        {
            char current = Peek(0);
            while ("\r\n\0".Contains(current))
            {
                current = Next();
            }
        }
        private void TokenizeMultilineComment()
        {
            char current = Peek(0);
            while (true)
            {
                if (current == '\0')
                    ErrorHandler.ThrowLexicalError("Пропущен закрывающий тег для многострочного комментария.");

                if (current == '*' && Peek(1) == '/') break;
                current = Next();
            }
            Next();
            Next();
        }
        private char Next()
        {
            Pos++;
            return Peek(0);
        }
        private char Peek(int relativePosition)
        {
            int position = Pos + relativePosition;
            if (position >= Length) return '\0';
            return Input[position];
        }
        private void AddToken(TokenType type)
        {
            Tokens.Add(new Token(type, ""));
        }
        private void AddToken(TokenType type, string text)
        {
            Tokens.Add(new Token(type, text));
        }
    }
}