using System;
using Quizz_Game.Model;
using System.Drawing;
using Colorful;
using System.Linq;

using static System.Console;
using Console = Colorful.Console;

namespace Quizz_Game
{
    internal class Registration
    {
        private Game _game;
        private Database _database;
        public Registration(Database database, Game game)
        {
            _database = database;
            _game = game;
        }
        public void Registry() 
        {
            Clear();
            
            Player player = new Player();
            StyleSheet styleSheet = new StyleSheet(Color.LightGray);

            styleSheet.AddStyle("SIGN UP", Color.Orange);
            styleSheet.AddStyle("PlayerName:", Color.LemonChiffon);

            Console.WriteLineStyled("\n\n\t\t\t\t\t     ------------| SIGN UP |------------", styleSheet);
            Console.WriteStyled("\n\t Enter your player name: ", styleSheet);
            string inputPlayerName = Console.ReadLine(); //Enter New Player Name

            // Player same In Database
            if (!_database.Players.Any(a=>a.PlayerName.ToLower()== inputPlayerName.ToLower()))
            {
                player.PlayerName = inputPlayerName;
                _database.Players.Add(player);

                Console.WriteLine("\n\tRegistration successful!");
                Console.WriteLine($"\t Welcome to Quiz Game [{player.PlayerName}]");
                WriteLine("\tPress any key..."); ReadKey();

                _game.QuizMenu(player);
            }

            // Player not same In Database
            else
            {

                Clear();

                string playerExists = "The player database already exists. Registration is not allowed.";

                styleSheet.AddStyle("SIGN UP", Color.Red);
                styleSheet.AddStyle(playerExists, Color.Red);

                String loginError = "\n\n\t\t\t\t\t     ------------| SIGN UP |------------";
                Console.WriteLineStyled(loginError, styleSheet);
                Console.WriteLineStyled($"\n\t{ playerExists}", styleSheet);
                WriteLine("\tPress any key..."); ReadKey();

                Registry();//Return Registry Page
            }
        }





    }
}
