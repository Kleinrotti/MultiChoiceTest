using PacketModel.Models;
using System;
using System.Collections.Generic;

namespace Server.File
{
    internal sealed class CsvImport : FileOperation
    {
        private string _path;
        public List<DefaultExercise> Exercises { get; private set; }

        public CsvImport(string path)
        {
            _path = path;
        }

        public List<DefaultExercise> GetExercises()
        {
            ReadLinesFromFile(_path);
            var v = new List<DefaultExercise>();
            foreach (string s in Lines)
            {
                v.Add(GetExercise(s));
            }
            return v;
        }

        private DefaultExercise GetExercise(string line)
        {
            var v = new DefaultExercise();
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