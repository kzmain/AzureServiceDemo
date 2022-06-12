using System.Net;
using Grpc.Net.Client;

namespace AzureClassLibrary.Grpc.Channel;

public class GrpcChannelDemoFactory: GrpcChannelFactory
{
    public override GrpcChannel GetChannel(string address)
    {
        var httpClientHandler = new HttpClientHandler();
        var channel = GrpcChannel.ForAddress(
            address,
            new GrpcChannelOptions
            {
                HttpClient = new HttpClient(httpClientHandler){DefaultRequestVersion = HttpVersion.Version20}
            }
        );
        return channel;
    }
}