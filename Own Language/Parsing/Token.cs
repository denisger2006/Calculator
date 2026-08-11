namespace Own_Language_Course.Parsing
{
    public class Token
    {
        public string Text { get; private set; }
        public TokenType Type { get; private set; }

        public Token(TokenType type, string text)
        {
            Type = type;
            Text = text;
        }
        public override string ToString()
            => $"{Type} {Text}";
    }

    public enum TokenType
    {
        NUMBER,
        HEX_NUMBER,
        WORD,
        TEXT,

        PRINT,
        PRINTLN,
        IF,
        ELSE,
        WHILE,
        FOR,
        DO,
        BREAK,
        CONTINUE,
        DEF,
        RETURN,

        PLUS,
        MINUS,
        STAR, 
        SLASH,
        CARET,
        EQU,
        EQUEQU,
        EXCL,
        EXCLEQU,
        LT,
        LTEQU,
        GT,
        GTEQU,

        BAR,
        BARBAR,
        AMP,
        AMPAMP,

        LPAREN,
        RPAREN,
        LBRACKET,
        RBRACKET,
        LBRACE,
        RBRACE,
        COMMA,

        EOF
    }
}
