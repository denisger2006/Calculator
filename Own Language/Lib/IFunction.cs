namespace Own_Language_Course.Lib
{
    public interface IFunction
    {
        IValue Execute(params IValue[] args);
    }
}
