using System.Collections.ObjectModel;
using Shouldly;
using Xunit;

namespace System.Collections
{
    // ReSharper disable once InconsistentNaming
    public class CollectionExtensions_Tests
    {
        [Fact]
        public void Can_Determine_Array_Expression()
        {
            int[] array = null;
            array.IsNullOrEmpty().ShouldBeTrue();

            array = new int[0];
            array.IsNullOrEmpty().ShouldBeTrue();

            array = new[] {1};
            array.IsNullOrEmpty().ShouldBeFalse();
        }

        [Fact]
        public void Can_Determine_Collection_Expression()
        {
            Collection<int> collection = null;
            collection.IsNullOrEmpty().ShouldBeTrue();

            collection = new Collection<int>();
            collection.IsNullOrEmpty().ShouldBeTrue();

            collection = new Collection<int>(new[] {1});
            collection.IsNullOrEmpty().ShouldBeFalse();
        }
    }
}