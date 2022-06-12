using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc1;
using GrpcService;
using Microsoft.Extensions.Logging;
using AzureClassLibrary.Database;
using AzureClassLibrary.Storage;

namespace Grpc1.Services;

public class Serv1DemoService : Serv1Demo.Serv1DemoBase
{
    private readonly ILogger<Serv1DemoService> _logger;

    public Serv1DemoService(ILogger<Serv1DemoService> logger)
    {
        _logger = logger;
    }

    public override Task<Serv1Reply> Demo(Serv1Request request, ServerCallContext context)
    {
        var sqlserver = new SqlServer();
        sqlserver.Demo();
        var blob = new Blob();
        blob.Demo();
        return Task.FromResult(new Serv1Reply
        {
            ReturnDescription = "Grpc Ser1.Demo()"
        });
    }
    
}