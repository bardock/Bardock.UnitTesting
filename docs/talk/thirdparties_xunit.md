name: xunit-header
layout: true

# xUnit

---
template: xunit-header

xUnit.net is a free, open source, community-focused **unit testing tool** for the .NET Framework

---
template: xunit-header


**Facts** are tests which are always true. They test invariant conditions.

```csharp
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
```

---
template: xunit-header

**Theories** are tests which are only true for a particular set of data.

```csharp
[Theory]
[InlineData(3)]
[InlineData(5)]
[InlineData(6)]
public void MyFirstTheory(int value)
{
    Assert.True(IsOdd(value));
}

bool IsOdd(int value)
{
    return value % 2 == 1;
}
```

---
template: xunit-header
name: xunit-parallel-header
layout: true

---
template: xunit-parallel-header

## Running Tests in Parallel

* Running unit tests in parallel is a new feature in xUnit.net **version 2**
* Take advantage of the available **resources**

### Test Collections

* **Tests of different collections can run in parallel**
* Tests within the same collection will not run in parallel against each other
* By default, **each test class is a unique test collection**
* If we need to indicate that **multiple test classes should not be run in parallel** against one another, then we place them into the **same test collection**

---
template: xunit-parallel-header

```csharp
[Collection("Our Test Collection #1")]
public class TestClass1
{
    [Fact]
    public void Test1()
    {
        Thread.Sleep(3000);
    }
}

[Collection("Our Test Collection #1")]
public class TestClass2
{
    [Fact]
    public void Test2()
    {
        Thread.Sleep(5000);
    }
}
```

---
template: xunit-header
name: xunit-sharedctx-header
layout: true

## Shared Context between Tests

---
template: xunit-sharedctx-header

### Constructor and Dispose

* Used when you want to share the setup and cleanup code, **without sharing the object instance**
* xUnit.net creates **a new instance of the test class for every test** that is run
* Any code which is placed into the **constructor** of the test class will be run for every single test
* For context cleanup, add the **IDisposable** interface to your test class, and **put the cleanup code in the Dispose()** method

```csharp
public class MyDatabaseTests : IDisposable
{
	public MyDatabaseTests() { /* setup */ }
	public void Dispose() { /* cleanup */ }

    [Fact]
    public void Test1() { ... }
}
```

---
template: xunit-sharedctx-header

### Class Fixtures

* Used when you want to create a **single test context** and share it among **all the tests in the class**
* xUnit.net will ensure that the fixture instance will be **created before any of the tests have run**
* Once all the tests have finished, it will **clean up the fixture** object by calling Dispose

```csharp
public class DatabaseFixture : IDisposable
{
	public DatabaseFixture() { /* setup */ }
	public void Dispose() { /* cleanup */ }
}
public class MyDatabaseTests : IClassFixture<DatabaseFixture>
{
	public MyDatabaseTests(DatabaseFixture fixture) { /* store fixture */ }

    [Fact]
    public void Test1() { ... }
}
```

---
template: xunit-sharedctx-header

### Collection Fixtures

* Used when you want to create a **single test context** and share it among tests in **several test classes**

```csharp
public class DatabaseFixture : IDisposable { ... }

[CollectionDefinition("Database collection")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
    // This class has no code and is never created
}

[Collection("Database collection")]
public class DatabaseTestClass1 { ... }

[Collection("Database collection")]
public class DatabaseTestClass2 { ... }
```

---
layout: true