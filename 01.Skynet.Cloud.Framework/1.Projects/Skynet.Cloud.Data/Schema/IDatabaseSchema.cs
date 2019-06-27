
namespace UWay.Skynet.Cloud.Data.Schema
{
    public interface IDatabaseSchema
    {
        ITableSchema[] Tables { get; }
        ITableSchema[] Views { get; }
    }
}
