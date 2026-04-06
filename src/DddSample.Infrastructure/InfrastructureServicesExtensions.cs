using DddSample.Domain.DeliveryPoints;
using DddSample.Domain.Merchants;
using DddSample.Domain.Warehouses;
using DddSample.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServicesExtensions
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
  {
    ArgumentNullException.ThrowIfNull(services);

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

  public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    ArgumentNullException.ThrowIfNull(services);
    ArgumentNullException.ThrowIfNull(configuration);
    services.Configure<DddSampleDbSettings>(configuration);
    return services;
  }
}
