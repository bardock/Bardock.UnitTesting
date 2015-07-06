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

	ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued
	"1","Chai","1","1","10 boxes x 20 bags","18.0000","39","0","10","False"
	"2","Chang","1","1","24 - 12 oz bottles","19.0000","17","40","25","False"

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