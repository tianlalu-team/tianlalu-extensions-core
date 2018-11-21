using System.Collections.Generic;
using System.Linq;

namespace System.Collections
{
    public static class EnumerableExtensions
    {
        #region -- Cartesian --

        /// <summary>
        /// 求二维数组(集合)的笛卡尔积
        /// </summary>
        /// <seealso cref="https://baike.baidu.com/item/笛卡尔乘积"/>
        /// <code>
        /// <![CDATA[
        ///  var result = new List<List<string>>() {
        ///    new List<string> { "a1","a2","a3"},
        ///    new List<string> { "b4","b5"},
        ///    new List<string> { "c6" }
        ///  }.Cartesian();
        ///
        ///  // cartesion is:
        ///  //  [
        ///  //    ["a1", "b4", "c6"],
        ///  //    ["a1", "b5", "c6"],
        ///  //    ["a2", "b4", "c6"],
        ///  //    ["a2", "b5", "c6"],
        ///  //    ["a3", "b4", "c6"],
        ///  //    ["a3", "b5", "c6"],
        ///  //  ]
        /// ]]>
        /// </code>
        /// <param name="sequences"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Cartesian<T>(this IEnumerable<IEnumerable<T>> sequences)
        {
            return CartesianInternal(sequences);
        }

        /// <summary>
        /// 求二维数组(集合)的笛卡尔积
        /// </summary>
        /// <seealso cref="https://baike.baidu.com/item/笛卡尔乘积"/>
        /// <code>
        /// <![CDATA[
        ///  var result = new[] {
        ///    new[] { "a1","a2","a3"},
        ///    new[] { "b4","b5"},
        ///    new[] { "c6" }
        ///  }.Cartesian();
        ///
        ///  // result is:
        ///  //  [
        ///  //    ["a1", "b4", "c6"],
        ///  //    ["a1", "b5", "c6"],
        ///  //    ["a2", "b4", "c6"],
        ///  //    ["a2", "b5", "c6"],
        ///  //    ["a3", "b4", "c6"],
        ///  //    ["a3", "b5", "c6"],
        ///  //  ]
        /// ]]>
        /// </code>
        /// <param name="sequences"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[][] Cartesian<T>(this T[][] sequences)
        {
            return CartesianInternal(sequences).Select(x => x.ToArray()).ToArray();
        }

        private static IEnumerable<IEnumerable<T>> CartesianInternal<T>(IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> tempProduct = new[] {Enumerable.Empty<T>()};

            return sequences.Aggregate(tempProduct, (accumulator, sequence) =>
                from enumerable in accumulator
                from item in sequence
                select enumerable.Concat(new[] {item})
            );
        }

        #endregion
    }
}