using PacketModel.Connection.EventArguments;
using PacketModel.Enums;
using PacketModel.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Server.Handler
{
    internal class PacketHandler
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
            try
            {
                //Server will check the received answers from client
                if (type == typeof(List<DefaultAnswer>))
                {
                    var v = _packet as List<DefaultAnswer>;
                    del[2].DynamicInvoke(e.Sender, v);
                }
                else if (type == typeof(DefaultMessage))
                {
                    var obj = _packet as DefaultMessage;
                    switch (obj.ExecuteCommand)
                    {
                        case Command.SendExamList:
                            del[0].DynamicInvoke(e.Sender, "");
                            break;

                        case Command.SendExercises:
                            del[1].DynamicInvoke(e.Sender, obj.MessageString);
                            break;

                        default:
                            Console.WriteLine("Message: " + ((DefaultMessage)_packet).MessageString);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private bool IsList(object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public string ProcessConnectionState(ClientConnectionChangedEventArgs e)
        {
            string info;
            if (e.IsConnected)
                info = "Client: " + e.IP + " connected";
            else
                info = "Client: " + e.IP + " disconnected";
            return info;
        }
    }
}