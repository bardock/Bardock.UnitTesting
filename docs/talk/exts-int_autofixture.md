name: extensions-integrations-autofixture-header
layout: true

# Extensions and Integrations

## Autofixture

---
template: extensions-integrations-autofixture-header

### StringDataAnnotationsCustomization

*A customization that removes AutoFixture StringLengthAttribute DataAnnotations default support and adds custom combined support for String DataAnnotation attributes.*

* Supports
	* StringLengthAttribute
	* MinLengthAttribute
	* MaxLengthAttribute
	* EmailAddressAttribute

---
template: extensions-integrations-autofixture-header

### MapMembersCustomization

*A customization that maps the generation of member specimens of a source type to a destination type*

```csharp
new MapMembersCustomization<CustomerCreate,Customer>(
    new MembersMappingComposer<CustomerCreate, Customer>()
        .Map(c => c.FirstName, c => c.FirstName)
        .Map(c => c.Surname, c => c.LastName)
)
```

---
template: extensions-integrations-autofixture-header

### MapMembersAttribute

*An attribute that can be applied to parameters in an AutoData-driven theory to indicate that the parameter value should have its members mapped to the destination type members*

```csharp
public class MapToCustomerAttribute : MapMembersAttribute
{
    public MapToCustomerAttribute()
        : base(typeof(Customer))
    { }

    public override IEnumerable<MemberMapping> Configure(Type sourceType, Type destinationType)
    {
        return new MembersMappingComposer<CustomerCreate, Customer>()
                    .Map(c => c.FirstName, c => c.FirstName)
                    .Map(c => c.Surname, c => c.LastName);
    }
}
```

---
template: extensions-integrations-autofixture-header

### AutoMapMembersAttribute

*An attribute that can be applied to parameters in an AutoData-driven theory to indicate that the parameter value should have its **members auto mapped by an AutoMapper Map** to the destination type members*

```csharp
//Attribute declaration
public class AutoMapToCustomerAttribute : AutoMapMembersAttribute
{
    public AutoMapToCustomerAttribute()
        : base(typeof(Customer))
    { }
}

//Apply auto-mapping by an existing AutoMapper Map
public void Create_ValidEmail_SendMail(
            [AutoMapToCustomer] CustomerCreate data,
			...)
```

---
layout: true