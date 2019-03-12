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

        public DefaultMessage(HandlerOperator op)
        {
            Operator = op;
        }

        public DefaultMessage(HandlerOperator op, Command command)
        {
            Operator = op;
            ExecuteCommand = command;
        }
    }
}