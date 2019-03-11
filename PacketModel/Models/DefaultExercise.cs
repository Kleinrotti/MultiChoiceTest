using System;
using System.Collections.Generic;

namespace PacketModel.Models
{
    [Serializable]
    public class DefaultExercise
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public List<string> Answers { get; set; } = new List<string>();
    }
}