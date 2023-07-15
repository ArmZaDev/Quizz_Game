using System;
using System.Collections.Generic;

namespace Quizz_Game.Model
{
    public class Question
    {
        public string Quest { get; set; }
        public List<String> Answers { get; set; }
        public List<String> RightAnswers { get; set; }
    }

    public class Player
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public double Time { get; set; }
    }


    public class Database
    {
        public List<Player> Players { get; set; }
        public List<Question> Quizz { get; set; }
    }
}
