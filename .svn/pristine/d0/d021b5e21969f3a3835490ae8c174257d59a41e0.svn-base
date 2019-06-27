namespace UWay.Skynet.Cloud.Linq.Impl
{
    public class PropertyNode : IFilterNode
    {
        public string Name
        {
            get;
            set;
        }

        public void Accept(IFilterNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
