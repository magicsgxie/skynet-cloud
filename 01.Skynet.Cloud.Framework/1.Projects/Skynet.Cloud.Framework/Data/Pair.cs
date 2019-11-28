using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 存储两个int的对象
    /// </summary>
    public class Pair
    {
        /// <summary>
        /// 第一个int
        /// </summary>
        public int First { set; get; }
        
        /// <summary>
        /// 第二个int
        /// </summary>
        public int Second { set; get; }
    }

    /// <summary>
    /// 存储两个值的对象
    /// </summary>
    /// <typeparam name="F">第一个值的类型</typeparam>
    /// <typeparam name="S">第二个值的类型</typeparam>
    public class Pair<F, S>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Pair()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Pair(F f, S s)
        {
            First = f;
            Second = s;
        }

        /// <summary>
        /// 第一个值
        /// </summary>
        public F First
        {
            get;
            set;
        }

        /// <summary>
        /// 第二个值
        /// </summary>
        public S Second
        {
            get;
            set;
        }

        /// <summary>
        /// 将对象转化为对应的字符串显示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Convert.ToString(First);
        }
    }

    /// <summary>
    /// 存储三个值的类型
    /// </summary>
    /// <typeparam name="F">第一个值的类型</typeparam>
    /// <typeparam name="S">第二个值的类型</typeparam>
    /// <typeparam name="T">的三个值的类型</typeparam>
    public class Triplet<F, S, T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Triplet()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Triplet(F f, S s, T t)
        {
            First = f;
            Second = s;
            Third = t;
        }

        /// <summary>
        /// 第一个值
        /// </summary>
        public F First
        {
            get;
            set;
        }

        /// <summary>
        /// 第二个值
        /// </summary>
        public S Second
        {
            get;
            set;
        }

        /// <summary>
        /// 第三个值
        /// </summary>
        public T Third
        {
            get;
            set;
        }

        /// <summary>
        /// 将对象转化为对应的字符串显示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Convert.ToString(First);
        }
    }

    /// <summary>
    /// 存储两个对象列表
    /// </summary>
    /// <typeparam name="F">第一个对象</typeparam>
    /// <typeparam name="S">第二个对象</typeparam>
    public class Pairs<F, S> : IList<Pair<F, S>>
    {
        List<Pair<F, S>> pairs = new List<Pair<F, S>>();

        #region ICollection<Pair<F,S>> 成员

        public void Add(Pair<F, S> item)
        {
            pairs.Add(item);
        }

        public void Add(F f, S s)
        {
            pairs.Add(new Pair<F, S>(f, s));
        }

        public void Clear()
        {
            pairs.Clear();
        }

        public bool Contains(Pair<F, S> item)
        {
            return pairs.Contains(item);
        }

        public void CopyTo(Pair<F, S>[] array, int arrayIndex)
        {
            pairs.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return pairs.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<Pair<F, S>>)pairs).IsReadOnly; }
        }

        public bool Remove(Pair<F, S> item)
        {
            return pairs.Remove(item);
        }

        #endregion

        #region IEnumerable<Pair<F,S>> 成员

        public IEnumerator<Pair<F, S>> GetEnumerator()
        {
            return pairs.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)pairs).GetEnumerator();
        }

        #endregion

        #region IList<Pair<F,S>> 成员

        public int IndexOf(Pair<F, S> item)
        {
            return pairs.IndexOf(item);
        }

        public void Insert(int index, Pair<F, S> item)
        {
            pairs.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            pairs.RemoveAt(index);
        }

        public Pair<F, S> this[int index]
        {
            get
            {
                return pairs[index];
            }
            set
            {
                pairs[index] = value;
            }
        }

        public Pair<F, S> this[F f]
        {
            get { return pairs.FirstOrDefault(p => p.First.Equals(f)); }
        }

        #endregion
    }

    /// <summary>
    /// 三个对象列表
    /// </summary>
    /// <typeparam name="F">第一个对象</typeparam>
    /// <typeparam name="S">第二个对象</typeparam>
    /// <typeparam name="T">第三个对象</typeparam>
    public class Triplets<F, S, T> : IList<Triplet<F, S, T>>
    {
        List<Triplet<F, S, T>> triplets = new List<Triplet<F, S, T>>();

        #region IList<Triplet<F,S,T>> 成员
        /// <summary>
        /// 查找对象
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(Triplet<F, S, T> item)
        {
            return triplets.IndexOf(item);
        }

        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, Triplet<F, S, T> item)
        {
            triplets.Insert(index, item);
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="index">索引</param>
        public void RemoveAt(int index)
        {
            triplets.RemoveAt(index);
        }

        /// <summary>
        /// 对象索引
        /// </summary>
        /// <param name="index">索引值</param>
        /// <returns></returns>
        public Triplet<F, S, T> this[int index]
        {
            get
            {
                return triplets[index];
            }
            set
            {
                triplets[index] = value;
            }
        }

        #endregion

        #region ICollection<Triplet<F,S,T>> 成员
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="item"></param>
        public void Add(Triplet<F, S, T> item)
        {
            triplets.Add(item);
        }

        /// <summary>
        /// 清除所有对象
        /// </summary>
        public void Clear()
        {
            triplets.Clear();
        }

        /// <summary>
        /// 是否包含对象
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(Triplet<F, S, T> item)
        {
            return triplets.Contains(item);
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="array">对象数组</param>
        /// <param name="arrayIndex">复制索引</param>
        public void CopyTo(Triplet<F, S, T>[] array, int arrayIndex)
        {
            triplets.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count
        {
            get { return triplets.Count; }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return ((ICollection<Triplet<F, S, T>>)triplets).IsReadOnly; }
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(Triplet<F, S, T> item)
        {
            return triplets.Remove(item);
        }

        #endregion

        #region IEnumerable<Triplet<F,S,T>> 成员

        public IEnumerator<Triplet<F, S, T>> GetEnumerator()
        {
            return triplets.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)triplets).GetEnumerator();
        }

        #endregion

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="f">第一个对象</param>
        /// <param name="s">第二个对象</param>
        /// <param name="t">第三个对象</param>
        public void Add(F f, S s, T t)
        {
            triplets.Add(new Triplet<F, S, T>(f, s, t));
        }
    }
}
