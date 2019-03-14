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

        /// <summary>
        /// Initializes a new instance of the <see cref="AvailibleExams"/> class.
        /// </summary>
        /// <param name="op"></param>
        /// <param name="examnames"></param>
        public AvailibleExams(HandlerOperator op, List<string> examnames)
        {
            Operator = op;
            ExamNames = examnames;
        }
    }
}