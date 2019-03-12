using PacketModel.Connection.EventArguments;
using PacketModel.Enums;
using PacketModel.Models;
using System;
using System.Collections;
using System.Collections.Generic;

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
            try
            {
                if (IsList(_packet))
                {
                    //If client received exercise list from server
                    if (type == typeof(List<DefaultExercise>))
                    {
                        var v = _packet as List<DefaultExercise>;
                        Console.WriteLine("Client received exercises");
                        del[1].DynamicInvoke(v);
                        return;
                    }
                }
                //Packet needs to be handled by client
                if (((IPacket)_packet).Operator == HandlerOperator.Client)
                {
                    //If client received examnames list from server
                    if (type == typeof(AvailibleExams))
                    {
                        var v = _packet as AvailibleExams;
                        Console.WriteLine("Client received exam names " + v.ExamNames[0]);
                        del[0].DynamicInvoke(v.ExamNames);
                    }
                }
                else
                {
                    if (type == typeof(DefaultMessage))
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
                    //Server will check the answers from client
                    else if (type == typeof(List<DefaultAnswer>))
                    {
                        var v = _packet as List<DefaultAnswer>;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool IsList(object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public string ProcessConnectionState(ClientConnectionChangedEventArgs e)
        {
            string info;
            if (e.IsConnected)
                info = "Client: " + e.IP + " connected";
            else
                info = "Client: " + e.IP + " disconnected";
            return info;
        }

        private bool IsEmpty()
        {
            return false;
        }
    }
}