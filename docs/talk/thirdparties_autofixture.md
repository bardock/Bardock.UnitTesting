name: afx-header
layout: true

# AutoFixture

---
template: afx-header

*"Write maintainable unit tests, faster"*

**AutoFixture makes it easier for developers to do Test-Driven Development by automating non-relevant Test Fixture Setup**, allowing the Test Developer to focus on the essentials of each test case.

---
template: afx-header
name: afx-internal-architecture-header
layout: true

## Internal architecture

---
template: afx-internal-architecture-header

### Fixture high-level layout

* The fixture class **is a specialization of IFixture**.
* **Contains a set of specimen builders** called engine parts.
* Engine Parts contain logic to instantiate
	* Well known **primitive types**.
	* **Complex types**, via reflection.

.autofixtureimage[![](./img/thirdparties-autofixture-architecture.png)]

---
template: afx-internal-architecture-header

### Customizations

* By default, Customizations collection is empty.
* Customize the fixture by adding ISpecimenBuilder instances.
* Specimen Builders intercept the default engine parts.
* Allows the customization to get a shot at each request before them.
* Basic usage:
```csharp
//by convention, all fields that end with "Email"
//will have its value set to "foo@mail.com"
fixture.Customize(new EmailConventionCustomization());
```

---
template: afx-internal-architecture-header

### Residue collectors

* There are requests that a Fixture cannot satisfy.
* Adding an ISpecimenBuilder instance to the ResidueCollectors collection gives that specimen builder a chance to handle such request.

### Behaviors

* A behavior is a decorator.
* Can monitor or intercept requests and specimens.
* Tracing and Recursion Guarding are implemented as Behaviors.

---
template: afx-header



---
layout: true