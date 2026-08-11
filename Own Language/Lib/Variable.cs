namespace Own_Language_Course.Lib
{
    public static class Variable
    {
        private static readonly NumberValue ZERO = new(0);
        private static Dictionary<string, IValue> variables;
        private static readonly Stack<Dictionary<string, IValue>> stack;

        static Variable()
        {
            stack = new();
            variables = new()
            {
                ["PI"] = new NumberValue(Math.PI),
                ["E"] = new NumberValue(Math.E),
                ["PHI"] = new NumberValue(1.618),
            };
        }
        public static bool IsExists(string key)
            => variables.ContainsKey(key);
        public static void Push()
        {
            stack.Push(new Dictionary<string, IValue>(variables));
        }
        public static void Pop()
        {
            variables = stack.Pop();
        }
        public static IValue GetVariable(string key)
        {
            if (!IsExists(key))
                return ZERO;
            return variables[key];
        }
        public static void Set(string key, IValue value)
        {
            variables[key] = value;
        }
    }
}