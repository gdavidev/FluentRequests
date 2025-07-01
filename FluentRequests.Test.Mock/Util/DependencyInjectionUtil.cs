using Microsoft.Extensions.DependencyInjection;

namespace FluentRequests.Test.Mock.Util;

public class DependencyInjectionUtil
{
    public IServiceCollection Services { get; set; }
    public IServiceProvider ServiceProvider { get; set; }

    public DependencyInjectionUtil()
    {
        Services = new ServiceCollection();
        ServiceProvider = Services.BuildServiceProvider();
    }
}