using ConsoleTables;
using Quizz_Game.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizz_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Play();

            Console.ReadKey();  
        }
    }
}
