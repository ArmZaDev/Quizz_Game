using Quizz_Game.Model;
using System;
using System.Linq;

namespace Quizz_Game
{
    internal class Table
    {
        private Database _database;
        public Table(Database database)
        {
            _database = database;
        }
        int tableWidth = 48;

        public void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        public void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                if (_database.Players.Any(a => Convert.ToString(a.Time) == column))
                {
                    row += AlignRight(column, width) + "|";
                }
                else
                {
                    row += AlignCentre(column, width) + "|";
                }
            }

            Console.WriteLine(row);
        }

        public void PrintTable(Database database)
        {
            string[] headers = { "Player", "Score", "Time(ms)" };

            PrintLine();
            PrintRow(headers);
            PrintLine();

            foreach (Player player in database.Players)
            {
                string[] row = new string[headers.Length];

                row[0] = player.PlayerName;
                row[1] = Convert.ToString($"{player.Score}/10");
                row[2] = Convert.ToString($"{player.Time}");
                //row[2] = AlignRight(Convert.ToString(player.Time), tableWidth / headers.Length);

                PrintRow(row);
                PrintLine();
            }
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        static string AlignRight(string text,int width)
        {
            return text.PadLeft(15);
            //return text;
            //return text.PadLeft(width + (width - text.Length));
        }
    }
}
