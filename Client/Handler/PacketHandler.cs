using PacketModel.Connection.EventArguments;
using PacketModel.Enums;
using PacketModel.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Client.Handler
{
    internal class PacketHandler
    {
        private object _packet;

        /// <summary>
        /// Decide how packets will be processed by the client.
        /// </summary>
        public void ProcessPacket(PacketReceivedEventArgs e, Delegate response)
        {
            Delegate[] del = response.GetInvocationList();
            _packet = e.Packet;
            var type = _packet.GetType();
            try
            {
                //If client received exercise list from server
                if (type == typeof(List<DefaultExercise>))
                {
                    var v = _packet as List<DefaultExercise>;
                    del[1].DynamicInvoke(v);
                    return;
                }
                //If client received examnames list from server
                else if (type == typeof(AvailibleExams))
                {
                    var v = _packet as AvailibleExams;
                    del[0].DynamicInvoke(v.ExamNames);
                }
                else if(type == typeof(ExamResult))
                {
                    del[0].DynamicInvoke(_packet);
                }
                else if (type == typeof(DefaultMessage))
                {
                    var v = _packet as DefaultMessage;
                    switch (v.ExecuteCommand)
                    {

                        case Command.Undefined:
                            Console.WriteLine(v.MessageString);
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
        /// Return <see cref="object"/> parameter as <seealso cref="List{T}"/>.
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
    }
}