using System;

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
