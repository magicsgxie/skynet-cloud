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
	///   A collection that stores <see cref='Join'/> objects.
	/// </summary>
	[Serializable()]
	internal class JoinCollection : CollectionBase {
		
		/// <summary>
		///   Initializes a new instance of <see cref='JoinCollection'/>.
		/// </summary>
		internal JoinCollection()
		{
		}
		
		/// <summary>
		///   Initializes a new instance of <see cref='JoinCollection'/> based on another <see cref='JoinCollection'/>.
		/// </summary>
		/// <param name='val'>
		///   A <see cref='JoinCollection'/> from which the contents are copied
		/// </param>
		internal JoinCollection(JoinCollection val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		///   Initializes a new instance of <see cref='JoinCollection'/> containing any array of <see cref='Join'/> objects.
		/// </summary>
		/// <param name='val'>
		///       A array of <see cref='Join'/> objects with which to intialize the collection
		/// </param>
		internal JoinCollection(Join[] val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		///   Represents the entry at the specified index of the <see cref='Join'/>.
		/// </summary>
		/// <param name='index'>The zero-based index of the entry to locate in the collection.</param>
		/// <value>The entry at the specified index of the collection.</value>
		/// <exception cref='ArgumentOutOfRangeException'><paramref name='index'/> is outside the valid range of indexes for the collection.</exception>
		internal Join this[int index] {
			get {
				return ((Join)(List[index]));
			}
			set {
				List[index] = value;
			}
		}
		
		/// <summary>
		///   Adds a <see cref='Join'/> with the specified value to the 
		///   <see cref='JoinCollection'/>.
		/// </summary>
		/// <param name='val'>The <see cref='Join'/> to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <seealso cref='JoinCollection.AddRange'/>
		internal int Add(Join val)
		{
			return List.Add(val);
		}
		
		/// <summary>
		///   Copies the elements of an array to the end of the <see cref='JoinCollection'/>.
		/// </summary>
		/// <param name='val'>
		///    An array of type <see cref='Join'/> containing the objects to add to the collection.
		/// </param>
		/// <seealso cref='JoinCollection.Add'/>
		internal void AddRange(Join[] val)
		{
			for (int i = 0; i < val.Length; i++) {
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		///   Adds the contents of another <see cref='JoinCollection'/> to the end of the collection.
		/// </summary>
		/// <param name='val'>
		///    A <see cref='JoinCollection'/> containing the objects to add to the collection.
		/// </param>
		/// <seealso cref='JoinCollection.Add'/>
		internal void AddRange(JoinCollection val)
		{
			for (int i = 0; i < val.Count; i++)
			{
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		///   Gets a value indicating whether the 
		///    <see cref='JoinCollection'/> contains the specified <see cref='Join'/>.
		/// </summary>
		/// <param name='val'>The <see cref='Join'/> to locate.</param>
		/// <returns>
		/// <see langword='true'/> if the <see cref='Join'/> is contained in the collection; 
		///   otherwise, <see langword='false'/>.
		/// </returns>
		/// <seealso cref='JoinCollection.IndexOf'/>
		public bool Contains(Join val)
		{
			return List.Contains(val);
		}
		
		/// <summary>
		///   Copies the <see cref='JoinCollection'/> values to a one-dimensional <see cref='Array'/> instance at the 
		///    specified index.
		/// </summary>
		/// <param name='array'>The one-dimensional <see cref='Array'/> that is the destination of the values copied from <see cref='JoinCollection'/>.</param>
		/// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
		/// <exception cref='ArgumentException'>
		///   <para><paramref name='array'/> is multidimensional.</para>
		///   <para>-or-</para>
		///   <para>The number of elements in the <see cref='JoinCollection'/> is greater than
		///         the available space between <paramref name='arrayIndex'/> and the end of
		///         <paramref name='array'/>.</para>
		/// </exception>
		/// <exception cref='ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
		/// <exception cref='ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
		/// <seealso cref='Array'/>
		public void CopyTo(Join[] array, int index)
		{
			List.CopyTo(array, index);
		}
		
		/// <summary>
		///    Returns the index of a <see cref='Join'/> in 
		///       the <see cref='JoinCollection'/>.
		/// </summary>
		/// <param name='val'>The <see cref='Join'/> to locate.</param>
		/// <returns>
		///   The index of the <see cref='Join'/> of <paramref name='val'/> in the 
		///   <see cref='JoinCollection'/>, if found; otherwise, -1.
		/// </returns>
		/// <seealso cref='JoinCollection.Contains'/>
		public int IndexOf(Join val)
		{
			return List.IndexOf(val);
		}
		
		/// <summary>
		///   Inserts a <see cref='Join'/> into the <see cref='JoinCollection'/> at the specified index.
		/// </summary>
		/// <param name='index'>The zero-based index where <paramref name='val'/> should be inserted.</param>
		/// <param name='val'>The <see cref='Join'/> to insert.</param>
		/// <seealso cref='JoinCollection.Add'/>
		internal void Insert(int index, Join val)
		{
			List.Insert(index, val);
		}
		
		/// <summary>
		///  Returns an enumerator that can iterate through the <see cref='JoinCollection'/>.
		/// </summary>
		/// <seealso cref='IEnumerator'/>
		public new JoinEnumerator GetEnumerator()
		{
			return new JoinEnumerator(this);
		}
		
		/// <summary>
		///   Removes a specific <see cref='Join'/> from the <see cref='JoinCollection'/>.
		/// </summary>
		/// <param name='val'>The <see cref='Join'/> to remove from the <see cref='JoinCollection'/>.</param>
		/// <exception cref='ArgumentException'><paramref name='val'/> is not found in the Collection.</exception>
		internal void Remove(Join val)
		{
			List.Remove(val);
		}
		
		/// <summary>
		///   Enumerator that can iterate through a JoinCollection.
		/// </summary>
		/// <seealso cref='IEnumerator'/>
		/// <seealso cref='JoinCollection'/>
		/// <seealso cref='Join'/>
		public class JoinEnumerator : IEnumerator
		{
			IEnumerator baseEnumerator;
			IEnumerable temp;
			
			/// <summary>
			///   Initializes a new instance of <see cref='JoinEnumerator'/>.
			/// </summary>
			public JoinEnumerator(JoinCollection mappings)
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			
			/// <summary>
			///   Gets the current <see cref='Join'/> in the <seealso cref='JoinCollection'/>.
			/// </summary>
			public Join Current {
				get {
					return ((Join)(baseEnumerator.Current));
				}
			}
			
			object IEnumerator.Current {
				get {
					return baseEnumerator.Current;
				}
			}
			
			/// <summary>
			///   Advances the enumerator to the next <see cref='Join'/> of the <see cref='JoinCollection'/>.
			/// </summary>
			public bool MoveNext()
			{
				return baseEnumerator.MoveNext();
			}
			
			/// <summary>
			///   Sets the enumerator to its initial position, which is before the first element in the <see cref='JoinCollection'/>.
			/// </summary>
			public void Reset()
			{
				baseEnumerator.Reset();
			}
		}
	}
}
