using PacketModel.Connection;
using PacketModel.Connection.EventArguments;
using PacketModel.Enums;
using PacketModel.Models;
using Server.Connection;
using Server.Enums;
using Server.File;
using Server.Logging;
using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class Program
    {
        private static TCPServer _server;
        private static ExecuteSend del;
        private static CsvImport _csv;
        private static string _exampath = @"..\..\Exams\";
        private static Log _log;

        public delegate void ExecuteSend(TcpClient client, string filename);

        private static void Main(string[] args)
        {
            _server = new TCPServer(IPAddress.Parse("127.0.0.1"), 15000);
            _server.ClientConnectionChanged += OnConnectionChanged;
            _server.PacketReceived += OnPacketReceived;
            _server.Start();
            _log = new Log();
            _log.ConsoleOutput = true;
            Console.ReadKey();
        }

        private static void OnConnectionChanged(object sender, ClientConnectionChangedEventArgs e)
        {
            PacketHandler h = new PacketHandler();
            _log.AppendToLog(h.ProcessConnectionState(e), LogType.Connection);
        }

        private static void SendExamListToClient(TcpClient client, string filename)
        {
            _log.AppendToLog("Sending exam list to client: " +
                             ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), LogType.Info);
            _csv = new CsvImport(_exampath);
            var list = _csv.GetExamNames();
            _server.SendPacket(client, new AvailibleExams(HandlerOperator.Client, list));
        }

        private static void SendExercisesToClient(TcpClient client, string filename)
        {
            _log.AppendToLog("Sending exercises to client: " +
                             ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), LogType.Info);
            _csv = new CsvImport(_exampath);
            var ex = _csv.GetExercises(filename);
            _server.SendPacket(client, ex);
        }

        private static void OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            PacketHandler h = new PacketHandler();
            del = new ExecuteSend(SendExamListToClient);
            del += SendExercisesToClient;
            h.ProcessPacket(e, del);
        }
    }
}