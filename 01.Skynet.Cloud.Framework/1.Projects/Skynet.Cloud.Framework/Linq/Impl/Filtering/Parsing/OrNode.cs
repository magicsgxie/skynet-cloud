using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Linq.Impl
{

    public class OrNode : IFilterNode, ILogicalNode
    {
        public IFilterNode First
        {
            get;
            set;
        }

        public IFilterNode Second 
        { 
            get; 
            set; 
        }
		
        public FilterCompositionLogicalOperator LogicalOperator
		{
			get
			{
                return FilterCompositionLogicalOperator.Or;
			}
		}

        

        public void Accept(IFilterNodeVisitor visitor)
        {
            visitor.StartVisit(this);

            First.Accept(visitor);
            Second.Accept(visitor);

            visitor.EndVisit();
        }
    }
}
