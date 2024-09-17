The enum service for work with enums and add descriptor to them.

### Start
Install the package with NuGet:
```
dotnet add package SaeedAzari.Core.Enums

```

this packages includes follwoing 

- Attribute for add description to enum
- Service to get them and work with them in application
## attribute usage

add `EnumDescriptorAttribute` as attribute to enums

### Service Usage
Just inherit or inject instance of `Descriptor<>` or `IDescriptor<>`
