name: xunit-header
layout: true

# xUnit

---
template: xunit-header

xUnit.net is a free, open source, community-focused **unit testing tool** for the .NET Framework

---
template: xunit-header


```csharp
using Xunit;

namespace MyFirstUnitTests
{
    public class Class1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}
```

---
layout: true