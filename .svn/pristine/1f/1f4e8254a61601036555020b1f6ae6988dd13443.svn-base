using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using UWay.Skynet.Cloud.Reflection;
using System.Reflection;
using UWay.Skynet.Cloud;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Mapping.Internal
{
    sealed class DataTableMapper : MapperBase
    {
        private bool isFromDataTable;
        private bool isWeakTypeDataTable;

        private Type elementType;

        class ColumnModel
        {
            public string ColumnName;
            public Type DataType;
            Type MemberType;
            public bool AllowDBNull;

            public Action<object, DataRow> SetColumnValue;
            bool IsEnum;
            static readonly MappingEngine mappingEngine = Mapper.Current as MappingEngine;

            public ColumnModel(MemberInfo member)
            {
                MemberType = member.GetMemberType();
                var sourceType = MemberType;
                AllowDBNull = sourceType.IsNullable();

                if (!AllowDBNull)
                {
                    IsEnum = sourceType.IsEnum;
                    if (IsEnum)
                        DataType = Enum.GetUnderlyingType(sourceType);
                    else
                        DataType = sourceType;
                }
                else
                {
                    sourceType = Nullable.GetUnderlyingType(sourceType);
                    IsEnum = sourceType.IsEnum;
                    if (IsEnum)
                        DataType = Enum.GetUnderlyingType(sourceType);
                    else
                        DataType = sourceType;
                }
                ColumnName = member.Name;


                var getter = member.GetGetter();
                if (AllowDBNull)
                {
                    var p = MemberType.GetProperty("Value");

                    if (IsEnum)
                    {
                        SetColumnValue = (entity, row) =>
                        {
                            var v = getter(entity);
                            if (v == null)
                                row[ColumnName] = DBNull.Value;
                            else
                                row[ColumnName] = Converter.Convert(p.GetValue(v, null), DataType);
                        };
                    }
                    else
                    {
                        SetColumnValue = (entity, row) =>
                        {
                            var v = getter(entity);
                            if (v == null)
                                row[ColumnName] = DBNull.Value;
                            else
                                row[ColumnName] = p.GetValue(v, null);
                        };
                    }

                }
                else
                {
                    if (DataType == Types.String)
                    {
                        AllowDBNull = true;
                        SetColumnValue = (entity, row) =>
                        {
                            var v = getter(entity);
                            row[ColumnName] = v == null ? DBNull.Value : v;
                        };
                    }
                    else
                    {
                        if (IsEnum)
                            SetColumnValue = (entity, row) => row[ColumnName] = Converter.Convert(getter(entity), DataType);
                        else
                            SetColumnValue = (entity, row) => row[ColumnName] = getter(entity);
                    }
                }
            }

        }
        private ColumnModel[] SourceMembers;

        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
        public DataTableMapper(Type fromType, Type toType)
            : base(fromType, toType)
        {
            isFromDataTable = Types.DataTable.IsAssignableFrom(fromType);
            if (!isFromDataTable)
            {
                elementType = TypeHelper.GetElementType(fromType);
                isWeakTypeDataTable = toType == Types.DataTable;
                SourceMembers = elementType.GetFields(bindingFlags | BindingFlags.GetField)
                               .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                               .Where(p => !p.Name.Contains("k__BackingField"))
                               .Where(p => Converter.IsPrimitiveType(p.FieldType))
                               .Cast<MemberInfo>()
                               .Union(elementType
                                   .GetProperties(bindingFlags)
                                   .Where(p => p.CanRead)
                                   .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                                   .Where(p => Converter.IsPrimitiveType(p.PropertyType))
                                   .Cast<MemberInfo>()
                                   )
                               .Distinct()
                               .Select(p => new ColumnModel(p))
                               .ToArray();
            }
        }

        public override void Map(ref object from, ref object to)
        {
            if (from == null)
                return;

            if (isFromDataTable)
                new DataReaderMapper(_Info.To).Map(ref from, ref to);
            else
            {
                DataTable dt = to as DataTable;
                if (dt == null)
                    dt = ObjectCreator.Create(_Info.To) as DataTable;
                if (string.IsNullOrEmpty(dt.TableName))
                    dt.TableName = elementType.Name;
                PopulateColumns(dt);
                FillDataTable(from as IEnumerable, dt);
                to = dt;
            }

        }

        private void PopulateColumns(DataTable dt)
        {
            var columns = dt.Columns.Cast<DataColumn>().ToArray();
            if (columns.Length == 0)
            {
                foreach (var item in SourceMembers)
                    dt.Columns.Add(new DataColumn { ColumnName = item.ColumnName, AllowDBNull = item.AllowDBNull, DataType = item.DataType });
            }
            else
            {
                foreach (var item in SourceMembers)
                {
                    var c = columns.FirstOrDefault(p => string.Equals(p.ColumnName, item.ColumnName));
                    if (c == null)
                        dt.Columns.Add(new DataColumn { ColumnName = item.ColumnName, AllowDBNull = item.AllowDBNull, DataType = item.DataType });
                }
            }
        }


        private void FillDataTable(IEnumerable from, DataTable to)
        {
            foreach (var entity in from)
            {
                var row = to.NewRow();
                foreach (var m in SourceMembers)
                    m.SetColumnValue(entity, row);
                to.Rows.Add(row);
            }

            to.AcceptChanges();
        }
    }
}
