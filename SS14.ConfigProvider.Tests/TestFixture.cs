using Microsoft.EntityFrameworkCore;

namespace SS14.ConfigProvider.Tests;

public class TestFixture
{
    public DbConfigurationProvider<TestContext> Provider => new(
        new DbConfigurationSource<TestContext>
        {
            OptionsAction = b => b.UseInMemoryDatabase("TestDb")
        });
    
}