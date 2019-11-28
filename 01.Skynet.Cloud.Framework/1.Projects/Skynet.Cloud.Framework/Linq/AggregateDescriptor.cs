using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UWay.Skynet.Cloud.Linq.Impl;
    using UWay.Skynet.Cloud.Linq.Impl.Grouping.Aggregates;
    /// <summary>
    /// 聚合
    /// </summary>
    public class AggregateDescriptor : IDescriptor
    {
        private readonly IDictionary<string, Func<AggregateFunction>> aggregateFactories;
        /// <summary>
        /// 
        /// </summary>
        public AggregateDescriptor()
        {
            Aggregates = new List<AggregateFunction>();

            aggregateFactories = new Dictionary<string, Func<AggregateFunction>>
              {
                  { "sum", () => new SumFunction { SourceField = Member } },
                  { "count", () => new CountFunction{ SourceField = Member } },
                  { "average", () => new AverageFunction { SourceField = Member } },
                  { "min", () => new MinFunction { SourceField = Member } },
                  { "max", () => new MaxFunction { SourceField = Member } }
              };
        }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<AggregateFunction> Aggregates
        {
            get;
            private set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Member
        {
            get;
            set;
        }

        /// <summary>
        /// 反序列
        /// </summary>
        /// <param name="source"></param>
        public void Deserialize(string source)
        {
            var components = source.Split('-');

            if (components.Any())
            {
                Member = components[0];

                for (int i = 1; i < components.Length; i++)
                {
                    DeserializeAggregate(components[i]);
                }
            }
        }

        private void DeserializeAggregate(string aggregate)
        {
            Func<AggregateFunction> factory;

            if (aggregateFactories.TryGetValue(aggregate, out factory))
            {
                Aggregates.Add(factory());
            }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            var result = new StringBuilder(Member);

            var aggregates = Aggregates.Select(aggregate => aggregate.FunctionName.Split('_')[0].ToLowerInvariant());

            foreach (var aggregate in aggregates)
            {
                result.Append("-");
                result.Append(aggregate);
            }

            return result.ToString();
        }


        //public string CreateQuery()
        //{
        //    foreach (var aggregate in aggregates)
        //    {
        //        result.Append("-");
        //        result.Append(aggregate);
        //    }

        //}
    }
}
