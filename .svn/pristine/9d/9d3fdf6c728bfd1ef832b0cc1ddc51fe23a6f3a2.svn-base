using System.Data.Common;
using UWay.Skynet.Cloud.Data.Common;
//using UWay.Skynet.Cloud.Data.Render;

namespace UWay.Skynet.Cloud.Data.Driver
{
    public interface IDriver
    {
        char NamedPrefix { get; }


        //ISqlOmRenderer Render { get; }
        bool AllowsMultipleOpenReaders { get; }
        void AddParameter(DbCommand command, NamedParameter parameter, object value);
        void AddParameters(DbCommand cmd, object namedParameters);
        DbCommand CreateCommand(DbConnection conn, string sql, object namedParameters);

        string BuildPageQuery(long skip, long take, PagingHelper.SQLParts parts, object namedParameters);
    }
}
