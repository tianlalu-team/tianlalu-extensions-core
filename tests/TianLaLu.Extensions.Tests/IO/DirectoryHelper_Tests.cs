using Shouldly;
using Xunit;

namespace System.IO
{
    // ReSharper disable once InconsistentNaming
    public class DirectoryHelper_Tests
    {
        [Fact]
        public void Can_Search_When_File_Exists()
        {
            var assemblyPath = typeof(DirectoryHelper_Tests).Assembly.Location;
            var path = DirectoryHelper.Search(assemblyPath, "tianlalu-extensions-core.sln");
            
            assemblyPath.ShouldBe(typeof(DirectoryHelper_Tests).Assembly.Location);
            assemblyPath.StartsWith(path).ShouldBeTrue();
        }
        
        [Fact]
        public void Can_Not_Search_When_File_Not_Exists()
        {
            var assemblyPath = typeof(DirectoryHelper_Tests).Assembly.Location;
            var path = DirectoryHelper.Search(assemblyPath, "tianlalu-extensions-core.sln222");
            
            assemblyPath.ShouldBe(typeof(DirectoryHelper_Tests).Assembly.Location);
            path.ShouldBeNull();
        }
    }
}