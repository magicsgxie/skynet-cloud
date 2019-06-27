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
	/// A collection of elements of type PivotColumnValue
	/// </summary>
	public class PivotColumnValueCollection: System.Collections.CollectionBase
	{
		/// <summary>
		/// Initializes a new empty instance of the PivotColumnValueCollection class.
		/// </summary>
		public PivotColumnValueCollection()
		{
			// empty
		}

		/// <summary>
		/// Initializes a new instance of the PivotColumnValueCollection class, containing elements
		/// copied from an array.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the new PivotColumnValueCollection.
		/// </param>
		public PivotColumnValueCollection(PivotColumnValue[] items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Initializes a new instance of the PivotColumnValueCollection class, containing elements
		/// copied from another instance of PivotColumnValueCollection
		/// </summary>
		/// <param name="items">
		/// The PivotColumnValueCollection whose elements are to be added to the new PivotColumnValueCollection.
		/// </param>
		public PivotColumnValueCollection(PivotColumnValueCollection items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Adds the elements of an array to the end of this PivotColumnValueCollection.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the end of this PivotColumnValueCollection.
		/// </param>
		public virtual void AddRange(PivotColumnValue[] items)
		{
			foreach (PivotColumnValue item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds the elements of another PivotColumnValueCollection to the end of this PivotColumnValueCollection.
		/// </summary>
		/// <param name="items">
		/// The PivotColumnValueCollection whose elements are to be added to the end of this PivotColumnValueCollection.
		/// </param>
		public virtual void AddRange(PivotColumnValueCollection items)
		{
			foreach (PivotColumnValue item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds an instance of type PivotColumnValue to the end of this PivotColumnValueCollection.
		/// </summary>
		/// <param name="value">
		/// The PivotColumnValue to be added to the end of this PivotColumnValueCollection.
		/// </param>
		public virtual void Add(PivotColumnValue value)
		{
			this.List.Add(value);
		}

		/// <summary>
		/// Determines whether a specfic PivotColumnValue value is in this PivotColumnValueCollection.
		/// </summary>
		/// <param name="value">
		/// The PivotColumnValue value to locate in this PivotColumnValueCollection.
		/// </param>
		/// <returns>
		/// true if value is found in this PivotColumnValueCollection;
		/// false otherwise.
		/// </returns>
		public virtual bool Contains(PivotColumnValue value)
		{
			return this.List.Contains(value);
		}

		/// <summary>
		/// Return the zero-based index of the first occurrence of a specific value
		/// in this PivotColumnValueCollection
		/// </summary>
		/// <param name="value">
		/// The PivotColumnValue value to locate in the PivotColumnValueCollection.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of the _ELEMENT value if found;
		/// -1 otherwise.
		/// </returns>
		public virtual int IndexOf(PivotColumnValue value)
		{
			return this.List.IndexOf(value);
		}

		/// <summary>
		/// Inserts an element into the PivotColumnValueCollection at the specified index
		/// </summary>
		/// <param name="index">
		/// The index at which the PivotColumnValue is to be inserted.
		/// </param>
		/// <param name="value">
		/// The PivotColumnValue to insert.
		/// </param>
		public virtual void Insert(int index, PivotColumnValue value)
		{
			this.List.Insert(index, value);
		}

		/// <summary>
		/// Gets or sets the PivotColumnValue at the given index in this PivotColumnValueCollection.
		/// </summary>
		public virtual PivotColumnValue this[int index]
		{
			get
			{
				return (PivotColumnValue) this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		/// <summary>
		/// Removes the first occurrence of a specific PivotColumnValue from this PivotColumnValueCollection.
		/// </summary>
		/// <param name="value">
		/// The PivotColumnValue value to remove from this PivotColumnValueCollection.
		/// </param>
		public virtual void Remove(PivotColumnValue value)
		{
			this.List.Remove(value);
		}

		/// <summary>
		/// Type-specific enumeration class, used by PivotColumnValueCollection.GetEnumerator.
		/// </summary>
		public class Enumerator: System.Collections.IEnumerator
		{
			private System.Collections.IEnumerator wrapped;

			/// <summary>
			/// 
			/// </summary>
			/// <param name="collection"></param>
			public Enumerator(PivotColumnValueCollection collection)
			{
				this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
			}

			/// <summary>
			/// 
			/// </summary>
			public PivotColumnValue Current
			{
				get
				{
					return (PivotColumnValue) (this.wrapped.Current);
				}
			}

			object System.Collections.IEnumerator.Current
			{
				get
				{
					return (PivotColumnValue) (this.wrapped.Current);
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
		/// Returns an enumerator that can iterate through the elements of this PivotColumnValueCollection.
		/// </summary>
		/// <returns>
		/// An object that implements System.Collections.IEnumerator.
		/// </returns>        
		public new virtual PivotColumnValueCollection.Enumerator GetEnumerator()
		{
			return new PivotColumnValueCollection.Enumerator(this);
		}
	}
}
