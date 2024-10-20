## SaeedAzari.Core.Security.Identity: Implement Identity Authentication

Using [SaeedAzari.Core.Security.Identity] To login with Identity.


### Start


Install the package with NuGet:
```
dotnet add package SaeedAzari.Core.Security.Identity
```

<hr>

### Inject service
Just put the following code in Program.cs:

```cs

builder.Services.AddSecurity<TBaseIdentityUser, TBaseIdentityRole>( Configuration,  connectionString);

```

or 

```cs

builder.Services.AddSecurity<TBaseIdentityUser>( Configuration,  connectionString);

```



### Add Authorization

```cs

app.UseAuthorization();

```

<hr/>

### Login

Injcet `IAuthenticationService` to use methods