using System;
using System.Collections.Generic;
using System.Data.OleDb;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Common
{
    class TypeMapping
    {
        public static readonly Dictionary<int, TypeMappingInfo> OleDbMap = new Dictionary<int, TypeMappingInfo>
        {
            //{(int)OleDbType.BigInt,new TypeMappingInfo{ CLRType = Types.Int64, DbType = DBType.Int64, ProviderDbType = (int)OleDbType.BigInt}},
            //{(int)OleDbType.Binary,new TypeMappingInfo{ CLRType = Types.ByteArray, DbType = DBType.Binary, ProviderDbType = (int)OleDbType.Binary}},
            //{(int)OleDbType.Boolean,new TypeMappingInfo{ CLRType = Types.Boolean, DbType = DBType.Boolean, ProviderDbType = (int)OleDbType.Boolean}},
            //{(int)OleDbType.BSTR,new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.NVarChar, ProviderDbType = (int)OleDbType.BSTR}},
            //{(int)OleDbType.Char,new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.Char, ProviderDbType = (int)OleDbType.Char}},
            //{(int)OleDbType.Currency,new TypeMappingInfo{ CLRType = Types.Decimal, DbType = DBType.Currency, ProviderDbType = (int)OleDbType.Currency}},
            //{(int)OleDbType.Date,new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime, ProviderDbType = (int)OleDbType.Date}},
            //{(int)OleDbType.DBDate,new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime, ProviderDbType = (int)OleDbType.DBDate}},
            //{(int)OleDbType.DBTime,new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime, ProviderDbType = (int)OleDbType.DBTime}},
            //{(int)OleDbType.DBTimeStamp,new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime, ProviderDbType = (int)OleDbType.DBTimeStamp}},
            //{(int)OleDbType.Decimal,new TypeMappingInfo{ CLRType = Types.Decimal, DbType = DBType.Decimal, ProviderDbType = (int)OleDbType.Decimal}},
            //{(int)OleDbType.Double,new TypeMappingInfo{ CLRType = Types.Double, DbType = DBType.Double, ProviderDbType = (int)OleDbType.Double}},
            //{(int)OleDbType.Filetime,new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime, ProviderDbType = (int)OleDbType.Filetime}},
            //{(int)OleDbType.Guid,new TypeMappingInfo{ CLRType = Types.Guid, DbType = DBType.Guid, ProviderDbType = (int)OleDbType.Guid}},
            //{(int)OleDbType.Integer,new TypeMappingInfo{ CLRType = Types.Int32, DbType = DBType.Int32, ProviderDbType = (int)OleDbType.Integer}},
            //{(int)OleDbType.LongVarBinary,new TypeMappingInfo{ CLRType = Types.ByteArray, DbType = DBType.Binary, ProviderDbType = (int)OleDbType.LongVarBinary}},
            //{(int)OleDbType.LongVarChar,new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.NVarChar, ProviderDbType = (int)OleDbType.LongVarChar}},
            //{(int)OleDbType.LongVarWChar,new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.NVarChar, ProviderDbType = (int)OleDbType.LongVarWChar}},
            //{(int)OleDbType.Numeric,new TypeMappingInfo{ CLRType = Types.Decimal, DbType = DBType.Decimal, ProviderDbType = (int)OleDbType.Numeric}},
            //{(int)OleDbType.Single,new TypeMappingInfo{ CLRType = Types.Single, DbType = DBType.Single, ProviderDbType = (int)OleDbType.Single}},
            //{(int)OleDbType.SmallInt,new TypeMappingInfo{ CLRType = Types.Int16, DbType = DBType.Int16, ProviderDbType = (int)OleDbType.SmallInt}},
            //{(int)OleDbType.TinyInt,new TypeMappingInfo{ CLRType = Types.SByte, DbType = DBType.Byte, ProviderDbType = (int)OleDbType.TinyInt}},
            //{(int)OleDbType.UnsignedBigInt,new TypeMappingInfo{ CLRType = Types.UInt64, DbType = DBType.Int64, ProviderDbType = (int)OleDbType.UnsignedBigInt}},
            //{(int)OleDbType.UnsignedInt,new TypeMappingInfo{ CLRType = Types.UInt32, DbType = DBType.Int32, ProviderDbType = (int)OleDbType.UnsignedInt}},
            //{(int)OleDbType.UnsignedSmallInt,new TypeMappingInfo{ CLRType = Types.UInt16, DbType = DBType.Int16, ProviderDbType = (int)OleDbType.UnsignedSmallInt}},
            //{(int)OleDbType.UnsignedTinyInt,new TypeMappingInfo{ CLRType = Types.Byte, DbType = DBType.Byte, ProviderDbType = (int)OleDbType.UnsignedTinyInt}},
            //{(int)OleDbType.VarBinary,new TypeMappingInfo{ CLRType = Types.ByteArray, DbType = DBType.Binary, ProviderDbType = (int)OleDbType.VarBinary}},
            //{(int)OleDbType.VarChar,new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.VarChar, ProviderDbType = (int)OleDbType.VarChar}},
            //{(int)OleDbType.VarNumeric,new TypeMappingInfo{ CLRType = Types.Decimal, DbType = DBType.Decimal, ProviderDbType = (int)OleDbType.VarNumeric}},
            //{(int)OleDbType.VarWChar,new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.VarChar, ProviderDbType = (int)OleDbType.VarWChar}},
            //{(int)OleDbType.WChar,new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.Char, ProviderDbType = (int)OleDbType.WChar}},
        };


        public static readonly Dictionary<string, TypeMappingInfo> OracleDbMap = new Dictionary<string, TypeMappingInfo>(StringComparer.OrdinalIgnoreCase)
        {
            {"BLOB", new TypeMappingInfo{ CLRType= Types.ByteArray, DbType = DBType.Image, ProviderDbType =2, NativeType = "BLOB"}},
            {"CLOB",new TypeMappingInfo{ CLRType = Types.String,DbType = DBType.Text,ProviderDbType =4,NativeType = "CLOB"}},
            {"DATE", new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime,ProviderDbType =6, NativeType = "DATE"}},
            {"LONG",new TypeMappingInfo{ CLRType= Types.String,DbType = DBType.NVarChar,ProviderDbType =10,NativeType = "LONG"}},
            {"LONG RAW",new TypeMappingInfo {CLRType = Types.ByteArray, DbType = DBType.Binary,ProviderDbType =9, NativeType = "LONG RAW"}},
            {"NCLOB",new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.NText,ProviderDbType =12, NativeType = "NCLOB"}},
            {"NUMBER", new TypeMappingInfo{ CLRType = Types.Decimal, DbType = DBType.Decimal,ProviderDbType =13, NativeType = "NUMBER"}},
            {"NVARCHAR2", new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.NVarChar,ProviderDbType =14, NativeType = "NVARCHAR2"}},
            {"RAW",new TypeMappingInfo{ CLRType = Types.ByteArray, DbType = DBType.Binary,ProviderDbType =15, NativeType = "RAW"}},
            {"ROWID", new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.NVarChar,ProviderDbType =16, NativeType = "ROWID"}},
            {"CHAR",new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.Char,ProviderDbType =254, NativeType = "CHAR"}},
            {"INTERVAL DAY TO SECOND",new TypeMappingInfo{ CLRType = Types.TimeSpan, DbType = DBType.DateTime,ProviderDbType =7, NativeType = "INTERVAL DAY TO SECOND"}}, 
            {"INTERVAL YEAR TO MONTH",new TypeMappingInfo{ CLRType = Types.Int32, DbType = DBType.Int32,ProviderDbType =8, NativeType ="INTERVAL YEAR TO MONTH"}},
            {"NCHAR",new TypeMappingInfo{ CLRType= Types.String, DbType = DBType.NChar,ProviderDbType =11, NativeType ="NCHAR"}},
            {"TIMESTAMP",new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime,ProviderDbType =18, NativeType = "TIMESTAMP"}},
            {"TIMESTAMP WITH LOCAL TIME ZONE",new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime,ProviderDbType =19, NativeType = "TIMESTAMP WITH LOCAL TIME ZONE"}},
            {"TIMESTAMP WITH TIME ZONE",new TypeMappingInfo{ CLRType= Types.DateTime, DbType = DBType.DateTime,ProviderDbType =20,NativeType = "TIMESTAMP WITH TIME ZONE"}},
            {"VARCHAR2",new TypeMappingInfo{ CLRType= Types.String, DbType = DBType.VarChar,ProviderDbType =22, NativeType = "VARCHAR2"}},
            {"FLOAT",new TypeMappingInfo{ CLRType= Types.Decimal, DbType = DBType.Decimal,ProviderDbType =29, NativeType ="FLOAT"}},
            
        };

        public static readonly Dictionary<string, TypeMappingInfo> MySQLDbMap = new Dictionary<string, TypeMappingInfo>(StringComparer.OrdinalIgnoreCase)
        {
            {"BLOB", new TypeMappingInfo{ CLRType= Types.ByteArray, DbType = DBType.Image, ProviderDbType =252, NativeType = "BLOB"}},
            {"TINYBLOB",new TypeMappingInfo{ CLRType = Types.ByteArray,DbType = DBType.Image,ProviderDbType =249,NativeType = "TINYBLOB"}},
            {"MEDIUMBLOB", new TypeMappingInfo{ CLRType = Types.ByteArray, DbType = DBType.Image,ProviderDbType =250, NativeType = "MEDIUMBLOB"}},
            {"LONGBLOB",new TypeMappingInfo{ CLRType= Types.ByteArray,DbType = DBType.Image,ProviderDbType =251,NativeType = "LONGBLOB"}},
            {"BINARY",new TypeMappingInfo {CLRType = Types.ByteArray, DbType = DBType.Binary,ProviderDbType =600, NativeType = "BINARY"}},
            {"VARBINARY",new TypeMappingInfo{ CLRType = Types.ByteArray, DbType = DBType.Binary,ProviderDbType =601, NativeType = "VARBINARY"}},
           
            {"DATE", new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime,ProviderDbType =10, NativeType = "DATE"}},
            {"DATETIME", new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime,ProviderDbType =12, NativeType = "DATETIME"}},
            {"TIMESTAMP",new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime,ProviderDbType =7, NativeType = "TIMESTAMP"}},
            {"TIME", new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime,ProviderDbType =11, NativeType = "TIME"}},
            
            {"CHAR",new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.Char,ProviderDbType =254, NativeType = "CHAR"}},
            {"NCHAR",new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.NChar,ProviderDbType =254, NativeType = "NCHAR"}}, 
            {"VARCHAR",new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.VarChar,ProviderDbType =253, NativeType ="VARCHAR"}},
            {"NVARCHAR",new TypeMappingInfo{ CLRType= Types.String, DbType = DBType.NVarChar,ProviderDbType =253, NativeType ="NVARCHAR"}},
           
            {"SET",new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.NVarChar,ProviderDbType =248, NativeType = "SET"}},
            {"ENUM",new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.NVarChar,ProviderDbType =247, NativeType = "ENUM"}},
            {"TINYTEXT",new TypeMappingInfo{ CLRType= Types.String, DbType = DBType.NText,ProviderDbType =749,NativeType = "TINYTEXT"}},
            {"TEXT",new TypeMappingInfo{ CLRType= Types.String, DbType = DBType.NText,ProviderDbType =752, NativeType = "TEXT"}},
            {"MEDIUMTEXT",new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.NText,ProviderDbType =750, NativeType = "MEDIUMTEXT"}},
            {"LONGTEXT", new TypeMappingInfo { CLRType = Types.String, DbType = DBType.NText,ProviderDbType =751, NativeType = "LONGTEXT"}},

            {"DOUBLE",new TypeMappingInfo{ CLRType = Types.Double, DbType = DBType.Double,ProviderDbType =5, NativeType = "DOUBLE"}},
            {"FLOAT",new TypeMappingInfo{ CLRType= Types.Single, DbType = DBType.Single,ProviderDbType =4, NativeType ="FLOAT"}},
            {"TINYINT",new TypeMappingInfo { CLRType= Types.Byte, DbType = DBType.Byte,ProviderDbType =1, NativeType = "TINYINT"}},
            {"SMALLINT",new TypeMappingInfo{CLRType= Types.Int16, DbType= DBType.Int16,ProviderDbType =2, NativeType = "SMALLINT"}},
            {"INT",new TypeMappingInfo{ CLRType= Types.Int32, DbType= DBType.Int32,ProviderDbType =3, NativeType = "INT"}},
            
            {"YEAR",new TypeMappingInfo{ CLRType= Types.Int32,DbType= DBType.Int32,ProviderDbType =13, NativeType = "YEAR"}},
            {"MEDIUMINT",new TypeMappingInfo{CLRType= Types.Int32,DbType = DBType.Int32,ProviderDbType =9, NativeType = "MEDIUMINT"} },
            {"BIGINT",new TypeMappingInfo{CLRType= Types.Int64,DbType = DBType.Int64,ProviderDbType =8, NativeType = "BIGINT"} },
            {"DECIMAL",new TypeMappingInfo{CLRType= Types.Decimal,DbType = DBType.Decimal,ProviderDbType =246, NativeType = "DECIMAL"} },
            {"TINY INT",new TypeMappingInfo{ CLRType = Types.Byte, DbType = DBType.Byte,ProviderDbType =501, NativeType = "TINY INT"}},
        };

        public static readonly Dictionary<string, TypeMappingInfo> SQLiteDbMap = new Dictionary<string, TypeMappingInfo>(StringComparer.OrdinalIgnoreCase)
        {
            {"int", new TypeMappingInfo{ CLRType= Types.Int32, DbType = DBType.Int32, ProviderDbType =11, NativeType = "int"}},
            {"real",new TypeMappingInfo{ CLRType = Types.Single,DbType = DBType.Single,ProviderDbType =15,NativeType = "real"}},
            {"float", new TypeMappingInfo{ CLRType = Types.Double, DbType = DBType.Double,ProviderDbType =8, NativeType = "float"}},
            {"double",new TypeMappingInfo{ CLRType= Types.Double,DbType = DBType.Double,ProviderDbType =8,NativeType = "double"}},
            {"money",new TypeMappingInfo {CLRType = Types.Decimal, DbType = DBType.Currency,ProviderDbType =7, NativeType = "money"}},
            {"currency",new TypeMappingInfo{ CLRType = Types.Decimal, DbType = DBType.Currency,ProviderDbType =7, NativeType = "currency"}},
            {"decimal", new TypeMappingInfo{ CLRType = Types.Decimal, DbType = DBType.Decimal,ProviderDbType =7, NativeType = "decimal"}},
            {"numeric", new TypeMappingInfo{ CLRType = Types.Decimal, DbType = DBType.Decimal,ProviderDbType =7, NativeType = "numeric"}},
            {"bit",new TypeMappingInfo{ CLRType = Types.Boolean, DbType = DBType.Boolean,ProviderDbType =3, NativeType = "bit"}},
            {"yesno", new TypeMappingInfo{ CLRType = Types.Boolean, DbType = DBType.Boolean,ProviderDbType =3, NativeType = "yesno"}},
            {"logical",new TypeMappingInfo{ CLRType = Types.Boolean, DbType = DBType.Boolean,ProviderDbType =3, NativeType = "logical"}},
            {"bool",new TypeMappingInfo{ CLRType = Types.Boolean, DbType = DBType.Boolean,ProviderDbType =3, NativeType = "Boolean"}}, 
            {"boolean",new TypeMappingInfo{ CLRType = Types.Boolean, DbType = DBType.Boolean,ProviderDbType =3, NativeType ="boolean"}},
            {"tinyint",new TypeMappingInfo{ CLRType= Types.Byte, DbType = DBType.Byte,ProviderDbType =2, NativeType ="tinyint"}},
            {"integer",new TypeMappingInfo{ CLRType = Types.Int64, DbType = DBType.Int64,ProviderDbType =12, NativeType = "integer"}},
            {"counter",new TypeMappingInfo{ CLRType = Types.Int64, DbType = DBType.Int64,ProviderDbType =12, NativeType = "counter"}},
            {"autoincrement",new TypeMappingInfo{ CLRType= Types.Int64, DbType = DBType.Int64,ProviderDbType =12,NativeType = "autoincrement"}},
            {"identity",new TypeMappingInfo{ CLRType= Types.Int64, DbType = DBType.Int64,ProviderDbType =12, NativeType = "identity"}},
            {"long",new TypeMappingInfo{ CLRType = Types.Int64, DbType = DBType.Int64,ProviderDbType =12, NativeType = "long"}},
            {"bigint", new TypeMappingInfo { CLRType = Types.Int64, DbType = DBType.Int64,ProviderDbType =12, NativeType = "bigint"}},
            {"binary",new TypeMappingInfo{CLRType= Types.ByteArray, DbType = DBType.Binary,ProviderDbType =1, NativeType = "binary"}},

            {"varbinary",new TypeMappingInfo{ CLRType = Types.ByteArray, DbType = DBType.Binary,ProviderDbType =1, NativeType = "varbinary"}},
            {"blob",new TypeMappingInfo{ CLRType= Types.ByteArray, DbType = DBType.Image,ProviderDbType =1, NativeType ="blob"}},
            {"image",new TypeMappingInfo { CLRType= Types.ByteArray, DbType = DBType.Image,ProviderDbType =1, NativeType = "image"}},
            {"general",new TypeMappingInfo{CLRType= Types.ByteArray, DbType= DBType.Binary,ProviderDbType =1, NativeType = "general"}},
            {"oleobject",new TypeMappingInfo{ CLRType= Types.ByteArray, DbType= DBType.Binary,ProviderDbType =1, NativeType = "oleobject"}},
            {"varchar",new TypeMappingInfo{ CLRType= Types.String,DbType= DBType.VarChar,ProviderDbType =16, NativeType = "varchar"}},
            {"nvarchar",new TypeMappingInfo{CLRType= Types.String,DbType = DBType.NVarChar,ProviderDbType =16, NativeType = "nvarchar"} },

            {"memo",new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.NText,ProviderDbType =16, NativeType = "memo"}},
            {"longtext",new TypeMappingInfo{ CLRType= Types.String, DbType = DBType.NText,ProviderDbType =16, NativeType ="longtext"}},
            {"note",new TypeMappingInfo { CLRType= Types.String, DbType = DBType.NText,ProviderDbType =16, NativeType = "note"}},
            {"text",new TypeMappingInfo{CLRType= Types.String, DbType= DBType.Text,ProviderDbType =16, NativeType = "text"}},
            {"ntext",new TypeMappingInfo{ CLRType= Types.String, DbType= DBType.NText,ProviderDbType =16, NativeType = "ntext"}},
            {"string",new TypeMappingInfo{ CLRType= Types.String,DbType= DBType.NVarChar,ProviderDbType =16, NativeType = "string"}},
            {"char",new TypeMappingInfo{CLRType= Types.String,DbType = DBType.Char,ProviderDbType =16, NativeType = "char"} },

            {"nchar",new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.NChar,ProviderDbType =16, NativeType = "nchar"}},
            {"datetime",new TypeMappingInfo{ CLRType= Types.DateTime, DbType = DBType.DateTime,ProviderDbType =6, NativeType ="datetime"}},
            {"smalldate",new TypeMappingInfo { CLRType= Types.DateTime, DbType = DBType.DateTime,ProviderDbType =6, NativeType = "smalldate"}},
            {"timestamp",new TypeMappingInfo{CLRType= Types.DateTime, DbType= DBType.DateTime,ProviderDbType =6, NativeType = "timestamp"}},
            {"date",new TypeMappingInfo{ CLRType= Types.DateTime, DbType= DBType.DateTime,ProviderDbType =6, NativeType = "date"}},
            {"time",new TypeMappingInfo{ CLRType= Types.DateTime,DbType= DBType.DateTime,ProviderDbType =6, NativeType = "time"}},
            {"uniqueidentifier",new TypeMappingInfo{CLRType= Types.Guid,DbType = DBType.Guid,ProviderDbType =4, NativeType = "uniqueidentifier"} },
            {"guid",new TypeMappingInfo{CLRType= Types.Guid,DbType = DBType.Guid,ProviderDbType =4, NativeType = "guid"} },
        };

        public static readonly Dictionary<string, TypeMappingInfo> SqlDbMap = new Dictionary<string, TypeMappingInfo>(StringComparer.OrdinalIgnoreCase)
        {
            {"BigInt", new TypeMappingInfo{ CLRType= Types.Int64, DbType = DBType.Int64, NativeType = "BigInt"}},
            {"Binary",new TypeMappingInfo{ CLRType = Types.ByteArray,DbType = DBType.Binary,NativeType = "Binary"}},
            {"Bit", new TypeMappingInfo{ CLRType = Types.Boolean, DbType = DBType.Boolean, NativeType = "Bit"}},
            {"Char",new TypeMappingInfo{ CLRType= Types.String,DbType = DBType.Char,NativeType = "Char"}},
            {"Date",new TypeMappingInfo {CLRType = Types.DateTime, DbType = DBType.DateTime, NativeType = "Date"}},
            {"DateTime",new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime, NativeType = "DateTime"}},
            {"DateTime2", new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime, NativeType = "DateTime2"}},
            {"DateTimeOffset",new TypeMappingInfo{ CLRType = typeof(DateTimeOffset), DbType = DBType.DateTime, NativeType = "DateTimeOffset"}},
            {"Decimal", new TypeMappingInfo{ CLRType = Types.Decimal, DbType = DBType.Decimal, NativeType = "Decimal"}},
            {"Float",new TypeMappingInfo{ CLRType = Types.Double, DbType = DBType.Double, NativeType = "Float"}},
            {"Image",new TypeMappingInfo{ CLRType = Types.ByteArray, DbType = DBType.Image, NativeType = "Image"}}, 
            {"Int",new TypeMappingInfo{ CLRType = Types.Int32, DbType = DBType.Int32, NativeType ="Int"}},
            {"Money",new TypeMappingInfo{ CLRType= Types.Decimal, DbType = DBType.Currency, NativeType ="Money"}},
            {"Numeric",new TypeMappingInfo{ CLRType= Types.Decimal, DbType = DBType.Decimal, NativeType ="Numeric"}},
            {"NChar",new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.NChar, NativeType = "NChar"}},
            {"NText",new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.NText, NativeType = "NText"}},
            {"NVarChar",new TypeMappingInfo{ CLRType= Types.String, DbType = DBType.NVarChar,NativeType = "NVarChar"}},
            {"Real",new TypeMappingInfo{ CLRType= Types.Single, DbType = DBType.Single, NativeType = "Real"}},
            {"SmallDateTime",new TypeMappingInfo{ CLRType = Types.DateTime, DbType = DBType.DateTime, NativeType = "SmallDateTime"}},
            {"SmallInt", new TypeMappingInfo { CLRType = Types.Int16, DbType = DBType.Int16, NativeType = "SmallInt"}},
            {"SmallMoney",new TypeMappingInfo{CLRType= Types.Decimal, DbType = DBType.Currency, NativeType = "SmallMoney"}},
            {"Text",new TypeMappingInfo{ CLRType = Types.String, DbType = DBType.Text, NativeType = "Text"}},
            {"Time",new TypeMappingInfo{ CLRType= Types.TimeSpan, DbType = DBType.DateTime, NativeType ="Time"}},
            {"Timestamp",new TypeMappingInfo { CLRType= Types.ByteArray, DbType = DBType.Binary, NativeType = "Timestamp"}},
            {"TinyInt",new TypeMappingInfo{CLRType= Types.Byte, DbType= DBType.Byte, NativeType = "TinyInt"}},
            {"UniqueIdentifier",new TypeMappingInfo{ CLRType= Types.Guid, DbType= DBType.Guid, NativeType = "UniqueIdentifier"}},
            {"VarBinary",new TypeMappingInfo{ CLRType= Types.ByteArray,DbType= DBType.Binary, NativeType = "VarBinary"}},
            {"VarChar",new TypeMappingInfo{CLRType= Types.String,DbType = DBType.VarChar, NativeType = "VarChar"} },
        };


    }
}
