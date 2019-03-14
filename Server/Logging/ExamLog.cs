using Server.Enums;
using System;
using System.Net;

namespace Server.Logging
{
    internal class ExamLog : Log, IDisposable
    {
        private string _path = "examlog";
        public override string Path { get => _path; protected set => _path = value; }
        private IPAddress _address;
        private int _exerciseAmount;
        private int _correctAnswers;

        public void AppendToLog(IPAddress address, int exerciseamount, int correctanswers)
        {
            _address = address;
            _exerciseAmount = exerciseamount;
            _correctAnswers = correctanswers;
            base.AppendToLog(ParseClient(), LogType.Exam);
        }

        private string ParseClient()
        {
            return "Client: " + _address.ToString() + " has " + _correctAnswers + " out of " + _exerciseAmount + " points";
        }

        public override void Dispose()
        {
            _path = null;
            _address = null;
            base.Dispose();
        }
    }
}