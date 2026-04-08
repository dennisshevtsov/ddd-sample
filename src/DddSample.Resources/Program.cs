using Microsoft.AspNetCore.Rewrite;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureInfrastructure(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddInfrastructure();

WebApplication app = builder.Build();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.UseRewriter(new RewriteOptions().AddRedirect("^$", "/swagger/index.html"));
app.Run();
