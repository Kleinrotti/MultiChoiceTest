using PacketModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Helper
{
    internal class ExerciseResultHelper
    {
        public int CorrectAnswers { get; private set; }
        public int ExistingAnswers { get; private set; }

        public ExamResult ProcessResult(List<DefaultAnswer> answers, List<DefaultExercise> exercises)
        {
            var res = new ExamResult();
            int[] skippedanswerids = new int[exercises.Count - answers.Count];
            int[] correctanswerids = new int[exercises.Count];
            int[] wronganswerids = new int[exercises.Count];
            int correct = 0;
            int wrong = 0;
            int skippedcount = 0;
            int amount = exercises.Count;
            ExistingAnswers = amount;
            foreach (var v in answers)
            {
                foreach (var e in exercises)
                {
                    if (e.ID == v.ID)
                    {
                        if (v.ResultIndex == e.ResultIndex)
                        {
                            correctanswerids[correct] = e.ID;
                            correct++;
                        }
                        else
                        {
                            wronganswerids[wrong] = e.ID;
                            wrong++;
                        }
                    }
                }
            }
            Array.Resize(ref correctanswerids, correct);
            Array.Resize(ref wronganswerids, wrong);
            int[] combined = correctanswerids.Concat(wronganswerids).ToArray();
            for (int i = 0; i < exercises.Count; i++)
            {
                if (!combined.Contains(exercises[i].ID))
                {
                    skippedanswerids[skippedcount] = exercises[i].ID;
                    skippedcount++;
                }
            }
            res.CorrectAnswerIds = correctanswerids;
            res.WrongAnswerIds = wronganswerids;
            res.SkippedAnswerIds = skippedanswerids;
            CorrectAnswers = correct;
            return res;
        }
    }
}