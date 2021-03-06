using Google.Protobuf.WellKnownTypes;
using NewLake.Core.GrpcProto.Services;

namespace NewLake.GrpcServer.Sender.Services
{
    public class BulkPacketGenerator : IBulkPacketGenerator
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
                MessageData = Guid.NewGuid().ToString(),
                MessageTime = Timestamp.FromDateTime(DateTime.UtcNow)
            });

            pkt.InfoMessages.Add(new InfoMessage
            {
                MessageId = messageId + 1,
                MessageData = Guid.NewGuid().ToString(),
                MessageTime = Timestamp.FromDateTime(DateTime.UtcNow)
            });

            pkt.InfoMessages.Add(new InfoMessage
            {
                MessageId = messageId + 2,
                MessageData = Guid.NewGuid().ToString(),
                MessageTime = Timestamp.FromDateTime(DateTime.UtcNow)
            });

            return pkt;
        }
    }
}
