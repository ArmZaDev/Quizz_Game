using System;
using Quizz_Game.Model;
using Quizz_Game.Content;
using System.Drawing;
using Newtonsoft.Json;
using Colorful;

using static System.Console;
using Console = Colorful.Console;

namespace Quizz_Game
{
    public class Registration:Encryptor
    {
        private const string pathEncrypt = @".\file\Database.json.aes"; //Path Database Json File
        private const string passWord = "1234567891234567"; //Key Encrypt And Decrypt Database

        public void Registry() 
        {
            Clear();
            
            Game game = new Game();
            Player player = new Player();
            StyleSheet styleSheet = new StyleSheet(Color.LightGray);

            styleSheet.AddStyle("SIGN UP", Color.Orange);
            styleSheet.AddStyle("PlayerName:", Color.LemonChiffon);

            Console.WriteLineStyled("\n\n\t\t\t\t\t     ------------| SIGN UP |------------", styleSheet);
            Console.WriteStyled("\n\t Enter your player name: ", styleSheet);
            string playerLogin = Console.ReadLine(); //Enter New Player Name

            //Decrypt Database 
            string decryptedData = FileDecryptJson(pathEncrypt, passWord);
            Database database = JsonConvert.DeserializeObject<Database>(decryptedData);

            //Search Player Name In Database
            foreach (Player pName in database.Players)
            {
                if (playerLogin == pName.PlayerName) { player = pName; }
            }

            // Player same In Database
            if (playerLogin != player.PlayerName)
            {
                player.PlayerName = playerLogin;
                database.Players.Add(player);

                //Decrypt Data Player And Update New Player
                FileEncryptJson(pathEncrypt, passWord, database);

                Console.WriteLine("\n\tRegistration successful!");
                Console.WriteLine($"\t Welcome to Quiz Game [{player.PlayerName}]");
                WriteLine("\tPress any key..."); ReadKey();
                game.QuizMenu(player); // Link to QuizMenu 
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
