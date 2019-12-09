using System.Reflection;

namespace UWay.Skynet.Cloud.Helpers
{
    ///// <summary>
    ///// 
    ///// </summary>
    //public class CacheKey
    //{
    //    private MethodInfo Method { get; }
    //    private ParameterInfo[] InputArguments { get; }
    //    private object[] ParameterValues { get; }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="method"></param>
    //    /// <param name="arguments"></param>
    //    /// <param name="values"></param>
    //    public CacheKey(MethodInfo method, ParameterInfo[] arguments, object[] values)
    //    {
    //        this.Method = method;
    //        this.InputArguments = arguments;
    //        this.ParameterValues = values;
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="obj"></param>
    //    /// <returns></returns>
    //    public override bool Equals(object obj)
    //    {
    //        CacheKey another = obj as CacheKey;
    //        if (null == another)
    //        {
    //            return false;
    //        }
    //        if (!this.Method.Equals(another.Method))
    //        {
    //            return false;
    //        }
    //        for (int index = 0; index < this.InputArguments.Length; index++)
    //        {
    //            var argument1 = this.InputArguments[index];
    //            var argument2 = another.InputArguments[index];
    //            if (argument1 == null && argument2 == null)
    //            {
    //                continue;
    //            }

    //            if (argument1 == null || argument2 == null)
    //            {
    //                return false;
    //            }

    //            if (!argument1.Equals(argument2))
    //            {
    //                return false;
    //            }
    //        }
    //        return true;
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns></returns>
    //    public string GetRedisCacheKey()
    //    {
    //        return
    //            $"{this.Method.DeclaringType.Namespace}:{this.Method.DeclaringType.Name}:{this.Method.Name}:{GetHashCode()}";
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns></returns>
    //    public string GetMemoryCacheKey()
    //    {
    //        return
    //            $"{this.Method.DeclaringType.Namespace}_{this.Method.DeclaringType.Name}_{this.Method.Name}_{GetHashCode()}";
    //    }


    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns></returns>
    //    public override int GetHashCode()
    //    {
    //        int hashCode = this.Method.GetHashCode();
    //        foreach (var argument in this.InputArguments)
    //        {
    //            hashCode = hashCode ^ argument.GetHashCode();
    //        }

    //        foreach (var value in ParameterValues)
    //        {
    //            hashCode = hashCode ^ value.GetHashCode();
    //        }
    //        return hashCode;
    //    }
    //}
}
