namespace UWay.Skynet.Cloud.Linq.Impl
{
    using Reflection;
    using System;
    using System.Collections.Generic;


    public class FilterNodeVisitor : IFilterNodeVisitor
    {
        private Stack<IFilterDescriptor> context;

        public FilterNodeVisitor()
        {
            context = new Stack<IFilterDescriptor>();
        }

        public IFilterDescriptor Result
        {
            get
            {
                return context.Pop();
            }
        }

        private IFilterDescriptor CurrentDescriptor
        {
            get
            {
                if (context.Count > 0)
                {
                    return context.Peek();
                }

                return null;
            }
        }

        public void StartVisit(IOperatorNode operatorNode)
        {
            FilterDescriptor filterDescriptor = new FilterDescriptor
            {
                Operator = operatorNode.FilterOperator
            };

            CompositeFilterDescriptor compositeFilterDescriptor = CurrentDescriptor as CompositeFilterDescriptor;

            if (compositeFilterDescriptor != null)
            {
                compositeFilterDescriptor.FilterDescriptors.Add(filterDescriptor);
            }

            context.Push(filterDescriptor);
        }

        public void StartVisit(ILogicalNode logicalNode)
        {
            CompositeFilterDescriptor filterDescriptor = new CompositeFilterDescriptor
            {
                LogicalOperator = logicalNode.LogicalOperator
            };

            CompositeFilterDescriptor compositeFilterDescriptor = CurrentDescriptor as CompositeFilterDescriptor;
            if (compositeFilterDescriptor != null)
            {
                compositeFilterDescriptor.FilterDescriptors.Add(filterDescriptor);
            }

            context.Push(filterDescriptor);
        }

        public void Visit(PropertyNode propertyNode)
        {
            ((FilterDescriptor)CurrentDescriptor).Member = propertyNode.Name.TrimStart(new char[] { '_' });
        }

        public void EndVisit()
        {
            if (context.Count > 1)
            {
                context.Pop();
            }
        }

        public void Visit(IValueNode valueNode)
        {

            if (((FilterDescriptor)CurrentDescriptor).Operator == FilterOperator.In)
            {
                //List<object> list = new List<object>();
                //if(((FilterDescriptor)CurrentDescriptor).Value != null)
                //{
                //    list.AddRange(((FilterDescriptor)CurrentDescriptor).Value as object[]);
                //}
                //list.Add(valueNode.Value);
                if (valueNode.Value.GetType() == Types.Double)
                {
                    ((FilterDescriptor)CurrentDescriptor).Value = GetValues((valueNode.Value).ToDouble()).ToArray();
                }
                else if (valueNode.Value.GetType() == Types.String)
                {
                    ((FilterDescriptor)CurrentDescriptor).Value = GetValues((valueNode.Value).ToString()).ToArray();
                }
                else if (valueNode.Value.GetType() == Types.DateTime)
                {
                    ((FilterDescriptor)CurrentDescriptor).Value = GetValues((valueNode.Value).ToDateTime()).ToArray();
                }
                else if (valueNode.Value.GetType() == Types.Boolean)
                {
                    ((FilterDescriptor)CurrentDescriptor).Value = GetValues((valueNode.Value).ToBoolean()).ToArray();
                }

            }
            else
            {
                ((FilterDescriptor)CurrentDescriptor).Value = valueNode.Value;
            }
        }

        private List<bool> GetValues(bool value)
        {
            List<bool> list = new List<bool>();
            if (((FilterDescriptor)CurrentDescriptor).Value != null)
            {
                list.AddRange(((FilterDescriptor)CurrentDescriptor).Value as bool[]);
            }
            list.Add(value);
            return list;
        }

        private List<double> GetValues(double value)
        {
            List<double> list = new List<double>();
            if (((FilterDescriptor)CurrentDescriptor).Value != null)
            {
                list.AddRange(((FilterDescriptor)CurrentDescriptor).Value as double[]);
            }
            list.Add(value);
            return list;
        }

        private List<string> GetValues(string value)
        {
            List<string> list = new List<string>();
            if (((FilterDescriptor)CurrentDescriptor).Value != null)
            {
                list.AddRange(((FilterDescriptor)CurrentDescriptor).Value as string[]);
            }
            list.Add(value);
            return list;
        }

        private List<DateTime> GetValues(DateTime value)
        {
            List<DateTime> list = new List<DateTime>();
            if (((FilterDescriptor)CurrentDescriptor).Value != null)
            {
                list.AddRange(((FilterDescriptor)CurrentDescriptor).Value as DateTime[]);
            }
            list.Add(value);
            return list;
        }
    }
}
