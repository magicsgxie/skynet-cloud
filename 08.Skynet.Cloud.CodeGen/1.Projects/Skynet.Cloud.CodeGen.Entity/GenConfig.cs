using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.CodeGen.Entity
{
    public class GenConfig
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }


        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 表前缀
        /// </summary>
        public string TablePrefix { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表评论
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 是否生成映射文件
        /// </summary>
        public bool IsGeneratorMapping { set; get; }
    }
}
