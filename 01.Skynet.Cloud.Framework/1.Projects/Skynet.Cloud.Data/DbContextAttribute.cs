using System;

namespace UWay.Skynet.Cloud.Data
{

    /// <summary>
    /// DbContext标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public class DbContextAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string DbConfigurationName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConfigurationName"></param>
        public DbContextAttribute(string dbConfigurationName)
        {
            Guard.NotNullOrEmpty(dbConfigurationName, "dbConfigurationName");
            DbConfigurationName = dbConfigurationName;
        }
    }
}
