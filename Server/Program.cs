using PacketModel.Connection;
using PacketModel.Connection.EventArguments;
using Server.Connection;
using Server.File;
using System;
using System.Net;

namespace Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TCPServer server = new TCPServer(IPAddress.Parse("127.0.0.1"), 15000);
            server.ClientConnectionChanged += OnConnectionChanged;
            server.PacketReceived += OnPacketReceived;
            server.Start();
            CsvImport i = new CsvImport(@"..\..\Exams\Dummy Test.csv");
            i.GetExercises();
            Console.ReadKey();
        }

        private static void OnConnectionChanged(object sender, ClientConnectionChangedEventArgs e)
        {
            PacketHandler h = new PacketHandler();
            h.ProcessConnectionState(e);
        }

        private static void OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            PacketHandler h = new PacketHandler();
            h.ProcessPacket(e.Packet);
        }
    }
}