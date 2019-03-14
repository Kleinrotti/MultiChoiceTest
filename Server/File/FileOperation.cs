using Server.Enums;
using Server.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Server.File
{
    internal class FileOperation
    {
        protected List<string> Lines { get; private set; }
        private static object _lockObject = new object();

        /// <summary>
        /// Reading a complete file line by line. Saved in List Lines
        /// </summary>
        /// <param name="path"></param>
        protected void ReadLinesFromFile(string path)
        {
            Lines = new List<string>();
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader r = new StreamReader(fs))
                    {
                        while (!r.EndOfStream)
                        {
                            Lines.Add(r.ReadLine());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (Log l = new Log())
                {
                    l.AppendToLog(ex.Message, LogType.Exception);
                }
            }
        }

        /// <summary>
        /// Get Exam Names as <see cref="String"/> List.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected List<string> GetExamNames(string path)
        {
            return System.IO.Directory.GetFiles(path).Select(System.IO.Path.GetFileNameWithoutExtension).ToList<string>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        protected void WriteLineToFile(string path, string content)
        {
            lock (_lockObject)
            {
                using (FileStream fs = System.IO.File.Open(path, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter w = new StreamWriter(fs))
                    {
                        w.WriteLine(content);
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="list"></param>
        protected void WriteListToFile(string path, List<string> list)
        {
            lock (_lockObject)
            {
                using (FileStream fs = System.IO.File.Open(path, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter w = new StreamWriter(fs))
                    {
                        foreach (var s in list)
                        {
                            w.WriteLine(s);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check if File Exists.
        /// </summary>
        /// <param name="path"></param>
        /// <returns><see cref="bool"/></returns>
        protected bool FileExists(string path)
        {
            return System.IO.File.Exists(path);
        }
    }
}