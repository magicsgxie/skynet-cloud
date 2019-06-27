using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Mapping.Internal;

namespace UWay.Skynet.Cloud.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ReflectionService
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static Assembly Assembly(this MemberInfo member)
        {
            Guard.NotNull(member, "member");
            Type type = member as Type;
            if (type != null)
            {
                return type.Assembly;
            }

            return member.DeclaringType.Assembly;
        }

        internal static bool TryGetGenericInterfaceType(Type instanceType, Type targetOpenInterfaceType, out Type targetClosedInterfaceType)
        {
            // The interface must be open
            Guard.IsTrue(targetOpenInterfaceType.IsInterface);
            Guard.IsTrue(targetOpenInterfaceType.IsGenericTypeDefinition);
            Guard.IsTrue(!instanceType.IsGenericTypeDefinition);

            // if instanceType is an interface, we must first check it directly
            if (instanceType.IsInterface &&
                instanceType.IsGenericType &&
                instanceType.GetGenericTypeDefinition() == targetOpenInterfaceType)
            {
                targetClosedInterfaceType = instanceType;
                return true;
            }

            try
            {
                // Purposefully not using FullName here because it results in a significantly
                //  more expensive component of GetInterface, this does mean that we're
                //  takign the chance that there aren't too many types which implement multiple
                //  interfaces by the same name...
                Type targetInterface = instanceType.GetInterface(targetOpenInterfaceType.Name, false);
                if (targetInterface != null &&
                    targetInterface.GetGenericTypeDefinition() == targetOpenInterfaceType)
                {
                    targetClosedInterfaceType = targetInterface;
                    return true;
                }
            }
            catch (AmbiguousMatchException)
            {
                // If there are multiple with the same name we should not pick any
            }

            targetClosedInterfaceType = null;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetAllProperties(this Type type)
        {
            return type.GetInterfaces().Concat(new Type[] { type }).SelectMany(itf => itf.GetProperties()).Distinct();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parentType"></param>
        /// <returns></returns>
        public static int GetInheriteDeep(this Type type, Type parentType)
        {
            Guard.NotNull(type, "type");
            Guard.NotNull(parentType, "parentType");
            var calc = new TypeDeepCalculator();
            var result = calc.Calculate(parentType, type);
            return result ? calc.Deep : -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static Type[] GetParameterTypes(this ParameterInfo[] ps)
        {
            Guard.NotNull(ps, "ps");
            return (from p in ps
                    select p.ParameterType).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Type[] GetParameterTypes(this MethodBase method)
        {
            Guard.NotNull(method, "method");
            return method.GetParameters().GetParameterTypes();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="impType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        internal static ConstructorInfo FindEligibleConstructor(this Type impType, IDictionary<string, object> args)
        {
            var result = (from c in impType.GetConstructors()
                          let ps = c.GetParameters()
                          where
                            ps.Length == args.Count
                            && ps.TrueForAll(p => args.ContainsKey(p.Name)
                            && (
                              args[p.Name].GetType() == p.ParameterType
                              || p.ParameterType.IsAssignableFrom(args[p.Name].GetType())
                              ))
                          select c).FirstOrDefault();

            if (result == null)
                throw new InvalidOperationException("Could not find eligible constructor for " + impType.FullName);


            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="impType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        internal static ConstructorInfo FindEligibleConstructor(this Type impType, object[] args)
        {
            var result = (from c in impType.GetConstructors()
                          let ps = c.GetParameters()
                          where ps.Length == args.Length
                          && ps.TrueForAll(
                            p => args[p.Position].GetType() == p.ParameterType
                            || p.ParameterType.IsAssignableFrom(args[p.Position].GetType()))
                          select c).FirstOrDefault();
            if (result == null)
                throw new InvalidOperationException("Could not find eligible constructor for " + impType.FullName);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lambdaExpression"></param>
        /// <returns></returns>
        public static MemberInfo FindMember(this LambdaExpression lambdaExpression)
        {
            Guard.NotNull(lambdaExpression, "lambdaExpression");
            Expression expressionToCheck = lambdaExpression;

            bool done = false;

            while (!done)
            {
                switch (expressionToCheck.NodeType)
                {
                    case ExpressionType.Convert:
                        expressionToCheck = ((UnaryExpression)expressionToCheck).Operand;
                        break;
                    case ExpressionType.Lambda:
                        expressionToCheck = ((LambdaExpression)expressionToCheck).Body;
                        break;
                    case ExpressionType.MemberAccess:
                        var memberExpression = ((MemberExpression)expressionToCheck);

                        if (memberExpression.Expression.NodeType != ExpressionType.Parameter &&
                            memberExpression.Expression.NodeType != ExpressionType.Convert)
                        {
                            throw new ArgumentException(string.Format("Expression '{0}' must resolve to top-level member.", lambdaExpression), "lambdaExpression");
                        }

                        MemberInfo member = memberExpression.Member;

                        return member;
                    default:
                        done = true;
                        break;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static Type GetMemberType(this MemberInfo member)
        {
            Guard.NotNull(member, "member");

            switch (member.MemberType)
            {
                case MemberTypes.Field: return (member as FieldInfo).FieldType;
                case MemberTypes.Property: return (member as PropertyInfo).PropertyType;
                case MemberTypes.Method: return (member as MethodInfo).ReturnType;
            }
            return null;
        }

        internal static Getter ToMemberGetter(this MemberInfo member)
        {
            Guard.NotNull(member, "member");

            switch (member.MemberType)
            {
                case MemberTypes.Field: return DynamicMethodFactory.GetGetter(member as FieldInfo);
                case MemberTypes.Property: return DynamicMethodFactory.GetGetter(member as PropertyInfo);
                case MemberTypes.Method: return DynamicMethodFactory.GetGetter(member as MethodInfo);
            }

            return null;
        }

        internal static Setter ToMemberSetter(this MemberInfo member)
        {
            Guard.NotNull(member, "member");

            switch (member.MemberType)
            {
                case MemberTypes.Field: return DynamicMethodFactory.GetSetter(member as FieldInfo);
                case MemberTypes.Property: return DynamicMethodFactory.GetSetter(member as PropertyInfo);
                case MemberTypes.Method: return DynamicMethodFactory.GetSetter(member as MethodInfo);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsOpenGenericType(this Type type)
        {
            Guard.NotNull(type, "type");
            return type.IsGenericType && type.ContainsGenericParameters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsCloseGenericType(this Type type)
        {
            Guard.NotNull(type, "type");
            return type.IsGenericType && !type.ContainsGenericParameters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool HasOpenGenericParameters(this MethodBase method)
        {
            return method.GetParameters().FirstOrDefault(p => p.ParameterType.IsOpenGenericType()) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="genericParameterName"></param>
        /// <returns></returns>
        public static Type GetNamedGenericParameter(this Type type, string genericParameterName)
        {
            Guard.NotNull(type, "type");
            if (type.IsOpenGenericType())
                return type.GetGenericArguments().FirstOrDefault(p => p.Name == genericParameterName);
            return type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="genericArgTypes"></param>
        /// <returns></returns>
        public static Type MakeCloseGenericType(this Type type, params Type[] genericArgTypes)
        {
            Guard.NotNull(type, "type");
            if (type.IsOpenGenericType())
            {
                var targetGenericArgTypes = type.GetGenericArguments();
                if (genericArgTypes == null || genericArgTypes.Length != targetGenericArgTypes.Length)
                    throw new ArgumentException("Argument not match!");

                for (int i = 0; i < targetGenericArgTypes.Length; i++)
                    targetGenericArgTypes[i] = genericArgTypes[targetGenericArgTypes[i].GenericParameterPosition];

                return type.MakeGenericType(targetGenericArgTypes);
            }
            else if (type.IsArray && type.GetElementType().IsGenericParameter)
            {
                int rank;
                if ((rank = type.GetArrayRank()) == 1)
                    return genericArgTypes[type.GetElementType().GenericParameterPosition]
                        .MakeArrayType();
                else
                    return genericArgTypes[type.GetElementType().GenericParameterPosition]
                        .MakeArrayType(rank);
            }

            return type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="procName"></param>
        /// <param name="args"></param>
        public static void Proc(this object obj, string procName, params object[] args)
        {
            Guard.NotNull(obj, "obj");
            Guard.NotNull(procName, "procName");

            ReflectionService.Func(obj, procName, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="funcName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object Func(this object obj, string funcName, params object[] args)
        {
            Guard.NotNull(obj, "obj");
            Guard.NotNull(funcName, "funcName");

            var method = obj.GetType().GetMethod(funcName, args.ConvertAll<object, Type>(item => item.GetType()).ToArray());
            if (method == null)
                method = obj.GetType().GetMethod(funcName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            if (method == null)
                throw new NotSupportedException(funcName + " does not exists.");

            return method.FastFuncInvoke(obj, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="funcName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T Func<T>(this object obj, string funcName, params object[] args)
        {
            Guard.NotNull(obj, "obj");
            Guard.NotNull(funcName, "funcName");
            return (T)Func(obj, funcName, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T GetProperty<T>(this object obj, string propertyName, params object[] index)
        {
            Guard.NotNull(obj, "obj");
            Guard.NotNull(propertyName, "propertyName");
            return (T)GetProperty(obj, propertyName, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static object GetProperty(this object obj, string propertyName, params object[] index)
        {
            Guard.NotNull(obj, "obj");
            Guard.NotNull(propertyName, "propertyName");

            object value = null;
            //如果是对象是Dictionary类型的，需要另作处理
            var map = obj as IDictionary<string, object>;
            if (map != null)
            {
                object tmpValue = null;
                map.TryGetValue(propertyName, out tmpValue);
                value = tmpValue;
            }
            else
            {
                //处理普通对象
                Property p = ParseProperty(obj, propertyName);

                if (p != null && p.Name != null && p.Name.CanRead)
                {
                    if (p.Owner.GetType().IsValueType || (index != null && index.Length > 0))
                        return p.Name.GetValue(p.Owner, index);
                    else
                        return p.Name.ToMemberGetter()(p.Owner);
                }
            }
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        public static void SetProperty(this object obj, string propertyName, object value, params object[] index)
        {
            Guard.NotNull(obj, "obj");
            Guard.NotNull(propertyName, "propertyName");

            Property p = ParseProperty(obj, propertyName);
            if (p == null || p.Name == null)
                return;

            if (!p.Name.CanWrite)
                return;

            if (value == null)
            {
                if (p.Name.PropertyType.IsValueType)
                    value = ObjectCreator.Create(p.Name.PropertyType);
            }
            try
            {
                if (!p.Name.PropertyType.IsAssignableFrom(value.GetType()))
                    value = Mapper.Map(value, value.GetType(), p.Name.PropertyType);
                if (p.Owner.GetType().IsValueType || (index != null && index.Length > 0))
                    p.Name.SetValue(p.Owner, value, index);
                else
                    p.Name.ToMemberSetter()(p.Owner, value);
            }
            finally { }
        }

        private static readonly BindingFlags propertyBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        private static Property ParseProperty(object root, string propertyName)
        {
            try
            {
                if (root == null)
                    return null;
                string[] items = propertyName.Split('.');
                if (items == null || items.Length < 1)
                    return null;
                //单个属性
                if (items.Length == 1)
                    return new Property(root.GetType().GetProperty(propertyName, propertyBindingFlags), root);
                //多个属性
                object target = root;

                string name = null;
                for (int i = 0; i < items.Length; i++)
                {
                    name = items[i];
                    //依据属性名称获取对象的属性
                    PropertyInfo p = target.GetType().GetProperty(name, propertyBindingFlags);
                    //取最后一个即属性的本身(过滤了该属性的基类)
                    if (i < items.Length - 1)
                    {
                        var oldTarget = target;
                        target = p.Get(target);//p.GetValue(target, null);
                        if (target == null)
                        {
                            if (!p.CanWrite)
                                return null;
                            target = ObjectCreator.Create(p.PropertyType);
                            p.Set(oldTarget, target);
                        }
                    }
                }
                return new Property(target.GetType().GetProperty(name, propertyBindingFlags), target);
            }
            catch
            {
                return null;
            }
        }

    }
}
