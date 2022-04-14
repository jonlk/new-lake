using NewLake.Core.GrpcProto.Services;

namespace NewLake.GrpcServer.Sender.Services
{
    public interface IBulkPacketGenerator
    {
        MessagePacket BuildMessagePacket(int packetId);
    }
}