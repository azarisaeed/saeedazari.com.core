The  Repository for entity framework sql Library serves as a base for connecting to sql database.

### Start
Install the package with NuGet:
```
dotnet add package SaeedAzari.Core.Repositories.EF
```

this packages includes follwoing 

- Implemimentaion for database repository interfaces

Inherits AuditEntityRepository for audit entities
Inherits EntityRepository for entities

### Inject service
Just put the following code in Program.cs:

```cs

 builder.Services.AddSqlServer<TSqlserverDbContext>( string connectionString)

```


`TSqlserverDbContext` inherited from `CoreDBContext`