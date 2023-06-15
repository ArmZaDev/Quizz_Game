using Quizz_Game.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Quizz_Game.Games;

using static System.Console;
using Nancy.Json;

namespace Quizz_Game.Content
{
    public class Registration
    {

        public void Registry() 
        {
            Clear();
            Players player = new Players();
            Game game = new Game();
            List<Players> players = new List<Players>();

            const string path = "Users";
            DirectoryInfo dict = new DirectoryInfo(path);
            var Files = dict.GetFiles();
            foreach (var file in Files)
            {
                using (FileStream fso = new FileStream(file.FullName, FileMode.Open))
                {
                    players.Add(JsonSerializer.Deserialize<Players>(fso)); fso.Close();
                }

            }

            Console.WriteLine("\n\n\t\t\t\t\t     ------------| SIGN UP |------------");
            Console.Write("\n\t\t\t\t\t\t    PlayerName: ");
            String tempLogin = Console.ReadLine();

            if (tempLogin.Length == 0) 
            { tempLogin = Console.ReadLine(); }

            foreach (var item in players)
            {
                if (tempLogin == item.PlayerName) { player = item; }
            }

            if (tempLogin != player.PlayerName)
            {
                player.PlayerName = tempLogin;

                using (FileStream fs = new FileStream($"{path}\\{player.PlayerName}.json", FileMode.Append))
                {
                    JsonSerializer.Serialize(fs, player, new JsonSerializerOptions { WriteIndented = true }); fs.Close();
                }
                game.QuizMenu(player);
            }
            else
            {
                WriteLine("\n\t\t\t\t\t\t      Such user already exists!");

                WriteLine("Press any key..."); Console.ReadKey();
                Registry();
            }
        }
    }
}
