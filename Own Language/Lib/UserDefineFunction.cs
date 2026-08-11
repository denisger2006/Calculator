using Own_Language_Course.Parsing.AST.Statements;

namespace Own_Language_Course.Lib
{
    public class UserDefineFunction : IFunction
    {
        private readonly List<string> argNames;
        private readonly IStatement body;
        public int ArgsCount => argNames.Count;

        public UserDefineFunction(List<string> argNames, IStatement body)
        {
            this.argNames = argNames;
            this.body = body;
        }
        public string GetArgsName(int index)
        {
            if (index < 0 || index >= ArgsCount)
                return string.Empty;
            return argNames[index];
        }
        public IValue Execute(params IValue[] args)
        {
            try 
            {
                body.Execute();
                return NumberValue.ZERO;
            }
            catch (ReturnStatement rt)
            { return rt.Result; }
        }
    }
}
