
namespace UWay.Skynet.Cloud.Data.Schema.Script
{
    /// <summary>
    /// 建库脚本实体类
    /// </summary>
    public class DatabaseScriptEntry
    {
        /// <summary>
        /// 得到或设置创建数据库的脚本
        /// </summary>
        public string DatabaseScript { get; set; }

        /// <summary>
        /// 得到或设置建Schema的脚本
        /// </summary>
        public string[] SchemaScripts { get; set; }

        /// <summary>
        /// 得到或设置建表的脚本
        /// </summary>
        public string[] TableScripts { get; set; }

        /// <summary>
        /// 得到或设置创建主键的脚本
        /// </summary>
        public string[] PKConstraintScripts { get; set; }

        /// <summary>
        /// 得到或设置创建外键的脚本
        /// </summary>
        public string[] FKConstraintScripts { get; set; }

        /// <summary>
        /// 得到或设置创建Uniqule约束的脚本
        /// </summary>
        public string[] UniquleConstraintScripts { get; set; }

        /// <summary>
        /// 得到或设置创建检查约束的脚本
        /// </summary>
        public string[] CheckConstraintScript { get; set; }

        /// <summary>
        /// 得到或设置创建序列脚本
        /// </summary>
        public string[] SequenceScripts { get; set; }
    }
}
