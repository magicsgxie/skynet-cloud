using System.Data.Common;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Render;
//using UWay.Skynet.Cloud.Data.Render;

namespace UWay.Skynet.Cloud.Data.Driver
{
    /// <summary>
    /// 驱动接口
    /// </summary>
    public interface IDriver
    {
        /// <summary>
        /// SQL参数前缀
        /// </summary>
        char NamedPrefix { get; }

        /// <summary>
        /// SQL生成工厂
        /// </summary>
        ISqlOmRenderer Render { get; }
        /// <summary>
        /// 是否允许多层读取
        /// </summary>
        bool AllowsMultipleOpenReaders { get; }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="command">数据库执行命令</param>
        /// <param name="parameter">参数信息</param>
        /// <param name="value">参数值</param>
        void AddParameter(DbCommand command, NamedParameter parameter, object value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd">数据库执行命令</param>
        /// <param name="namedParameters">参数实体</param>
        void AddParameters(DbCommand cmd, object namedParameters);

        /// <summary>
        /// 创建数据库执行命令
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">执行SQL</param>
        /// <param name="namedParameters">参数信息</param>
        /// <returns></returns>
        DbCommand CreateCommand(DbConnection conn, string sql, object namedParameters);

        /// <summary>
        /// 构建分页查询
        /// </summary>
        /// <param name="skip">开始行数</param>
        /// <param name="take">获取行数</param>
        /// <param name="parts">SQL分页信息</param>
        /// <param name="namedParameters">参数</param>
        /// <returns></returns>
        string BuildPageQuery(long skip, long take, PagingHelper.SQLParts parts, object namedParameters);
    }
}
