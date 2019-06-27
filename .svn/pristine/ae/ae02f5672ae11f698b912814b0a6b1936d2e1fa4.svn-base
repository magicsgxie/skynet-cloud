
namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 缺省约定映射策略，比如ClassName到TableName的转换约定，字段或属性到列名的转换约定
    /// </summary>
    public interface IMappingConversion
    {
        /// <summary>
        /// 类名转表名
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        string TableName(string className);

        /// <summary>
        /// 字段或属性转列名
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        string ColumnName(string memberName);
    }
}
