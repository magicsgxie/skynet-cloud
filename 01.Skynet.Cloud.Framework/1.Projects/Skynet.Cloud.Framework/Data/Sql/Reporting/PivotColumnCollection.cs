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

namespace UWay.Skynet.Cloud.Data.Reporting
{
	/// <summary>
	///透视表列集合
	/// </summary>
	public class PivotColumnCollection: System.Collections.CollectionBase
	{
		/// <summary>
        /// 新建一个透视表列集合
		/// </summary>
		public PivotColumnCollection()
		{
			// empty
		}

		/// <summary>
        /// 从array集合中新建一个透视表列集合
		/// </summary>
		/// <param name="items">
        /// array集合
		/// </param>
		public PivotColumnCollection(PivotColumn[] items)
		{
			this.AddRange(items);
		}

		/// <summary>
        /// 从透视表列集合中新建一个透视表列集合
		/// </summary>
		/// <param name="items">
        /// 透视表列集合
		/// </param>
		public PivotColumnCollection(PivotColumnCollection items)
		{
			this.AddRange(items);
		}

		/// <summary>
        /// 从数组集合中新建一个透视表列集合
		/// </summary>
		/// <param name="items">
        /// 数组集合
		/// </param>
		public virtual void AddRange(PivotColumn[] items)
		{
			foreach (PivotColumn item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
        /// 添加一个集合到透视表列集合
		/// </summary>
		/// <param name="items">
		/// The PivotColumnCollection whose elements are to be added to the end of this PivotColumnCollection.
		/// </param>
		public virtual void AddRange(PivotColumnCollection items)
		{
			foreach (PivotColumn item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds an instance of type PivotColumn to the end of this PivotColumnCollection.
		/// </summary>
		/// <param name="value">
		/// The PivotColumn to be added to the end of this PivotColumnCollection.
		/// </param>
		public virtual void Add(PivotColumn value)
		{
			this.List.Add(value);
		}

		/// <summary>
		/// Determines whether a specfic PivotColumn value is in this PivotColumnCollection.
		/// </summary>
		/// <param name="value">
		/// The PivotColumn value to locate in this PivotColumnCollection.
		/// </param>
		/// <returns>
		/// true if value is found in this PivotColumnCollection;
		/// false otherwise.
		/// </returns>
		public virtual bool Contains(PivotColumn value)
		{
			return this.List.Contains(value);
		}

		/// <summary>
		/// Return the zero-based index of the first occurrence of a specific value
		/// in this PivotColumnCollection
		/// </summary>
		/// <param name="value">
		/// The PivotColumn value to locate in the PivotColumnCollection.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of the _ELEMENT value if found;
		/// -1 otherwise.
		/// </returns>
		public virtual int IndexOf(PivotColumn value)
		{
			return this.List.IndexOf(value);
		}

		/// <summary>
		/// Inserts an element into the PivotColumnCollection at the specified index
		/// </summary>
		/// <param name="index">
		/// The index at which the PivotColumn is to be inserted.
		/// </param>
		/// <param name="value">
		/// The PivotColumn to insert.
		/// </param>
		public virtual void Insert(int index, PivotColumn value)
		{
			this.List.Insert(index, value);
		}

		/// <summary>
		/// Gets or sets the PivotColumn at the given index in this PivotColumnCollection.
		/// </summary>
		public virtual PivotColumn this[int index]
		{
			get
			{
				return (PivotColumn) this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		/// <summary>
		/// Removes the first occurrence of a specific PivotColumn from this PivotColumnCollection.
		/// </summary>
		/// <param name="value">
		/// The PivotColumn value to remove from this PivotColumnCollection.
		/// </param>
		public virtual void Remove(PivotColumn value)
		{
			this.List.Remove(value);
		}

		/// <summary>
		/// Type-specific enumeration class, used by PivotColumnCollection.GetEnumerator.
		/// </summary>
		public class Enumerator: System.Collections.IEnumerator
		{
			private System.Collections.IEnumerator wrapped;

			/// <summary>
			/// 
			/// </summary>
			/// <param name="collection"></param>
			public Enumerator(PivotColumnCollection collection)
			{
				this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
			}

			/// <summary>
			/// 
			/// </summary>
			public PivotColumn Current
			{
				get
				{
					return (PivotColumn) (this.wrapped.Current);
				}
			}

			object System.Collections.IEnumerator.Current
			{
				get
				{
					return (PivotColumn) (this.wrapped.Current);
				}
			}

			/// <summary>
			/// 
			/// </summary>
			/// <returns></returns>
			public bool MoveNext()
			{
				return this.wrapped.MoveNext();
			}

			/// <summary>
			/// 
			/// </summary>
			public void Reset()
			{
				this.wrapped.Reset();
			}
		}

		/// <summary>
		/// Returns an enumerator that can iterate through the elements of this PivotColumnCollection.
		/// </summary>
		/// <returns>
		/// An object that implements System.Collections.IEnumerator.
		/// </returns>        
		public new virtual PivotColumnCollection.Enumerator GetEnumerator()
		{
			return new PivotColumnCollection.Enumerator(this);
		}
	}
}
