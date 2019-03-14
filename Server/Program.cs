using PacketModel.Connection;
using PacketModel.Connection.EventArguments;
using PacketModel.Enums;
using PacketModel.Models;
using Server.Connection;
using Server.Enums;
using Server.File;
using Server.Helper;
using Server.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class Program
    {
        private static IPAddress _ip = IPAddress.Parse("127.0.0.1");
        private static int _port = 15000;

        private static TCPServer _server;
        private static ExecuteSend del;
        private static CsvImport _csv;
        private static string _exampath = @"..\..\Exams\";
        private static Log _log;

        public delegate void ExecuteSend(TcpClient client, object data);

        /// <summary>
        /// Server Main method
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            Console.WindowHeight = 20;
            Console.WindowWidth = 80;

            _server = new TCPServer(_ip, _port);
            _server.ClientConnectionChanged += OnConnectionChanged;
            _server.PacketReceived += OnPacketReceived;
            _server.Start();

            _log = new Log();
            _log.ConsoleOutput = true;

            while(true);
        }

        #region Event Raiser
        /// <summary>
        /// If connection state has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnConnectionChanged(object sender, ClientConnectionChangedEventArgs e)
        {
            PacketHandler h = new PacketHandler();
            _log.AppendToLog(h.ProcessConnectionState(e), LogType.Connection);
        }

        /// <summary>
        /// If server received packets.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            PacketHandler h = new PacketHandler();
            del = new ExecuteSend(SendExamListToClient);
            del += SendExercisesToClient;
            del += ProcessAnswers;
            h.ProcessPacket(e, del);
        }

        #endregion

        /// <summary>
        /// Send exam list to client.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="filename"></param>
        private static void SendExamListToClient(TcpClient client, object data)
        {
            
            _log.AppendToLog("Sending exam list to client: " +
                             ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), LogType.Info);
            _csv = new CsvImport(_exampath);
            var list = _csv.GetExamNames();
            _server.SendPacket(client, new AvailibleExams(HandlerOperator.Client, list));
        }

        private static void ProcessAnswers(TcpClient client, object data)
        {
            _csv = new CsvImport(_exampath);
            var filename = _server.GetExamFileName(client);
            var list = _csv.GetExercises(filename);
            var result = ExerciseResultHelper.ProcessResult((List<DefaultAnswer>)data, list);
            var msg = new DefaultMessage(HandlerOperator.Client, Command.SendUserAnswers)
            {
                MessageString = result
            };
            _log.AppendToLog(result, LogType.Exam);
            _server.SendPacket(msg);
        }
        
        /// <summary>
        /// Send exercises to client, depending on selected exam.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="filename"></param>
        private static void SendExercisesToClient(TcpClient client, object data)
        {
            _server.SetExamFileName(client, (string)data);
            _log.AppendToLog("Sending exercises to client: " +
                             ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), LogType.Info);
            _csv = new CsvImport(_exampath);
            var ex = _csv.GetExercises((string)data);
            _server.SendPacket(client, ex);
        }
    }
}