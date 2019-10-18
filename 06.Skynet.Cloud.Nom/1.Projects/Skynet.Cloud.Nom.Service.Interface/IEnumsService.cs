using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Skynet.Cloud.Request;

namespace UWay.Skynet.Cloud.Nom.Service.Interface
{
    public interface IEnumsService
    {
        /// <summary>
        /// 分页获取表结构枚举
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        DataSourceResult Page(DataSourceRequest request);

        /// <summary>
        /// 获取枚举表结构
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<EnumValue> Query(string fieldName);

        EnumType GetByFieldName(string fieldName);

        /// <summary>
        /// 添加表枚举类型
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        long AddEnumType(EnumType enumType);

        int UpdateEnumType(EnumType enumType);

        long AddEnumValue(EnumValue enumType);

        int UpdateEnumValue(EnumValue enumType);


        int DeleteEnumValue(long[] id);

        void DeleteEnumType(long id);
    }
}
