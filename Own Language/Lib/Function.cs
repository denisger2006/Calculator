namespace Own_Language_Course.Lib
{
    public class Function
    {
        public static readonly Dictionary<string, IFunction> functions;
        static Function()
        {
            functions = new()
            {
                {
                    "sin",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 1) 
                            ErrorHandler.ThrowRuntimeError("Ожидается один аргумент. \n(значение в радианах)");

                        double result = Math.Round(Math.Sin(args[0].AsNumber()), 10);
                        return new NumberValue(result);
                    })
                },
                {
                    "cos",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 1) 
                            ErrorHandler.ThrowRuntimeError("Ожидается один аргумент. \n(значение в радианах)");

                        double result = Math.Round(Math.Cos(args[0].AsNumber()), 10);
                        return new NumberValue(result);
                    })
                },
                {
                    "tg",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 1)
                            ErrorHandler.ThrowRuntimeError("Ожидается один аргумент. \n(значение в радианах)");


                        double result = Math.Round(Math.Sin(args[0].AsNumber()), 10) /
                        Math.Round(Math.Cos(args[0].AsNumber()), 10);
                        return new NumberValue(result);
                    })
                },
                {
                    "ctg",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 1)
                            ErrorHandler.ThrowRuntimeError("Ожидается один аргумент. \n(значение в радианах)");

                        double result = Math.Round(Math.Cos(args[0].AsNumber()), 10) /
                        Math.Round(Math.Sin(args[0].AsNumber()), 10);
                        return new NumberValue(result);
                    })
                },
                {
                    "fact",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 1)
                             ErrorHandler.ThrowRuntimeError("Ожидается один аргумент.");

                        double x = args[0].AsNumber();

                        if (x < 0)
                            ErrorHandler.ThrowRuntimeError("Факториал определен только для неотрицательных чисел.");

                        double result = MathNet.Numerics.SpecialFunctions.Gamma(x + 1);
                        return new NumberValue(result);
                    })
                },
                {
                    "sqrt",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 1) 
                            ErrorHandler.ThrowRuntimeError("Ожидается один аргумент.");
                        double x = args[0].AsNumber();

                        if (x < 0)
                            ErrorHandler.ThrowRuntimeError("Подкоренное выражение не может быть отрицательным.");
                        return new NumberValue(Math.Sqrt(x));
                    })
                },
                {
                    "abs",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 1) 
                            ErrorHandler.ThrowRuntimeError("Ожидается один аргумент.");
                        return new NumberValue(Math.Abs(args[0].AsNumber()));
                    })
                },
                {
                    "cbrt",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 1)
                            ErrorHandler.ThrowRuntimeError("Ожидается один аргумент.");
                        return new NumberValue(Math.Cbrt(args[0].AsNumber()));
                    })
                },
                {
                    "exp10",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 1) 
                            ErrorHandler.ThrowRuntimeError("Ожидается один аргумент.");
                        return new NumberValue(Math.Pow(10, args[0].AsNumber()));
                    })
                },
                {
                    "exp2",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 1) 
                            ErrorHandler.ThrowRuntimeError("Ожидается один аргумент.");
                        return new NumberValue(Math.Pow(2, args[0].AsNumber()));
                    })
                },
                {
                    "sqr",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 1)
                            ErrorHandler.ThrowRuntimeError("Ожидается один аргумент.");
                        return new NumberValue(Math.Pow(args[0].AsNumber(), 2));
                    })
                },
                {
                    "cube",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 1)
                            ErrorHandler.ThrowRuntimeError("Ожидается один аргумент.");
                        return new NumberValue(Math.Pow(args[0].AsNumber(), 3));
                    })
                },
                {
                    "ln",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 1) 
                            ErrorHandler.ThrowRuntimeError("Ожидается один аргумент.");

                        double x = args[0].AsNumber();

                        if (x <= 0)
                            ErrorHandler.ThrowRuntimeError("Основание логарифма должно быть положительным числом.");
                        return new NumberValue(Math.Log(x));
                    })
                },
                {
                    "lg",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 1)
                            ErrorHandler.ThrowRuntimeError("Ожидается один аргумент.");


                        double x = args[0].AsNumber();

                        if (x <= 0)
                            ErrorHandler.ThrowRuntimeError("Основание логарифма должно быть положительным числом.");
                        return new NumberValue(Math.Log10(x));
                    })
                },
                {
                    "log",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 2) ErrorHandler.ThrowRuntimeError
                            ("Ожидается два аргумента\n. (1-й - подлогарифмическое выражение, 2-й - основание)");

                        double x = args[0].AsNumber(); 
                        double y = args[1].AsNumber(); 

                        if (x <= 0 || y <= 0 || y == 1)
                            ErrorHandler.ThrowRuntimeError
                            ("Для логарифма должны выполняться следующие условия:\n " +
                            "число (1 арг.) > 0, основание (2 арг.) > 0 и не равно 1");

                        return new NumberValue(Math.Log(x, y));
                    })
                },
                {
                    "pow",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 2) ErrorHandler.ThrowRuntimeError
                            ("Ожидается два аргумента\n. (1-й - основание степени, 2-й - показатель степени)");
                        return new NumberValue(Math.Pow(args[0].AsNumber(), args[1].AsNumber()));
                    })
                },
                {
                    "root",
                    new DelegateFunction(args =>
                    {
                        if (args.Length != 2)
                            ErrorHandler.ThrowRuntimeError("Ожидается два аргумента (1-й - основание, 2-й - степень корня)");

                        double x = args[0].AsNumber();
                        double y = args[1].AsNumber();

                        if (y <= 0)
                            ErrorHandler.ThrowRuntimeError("Степень корня должна быть положительным числом.");

                        if (Math.Abs(y - Math.Round(y)) > 1e-10)
                            ErrorHandler.ThrowRuntimeError("Степень корня должна быть целым числом.");

                        int n = (int)Math.Round(y);

                        if (n % 2 == 0 && x < 0)
                            ErrorHandler.ThrowRuntimeError("Нельзя взять корень чётной степени из отрицательного числ (1-й арг).");

                        double result = Math.Sign(x) * Math.Pow(Math.Abs(x), 1.0 / n);
                        return new NumberValue(result);
                    })
                },
                {
                    "echo",
                    new DelegateFunction(args =>
                    {
                        foreach (var arg in args)
                        {
                            Console.WriteLine(arg.AsString());
                        }
                        return NumberValue.ZERO;
                    })
                },
                {
                    "echoline",
                    new DelegateFunction(args =>
                    {
                        foreach (var arg in args)
                        {
                            Console.WriteLine(arg.AsString());
                        }
                        Console.WriteLine();
                        return NumberValue.ZERO;
                    })
                },
                {
                    "newArray",
                    new DelegateFunction(args =>
                    {
                        return CreateArray(args, 0);
                    })
                }
            };
        }
        private static ArrayValue CreateArray(IValue[] args, int index)
        {
            int size = (int)args[index].AsNumber();
            int last = args.Length - 1;
            var array = new ArrayValue(size);
            if (index == last)
            {
                for (int i = 0; i < size; i++)
                    array[i] = NumberValue.ZERO;
            }
            else if (index < last)
            {
                for (int i = 0; i < size; i++)
                    array[i] = CreateArray(args, index + 1);
            }
            return array;
        }
        public static bool IsExists(string key)
            => functions.ContainsKey(key);

        public static IFunction GetFunction(string key)
        {
            // ИЗМЕНЕНИЕ
            if (!IsExists(key))
                ErrorHandler.ThrowRuntimeError($"Не существует функции с названием <{key}>.");
            return functions[key];
        }
        public static void Set(string key, IFunction function)
        {
            functions[key] = function;
        }
    }
    public class DelegateFunction : IFunction
    {
        private readonly Func<IValue[], IValue> _executeMethod;
        public DelegateFunction(Func<IValue[], IValue> executeMethod)
        {
            _executeMethod = executeMethod;
        }
        public IValue Execute(params IValue[] args)
        {
            return _executeMethod(args);
        }
    }
}