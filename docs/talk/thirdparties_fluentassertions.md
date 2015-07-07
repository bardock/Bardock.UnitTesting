name: fluentassertions-header
layout: true

# Fluent Assertions

---
template: fluentassertions-header

Fluent Assertions is a set of .NET extension methods that allow you to **more naturally specify the expected outcome of a test**

### Before

```csharp
string theString = "ABCDEFGHI";

Assert.True(theString.StartsWith("AB"));
Assert.True(theString.EndsWith("HI"));
Assert.True(theString.Contains("EF"));
Assert.Equal(9, theString.Length);
```

### After

```csharp
theString.Should().StartWith("AB").And.EndWith("HI").And.Contain("EF").And.HaveLength(9);
```

---
template: fluentassertions-header

## Examples

```csharp
IEnumerable collection = new[] { 1, 2, 3 };
collection.Should().HaveCount(4, "because we thought we put three items in the collection"))
// It will throw an exception with the message: 
// "Expected <4> items because we thought we put three items in the collection, but found <3>."
```

```csharp
Action action = () => recipe.AddIngredient("Milk", 100, Unit.Spoon);
action
    .ShouldThrow<RuleViolationException>()
    .WithMessage("change the unit of an existing ingredient", ComparisonMode.Substring)
    .And.Violations.Should().Contain(BusinessRule.CannotChangeIngredientQuanity);
```

```csharp
dictionary.Should().ContainValue(myClass).Which.SomeProperty.Should().BeGreaterThan(0);

someObject.Should().BeOfType<Exception>().Which.Message.Should().Be("Other Message");
```

---
template: fluentassertions-header

## Why?

* Coding by Example
* Expressive Tests & Tests as Documentation
* The message when the test fail is more explanatory

More examples?

```csharp
theInteger.Should().BeOneOf(new[] { 3, 6});
theFloat.Should().BeApproximately(3.14F, 0.001F);
theDatetime.Should().BeAfter(1.February(2010));
theDatetime.Should().BeLessThan(10.Minutes()).Before(otherDatetime);
theDatetime.Should().BeCloseTo(March(2010).At(22,15), 2000);
collection.Should().NotBeEmpty()
     .And.HaveCount(4)
     .And.ContainInOrder(new[] { 2, 5 })
     .And.ContainItemsAssignableTo<int>();
collection.Should().IntersectWith(otherCollection);
collection.Should().BeInAscendingOrder(x => x.SomeProperty);
orderDto.ShouldBeEquivalentTo(order, options => options.ExcludingMissingMembers());
```

---
layout: true