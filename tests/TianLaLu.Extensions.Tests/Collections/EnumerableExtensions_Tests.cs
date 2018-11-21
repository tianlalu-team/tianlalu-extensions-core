using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;

namespace System.Collections
{
    // ReSharper disable once InconsistentNaming
    public class EnumerableExtensions_Tests
    { 
        #region -- Cartesian --
        [Fact]
        public void Can_Calculation_Cartesian_Product_With_List()
        {
            List<List<string>> items = new List<List<string>>()
            {
                new List<string> { "a1","a2","a3"},
                new List<string> { "b4","b5"},
                new List<string> { "c6" }
            };

            var cartesian = items.Cartesian();
            cartesian.Count().ShouldBe(items[0].Count * items[1].Count * items[2].Count);
            var array = cartesian.Select(x => string.Join(",", x)).ToArray();
            array.ShouldContain("a1,b4,c6");
            array.ShouldContain("a1,b5,c6");
            array.ShouldContain("a2,b4,c6");
            array.ShouldContain("a2,b5,c6");
            array.ShouldContain("a3,b4,c6");
            array.ShouldContain("a3,b5,c6");
        }
        
        [Fact]
        public void Can_Calculation_Cartesian_Product_With_Array()
        {
            var items = new[]
            {
                new[] { "a1","a2","a3"},
                new[] { "b4","b5"},
                new[] { "c6" }
            };

            var cartesian = items.Cartesian();
            cartesian.Count().ShouldBe(items[0].Length * items[1].Length * items[2].Length);
            var array = cartesian.Select(x => string.Join(",", x)).ToArray();
            array.ShouldContain("a1,b4,c6");
            array.ShouldContain("a1,b5,c6");
            array.ShouldContain("a2,b4,c6");
            array.ShouldContain("a2,b5,c6");
            array.ShouldContain("a3,b4,c6");
            array.ShouldContain("a3,b5,c6");
        }
        #endregion
    }
}