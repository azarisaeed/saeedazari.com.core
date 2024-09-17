The Saeed Azari Base data Library abstractions serves as a base for base data .

### Start
Install the package with NuGet:
```
dotnet add package SaeedAzari.Core.CoreData.Abstraction

```

this packages includes follwoing 

- Interface for manager base data
- Interface for base data
- Abstract class for base data

Inherit your class from  `BaseCoreData` 

```
public class CLASSNAME :BaseCoreData
```

Implement  your interface from  `ICoreData` for your db structure

```
public Interface ICLASSNAME :ICoreData<CLASSNAME>
{
  // your code for db
}
```


implement `ICoreDataManager` for your class 

```
Class ClASSNAMEManager : ICoreDataManager<ClASSNAME>
```



