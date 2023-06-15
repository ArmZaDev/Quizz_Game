using ConsoleTables;
using Quizz_Game.Content;
using Quizz_Game.Model;
using Quizz_Game.Games;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using static System.Console;

namespace Quizz_Game.Games
{
    public class Game
    {
        public void Play()
        {
            StartMenu();
            //Test();
        }

        private  void ShowStartMenu()
        {
            Clear();
            WriteLine("\n\n\t\t\t\t\t     ------------| MENU |------------");
            WriteLine("\n\t\t\t\t\t          [1] - Authorization");
            WriteLine("\t\t\t\t\t          [2] - Registration");
            WriteLine("\t\t\t\t\t          [0] - Exit");
            WriteLine("\n\t\t\t\t\t     --------------------------------");
            Write("\t\t\t\t\t    Enter: ");
        }

        private  void ShowQuizMenu()
        {
            Clear();
            WriteLine("\n\n\t\t\t\t\t    -----------| QUIZ MENU |-----------");
            WriteLine("\n\t\t\t\t\t\t     [1] - List of quizzes");
            WriteLine("\t\t\t\t\t\t     [0] - Back to main menu");
            WriteLine("\n\t\t\t\t\t    -----------------------------------");
            Write("\t\t\t\t\t       Enter: ");
        }

        public void StartMenu()
        {
            Authorization auth = new Authorization();
            Registration regn = new Registration();
            bool EXT = true;
            while (EXT != false)
            {
                ShowStartMenu();
                String choice = ReadLine();
                if (choice == "0") { EXT = false; Environment.Exit(0); }
                else if (choice == "1") { EXT = false; auth.Logistry(); }
                else if (choice == "2") { EXT = false; regn.Registry(); }
            }
        }

        public void QuizMenu(Players player)
        {
            bool EXT = true;
            while (EXT != false)
            {
                ShowQuizMenu();
                String choice = ReadLine();
                switch (choice)
                {
                    case "0": { StartMenu(); } break;
                    case "1":
                        {
                            Clear();
                            const string path = @"QuizGame_DB\QuizzGame.json";
                            FileStream fso = new FileStream(path, FileMode.Open);
                            Quiz quiz = JsonSerializer.Deserialize<Quiz>(fso);
                            fso.Close();

                            Clear();
                            WriteLine($"\n\n\t\t\t\t\t    ------------| QUIZ |------------");
                            WriteLine("\n\t\t\t\t\t\t  [1] - Start quiz");
                            WriteLine("\t\t\t\t\t\t  [2] - Show Score");
                            WriteLine($"\n\t\t\t\t\t    --------------------------------");
                            Write("\t\t\t\t\t    Enter: ");
                            String chs = ReadLine();

                            Table table = new Table();
                            if (chs == "1") { quiz.Start(player); }
                            else if (chs == "2")
                            {
                                Clear();
                                WriteLine($"\n\n\t\t\t\tScore Player");
                                table.PrintLine();
                                table.PrintRow("Player", "Score");
                                table.PrintLine();

                                foreach (var str in quiz.Top20)
                                {
                                    table.PrintRow(str.Key, Convert.ToString(str.Value));
                                    table.PrintLine();
                                    //Console.WriteLine($"\t\t\t\t\t\t      {str.Key} | {str.Value} / {quiz.Quizz.Count}");
                                }
                                WriteLine("Press any key..."); ReadKey();
                            }
                            break;
                        }
                }
            }
        }
        
        
    }
}
