using AzureClassLibrary.Monitor;
using Grpc3.Services;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// 📖 Enable the Application Insight Service in your application
// 1. If you configured the connection string in the appsettings.json,
// you can use a single line code to enable the Application Insights service in your program:
// builder.Services.AddApplicationInsightsTelemetry();
// 2. If you want config by environment please use the following code:
var appInsConnStr = Environment.GetEnvironmentVariable("APPLICATION_INSIGHTS_CONNECTION_STRING") ?? ""; 
var appInsOptions = new ApplicationInsightsServiceOptions { ConnectionString = appInsConnStr };
builder.Services.AddApplicationInsightsTelemetry(options:appInsOptions);

// 📖 Config the service/module name which will show in the application map. 
builder.Services.AddSingleton<ITelemetryInitializer>(new CustomTelemetryInitializer("Grpc3"));

// Get ASP .Net Core Environment
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var isDevelopment = environment == Environments.Development;

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
if (System.Runtime.InteropServices.RuntimeInformation.OSDescription.Contains("Darwin") && isDevelopment) {
    builder.WebHost.ConfigureKestrel(options =>
    {
        // Setup a HTTP/2 endpoint without TLS.
        options.ListenLocalhost(5253, o => o.Protocols = HttpProtocols.Http2);
    });
}

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<Serv3DemoService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();