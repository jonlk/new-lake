using Google.Protobuf.WellKnownTypes;
using NewLake.Core.GrpcProto.Services;

namespace NewLake.GrpcClient.Sender.Services
{
    public class BulkInfoServiceClient : IBulkInfoServiceClient
    {
        public MessagePacket BuildMessagePacket(int packetId)
        {
            int messageId = 1;

            var pkt = new MessagePacket
            {
                PacketId = packetId,
                MessageStatus = MessageStatus.Success,
                Notes = "This is a test message packet"
            };

            pkt.InfoMessages.Add(new InfoMessage
            {
                MessageId = messageId,
                MessageData = "Test info message",
                MessageTime = Timestamp.FromDateTime(DateTime.UtcNow)
            });

            return pkt;
        }
    }
}
