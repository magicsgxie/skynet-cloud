namespace UWay.Skynet.Cloud.Linq.Impl
{

    public class DateTimeNode : IFilterNode, IValueNode
    {
        public object Value
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
