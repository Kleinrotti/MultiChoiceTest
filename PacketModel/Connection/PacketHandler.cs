using PacketModel.Connection.EventArguments;
using PacketModel.Models;
using System;

namespace PacketModel.Connection
{
    /// <summary>
    /// Packet handling for received object
    /// </summary>
    public class PacketHandler
    {
        private object _packet;

        /// <summary>
        /// Decide how packets will be processed
        /// </summary>
        public void ProcessPacket(object packet)
        {
            _packet = packet;
            var type = _packet.GetType();
            if (type == typeof(DefaultConnectionInfo))
            {
                var v = _packet as DefaultConnectionInfo;
                Console.WriteLine("Nachricht: " + v.Message);
            }
            else if (type == typeof(DefaultExercise))
            {
            }
        }

        public void ProcessConnectionState(ClientConnectionChangedEventArgs e)
        {
            string info;
            if (e.IsConnected)
                info = "Client: " + e.IP + " ist verbunden";
            else
                info = "Client: " + e.IP + " ist nicht mehr verbunden";
            Console.WriteLine(info);
        }

        private bool IsEmpty()
        {
            return false;
        }
    }
}