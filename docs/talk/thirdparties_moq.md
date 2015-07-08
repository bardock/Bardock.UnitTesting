name: moq-header
layout: true

# Moq

---
template: moq-header

Moq is a **mocking framework for .NET**. It is used in unit testing to **isolate your system under test from its dependencies** and ensure that the proper methods on the dependent objects are being called.

## Features at a glance

* Strong-typed.
* Unsurpassed VS intellisense integration.
* No Record/Replay idioms to learn.
* VERY low learning curve.
* Granular control over mock behavior.
* Mock both interfaces and classes.
* Override expectations.
* Pass constructor arguments for mocked classes.
* Intercept and raise events on mocks.

---
template: moq-header
name: moq-methods-header
layout: true

## Methods

---
template: moq-methods-header

* Basic usage
```csharp
var mock = new Mock<IFoo>();
mock.Setup(foo => foo.DoSomething("ping"))
		.Returns(true);
```

* Access invocation arguments when returning a value
```csharp
mock.Setup(x => x.DoSomething(It.IsAny<string>()))
        .Returns((string s) => s.ToLower());
```

---
template: moq-methods-header

* Throwing when invoked
```csharp
mock.Setup(foo => foo.DoSomething("reset"))
		.Throws<InvalidOperationException>();
mock.Setup(foo => foo.DoSomething(""))
		.Throws(new ArgumentException("command");
```

* Returning different values on each invocation
```csharp
var mock = new Mock<IFoo>(); var calls = 0;
mock.Setup(foo => foo.GetCountThing())
		.Returns(() => calls)
		.Callback(() => calls++);
```

---
template: moq-header

## Matching arguments

* Any value
```csharp
mock.Setup(foo => foo.DoSomething(It.IsAny<string>())).Returns(true);
```

* Matching Func<int>, lazy evaluated
```csharp
mock.Setup(foo => foo.Add(It.Is<int>(i => i % 2 == 0))).Returns(true); 
```

* Matching ranges
```csharp
mock.Setup(foo => foo.Add(It.IsInRange<int>(0, 10, Range.Inclusive))).Returns(true); 
```

* Matching regex
```csharp
mock.Setup(x => x.DoSomething(It.IsRegex("[a-d]+", RegexOptions.IgnoreCase))).Returns("foo");
```

---
template: moq-header

## Properties

* Basic usage
```csharp
mock.Setup(foo => foo.Name)
		.Returns("bar");
```

* Expect an invocation to set the value to "foo"
```csharp
mock.SetupSet(foo => foo.Name = "foo");
```

* Verify the setter
```csharp
mock.VerifySet(foo => foo.Name = "foo");
```

---
template: moq-header

## Callbacks

* Access invocation arguments
```csharp
mock.Setup(foo => foo.Execute(It.IsAny<string>()))
    	.Returns(true)
    	.Callback((string s) => calls.Add(s));
```

* Access arguments for methods with multiple parameters
```csharp
mock.Setup(foo => foo.Execute(It.IsAny<int>(), It.IsAny<string>()))
    	.Returns(true)
    	.Callback<int, string>((i, s) => calls.Add(s));
```

* Callbacks can be specified before and after invocation
```csharp
mock.Setup(foo => foo.Execute("ping"))
    	.Callback(() => Console.WriteLine("Before returns"))
    	.Returns(true)
    	.Callback(() => Console.WriteLine("After returns"));
```

---
template: moq-header

## Verification

```csharp
mock.Verify(foo => foo.Execute("ping"));

// Verify with custom error message for failure
mock.Verify(foo => foo.Execute("ping"), "When doing operation X, the service should be pinged always");

// Method should never be called
mock.Verify(foo => foo.Execute("ping"), Times.Never());

// Called at least once
mock.Verify(foo => foo.Execute("ping"), Times.AtLeastOnce());

// Verify getter invocation
mock.VerifyGet(foo => foo.Name);

// Verify setter invocation, regardless of value.
mock.VerifySet(foo => foo.Name);

// Verify setter called with specific value
mock.VerifySet(foo => foo.Name ="foo");

// Verify setter with an argument matcher
mock.VerifySet(foo => foo.Value = It.IsInRange(1, 5, Range.Inclusive));
```

---
template: moq-header

## Mock behavior

* True mock
```csharp
var mock = new Mock(MockBehavior.Strict);
```

* Invoke base class implementation by default
```csharp
var mock = new Mock { CallBase = true };
``` 

* Automatic recursive mock
```csharp
var mock = new Mock<IFoo> { DefaultValue = DefaultValue.Mock };
// this property access would return a new mock of IBar as it's "mock-able"
IBar value = mock.Object.Bar;
//this allows us to also use this instance to set further expectations on it if we want
var barMock = Mock.Get(value);
barMock.Setup(b => b.Submit()).Returns(true);
```

---
layout: true