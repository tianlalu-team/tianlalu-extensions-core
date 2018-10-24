using System.Linq.Expressions;
using Shouldly;
using Xunit;

namespace System
{
    // ReSharper disable once InconsistentNaming
    public class ObjectExtensions_Tests
    {
        private class SomeClass
        {
            private string PrivateProperty { get; set; }
            
            protected string ProtectedProperty { get; set; }
            
            internal string InternalProperty { get; set; }
            
            public string PrivateSetterProperty { get; private set; }
            
            public string ProtectedSetterProperty { get; protected set; }
            
            public string InternalSetterProperty { get; internal set; }
        }
        
        [Fact]
        public void Can_SetProperty_And_GetProperty_With_Expression()
        {
            var obj = new SomeClass();
            obj.SetProperty(x => x.InternalProperty, "value1");
            obj.GetProperty(x => x.InternalProperty).ShouldBe("value1");
            
            obj.SetProperty(x => x.PrivateSetterProperty, "value2");
            obj.GetProperty(x => x.PrivateSetterProperty).ShouldBe("value2");
            
            obj.SetProperty(x => x.ProtectedSetterProperty, "value3");
            obj.GetProperty(x => x.ProtectedSetterProperty).ShouldBe("value3");
            
            obj.SetProperty(x => x.InternalSetterProperty, "value4");
            obj.GetProperty(x => x.InternalSetterProperty).ShouldBe("value4");
        }
        
        [Fact]
        public void Can_SetProperty_And_GetProperty_With_PropertyName()
        {
            var obj = new SomeClass();

            obj.SetProperty("PrivateProperty", "value1");
            obj.GetProperty("PrivateProperty").ShouldBe("value1");

            obj.SetProperty("ProtectedProperty", "value2");
            obj.GetProperty("ProtectedProperty").ShouldBe("value2");
            
            obj.SetProperty("InternalProperty", "value3");
            obj.GetProperty("InternalProperty").ShouldBe("value3");
            
            obj.SetProperty("PrivateSetterProperty", "value4");
            obj.GetProperty("PrivateSetterProperty").ShouldBe("value4");

            obj.SetProperty("ProtectedSetterProperty", "value5");
            obj.GetProperty("ProtectedSetterProperty").ShouldBe("value5");

            obj.SetProperty("InternalSetterProperty", "value6");
            obj.GetProperty("InternalSetterProperty").ShouldBe("value6");
        }
    }
}