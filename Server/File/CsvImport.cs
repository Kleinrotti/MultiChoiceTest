using PacketModel.Enums;
using PacketModel.Models;
using System;
using System.Collections.Generic;

namespace Server.File
{
    /// <summary>
    /// Import Csv files to object list
    /// </summary>
    internal sealed class CsvImport : FileOperation
    {
        private string _path;
        public List<DefaultExercise> Exercises { get; private set; }
        public List<string> ExamNames { get; private set; }

        /// <summary>
        /// Path without filename
        /// </summary>
        /// <param name="path"></param>
        public CsvImport(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Get Exam Names as <see cref="string"/> list.
        /// </summary>
        /// <returns></returns>
        public List<string> GetExamNames()
        {
            var v = GetExamNames(_path);
            ExamNames = v;
            return v;
        }

        /// <summary>
        /// Get Exercises as <see cref="DefaultExercise"/> List.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<DefaultExercise> GetExercises(string filename)
        {
            ReadLinesFromFile(System.IO.Path.Combine(_path, filename + ".csv"));
            var v = new List<DefaultExercise>();
            foreach (string s in Lines)
            {
                v.Add(GetExercise(s));
            }
            return v;
        }

        /// <summary>
        /// Get Exercise as <see cref="DefaultExercise"/>.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private DefaultExercise GetExercise(string line)
        {
            var v = new DefaultExercise(HandlerOperator.Client);
            string[] a = line.Split(';');
            v.ID = Convert.ToInt32(a[0]);
            v.Question = a[1];
            v.ResultIndex = Convert.ToInt32(a[2]);
            var l = new List<string>();
            for (int i = 3; i < a.Length; i++)
            {
                l.Add(a[i]);
            }
            v.Answers = l;
            return v;
        }
    }
}