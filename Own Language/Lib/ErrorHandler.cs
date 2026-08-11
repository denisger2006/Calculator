using System;

namespace Own_Language_Course.Lib
{
    public static class ErrorHandler
    {
        public static void ThrowLexicalError(string message)
        {
            throw new Exception($"[Ошибка лексера]: {message}");
        }
        public static void ThrowSyntaxError(string message)
        {
            throw new Exception($"[Ошибка синтаксиса]: {message}");
        }
        public static void ThrowRuntimeError(string message)
        {
            throw new Exception($"[Ошибка выполнения]: {message}");
        }
    }
}