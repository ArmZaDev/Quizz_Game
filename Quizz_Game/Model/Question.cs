using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Quizz_Game
{
    public class Question
    {
        public string Quest { get; set; }
        public List<String> Answers { get; set; }
        public List<String> RightAnswers { get; set; }
    }
}
