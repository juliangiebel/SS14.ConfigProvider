namespace SS14.ConfigProvider.Tests;

public class ProviderTest : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;

    public ProviderTest(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void SaveAndLoadTest()
    {
        var provider = _fixture.Provider;
        provider.Set("test", "value");
        
        provider.Load();
        
        var hasResult = provider.TryGet("test", out var value);
        Assert.True(hasResult);
        Assert.Equal("value", value);
    }
}