using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Nom.Entity;

namespace UWay.Skynet.Cloud.Nom.Repository
{
    public class ImportDataRepository : ObjectRepository
    {
        public ImportDataRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public ImportTemplate Get(object id)
        {
            return GetByID<ImportTemplate>(id);
        }

        public int Update(ImportTemplate entity)
        {
            return Update<ImportTemplate>(entity);
        }

        public void Update(IEnumerable<ImportTemplate> entitys)
        {
            Batch<int, ImportTemplate>(entitys, (u,v)=> u.Update(v));
        }

        public void Add(IEnumerable<ImportTemplate> entitys)
        {
            Batch<int, ImportTemplate>(entitys, (u, v) => u.Insert(v));
        }

        public int Add(ImportTemplate entity)
        {
            return Add<ImportTemplate>(entity);
        }

        public IQueryable<ImportTemplate> CreateQuery()
        {
            return CreateQuery<ImportTemplate>();
        }


        public DataTable CreateQuery(SelectQuery selectQuery, IDictionary<string, object> parameters)
        {
            return ExecuteDataTable(selectQuery, parameters);
        }
    }
}
