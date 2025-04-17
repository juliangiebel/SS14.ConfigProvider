# SS14.ConfigProvider

A configuration provider implementation for [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/)
providing configuration values from a configurable EfCore data source.

## Usage
To use the configuration provider you need to use the `AddConfigurationDb`.
It takes a `DbContext` implementing `IConfigurationDbContext` as a type parameter. You need to use the callback parameter to configure a `DbContextOptionsBuilder` to use the correct database.

````csharp
// Example using an in memory database
builder.Configuration.AddConfigurationDb<TestContext>(b => b.UseInMemoryDatabase("TestDb"));
````

## Storing values
The configuration provider stores values in a table called `ConfigurationStore`.
To store a value, you need to use the `ConfigurationStore` entity. This package doesn't provide its own way of setting
values in the database. You need to use EfCore for that.

## Working with dynamic configuration values
Using `IConfiguration.Bind` works for when you call `Bind` every time you want to get a configuration value.
A smarter approach is to use `IOptionsSnapshot<T>` to get a snapshot of the configuration values in scoped services
`IOptionsMonitor` for singleton services or when you need a delegate that gets called on configuration updates.

For more information see the [Microsoft documentation about the options pattern](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-9.0).