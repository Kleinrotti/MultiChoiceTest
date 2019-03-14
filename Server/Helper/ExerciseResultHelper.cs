using PacketModel.Models;
using Server.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Helper
{
    class ExerciseResultHelper
    {
        public static int CorrectAnswers { get; private set; }
        public static int ExistingAnswers { get; private set; }
        public static string ProcessResult(List<DefaultAnswer> answers, List<DefaultExercise> exercises)
        {
            int correct = 0;
            int amount = exercises.Count;
            
            foreach(var v in answers)
            {
                foreach(var e in exercises)
                {
                    if (e.ID == v.ID)
                    {
                        if(v.ResultIndex == e.ResultIndex)
                        {
                            correct++;
                        }
                    }
                }
            }
            CorrectAnswers = correct;
            ExistingAnswers = amount;
            return "You have reached " + correct + " out of " + amount + " points";
        }
    }
}
