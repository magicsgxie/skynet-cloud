using UWay.Skynet.Cloud.CodeGen.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.CodeGen.Service
{
    internal class GenUtils
    {
        public static string GeneratorXmlHeader(string assembly, string Namespace)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf - 8\" ?>");
            sb.AppendLine("Mapping xlmns=\"urn: ulinq - mapping - 1.0\"");
            sb.AppendLine("Namespace = \""+ Namespace + "\"");
            sb.AppendLine("AssemblyName = \""+assembly+"\"> ");
            return sb.ToString();
        }

        public static string GeneratorXmlFooter()
        {

            return "</Mapping>";
        }

        public static string GeneratorUnity(string Namespace,string moduleName)
        {
            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("namespace " + Namespace +"." + moduleName);
            sb.AppendLine("{");
            sb.AppendLine(GetTabs(1) + "internal class Unity");
            sb.AppendLine(GetTabs(1)+"{");
            sb.AppendLine(GetTabs(2) + " public const string ContainerName=\""+ moduleName+"\";");
            sb.AppendLine(GetTabs(1)+"}");
            sb.AppendLine( "}");

            return sb.ToString();
        }

        public static IDictionary<string, StringBuilder> GeneratorCode(GenConfig genConfig,  DataRow table, DataTable genColumns, string tablePrefix="mod_",string provider = DbProviderNames.Oracle_Managed_ODP)
        {

            IDictionary<string, StringBuilder> classDetails = new Dictionary<string, StringBuilder>();
            var comments = table["tableComment"].ToString();
            
            GenTable genTable = new GenTable();
            if (comments.IsNullOrEmpty())
                genTable.Comments = genConfig.Comments;
            else
                genTable.Comments = comments;


            if (genConfig.TablePrefix.IsNullOrEmpty())
            {
                genConfig.TablePrefix = tablePrefix;
            } else
            {
                if (!genConfig.TablePrefix.EndsWith("_"))
                    genConfig.TablePrefix = genConfig.TablePrefix + "_";
            }

            genTable.TableName = table["tableName"].ToString();
            string className = genTable.TableName.Replace(genConfig.TablePrefix, "").ToCamelCase();
            genTable.CaseClassName = className;
            genTable.LowerClassName = className.Replace(className.Substring(0, 1), className.Substring(0, 1).ToLower());

            IList<GenColumn> columns = new List<GenColumn>();
            
            foreach(DataRow item in genColumns.Rows)
            {
                var column = new GenColumn();
                var columnName = item["columnName"].ToString();
                column.CaseAttrName = columnName.ToCamelCase();
                column.LowerAttrName = column.CaseAttrName.Substring(0, 1).ToLower() + column.CaseAttrName.Substring(1);
               
                //是否主键
                if ("PRI".IsCaseInsensitiveEqual(item["columnKey"].ToString()) && genTable.PrimaryKey == null)
                {
                    genTable.PrimaryKey = column;
                }
                column.ColumnName = columnName;
                column.Comments = item["columnComment"].ToString();
                column.DataType = item["dataType"].ToString();
                column.AttrType = ConvertType(column.DataType, item["nullable"].ToString());
                column.DataType = ConvertDbType(column.DataType, provider);
                column.Extra = item["extra"].ToString();
                columns.Add(column);
            }
            classDetails.Add("Entity_"+genTable.CaseClassName, GeneratorEntity(genConfig, genTable, columns));
            classDetails.Add("Service.Interface_I"+genTable.CaseClassName+"Service", GeneratorServiceInterface(genConfig, genTable));
            classDetails.Add("Service_" + genTable.CaseClassName + "Service", GeneratorService(genConfig, genTable));
            classDetails.Add("Repository_" + genTable.CaseClassName + "Repository", GeneratorRepository(genConfig, genTable));
            if(genConfig.IsGeneratorMapping)
            {
                classDetails.Add("xml_mapping", GeneratorMapping(genTable, columns));
            }
            return classDetails;
        }




        public static StringBuilder GeneratorServiceProject(GenConfig genConfig)
        {
            var assemblyName = genConfig.Namespace.Substring(genConfig.Namespace.IndexOf(".") + 1);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<Project Sdk=\"Microsoft.NET.Sdk\">");
            sb.AppendLine(GetTabs(1) + "<PropertyGroup>");
            sb.AppendLine(GetTabs(2) + "<TargetFramework>netstandard2.0</TargetFramework>");
            sb.AppendLine(GetTabs(2) + "<RootNamespace>" + genConfig.Namespace + "." + genConfig.ModuleName + ".Service</RootNamespace>");
            sb.AppendLine(GetTabs(1) + "</PropertyGroup>");
            sb.AppendLine(GetTabs(1) + "<ItemGroup>");
            sb.AppendLine(GetTabs(2) + "<ProjectReference Include=\"..\\" + assemblyName + "." + genConfig.ModuleName + ".Repository\\" + assemblyName + "." + genConfig.ModuleName  + ".Repository.csproj\" />");
            sb.AppendLine(GetTabs(2) + "<ProjectReference Include=\"..\\" + assemblyName + "." + genConfig.ModuleName + ".Service.Interface\\" + assemblyName + "." + genConfig.ModuleName + ".Service.Interface.csproj\" />");
            sb.AppendLine(GetTabs(1) + "</ItemGroup>");
            sb.AppendLine("</Project>");
            return sb;

        }

        public static StringBuilder GeneratorRepositoryProject(GenConfig genConfig)
        {
            var assemblyName = genConfig.Namespace.Substring(genConfig.Namespace.IndexOf(".")+1);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<Project Sdk=\"Microsoft.NET.Sdk\">");
            sb.AppendLine(GetTabs(1) + "<PropertyGroup>");
            sb.AppendLine(GetTabs(2) + "<TargetFramework>netstandard2.0</TargetFramework>");
            sb.AppendLine(GetTabs(2) + "<RootNamespace>" + genConfig.Namespace + "." + genConfig.ModuleName  + ".Repository</RootNamespace>");
            sb.AppendLine(GetTabs(1) + "</PropertyGroup>");
            sb.AppendLine(GetTabs(1) + "<ItemGroup>");
            sb.AppendLine(GetTabs(2) + "<ProjectReference Include=\"..\\..\\..\\01.Skynet.Cloud.Framework\\1.Projects\\Skynet.Cloud.Data\\Skynet.Cloud.Data.csproj\" />");
            sb.AppendLine(GetTabs(2) + "<ProjectReference Include=\"..\\" + assemblyName + "." + genConfig.ModuleName + ".Entity\\" + assemblyName + "." + genConfig.ModuleName + ".Entity.csproj\" />");
            sb.AppendLine(GetTabs(1) + "</ItemGroup>");
            sb.AppendLine(GetTabs(1) + "</Project>");
            return sb;
        }
        public static StringBuilder GeneratorEntityProject(GenConfig genConfig)
        {
            var assemblyName = genConfig.Namespace.Substring(genConfig.Namespace.IndexOf(".") + 1);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<Project Sdk=\"Microsoft.NET.Sdk\">");
            sb.AppendLine(GetTabs(1)+"<PropertyGroup>");
            sb.AppendLine(GetTabs(2) + "<TargetFramework>netstandard2.0</TargetFramework>");
            sb.AppendLine(GetTabs(2) + "<RootNamespace>" + genConfig.Namespace+"." +genConfig.ModuleName+ ".Entity</RootNamespace>");
            sb.AppendLine(GetTabs(1) + "</PropertyGroup>");
            sb.AppendLine(GetTabs(1) + "<ItemGroup>");
            sb.AppendLine(GetTabs(2) + "<ProjectReference Include =\"..\\..\\..\\01.Skynet.Cloud.Framework\\1.Projects\\Skynet.Cloud.Framework\\Skynet.Cloud.Framework.csproj\" />");
            sb.AppendLine(GetTabs(1) + "</ItemGroup>");
            sb.AppendLine("</Project>");
            return sb;
        }

        public static StringBuilder GeneratorServiceInterfaceProject(GenConfig genConfig)
        {
            var assemblyName = genConfig.Namespace.Substring(genConfig.Namespace.IndexOf(".") + 1);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<Project Sdk=\"Microsoft.NET.Sdk\">");
            sb.AppendLine(GetTabs(1) + "<PropertyGroup>");
            sb.AppendLine(GetTabs(2) + "<TargetFramework>netstandard2.0</TargetFramework>");
            sb.AppendLine(GetTabs(2) + "<RootNamespace>" + genConfig.Namespace + "." + genConfig.ModuleName + ".Service.Interface</RootNamespace>");
            sb.AppendLine(GetTabs(1) + "</PropertyGroup>");
            sb.AppendLine(GetTabs(1) + "<ItemGroup>");
            sb.AppendLine(GetTabs(2) + "<ProjectReference Include=\"..\\" + assemblyName + "." + genConfig.ModuleName + ".Entity\\" + assemblyName + "." + genConfig.ModuleName + ".Entity.csproj\" />");
            sb.AppendLine(GetTabs(1) + "</ItemGroup>");
            sb.AppendLine("</Project>");
            return sb;
        }

        private static StringBuilder GeneratorMapping( GenTable table, IEnumerable<GenColumn> colums)
        {
            var sb = new StringBuilder();
            sb.AppendLine(GetTabs(1)+"<Table Name = \""+table.TableName+ "\" Class = \"" + table.CaseClassName + "\"");
            foreach(var column in colums)
            {
                if(table.PrimaryKey != null && table.PrimaryKey.CaseAttrName.Equals( column.CaseAttrName, StringComparison.InvariantCultureIgnoreCase))
                {

                    sb.AppendLine(GetTabs(2) + "<Id Name = \"" + column.ColumnName + "\" Member = \"" + column.CaseAttrName + "\"  IsDbGenerated = \"true\"  SequenceName=\"seq_" + column.ColumnName+"\" /> ");
                } else
                {
                    sb.AppendLine(GetTabs(2) + "<Column Name = \"" + column.ColumnName + "\" Member=\"" + column.CaseAttrName + "\" ");
                }
                
            }
            sb.AppendLine("</Table>");
            return sb;
        }

        private static StringBuilder GeneratorEntity(GenConfig genConfig, GenTable table, IEnumerable<GenColumn> columns)
        {
            var sb = new StringBuilder();
            sb.Append(GetHeader(genConfig, table, "Entity"));
            sb.AppendLine("{");
            
            sb.AppendLine(GetEntityUsing());
            sb.Append(GetClassDescription(table));
            sb.Append(GetName(table));
            sb.AppendLine(GetTabs(1)+"{");
            sb.Append(GetEntityBody(genConfig.IsGeneratorMapping, table.PrimaryKey == null ? string.Empty: table.PrimaryKey.ColumnName,columns));
            sb.AppendLine(GetTabs(1) + "}");
            sb.AppendLine("}");
            return sb;
        }


        private static string GetEntityUsing()
        {
            var sb = new StringBuilder();
            sb.AppendLine(GetTabs(1)+"using System;");
            sb.AppendLine(GetTabs(1) + "using UWay.Skynet.Cloud.Data;");
            return sb.ToString();
            
        }
        private static string GetInterfaceUsing(GenConfig genConfig)
        {
            var sb = new StringBuilder();
            sb.AppendLine(GetEntityUsing());
            sb.AppendLine(GetTabs(1) + "using UWay.Skynet.Cloud.Request;");
            sb.AppendLine(GetTabs(1) + "using " + genConfig.Namespace + "." + genConfig.ModuleName + ".Entity;");
            sb.AppendLine(GetTabs(1) + "using System.Collections.Generic;");
            return sb.ToString();
        }

        private static string GetServiceUsing(GenConfig genConfig)
        {
            var sb = new StringBuilder();

            sb.AppendLine(GetInterfaceUsing(genConfig));
            sb.AppendLine(GetTabs(1) + "using System.Linq;");
            sb.AppendLine(GetTabs(1) + "using " + genConfig.Namespace + "." + genConfig.ModuleName + ".Service.Interface;");
            sb.AppendLine(GetTabs(1) + "using " + genConfig.Namespace + "." + genConfig.ModuleName + ".Repository;");
            sb.AppendLine(GetTabs(1) + "using UWay.Skynet.Cloud.Linq;");
            sb.AppendLine(GetTabs(1) + "using UWay.Skynet.Cloud;");
            return sb.ToString();
        }

        private static string GetRepositoryUsing(GenConfig genConfig)
        {
            var sb = new StringBuilder();
            sb.AppendLine(GetEntityUsing());
            sb.AppendLine(GetTabs(1) + "using  " + genConfig.Namespace + "." + genConfig.ModuleName + ".Entity;");
            sb.AppendLine(GetTabs(1) + "using System.Linq;");
            sb.AppendLine(GetTabs(1) + "using System.Collections.Generic;");
            return sb.ToString();
        }

        private static string GetName(GenTable genTable, string classSubfix = "",bool isInterface = false)
        {
            StringBuilder ret = new StringBuilder();
            if(classSubfix.Length <=0)
                ret.AppendLine(GetTabs(1) + "[Table(\"" + genTable.TableName + "\")]");
            ret.Append(GetTabs(1) + "public ");
            ret.Append(isInterface ? "interface I" : "class ");
            if(classSubfix.Equals("Repository", StringComparison.InvariantCultureIgnoreCase))
            {
                ret.AppendLine(genTable.CaseClassName + classSubfix + ":ObjectRepository");
            }
            else
            {
                ret.AppendLine(genTable.CaseClassName + classSubfix + "" + ((isInterface == false && classSubfix.IsCaseInsensitiveEqual("Service")) ? (": I" + genTable.CaseClassName + classSubfix) : ""));
            }
            

            return ret.ToString();
        }

   

        private static string GetInterfaceBody(GenTable table)
        {
            StringBuilder sbBody = new StringBuilder();
            #region public ind Add()
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 添加" + table.Comments +"{" + table.Comments + "}"+ "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            sbBody.AppendLine(GetTabs(2) + "long Add(" + table.CaseClassName + "  "+ table.LowerClassName+");");
            #endregion

            #region public ind Add()
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 添加" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            sbBody.AppendLine(GetTabs(2) + "void Add(IList<" + table.CaseClassName + ">  " + table.LowerClassName + "s);");
            #endregion

            #region public ind Update()
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 更新" + table.Comments + "{" + table.Comments + "}" +"对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            sbBody.AppendLine(GetTabs(2) + "int Update(" + table.CaseClassName + "  " + table.LowerClassName + ");");
            #endregion

            //string strKeyParameters = tableInfo.GetParamaters(ColumnsType.KeyColumns, "idb");
            #region public ind Delete(string var1,.....)
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 删除" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            if (table.PrimaryKey != null)
            {
                sbBody.AppendLine(GetTabs(2) + "int Delete(" + table.PrimaryKey.AttrType + "[] idArrays );");
            } else
            {
                sbBody.AppendLine(GetTabs(2) + "int Delete(" + table.CaseClassName + " " + table.LowerClassName + ");");
            }
            
            
            #endregion

            //strKeyParameters = tableInfo.GetParamaters(ColumnsType.KeyColumns, "idb");
            #region public ind GetByKey(string var1,.....)
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 获取指定的" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            if(table.PrimaryKey != null)
                sbBody.AppendLine(GetTabs(2) + table.CaseClassName + " GetById("+table.PrimaryKey.AttrType+" id);");
            else
                sbBody.AppendLine(GetTabs(2) + table.CaseClassName + " GetById(object id);");
            #endregion

            //strKeyParameters = tableInfo.GetParamaters(ColumnsType.KeyColumns, "idb", classOjbect);
            #region public ind GetAll()
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 获取所有的" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            
            sbBody.AppendLine(GetTabs(2) + "DataSourceResult Page(DataSourceRequest request);");
            #endregion



            //sbBody.Append(sbBody.ToString());

            return sbBody.ToString();
        }

        private static string GetServiceBody(GenTable table)
        {
            StringBuilder sbBody = new StringBuilder();
            #region public ind Add()
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 添加" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            sbBody.AppendLine(GetTabs(2) + "public long Add(" + table.CaseClassName + "  " + table.LowerClassName + ")");
            sbBody.AppendLine(GetTabs(2) + "{");
            sbBody.AppendLine(GetTabs(3) + "using(var dbContext = UnitOfWork.Get(Unity.ContainerName))");
            sbBody.AppendLine(GetTabs(3) + "{");
            sbBody.AppendLine(GetTabs(4) + "return new " + table.CaseClassName + "Repository(dbContext).Add(" + table.LowerClassName+");");
            sbBody.AppendLine(GetTabs(3) + "}");
            sbBody.AppendLine(GetTabs(2) + "}");
            #endregion

            #region public ind Add()
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 添加" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            sbBody.AppendLine(GetTabs(2) + "public void Add(IList<" + table.CaseClassName + ">  " + table.LowerClassName + "s)");
            sbBody.AppendLine(GetTabs(2) + "{");
            sbBody.AppendLine(GetTabs(3) + "using(var dbContext = UnitOfWork.Get(Unity.ContainerName))");
            sbBody.AppendLine(GetTabs(3) + "{");
            sbBody.AppendLine(GetTabs(4) + "new " + table.CaseClassName + "Repository(dbContext).Add(" + table.LowerClassName + "s);");
            sbBody.AppendLine(GetTabs(3) + "}");
            sbBody.AppendLine(GetTabs(2) + "}");
            #endregion

            #region public ind Update()
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 更新" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            sbBody.AppendLine(GetTabs(2) + "public int Update(" + table.CaseClassName + "  " + table.LowerClassName + ")");
            sbBody.AppendLine(GetTabs(2) + "{");
            sbBody.AppendLine(GetTabs(3) + "using(var dbContext = UnitOfWork.Get(Unity.ContainerName))");
            sbBody.AppendLine(GetTabs(3) + "{");
            sbBody.AppendLine(GetTabs(4) + "return new " + table.CaseClassName + "Repository(dbContext).Update(" + table.LowerClassName + ");");
            sbBody.AppendLine(GetTabs(3) + "}");
            sbBody.AppendLine(GetTabs(2) + "}");
            #endregion

            //string strKeyParameters = tableInfo.GetParamaters(ColumnsType.KeyColumns, "idb");
            #region public ind Delete(string var1,.....)
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 删除" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            if (table.PrimaryKey != null)
            {
                sbBody.AppendLine(GetTabs(2) + "public int Delete(" + table.PrimaryKey.AttrType + "[] idArrays )");
                sbBody.AppendLine(GetTabs(2) + "{");
                sbBody.AppendLine(GetTabs(3) + "using(var dbContext = UnitOfWork.Get(Unity.ContainerName))");
                sbBody.AppendLine(GetTabs(3) + "{");
                sbBody.AppendLine(GetTabs(4) + "return new " + table.CaseClassName + "Repository(dbContext).Delete(idArrays);");
                sbBody.AppendLine(GetTabs(3) + "}");
                sbBody.AppendLine(GetTabs(2) + "}");
            }
            else
            {
                sbBody.AppendLine(GetTabs(2) + "public int Delete("+table.CaseClassName+" "+table.LowerClassName+")");
                sbBody.AppendLine(GetTabs(2) + "{");
                sbBody.AppendLine(GetTabs(3) + "using(var dbContext = UnitOfWork.Get(Unity.ContainerName))");
                sbBody.AppendLine(GetTabs(3) + "{");
                sbBody.AppendLine(GetTabs(4) + "return new " + table.CaseClassName + "Repository(dbContext).Delete(" + table.LowerClassName + ");");
                sbBody.AppendLine(GetTabs(3) + "}");
                sbBody.AppendLine(GetTabs(2) + "}");
            }
            #endregion

            //strKeyParameters = tableInfo.GetParamaters(ColumnsType.KeyColumns, "idb");
            #region public ind GetByKey(string var1,.....)
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 获取指定的" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            if (table.PrimaryKey != null)
                sbBody.AppendLine(GetTabs(2) + "public " +table.CaseClassName + " GetById("+ table.PrimaryKey.AttrType+ " id)");
            else
                sbBody.AppendLine(GetTabs(2) + "public " + table.CaseClassName + " GetById(object id)");
            sbBody.AppendLine(GetTabs(2) + "{");
            sbBody.AppendLine(GetTabs(3) + "using(var dbContext = UnitOfWork.Get(Unity.ContainerName))");
            sbBody.AppendLine(GetTabs(3) + "{");
            sbBody.AppendLine(GetTabs(4) + "return new " + table.CaseClassName + "Repository(dbContext).GetById(id);");
            sbBody.AppendLine(GetTabs(3) + "}");
            sbBody.AppendLine(GetTabs(2) + "}");
            #endregion

            //strKeyParameters = tableInfo.GetParamaters(ColumnsType.KeyColumns, "idb", classOjbect);
            #region public ind GetAll()
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 获取所有的" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            sbBody.AppendLine(GetTabs(2) + "public DataSourceResult Page(DataSourceRequest request)");
            sbBody.AppendLine(GetTabs(2) + "{");
            sbBody.AppendLine(GetTabs(3) + "using(var dbContext = UnitOfWork.Get(Unity.ContainerName))");
            sbBody.AppendLine(GetTabs(3) + "{");
            sbBody.AppendLine(GetTabs(4) + "return new " + table.CaseClassName + "Repository(dbContext).Query().ToDataSourceResult(request);");
            sbBody.AppendLine(GetTabs(3) + "}");
            sbBody.AppendLine(GetTabs(2) + "}");
            #endregion

            return sbBody.ToString();
        }

        private static string GetRepositoryBody(GenTable table)
        {
            StringBuilder sbBody = new StringBuilder();

            #region Construct 
            sbBody.AppendLine(GetTabs(2) + "public " + table.CaseClassName + "Repository(IDbContext uow):base(uow)");
            sbBody.AppendLine(GetTabs(2) + "{");
            sbBody.AppendLine(GetTabs(2) + "}");
            #endregion

            #region public ind Add()
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 添加" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            sbBody.AppendLine(GetTabs(2) + "public long Add(" + table.CaseClassName + "  " + table.LowerClassName + ")");
            sbBody.AppendLine(GetTabs(2) + "{");
            sbBody.AppendLine(GetTabs(3) + "return Add<" + table.CaseClassName + ">(" + table.LowerClassName + ");");
            sbBody.AppendLine(GetTabs(2) + "}");
            #endregion

            #region public ind Add()
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 批量添加" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            sbBody.AppendLine(GetTabs(2) + "public void Add(IList<" + table.CaseClassName + ">  " + table.LowerClassName + "s)");
            sbBody.AppendLine(GetTabs(2) + "{");
            sbBody.AppendLine(GetTabs(3) + "Batch<long, " + table.CaseClassName + ">(" + table.LowerClassName + "s, (u, v) => u.Insert(v));");
            sbBody.AppendLine(GetTabs(2) + "}");
            #endregion

            #region public ind Update()
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 更新" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            sbBody.AppendLine(GetTabs(2) + "public int Update(" + table.CaseClassName + "  " + table.LowerClassName + ")");
            sbBody.AppendLine(GetTabs(2) + "{");
            sbBody.AppendLine(GetTabs(3) + "return Update<" + table.CaseClassName + ">(" + table.LowerClassName + ");");
            sbBody.AppendLine(GetTabs(2) + "}");
            #endregion

            //string strKeyParameters = tableInfo.GetParamaters(ColumnsType.KeyColumns, "idb");
            #region public ind Delete(string var1,.....)
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 删除" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            if(table.PrimaryKey != null)
            {
                sbBody.AppendLine(GetTabs(2) + "public int Delete(" + table.PrimaryKey.AttrType + "[] idArrays )");
                sbBody.AppendLine(GetTabs(2) + "{");
                sbBody.AppendLine(GetTabs(3) + "return Delete<" + table.CaseClassName + ">(p => idArrays.Contains(p." + table.PrimaryKey.CaseAttrName + ")); ");
                sbBody.AppendLine(GetTabs(2) + "}");
            }  else
            {
                sbBody.AppendLine(GetTabs(2) + "public int Delete(" + table.CaseClassName + " "+table.LowerClassName+" )");
                sbBody.AppendLine(GetTabs(2) + "{");
                sbBody.AppendLine(GetTabs(3) + "return Delete<" + table.CaseClassName + ">(" + table.LowerClassName + "); ");
                sbBody.AppendLine(GetTabs(2) + "}");
            }

            #endregion

            //strKeyParameters = tableInfo.GetParamaters(ColumnsType.KeyColumns, "idb");
            #region public ind GetByKey(string var1,.....)
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 获取指定的" + table.Comments + "{" + table.Comments + "}" + "对象(即:一条记录");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            if(table.PrimaryKey != null)
            {
                sbBody.AppendLine(GetTabs(2) + "public " + table.CaseClassName + " GetById(" + table.PrimaryKey.AttrType + " id)");
            }
            else
            {
                sbBody.AppendLine(GetTabs(2) + "public " + table.CaseClassName + " GetById(object id)");
            }
            
            sbBody.AppendLine(GetTabs(2) + "{");
            sbBody.AppendLine(GetTabs(3) + "return GetByID<" + table.CaseClassName + ">(id);");
            sbBody.AppendLine(GetTabs(2) + "}");
            #endregion

            //strKeyParameters = tableInfo.GetParamaters(ColumnsType.KeyColumns, "idb", classOjbect);
            #region public ind GetAll()
            sbBody.AppendLine(GetTabs(2) + "/// <summary>");
            sbBody.AppendLine(GetTabs(2) + "/// 获取所有的" + table.Comments + "{" + table.Comments + "}" + "对象");
            sbBody.AppendLine(GetTabs(2) + "/// </summary>");
            sbBody.AppendLine(GetTabs(2) + "public IQueryable<"+table.CaseClassName+"> Query()");
            sbBody.AppendLine(GetTabs(2) + "{");
            sbBody.AppendLine(GetTabs(3) + "return CreateQuery<" + table.CaseClassName + ">();");
            sbBody.AppendLine(GetTabs(2) + "}");
            #endregion

            return sbBody.ToString();
        }

        private static string GetEntityBody(bool isGeneratMapping, string pkName, IEnumerable<GenColumn> genColumns)
        {
            StringBuilder sbBody = new StringBuilder();
            foreach(var column in genColumns) {
                if(!column.Comments.IsNullOrEmpty())
                {
                    sbBody.AppendLine(GetTabs(2) + "/// <summary>");
                    sbBody.AppendLine(GetTabs(2) + "/// " + column.Comments);
                    sbBody.AppendLine(GetTabs(2) + "/// <summary>");
                }
                if (isGeneratMapping == false )
                {
                    if(!pkName.IsNullOrEmpty() &&pkName.Equals(column.CaseAttrName, StringComparison.InvariantCultureIgnoreCase))
                    {

                        sbBody.AppendLine(GetTabs(2) + "[Id(\""+column.ColumnName+"\", IsDbGenerated =true, SequenceName =\"seq_" + column.ColumnName + "\")]");
                    }
                    else
                    {

                        sbBody.AppendLine(GetTabs(2) + "[Column(\"" + column.ColumnName + "\",DbType = DBType." + column.DataType + ")]");
                    }
                }
                    
                sbBody.Append(GetTabs(2) + "public " + column.AttrType + " " + column.CaseAttrName);
                sbBody.AppendLine("{ set; get;}");
            }

            return sbBody.ToString();
        }


        private static string GetClassDescription(GenTable genTable, string classDescriptionSubfix = "")
        {
            StringBuilder ret = new StringBuilder();
            if(!genTable.Comments.IsNullOrEmpty())
            {
                ret.AppendLine(GetTabs(1) + "/// <summary>");
                ret.AppendLine(GetTabs(1) + "/// " + genTable.Comments+ classDescriptionSubfix);
                ret.AppendLine(GetTabs(1) + "/// </summary>");
            }
            return ret.ToString();
        }

        #region
        /// <summary>
        /// 生成Class字符串
        /// </summary>
        /// <param name="genConfig">生成配置信息</param>
        /// <param name="genTable">表信息</param>
        /// <param name="classType">类型：Entity-实体，Service-服务实现类, Service.Interface-服务接口类, Repository-数据库仓储类</param>
        /// <param name="classNameSubfix">类后缀</param>
        /// <returns></returns>
        private static string GetHeader(GenConfig genConfig, GenTable genTable,string classType, string classNameSubfix="")
        {
            StringBuilder ret = new StringBuilder();
            
            ret.AppendLine(@"/************************************************************************************
* Copyright (c) " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @" 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：" + genConfig.Namespace+"." + genConfig.ModuleName +"."+ classType + @"
* 文件名：  " + genConfig.Namespace + "." + genConfig.ModuleName + "." + classType + "." +genTable.CaseClassName+classNameSubfix+ @".cs
* 版本号：  V1.0.0.0
* 唯一标识：" + Guid.NewGuid() + @"
* 创建人：  "+genConfig.Author+ @"
* 电子邮箱：" + genConfig.Email + @"
* 创建时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @" 
* 描述：" + genTable.Comments + @" 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @" 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/
");
            ret.AppendLine();
            ret.AppendLine();
            ret.AppendLine("namespace   " + genConfig.Namespace + "." + genConfig.ModuleName + "." + classType);
            return ret.ToString();
        
        }
        #endregion

        private static StringBuilder GeneratorRepository(GenConfig genConfig,GenTable table)
        {
            var sb = new StringBuilder();
            sb.Append(GetHeader(genConfig, table, "Repository"));
            sb.AppendLine("{");
            sb.AppendLine(GetRepositoryUsing(genConfig));
            sb.Append(GetClassDescription(table, "仓储类"));
            sb.Append(GetName(table, "Repository"));
            sb.AppendLine(GetTabs(1) + "{");
            sb.Append(GetRepositoryBody(table));
            sb.AppendLine(GetTabs(1) + "}");
            sb.AppendLine("}");
            return sb;
        }

        private static StringBuilder GeneratorService(GenConfig genConfig, GenTable table)
        {
            var sb = new StringBuilder();
            sb.Append(GetHeader(genConfig, table, "Service"));
            sb.AppendLine("{");
            sb.AppendLine(GetServiceUsing(genConfig));
            sb.Append(GetClassDescription(table, "服务实现类"));
            sb.Append(GetName(table, "Service"));
            sb.AppendLine(GetTabs(1) + "{");
            sb.Append(GetServiceBody(table));
            sb.AppendLine(GetTabs(1) + "}");
            sb.AppendLine("}");
            return sb;
        }

        private static StringBuilder GeneratorServiceInterface(GenConfig genConfig, GenTable table)
        {
            var sb = new StringBuilder();
            sb.Append(GetHeader(genConfig, table, "Service.Interface"));
            sb.AppendLine("{");
            sb.Append(GetInterfaceUsing(genConfig));
            sb.Append(GetClassDescription(table, "服务接口类"));
            sb.Append(GetName(table, "Service", true));
            sb.AppendLine(GetTabs(1) + "{");
            sb.Append(GetInterfaceBody(table));
            sb.AppendLine(GetTabs(1) + "}");
            sb.AppendLine("}");
            return sb;
        }

        #region 获取tab占位字符串
        /// <summary>
        /// 获取tab占位字符串
        /// </summary>
        /// <param name="i">个数</param>
        /// <returns></returns>
        private static string GetTabs(int i)
        {
            StringBuilder tabs = new StringBuilder();
            string strTab = "   ";//一个tab占位
            for (int j = 0; j <i; j++)
            {
                tabs.Append(strTab);
            }
            return tabs.ToString();
        }
        #endregion


        #region 设置带名称空间时,类等tab占位
        
        private static string ConvertDbType(string dbType, string provider)
        {
            if (provider.Equals(DbProviderNames.Oracle_Managed_ODP) || provider.Equals(DbProviderNames.Oracle_ODP) || provider.Equals(DbProviderNames.Oracle_Managed_ODP))
                return ConvertOracleDbType(dbType);
            return dbType;
        }

        private static string ConvertOracleDbType(string dbType)
        {
            switch(dbType.ToUpper())
            {
                case "BLOB":
                    return "Binary";
                case "NCLOB":
                    return "NText";
                case "CLOB":
                    return "Text";
                case "NVARCHAR2":
                    return "NVarChar";
                case "VARCHAR2":
                    return "VarChar";
                case "NCHAR":
                    return "NChar";
                case "DATE":
                    return "DateTime";
                case "NUMBER":
                    return "Int32";
                default:
                    return dbType.ToLower().FirstUpper();
            }
            
        }

        /// <summary>
        /// 数据库类型到C#类型的转换
        /// </summary>
        /// <param name="dbtype">数据库中的类型</param>
        /// <returns></returns>
        private static string ConvertType(string dbtype, string nullabel)
        {
            string ret = "string";
            switch (dbtype.ToLower())
            {
                case "bit":
                    ret = "bool";
                    break;
                case "int":
                case "number":
                case "smallint":
                case "tinyint":
                case "bigint":
                    if (nullabel.ToUpper().Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                    {
                        ret = "int?";
                    }
                    else
                    {
                        ret = "int";
                    }

                    break;
                case "smallmoney":
                case "decimal":
                case "numeric":

                    ret = "decimal";
                    if (nullabel.ToUpper().Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                    {
                        ret = "decimal?";
                    }
                    break;
                case "float":
                    ret = "float";
                    if (nullabel.ToUpper().Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                    {
                        ret = "float?";
                    }
                    break;
                case "money":
                    ret = "double";
                    if (nullabel.ToUpper().Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                    {
                        ret = "double?";
                    }
                    break;
                case "datetime":
                case "date":
                case "smalldatetime":
                    ret = "DateTime";
                    if (nullabel.ToUpper().Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                    {
                        ret = "DateTime?";
                    }
                    break;
                default:
                    ret = "string";
                    break;
            }
            return ret;
        }
        #endregion
    }
}
