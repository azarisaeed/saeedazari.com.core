## SaeedAzari.Core.Caching: serve caching manager

Using [SaeedAzari.Core.Caching] To use cachinbg.

### Start

Install the package with NuGet:

```
dotnet add package SaeedAzari.Core.Caching
```

<hr>

### Inject manager
Just put the following code in Program.cs:

```cs

builder.Services.AddCaching();

or

builder.Services.AddCaching(setupAction);

or

builder.Services.AddCaching(memoryCacheEntryOptions);


```


you can get entry option with GetMemoryCacheEntryOptions() :

```cs

CacheManagerExtention.GetMemoryCacheEntryOptions()

```
with defaults below

<param name="SlidingExpiration"> default is 1 hour</param>
<param name="AbsoluteExpiration">default is 1 day</param>

Caching manager is type of `ICacheManager<>`

Caching manager with multi key is type of `IMultiKeyCacheManager<>`