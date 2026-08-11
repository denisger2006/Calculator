namespace Own_Language_Course.Parsing.Visitors
{
    public interface INode
    {
        void Accept(IVisitor visitor);
    }
}
