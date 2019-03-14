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

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultAnswer"/> class.
        /// </summary>
        /// <param name="op"></param>
        public DefaultAnswer(HandlerOperator op)
        {
            Operator = op;
        }
    }
}