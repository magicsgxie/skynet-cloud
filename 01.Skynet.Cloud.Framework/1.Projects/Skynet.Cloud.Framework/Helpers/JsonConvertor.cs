using Jil;
using System;

namespace UWay.Skynet.Cloud.Helpers
{
    /// <summary>
    /// json serialization and deserialization, using Jil.
    /// </summary>
    public class JsonConvertor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string Serialize(object source, Jil.Options options = null)
        {
            return JSON.Serialize(source, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string Serialize<T>(T source, Jil.Options options = null)
        {
            return JSON.Serialize(source, options);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string source, Jil.Options options = null)
        {
            return JSON.Deserialize<T>(source, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destinationType"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static object Deserialize(string source, Type destinationType, Jil.Options options = null)
        {
            return JSON.Deserialize(source, destinationType, options);                
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static dynamic Deserialize(string source, Jil.Options options = null)
        {
            return JSON.DeserializeDynamic(source, options);
        }
    }
}
