﻿using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SS14.ConfigProvider.Model;

namespace SS14.ConfigProvider;

[PublicAPI]
public sealed class DbConfigurationProvider<TContext> : ConfigurationProvider, IDisposable, IAsyncDisposable where TContext: DbContext, IConfigDbContext 
{
    private DbConfigurationSource<TContext> Source { get; }
    private readonly Timer? _timer;
    private readonly DbContextOptions<TContext> _contextOptions;
    public DbConfigurationProvider(DbConfigurationSource<TContext> source)
    {
        Source = source;
        
        var contextBuilder = new DbContextOptionsBuilder<TContext>();
        source.OptionsAction?.Invoke(contextBuilder);
        _contextOptions = contextBuilder.Options;
        
        if (!Source.ReloadPeriodically) return;
        
        _timer = new Timer
        (
            callback: ReloadSettings,
            dueTime: TimeSpan.FromSeconds(10),
            period: TimeSpan.FromSeconds(Source.PeriodInSeconds),
            state: null
        );
    }
    
    public override void Load()
    {
        using var context = (TContext)Activator.CreateInstance(_contextOptions.ContextType, _contextOptions)!;
        
        Data = context.ConfigurationStore.ToDictionary<ConfigurationStore, string, string?>(
            c => c.Name, 
            c => c.Value, 
            StringComparer.OrdinalIgnoreCase);
        
    }

    public override void Set(string key, string? value)
    {
        base.Set(key, value);
        
        using var context = (TContext)Activator.CreateInstance(_contextOptions.ContextType, _contextOptions)!;

        var configurationValue = context.ConfigurationStore.SingleOrDefault(c => c.Name == key);

        configurationValue ??= new ConfigurationStore
        {
            Name = key,
            Value = value
        };

        context.ConfigurationStore.Update(configurationValue);
        context.SaveChanges();
    }

    private void ReloadSettings(object? state)
    {
        Load();
        OnReload();
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_timer != null) await _timer.DisposeAsync();
    }
}