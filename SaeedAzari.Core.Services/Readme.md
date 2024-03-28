The Saee Azari Service Library serves as a base for domain services.

### Start
Install the package with NuGet:
```
dotnet add package SaeedAzari.Core.Services

```

this packages includes follwoing 

- Implemimentaion for repository Services interfaces

Inherits AuditEntityService for audit entities
Inherits EntityService for entities

### Inject service
Just put the following code in Program.cs:

```cs

 builder.Services.RegisterBaseServices( )

```
