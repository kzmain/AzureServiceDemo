using System.Threading.Tasks;
using AzureClassLibrary.Database;
using AzureClassLibrary.Storage;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc3;
using GrpcService;
using Microsoft.Extensions.Logging;
using FileShare = AzureClassLibrary.Storage.FileShare;

namespace Grpc3.Services;

public class Serv3DemoService : Serv3Demo.Serv3DemoBase
{
    private readonly ILogger<Serv3DemoService> _logger;

    public Serv3DemoService(ILogger<Serv3DemoService> logger)
    {
        _logger = logger;
    }

    public override Task<Serv3Reply> Demo(Serv3Request request, ServerCallContext context)
    {
        var cosmosDbServer = new CosmosDbServer();
        cosmosDbServer.Demo();
        var fileShare = new FileShare();
        fileShare.Demo();
        return Task.FromResult(new Serv3Reply
        {
            ReturnDescription = "Grpc Ser3.Demo()"
        });
    }

    public override Task<Serv3Reply> Return500(Serv3Request request, ServerCallContext context)
    {
        var metadata = new Metadata
        {
            { "Message", "Grpc Ser3.Return500()" }
        };
        throw new RpcException(new Status(StatusCode.Internal, "Internal Error"), metadata);
    }

    public override Task<Serv3Reply> Return503(Serv3Request request, ServerCallContext context)
    {
        var metadata = new Metadata
        {
            { "Message", "Grpc Ser3.Return503()" }
        };
        throw new RpcException(new Status(StatusCode.Unavailable, "Service Unavailable"), metadata);
    }
}