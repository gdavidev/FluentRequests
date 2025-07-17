using Microsoft.Extensions.DependencyInjection;

namespace Requests.Test.Mock.Util;

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