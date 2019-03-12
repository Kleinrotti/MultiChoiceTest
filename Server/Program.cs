using PacketModel.Connection;
using PacketModel.Connection.EventArguments;
using PacketModel.Models;
using Server.Connection;
using Server.File;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            _server = new TCPServer(IPAddress.Parse("127.0.0.1"), 15000);
            _server.ClientConnectionChanged += OnConnectionChanged;
            _server.PacketReceived += OnPacketReceived;
            _server.Start();
            GetCsv();
            s = new ExecuteSend(SendListToClient);
            Console.ReadKey();
            //server.SendPacket()
        }
        static TCPServer _server;
        private static List<DefaultExercise> _exercises = new List<DefaultExercise>();
        static ExecuteSend s;

        private static void GetCsv()
        {
            CsvImport i = new CsvImport(@"..\..\Exams\");
            //_exercises = i.GetExercises();
            var vb = i.GetExamNames();
        }

        private static void OnConnectionChanged(object sender, ClientConnectionChangedEventArgs e)
        {
            PacketHandler h = new PacketHandler();
            h.ProcessConnectionState(e);
        }
        public delegate void ExecuteSend(TcpClient client);

        private static void SendListToClient(TcpClient client)
        {
            _server.SendPacket(client, _exercises);   
        }
        private static void OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            PacketHandler h = new PacketHandler();
            h.ProcessPacket(e.Packet, s);
        }
    }
}