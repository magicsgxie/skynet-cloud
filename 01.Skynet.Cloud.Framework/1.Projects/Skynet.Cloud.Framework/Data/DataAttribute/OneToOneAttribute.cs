using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 一对一
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class OneToOneAttribute : AbstractAssociationAttribute
    {

    }
}
