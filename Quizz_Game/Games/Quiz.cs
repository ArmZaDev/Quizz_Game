using System;
using System.Collections.Generic;
using System.IO;
using Quizz_Game.Content;
using Quizz_Game.Model;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quizz_Game.Games
{ 
    public class Quiz
    {
        public string Name { get; set; }
        public List<Question> Quizz { get; set; }
        public Dictionary<string, int> Top20 { get; set; }

        public void Start(Players player, bool rnd = false)
        {
            Console.Clear();
            int countOfRightAnswers = 0;

            foreach (var question in Quizz)
            {
                bool RIGHT = true;
                List<String> userAnswers = new List<string>();

                Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------");

                Console.WriteLine($"\t\t\t\t\tQuestion: {question.Quest}");
                Console.WriteLine("\t\t\t\t\tAnswers:");
                for (int i = 0; i < question.Answers.Count; i++)
                {
                    Console.WriteLine($"\t\t\t\t\t[{i}] {question.Answers[i]}");
                }

                Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------");

                Console.WriteLine("\t\t\tWrite the number of correct answer");
                Console.Write("\n\t\tEnter: ");

                String ans = Console.ReadLine();
                userAnswers.Add(ans);

                foreach (var rightAnswers in userAnswers)
                {
                    if (!question.RightAnswers.Contains(rightAnswers)) 
                    { 
                        RIGHT = false; 
                    }
                }
                if (RIGHT) { countOfRightAnswers++; }
            }

            Console.WriteLine($"{countOfRightAnswers}/{Quizz.Count} are right");

            if (!rnd) { LeaderBoard(player, countOfRightAnswers); }
            Console.WriteLine("Press any key..."); Console.ReadKey();
        }

        public void LeaderBoard(Players player, int persent)
        {
            if (Top20.ContainsKey(player.PlayerName))
            {
                if (Top20[player.PlayerName] < persent)
                {
                    Top20.Remove(player.PlayerName);
                    Top20.Add(player.PlayerName, persent);
                }
            }
            else { Top20.Add(player.PlayerName, persent); }

            var sortedDict = from entry in Top20 orderby entry.Value ascending select entry;
            Top20 = sortedDict.ToDictionary(T => T.Key, T => T.Value);
            if (Top20.Count > 20) { Top20.Remove(Top20.Keys.Last()); }
            LoadToFile(this, false);
        }

        public void LoadToFile(Quiz _quiz, bool options = false)
        {
            String point = "point.txt",
                path = new FileInfo(point).FullName.Replace("point.txt", $"QuizGame_DB\\{Name}.json");
            if (options)
            {
                StreamWriter sw = new StreamWriter("point.txt", options);
                sw.WriteLine($"\n{Name}");
                sw.Close();
            }
            FileStream fsc = new FileStream(path, FileMode.Create);
            JsonSerializer.Serialize<Quiz>(fsc, _quiz, new JsonSerializerOptions { WriteIndented = true });
            fsc.Close();
        }
    }
}
