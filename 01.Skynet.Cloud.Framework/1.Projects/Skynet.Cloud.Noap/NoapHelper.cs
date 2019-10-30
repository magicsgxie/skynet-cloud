
using System;

namespace Skynet.Cloud.Noap
{
    public static class NoapHelper
    {
        public static string ToContainerName(this NetType netType, DataBaseType dataBaseType = DataBaseType.Normal)
        {
            return string.Format("{0}_{1}", (int)netType, (int)dataBaseType);
        }

        /// <summary>
        /// 获取指标对应的网络类型
        /// </summary>
        /// <param name="neType">性能中的网络类型</param>
        /// <returns></returns>
        public static NetType GetPerfNetType(this int neType)
        {
            if (neType > 1000)
                neType = neType / 1000;
            int result = 1;
            switch (neType)
            {
                case 2:
                    result = 4;
                    break;
                case 3:
                    result = 2;
                    break;
                case 4:
                    result = 3;
                    break;
                default:
                    result = neType;
                    break;
            }

            return (NetType)result;
        }

        public static string GetBusinessType(this int neType, int businessType)
        {
            string busyType = string.Empty;

            if (neType == 1)
            {
                switch (businessType)
                {
                    case 1:
                        busyType = "1X";
                        break;
                    case 2:
                        busyType = "DO";
                        break;
                }
            }
            else if (neType == 2)
            {
                switch (businessType)
                {
                    case 1:
                        busyType = "FDD";
                        break;
                    case 2:
                        busyType = "TDD";
                        break;
                    case 3:
                        busyType = "FDD,TDD";
                        break;
                    case 4:
                        busyType = "NB-IOT";
                        break;
                    case 5:
                        busyType = "VOLTE";
                        break;
                }
            }

            return busyType;
        }

       




        /// <summary>
        /// 获取网元表名称
        /// </summary>
        /// <param name="netType"></param>
        /// <param name="neLevel"></param>
        /// <returns></returns>
        public static string GetNeTableSubffix(this NetType netType, NeLevel neLevel, bool isTaizhang = false)
        {
            var nePrefix = "CELL";
            switch (neLevel)
            {
                case NeLevel.Bsc:
                    nePrefix = "BSC";
                    break;
                case NeLevel.Bts:
                    nePrefix = "BTS";
                    if (netType == NetType.LTE)
                    {
                        nePrefix = "ENB";
                    }
                    break;
                case NeLevel.Carr:
                    nePrefix = "CARRIER";
                    break;
                case NeLevel.OMC:
                    nePrefix = "OMC";
                    if (netType == NetType.LTE)
                    {
                        nePrefix = "MME";
                    }
                    break;
            }

            if(isTaizhang == true)
            {
                nePrefix = "TAIZHANG_" + nePrefix;
            }

            switch (netType)
            {
                case NetType.LTE:
                    return string.Format("{0}_{1}_{2}", "NE", nePrefix, "L");
                case NetType.GSM:
                    return string.Format("{0}_{1}_{2}", "NE", nePrefix, "G");
                case NetType.WCDMA:
                    return string.Format("{0}_{1}_{2}", "NE", nePrefix, "W");
                case NetType.FIVEG:
                    return string.Format("{0}_{1}_{2}", "NE", nePrefix, "5G");
                default:
                    if(isTaizhang)
                        return string.Format("{0}_{1}", "NE", nePrefix);
                    else 
                        return string.Format("{0}_{1}_{2}", "NE", nePrefix, "C");
            }
        }

        /// <summary>
        /// 获取从网络制式转化成指标中的数据类型
        /// </summary>
        /// <param name="netType"></param>
        /// <returns></returns>
        public static int GetNeType(this NetType netType)
        {
            switch (netType)
            {
                case NetType.LTE:
                    return 2;
                case NetType.GSM:
                    return 3;
                case NetType.WCDMA:
                    return 4;
                default:
                    return (int)netType;
            }
        }
    }
}
