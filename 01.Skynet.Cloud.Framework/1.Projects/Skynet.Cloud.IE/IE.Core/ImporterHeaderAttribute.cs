// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : ImporterHeaderAttribute.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
//           
//           
//           QQ：279218456（编程交流）
//           
// 
// ======================================================================

using System;

namespace UWay.Skynet.Cloud.IE.Core
{
    /// <summary>
    /// 导入头部特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ImporterHeaderAttribute : Attribute
    {
        /// <summary>
        ///     显示名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        ///     批注
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        ///     作者
        /// </summary>
        public string Author { set; get; } = "麦扣";

        /// <summary>
        ///     自动过滤空格，默认启用
        /// </summary>
        public bool AutoTrim { get; set; } = true;

        /// <summary>
        ///     处理掉所有的空格，包括中间空格
        /// </summary>
        public bool FixAllSpace { get; set; }

        /// <summary>
        ///     列索引，如果为0则自动计算
        /// </summary>
        public int ColumnIndex { get; set; }

        /// <summary>
        /// 是否允许重复
        /// </summary>
        public bool IsAllowRepeat { get; set; } = true;

        /// <summary>
        ///     是否忽略
        /// </summary>
        public bool IsIgnore { get; set; }
    }
}