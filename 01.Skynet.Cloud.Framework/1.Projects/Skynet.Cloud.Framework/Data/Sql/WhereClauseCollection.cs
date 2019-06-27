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
	///   A collection that stores <see cref='WhereClause'/> objects.
	/// </summary>
	[Serializable()]
	public class WhereClauseCollection : CollectionBase {
		
		/// <summary>
		///   Initializes a new instance of <see cref='WhereClauseCollection'/>.
		/// </summary>
		public WhereClauseCollection()
		{
		}
		
		/// <summary>
		///   Initializes a new instance of <see cref='WhereClauseCollection'/> based on another <see cref='WhereClauseCollection'/>.
		/// </summary>
		/// <param name='val'>
		///   A <see cref='WhereClauseCollection'/> from which the contents are copied
		/// </param>
		public WhereClauseCollection(WhereClauseCollection val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		///   Initializes a new instance of <see cref='WhereClauseCollection'/> containing any array of <see cref='WhereClause'/> objects.
		/// </summary>
		/// <param name='val'>
		///       A array of <see cref='WhereClause'/> objects with which to intialize the collection
		/// </param>
		public WhereClauseCollection(WhereClause[] val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		///   Represents the entry at the specified index of the <see cref='WhereClause'/>.
		/// </summary>
		/// <param name='index'>The zero-based index of the entry to locate in the collection.</param>
		/// <value>The entry at the specified index of the collection.</value>
		/// <exception cref='ArgumentOutOfRangeException'><paramref name='index'/> is outside the valid range of indexes for the collection.</exception>
		public WhereClause this[int index] {
			get {
				return ((WhereClause)(List[index]));
			}
			set {
				List[index] = value;
			}
		}
		
		/// <summary>
		///   Adds a <see cref='WhereClause'/> with the specified value to the 
		///   <see cref='WhereClauseCollection'/>.
		/// </summary>
		/// <param name='val'>The <see cref='WhereClause'/> to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <seealso cref='WhereClauseCollection.AddRange'/>
		public int Add(WhereClause val)
		{
			return List.Add(val);
		}
		
		/// <summary>
		///   Copies the elements of an array to the end of the <see cref='WhereClauseCollection'/>.
		/// </summary>
		/// <param name='val'>
		///    An array of type <see cref='WhereClause'/> containing the objects to add to the collection.
		/// </param>
		/// <seealso cref='WhereClauseCollection.Add'/>
		public void AddRange(WhereClause[] val)
		{
			for (int i = 0; i < val.Length; i++) {
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		///   Adds the contents of another <see cref='WhereClauseCollection'/> to the end of the collection.
		/// </summary>
		/// <param name='val'>
		///    A <see cref='WhereClauseCollection'/> containing the objects to add to the collection.
		/// </param>
		/// <seealso cref='WhereClauseCollection.Add'/>
		public void AddRange(WhereClauseCollection val)
		{
			for (int i = 0; i < val.Count; i++)
			{
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		///   Gets a value indicating whether the 
		///    <see cref='WhereClauseCollection'/> contains the specified <see cref='WhereClause'/>.
		/// </summary>
		/// <param name='val'>The <see cref='WhereClause'/> to locate.</param>
		/// <returns>
		/// <see langword='true'/> if the <see cref='WhereClause'/> is contained in the collection; 
		///   otherwise, <see langword='false'/>.
		/// </returns>
		/// <seealso cref='WhereClauseCollection.IndexOf'/>
		public bool Contains(WhereClause val)
		{
			return List.Contains(val);
		}
		
		/// <summary>
		///   Copies the <see cref='WhereClauseCollection'/> values to a one-dimensional <see cref='Array'/> instance at the 
		///    specified index.
		/// </summary>
		/// <param name='array'>The one-dimensional <see cref='Array'/> that is the destination of the values copied from <see cref='WhereClauseCollection'/>.</param>
		/// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
		/// <exception cref='ArgumentException'>
		///   <para><paramref name='array'/> is multidimensional.</para>
		///   <para>-or-</para>
		///   <para>The number of elements in the <see cref='WhereClauseCollection'/> is greater than
		///         the available space between <paramref name='arrayIndex'/> and the end of
		///         <paramref name='array'/>.</para>
		/// </exception>
		/// <exception cref='ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
		/// <exception cref='ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
		/// <seealso cref='Array'/>
		public void CopyTo(WhereClause[] array, int index)
		{
			List.CopyTo(array, index);
		}
		
		/// <summary>
		///    Returns the index of a <see cref='WhereClause'/> in 
		///       the <see cref='WhereClauseCollection'/>.
		/// </summary>
		/// <param name='val'>The <see cref='WhereClause'/> to locate.</param>
		/// <returns>
		///   The index of the <see cref='WhereClause'/> of <paramref name='val'/> in the 
		///   <see cref='WhereClauseCollection'/>, if found; otherwise, -1.
		/// </returns>
		/// <seealso cref='WhereClauseCollection.Contains'/>
		public int IndexOf(WhereClause val)
		{
			return List.IndexOf(val);
		}
		
		/// <summary>
		///   Inserts a <see cref='WhereClause'/> into the <see cref='WhereClauseCollection'/> at the specified index.
		/// </summary>
		/// <param name='index'>The zero-based index where <paramref name='val'/> should be inserted.</param>
		/// <param name='val'>The <see cref='WhereClause'/> to insert.</param>
		/// <seealso cref='WhereClauseCollection.Add'/>
		public void Insert(int index, WhereClause val)
		{
			List.Insert(index, val);
		}
		
		/// <summary>
		///  Returns an enumerator that can iterate through the <see cref='WhereClauseCollection'/>.
		/// </summary>
		/// <seealso cref='IEnumerator'/>
		public new WhereClauseGroupEnumerator GetEnumerator()
		{
			return new WhereClauseGroupEnumerator(this);
		}
		
		/// <summary>
		///   Removes a specific <see cref='WhereClause'/> from the <see cref='WhereClauseCollection'/>.
		/// </summary>
		/// <param name='val'>The <see cref='WhereClause'/> to remove from the <see cref='WhereClauseCollection'/>.</param>
		/// <exception cref='ArgumentException'><paramref name='val'/> is not found in the Collection.</exception>
		public void Remove(WhereClause val)
		{
			List.Remove(val);
		}
		
		/// <summary>
		///   Enumerator that can iterate through a WhereClauseGroupCollection.
		/// </summary>
		/// <seealso cref='IEnumerator'/>
		/// <seealso cref='WhereClauseCollection'/>
		/// <seealso cref='WhereClause'/>
		public class WhereClauseGroupEnumerator : IEnumerator
		{
			IEnumerator baseEnumerator;
			IEnumerable temp;
			
			/// <summary>
			///   Initializes a new instance of <see cref='WhereClauseGroupEnumerator'/>.
			/// </summary>
			public WhereClauseGroupEnumerator(WhereClauseCollection mappings)
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			
			/// <summary>
			///   Gets the current <see cref='WhereClause'/> in the <seealso cref='WhereClauseCollection'/>.
			/// </summary>
			public WhereClause Current {
				get {
					return ((WhereClause)(baseEnumerator.Current));
				}
			}
			
			object IEnumerator.Current {
				get {
					return baseEnumerator.Current;
				}
			}
			
			/// <summary>
			///   Advances the enumerator to the next <see cref='WhereClause'/> of the <see cref='WhereClauseCollection'/>.
			/// </summary>
			public bool MoveNext()
			{
				return baseEnumerator.MoveNext();
			}
			
			/// <summary>
			///   Sets the enumerator to its initial position, which is before the first element in the <see cref='WhereClauseCollection'/>.
			/// </summary>
			public void Reset()
			{
				baseEnumerator.Reset();
			}
		}
	}
}
