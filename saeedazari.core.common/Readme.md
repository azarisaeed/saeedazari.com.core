The Saeed Azari Common Library serves as a foundational and versatile resource for shared components and utilities within the Saeed Azari projects ecosystem.

### Start
Install the package with NuGet:
```
dotnet add package saeedazari.core.common
```

this packages includes follwoing 

- models for **Error** and diffrent type of **Result**
- the implmentation of **ApplicationContext**

to Add Application context to your app Just put the following code in Program.cs:
```cs
builder.Services.AddApplicationContext();
```
