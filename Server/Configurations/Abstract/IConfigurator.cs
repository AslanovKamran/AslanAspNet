using AslanAspNet.Server.Builders;

namespace AslanAspNet.Server.Configurations.Abstract;

public interface IConfigurator
{
    // Метод, собирающий все m/w,
    // которые будут использоваться на сервере 
    void Configure(MiddlewareBuilder builder);
}
