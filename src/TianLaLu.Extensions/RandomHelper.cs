using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System
{
    /// <summary>
    /// 一个用于使用Random类的快捷方式。 还提供了一些其他用于生成随机数据的有用的方法。
    /// </summary>
    public static class RandomHelper
    {
        private static readonly Random Rnd = new Random();
        
        #region -- GetRandom --
        /// <summary>
        /// 返回指定范围内的随机数。
        /// </summary>
        /// <param name="minValue">返回的随机数的范围下限(包含此值)</param>
        /// <param name="maxValue">返回的随机数的范围上限(不包含此值)。 <see cref="maxValue"/> 必须大于或等于 <see cref="maxValue"/>。</param>
        /// <returns>
        /// 大于或等于 minValue 且小于 maxValue 的32位有符号整数;
        /// 也就是说，返回值的范围包括 minValue 但不包括 maxValue。
        /// 如果 minValue 等于 maxValue，则返回 minValue。
        /// </returns>
        public static int GetRandom(int minValue, int maxValue)
        {
            lock (Rnd)
            {
                return Rnd.Next(minValue, maxValue);
            }
        }

        /// <summary>
        /// 返回小于指定最大值的非负随机数。
        /// </summary>
        /// <param name="maxValue">要返回的随机数的范围上限(不包含此值)。 maxValue 必须大于或等于零。</param>
        /// <returns>
        /// 大于或等于零且小于 maxValue 的32位有符号整数;
        /// 也就是说，返回值的范围包括零但不包括 maxValue。
        /// 如果 maxValue 等于零，则返回 maxValue。
        /// </returns>
        public static int GetRandom(int maxValue)
        {
            lock (Rnd)
            {
                return Rnd.Next(maxValue);
            }
        }

        /// <summary>
        /// 返回非负的随机数。
        /// </summary>
        /// <returns>大于或等于零且小于 <see cref="int.MaxValue"/> 的32位有符号整数;</returns>
        public static int GetRandom()
        {
            lock (Rnd)
            {
                return Rnd.Next();
            }
        }
        #endregion
        
        /// <summary>
        /// Gets random of given objects.
        /// 从给定对象的数组中获取一个随机对象。
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="objs">用于选择随机对象的对象列表</param>
        public static T GetRandomOf<T>(params T[] objs)
        {
            if (objs.IsNullOrEmpty())
            {
                throw new ArgumentException("objs can not be null or empty!", nameof(objs));
            }

            return objs[GetRandom(0, objs.Length)];
        }

        /// <summary>
        /// 从给定对象列表中生成随机列表。
        /// </summary>
        /// <typeparam name="T">列表中的项目类型</typeparam>
        /// <param name="items">items</param>
        /// <param name="count">随机列表中包含的项目个数</param>
        public static List<T> GenerateRandomizedList<T>(IEnumerable<T> items, int? count = null)
        {
            var currentList = new List<T>(items);
            var randomList = new List<T>();

            while (currentList.Any())
            {
                var randomIndex = GetRandom(0, currentList.Count);
                randomList.Add(currentList[randomIndex]);
                currentList.RemoveAt(randomIndex);
                
                if(count.HasValue && randomList.Count == count)
                    break;
            }

            return randomList;
        }
    }
}