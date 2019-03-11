using System.Collections.Generic;

namespace PacketModel.Models
{
    public class DefaultExercise
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public int ResultIndex { get; set; }
        public List<string> Answers { get; set; } = new List<string>();
    }
}