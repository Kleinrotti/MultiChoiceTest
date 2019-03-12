using Server.Connection;
using Server.Enums;
using System;

namespace Server.Logging
{
    internal class ExamLog : Log, IDisposable
    {
        private string _path = "examlog";
        public override string Path { get => _path; protected set => _path = value; }
        private Client _client;
        private int _questionId;
        private bool _answer;

        /// <summary>
        /// Ergebnis eines Exams vom Client ins Log schreiben
        /// </summary>
        /// <param name="client"></param>
        /// <param name="questionId"></param>
        /// <param name="answer"></param>
        public void AppendToLog(Client client, int questionId, bool answer)
        {
            _client = client;
            _questionId = questionId;
            _answer = answer;
            base.AppendToLog(ParseClient(), LogType.Exam);
        }

        private string ParseClient()
        {
            return _client.Ip.ToString() + ";" + _questionId + ";" + _answer;
        }

        public override void Dispose()
        {
            _path = null;
            _client = null;
            base.Dispose();
        }
    }
}