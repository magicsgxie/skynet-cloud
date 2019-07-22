/************************************************************************************
* Copyright (c) 2019-07-11 12:08:19 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisUserResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：655e446a-025e-4a49-b194-8540e2968cd5
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:19 
* 描述：人员处理结果表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:19 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/



namespace   UWay.Skynet.Cloud.Uflow.Repository
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using  UWay.Skynet.Cloud.Uflow.Entity;
   using System.Linq;
   using System.Collections.Generic;

   /// <summary>
   /// 人员处理结果表仓储类
   /// </summary>
   public class HisUserResultRepository:ObjectRepository
   {
      public HisUserResultRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加人员处理结果表{人员处理结果表}对象(即:一条记录
      /// </summary>
      public long Add(HisUserResult  hisUserResult)
      {
         return Add<HisUserResult>(hisUserResult);
      }
      /// <summary>
      /// 批量添加人员处理结果表{人员处理结果表}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisUserResult>  hisUserResults)
      {
         Batch<long, HisUserResult>(hisUserResults, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新人员处理结果表{人员处理结果表}对象(即:一条记录
      /// </summary>
      public int Update(HisUserResult  hisUserResult)
      {
         return Update<HisUserResult>(hisUserResult);
      }
      /// <summary>
      /// 删除人员处理结果表{人员处理结果表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisUserResult>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的人员处理结果表{人员处理结果表}对象(即:一条记录
      /// </summary>
      public HisUserResult GetById(string id)
      {
         return GetByID<HisUserResult>(id);
      }
      /// <summary>
      /// 获取所有的人员处理结果表{人员处理结果表}对象
      /// </summary>
      public IQueryable<HisUserResult> Query()
      {
         return CreateQuery<HisUserResult>();
      }
   }
}
