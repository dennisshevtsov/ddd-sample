using DddSample.Domain.DeliveryPoints;
using DddSample.Domain.Merchants;
using DddSample.Domain.Warehouses;
using DddSample.Infrastructure;
using DddSample.Infrastructure.DeliveryPoints;
using DddSample.Infrastructure.Merchants;
using DddSample.Infrastructure.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServicesExtensions
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, string configSectionPath = "DddSampleDb")
  {
    ArgumentNullException.ThrowIfNull(services);
    ArgumentNullException.ThrowIfNull(configSectionPath);

    services.AddOptions<DddSampleDbSettings>().BindConfiguration(configSectionPath);

    services.AddDbContext<DbContext, DddSampleDbContext>((provider, builder) =>
    {
      var options = provider.GetRequiredService<IOptions<DddSampleDbSettings>>().Value;
      ArgumentException.ThrowIfNullOrEmpty(options.ConnectionString);

      builder.UseNpgsql(options.ConnectionString);
    });

    services.AddScoped<IDeliveryPointRepository, DeliveryPointRepository>();
    services.AddScoped<IMerchantRepository, MerchantRepository>();
    services.AddScoped<IWarehouseRepository, WarehouseRepository>();

    return services;
  }
}
