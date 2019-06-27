
namespace UWay.Skynet.Cloud.Data.Common
{
    /// <summary>
    /// SqlCommand 接口
    /// </summary>
    interface ISqlCommand
    {
        /// <summary>
        /// SQL文本
        /// </summary>
        string CommandText { get; }
        /// <summary>
        /// 命名参数集合
        /// </summary>
        NamedParameter[] Parameters { get; }
        /// <summary>
        /// 命名参数Value集合
        /// </summary>
        object ParameterValues { get; }
    }
}
