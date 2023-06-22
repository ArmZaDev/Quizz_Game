using System;
using System.Collections.Generic;
using Quizz_Game.Model;
using Quizz_Game.Content;
using Newtonsoft.Json;
using System.Drawing;
using Colorful;

using Console = Colorful.Console;

namespace Quizz_Game
{ 
    public class Quiz:Encryptor
    {

        private const string pathEncrypt = @".\file\Database.json.aes"; //Path Database Json File
        private const string passWord = "1234567891234567"; //Key Encrypt And Decrypt Database

        public void Start(Player player, bool rnd = false)
        {
            Player score = new Player();
            Quiz quiz = new Quiz();
            int countOfRightAnswers = 0;
            StyleSheet styleSheet = new StyleSheet(Color.LightGray);

            styleSheet.AddStyle("Enter", Color.LemonChiffon);
            styleSheet.AddStyle("Question", Color.LightBlue);
            styleSheet.AddStyle("Answers", Color.LightGreen);

            // Decrypt Data Player
            string decryptedData = FileDecryptJson(pathEncrypt, passWord);
            Database database = JsonConvert.DeserializeObject<Database>(decryptedData);

            Console.Clear();

            // Question Display
            foreach (var question in database.Quizz)
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

            Console.WriteLine($"{countOfRightAnswers}/{database.Quizz.Count} are right");

            if (!rnd) { LeaderBoard(player, countOfRightAnswers); }
            Console.WriteLine("Press any key..."); Console.ReadKey();
        }

        public void LeaderBoard(Player player, int persent)
        {
            Player score = new Player();
            // Decrypt Data Player
            string decryptedData = FileDecryptJson(pathEncrypt, passWord);
            Database database = JsonConvert.DeserializeObject<Database>(decryptedData);

            //เพิ่มคะแนนผู้เล่น
            foreach (var item in database.Players)
            {
                if (score.Score <= item.Score && player.PlayerName == item.PlayerName)
                {
                    score = item;
                    player = item;
                }
            }

            if (score.Score <= persent)
            {
                score.Score = persent;
            }

            //เข้ารหัสข้อมูลคะแนนผูเล่น
            FileEncryptJson(pathEncrypt, passWord, database);
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
