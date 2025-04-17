using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SS14.ConfigProvider.Model;

namespace SS14.ConfigProvider;


/// <summary>
/// Extension methods for adding a db configuration source to an <see cref="IConfigurationManager"/>.
/// </summary>
[PublicAPI]
public static class DbConfigurationExtension
{
    /// <summary>
    /// Adds a database configuration source to the <see cref="IConfigurationManager"/>.<br/>
    /// See <see cref="DbConfigurationSource{TContext}"/> for more information.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the database context, which must implement <see cref="IConfigDbContext"/>.
    /// </typeparam>
    /// <param name="manager">
    /// The <see cref="IConfigurationManager"/> to which the database configuration source will be added.
    /// </param>
    /// <param name="optionsAction">
    /// A callback to configure the <see cref="DbContextOptionsBuilder"/> for the database context.
    /// </param>
    /// <code language="csharp">
    ///     // Example using an in memory database
    ///     builder.Configuration.AddConfigurationDb&lt;TestContext&gt;(b => b.UseInMemoryDatabase("TestDb"));
    /// </code>
    public static void AddConfigurationDb<TContext>(this IConfigurationManager manager,
        Action<DbContextOptionsBuilder> optionsAction) where TContext : DbContext, IConfigDbContext
    {
        var source = new DbConfigurationSource<TContext>
        {
            OptionsAction = optionsAction
        };
        
        IConfigurationBuilder builder = manager;
        builder.Add(source);
    }
}