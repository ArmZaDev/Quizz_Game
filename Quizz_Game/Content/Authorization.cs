using System;
using Colorful;
using System.Drawing;
using Quizz_Game.Content;
using Quizz_Game.Model;
using Newtonsoft.Json;

using Console = Colorful.Console;
using static System.Console;

namespace Quizz_Game
{
    public class Authorization:Encryptor
    {
        private const string pathEncrypt = @".\file\Database.json.aes"; //Path Database Json File
        private const string passWord = "1234567891234567"; //Key Encrypt And Decrypt Database

        public void Logistry()
        {
            Console.Clear();
            Game game = new Game();
            Player player = new Player();
            StyleSheet styleSheet = new StyleSheet(Color.LightGray);

            styleSheet.AddStyle("SIGN IN",Color.Orange);
            styleSheet.AddStyle("Login", Color.LemonChiffon);

            Console.WriteLineStyled("\n\n\t\t\t\t\t     ------------| SIGN IN |------------", styleSheet);
            Console.WriteStyled("\n\t\t\t\t\t\t    Login: ", styleSheet);
            string playerLogin = Console.ReadLine(); // Enter Current Player Name

            // Decrypt Data Player
            string decryptedData = FileDecryptJson(pathEncrypt, passWord);
            Database database = JsonConvert.DeserializeObject<Database>(decryptedData);

            // Search Player Name In Database
            foreach (Player pName in database.Players)
            {
                if (playerLogin == pName.PlayerName) { player = pName; }
            }

            // Player same In Database
            if (playerLogin == player.PlayerName)
            {
                Console.WriteLine("\tLogin successful!");
                Console.WriteLine($"\tWelcome back [{player.PlayerName}] to Quiz Game");
                WriteLine("\tPress any key..."); ReadKey();
                game.QuizMenu(player);//Link To QuizMenu
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
