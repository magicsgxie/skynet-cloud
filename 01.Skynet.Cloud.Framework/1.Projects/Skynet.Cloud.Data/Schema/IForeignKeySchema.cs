
namespace UWay.Skynet.Cloud.Data.Schema
{
    /// <summary>
    /// 外键Schema
    /// </summary>
    public interface IForeignKeySchema : IRelationSchema
    {
        /// <summary>
        /// 外键名称
        /// </summary>
        string Name { get; }


    }
}
