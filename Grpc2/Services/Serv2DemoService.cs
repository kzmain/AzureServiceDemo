using System.Threading.Tasks;
using AzureClassLibrary.Cognitive;
using AzureClassLibrary.Database;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc2;
using GrpcService;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Logging;
namespace Grpc2.Services;

public class Serv2DemoService : Serv2Demo.Serv2DemoBase
{
    private readonly ILogger<Serv2DemoService> _logger;
    private readonly TelemetryClient _telemetryClient;
    
    public Serv2DemoService(ILogger<Serv2DemoService> logger, TelemetryClient telemetryClient)
    {
        _logger = logger;
        _telemetryClient = telemetryClient;
    }

    public override Task<Serv2Reply> Demo(Serv2Request request, ServerCallContext context)
    {
        var translator = new Translator();
        translator.Demo();
        var mySqlServer = new MySqlServer();
        
        //-------------------------------------------------------------------------------------------------------------
        // MySQL/Redis/Cosmos DB/IoT etc. not connect by http(s) protocol (e.g. DB Protocol/TCP Mode)
        var startTime = DateTime.Now;
        var timer = System.Diagnostics.Stopwatch.StartNew();
        var success = true;
        try
        {
            // making dependency call
            mySqlServer.Demo();
        }
        catch
        {
            success = false;
        }
        finally
        {
            timer.Stop();
            this._telemetryClient.TrackDependency(
                "MySqlServer", 
                "MySqlDemo", 
                "MySqlDemoData", 
                startTime, 
                timer.Elapsed,
                success);
        }
        //-------------------------------------------------------------------------------------------------------------

        return Task.FromResult(new Serv2Reply
        {
            ReturnDescription = "Grpc Ser2.Demo()"
        }) ;
    }

    public override Task<Serv2Reply> Return401(Serv2Request request, ServerCallContext context)
    {
        var metadata = new Metadata
        {
            { "Message", "Grpc Ser2.Return401()" }
        };
        throw new RpcException(new Status(StatusCode.Unauthenticated, "Unauthenticated"), metadata);
    }

    public override Task<Serv2Reply> Return403(Serv2Request request, ServerCallContext context)
    {
        var metadata = new Metadata
        {
            { "Message", "Grpc Ser2.Return403()" }
        };
        throw new RpcException(new Status(StatusCode.PermissionDenied, "Permission denied"), metadata);
    }

    public override Task<Serv2Reply> Return404(Serv2Request request, ServerCallContext context)
    {
        var metadata = new Metadata
        {
            { "Message", "Grpc Ser2.Return404()" }
        };
        throw new RpcException(new Status(StatusCode.NotFound, "Not Found"), metadata);
    }
}