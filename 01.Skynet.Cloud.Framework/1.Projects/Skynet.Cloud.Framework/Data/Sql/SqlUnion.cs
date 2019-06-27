/*----------------------------------------------------------------
// Copyright (C) 2010 深圳市优网科技有限公司
// 版权所有。 
//
// 文件名：
// 文件功能描述：
//
// 
// 创建标识：
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections;

namespace UWay.Skynet.Cloud.Data
{

	/// <summary>
	/// Encapsulates SQL UNION statement
    /// </summary>
    [Serializable]
	public class SqlUnion
	{
		ArrayList items = new ArrayList(5);

		/// <summary>
		/// Creates a new SqlUnion
		/// </summary>
		public SqlUnion()
		{
		}

		/// <summary>
		/// Adds a query to the UNION clause
		/// </summary>
		/// <param name="query">SelectQuery to be added</param>
		/// <remarks>Query will be added with DistinctModifier.Distinct </remarks>
		public void Add(SelectQuery query)
		{
			Add(query, DistinctModifier.Distinct);
		}

		/// <summary>
		/// Adds a query to the UNION clause with the specified DistinctModifier
		/// </summary>
		/// <param name="query">SelectQuery to be added</param>
		/// <param name="repeatingAction">Distinct modifier</param>
		public void Add(SelectQuery query, DistinctModifier repeatingAction)
		{
			items.Add(new SqlUnionItem(query, repeatingAction));
		}

		internal IList Items
		{
			get { return items; }
		}
	}
}
