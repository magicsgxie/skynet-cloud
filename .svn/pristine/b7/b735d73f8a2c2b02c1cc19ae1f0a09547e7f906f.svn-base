using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Mapping.Fluent
{
    /// <summary>
    /// 实体关系映射类
    /// </summary>
    public class ClassMap
    {
        /// <summary>
        /// 实体类型
        /// </summary>
        public Type EntityType { get; internal set; }

        internal TableAttribute table;
        internal Dictionary<MemberInfo, MemberAttribute> members = new Dictionary<MemberInfo, MemberAttribute>();
        internal List<MemberInfo> IgnoreMembers = new List<MemberInfo>();

        internal EntityMapping CreateMapping()
        {
            var entity = new EntityMapping { entityType = EntityType };
            if (table == null)
                table = entity.entityType.GetAttribute<TableAttribute>(false);
            if (table != null)
            {
                entity.tableName = table.Name.IsNullOrEmpty()
                    ? entity.entityType.Name
                    : table.Name;
                entity.@readonly = table.Readonly;
                entity.schema = table.Schema;
                entity.databaseName = table.DatabaseName;
                entity.serverName = table.Server;
            }
            else
                entity.tableName = entity.entityType.Name;

            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            var allMembers = new Dictionary<MemberInfo, MemberMapping>();
            foreach (var k in members.Keys)
                allMembers[k] = new MemberMapping(k, members[k], entity);

            var items = from m in entity.entityType
                       .GetFields(bindingFlags)
                       .Where(p => !members.ContainsKey(p))
                       .Where(p => !IgnoreMembers.Contains(p))
                       .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                       .Where(p => !p.Name.Contains("k__BackingField"))
                       .Where(p => !p.IsInitOnly)
                       .Cast<MemberInfo>()
                       .Union(entity.entityType
                           .GetProperties(bindingFlags)
                           .Where(p => !members.ContainsKey(p))
                           .Where(p => !IgnoreMembers.Contains(p))
                           .Where(p => p.CanRead && p.CanWrite)
                           .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                           .Cast<MemberInfo>()
                           ).Distinct()
                        let att = m.GetAttribute<MemberAttribute>(true)
                        select new UWay.Skynet.Cloud.Data.Mapping.MemberMapping(m, att, entity);

            foreach (var item in items)
                allMembers[item.member] = item;

            entity.innerMappingMembers = allMembers.Values.ToArray();
            return entity;
        }
    }



}
