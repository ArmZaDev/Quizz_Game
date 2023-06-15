using Quizz_Game.Games;
using Quizz_Game.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using static System.Console;

namespace Quizz_Game.Content
{
    public class Authorization
    {
        public void Logistry()
        {
            Console.Clear();
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

            Console.WriteLine("\n\n\t\t\t\t\t     ------------| SIGN IN |------------");
            Console.Write("\n\t\t\t\t\t\t    Login: ");
            String tempLogin = Console.ReadLine();

            foreach (var item in players)
            {
                if (tempLogin == item.PlayerName) { player = item; }
            }

            if (tempLogin == player.PlayerName)
            {
                game.QuizMenu(player);
            }
            else
            {
                WriteLine("\n\t\t\t\t\t\t      Login not exist!");

                WriteLine("Press any key..."); Console.ReadKey();
                Logistry();
            }
        }
    }
}
