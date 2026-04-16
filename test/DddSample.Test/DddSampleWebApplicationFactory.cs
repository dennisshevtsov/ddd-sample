using Microsoft.AspNetCore.Mvc.Testing;

namespace DddSample.Test;

internal sealed class DddSampleWebApplicationFactory : WebApplicationFactory<Program>
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureAppConfiguration((context, conf) =>
    {
      conf.AddJsonFile("appsettings.Test.json");
    });
  }
}
