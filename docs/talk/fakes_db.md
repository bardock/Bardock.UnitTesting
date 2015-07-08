name: fakes-db-header
layout: true

# Fakes

## DB

---
template: fakes-db-header

### Abstraction

A set of abstractions for unit testing purposes.

* **IDataContextWrapper**
* **IDataContextScope**
* **IDataContextScopeFactory**

```csharp
using (var s = _dataContextScopeFactory.CreateDefault())
{
    s.Db.Add(new Country() { ID = 1, Name = "Argentina", IsoCode = "AR" });
	s.Db.Add(new Country() { ID = 2, Name = "Canada", IsoCode = "CA" });
} // Ending of the scope automatically saves changes and detachs all entities. 
```

---
template: fakes-db-header

### Entity Framework

Unit testing data access implementations for entity framework.

* **DataContextWrapper**
	* A wrapper for a DbContext
	* Tracks in-going and out-going entities.
* **DataContextScope**
	* A scope for a DataContextWrapper instance.
* **DataContextScopeFactory**
	* Creates a DataContext scope with no AutoDetectChangesEnabled and ValidateOnSaveEnabled configurations.

---
template: fakes-db-header

### Effort

*Effort is a powerful tool that enables a convenient way to create automated tests for Entity Framework based applications.*

#### DataLoaders

Provide functionality for seeding initial data for a table.

* CSV Dataloader
* Table Dataloader
* CachingTable Dataloader
* **Typed (introduced by bardock)**

---
template: fakes-db-header

* CSV
```
	ID,Name,IsoCode
	"1","Argentina","AR"
	"2","Canada","CA"
```

* Typed
```csharp
public class CountriesDataLoader : IEntityDataLoader<Country>
{
    	public IEnumerable<Country> GetData()
    	{
        	yield return new Country()
        	{
        	    ID = (int)Country.Options.Canada,
        	    Name = "Canada",
        	    IsoCode = "CA"
        	};
		}
}
```
---
layout: true