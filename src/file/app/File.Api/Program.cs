using System.Xml.XPath;
using File.Api.Services;
using Microsoft.OpenApi.Models;

namespace File.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCors();
        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

        // Add services to the container.
        builder.Services.AddGrpc((opt) =>
        {
            opt.EnableDetailedErrors = true;
        }).AddJsonTranscoding();

        builder.Services.AddGrpcSwagger();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "File API", Version = "V1" });
        });

        var app = builder.Build();
        app.UseCors(opt=>opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("v1/swagger.json", "File API V1");
        });
        // Configure the HTTP request pipeline.
        app.MapGrpcService<FileService>();
        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}
