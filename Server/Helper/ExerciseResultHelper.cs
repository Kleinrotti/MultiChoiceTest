using PacketModel.Models;
using System;
using System.Collections.Generic;

namespace Server.Helper
{
    internal class ExerciseResultHelper
    {
        public int CorrectAnswers { get; private set; }
        public int ExistingAnswers { get; private set; }

        public string ProcessResult(List<DefaultAnswer> answers, List<DefaultExercise> exercises)
        {
            int correct = 0;
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
                            correct++;
                        }
                    }
                }
            }
            Console.WriteLine(correct + "   " + amount);
            CorrectAnswers = correct;
            return "You have reached " + correct + " out of " + amount + " points";
        }
    }
}