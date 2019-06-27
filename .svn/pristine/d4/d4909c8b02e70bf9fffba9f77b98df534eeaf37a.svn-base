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

namespace UWay.Skynet.Cloud.Data
{
	/// <summary>
	/// A collection of elements of type CaseTerm
    /// </summary>
    [Serializable]
	public class CaseTermCollection: System.Collections.CollectionBase
	{
		/// <summary>
		/// Initializes a new empty instance of the CaseTermCollection class.
		/// </summary>
		public CaseTermCollection()
		{
			// empty
		}

		/// <summary>
		/// Initializes a new instance of the CaseTermCollection class, containing elements
		/// copied from an array.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the new CaseTermCollection.
		/// </param>
		public CaseTermCollection(CaseTerm[] items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Initializes a new instance of the CaseTermCollection class, containing elements
		/// copied from another instance of CaseTermCollection
		/// </summary>
		/// <param name="items">
		/// The CaseTermCollection whose elements are to be added to the new CaseTermCollection.
		/// </param>
		public CaseTermCollection(CaseTermCollection items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Adds the elements of an array to the end of this CaseTermCollection.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the end of this CaseTermCollection.
		/// </param>
		public virtual void AddRange(CaseTerm[] items)
		{
			foreach (CaseTerm item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds the elements of another CaseTermCollection to the end of this CaseTermCollection.
		/// </summary>
		/// <param name="items">
		/// The CaseTermCollection whose elements are to be added to the end of this CaseTermCollection.
		/// </param>
		public virtual void AddRange(CaseTermCollection items)
		{
			foreach (CaseTerm item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds an instance of type CaseTerm to the end of this CaseTermCollection.
		/// </summary>
		/// <param name="value">
		/// The CaseTerm to be added to the end of this CaseTermCollection.
		/// </param>
		public virtual void Add(CaseTerm value)
		{
			this.List.Add(value);
		}

		/// <summary>
		/// Determines whether a specfic CaseTerm value is in this CaseTermCollection.
		/// </summary>
		/// <param name="value">
		/// The CaseTerm value to locate in this CaseTermCollection.
		/// </param>
		/// <returns>
		/// true if value is found in this CaseTermCollection;
		/// false otherwise.
		/// </returns>
		public virtual bool Contains(CaseTerm value)
		{
			return this.List.Contains(value);
		}

		/// <summary>
		/// Return the zero-based index of the first occurrence of a specific value
		/// in this CaseTermCollection
		/// </summary>
		/// <param name="value">
		/// The CaseTerm value to locate in the CaseTermCollection.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of the _ELEMENT value if found;
		/// -1 otherwise.
		/// </returns>
		public virtual int IndexOf(CaseTerm value)
		{
			return this.List.IndexOf(value);
		}

		/// <summary>
		/// Inserts an element into the CaseTermCollection at the specified index
		/// </summary>
		/// <param name="index">
		/// The index at which the CaseTerm is to be inserted.
		/// </param>
		/// <param name="value">
		/// The CaseTerm to insert.
		/// </param>
		public virtual void Insert(int index, CaseTerm value)
		{
			this.List.Insert(index, value);
		}

		/// <summary>
		/// Gets or sets the CaseTerm at the given index in this CaseTermCollection.
		/// </summary>
		public virtual CaseTerm this[int index]
		{
			get
			{
				return (CaseTerm) this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		/// <summary>
		/// Removes the first occurrence of a specific CaseTerm from this CaseTermCollection.
		/// </summary>
		/// <param name="value">
		/// The CaseTerm value to remove from this CaseTermCollection.
		/// </param>
		public virtual void Remove(CaseTerm value)
		{
			this.List.Remove(value);
		}

		/// <summary>
		/// Type-specific enumeration class, used by CaseTermCollection.GetEnumerator.
		/// </summary>
		public class Enumerator: System.Collections.IEnumerator
		{
			private System.Collections.IEnumerator wrapped;

			/// <summary>
			/// 
			/// </summary>
			/// <param name="collection"></param>
			public Enumerator(CaseTermCollection collection)
			{
				this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
			}

			/// <summary>
			/// 
			/// </summary>
			public CaseTerm Current
			{
				get
				{
					return (CaseTerm) (this.wrapped.Current);
				}
			}

			/// <summary>
			/// 
			/// </summary>
			object System.Collections.IEnumerator.Current
			{
				get
				{
					return (CaseTerm) (this.wrapped.Current);
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
		/// Returns an enumerator that can iterate through the elements of this CaseTermCollection.
		/// </summary>
		/// <returns>
		/// An object that implements System.Collections.IEnumerator.
		/// </returns>        
		public new virtual CaseTermCollection.Enumerator GetEnumerator()
		{
			return new CaseTermCollection.Enumerator(this);
		}
	}

}
