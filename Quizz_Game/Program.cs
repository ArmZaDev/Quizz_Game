using Newtonsoft.Json;
using Quizz_Game.Model;

namespace Quizz_Game
{
    internal class Program
    {
        const string pathEncrypt = @".\file\Database.json.aes"; //Path Database Json File
        const string passWord = "1234567891234567"; //Key Encrypt And Decrypt Database
        static void Main(string[] args)
        {
            string decryptedData = Encryptor.FileDecryptJson(pathEncrypt, passWord);
            Database database = JsonConvert.DeserializeObject<Database>(decryptedData);
            using (Game game = new Game(database, SaveDb))
            {
                game.Play();
            }

        }
        static void SaveDb(string cmd, Database db)
        {
            if (cmd.ToUpper() == "Y")
            {
                Encryptor.FileEncryptJson(pathEncrypt, passWord, db);
            }
        }
    }
}
