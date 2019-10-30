using UWay.Skynet.Cloud.CodeGen.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Request;

namespace UWay.Skynet.Cloud.CodeGen.Repository
{
    public class CodeGenRepository : ObjectRepository
    {
        public CodeGenRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public DataTable QueryTable(string tableName)
        {
            IDictionary<string, object> paramters = new Dictionary<string, object>();
            var sql = GetQueryDataBaseTableSql(tableName, paramters);

            return ExecuteDataTable(sql, paramters);
        }




        public DataSourceResult QueryPage(DataSourceRequest request)
        {
            IDictionary<string, object> paramters = new Dictionary<string, object>();
            var sql = GetQueryDataBaseTableSql(string.Empty, paramters);
            
            return Page<GenTable>(sql, request);
        }


        public DataTable QueryColumns(string tableName)
        {
            Guard.NotNullOrEmpty(tableName, "tableName");
            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tableName", tableName);
            var sql = GetQueryDataBaseTableColumnSql(tableName);
            return  ExecuteDataTable(sql, parameters);
        }
    }
}
