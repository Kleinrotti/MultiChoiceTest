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

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultExercise"/> class.
        /// </summary>
        /// <param name="op"></param>
        public DefaultExercise(HandlerOperator op)
        {
            Operator = op;
        }
    }
}