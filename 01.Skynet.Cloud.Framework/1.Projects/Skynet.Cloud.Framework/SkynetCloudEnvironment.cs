using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Extensions;

namespace UWay.Skynet.Cloud
{
    public class SkynetCloudEnvironment
    {
        internal static string PropertiesFile;

        /// <summary>
        /// 得到当前程序的物理路径
        /// </summary>
        public static string ApplicationPhysicalPath { get; private set; }

        static SkynetCloudEnvironment()
        {
            ApplicationPhysicalPath = AppDomain.CurrentDomain.BaseDirectory;
            PropertiesFile = @".Skynet.CloudProperties.xml";
        }


    }

    sealed class HashtableDataCollection : Hashtable, IDataCollection { }
}
