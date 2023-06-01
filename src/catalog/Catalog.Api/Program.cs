using Catalog.Api;
using Catalog.Api.Services;
using Volo.Abp;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseAutofac();
await builder.AddApplicationAsync<CatalogApiModule>();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
var app = builder.Build();
await app.InitializeApplicationAsync();
await app.RunAsync();
//app.Run();