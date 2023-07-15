using System;
using Quizz_Game.Model;
using Colorful;
using System.Drawing;

using static System.Console;
using Console = Colorful.Console;

namespace Quizz_Game
{

    internal class Game : IDisposable
    {
        private string GameTitleArt = @"
 
  ______   __    __  ______  ________         ______    ______   __       __  ________ 
 /      \ |  \  |  \|      \|        \       /      \  /      \ |  \     /  \|        \
|  $$$$$$\| $$  | $$ \$$$$$$ \$$$$$$$$      |  $$$$$$\|  $$$$$$\| $$\   /  $$| $$$$$$$$
| $$  | $$| $$  | $$  | $$      /  $$       | $$ __\$$| $$__| $$| $$$\ /  $$$| $$__    
| $$  | $$| $$  | $$  | $$     /  $$        | $$|    \| $$    $$| $$$$\  $$$$| $$  \   
| $$ _| $$| $$  | $$  | $$    /  $$         | $$ \$$$$| $$$$$$$$| $$\$$ $$ $$| $$$$$   
| $$/ \ $$| $$__/ $$ _| $$_  /  $$___       | $$__| $$| $$  | $$| $$ \$$$| $$| $$_____ 
 \$$ $$ $$ \$$    $$|   $$ \|  $$    \       \$$    $$| $$  | $$| $$  \$ | $$| $$     \
  \$$$$$$\  \$$$$$$  \$$$$$$ \$$$$$$$$        \$$$$$$  \$$   \$$ \$$      \$$ \$$$$$$$$
      \$$$                                                                             
                                                                                       
                                                                                       
";

        private string GameTitle = "Quiz Game";

        private Database _database;
        Authorization auth;
        Registration regn;
        Quiz quiz;
        Table table;
        Action<string,Database> _action;
        public Game(Database database, Action<string, Database> action=null)
        {
            _database = database;
            auth = new Authorization(database, this);
            regn = new Registration(database, this);
            quiz = new Quiz(database);
            table = new Table(database);
            _action = action;
        }
        // Start Game
        public void Play()
        {
            StyleSheet styleSheet = new StyleSheet(Color.LightGray);
            styleSheet.AddStyle("Welcome to", Color.LightGreen);
            styleSheet.AddStyle(GameTitle, Color.Salmon);
            styleSheet.AddStyle(GameTitleArt, Color.OrangeRed);

            Console.WriteLine(GameTitleArt);
            Console.WriteLineStyled($"Welcome to { GameTitle }", styleSheet);
            WriteLine("Press any key...");
            ReadKey();

            StartMenu();
        }

        // ShowStartMenu
        private void ShowStartMenu()
        {
            string menu = "MENU";
            string auth = "Authorization";
            string regis = "Registration";
            StyleSheet styleSheet = new StyleSheet(Color.LightGray);

            styleSheet.AddStyle(menu, Color.Orange);
            styleSheet.AddStyle(auth, Color.LightGreen);
            styleSheet.AddStyle(regis, Color.LightBlue);
            styleSheet.AddStyle("Exit", Color.OrangeRed);
            styleSheet.AddStyle("Enter", Color.LemonChiffon);

            Clear();
            Console.WriteLineStyled($"\n\n\t\t\t\t\t     ------------| {menu} |------------", styleSheet);
            Console.WriteLineStyled($"\n\t\t\t\t\t          [1] - {auth}", styleSheet);
            Console.WriteLineStyled($"\t\t\t\t\t          [2] - {regis}", styleSheet);
            Console.WriteLineStyled("\t\t\t\t\t          [0] - Exit", styleSheet);

            Console.WriteLineStyled("\n\t\t\t\t\t     --------------------------------", styleSheet);
            Console.WriteStyled("\t\t\t\t\t    Enter: ", styleSheet);
        }

        // ShowQuizMenu
        private void ShowQuizMenu()
        {
            string quizMenu = "QUIZ MENU";
            string listQuiz = "List of quizzes";
            string btMenu = "Back to main menu";
            StyleSheet styleSheet = new StyleSheet(Color.LightGray);

            styleSheet.AddStyle(quizMenu, Color.Orange);
            styleSheet.AddStyle(listQuiz, Color.LightGreen);
            styleSheet.AddStyle(btMenu, Color.LightBlue);
            styleSheet.AddStyle("Enter", Color.LemonChiffon);

            Clear();
            Console.WriteLineStyled($"\n\n\t\t\t\t\t    -----------| {quizMenu} |-----------", styleSheet);
            Console.WriteLineStyled($"\n\t\t\t\t\t\t     [1] - {listQuiz}", styleSheet);
            Console.WriteLineStyled($"\t\t\t\t\t\t     [0] - {btMenu}", styleSheet);

            Console.WriteLineStyled("\n\t\t\t\t\t    -----------------------------------", styleSheet);
            Console.WriteStyled("\t\t\t\t\t       Enter: ", styleSheet);
        }

        // StartMenu
        public void StartMenu()
        {
          
            bool EXT = true;
            while (EXT != false)
            {
                ShowStartMenu(); 
                String choice = ReadLine();
                if (choice == "0") { EXT = false; }
                else if (choice == "1") {   auth.Logistry(); }
                else if (choice == "2") {   regn.Registry(); }
            }
        }

        // QuizMenu
        public void QuizMenu(Player player)
        {
            bool EXT = true;
            string quizz = "QUIZ";
            string start = "Start quiz";
            string show = "Show Score";
            StyleSheet styleSheet = new StyleSheet(Color.LightGray);

            styleSheet.AddStyle(quizz, Color.Orange);
            styleSheet.AddStyle(start, Color.LightGreen);
            styleSheet.AddStyle(show, Color.Salmon);
            styleSheet.AddStyle("Enter", Color.LemonChiffon);

            while (EXT != false)
            {
                ShowQuizMenu();
                String choice = ReadLine();
                switch (choice)
                {
                    case "0": { EXT = false; } break;
                    case "1":
                        {

                            Clear();
                            Console.WriteLineStyled($"\n\n\t\t\t\t\t    ------------| {quizz} |------------", styleSheet);
                            Console.WriteLineStyled($"\n\t\t\t\t\t\t  [1] - {start}", styleSheet);
                            Console.WriteLineStyled($"\t\t\t\t\t\t  [2] - {show}", styleSheet);
                            Console.WriteLineStyled($"\n\t\t\t\t\t    --------------------------------", styleSheet);
                            Console.WriteStyled("\t\t\t\t\t    Enter: ", styleSheet);
                            String chs = ReadLine();

                            // Print table
                            if (chs == "1") { quiz.Start(player); }
                            else if (chs == "2")
                            {
                                Clear();

                                // Score Table
                                WriteLine($"\n\n\t\t   Score Player");
                                table.PrintTable(_database);
                                WriteLine("Press any key..."); ReadKey();
                            }
                            break;
                        }
                }
            }
        }

        public void Dispose()
        {
            if (_action != null)
            {
                Console.WriteLine("Save game? Y/N");
                string command = Console.ReadLine();
                _action(command, _database);

            }
            GC.SuppressFinalize(this);
            //  Environment.Exit(0);
        }
        // Protected implementation of Dispose pattern.

    }
}
