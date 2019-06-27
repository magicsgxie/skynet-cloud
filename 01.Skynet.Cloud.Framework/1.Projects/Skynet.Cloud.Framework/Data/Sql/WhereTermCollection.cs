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
	///   A collection that stores <see cref='WhereTerm'/> objects.
	/// </summary>
	[Serializable()]
	public class WhereTermCollection : CollectionBase {
		
		/// <summary>
		///   Initializes a new instance of <see cref='WhereTermCollection'/>.
		/// </summary>
		public WhereTermCollection()
		{
		}
		
		/// <summary>
		///   Initializes a new instance of <see cref='WhereTermCollection'/> based on another <see cref='WhereTermCollection'/>.
		/// </summary>
		/// <param name='val'>
		///   A <see cref='WhereTermCollection'/> from which the contents are copied
		/// </param>
		public WhereTermCollection(WhereTermCollection val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		///   Initializes a new instance of <see cref='WhereTermCollection'/> containing any array of <see cref='WhereTerm'/> objects.
		/// </summary>
		/// <param name='val'>
		///       A array of <see cref='WhereTerm'/> objects with which to intialize the collection
		/// </param>
		public WhereTermCollection(WhereTerm[] val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		///   Represents the entry at the specified index of the <see cref='WhereTerm'/>.
		/// </summary>
		/// <param name='index'>The zero-based index of the entry to locate in the collection.</param>
		/// <value>The entry at the specified index of the collection.</value>
		/// <exception cref='ArgumentOutOfRangeException'><paramref name='index'/> is outside the valid range of indexes for the collection.</exception>
		public WhereTerm this[int index] {
			get {
				return ((WhereTerm)(List[index]));
			}
			set {
				List[index] = value;
			}
		}
		
		/// <summary>
		///   Adds a <see cref='WhereTerm'/> with the specified value to the 
		///   <see cref='WhereTermCollection'/>.
		/// </summary>
		/// <param name='val'>The <see cref='WhereTerm'/> to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <seealso cref='WhereTermCollection.AddRange'/>
		public int Add(WhereTerm val)
		{
			return List.Add(val);
		}
		
		/// <summary>
		///   Copies the elements of an array to the end of the <see cref='WhereTermCollection'/>.
		/// </summary>
		/// <param name='val'>
		///    An array of type <see cref='WhereTerm'/> containing the objects to add to the collection.
		/// </param>
		/// <seealso cref='WhereTermCollection.Add'/>
		public void AddRange(WhereTerm[] val)
		{
			for (int i = 0; i < val.Length; i++) {
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		///   Adds the contents of another <see cref='WhereTermCollection'/> to the end of the collection.
		/// </summary>
		/// <param name='val'>
		///    A <see cref='WhereTermCollection'/> containing the objects to add to the collection.
		/// </param>
		/// <seealso cref='WhereTermCollection.Add'/>
		public void AddRange(WhereTermCollection val)
		{
			for (int i = 0; i < val.Count; i++)
			{
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		///   Gets a value indicating whether the 
		///    <see cref='WhereTermCollection'/> contains the specified <see cref='WhereTerm'/>.
		/// </summary>
		/// <param name='val'>The <see cref='WhereTerm'/> to locate.</param>
		/// <returns>
		/// <see langword='true'/> if the <see cref='WhereTerm'/> is contained in the collection; 
		///   otherwise, <see langword='false'/>.
		/// </returns>
		/// <seealso cref='WhereTermCollection.IndexOf'/>
		public bool Contains(WhereTerm val)
		{
			return List.Contains(val);
		}
		
		/// <summary>
		///   Copies the <see cref='WhereTermCollection'/> values to a one-dimensional <see cref='Array'/> instance at the 
		///    specified index.
		/// </summary>
		/// <param name='array'>The one-dimensional <see cref='Array'/> that is the destination of the values copied from <see cref='WhereTermCollection'/>.</param>
		/// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
		/// <exception cref='ArgumentException'>
		///   <para><paramref name='array'/> is multidimensional.</para>
		///   <para>-or-</para>
		///   <para>The number of elements in the <see cref='WhereTermCollection'/> is greater than
		///         the available space between <paramref name='arrayIndex'/> and the end of
		///         <paramref name='array'/>.</para>
		/// </exception>
		/// <exception cref='ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
		/// <exception cref='ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
		/// <seealso cref='Array'/>
		public void CopyTo(WhereTerm[] array, int index)
		{
			List.CopyTo(array, index);
		}
		
		/// <summary>
		///    Returns the index of a <see cref='WhereTerm'/> in 
		///       the <see cref='WhereTermCollection'/>.
		/// </summary>
		/// <param name='val'>The <see cref='WhereTerm'/> to locate.</param>
		/// <returns>
		///   The index of the <see cref='WhereTerm'/> of <paramref name='val'/> in the 
		///   <see cref='WhereTermCollection'/>, if found; otherwise, -1.
		/// </returns>
		/// <seealso cref='WhereTermCollection.Contains'/>
		public int IndexOf(WhereTerm val)
		{
			return List.IndexOf(val);
		}
		
		/// <summary>
		///   Inserts a <see cref='WhereTerm'/> into the <see cref='WhereTermCollection'/> at the specified index.
		/// </summary>
		/// <param name='index'>The zero-based index where <paramref name='val'/> should be inserted.</param>
		/// <param name='val'>The <see cref='WhereTerm'/> to insert.</param>
		/// <seealso cref='WhereTermCollection.Add'/>
		public void Insert(int index, WhereTerm val)
		{
			List.Insert(index, val);
		}
		
		/// <summary>
		///  Returns an enumerator that can iterate through the <see cref='WhereTermCollection'/>.
		/// </summary>
		/// <seealso cref='IEnumerator'/>
		public new WhereClauseEnumerator GetEnumerator()
		{
			return new WhereClauseEnumerator(this);
		}
		
		/// <summary>
		///   Removes a specific <see cref='WhereTerm'/> from the <see cref='WhereTermCollection'/>.
		/// </summary>
		/// <param name='val'>The <see cref='WhereTerm'/> to remove from the <see cref='WhereTermCollection'/>.</param>
		/// <exception cref='ArgumentException'><paramref name='val'/> is not found in the Collection.</exception>
		public void Remove(WhereTerm val)
		{
			List.Remove(val);
		}
		
		/// <summary>
		///   Enumerator that can iterate through a WhereClauseCollection.
		/// </summary>
		/// <seealso cref='IEnumerator'/>
		/// <seealso cref='WhereTermCollection'/>
		/// <seealso cref='WhereTerm'/>
		public class WhereClauseEnumerator : IEnumerator
		{
			IEnumerator baseEnumerator;
			IEnumerable temp;
			
			/// <summary>
			///   Initializes a new instance of <see cref='WhereClauseEnumerator'/>.
			/// </summary>
			public WhereClauseEnumerator(WhereTermCollection mappings)
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			
			/// <summary>
			///   Gets the current <see cref='WhereTerm'/> in the <seealso cref='WhereTermCollection'/>.
			/// </summary>
			public WhereTerm Current {
				get {
					return ((WhereTerm)(baseEnumerator.Current));
				}
			}
			
			object IEnumerator.Current {
				get {
					return baseEnumerator.Current;
				}
			}
			
			/// <summary>
			///   Advances the enumerator to the next <see cref='WhereTerm'/> of the <see cref='WhereTermCollection'/>.
			/// </summary>
			public bool MoveNext()
			{
				return baseEnumerator.MoveNext();
			}
			
			/// <summary>
			///   Sets the enumerator to its initial position, which is before the first element in the <see cref='WhereTermCollection'/>.
			/// </summary>
			public void Reset()
			{
				baseEnumerator.Reset();
			}
		}
	}
}
