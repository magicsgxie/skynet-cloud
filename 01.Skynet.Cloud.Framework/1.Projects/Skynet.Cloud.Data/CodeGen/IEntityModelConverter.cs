using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UWay.Skynet.Cloud.Data.Schema;

namespace UWay.Skynet.Cloud.Data.CodeGen
{
    public interface IEntityModelConverter
    {
        IEntityModel Convert(ITableSchema table);
    }

    public class EntityModelConverter : IEntityModelConverter
    {
        public EntityModelConverter()
        {
            NamingConversion = Data.CodeGen.NamingConversion.Default;
        }
        public INamingConversion NamingConversion { get; set; }

        public IEntityModel Convert(ITableSchema table)
        {
            var entity = new EntityModel { Table = table };
            entity.ClassName = NamingConversion.ClassName(table.TableName);
            foreach (var col in table.AllColumns)
            {
                var member = new MemberModel { Column = col, MemberName = PopulateMemberName(entity, NamingConversion.PropertyName(col.ColumnName)) };
                entity.members.Add(member);
            }

            foreach (var col in table.ForeignKeys)
            {
                var member = new NavigationMemberModel { IsManyToOne = true, Relation = col, MemberName = PopulateMemberName(entity, NamingConversion.ManyToOneName(col)) };
                member.DeclareTypeName = NamingConversion.ClassName(col.OtherTable.TableName);
                entity.manyToOnes.Add(member);
            }

            foreach (var col in table.Children)
            {
                var member = new NavigationMemberModel { Relation = col, MemberName = PopulateMemberName(entity, NamingConversion.QueryableName(col.OtherTable.TableName)) };
                member.DeclareTypeName = NamingConversion.ClassName(col.OtherTable.TableName);
                entity.manyToOnes.Add(member);
            }
            return entity;
        }

        private string PopulateMemberName(EntityModel entity, string sourceMemberName)
        {
            var source = sourceMemberName;
            var memberName = source;

            var i = 1;
            if (string.Equals(entity.ClassName, memberName))
            {
                memberName = source + i;
                i++;
            }

            var memberNames = entity.members.Select(p => p.MemberName)
                .Union(entity.manyToOnes.Select(p => p.MemberName))
                .Union(entity.oneToMany.Select(p => p.MemberName))
                .ToArray();

            while (memberNames.Contains(memberName))
            {
                memberName = source + i;
                i++;
            }

            return memberName;
        }
    }
}
