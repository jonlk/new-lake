using NewLake.GrpcGenerator.Services;

namespace NewLake.GrpcGenerator.Services
{
    public interface IBulkPacketGenerator
    {
        MessagePacket BuildMessagePacket(int packetId);
    }
}