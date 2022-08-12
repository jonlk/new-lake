namespace NewLake.GrpcGenerator.Services
{
    public interface IBulkPacketGenerator
    {
        MessagePacket BuildMessagePacket(int packetId);
    }
}