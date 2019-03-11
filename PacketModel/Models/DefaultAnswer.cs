using System;

namespace PacketModel.Models
{
    [Serializable]
    public class DefaultAnswer
    {
        public int ID { get; set; }
        public int ResultIndex { get; set; }
    }
}