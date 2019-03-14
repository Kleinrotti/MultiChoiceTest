using PacketModel.Enums;
using System;

namespace PacketModel.Models
{
    [Serializable]
    public class DefaultMessage : IPacket
    {
        public string MessageString { get; set; }
        public HandlerOperator Operator { get; set; }
        public Command ExecuteCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMessage"/> class.
        /// </summary>
        /// <param name="op"></param>
        public DefaultMessage(HandlerOperator op)
        {
            Operator = op;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMessage"/> class.
        /// </summary>
        /// <param name="op"></param>
        /// <param name="command"></param>
        public DefaultMessage(HandlerOperator op, Command command)
        {
            Operator = op;
            ExecuteCommand = command;
        }
    }
}