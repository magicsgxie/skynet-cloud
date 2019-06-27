
namespace UWay.Skynet.Cloud.Data.Schema.Script
{
    /// <summary>
    /// 数据库脚本执行器接口
    /// </summary>
    public interface IDatabaseScriptExecutor
    {
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="dbConfiguration"></param>
        /// <param name="script"></param>
        void CreateDatabase(DbConfiguration dbConfiguration, DatabaseScriptEntry script);
        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <param name="dbConfiguration"></param>
        /// <param name="script"></param>
        void CreateTables(DbConfiguration dbConfiguration, DatabaseScriptEntry script);
        /// <summary>
        /// 判断数据库是否存在
        /// </summary>
        /// <param name="dbConfiguration"></param>
        /// <returns></returns>
        bool DatabaseExists(DbConfiguration dbConfiguration);
        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="dbConfiguration"></param>
        void DeleteDatabase(DbConfiguration dbConfiguration);
    }
}
