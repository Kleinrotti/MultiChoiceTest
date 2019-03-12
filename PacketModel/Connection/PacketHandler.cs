using PacketModel.Connection.EventArguments;
using PacketModel.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

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
        public void ProcessPacket(PacketReceivedEventArgs e, Delegate response)
        {
            Delegate[] del = response.GetInvocationList();
            _packet = e.Packet;
            var type = _packet.GetType();
            //If server received defaultconnectioninfo from client
            if (type == typeof(DefaultConnectionInfo))
            {
                var v = _packet as DefaultConnectionInfo;
                Console.WriteLine("Nachricht: " + v.Message);
                if (v.IsExamSelectionFromClient)
                {
                    del[1].DynamicInvoke(e.Sender,"");
                }
                del[0].DynamicInvoke(e.Sender,"");
                
            }
            //If client received exercise list from server
            else if (type == typeof(List<DefaultExercise>))
            {
                var v = _packet as List<DefaultExercise>;
                Console.WriteLine("Client received Exercises");
            }
            //If client received examnames list from server
            else if(type == typeof(AvailibleExams))
            {
                var v = _packet as AvailibleExams;
                Console.WriteLine("Client received exam names" + v.ExamNames[0]);
            }
            //If server received answers from client
            else if(type == typeof(List<DefaultAnswer>))
            {
                var v = _packet as List<DefaultAnswer>;
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