using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace SS14.ConfigProvider.Model;

/// <summary>
/// Represents a database context interface for managing and interacting with stored configuration values.
/// </summary>
[PublicAPI]
public interface IConfigDbContext
{ 
    /// <summary>
    /// Stored configuration values.
    /// </summary>
    DbSet<ConfigurationStore> ConfigurationStore { get; }
}