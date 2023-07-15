using System;
using System.Collections.Generic;
using Quizz_Game.Model;
using System.Drawing;
using Colorful;
using System.Linq;

using Console = Colorful.Console;

namespace Quizz_Game
{ 
    internal class Quiz
    {
        private Database _database;
        private DateTime startTime;
        private DateTime endTime;
        public Quiz(Database database)
        {
            _database = database;
        }
        

        public void Start(Player player, bool rnd = false)
        {
            int countOfRightAnswers = 0;
            startTime = DateTime.Now;// Start Time

            StyleSheet styleSheet = new StyleSheet(Color.LightGray);

            styleSheet.AddStyle("Enter", Color.LemonChiffon);
            styleSheet.AddStyle("Question", Color.LightBlue);
            styleSheet.AddStyle("Answers", Color.LightGreen);

            Console.Clear();

            // Question Display
            foreach (var question in _database.Quizz)
            {
                bool RIGHT = true;
                List<String> userAnswers = new List<string>();

                Console.WriteLineStyled("\n------------------------------------------------------------------------------------------------------------------------", styleSheet);

                Console.WriteLineStyled($"\t\t\t\t\t\t   Question: {question.Quest}", styleSheet);
                Console.WriteLineStyled("\t\t\t\t\t\t   Answers:", styleSheet);
                for (int i = 0; i < question.Answers.Count; i++)
                {
                    Console.WriteLine($"\t\t\t\t\t\t   [{i}] {question.Answers[i]}");
                }

                Console.WriteLineStyled("\n------------------------------------------------------------------------------------------------------------------------", styleSheet);

                Console.WriteLineStyled("\t\t\t\t\t Write the number of correct answer", styleSheet);
                Console.WriteStyled("\n\t\tEnter: ", styleSheet);

                String ans = Console.ReadLine(); // Enter Answer
                userAnswers.Add(ans);

                foreach (var rightAnswers in userAnswers)
                {
                    // Check Right Answer
                    if (!question.RightAnswers.Contains(rightAnswers)) 
                    { 
                        RIGHT = false; 
                    }
                }
                if (RIGHT) { countOfRightAnswers++; }
            }

            endTime = DateTime.Now; // End Time
            TimeSpan totalTime = endTime - startTime; // Total Time

            double timeMilliseconds = totalTime.TotalMilliseconds / 1000.0;

            Console.WriteLine($"{countOfRightAnswers}/{_database.Quizz.Count} are right");
            Console.WriteLine($"tatalTime {timeMilliseconds} ms");


            if (!rnd) { LeaderBoard(player, countOfRightAnswers, timeMilliseconds); }
            Console.WriteLine("Press any key..."); Console.ReadKey();
        }

        public void LeaderBoard(Player player, int persent, double totalTime)
        {
            Player foundPlayer = _database.Players.FirstOrDefault(p => p.PlayerName == player.PlayerName);

            if (foundPlayer != null)
            {
                if (foundPlayer.Score < persent)
                {
                    foundPlayer.Score = persent;
                }

                foundPlayer.Time = totalTime;
            }
            else
            {
                player.Score = persent;
                player.Time = totalTime;
                _database.Players.Add(player);
            }
        }

        //ลองทำ
        /*foreach (Question question in database.Quizz)
        {
            Console.WriteLine(question.Quest);
            Console.WriteLine("Choose an answer:");

            for (int i = 0; i < question.Answers.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + question.Answers[i]);
            }

            Console.Write("Your answer: ");
            int selectedAnswerIndex = int.Parse(Console.ReadLine()) - 1;
            string selectedAnswer = question.Answers[selectedAnswerIndex];

            if (question.RightAnswers.Contains(selectedAnswer))
            {
                Console.WriteLine("Correct!");
                player.Score++;
            }
            else
            {
                Console.WriteLine("Wrong answer!");
            }
        }*/








    }
}
