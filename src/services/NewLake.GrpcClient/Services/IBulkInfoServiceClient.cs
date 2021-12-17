using NewLake.Core.GrpcProto.Services;

namespace NewLake.GrpcClient.Sender.Services
{
    public interface IBulkInfoServiceClient
    {
        MessagePacket BuildMessagePacket(int packetId);
    }
}