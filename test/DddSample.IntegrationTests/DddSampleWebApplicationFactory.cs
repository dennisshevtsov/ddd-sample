using DddSample.IntegrationTests.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace DddSample.Infrastructure.IntegrationTests;

public sealed class DddSampleWebApplicationFactory : WebApplicationFactory<Program>
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureAppConfiguration((context, configurationBuilder) =>
    {
      configurationBuilder.AddJsonFile("appsettings.Test.json");
    });
    builder.ConfigureServices(services =>
    {
      services.AddScoped(provider => RestService.For<IDeliveryPointApi>("http://localhost:5001"));
      services.AddScoped(provider => RestService.For<IMerchantApi>("http://localhost:5001"));
    });
  }
}
