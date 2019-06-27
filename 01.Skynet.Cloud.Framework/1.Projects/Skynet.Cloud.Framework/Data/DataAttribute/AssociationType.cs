using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 关系类型
    /// </summary>
    enum AssociationType
    {
        /// <summary>
        /// 多对一
        /// </summary>
        ManyToOne,
        /// <summary>
        /// 一对多
        /// </summary>
        OneToMany,
        /// <summary>
        /// 一对一
        /// </summary>
        OneToOne,
    }
}
