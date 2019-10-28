using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Nom.Entity;

namespace UWay.Skynet.Cloud.Nom.Repository
{
    public class ImportDataFieldRepository : ObjectRepository
    {
        public ImportDataFieldRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public ImportDataField Get(object id)
        {
            return GetByID<ImportDataField>(id);
        }

        public int Update(ImportDataField entity)
        {
            return Update<ImportDataField>(entity);
        }

        public void Update(IEnumerable<ImportDataField> entitys)
        {
            Batch<int, ImportDataField>(entitys, (u, v) => u.Update(v));
        }

        public void Add(IEnumerable<ImportDataField> entitys)
        {
            Batch<int, ImportDataField>(entitys, (u, v) => u.Insert(v));
        }

        public int Add(ImportDataField entity)
        {
            return Add<ImportDataField>(entity);
        }

        public IQueryable<ImportDataField> CreateQuery()
        {
            return CreateQuery<ImportDataField>();
        }
    }
}
