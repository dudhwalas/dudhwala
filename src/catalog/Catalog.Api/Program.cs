using Catalog.Api;
using Catalog.Api.Services;
using Volo.Abp;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseAutofac(); // Integrate Autofac!
await builder.AddApplicationAsync<CatalogApiModule>();
var app = builder.Build();
await app.InitializeApplicationAsync();
await app.RunAsync();