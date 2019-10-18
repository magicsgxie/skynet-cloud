using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Skynet.Cloud.Nom.Repository;
using UWay.Skynet.Cloud.Nom.Service.Interface;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Linq;


namespace UWay.Skynet.Cloud.Nom.Service
{
    public class EnumsService : IEnumsService
    {
        public long AddEnumType(EnumType enumType)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new EnumsRepository(context).AddEnumTypes(enumType);
            }
        }

        public long AddEnumValue(EnumValue enumType)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new EnumsRepository(context).AddEnumValue(enumType);
            }
        }

        public void DeleteEnumType(long id)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                var r = new EnumsRepository(context);
                r.UsingTransaction(() =>
                {
                    r.DelEnumValues(id);
                    r.DelEnumTypes(id);
                });
            }
        }

        public int DeleteEnumValue(long[] id)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                var r = new EnumsRepository(context);
                return r.DelEnumValues(id);
            }
        }

        public EnumType GetByFieldName(string fieldName)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                var r = new EnumsRepository(context);
                return r.GetEnumTypes().SingleOrDefault(p => p.NameEn.Equals(fieldName,StringComparison.InvariantCultureIgnoreCase));
            }
        }

        public DataSourceResult Page(DataSourceRequest request)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new EnumsRepository(context).GetEnumTypes().ToDataSourceResult(request);
            }
        }

        public List<EnumValue> Query(string fieldName)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                var r = new EnumsRepository(context);
                var q1 = r.GetEnumTypes();
                var q2 = r.GetEnumValues();
                var q = from p in q1
                        join c in q2
                        on p.Id equals c.TypeId
                        where p.NameEn == fieldName
                        select c;
                return q.ToList();
            }
        }

        public int UpdateEnumType(EnumType enumType)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new EnumsRepository(context).UpdateEnumTypes(enumType);
            }
        }

        public int UpdateEnumValue(EnumValue enumType)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new EnumsRepository(context).UpdateEnumValue(enumType);
            }
        }
    }
}
