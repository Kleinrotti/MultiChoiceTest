using PacketModel.Enums;

namespace PacketModel
{
    public interface IPacket
    {
        HandlerOperator Operator { get; set; }
    }
}