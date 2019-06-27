using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    static class EmitHelper
    {
        private static readonly MethodInfo _typeGetTypeFromHandleMethod = typeof(Type).GetMethod("GetTypeFromHandle");
        private static readonly MethodInfo ExceptionGetData = typeof(Exception).GetProperty("Data").GetGetMethod();
        private static readonly MethodInfo DictionaryAdd = typeof(IDictionary).GetMethod("Add");
        private static readonly ConstructorInfo ObjectCtor = typeof(object).GetConstructor(Type.EmptyTypes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeBuilder"></param>
        /// <param name="ctrArgumentTypes"></param>
        /// <returns></returns>
        public static ILGenerator CreateGeneratorForPublicConstructor(this TypeBuilder typeBuilder, Type[] ctrArgumentTypes)
        {
            ConstructorBuilder ctorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                ctrArgumentTypes);

            ILGenerator ctorIL = ctorBuilder.GetILGenerator();
            ctorIL.Emit(OpCodes.Ldarg_0);
            ctorIL.Emit(OpCodes.Call, ObjectCtor);

            return ctorIL;
        }


        public static void GetExceptionDataAndStoreInLocal(this ILGenerator ilGenerator, LocalBuilder exception, LocalBuilder dataStore)
        {
            ilGenerator.Emit(OpCodes.Ldloc, exception);
            ilGenerator.Emit(OpCodes.Callvirt, ExceptionGetData);
            ilGenerator.Emit(OpCodes.Stloc, dataStore);
        }


        public static void AddItemToLocalDictionary(this ILGenerator ilGenerator, LocalBuilder dictionary, object key, object value)
        {
            ilGenerator.Emit(OpCodes.Ldloc, dictionary);
            ilGenerator.LoadValue(key);
            ilGenerator.LoadValue(value);
            ilGenerator.Emit(OpCodes.Callvirt, DictionaryAdd);
        }


        public static void AddLocalToLocalDictionary(this ILGenerator ilGenerator, LocalBuilder dictionary, object key, LocalBuilder value)
        {
            ilGenerator.Emit(OpCodes.Ldloc, dictionary);
            ilGenerator.LoadValue(key);
            ilGenerator.Emit(OpCodes.Ldloc, value);
            ilGenerator.Emit(OpCodes.Callvirt, DictionaryAdd);
        }


        //public static Type[] GetActualParameterTypes(ParameterInfo[] parameters)
        //{
        //    Type[] types = new Type[parameters.Length];

        //    for (int index = 0; index < parameters.Length; index++)
        //    {
        //        Type binderType = parameters[index].ParameterType;
        //        types[index] = (binderType.IsByRef ? binderType.GetElementType() : binderType);
        //    }

        //    return types;
        //}


        public static ILGenerator UnboxOrCast(this ILGenerator il, Type type)
        {
            if (type.IsValueType)
                il.Emit(OpCodes.Unbox_Any, type);
            else
                il.Emit(OpCodes.Castclass, type);

            return il;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ILGenerator LoadInt(this ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    break;
                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    break;
                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    break;
                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    break;
                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    break;
                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    break;
                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    break;
                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    break;
                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    break;
                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    break;
                default:
                    if (value > -129 && value < 128)
                        il.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
                    else
                        il.Emit(OpCodes.Ldc_I4, value);
                    break;
            }

            return il;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="il"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ILGenerator LoadArgument(this ILGenerator il, int index)
        {
            switch (index)
            {
                case 0:
                    il.Emit(OpCodes.Ldarg_0);
                    break;
                case 1:
                    il.Emit(OpCodes.Ldarg_1);
                    break;
                case 2:
                    il.Emit(OpCodes.Ldarg_2);
                    break;
                case 3:
                    il.Emit(OpCodes.Ldarg_3);
                    break;
                default:
                    if (index > -129 && index < 128)
                        il.Emit(OpCodes.Ldarg_S, (sbyte)index);
                    else
                        il.Emit(OpCodes.Ldarg, index);
                    break;
            }

            return il;
        }


        public static void LoadValue(this ILGenerator ilGenerator, object value)
        {
            if (value == null)
            {
                ilGenerator.LoadNull();
                return;
            }

            //
            // Prepare for literal loading - decide whether we should box, and handle enums properly
            //
            Type valueType = value.GetType();
            object rawValue = value;
            if (valueType.IsEnum)
            {
                // enums are special - we need to load the underlying constant on the stack
                rawValue = Convert.ChangeType(value, Enum.GetUnderlyingType(valueType), null);
                valueType = rawValue.GetType();
            }

            //
            // Generate IL depending on the valueType - this is messier than it should ever be, but sadly necessary
            //
            if (valueType == Types.String)
            {
                // we need to check for strings before enumerables, because strings are IEnumerable<char>
                ilGenerator.LoadString((string)rawValue);
            }
            else if (Types.Type.IsAssignableFrom(valueType))
            {
                ilGenerator.LoadTypeOf((Type)rawValue);
            }
            else if (Types.IEnumerable.IsAssignableFrom(valueType))
            {
                // NOTE : strings and dictionaries are also enumerables, but we have already handled those
                ilGenerator.LoadEnumerable((IEnumerable)rawValue);
            }
            else if (
                (valueType == Types.Char) ||
                (valueType == Types.Boolean) ||
                (valueType == Types.Byte) ||
                (valueType == Types.SByte) ||
                (valueType == Types.Int16) ||
                (valueType == Types.UInt16) ||
                (valueType == Types.Int32)
                )
            {
                // NOTE : Everything that is 32 bit or less uses ldc.i4. We need to pass int32, even if the actual types is shorter - this is IL memory model
                // direct casting to (int) won't work, because the value is boxed, thus we need to use Convert.
                // Sadly, this will not work for all cases - namely large uint32 - because they can't semantically fit into 32 signed bits
                // We have a special case for that next
                ilGenerator.LoadInt((int)Convert.ChangeType(rawValue, typeof(int), CultureInfo.InvariantCulture));
            }
            else if (valueType == Types.UInt32)
            {
                // NOTE : This one is a bit tricky. Ldc.I4 takes an Int32 as an argument, although it really treats it as a 32bit number
                // That said, some UInt32 values are larger that Int32.MaxValue, so the Convert call above will fail, which is why 
                // we need to treat this case individually and cast to uint, and then - unchecked - to int.
                ilGenerator.LoadInt(unchecked((int)((uint)rawValue)));
            }
            else if (valueType == Types.Int64)
            {
                ilGenerator.LoadLong((long)rawValue);
            }
            else if (valueType == Types.UInt64)
            {
                // NOTE : This one is a bit tricky. Ldc.I8 takes an Int64 as an argument, although it really treats it as a 64bit number
                // That said, some UInt64 values are larger that Int64.MaxValue, so the direct case we use above (or Convert, for that matter)will fail, which is why
                // we need to treat this case individually and cast to ulong, and then - unchecked - to long.
                ilGenerator.LoadLong(unchecked((long)((ulong)rawValue)));
            }
            else if (valueType == Types.Single)
            {
                ilGenerator.LoadFloat((float)rawValue);
            }
            else if (valueType == Types.Double)
            {
                ilGenerator.LoadDouble((double)rawValue);
            }
            else
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, "InvalidMetadataValue", value.GetType().FullName));
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ilGenerator"></param>
        /// <param name="enumerable"></param>
        public static void LoadEnumerable(this ILGenerator ilGenerator, IEnumerable enumerable)
        {
            // We load enumerable as an array - this is the most compact and efficient way of representing it
            Type elementType = null;
            Type closedType = null;
            if (ReflectionService.TryGetGenericInterfaceType(enumerable.GetType(), Types.IEnumerableofT, out closedType))
                elementType = closedType.GetGenericArguments()[0];
            else
                elementType = Types.Object;

            //
            // elem[] array = new elem[<enumerable.Count()>]
            //
            Type generatedArrayType = elementType.MakeArrayType();
            LocalBuilder generatedArrayLocal = ilGenerator.DeclareLocal(generatedArrayType);

            ilGenerator.LoadInt(enumerable.Cast<object>().Count());
            ilGenerator.Emit(OpCodes.Newarr, elementType);
            ilGenerator.Emit(OpCodes.Stloc, generatedArrayLocal);

            int index = 0;
            foreach (var value in enumerable)
            {
                //
                //array[<index>] = value;
                //
                ilGenerator.Emit(OpCodes.Ldloc, generatedArrayLocal);
                ilGenerator.LoadInt(index);
                ilGenerator.LoadValue(value);
                //if (EmitHelper.IsBoxingRequiredForValue(value) && !elementType.IsValueType)
                //{
                //    ilGenerator.Emit(OpCodes.Box, value.GetType());
                //}
                if (value != null)
                    ilGenerator.UnboxOrCast(value.GetType());

                ilGenerator.Emit(OpCodes.Stelem, elementType);
                index++;
            }

            ilGenerator.Emit(OpCodes.Ldloc, generatedArrayLocal);
        }

        //private static bool IsBoxingRequiredForValue(object value)
        //{
        //    if (value == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return value.GetType().IsValueType;
        //    }
        //}


        private static void LoadNull(this ILGenerator ilGenerator)
        {
            ilGenerator.Emit(OpCodes.Ldnull);
        }

        private static void LoadString(this ILGenerator ilGenerator, string s)
        {
            if (s == null)
            {
                ilGenerator.LoadNull();
            }
            else
            {
                ilGenerator.Emit(OpCodes.Ldstr, s);
            }
        }


        //private static void LoadInt(this ILGenerator ilGenerator, int value)
        //{
        //    Assumes.NotNull(ilGenerator);
        //    ilGenerator.Emit(OpCodes.Ldc_I4, value);
        //}

        private static void LoadLong(this ILGenerator ilGenerator, long value)
        {
            ilGenerator.Emit(OpCodes.Ldc_I8, value);
        }

        private static void LoadFloat(this ILGenerator ilGenerator, float value)
        {
            ilGenerator.Emit(OpCodes.Ldc_R4, value);
        }

        private static void LoadDouble(this ILGenerator ilGenerator, double value)
        {
            ilGenerator.Emit(OpCodes.Ldc_R8, value);
        }



        public static void LoadTypeOf(this ILGenerator ilGenerator, Type type)
        {
            ilGenerator.Emit(OpCodes.Ldtoken, type);
            ilGenerator.EmitCall(OpCodes.Call, EmitHelper._typeGetTypeFromHandleMethod, null);
        }


        public static ILGenerator LoadLocalVariable(this ILGenerator il, int localIndex)
        {
            switch (localIndex)
            {
                case 0:
                    il.Emit(OpCodes.Ldloc_0);//将索引 0 处的局部变量加载到计算堆栈上
                    break;
                case 1:
                    il.Emit(OpCodes.Ldloc_1);//将索引 1 处的局部变量加载到计算堆栈上
                    break;
                case 2:
                    il.Emit(OpCodes.Ldloc_2);//将索引 2 处的局部变量加载到计算堆栈上
                    break;
                case 3:
                    il.Emit(OpCodes.Ldloc_3);//将索引 3 处的局部变量加载到计算堆栈上
                    break;
                default:
                    if (localIndex <= 0xff)
                        il.Emit(OpCodes.Ldloc_S, localIndex);//将特定索引处的局部变量加载到计算堆栈上（短格式）
                    else
                        il.Emit(OpCodes.Ldloc, localIndex);//将指定索引处的局部变量加载到计算堆栈上
                    break;
            }
            return il;
        }


        public static ILGenerator StoreLocalVariable(this ILGenerator il, int localIndex)
        {

            switch (localIndex)
            {
                case 0:
                    il.Emit(OpCodes.Stloc_0);//从计算堆栈的顶部弹出当前值并将其存储到索引 0 处的局部变量列表中
                    break;
                case 1:
                    il.Emit(OpCodes.Stloc_1);//从计算堆栈的顶部弹出当前值并将其存储到索引 1 处的局部变量列表中
                    break;
                case 2:
                    il.Emit(OpCodes.Stloc_2);//从计算堆栈的顶部弹出当前值并将其存储到索引 2 处的局部变量列表中
                    break;
                case 3:
                    il.Emit(OpCodes.Stloc_3);//从计算堆栈的顶部弹出当前值并将其存储到索引 3 处的局部变量列表中
                    break;
                default:
                    if (localIndex <= 0xff)
                        il.Emit(OpCodes.Stloc_S, localIndex);//从计算堆栈的顶部弹出当前值并将其存储在局部变量列表中的 localIndex 处（短格式）
                    else
                        il.Emit(OpCodes.Stloc, localIndex);//从计算堆栈的顶部弹出当前值并将其存储到指定索引处的局部变量列表中
                    break;
            }

            return il;
        }

        private static OpCode GetLdindOpCode(TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return OpCodes.Ldind_I1;//将 int8 类型的值作为 int32 间接加载到计算堆栈上

                case TypeCode.Char:
                    return OpCodes.Ldind_I2;//将 int16 类型的值作为 int32 间接加载到计算堆栈上

                case TypeCode.SByte:
                    return OpCodes.Ldind_I1;//将 int8 类型的值作为 int32 间接加载到计算堆栈上

                case TypeCode.Byte:
                    return OpCodes.Ldind_U1;//将 unsigned int8 类型的值作为 int32 间接加载到计算堆栈上

                case TypeCode.Int16:
                    return OpCodes.Ldind_I2;//将 int16 类型的值作为 int32 间接加载到计算堆栈上

                case TypeCode.UInt16:
                    return OpCodes.Ldind_U2;//将 unsigned int16 类型的值作为 int32 间接加载到计算堆栈上

                case TypeCode.Int32:
                    return OpCodes.Ldind_I4;//将 int32 类型的值作为 int32 间接加载到计算堆栈上

                case TypeCode.UInt32:
                    return OpCodes.Ldind_U4;//将 unsigned int32 类型的值作为 int32 间接加载到计算堆栈上

                case TypeCode.Int64:
                    return OpCodes.Ldind_I8;//将 int8 类型的值作为 int32 间接加载到计算堆栈上

                case TypeCode.UInt64:
                    return OpCodes.Ldind_I8;//将 unsigned int64 类型的值作为 int64 间接加载到计算堆栈上

                case TypeCode.Single:
                    return OpCodes.Ldind_R4;//将 float32 类型的值作为 F (float) 类型间接加载到计算堆栈上

                case TypeCode.Double:
                    return OpCodes.Ldind_R8;//将 float64 类型的值作为 F (float) 类型间接加载到计算堆栈上

                case TypeCode.String:
                    return OpCodes.Ldind_Ref;//将对象引用作为 O（对象引用）类型间接加载到计算堆栈上
            }
            return OpCodes.Nop;
        }

        private static OpCode GetLdelemOpCode(TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Object:
                case TypeCode.String:
                case TypeCode.DBNull:
                    return OpCodes.Ldelem_Ref;
                case TypeCode.Boolean:
                case TypeCode.SByte:
                    return OpCodes.Ldelem_I1;
                case TypeCode.Char:
                case TypeCode.Int16:
                    return OpCodes.Ldelem_I2;
                case TypeCode.Byte:
                    return OpCodes.Ldelem_U1;
                case TypeCode.Int32:
                    return OpCodes.Ldelem_I4;
                case TypeCode.Int64:
                    return OpCodes.Ldelem_I8;
                case TypeCode.UInt16:
                    return OpCodes.Ldelem_U2;
                case TypeCode.UInt32:
                    return OpCodes.Ldelem_U4;
                case TypeCode.UInt64:
                    return OpCodes.Ldelem_I8;
                case TypeCode.Single:
                    return OpCodes.Ldelem_R4;
                case TypeCode.Double:
                    return OpCodes.Ldelem_R8;

            }
            return OpCodes.Nop;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public static OpCode GetStelemOpCode(TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Object:
                case TypeCode.DBNull:
                case TypeCode.String:
                    return OpCodes.Stelem_Ref;//用计算堆栈上的对象 ref 值（O 类型）替换给定索引处的数组元素
                case TypeCode.Boolean:
                case TypeCode.SByte:
                case TypeCode.Byte:
                    return OpCodes.Stelem_I1;//用计算堆栈上的 int8 值替换给定索引处的数组元素
                case TypeCode.Char:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                    return OpCodes.Stelem_I2;//用计算堆栈上的 int16 值替换给定索引处的数组元素
                case TypeCode.Int32:
                case TypeCode.UInt32:
                    return OpCodes.Stelem_I4;//用计算堆栈上的 int32 值替换给定索引处的数组元素
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return OpCodes.Stelem_I8;//用计算堆栈上的 int64 值替换给定索引处的数组元素
                case TypeCode.Single:
                    return OpCodes.Stelem_R4;//用计算堆栈上的 float32 值替换给定索引处的数组元素
                case TypeCode.Double:
                    return OpCodes.Stelem_R8;//用计算堆栈上的 float64 值替换给定索引处的数组元素

            }
            return OpCodes.Nop;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public static OpCode GetConvOpCode(TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return OpCodes.Conv_I1;

                case TypeCode.Char:
                    return OpCodes.Conv_I2;

                case TypeCode.SByte:
                    return OpCodes.Conv_I1;

                case TypeCode.Byte:
                    return OpCodes.Conv_U1;

                case TypeCode.Int16:
                    return OpCodes.Conv_I2;

                case TypeCode.UInt16:
                    return OpCodes.Conv_U2;

                case TypeCode.Int32:
                    return OpCodes.Conv_I4;

                case TypeCode.UInt32:
                    return OpCodes.Conv_U4;

                case TypeCode.Int64:
                    return OpCodes.Conv_I8;

                case TypeCode.UInt64:
                    return OpCodes.Conv_I8;

                case TypeCode.Single:
                    return OpCodes.Conv_R4;

                case TypeCode.Double:
                    return OpCodes.Conv_R8;
            }
            return OpCodes.Nop;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="il"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILGenerator Call(this ILGenerator il, MethodInfo method)
        {
            il.Emit(OpCodes.Call, method);
            return il;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="il"></param>
        /// <param name="ctor"></param>
        /// <returns></returns>
        public static ILGenerator Call(this ILGenerator il, ConstructorInfo ctor)
        {
            il.Emit(OpCodes.Call, ctor);
            return il;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="il"></param>
        /// <returns></returns>
        public static ILGenerator Return(this ILGenerator il)
        {
            il.Emit(OpCodes.Ret);
            return il;
        }

    }
}
