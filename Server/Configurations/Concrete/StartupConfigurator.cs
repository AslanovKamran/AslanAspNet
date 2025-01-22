using AslanAspNet.Server.Builders;
using AslanAspNet.Server.Configurations.Abstract;
using AslanAspNet.Server.Middlewares.Concrete;

namespace AslanAspNet.Server.Configurations.Concrete;

public class StartupConfigurator : IConfigurator
{
    public void Configure(MiddlewareBuilder builder)
    {
        builder.Use<LoggerMiddleware>();
        builder.Use<HtmlFileMiddleware>();
    }
}
