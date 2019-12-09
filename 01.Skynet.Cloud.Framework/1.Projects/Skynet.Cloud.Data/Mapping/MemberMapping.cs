// <copyright file="MemberMapping.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using System;
using System.Linq;
using System.Reflection;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Internal;
using UWay.Skynet.Cloud.Data.LinqToSql;
using UWay.Skynet.Cloud.Mapping;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Mapping
{


    class MemberMapping : IMemberMapping
    {
        internal MemberInfo member;
        internal MemberInfo storageMember;

        internal bool isColumn;
        internal bool isPrimaryKey;
        internal bool isComputed;
        internal bool isUpdatable;
        internal bool isGenerated;
        internal bool isVersion;
        internal bool isRelationship;
        internal bool isManyToOne;

        internal string alias;
        internal string columnName;
        internal SqlType sqlType;
        internal Type memberType;
        internal string sequenceName;

        internal string thisKey;
        internal Type relatedEntityType;
        internal string otherKey;
        internal EntityMapping entity;
        internal EntityMapping relatedEntity;

        internal Getter getter;
        internal Setter setter;

        internal MemberMapping(MemberInfo member, MemberAttribute attribute, EntityMapping entity)
        {
            this.member = member;
            this.entity = entity;

            memberType = member.GetMemberType();
            var isEnumerableType = memberType != Types.String
                && memberType != typeof(byte[])
                && Types.IEnumerable.IsAssignableFrom(memberType);

            if (attribute == null)
            {
                InitializeConversionMapping(isEnumerableType);
            }
            else
                InitializeAttributeMapping(attribute, isEnumerableType);

            getter = member.GetGetter();
            if (storageMember != null)
                setter = storageMember.GetSetter();
            else
                setter = member.GetSetter();
            if (columnName == null)
                columnName = member.Name;
        }

        private void InitializeAttributeMapping(MemberAttribute attribute, bool isEnumerableType)
        {
            var column = attribute as ColumnAttribute;
            var association = attribute as AbstractAssociationAttribute;

            if (attribute.Storage.HasValue())
            {
                storageMember = entity.entityType.GetMember(attribute.Storage.Trim(), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault();
                if (storageMember != null && storageMember.MemberType != MemberTypes.Field
                    && storageMember.MemberType != MemberTypes.Property)
                    storageMember = null;
            }

            isColumn = column != null;
            if (isColumn)
                IntiailizeColumnAttribute(column);

            isRelationship = association != null;

            if (isRelationship)
                InitializeAssociationAttribute(isEnumerableType, association);
        }

        private void InitializeAssociationAttribute(bool isEnumerableType, AbstractAssociationAttribute association)
        {
            isManyToOne = association.IsForeignKey;
            if (isEnumerableType)
            {
                if (isManyToOne)
                    throw new MappingException("Foreign Key member type should be entity class not collection type.");
                relatedEntityType = ReflectionHelper.GetElementType(memberType);
            }
            else
            {
                relatedEntityType = memberType;
            }
            thisKey = association.ThisKey;
            otherKey = association.OtherKey;
            if (relatedEntityType == entity.entityType)
                relatedEntity = entity;
        }

        private void IntiailizeColumnAttribute(ColumnAttribute column)
        {
            alias = column.Alias;
            columnName = column.Name.HasValue() ? column.Name : member.Name;
            isComputed = column is ComputedColumnAttribute;
            sqlType = SqlType.Get(memberType, column);

            var id = column as IdAttribute;
            isPrimaryKey = id != null;
            if (id != null)
            {
                isGenerated = isPrimaryKey && id.IsDbGenerated;
                sequenceName = id.SequenceName;
            }

            isUpdatable = !isPrimaryKey && !isComputed;
            var version = column as VersionAttribute;
            if (version != null)
            {
                switch (sqlType.DbType)
                {
                    case DBType.Int16:
                    case DBType.Int32:
                    case DBType.Int64:
                        isVersion = true;
                        break;
                    default:
                        throw new MappingException(string.Format(Res.VersionMemberTypeInvalid, sqlType.DbType.ToString(), this.entity.entityType.Name + "." + member.Name));
                }
            }
        }

        /// <summary>
        /// 初始化约定映射
        /// </summary>
        /// <param name="isEnumerableType"></param>
        private void InitializeConversionMapping(bool isEnumerableType)
        {
            if (!isEnumerableType)
            {
                var underlyingType = memberType;
                if (underlyingType.IsNullable())
                    underlyingType = Nullable.GetUnderlyingType(underlyingType);
                if (underlyingType.IsEnum)
                    underlyingType = Enum.GetUnderlyingType(underlyingType);
                if (Converter.IsPrimitiveType(underlyingType)
                    || memberType == typeof(byte[]))
                {
                    isColumn = true;
                    columnName = MappingConversion.Current.ColumnName(member.Name);
                    bool required = false;
                    int length = 0;
                    sqlType = SqlType.Get(underlyingType);
                    isUpdatable = true;

                    if (EFDataAnnotiationAdapter.Instance != null)
                        PopulateEFDataAnnotitions();
                    if (DataAnnotationMappingAdapter.Instance != null)
                    {
                        object attr = null;
#if !SDK35

                        attr = member.GetCustomAttributes(DataAnnotationMappingAdapter.KeyAttributeType, false).FirstOrDefault();
                        isPrimaryKey = attr != null;
#endif
                        attr = member.GetCustomAttributes(DataAnnotationMappingAdapter.RequiredAttributeType, false).FirstOrDefault();
                        required = attr != null;

                        attr = member.GetCustomAttributes(DataAnnotationMappingAdapter.StringLengthAttributeType, false).FirstOrDefault();
                        if (attr != null)
                            length = (int)DataAnnotationMappingAdapter.Instance.StringLength.Length(attr);
                    }

                    sqlType = SqlType.Get(underlyingType, new ColumnAttribute { IsNullable = !required, Length = length });

                }
                else//manyToOne
                {
                    isRelationship = true;
                    relatedEntityType = underlyingType;
                    isManyToOne = isRelationship && !isEnumerableType;

                    if (relatedEntityType == entity.entityType)
                        relatedEntity = entity;

                    if (DataAnnotationMappingAdapter.Instance != null)
                    {
#if !SDK35
                        var assAtt = member.GetCustomAttributes(DataAnnotationMappingAdapter.AssociationAttributeType, false).FirstOrDefault();
                        if (assAtt != null)
                        {
                            thisKey = DataAnnotationMappingAdapter.Instance.Association.ThisKey(assAtt) as string;
                            otherKey = DataAnnotationMappingAdapter.Instance.Association.OtherKey(assAtt) as string;
                        }
#endif
                    }
                }

            }
            else
            {
                isRelationship = true;
                relatedEntityType = ReflectionHelper.GetElementType(memberType);
                isManyToOne = isRelationship && !isEnumerableType;

                if (relatedEntityType == entity.entityType)
                    relatedEntity = entity;
                if (DataAnnotationMappingAdapter.Instance != null)
                {
#if !SDK35
                    var assAtt = member.GetCustomAttributes(DataAnnotationMappingAdapter.AssociationAttributeType, false).FirstOrDefault();
                    if (assAtt != null)
                    {
                        thisKey = DataAnnotationMappingAdapter.Instance.Association.ThisKey(assAtt) as string;
                        otherKey = DataAnnotationMappingAdapter.Instance.Association.OtherKey(assAtt) as string;
                    }
#endif
                }
            }
        }

        private void PopulateEFDataAnnotitions()
        {
            var attr = member.GetCustomAttributes(EFDataAnnotiationAdapter.ColumndAttributeType, false).FirstOrDefault();
            if (attr != null)
            {
                var cn = EFDataAnnotiationAdapter.Instance.Column.Name(attr) as string;
                if (cn.HasValue())
                    columnName = cn;
            }

            attr = member.GetCustomAttributes(EFDataAnnotiationAdapter.DatabaseGeneratedAttributeType, false).FirstOrDefault();
            if (attr != null)
            {
                if ((int)Converter.Convert(EFDataAnnotiationAdapter.Instance.DatabaseGenerated.DatabaseGeneratedOption(attr), Types.Int32) == 1)
                    isGenerated = true;
            }

            attr = member.GetCustomAttributes(EFDataAnnotiationAdapter.MaxLengthAttributeType, false).FirstOrDefault();
            if (attr != null)
            {
                var length = (int)Converter.Convert(EFDataAnnotiationAdapter.Instance.MaxLength.Length(attr), Types.Int32);
                if (length > 0)
                    sqlType = SqlType.Get(sqlType.DbType, length);
            }
        }

        internal void OnNotify(EntityMapping entityModel)
        {
            if (isRelationship)
            {
                if (entityModel == entity)
                {
                    thisKeyMembers = GetReferencedMembers(thisKey, "Association.ThisKey", entity);
                }
                else if (relatedEntity == null)
                {
                    if (relatedEntityType == entityModel.entityType)
                        relatedEntity = entityModel;
                    else if (relatedEntityType.FullName.Contains(ULinq.StrEntityRefType)
                        && relatedEntityType.GetGenericArguments()[0] == entityModel.entityType)
                        relatedEntity = entityModel;
                }

                if (relatedEntity != null)
                {
                    otherKeyMembers = GetReferencedMembers(otherKey, "Association.OtherKey", relatedEntity);
                }
            }
        }

        public override string ToString()
        {
            return entity.ToString() + "." + member.Name;
        }

        public string ColumnName
        {
            get { return columnName; }
        }

        public string SequenceName
        {
            get { return sequenceName; }
        }
        public string AliasName
        {
            get { return alias; }
        }
        public MemberInfo Member
        {
            get { return this.member; }
        }
        public MemberInfo StorageMember
        {
            get { return this.storageMember; }
        }
        public Type MemberType
        {
            get { return memberType; }
        }
        public SqlType SqlType
        {
            get { return sqlType; }
        }

        public Type RelatedEntityType
        {
            get { return relatedEntityType; }
        }

        public IEntityMapping RelatedEntity
        {
            get
            {
                return relatedEntity;
            }
        }

        private static readonly char[] separators = new char[] { ' ', ',', '|' };
        private static IMemberMapping[] GetReferencedMembers(string names, string sourceName, EntityMapping entity)
        {
            return names.Split(separators).Select(n => GetReferencedMember(n, sourceName, entity)).ToArray();
        }
        private static IMemberMapping GetReferencedMember(string name, string sourceName, EntityMapping entity)
        {
            var mm = entity.GetMappingMember(name);
            if (mm == null)
                throw new InvalidOperationException(string.Format(Res.AttrbuteMappingError, entity.entityType.Name, name, sourceName, entity.entityType.Name));
            return mm;
        }

        static readonly IMemberMapping[] EmptyMemberModels = new IMemberMapping[0];

        IMemberMapping[] thisKeyMembers = EmptyMemberModels;
        public IMemberMapping[] ThisKeyMembers
        {
            get
            {
                return thisKeyMembers;
            }
        }

        private IMemberMapping[] otherKeyMembers = EmptyMemberModels;
        public IMemberMapping[] OtherKeyMembers
        {
            get
            {
                return otherKeyMembers;
            }
        }

        public IEntityMapping Entity
        {
            get { return this.entity; }
        }
        public bool IsColumn
        {
            get { return isColumn; }
        }
        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
        }
        public bool IsComputed
        {
            get { return isComputed; }
        }
        public bool IsUpdatable
        {
            get { return isUpdatable; }
        }
        public bool IsGenerated
        {
            get { return isGenerated; }
        }
        public bool IsVersion
        {
            get { return isVersion; }
        }

        public bool IsRelationship
        {
            get { return isRelationship; }
        }
        public bool IsManyToOne
        {
            get { return isManyToOne; }
        }
        public bool IsUniqule
        {
            get { return false; }
        }

        public bool IsCheck
        {
            get { return false; }
        }
        public object GetValue(object target)
        {
            return getter(target);
        }

        public void SetValue(object target, object value)
        {
            setter(target, value);
        }
    }
}
