using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Reflection;
using UWay.Skynet.Cloud.Linq.Impl;
using UWay.Skynet.Cloud.Linq.Impl.Internal.Filtering;

namespace UWay.Skynet.Cloud.Linq
{
    /// <summary>
    /// Represents declarative filtering.
    /// </summary>
    public partial class FilterDescriptor : FilterDescriptorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterDescriptor"/> class.
        /// </summary>
        public FilterDescriptor() : this(string.Empty, FilterOperator.IsEqualTo, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterDescriptor"/> class.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="filterOperator">The filter operator.</param>
        /// <param name="filterValue">The filter value.</param>
        public FilterDescriptor(string member, FilterOperator filterOperator, object filterValue)
        {
            this.Member = member;
            this.Operator = filterOperator;
            this.Value = filterValue;
        }

        public object ConvertedValue
        {
            get
            {
                return this.Value;
            }
        }

        /// <summary>
        /// Gets or sets the member name which will be used for filtering.
        /// </summary>
        /// <filterValue>The member that will be used for filtering.</filterValue>
        public string Member
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the member that is used for filtering.
        /// Set this property if the member type cannot be resolved automatically.
        /// Such cases are: items with ICustomTypeDescriptor, XmlNode or DataRow.
        /// Changing this property did not raise         
        /// </summary>
        /// <value>The type of the member used for filtering.</value>
        public Type MemberType { get; set; }

        /// <summary>
        /// Gets or sets the filter operator.
        /// </summary>
        /// <filterValue>The filter operator.</filterValue>
        public FilterOperator Operator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the target filter value.
        /// </summary>
        /// <filterValue>The filter value.</filterValue>
        public object Value
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a predicate filter expression.
        /// </summary>
        /// <param name="parameterExpression">The parameter expression, which will be used for filtering.</param>
        /// <returns>A predicate filter expression.</returns>
        protected override Expression CreateFilterExpression(ParameterExpression parameterExpression)
        {
            var builder = new FilterDescriptorExpressionBuilder(parameterExpression, this);
            builder.Options.CopyFrom(ExpressionBuilderOptions);

            return builder.CreateBodyExpression();
        }

        /// <summary>
        /// Determines whether the specified <paramref name="other"/> descriptor 
        /// is equal to the current one.
        /// </summary>
        /// <param name="other">The other filter descriptor.</param>
        /// <returns>
        /// True if all members of the current descriptor are 
        /// equal to the ones of <paramref name="other"/>, otherwise false.
        /// </returns>
        public virtual bool Equals(FilterDescriptor other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return
                Equals(other.Operator, this.Operator) &&
                Equals(other.Member, this.Member) &&
                Equals(other.Value, this.Value);
        }

        /// <summary>
        /// Determines whether the specified <paramref name="obj"/>
        /// is equal to the current descriptor.
        /// </summary>
        public override bool Equals(object obj)
        {
            var other = obj as FilterDescriptor;
            if (other == null)
            {
                return false;
            }

            return Equals(other);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current filter descriptor.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = this.Operator.GetHashCode();
                result = (result * 397) ^ (this.Member != null ? this.Member.GetHashCode() : 0);
                result = (result * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return result;
            }
        }

        protected override void Serialize(System.Collections.Generic.IDictionary<string, object> json)
        {
            base.Serialize(json);

            json["field"] = Member;
            json["operator"] = Operator.ToToken();

            if (Value != null && Value.GetType().GetNonNullableType().IsEnum)
            {
                var type = Value.GetType().GetNonNullableType();
                var underlyingType = Enum.GetUnderlyingType(type);

                json["value"] = Convert.ChangeType(Value, underlyingType);
            }
            else
            {
                json["value"] = Value;
            }
        }

        public override string CreateFilter(IDictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, object>();
            }

            var predicate = new StringBuilder();
            if (!Member.IsNullOrEmpty())
            {
                if (Value != null && !Value.ToString().Equals("NULL", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (Operator == FilterOperator.In && Value is Array)
                    {
                        var array = (Array)Value;
                        object[] array1 = new object[array.Length];
                        array.CopyTo(array1, 0);
                        //predicate.AppendFormat("{0} {1} in (", OpenParen, Name);
                        var i = 0;
                        foreach (var item in array)
                        {
                            var paramName = string.Format("{0}{1}", Member, dictionary.Count);

                            if (i % 1000 == 0)
                            {
                                if (i > 0)
                                    predicate.AppendFormat(" ) OR  {0} in (", Member);
                                else
                                    predicate.AppendFormat(" ({0}  in (",  Member);
                                predicate.AppendFormat("@{0}", paramName);
                            }
                            else
                            {
                                predicate.AppendFormat(",@{0}", paramName);
                            }
                            dictionary.Add(paramName, item);
                            i++;
                        }
                        predicate.Append(")");
                        predicate.Append(")");
                        array1 = null;
                    }
                    else
                    {
                        var paramName = string.Format("{0}{1}", Member, dictionary.Count);
                        predicate.AppendFormat("{0} {1} {2}", "(", Operator.Where(Member, paramName), ")");
                        dictionary.Add(paramName, Value);
                    }
                }
            }
            return predicate.ToString();
        }

    }
}
