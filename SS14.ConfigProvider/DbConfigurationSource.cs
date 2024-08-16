using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SS14.ConfigProvider.Model;

namespace SS14.ConfigProvider;

[PublicAPI]
public sealed class DbConfigurationSource<TContext> : IConfigurationSource where TContext : DbContext, IConfigDbContext
{
    public required Action<DbContextOptionsBuilder> OptionsAction { get; init;}
    
    public bool ReloadPeriodically { get; init; }

    public int PeriodInSeconds { get; init; } = 5;

    public IConfigurationProvider Build(IConfigurationBuilder builder) =>
        new DbConfigurationProvider<TContext>(this);
}