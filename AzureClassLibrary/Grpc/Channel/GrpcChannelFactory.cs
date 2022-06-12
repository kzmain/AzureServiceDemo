using Grpc.Net.Client;

namespace AzureClassLibrary.Grpc.Channel;

public abstract class GrpcChannelFactory
{
    public abstract GrpcChannel GetChannel(string address);  
}