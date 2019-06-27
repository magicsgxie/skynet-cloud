using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Reflection
{
    class DefaultDynamicMethodFactory
    {
        #region Helper methods

        private static readonly ConstructorInfo TargetParameterCountExceptionConstructor =
          typeof(TargetParameterCountException).GetConstructor(Type.EmptyTypes);
        private static readonly Module Module = DynamicAssemblyManager.Module;


        private static DynamicMethod CreateDynamicFunc()
        {
            return new DynamicMethod(String.Empty, typeof(object), new[] { typeof(object), typeof(object[]) }, Module, true);
        }

        private static DynamicMethod CreateDynamicProc()
        {
            return new DynamicMethod(String.Empty, typeof(void), new[] { typeof(object), typeof(object[]) }, Module, true);
        }

        private static void EmitDynamicMethod(MethodInfo method, DynamicMethod callable)
        {
            var info = new MethodMataData(method);

            ILGenerator il = callable.GetILGenerator();

            EmitLoadParameters(info, il, 1);

            if (method.IsStatic)
                il.EmitCall(OpCodes.Call, method, null);
            else if (method.IsVirtual)
                il.EmitCall(OpCodes.Callvirt, method, null);
            else
                il.EmitCall(OpCodes.Call, method, null);

            if (method.ReturnType == typeof(void))
                il.Emit(OpCodes.Ldnull);
            else
            {
                if (method.ReturnType.IsValueType)
                    il.Emit(OpCodes.Box, method.ReturnType);
            }
            il.Emit(OpCodes.Ret);

        }

        private static DynamicMethod CreateDynamicFactoryMethod()
        {
            return new DynamicMethod(String.Empty, typeof(object), new[] { typeof(object[]) }, Module, true);
        }

        private static DynamicMethod CreateDynamicGetterMethod()
        {
            return new DynamicMethod(String.Empty, typeof(object), new[] { typeof(object) }, Module, true);
        }

        private static DynamicMethod CreateDynamicSetterMethod()
        {
            return new DynamicMethod(String.Empty, typeof(void), new[] { typeof(object), typeof(object) }, Module, true);
        }


        private static void EmitLoadParameters(MethodMataData info, ILGenerator il, int argumentArrayIndex)
        {
            if (!info.Method.IsStatic && !(info.Method is ConstructorInfo))
            {
                il.Emit(OpCodes.Ldarg_0);
                EmitHelper.UnboxOrCast(il, info.Method.DeclaringType);
            }

            for (int index = 0; index < info.Parameters.Length; index++)
            {
                EmitHelper.LoadArgument(il, argumentArrayIndex);
                EmitHelper.LoadInt(il, index);
                il.Emit(OpCodes.Ldelem_Ref);
                EmitHelper.UnboxOrCast(il, info.ParameterTypes[index]);
            }
        }


        #endregion

        public static Func CreateFuncMethod(MethodInfo method)
        {
            var dm = CreateDynamicFunc();
            EmitDynamicMethod(method, dm);

            return dm.CreateDelegate(typeof(Func)) as Func;
        }


        public static Proc CreateProcMethod(MethodInfo method)
        {
            var func = CreateFuncMethod(method);
            Proc result = (target, args) => func(target, args);
            return result;
        }

        public static ConstructorHandler CreateConstructorMethod(ConstructorInfo constructor)
        {
            DynamicMethod callable = CreateDynamicFactoryMethod();
            var info = new MethodMataData(constructor);

            Type returnType = constructor.ReflectedType;
            ILGenerator il = callable.GetILGenerator();

            EmitLoadParameters(info, il, 0);

            il.Emit(OpCodes.Newobj, constructor);

            if (info.ReturnType.IsValueType)
                il.Emit(OpCodes.Box, returnType);

            il.Emit(OpCodes.Ret);

            return callable.CreateDelegate(typeof(ConstructorHandler)) as ConstructorHandler;
        }

        public static DefaultConstructorHandler CreateDefaultConstructorMethod(Type type)
        {
            if (type == Types.String)
            {
                DefaultConstructorHandler s = () => null;
                return s;
            }

            //if (binderType.IsValueType)
            //{
            //    var dm = new DynamicMethod(string.Empty, binderType, Type.EmptyTypes, Module, true);
            //    var ilGen = dm.GetILGenerator();
            //    ilGen.Emit(OpCodes.Ldloc_0);
            //    ilGen.Emit(OpCodes.Stloc_1);
            //    ilGen.Emit(OpCodes.Br_S);
            //    ilGen.Emit(OpCodes.Ldloc_1);
            //    ilGen.Emit(OpCodes.Ret);
            //    return dm.CreateDelegate(typeof(DefaultConstructorHandler)) as DefaultConstructorHandler;
            //}

            var ctorExpression = Expression
                .Lambda<DefaultConstructorHandler>(
                    Expression
                        .Convert(Expression.New(type), typeof(object)));
            return ctorExpression.Compile();
        }

        #region Getter
        private static Getter CreateGetter(FieldInfo field)
        {
            DynamicMethod callable = CreateDynamicGetterMethod();

            Type returnType = field.FieldType;
            ILGenerator il = callable.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            EmitHelper.UnboxOrCast(il, field.DeclaringType);
            il.Emit(OpCodes.Ldfld, field);

            if (returnType.IsValueType)
                il.Emit(OpCodes.Box, returnType);

            il.Emit(OpCodes.Ret);

            return callable.CreateDelegate(typeof(Getter)) as Getter;
        }


        private static Getter CreateGetter(PropertyInfo property)
        {
            MethodInfo method = property.GetGetMethod();

            if (method == null)
                method = property.GetGetMethod(true);

            return CreateGetter(method);
        }


        private static Getter CreateGetter(MethodInfo method)
        {
            var callable = CreateDynamicGetterMethod();

            Type returnType = method.ReturnType;
            ILGenerator il = callable.GetILGenerator();


            il.Emit(OpCodes.Ldarg_0);
            EmitHelper.UnboxOrCast(il, method.DeclaringType);

            if (method.IsFinal)
                il.Emit(OpCodes.Call, method);
            else
                il.Emit(OpCodes.Callvirt, method);

            if (returnType.IsValueType)
                il.Emit(OpCodes.Box, returnType);

            il.Emit(OpCodes.Ret);

            return callable.CreateDelegate(typeof(Getter)) as Getter;
        }

        public static Getter CreateGetter(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field: return CreateGetter(member as FieldInfo);
                case MemberTypes.Property: return CreateGetter(member as PropertyInfo);
                case MemberTypes.Method: return CreateGetter(member as MethodInfo);
            }

            return null;
        }
        #endregion

        #region Setter
        private static Setter CreateSetter(FieldInfo field)
        {
            DynamicMethod callable = CreateDynamicSetterMethod();

            Type returnType = field.FieldType;
            ILGenerator il = callable.GetILGenerator();

            il.DeclareLocal(returnType);

            il.Emit(OpCodes.Ldarg_1);
            EmitHelper.UnboxOrCast(il, returnType);
            il.Emit(OpCodes.Stloc_0);

            il.Emit(OpCodes.Ldarg_0);
            EmitHelper.UnboxOrCast(il, field.DeclaringType);
            il.Emit(OpCodes.Ldloc_0);

            il.Emit(OpCodes.Stfld, field);
            il.Emit(OpCodes.Ret);

            return callable.CreateDelegate(typeof(Setter)) as Setter;
        }


        private static Setter CreateSetter(PropertyInfo property)
        {
            MethodInfo method = property.GetSetMethod();
            if (method == null)
                method = property.GetSetMethod(true);

            return CreateSetter(method);
        }

        private static Setter CreateSetter(MethodInfo method)
        {
            var dm = CreateDynamicSetterMethod();

            Type returnType = method.GetParameterTypes()[0];
            ILGenerator il = dm.GetILGenerator();
            il.DeclareLocal(returnType);

            il.Emit(OpCodes.Ldarg_1);
            EmitHelper.UnboxOrCast(il, returnType);
            il.Emit(OpCodes.Stloc_0);

            il.Emit(OpCodes.Ldarg_0);
            EmitHelper.UnboxOrCast(il, method.DeclaringType);
            il.Emit(OpCodes.Ldloc_0);

            if (method.IsFinal)
                il.Emit(OpCodes.Call, method);
            else
                il.Emit(OpCodes.Callvirt, method);

            il.Emit(OpCodes.Ret);

            return dm.CreateDelegate(typeof(Setter)) as Setter;
        }

        public static Setter CreateSetter(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field: return CreateSetter(member as FieldInfo);
                case MemberTypes.Property: return CreateSetter(member as PropertyInfo);
                case MemberTypes.Method: return CreateSetter(member as MethodInfo);
            }

            return null;
        }
        #endregion
    }

    class MethodMataData
    {
        public MethodBase Method { get; private set; }
        public Type ReturnType { get; private set; }
        public ParameterInfo[] Parameters { get; private set; }
        public Type[] ParameterTypes { get; private set; }

        public MethodMataData(ConstructorInfo ctor)
        {
            Method = ctor;
            ReturnType = ctor.ReflectedType;
            InitParameters();
        }

        public MethodMataData(MethodInfo method)
        {
            Method = method;
            ReturnType = method.ReturnType;
            InitParameters();
        }

        private void InitParameters()
        {
            Parameters = Method.GetParameters();
            ParameterTypes = Parameters.GetParameterTypes();
        }
    }
}
