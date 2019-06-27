using System;
using System.Reflection;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data
{
    class DataAnnotationMappingAdapter
    {
        internal const string StrAssociationAttribute = "System.ComponentModel.DataAnnotations.AssociationAttribute";
        internal const string StrKeyAttribute = "System.ComponentModel.DataAnnotations.KeyAttribute";
        internal const string StrRequiredAttribute = "System.ComponentModel.DataAnnotations.RequiredAttribute";
        internal const string StrStringLengthAttribute = "System.ComponentModel.DataAnnotations.StringLengthAttribute";
        internal const string StrAssemblyName = "System.ComponentModel.DataAnnotations";

#if !SDK35
        public static Type AssociationAttributeType;
        public static Type KeyAttributeType;
        public AssociationAttribute Association;
        internal class AssociationAttribute
        {
            public Getter ThisKey;
            public Getter OtherKey;
            public Getter IsForeignKey;
        }

#endif

        public static Type RequiredAttributeType;
        public static Type StringLengthAttributeType;


        public StringLengthAttribute StringLength;


        internal class StringLengthAttribute
        {
            public Getter Length;
        }

        public static DataAnnotationMappingAdapter Instance { get; private set; }
        public static void Init(Assembly asm)
        {
            var instance = new DataAnnotationMappingAdapter();
            Instance = instance;

#if !SDK35
            AssociationAttributeType = asm.GetType(StrAssociationAttribute);
            KeyAttributeType = asm.GetType(StrKeyAttribute);

            instance.Association = new AssociationAttribute();
            instance.Association.ThisKey = AssociationAttributeType.GetProperty("ThisKey").GetGetter();
            instance.Association.OtherKey = AssociationAttributeType.GetProperty("OtherKey").GetGetter();
            instance.Association.IsForeignKey = AssociationAttributeType.GetProperty("IsForeignKey").GetGetter();
#endif

            RequiredAttributeType = asm.GetType(StrRequiredAttribute);
            StringLengthAttributeType = asm.GetType(StrStringLengthAttribute);

            instance.StringLength = new StringLengthAttribute();
            instance.StringLength.Length = StringLengthAttributeType.GetProperty("MaximumLength").GetGetter();
        }


    }
}
