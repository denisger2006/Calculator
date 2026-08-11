using System.Text;
using Own_Language_Course.Parsing;
using Own_Language_Course.Parsing.Visitors;

namespace Own_Language_Course
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string input = File.ReadAllText("main.txt", Encoding.UTF8);
                var tokens = new Lexer(input).Tokenize();

                var program = new Parser(tokens).Parse();
                program.Accept(new FunctionAdder());
                program.Accept(new VariablePrinter());
                program.Accept(new AssignValidator());

                Console.WriteLine("\n\n\nВЫВОД ПРОГРАММЫ:");
                program.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}