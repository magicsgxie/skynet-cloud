using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using UWay.Skynet.Cloud.Extensions;
using UWay.Skynet.Cloud.Data.LinqToSql;
using UWay.Skynet.Cloud.Data.Mapping;
using UWay.Skynet.Cloud.Data.Mapping.Fluent;
using UWay.Skynet.Cloud.Data.Schema;
using UWay.Skynet.Cloud.Reflection;
using UWay.Skynet.Cloud.ExceptionHandle;
using Microsoft.Extensions.Logging;

namespace UWay.Skynet.Cloud.Data
{
    partial class DbConfiguration
    {
        internal Dictionary<IntPtr, EntityMapping> mappings = new Dictionary<IntPtr, EntityMapping>();
        
        /// <summary>
        /// 注册实体到数据表的映射关系
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public DbConfiguration AddClass<TEntity>()
        {
            var entityType = typeof(TEntity);
            return AddClass(entityType);
        }



        /// <summary>
        /// 注册实体到数据表的映射关系
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public DbConfiguration AddClass(Type entityType)
        {
            Guard.NotNull(entityType, "entityType");
            var entityTypeId = entityType.TypeHandle.Value;
            if (mappings.ContainsKey(entityTypeId))
                throw new RepeatRegistrationException("Repeat register entity mapping for entity '" + entityType.FullName + "'.");

            if (ULinq.Instance == null)
                RegistyMapping(CreateMapping(entityType));
            else
            {
                var dlinqTableAttribute = (Attribute)entityType.GetCustomAttributes(ULinq.Instance.Table.Type, false).FirstOrDefault();
                if (dlinqTableAttribute != null)
                    AddULinqClass(entityType, dlinqTableAttribute);
                else
                    RegistyMapping(CreateMapping(entityType));
            }

            return this;
        }

        /// <summary>
        /// 注册实体到数据表的映射关系
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="fnClassMap"></param>
        /// <returns></returns>
        public DbConfiguration AddClass<TEntity>(Action<ClassMap<TEntity>> fnClassMap)
        {
            Guard.NotNull(fnClassMap, "fnClassMap");
            var entityType = typeof(TEntity);
            var entityTypeId = entityType.TypeHandle.Value;
            if (mappings.ContainsKey(entityTypeId))
                throw new RepeatRegistrationException("Repeat register entity mapping for entity '" + entityType.FullName + "'.");
            var map = new ClassMap<TEntity>();

            fnClassMap(map);

            var mapping = map.CreateMapping();
            AutoMapping(mapping.entityType, mapping);

            RegistyMapping(mapping);
            return this;
        }

        /// <summary>
        /// 注册实体到数据表的映射关系
        /// </summary>
        /// <param name="classMap"></param>
        /// <returns></returns>
        public DbConfiguration AddClass(ClassMap classMap)
        {
            Guard.NotNull(classMap, "classMap");
            var entityType = classMap.EntityType;
            var entityTypeId = entityType.TypeHandle.Value;
            if (mappings.ContainsKey(entityTypeId))
                throw new RepeatRegistrationException("Repeat register entity mapping for entity '" + entityType.FullName + "'.");

            var mapping = classMap.CreateMapping();
            Guard.NotNull(mapping, "entity");

            AutoMapping(mapping.entityType, mapping);

            RegistyMapping(mapping);
            return this;
        }

        /// <summary>
        /// 校验Schema
        /// </summary>
        internal void ValidateSchema()
        {
            //如果不启用映射检查或者数据库还不存在则直接返回
            if (!enableValidateSchema || !DatabaseExists())
                return;
            var errors = new StringBuilder(500);
            foreach (var mapping in mappings.Values)
                ValidateTableSchema(mapping, errors);
            if (errors.Length > 0)
                throw new MappingException(errors.ToString());
        }


        private void ValidateTableSchema(EntityMapping mapping, StringBuilder errors)
        {
            //如果映射的数据库名称和实际的数据库名称不一致，那么有可能是跨库映射则直接返回
            if (mapping.databaseName.HasValue() && mapping.databaseName.ToLower() != DatabaseName)
                return;

            var tb = Schema.Tables.Union(Schema.Views).FirstOrDefault(p => p.TableName.ToLower() == mapping.tableName.ToLower());
            if (tb == null)
                errors.Append("Missing table:" + mapping.tableName).AppendLine();
            else
                ValidateColumns(mapping, tb, errors);
        }

        private void ValidateColumns(EntityMapping mapping, ITableSchema tb, StringBuilder errors)
        {
            var scriptGenerator = this.Option.ScriptGenerator();
            foreach (var m in mapping.innerMappingMembers.Where(p => p.isColumn))
            {
                var c = tb.AllColumns.FirstOrDefault(p => p.ColumnName.ToLower() == m.columnName.ToLower());
                if (c == null)
                {
                    errors.AppendFormat("Missing column: {0} in {1}", m.columnName, tb.TableName).AppendLine();
                    continue;
                }

                var memberType = m.memberType.IsNullable() ? Nullable.GetUnderlyingType(m.memberType) : m.memberType;
                if (c.Type == memberType)
                    continue;

                var currentDbType = scriptGenerator.GetDbType(m.sqlType).ToLower();
                if (!currentDbType.StartsWith(c.ColumnType.ToLower()))
                    errors.AppendFormat("Wrong column type in {0} for column {1}. Found: {2}, Expected {3}", tb.TableName,
                                                            c.ColumnName,
                                                            currentDbType,
                                                            c.ColumnType)
                                                            .AppendLine();
            }
        }

        private void RegistyMapping(EntityMapping mapping)
        {
            foreach (var m in mapping.innerMappingMembers.Where(p => !string.IsNullOrEmpty(p.otherKey)))
                foreach (var e in mappings.Values)
                    m.OnNotify(e);
           
            if (!mappings.ContainsKey(mapping.entityType.TypeHandle.Value))
            {
                mappings[mapping.entityType.TypeHandle.Value] = mapping;
            }
            foreach (var item in mappings.Values)
                foreach (var m in item.mappingMembers.Values)
                    m.OnNotify(mapping);
        }

        /// <summary>
        /// 把单数单词转化为复数单词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Plural(string str)
        {
            return Inflector.Plural(str);
        }

        /// <summary>
        /// 把复数单词转换为单数单词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Singular(string str)
        {
            return Inflector.Singular(str);
        }

        private IMappingConversion mappingConversion = MappingConversion.Default;

        /// <summary>
        /// 设置缺省约定映射策略，比如ClassName到TableName的转换约定，字段或属性到列名的转换约定
        /// </summary>
        /// <param name="mappingConversion"></param>
        /// <returns></returns>
        public DbConfiguration SetMappingConversion(IMappingConversion mappingConversion)
        {
            Guard.NotNull(mappingConversion, "mappingConversion");
            this.mappingConversion = mappingConversion;
            MappingConversion.Current = mappingConversion;
            return this;
        }

        /// <summary>
        /// 设置ClassName到TableName的转换函数
        /// </summary>
        /// <param name="fnClassNameToTalbeName"></param>
        /// <returns></returns>
        public DbConfiguration SetClassNameToTalbeName(Func<string, string> fnClassNameToTalbeName)
        {
            Guard.NotNull(fnClassNameToTalbeName, "fnClassNameToTalbeName");
            this.mappingConversion = new ProxyMappingConversion(fnClassNameToTalbeName);
            return this;
        }

        static readonly Assembly ULinqAssembly = typeof(DbConfiguration).Assembly;
        static readonly Assembly UfaAssembly = typeof(SkynetCloudEnvironment).Assembly;

        ///// <summary>
        ///// 批量注册AppDomain下的程序集内符合特定条件的实体到数据表的映射关系
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="typeFilter"></param>
        ///// <returns></returns>
        //public DbConfiguration AddFromAppDomain(System.Func<Type, bool> typeFilter)
        //{
        //    Assembly[] asms = null;
        //    if (UfaEnvironment.IsWeb)
        //    {
        //        var buildManagerType = ClassLoader.Load("System.Web.Compilation.BuildManager,System.Web");
        //        Guard.NotNull(buildManagerType, "buildManagerType");
        //        asms = (buildManagerType
        //            .GetMethod("GetReferencedAssemblies", BindingFlags.Public | BindingFlags.Static)
        //            .Invoke(null, null) as IEnumerable)
        //            .Cast<Assembly>()
        //            .ToArray();
        //    }
        //    else
        //        asms = AppDomain.CurrentDomain.GetAssemblies();

        //    foreach (var asm in asms)
        //        AddFromAssembly(asm, typeFilter);
        //    return this;
        //}


        /// <summary>
        /// 批量注册指定类型T所在的程序集内符合特定条件的实体到数据表的映射关系
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeFilter"></param>
        /// <returns></returns>
        public DbConfiguration AddFromAssemblyOf<T>(System.Func<Type, bool> typeFilter)
        {
            return AddFromAssembly(typeof(T).Assembly, typeFilter);
        }

        /// <summary>
        /// 批量注册指定类型T所在的程序集内符合特定条件的实体到数据表的映射关系
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public DbConfiguration AddFromAssemblyOf<T>()
        {
            return AddFromAssembly(typeof(T).Assembly, null);
        }

        /// <summary>
        /// 批量注册特定程序集内符合特定条件的实体到数据表的映射关系
        /// </summary>
        /// <param name="typeFilter"></param>
        /// <param name="asm"></param>
        /// <returns></returns>
        public DbConfiguration AddFromAssembly(Assembly asm, System.Func<Type, bool> typeFilter)
        {
            Guard.NotNull(asm, "asm");
            if (asm.IsSystemAssembly() || asm == ULinqAssembly || asm == UfaAssembly)
                return this;

            Type[] types = null;
            try
            {
                types = asm.GetTypes();
            }
            catch
            {
            }
            finally
            {
            }

            if (types == null)
                return this;


            foreach (var t in asm.GetTypes().Where(p => !p.IsAbstract))
            {
                var entityTypeId = t.TypeHandle.Value;
                if (mappings.ContainsKey(entityTypeId))
                    throw new RepeatRegistrationException("Repeat register entity mapping for entity '" + t.FullName + "'.");
                if (typeof(ClassMap).IsAssignableFrom(t))
                {
                    AddClass(Activator.CreateInstance(t) as ClassMap);
                    continue;
                }

                if (typeFilter != null && !typeFilter(t))
                    continue;

                if (ULinq.Instance == null)
                    RegistyMapping(CreateMapping(t));
                else
                {
                    var dlinqTableAttribute = (Attribute)t.GetCustomAttributes(ULinq.Instance.Table.Type, false).FirstOrDefault();
                    if (dlinqTableAttribute != null)
                        AddULinqClass(t, dlinqTableAttribute);
                    else
                        RegistyMapping(CreateMapping(t));
                }
            }
            return this;
        }

        static private XmlSchemaSet schemas;
        static void GuardSchemaInited()
        {
            if (schemas == null)
            {
                schemas = new XmlSchemaSet();
                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("UWay.Skynet.Cloud.Data.ULinq.xsd");
                if(schemas.Contains("urn:ulinq-mapping-1.0"))
                {
                    schemas.Add("urn:ulinq-mapping-1.0", XmlReader.Create(stream));
                }
            }

        }

        static string ValidateXml(XDocument doc)
        {
            GuardSchemaInited();
            string result = null;
            doc.Validate(schemas, (sender, e) =>
            {
                result = e.Message;
            }, true);
            return result;
        }
        /// <summary>
        /// 基于Xml配置文件来加载OR映射
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        public DbConfiguration AddFile(string xmlFile)
        {
            Guard.NotNull(xmlFile, "xmlFile");
            using (var stream = File.OpenRead(xmlFile))
                return AddFile(stream);
        }

        /// <summary>
        /// 基于Xml流来加载OR映射
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public DbConfiguration AddFile(Stream xml)
        {
            Guard.NotNull(xml, "xml");
            try
            {

                XDocument doc = XDocument.Load(xml);

                var errorMessage = ValidateXml(doc);
                if (!string.IsNullOrEmpty(errorMessage))
                    throw new MappingException(errorMessage);
                var root = doc.Root;
                var globalSchema = root.GetAttributeValue<string>("Schema");
                var @namespace = root.GetAttributeValue<string>("Namespace");
                var assemblyName = root.GetAttributeValue<string>("AssemblyName");
                var asm = AssemblyLoader.Load(assemblyName);
                if (asm == null)
                    throw new MappingException("Not load assembly :'" + assemblyName + "'");
                var ns = root.GetAttributeValue<string>("xmlns");
                if (ns.HasValue())
                {
                    root = XElement.Parse(root.ToString().Replace("xmlns=\"urn:ulinq-mapping-1.0\"", ""));
                }
                var tables = root.Elements("Table").ToArray();
                foreach (var item in tables)
                    AddTable(globalSchema, @namespace, item, asm);

            }
            catch (MappingException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new MappingException(e.Message, e);
            }
            return this;
        }


        /// <summary>
        /// 加载映射文件
        /// </summary>
        public void AddMappingFile(string containername)
        {
            var path = System.AppDomain.CurrentDomain.BaseDirectory ?? AppDomain.CurrentDomain.RelativeSearchPath;
            var files = Directory.EnumerateFiles(path, string.Format("uway.{0}*.mapping.xml", containername));
            if (files.Any())
            {
                foreach (var item in files)
                {
                    using (var stream = File.OpenRead(item))
                    {
                        try
                        {
                            AddFile(stream);
                        }
                        catch(Exception ex)
                        {
                           sqlLogger().Log(LogLevel.Error, ex,string.Format("读取映射文件:{0}",
                                item));
                            //this.sqlLogger().Log();
                            continue;
                        }
                        
                    }
                }
            }
        }

       

        private void AddTable(string globalSchema, string @namespace, XElement item, Assembly asm)
        {
            var className = @namespace + "." + item.GetAttributeValue<string>("Class");
            var type = asm.GetType(className);
            var mapping = new ClassMap { EntityType = type };

            
            var table = new TableAttribute();
            mapping.table = table;

            table.Server = item.GetAttributeValue<string>("Server");
            table.DatabaseName = item.GetAttributeValue<string>("Database");
            table.Name = item.GetAttributeValue<string>("Name");
            table.Readonly = item.GetAttributeValue<bool>("Readonly");
            table.Schema = globalSchema;

            foreach (var e in item.Elements("Id"))
                AddId(type, mapping, e);
            foreach (var e in item.Elements("Column"))
                AddColumn(type, mapping, e);
            foreach (var e in item.Elements("ComputedColumn"))
                AddComputedColumn(type, mapping, e);
            foreach (var e in item.Elements("Ignore"))
                AddIgnoreColumn(type, mapping, e);
            foreach (var e in item.Elements("OneToOne"))
                AddAssociation<OneToOneAttribute>(type, mapping, e);
            foreach (var e in item.Elements("OneToMany"))
                AddAssociation<OneToManyAttribute>(type, mapping, e);
            foreach (var e in item.Elements("ManyToOne"))
                AddAssociation<ManyToOneAttribute>(type, mapping, e);
            var version = item.Element("Version");
            if (version != null)
                AddVersionColumn(type, mapping, version);
            var entity = mapping.CreateMapping();
            Guard.NotNull(entity, "entity");

            AutoMapping(entity.entityType, entity);

            RegistyMapping(entity);
        }

        private static void AddId(Type type, ClassMap mapping, XElement e)
        {
            var id = new IdAttribute();
            var memberName = e.GetAttributeValue<string>("Member");
            var member = type.GetMember(memberName).FirstOrDefault();
            if (member == null)
                throw new MappingException(string.Format("Invalid member:'{0}' in type '{1}'!", memberName, type.FullName));
            var storage = e.GetAttributeValue<string>("Storage");
            if (storage.HasValue())
                id.Storage = storage;

            id.Name = e.GetAttributeValue<string>("Name");
            id.Alias = e.GetAttributeValue<string>("Alias");

            id.SequenceName = e.GetAttributeValue<string>("SequenceName");
            id.IsDbGenerated = e.GetAttributeValue<bool>("IsDbGenerated");
            id.Length = e.GetAttributeValue<int>("Length");

            var dbType = e.GetAttributeValue<string>("DbType");
            if (dbType.HasValue())
                id.DbType = (DBType)Enum.Parse(typeof(DBType), dbType);
            mapping.members.Add(member, id);
        }

        private static void AddColumn(Type type, ClassMap mapping, XElement e)
        {
            var col = new ColumnAttribute();
            var memberName = e.GetAttributeValue<string>("Member");
            var member = type.GetMember(memberName).FirstOrDefault();
            if (member == null)
                throw new MappingException(string.Format("Invalid member:'{0}' in type '{1}'!", memberName, type.FullName));
            var storage = e.GetAttributeValue<string>("Storage");
            if (storage.HasValue())
                col.Storage = storage;

            col.Name = e.GetAttributeValue<string>("Name");
            col.Alias = e.GetAttributeValue<string>("Alias");


            col.IsNullable = !e.GetAttributeValue<bool>("Required");
            col.Length = e.GetAttributeValue<int>("Length");
            col.Precision = e.GetAttributeValue<byte>("Precision");
            col.Scale = e.GetAttributeValue<byte>("Scale");

            var dbType = e.GetAttributeValue<string>("DbType");
            if (dbType.HasValue())
                col.DbType = (DBType)Enum.Parse(typeof(DBType), dbType);
            mapping.members.Add(member, col);
        }

        private static void AddComputedColumn(Type type, ClassMap mapping, XElement e)
        {
            var col = new ComputedColumnAttribute();
            var memberName = e.GetAttributeValue<string>("Member");
            var member = type.GetMember(memberName).FirstOrDefault();
            if (member == null)
                throw new MappingException(string.Format("Invalid member:'{0}' in type '{1}'!", memberName, type.FullName));
            var storage = e.GetAttributeValue<string>("Storage");
            if (storage.HasValue())
                col.Storage = storage;

            col.Name = e.GetAttributeValue<string>("Name");
            col.Alias = e.GetAttributeValue<string>("Alias");


            col.IsNullable = !e.GetAttributeValue<bool>("Required");
            col.Length = e.GetAttributeValue<int>("Length");
            col.Precision = e.GetAttributeValue<byte>("Precision");
            col.Scale = e.GetAttributeValue<byte>("Scale");

            var dbType = e.GetAttributeValue<string>("DbType");
            if (dbType.HasValue())
                col.DbType = (DBType)Enum.Parse(typeof(DBType), dbType);
            mapping.members.Add(member, col);
        }

        private static void AddAssociation<TAssociationAttribute>(Type type, ClassMap mapping, XElement e) where TAssociationAttribute : AbstractAssociationAttribute, new()
        {
            var ass = new TAssociationAttribute();
            var memberName = e.GetAttributeValue<string>("Member");
            var member = type.GetMember(memberName).FirstOrDefault();
            if (member == null)
                throw new MappingException(string.Format("Invalid member:'{0}' in type '{1}'!", memberName, type.FullName));
            var storage = e.GetAttributeValue<string>("Storage");
            if (storage.HasValue())
                ass.Storage = storage;

            ass.ThisKey = e.GetAttributeValue<string>("ThisKey");
            ass.OtherKey = e.GetAttributeValue<string>("OtherKey");
            mapping.members.Add(member, ass);
        }

        private static void AddVersionColumn(Type type, ClassMap mapping, XElement e)
        {
            var version = new VersionAttribute();
            var memberName = e.GetAttributeValue<string>("Member");
            var member = type.GetMember(memberName).FirstOrDefault();
            if (member == null)
                throw new MappingException(string.Format("Invalid member:'{0}' in type '{1}'!", memberName, type.FullName));
            var storage = e.GetAttributeValue<string>("Storage");
            if (storage.HasValue())
                version.Storage = storage;

            version.Name = e.GetAttributeValue<string>("Name");
            version.IsNullable = !e.GetAttributeValue<bool>("Required");
            version.Length = e.GetAttributeValue<int>("Length");
            var dbType = e.GetAttributeValue<string>("DbType");
            if (dbType.HasValue())
                version.DbType = (DBType)Enum.Parse(typeof(DBType), dbType);
            mapping.members.Add(member, version);
        }


        /// <summary>
        /// 根据实体类型得到映射元数据，如果不存在则Throw ORMappingException.
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public IEntityMapping GetClass(Type entityType)
        {
            Guard.NotNull(entityType, "entityType");
            return GetMapping(entityType);
        }

        /// <summary>
        /// 根据实体类型得到映射元数据，如果不存在则Throw ORMappingException.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IEntityMapping GetClass<TEntity>()
        {
            return GetMapping(typeof(TEntity));
        }

        /// <summary>
        /// 判断指定的实体类型是否已经注册OR映射
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public bool HasClass(Type entityType)
        {
            Guard.NotNull(entityType, "entityType");
            return mappings.ContainsKey(entityType.TypeHandle.Value);
        }

        /// <summary>
        /// 判断指定的实体类型是否已经注册OR映射
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public bool HasClass<TEntity>()
        {
            return mappings.ContainsKey(typeof(TEntity).TypeHandle.Value);
        }

        private static void AddIgnoreColumn(Type type, ClassMap mapping, XElement e)
        {
            //var col = new IgnoreAttribute();
            var memberName = e.GetAttributeValue<string>("Member");
            var member = type.GetMember(memberName).FirstOrDefault();
            if (member == null)
                throw new MappingException(string.Format("Invalid member:'{0}' in type '{1}'!", memberName, type.FullName));
            var storage = e.GetAttributeValue<string>("Storage");
            //if (storage.HasValue())
            //    col.Storage = storage;
            mapping.IgnoreMembers.Add(member);
        }

        private EntityMapping AddMappingClass(Type entityType)
        {
            var entityTypeId = entityType.TypeHandle.Value;
            if (mappings.ContainsKey(entityTypeId))
                throw new RepeatRegistrationException("Repeat register entity mapping for entity '" + entityType.FullName + "'.");

            var mapping = CreateMapping(entityType);
            if (ULinq.Instance == null)
                RegistyMapping(mapping);
            else
            {
                var dlinqTableAttribute = (Attribute)entityType.GetCustomAttributes(ULinq.Instance.Table.Type, false).FirstOrDefault();
                if (dlinqTableAttribute != null)
                    AddULinqClass(entityType, dlinqTableAttribute);
                else
                    RegistyMapping(mapping);
            }
            return mapping;
        }

        internal EntityMapping GetMapping(Type entityType)
        {
            EntityMapping mapping;
            var tableId = entityType.TypeHandle.Value;
            // (mappings.Count == 0)
                //AddFromAppDomain(HasLazyMappingType);
            //if (mappings.Count == 0)
            //{
            //    //mapping = ;
            //    //mappings.Add(tableId, AddMappingClass(entityType));
            //    //throw new MappingException(string.Format("Entity type '{0}' not configure mapping!", entityType.FullName));
            //}



            if (!mappings.TryGetValue(tableId, out mapping))
            {
                mapping = AddMappingClass(entityType);
                
                //mappings.Add(tableId, mapping);
                //throw new MappingException(string.Format("Entity type '{0}' not configure mapping!", entityType.FullName));
            }
                
            return mapping;
        }


        static bool HasLazyMappingType(Type t)
        {
            if (typeof(ClassMap).IsAssignableFrom(t))
                return true;

            var items = t.GetCustomAttributes(false).Select(p => p.GetType()).ToArray();
            if (items.Length == 0)
                return false;

            if (items.Contains(typeof(UWay.Skynet.Cloud.Data.TableAttribute)))
                return true;

            if (ULinq.Instance != null && items.Contains(ULinq.Instance.Table.Type))
                return true;
            if (EFDataAnnotiationAdapter.Instance != null && items.Contains(EFDataAnnotiationAdapter.TableAttributeType))
                return true;

            return false;
        }


        EntityMapping CreateMapping(Type type)
        {
            var mapping = new EntityMapping { entityType = type };
            var tableAttr = type.GetAttribute<TableAttribute>(false);
            if (tableAttr != null)
                PopulateULinqTableAttribute(mapping, tableAttr, type.Name);
            else
            {
                var att = type.GetCustomAttributes(false).FirstOrDefault(p => p.GetType().FullName == EFDataAnnotiationAdapter.StrTableAttribute
                    && p.GetType().Assembly.GetName().Name == EFDataAnnotiationAdapter.StrAssemblyName);

                if (att != null)
                    PopulateEFTableAttribute(type, mapping, att);
                else
                    mapping.tableName = mappingConversion.TableName(type.Name);
            }

            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            var q = from m in type
                       .GetFields(bindingFlags)
                       .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                       .Where(p => !p.Name.Contains("k__BackingField"))
                       .Where(p => !p.IsInitOnly)
                       .Cast<MemberInfo>()
                       .Union(type
                           .GetProperties(bindingFlags)
                           .Where(p => p.CanRead && p.CanWrite)
                           .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                           .Cast<MemberInfo>()
                           ).Distinct()
                    select m;
            if (EFDataAnnotiationAdapter.Instance != null)
                q = q.Where(p => p.GetCustomAttributes(EFDataAnnotiationAdapter.NotMappedAttributeType, false).Length == 0);

            var items = from m in q
                        let att = m.GetAttribute<MemberAttribute>(true)
                        select new UWay.Skynet.Cloud.Data.Mapping.MemberMapping(m, att, mapping);


            mapping.innerMappingMembers = items.ToArray();

            return AutoMapping(type, mapping);
        }

        private static void PopulateEFTableAttribute(Type type, EntityMapping mapping, object att)
        {
            if (EFDataAnnotiationAdapter.Instance == null)
                EFDataAnnotiationAdapter.Init(att.GetType().Assembly);
            var tableName = EFDataAnnotiationAdapter.Instance.Table.Name(att) as string;
            var schemaName = EFDataAnnotiationAdapter.Instance.Table.Schema(att) as string;

            PopulateTableName(mapping, tableName, type.Name);

            if (schemaName.HasValue())
                mapping.schema = schemaName;
        }

        private static void PopulateULinqTableAttribute(EntityMapping mapping, TableAttribute tableAttr, string defaultTableName)
        {
            var tableName = tableAttr.Name;
            PopulateTableName(mapping, tableName, defaultTableName);

            mapping.@readonly = tableAttr.Readonly;
            if (tableAttr.Schema.HasValue())
                mapping.schema = tableAttr.Schema;
        }

        private static void PopulateTableName(EntityMapping mapping, string tableName, string defaultTableName)
        {
            if (tableName.HasValue())
            {
                var parts = tableName.Split('.').Where(p => !string.IsNullOrEmpty(p)).ToArray();
                switch (parts.Length)
                {
                    case 1:
                        mapping.tableName = tableName;
                        break;
                    case 2:
                        mapping.schema = parts[0];
                        mapping.tableName = parts[1].TrimStart('[').TrimEnd(']');
                        break;
                    case 3:
                        mapping.databaseName = parts[0];
                        mapping.schema = parts[1];
                        mapping.tableName = parts[2].TrimStart('[').TrimEnd(']');
                        break;
                    case 4:
                        mapping.serverName = parts[0];
                        mapping.databaseName = parts[1];
                        mapping.schema = parts[2];
                        mapping.tableName = parts[3].TrimStart('[').TrimEnd(']');
                        break;
                    default:
                        throw new MappingException("Invalid table name '" + tableName + "'");
                }
            }
            else
                mapping.tableName = defaultTableName;
        }

        private static EntityMapping AutoMapping(Type type, EntityMapping mapping)
        {
            var mappingEntity = mapping;
            var versions = mapping.innerMappingMembers.Where(p => p.isVersion).ToArray();
            if (versions.Length > 1)
                throw new MappingException(string.Format("Version member too many in entity '{0}'", mapping.entityType.Name));
            if (versions.Length == 1)
                mapping.version = versions[0];

            mapping.primairyKeys = mapping.innerMappingMembers.Where(p => p.isPrimaryKey).ToArray();
            if (mapping.primairyKeys.Length == 0)
            {
                var ids = new[] { "id", type.Name.ToLower() + "id" };
                var primairyKey = mapping.innerMappingMembers.Where(p => ids.Contains(p.member.Name.ToLower())).OrderBy(p => p.member.Name).FirstOrDefault();
                if (primairyKey != null)
                {
                    primairyKey.isPrimaryKey = true;
                    var idType = primairyKey.Member.GetMemberType();
                    if (idType == Types.Int32 || idType == Types.Int64)
                        primairyKey.isGenerated = true;
                    primairyKey.isUpdatable = false;
                    mapping.primairyKeys = new IMemberMapping[] { primairyKey };
                }
            }
            foreach (var m in mappingEntity.innerMappingMembers.Where(p => p.relatedEntityType != null))
            {
                const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;
                var memberType = m.member.GetMemberType();
                if (Types.IEnumerable.IsAssignableFrom(memberType))//一对多
                {
                    if (m.thisKey.IsNullOrEmpty())
                    {
                        if (mappingEntity.primairyKeys != null)
                            m.thisKey = mappingEntity.primairyKeys.Select(p => p.Member.Name).ToCSV(",");
                        else
                            throw new MappingException("Missing 'ThisKey' value in member '" + m.member.DeclaringType.Name + "." + m.member.Name + " when OR mapping");
                    }
                    if (m.otherKey.IsNullOrEmpty())
                    {
                        var otherEntityType = m.relatedEntityType;
                        var otherKey = otherEntityType.GetMember(m.member.DeclaringType.Name + "ID", flags).FirstOrDefault();

                        if (otherKey != null)
                            m.otherKey = otherKey.Name;
                        else
                            throw new MappingException("Missing 'OtherKey' value in member '" + m.member.DeclaringType.Name + "." + m.member.Name + "' when OR mapping");
                    }
                }
                else//多对一
                {
                    if (m.thisKey.IsNullOrEmpty())
                    {
                        var thisKey = m.member.DeclaringType.GetMember(memberType.Name + "ID", flags).FirstOrDefault();
                        if (thisKey != null)
                            m.thisKey = thisKey.Name;
                        else
                            throw new MappingException("Missing 'ThisKey' value in member '" + m.member.DeclaringType.Name + "." + m.member.Name + "' when OR mapping");
                    }
                    if (m.otherKey.IsNullOrEmpty())
                    {

                        var otherKey = memberType.GetMembers().Where(p => p.HasAttribute<IdAttribute>(true)).ToArray();
                        if (otherKey != null && otherKey.Length > 0)
                            m.otherKey = otherKey[0].Name;
                        else
                        {
                            otherKey = memberType.GetMember("ID", flags);
                            if (otherKey == null || otherKey.Length == 0)
                                otherKey = memberType.GetMember(memberType.Name + "ID", flags);
                            if (otherKey == null || otherKey.Length == 0)
                                throw new MappingException("Missing 'OtherKey' value in member '" + m.member.DeclaringType.Name + "." + m.member.Name + "' when OR mapping");
                            m.otherKey = otherKey[0].Name;
                        }
                    }
                }
            }

            mapping.mappingMembers = mapping.innerMappingMembers.ToDictionary(mm => mm.Member.Name);
            return mapping;
        }
    }
}
