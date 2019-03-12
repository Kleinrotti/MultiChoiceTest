using PacketModel.Enums;
using System;
using System.Collections.Generic;

namespace PacketModel.Models
{
    [Serializable]
    public class DefaultExercise : IPacket
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public int ResultIndex { get; set; }
        public List<string> Answers { get; set; } = new List<string>();
        public HandlerOperator Operator { get; set; }

        public DefaultExercise(HandlerOperator op)
        {
            Operator = op;
        }
    }
}