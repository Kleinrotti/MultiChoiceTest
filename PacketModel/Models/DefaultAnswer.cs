using PacketModel.Enums;
using System;

namespace PacketModel.Models
{
    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public class DefaultAnswer : IPacket
    {
        public int ID { get; set; }
        public int ResultIndex { get; set; }
        public HandlerOperator Operator { get; set; }

        public DefaultAnswer(HandlerOperator op)
        {
            Operator = op;
        }
    }
}