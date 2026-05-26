using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace DddSample.Infrastructure.IntegrationTest;

public sealed class DddSampleWebApplicationFactory : WebApplicationFactory<Program>
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureAppConfiguration((context, conf) =>
    {
      conf.AddJsonFile("appsettings.Test.json");
    });
  }
}
