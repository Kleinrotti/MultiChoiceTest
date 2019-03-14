using PacketModel.Connection.EventArguments;
using PacketModel.Enums;
using PacketModel.Models;
using Server.Connection;
using Server.Enums;
using Server.File;
using Server.Handler;
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
        private static IPAddress _ip;
        private static int _port = 15000;

        private static TCPServer _server;
        private static ExecuteSend del;
        private static CsvImport _csv;
        private static string _exampath = @"..\..\Exams\";
        private static Log _log;

        private delegate void ExecuteSend(TcpClient client, object data);

        private static void Main(string[] args)
        {
            Console.WindowHeight = 20;
            Console.WindowWidth = 80;
        again:
            Console.WriteLine("---SET LOKAL IP:---");
            var ip = Console.ReadLine();
            if (ip == string.Empty)
                goto again;
            try
            {
                _ip = IPAddress.Parse(ip);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                goto again;
            }
            Console.WriteLine("---SET PATH TO EXAMS (leave empty for default):---");
            var exams = Console.ReadLine();
            if (exams != string.Empty)
                _exampath = exams;

            _server = new TCPServer(_ip, _port);
            _server.ClientConnectionChanged += OnConnectionChanged;
            _server.PacketReceived += OnPacketReceived;
            _server.Start();
            Console.WriteLine("Server started");
            _log = new Log();
            _log.ConsoleOutput = true;

            while (true) ;
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

        #endregion Event Raiser

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
            _server.SendPacket(client, new AvailibleExams(list));
        }

        /// <summary>
        /// Process received answers from client and sending the result back
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        private static void ProcessAnswers(TcpClient client, object data)
        {
            _csv = new CsvImport(_exampath);
            var filename = _server.GetExamFileName(client);
            var list = _csv.GetExercises(filename);
            var erh = new ExerciseResultHelper();
            var result = erh.ProcessResult((List<DefaultAnswer>)data, list);
            var msg = new DefaultMessage(Command.SendUserAnswers)
            {
                MessageString = result
            };
            var examlog = new ExamLog
            {
                ConsoleOutput = true
            };
            examlog.AppendToLog(_server.GetIpFromClient(client), erh.ExistingAnswers, erh.CorrectAnswers);
            _server.SendPacket(client, msg);
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