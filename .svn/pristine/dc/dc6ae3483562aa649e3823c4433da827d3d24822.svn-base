using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UWay.Skynet.Cloud;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Reflection;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Mapping.Internal
{
    [DebuggerDisplay("{From.Name}->{To.Name}")]
    class MemberMapping
    {
        internal MemberModel From;
        internal MemberModel To;

        internal readonly Getter[] MemberPaths = DefaultMembers;

        internal readonly string Key;
        internal readonly IMapper MemberMapper;
        private static readonly MethodInfo CreateMapperMethod = typeof(IMappingEngine).GetMethod("CreateMapper");

        private static readonly Getter[] DefaultMembers = new Getter[0];
        private readonly MapperInfo _Info;


        public MemberMapping(MapperInfo mapperInfo, MemberModel from, MemberModel to, string fromMemberPath)
        {
            _Info = mapperInfo;
            From = from;
            To = to;
            from.GetMember = from.Member.GetGetter();
            to.SetMember = to.Member.GetSetter();

            //FromMemberPath = fromMemberPath;
            Key = From.Type.FullName + "->" + To.Type.FullName;

            if (to.Type != from.Type && !to.Type.IsAssignableFrom(from.Type))
                MemberMapper = CreateMapperMethod.MakeGenericMethod(from.Type, to.Type).Invoke(Mapper.Current, null) as IMapper;

            if (!string.IsNullOrEmpty(fromMemberPath))
            {
                var members = fromMemberPath.Split('.');
                var length = members.Length;
                if (length > 1)
                {
                    MemberPaths = new Getter[length - 1];
                    var subObjectType = from.Type;
                    for (int i = 1; i < length; i++)
                    {
                        var subMember = subObjectType
                            .GetMember(members[i])
                            .FirstOrDefault();
                        if (subMember == null)
                            break;
                        MemberPaths[i - 1] = subMember.GetGetter();
                        subObjectType = subMember.GetMemberType();
                    }
                }
            }
        }

        public object GetSourceMemberValue(ref object from)
        {
            var value = From.GetMember(from);

            if (MemberPaths.Length != 0)
            {
                foreach (var path in MemberPaths)
                    value = path(value);
            }

            return value;
        }

        public void SetTargetMemberValue(ref object to, ref object value)
        {
            if (_Info.CanUsingConverter(Key))
                value = _Info.converters[Key].DynamicInvoke(value);
            else if (MemberMapper != null)
            {
                if (value != null)
                {
                    var valueType = value.GetType();
                    if (MemberPaths.Length == 0)
                        value = MemberMapper.Map(value, valueType, To.Type); //Mapper.Map(value, item.From.Type, item.To.Type);
                    else if (To.Type != valueType && To.Type.IsAssignableFrom(valueType))
                        value = Mapper.Map(value, value.GetType(), To.Type);
                }
            }
            To.SetMember(to, value);
        }

    }


    internal class MemberSetterInfo
    {
        public string MemberName;
        public Delegate Delegate;
    }
    sealed class MapperInfo : IMapperInfo
    {
        const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

        public Type From { get; internal set; }
        public Type To { get; internal set; }
        internal string Key;

        public bool IgnoreCase { get; internal set; }
        public bool IgnoreUnderscore { get; internal set; }
        public string[] IgnoreSourceMembers { get { return ignoreSourceMembers == null ? new string[0] : ignoreSourceMembers.ToArray(); } }
        public string[] IgnoreDestinationMembers { get { return ignoreDestinationMembers == null ? new string[0] : ignoreDestinationMembers.ToArray(); } }



        internal Dictionary<Setter, MemberSetterInfo> memberMappings;
        internal HashSet<string> ignoreSourceMembers;
        internal HashSet<string> ignoreDestinationMembers;
        internal Dictionary<string, Delegate> converters;

        internal bool IsFromNullable;
        internal bool IsToNullable;

        public MapperInfo()
        {
            IgnoreCase = true;
            IgnoreUnderscore = true;
        }

        internal bool CanUsingConverter(string key)
        {
            return converters != null && converters.ContainsKey(key) && converters[key] != null;
        }

        internal Func<string, string, bool> membersMatcher;

        List<MemberMapping> mappings;

        internal List<MemberMapping> Mappings
        {
            get
            {
                if (mappings != null)
                    return mappings;

                IsFromNullable = From.IsNullable();
                IsToNullable = To.IsNullable();

                var fromMembers = SourceMembers;
                var items = new List<MemberMapping>();

                foreach (var toMember in DestinationMembers)
                {
                    string fromMemberPath = null;
                    MemberInfo fromMember = fromMembers.FirstOrDefault(m => IsMatchMember(m, toMember, ref fromMemberPath));
                    if (fromMember == null)
                        continue;

                    if (!string.IsNullOrEmpty(fromMemberPath))
                        fromMemberPath = fromMemberPath.Remove(0, 1);

                    items.Add(new MemberMapping(this, GetMappingItem(fromMember, true), GetMappingItem(toMember, false), fromMemberPath));
                }

                mappings = items;
                return mappings;
            }
        }

        List<MemberInfo> sourceMembers;
        internal List<MemberInfo> SourceMembers
        {
            get
            {
                if (sourceMembers != null)
                    return sourceMembers;

                var ignoreFromMembers = IgnoreSourceMembers;

                var fromType = From.IsNullable() ? Nullable.GetUnderlyingType(From) : From;
                sourceMembers = fromType
                           .GetFields(bindingFlags)
                           .Where(f => !IsMatchIgnoreMember(f.Name, ignoreFromMembers))
                           .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                           .Cast<MemberInfo>()
                           .Union(
                               GetAllProperties(From)
                               .Where(p => p.CanRead && !IsMatchIgnoreMember(p.Name, ignoreFromMembers))
                               .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                               .Cast<MemberInfo>()
                               )
                           .Union(
                               GetAllMethods(From)
                               .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                               .Where(p => p.ReturnType != Types.Void)
                               .Where(p => !p.Name.StartsWith("get_"))
                               .Where(p => !p.Name.StartsWith("set_"))
                               .Where(p => p.Name.StartsWith("Get"))
                               .Where(p => p.Name != "GetHashCode" && p.Name != "GetType")
                               .Cast<MemberInfo>())
                               .ToList();

                return sourceMembers;
            }
        }

        List<MemberInfo> destinationMembers;
        internal List<MemberInfo> DestinationMembers
        {
            get
            {
                if (destinationMembers != null)
                    return destinationMembers;
                if (memberMappings == null)
                    memberMappings = new Dictionary<Setter, MemberSetterInfo>(0);

                var ignoreToMembers = IgnoreDestinationMembers
                           .Union(memberMappings.Values.ConvertAll<MemberSetterInfo, string>(m => m.MemberName))
                           .ToArray();

                var toType = To.IsNullable() ? Nullable.GetUnderlyingType(To) : To;
                destinationMembers = toType
                    .GetFields(bindingFlags)
                    .Where(f => !f.IsInitOnly)//确保可写
                    .Where(f => !IsMatchIgnoreMember(f.Name, ignoreToMembers))
                    .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                    .Cast<MemberInfo>()
                    .Union(
                        GetAllProperties(To)
                        .Where(p => p.CanWrite && !IsMatchIgnoreMember(p.Name, ignoreToMembers))
                        .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                        .Cast<MemberInfo>()
                        )
                    .Union(
                       GetAllMethods(To)
                       .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                       .Where(p => p.ReturnType == Types.Void)
                       .Where(p => !p.Name.StartsWith("get_"))
                       .Where(p => !p.Name.StartsWith("set_"))
                       .Where(p => p.Name.StartsWith("Set"))
                        .Where(p => p.Name != "GetHashCode" && p.Name != "GetType")
                       .Cast<MemberInfo>())
                     .ToList();

                return destinationMembers;
            }
        }

        static IEnumerable<PropertyInfo> GetAllProperties(Type type)
        {
            return type.GetInterfaces().Concat(new Type[] { type }).SelectMany(itf => itf.GetProperties(bindingFlags)).Distinct();
        }

        static IEnumerable<MethodInfo> GetAllMethods(Type type)
        {
            return type.GetInterfaces().Concat(new Type[] { type }).SelectMany(itf => itf.GetMethods(bindingFlags)).Distinct();
        }

        private static MemberModel GetMappingItem(MemberInfo m, bool isFrom)
        {
            switch (m.MemberType)
            {
                case MemberTypes.Field:
                    var field = m as FieldInfo;
                    return new MemberModel { Member = field, Type = field.FieldType };
                case MemberTypes.Property:
                    var prop = m as PropertyInfo;
                    return new MemberModel { Member = isFrom ? prop.GetGetMethod() : prop.GetSetMethod(), Type = prop.PropertyType };
                case MemberTypes.Method:
                    var method = m as MethodInfo;
                    return new MemberModel { Member = method, Type = isFrom ? method.ReturnType : method.GetParameterTypes()[0] };
            }

            return new MemberModel();
        }

        private bool IsMatchMember(MemberInfo fromMember, MemberInfo toMember, ref string fromMemberPath)
        {
            var isFromFieldOrProperty = fromMember.MemberType == MemberTypes.Field || fromMember.MemberType == MemberTypes.Property;
            var isToFieldOrProperty = toMember.MemberType == MemberTypes.Field || toMember.MemberType == MemberTypes.Property;

            var fromName = fromMember.Name.Replace("get_", string.Empty).Replace("Get", string.Empty);
            var toName = toMember.Name.Replace("set_", string.Empty).Replace("Set", string.Empty);

            var flag = IsMatchMemberName(fromName, toName);
            if (flag)
                return true;

            flag = IsMatchFlattingMember(fromMember.GetMemberType(), fromName, toName, ref fromMemberPath);
            if (flag)
                return true;

            if (membersMatcher != null)
                return membersMatcher(fromName, toName);

            fromMemberPath = null;
            return false;
        }

        private bool IsMatchFlattingMember(Type fromType, string fromName, string toName, ref string fromMemberPath)
        {
            if (!toName.StartsWith(fromName))
                return false;

            fromMemberPath = fromMemberPath + "." + fromName;

            toName = toName.Remove(0, fromName.Length);
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            foreach (var f in fromType.GetFields(flags).Where(p => !p.HasAttribute<IgnoreAttribute>(true)))
            {
                fromName = f.Name;
                if (IsMatchMemberName(f.Name, toName))
                {
                    fromMemberPath = fromMemberPath + "." + fromName;
                    return true;
                }
                if (IsMatchFlattingMember(f.FieldType, fromName, toName, ref fromMemberPath))
                    return true;
            }

            foreach (var f in fromType.GetProperties(flags).Where(p => !p.HasAttribute<IgnoreAttribute>(true)))
            {
                fromName = f.Name;
                if (IsMatchMemberName(f.Name, toName))
                {
                    fromMemberPath = fromMemberPath + "." + fromName;
                    return true;
                }
                if (IsMatchFlattingMember(f.PropertyType, fromName, toName, ref fromMemberPath))
                    return true;
            }

            foreach (var f in fromType.GetMethods(flags).Where(m => m.Name.StartsWith("Get") && m.ReturnType != Types.Void && !m.HasAttribute<IgnoreAttribute>(true)))
            {
                fromName = f.Name;
                if (IsMatchMemberName(f.Name, toName))
                {
                    fromMemberPath = fromMemberPath + "." + fromName;
                    return true;
                }
                if (IsMatchFlattingMember(f.ReturnType, fromName, toName, ref fromMemberPath))
                    return true;
            }

            return false;
        }

        private bool IsMatchMemberName(string fromName, string toName)
        {
            if (IgnoreUnderscore)
                return string.Compare(
                         fromName.Replace("_", string.Empty),
                         toName.Replace("_", string.Empty),
                         IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0;

            return string.Compare(
                    fromName,
                    toName,
                    IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0;
        }

        private bool IsMatchIgnoreMember(string fieldName, string[] ignoreMembers)
        {

            var stringComparision = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            fieldName = IgnoreUnderscore ? fieldName.Replace("_", string.Empty) : fieldName;

            foreach (string ignoreField in ignoreMembers)
            {
                if (string.Compare(fieldName, ignoreField, stringComparision) == 0)
                    return true;
            }

            return false;
        }

        private string[] StringArrayFilter(string[] array)
        {
            if (IgnoreUnderscore)
            {
                int length = array.Length;
                for (int i = 0; i < length; i++)
                    array[i] = array[i].Replace("_", string.Empty);
            }

            return array;
        }
    }
}
