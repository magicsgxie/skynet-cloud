namespace UWay.Skynet.Cloud.Linq.Impl
{
    public interface IFilterNode
    {
        void Accept(IFilterNodeVisitor visitor);
    }
}
