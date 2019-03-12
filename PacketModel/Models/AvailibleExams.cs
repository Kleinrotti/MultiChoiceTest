using PacketModel.Enums;
using System;
using System.Collections.Generic;

namespace PacketModel.Models
{
    [Serializable]
    public class AvailibleExams : IPacket
    {
        public List<string> ExamNames { get; set; }
        public HandlerOperator Operator { get; set; }

        public AvailibleExams(HandlerOperator op, List<string> examnames)
        {
            Operator = op;
            ExamNames = examnames;
        }
    }
}