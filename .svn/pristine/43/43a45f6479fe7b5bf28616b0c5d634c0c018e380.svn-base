using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

#if SDK4
using System.Dynamic;
#endif

namespace UWay.Skynet.Cloud.Data
{

    /// <summary>
    /// SqlHelper 接口
    /// </summary>
    public interface IDbHelper : IDisposable
    {
        /// <summary>
        /// 得到DbConfiguration对象
        /// </summary>
        DbConfiguration DbConfiguration { get; }
        /// <summary>
        /// 得到连接对象
        /// </summary>
        DbConnection Connection { get; }
        /// <summary>
        /// 执行更新
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="namedParameters">参数</param>
        /// <param name="isAutoClose">是否自动关闭连接</param>
        /// <returns></returns>
        int ExecuteNonQuery(string sql,  dynamic namedParameters = null, bool isAutoClose = true);

        Task<int> ExecuteNonQueryAsync(string sql, dynamic namedParameters = null, bool isAutoClose = true);
        /// <summary>
        /// 执行查询并返回DataReader
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="namedParameters">参数</param>
        /// <param name="isAutoClose">是否自动关闭连接</param>
        /// <returns></returns>
        DbDataReader ExecuteReader(string sql,  dynamic namedParameters = null, bool isAutoClose = true);

        /// <summary>
        /// 执行查询并返回DataReader
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="namedParameters">参数</param>
        /// <param name="isAutoClose">是否自动关闭连接，使用事务时设置不自动关闭</param>
        /// <returns></returns>
        Task<DbDataReader> ExecuteReaderAsync(string sql, dynamic namedParameters = null, bool isAutoClose = true);
        /// <summary>
        /// 执行查询并返回DataTable
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="namedParameters">参数</param>
        /// <param name="isAutoClose">是否自动关闭连接，使用事务时设置不自动关闭</param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string sql, dynamic namedParameters = null, bool isAutoClose = true);

        /// <summary>
        /// 执行查询并返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="namedParameters">参数</param>
        /// <param name="isAutoClose">是否自动关闭连接，使用事务时设置不自动关闭</param>
        /// <returns></returns>
        DataSet ExecuteDataSet(string sql,dynamic namedParameters = null, bool isAutoClose = true);
        /// <summary>
        /// 执行查询并返回首行首列
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="namedParameters">参数</param>
        /// <param name="isAutoClose">是否自动关闭连接，使用事务时设置不自动关闭</param>
        /// <returns></returns>
        object ExecuteScalar(string sql,   dynamic namedParameters = null, bool isAutoClose = true);

        /// <summary>
        /// 执行查询并返回首行首列
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="namedParameters">参数</param>
        /// <param name="isAutoClose">是否自动关闭连接，使用事务时设置不自动关闭</param>
        /// <returns></returns>
        Task<object> ExecuteScalarAsync(string sql, dynamic namedParameters = null, bool isAutoClose = true);

        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="skip">起始行</param>
        /// <param name="take">获取行</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="nameparameters">参数</param>
        /// <param name="isAutoClose">是否自动关闭连接，使用事务时设置不自动关闭</param>
        /// <returns></returns>
        DataTable ExecutePageDataTable(string sql, long skip, long take, dynamic nameparameters, out long rowCount,bool isAutoClose = true);
    }
}

