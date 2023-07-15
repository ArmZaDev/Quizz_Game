using System;
using Colorful;
using System.Drawing;
using Quizz_Game.Model;
using System.Linq;

using Console = Colorful.Console;
using static System.Console;

namespace Quizz_Game
{
    internal class Authorization
    {
        private Game _game;
        private Database _database;
        public Authorization(Database database, Game game)
        {
            _database = database;
            _game = game;
        }

        public void Logistry()
        {
            Console.Clear();
          
            Player player = new Player();
            StyleSheet styleSheet = new StyleSheet(Color.LightGray);

            styleSheet.AddStyle("SIGN IN",Color.Orange);
            styleSheet.AddStyle("Login", Color.LemonChiffon);

            Console.WriteLineStyled("\n\n\t\t\t\t\t     ------------| SIGN IN |------------", styleSheet);
            Console.WriteStyled("\n\t\t\t\t\t\t    Login: ", styleSheet);
            string inputPlayerName = Console.ReadLine(); // Enter Current Player Name

            // Player same In Database
            if (_database.Players.Any(a => a.PlayerName.ToLower() == inputPlayerName.ToLower()))
            {
                player.PlayerName = inputPlayerName;
                Console.WriteLine("\n\tLogin successful!");
                Console.WriteLine($"\tWelcome back [{player.PlayerName}] to Quiz Game");
                WriteLine("\tPress any key..."); ReadKey();
                _game.QuizMenu(player);
            }

            // Player not same In Database
            else
            {
                Clear();

                string playerExist = "Invalid player name. Please try again.";

                styleSheet.AddStyle("SIGN IN", Color.Red);
                styleSheet.AddStyle(playerExist, Color.Red);
                String loginError = "\n\n\t\t\t\t\t     ------------| SIGN IN |------------";

                Console.WriteLineStyled(loginError, styleSheet);
                Console.WriteLineStyled($"\t{playerExist}", styleSheet);
                WriteLine("\tPress any key..."); ReadKey();

                Logistry(); //Return to Logistry Page
            }
        }

        






    }
}
