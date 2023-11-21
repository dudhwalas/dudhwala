using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("Catalog v1", new OpenApiInfo { Title = "Catalog API", Version = "v1" });
    c.SwaggerDoc("File v1", new OpenApiInfo { Title = "File API", Version = "v1" });
});
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("http://localhost:5010/catalog-api/swagger/v1/swagger.json", "Catalog API V1");
    c.SwaggerEndpoint("http://localhost:5010/file-api/swagger/v1/swagger.json", "File API V1");
});
}

app.UseCors();

app.UseHttpsRedirection();

app.MapReverseProxy();

await app.RunAsync();