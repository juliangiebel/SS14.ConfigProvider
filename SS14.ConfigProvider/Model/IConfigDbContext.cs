using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace SS14.ConfigProvider.Model;

[PublicAPI]
public interface IConfigDbContext
{ 
    DbSet<ConfigurationStore> ConfigurationStore { get; }
}