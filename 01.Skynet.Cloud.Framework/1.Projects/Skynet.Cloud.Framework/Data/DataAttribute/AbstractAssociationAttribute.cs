using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 
    /// </summary>
     [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public abstract class AbstractAssociationAttribute: MemberAttribute
    {
        /// <summary>
        /// Gets or sets members of this entity class to represent the key values on this side of the association.
        /// </summary>
        /// <value>Default = Id of the containing class</value>
        public string ThisKey { get; set; }
        /// <summary>
        /// Gets or sets one or more members of the target entity class as key values on the other side of the association.
        /// </summary>
        /// <value>Default = Id of the related class.</value>
        public string OtherKey { get; set; }

        public bool isForeignKey;

    }
}
