using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Nom.Entity;


namespace UWay.Skynet.Cloud.Nom.Repository
{
    public class EnumsRepository : ObjectRepository
    {
        public EnumsRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public long AddEnumTypes(EnumType item)
        {
            return Add<EnumType>(item);
        }

        public int UpdateEnumTypes(EnumType item)
        {
            return Update<EnumType>(item);
        }


        public int AddEnumValue(EnumValue item)
        {
            return Add<EnumValue>(item);
        }

        public int UpdateEnumValue(EnumValue item)
        {
            return Update<EnumValue>(item);
        }

        public int DelEnumTypes(long id)
        {
            return Delete<EnumValue>(p => p.Id == id);
        }
        public int DelEnumValues(long id)
        {
            return Delete<EnumValue>(p => p.TypeId == id);
        }


        public int DelEnumValues(long[] idArrays)
        {
            return Delete<EnumValue>(p => idArrays.Contains(p.TypeId));
        }

        public IQueryable<EnumType> GetEnumTypes()
        {
            return CreateQuery<EnumType>();
        }

        public IQueryable<EnumValue> GetEnumValues()
        {
            return CreateQuery<EnumValue>();
        }
    }
}
