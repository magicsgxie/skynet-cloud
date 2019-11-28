using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Collections;

namespace UWay.Skynet.Cloud.Linq
{
    /// <summary>
    /// Represents declarative sorting.
    /// </summary>
    public class SortDescriptor : JsonObject, IDescriptor
    {
        /// <summary>
        /// 
        /// </summary>
        public SortDescriptor() : this(null, ListSortDirection.Ascending)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="order"></param>
        public SortDescriptor(string member, ListSortDirection order)
        {
            Member = member;
            SortDirection = order;
        }

        /// <summary>
        /// Gets or sets the member name which will be used for sorting.
        /// </summary>
        /// <filterValue>The member that will be used for sorting.</filterValue>
        public string Member
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sort direction for this sort descriptor. If the value is null
        /// no sorting will be applied.
        /// </summary>
        /// <value>The sort direction. The default value is null.</value>
        public ListSortDirection SortDirection
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public void Deserialize(string source)
        {
            var parts = source.Split(new[] { '-' });

            if (parts.Length > 1)
            {
                Member = parts[0];
            }

            var sortDirection = parts.Last();

            SortDirection = sortDirection == "desc" ? ListSortDirection.Descending : ListSortDirection.Ascending;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            return "{0}-{1}".FormatWith(Member, SortDirection == ListSortDirection.Ascending ? "asc" : "desc");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToSortString()
        {
            return string.Format("{0} {1}", Member, SortDirection == ListSortDirection.Ascending ? "asc" : "desc");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        protected override void Serialize(System.Collections.Generic.IDictionary<string, object> json)
        {
            json["field"] = Member;
            json["dir"] = SortDirection == ListSortDirection.Ascending ? "asc" : "desc";
        }
    }
}
