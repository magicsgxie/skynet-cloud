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
	/// A collection of elements of type UpdateTerm
    /// </summary>
    [Serializable]
	public class UpdateTermCollection: System.Collections.CollectionBase
	{
		/// <summary>
		/// Initializes a new empty instance of the UpdateTermCollection class.
		/// </summary>
		public UpdateTermCollection()
		{
			// empty
		}

		/// <summary>
		/// Initializes a new instance of the UpdateTermCollection class, containing elements
		/// copied from an array.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the new UpdateTermCollection.
		/// </param>
		public UpdateTermCollection(UpdateTerm[] items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Initializes a new instance of the UpdateTermCollection class, containing elements
		/// copied from another instance of UpdateTermCollection
		/// </summary>
		/// <param name="items">
		/// The UpdateTermCollection whose elements are to be added to the new UpdateTermCollection.
		/// </param>
		public UpdateTermCollection(UpdateTermCollection items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Adds the elements of an array to the end of this UpdateTermCollection.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the end of this UpdateTermCollection.
		/// </param>
		public virtual void AddRange(UpdateTerm[] items)
		{
			foreach (UpdateTerm item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds the elements of another UpdateTermCollection to the end of this UpdateTermCollection.
		/// </summary>
		/// <param name="items">
		/// The UpdateTermCollection whose elements are to be added to the end of this UpdateTermCollection.
		/// </param>
		public virtual void AddRange(UpdateTermCollection items)
		{
			foreach (UpdateTerm item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds an instance of type UpdateTerm to the end of this UpdateTermCollection.
		/// </summary>
		/// <param name="value">
		/// The UpdateTerm to be added to the end of this UpdateTermCollection.
		/// </param>
		public virtual void Add(UpdateTerm value)
		{
			this.List.Add(value);
		}

		/// <summary>
		/// Determines whether a specfic UpdateTerm value is in this UpdateTermCollection.
		/// </summary>
		/// <param name="value">
		/// The UpdateTerm value to locate in this UpdateTermCollection.
		/// </param>
		/// <returns>
		/// true if value is found in this UpdateTermCollection;
		/// false otherwise.
		/// </returns>
		public virtual bool Contains(UpdateTerm value)
		{
			return this.List.Contains(value);
		}

		/// <summary>
		/// Return the zero-based index of the first occurrence of a specific value
		/// in this UpdateTermCollection
		/// </summary>
		/// <param name="value">
		/// The UpdateTerm value to locate in the UpdateTermCollection.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of the _ELEMENT value if found;
		/// -1 otherwise.
		/// </returns>
		public virtual int IndexOf(UpdateTerm value)
		{
			return this.List.IndexOf(value);
		}

		/// <summary>
		/// Inserts an element into the UpdateTermCollection at the specified index
		/// </summary>
		/// <param name="index">
		/// The index at which the UpdateTerm is to be inserted.
		/// </param>
		/// <param name="value">
		/// The UpdateTerm to insert.
		/// </param>
		public virtual void Insert(int index, UpdateTerm value)
		{
			this.List.Insert(index, value);
		}

		/// <summary>
		/// Gets or sets the UpdateTerm at the given index in this UpdateTermCollection.
		/// </summary>
		public virtual UpdateTerm this[int index]
		{
			get
			{
				return (UpdateTerm) this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		/// <summary>
		/// Removes the first occurrence of a specific UpdateTerm from this UpdateTermCollection.
		/// </summary>
		/// <param name="value">
		/// The UpdateTerm value to remove from this UpdateTermCollection.
		/// </param>
		public virtual void Remove(UpdateTerm value)
		{
			this.List.Remove(value);
		}

		/// <summary>
		/// Type-specific enumeration class, used by UpdateTermCollection.GetEnumerator.
		/// </summary>
		public class Enumerator: System.Collections.IEnumerator
		{
			private System.Collections.IEnumerator wrapped;

			/// <summary></summary>
			public Enumerator(UpdateTermCollection collection)
			{
				this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
			}

			/// <summary></summary>
			public UpdateTerm Current
			{
				get
				{
					return (UpdateTerm) (this.wrapped.Current);
				}
			}

			object System.Collections.IEnumerator.Current
			{
				get
				{
					return (UpdateTerm) (this.wrapped.Current);
				}
			}

			/// <summary></summary>
			public bool MoveNext()
			{
				return this.wrapped.MoveNext();
			}

			/// <summary></summary>
			public void Reset()
			{
				this.wrapped.Reset();
			}
		}

		/// <summary>
		/// Returns an enumerator that can iterate through the elements of this UpdateTermCollection.
		/// </summary>
		/// <returns>
		/// An object that implements System.Collections.IEnumerator.
		/// </returns>        
		public new virtual UpdateTermCollection.Enumerator GetEnumerator()
		{
			return new UpdateTermCollection.Enumerator(this);
		}
	}
}
