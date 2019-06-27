using System;
using System.Reflection;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data
{
    class EFDataAnnotiationAdapter
    {

        internal const string StrAssemblyName = "EntityFramework";

        internal static Type NotMappedAttributeType;
        internal static Type TableAttributeType;
        internal static Type ColumndAttributeType;
        internal static Type DatabaseGeneratedAttributeType;
        internal static Type MaxLengthAttributeType;
        internal static Type ForeignKeyAttributeType;

        internal static string StrNotMappedAttribute = "System.ComponentModel.DataAnnotations.NotMappedAttribute";
        internal static string StrTableAttribute = "System.ComponentModel.DataAnnotations.TableAttribute";
        internal static string StrColumnAttribute = "System.ComponentModel.DataAnnotations.ColumnAttribute";
        internal static string StrDatabaseGeneratedAttribute = "System.ComponentModel.DataAnnotations.DatabaseGeneratedAttribute";
        internal static string StrMaxLengthAttribute = "System.ComponentModel.DataAnnotations.MaxLengthAttribute";
        internal static string StrForeignKeyAttribute = "System.ComponentModel.DataAnnotations.ForeignKeyAttribute";


        public TableAttribute Table;
        internal class TableAttribute
        {
            public Getter Schema;
            public Getter Name;
        }

        public ColumnAttribute Column;
        internal class ColumnAttribute
        {
            public Getter Name;
            public Getter TypeName;
        }

        public DatabaseGeneratedAttribute DatabaseGenerated;
        internal class DatabaseGeneratedAttribute
        {
            public Getter DatabaseGeneratedOption;
        }

        public ForeignKeyAttribute ForeignKey;
        internal class ForeignKeyAttribute
        {
            public Getter Name;
        }

        public MaxLengthAttribute MaxLength;
        internal class MaxLengthAttribute
        {
            public Getter Length;
        }

        public static EFDataAnnotiationAdapter Instance { get; private set; }
        public static void Init(Assembly asm)
        {


            asm = AssemblyLoader.Load("System.ComponentModel.DataAnnotations");
            Guard.NotNull(asm, "asm");

            StrNotMappedAttribute = "System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute";
            StrTableAttribute = "System.ComponentModel.DataAnnotations.Schema.TableAttribute";
            StrColumnAttribute = "System.ComponentModel.DataAnnotations.Schema.ColumnAttribute";
            StrDatabaseGeneratedAttribute = "System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute";
            StrForeignKeyAttribute = "System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute";


            EFDataAnnotiationAdapter.NotMappedAttributeType = asm.GetType(StrNotMappedAttribute);
            EFDataAnnotiationAdapter.TableAttributeType = asm.GetType(StrTableAttribute);
            EFDataAnnotiationAdapter.ColumndAttributeType = asm.GetType(StrColumnAttribute);
            EFDataAnnotiationAdapter.DatabaseGeneratedAttributeType = asm.GetType(StrDatabaseGeneratedAttribute);
            EFDataAnnotiationAdapter.MaxLengthAttributeType = asm.GetType(StrMaxLengthAttribute);
            EFDataAnnotiationAdapter.ForeignKeyAttributeType = asm.GetType(StrForeignKeyAttribute);



            var instance = new EFDataAnnotiationAdapter();
            instance.Table = new TableAttribute();
            instance.Table.Schema = EFDataAnnotiationAdapter.TableAttributeType.GetProperty("Schema").GetGetter();
            instance.Table.Name = EFDataAnnotiationAdapter.TableAttributeType.GetProperty("Name").GetGetter();

            instance.Column = new ColumnAttribute();
            instance.Column.Name = EFDataAnnotiationAdapter.ColumndAttributeType.GetProperty("Name").GetGetter();
            instance.Column.TypeName = EFDataAnnotiationAdapter.ColumndAttributeType.GetProperty("TypeName").GetGetter();

            instance.DatabaseGenerated = new DatabaseGeneratedAttribute();
            instance.DatabaseGenerated.DatabaseGeneratedOption = EFDataAnnotiationAdapter.DatabaseGeneratedAttributeType.GetProperty("DatabaseGeneratedOption").GetGetter();

            instance.ForeignKey = new ForeignKeyAttribute();
            instance.ForeignKey.Name = EFDataAnnotiationAdapter.ForeignKeyAttributeType.GetProperty("Name").GetGetter();

            instance.MaxLength = new MaxLengthAttribute();
            instance.MaxLength.Length = EFDataAnnotiationAdapter.MaxLengthAttributeType.GetProperty("Length").GetGetter();
            Instance = instance;
        }
    }
}
