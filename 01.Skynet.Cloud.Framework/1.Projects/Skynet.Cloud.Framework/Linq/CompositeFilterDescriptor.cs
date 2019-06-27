

namespace UWay.Skynet.Cloud.Linq
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using UWay.Skynet.Cloud.Collections;

    using UWay.Skynet.Cloud;
    using UWay.Skynet.Cloud.Linq.Impl.Internal;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents a filtering descriptor which serves as a container for one or more child filtering descriptors.
    /// </summary>
    public partial class CompositeFilterDescriptor : FilterDescriptorBase
    {
        private FilterDescriptorCollection filterDescriptors;

        /// <summary>
        /// Gets or sets the logical operator used for composing of <see cref="FilterDescriptors"/>.
        /// </summary>
        /// <value>The logical operator used for composition.</value>
        public FilterCompositionLogicalOperator LogicalOperator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the filter descriptors that will be used for composition.
        /// </summary>
        /// <value>The filter descriptors used for composition.</value>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Used for initialization from XAML")]
        public FilterDescriptorCollection FilterDescriptors
        {
            get
            {
                if (this.filterDescriptors == null)
                {
                    SetFilterDescriptors(new FilterDescriptorCollection());
                }
                return this.filterDescriptors;
            }
            set
            {
                if (this.filterDescriptors != value)
                {
                    this.SetFilterDescriptors(value);
                }
            }
        }

        /// <summary>
        /// Creates a predicate filter expression combining <see cref="FilterDescriptors"/> 
        /// expressions with <see cref="LogicalOperator"/>.
        /// </summary>
        /// <param name="parameterExpression">The parameter expression, which will be used for filtering.</param>
        /// <returns>A predicate filter expression.</returns>
        protected override Expression CreateFilterExpression(ParameterExpression parameterExpression)
        {
            var builder = new FilterDescriptorCollectionExpressionBuilder(parameterExpression, this.FilterDescriptors, this.LogicalOperator);
            builder.Options.CopyFrom(this.ExpressionBuilderOptions);

            return builder.CreateBodyExpression();
        }

        private void SetFilterDescriptors(FilterDescriptorCollection value)
        {
            if (this.filterDescriptors != null)
            {
                this.UnsubscribeForFilterDescriptorCollectionEvents();
            }

            this.filterDescriptors = value;

            this.SubscribeForFilterDescriptorCollectionEvents();
        }

        protected override void Serialize(System.Collections.Generic.IDictionary<string, object> json)
        {
            base.Serialize(json);

            json["logic"] = LogicalOperator.ToString().ToLowerInvariant();

            if (FilterDescriptors.Any())
            {
                json["filters"] = FilterDescriptors.OfType<JsonObject>().ToJson();
            }
        }

        partial void SubscribeForFilterDescriptorCollectionEvents();

        partial void UnsubscribeForFilterDescriptorCollectionEvents();

        public override string CreateFilter(IDictionary<string, object> dictionary)
        {
            var conditions = new List<string>();
            foreach(var item in filterDescriptors)
            {
                conditions.Add(string.Format(" (0) ", item.CreateFilter(dictionary)));
            }
            
            return conditions.Join(this.LogicalOperator.ToString());
        }
    }
}
