using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UWay.Skynet.Cloud.Data
{ 
    /// <summary>
    /// 版本标签，该标签仅仅能够应用在实体成员的类型是短整型、整型、长整型上
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class VersionAttribute : ColumnAttribute
    {
        public VersionAttribute() { }

        public VersionAttribute(string name) : base(name) { }
    }

}
