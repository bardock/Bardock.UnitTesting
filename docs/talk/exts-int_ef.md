name: extensions-integrations-ef-header
layout: true

# Extensions and Integrations

## Entity Framework

---
template: extensions-integrations-ef-header

### PersistedAttribute

*Applies a PersistedEntityCustomization to parameters in methods decorated with AutoDataAttribute.*

```csharp
public void Update_ExistingAdultCustomer_Success(
            [Persisted] Customer e,
			...)
```

---
layout: true