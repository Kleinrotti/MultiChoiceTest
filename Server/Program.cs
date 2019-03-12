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
        static TCPServer _server;
        static ExecuteSend s;
        static CsvImport _csv;
        static string _exampath = @"..\..\Exams\";
        private static void Main(string[] args)
        {
            _server = new TCPServer(IPAddress.Parse("127.0.0.1"), 15000);
            _server.ClientConnectionChanged += OnConnectionChanged;
            _server.PacketReceived += OnPacketReceived;
            _server.Start();
            
            Console.ReadKey();
        }

        private static void OnConnectionChanged(object sender, ClientConnectionChangedEventArgs e)
        {
            PacketHandler h = new PacketHandler();
            h.ProcessConnectionState(e);
        }
        /// <summary>
        /// Delegate for 
        /// </summary>
        /// <param name="client"></param>
        public delegate void ExecuteSend(TcpClient client, string filename = "");

        private static void SendExamListToClient(TcpClient client, string filename = "")
        {
            _csv = new CsvImport(_exampath);
            _csv.GetExamNames();
            _server.SendPacket(client, new AvailibleExams(_csv.ExamNames));   
        }

        private static void SendExerciseListToClient(TcpClient client, string filename = "")
        {
            Console.WriteLine("SUCCESSS");
            _csv = new CsvImport(_exampath);
            var ex = _csv.GetExercises("Exam1.csv");
            _server.SendPacket(client, ex);
        }
        
        private static void OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
           
            PacketHandler h = new PacketHandler();
            s = new ExecuteSend(SendExamListToClient);
            s += SendExerciseListToClient;
            h.ProcessPacket(e,s);
        }
    }
}