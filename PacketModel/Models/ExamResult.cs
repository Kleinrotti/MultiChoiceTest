using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketModel.Models
{
    [Serializable]
    public class ExamResult
    {
        public int[] CorrectAnswerIds { get; set; }
        public int[] SkippedAnswerIds { get; set; }
        public int[] WrongAnswerIds { get; set; }
    }
}
