using System;
using System.Collections.Generic;

namespace PacketModel.Models
{
    [Serializable]
    public class AvailibleExams
    {
        public List<string> ExamNames { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AvailibleExams"/> class.
        /// </summary>
        /// <param name="op"></param>
        /// <param name="examnames"></param>
        public AvailibleExams(List<string> examnames)
        {
            ExamNames = examnames;
        }
    }
}