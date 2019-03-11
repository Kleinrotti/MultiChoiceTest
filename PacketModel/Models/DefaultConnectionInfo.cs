using PacketModel.Enums;
using System;

namespace PacketModel.Models
{
    [Serializable]
    public class DefaultConnectionInfo
    {
        public string Message { get; set; }
        public ConnectionState State { get; set; }
    }
}