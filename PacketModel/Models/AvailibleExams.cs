using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketModel.Models
{
    [Serializable]
    public class AvailibleExams
    {
        public List<string> ExamNames { get; set; }

        public AvailibleExams(List<string> examnames)
        {
            ExamNames = examnames;
        }
    }
}
