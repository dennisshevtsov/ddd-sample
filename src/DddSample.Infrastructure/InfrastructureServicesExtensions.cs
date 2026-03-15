using DddSample.Domain.DeliveryPoints;
using DddSample.Domain.Merchants;
using DddSample.Domain.Warehouses;
using DddSample.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServicesExtensions
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
  {
    ArgumentNullException.ThrowIfNull(services);
    services.AddScoped<IDeliveryPointRepository, DeliveryPointRepository>();
    services.AddScoped<IMerchantRepository, MerchantRepository>();
    services.AddScoped<IWarehouseRepository, WarehouseRepository>();
    return services;
  }
}
