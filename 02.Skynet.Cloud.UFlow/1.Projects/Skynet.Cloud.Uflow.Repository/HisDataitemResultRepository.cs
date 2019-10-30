/************************************************************************************
* Copyright (c) 2019-07-11 12:08:07 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisDataitemResult.cs
* 版本号：  V1.0.0.0
* 唯一标识：83f94515-fd1b-419f-a992-d83c12647c3d
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:07 
* 描述：数据项结果表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:07 
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
   /// 数据项结果表仓储类
   /// </summary>
   public class HisDataitemResultRepository:ObjectRepository
   {
      public HisDataitemResultRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public long Add(HisDataitemResult  hisDataitemResult)
      {
         return Add<HisDataitemResult>(hisDataitemResult);
      }
      /// <summary>
      /// 批量添加数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisDataitemResult>  hisDataitemResults)
      {
         Batch<long, HisDataitemResult>(hisDataitemResults, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public int Update(HisDataitemResult  hisDataitemResult)
      {
         return Update<HisDataitemResult>(hisDataitemResult);
      }
      /// <summary>
      /// 删除数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisDataitemResult>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的数据项结果表{数据项结果表}对象(即:一条记录
      /// </summary>
      public HisDataitemResult GetById(string id)
      {
         return GetByID<HisDataitemResult>(id);
      }
      /// <summary>
      /// 获取所有的数据项结果表{数据项结果表}对象
      /// </summary>
      public IQueryable<HisDataitemResult> Query()
      {
         return CreateQuery<HisDataitemResult>();
      }
   }
}
