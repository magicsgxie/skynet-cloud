using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 关系映射标签，支持一对多和多对一
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class AssociationAttribute : AbstractAssociationAttribute
    {

        public virtual bool IsForeignKey { get { return isForeignKey; } set { isForeignKey = value; } }
    }

}
