
public class DependencySetupFixture
{
    protected readonly IServiceProvider _serviceProvider;

    public DependencySetupFixture()
    {
        var serviceCollection = new ServiceCollection();
        _serviceProvider=serviceCollection.BuildServiceProvider();        
    }
}