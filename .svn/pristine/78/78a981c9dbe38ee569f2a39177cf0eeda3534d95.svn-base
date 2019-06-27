using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Dialect;
using UWay.Skynet.Cloud.Data.Mapping;

namespace UWay.Skynet.Cloud.Data.Schema.Script
{
    /// <summary>
    /// 数据库脚本生成器接口，提供数据库、表、主键约束、外键约束、检查约束、Uniqule约束等脚本的创建工作
    /// </summary>
    public interface IDatabaseScriptGenerator
    {
        /// <summary>
        /// 得到对应的数据库类型
        /// </summary>
        /// <param name="sqlType"></param>
        /// <returns></returns>
        string GetDbType(SqlType sqlType);
        /// <summary>
        /// 生成数据库脚本
        /// </summary>
        /// <param name="dialect">数据库方言</param>
        /// <param name="mappings">映射元数据</param>
        /// <param name="dbName">数据库名称</param>
        /// <returns>返回数据库脚本</returns>
        DatabaseScriptEntry Build(IDialect dialect, IEntityMapping[] mappings, string dbName);

    }
}
