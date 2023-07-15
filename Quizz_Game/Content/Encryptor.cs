using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Newtonsoft.Json;
using Quizz_Game.Model;

namespace Quizz_Game
{

    internal sealed class Encryptor
    {
        [DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        public static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    // Fille the buffer with the generated data
                    rng.GetBytes(data);
                }
            }

            return data;
        }

        // FileEncrypt Database
        public void FileEncrypt(string inputFile, string password)
        {

            //generate random salt
            byte[] salt = GenerateRandomSalt();

            //create output file name
            using (FileStream fsCrypt = new FileStream(inputFile + ".aes", FileMode.Create))
            {
                //convert password string to byte arrray
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                //Set Rijndael symmetric encryption algorithm
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Padding = PaddingMode.PKCS7;

                    var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CFB;

                    fsCrypt.Write(salt, 0, salt.Length);

                    using (CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                        {
                            byte[] buffer = new byte[1048576];
                            int read;

                            try
                            {
                                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    Application.DoEvents();
                                    cs.Write(buffer, 0, read);
                                }

                                // Close up
                                fsIn.Close();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                            finally
                            {
                                cs.Close();
                                fsCrypt.Close();
                            }
                        }
                    }
                }      
            } 
        }

        // FileDecrypt Database
        public static void FileDecrypt(string inputFile, string outputFile, string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[32];

            using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
            {
                fsCrypt.Read(salt, 0, salt.Length);

                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Padding = PaddingMode.PKCS7;
                    AES.Mode = CipherMode.CFB;

                    using (CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                        {
                            int read;
                            byte[] buffer = new byte[1048576];

                            try
                            {
                                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    Application.DoEvents();
                                    fsOut.Write(buffer, 0, read);
                                }
                            }
                            catch (CryptographicException ex_CryptographicException)
                            {
                                Console.WriteLine("CryptographicException error: " + ex_CryptographicException.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }

                            try
                            {
                                cs.Close();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error by closing CryptoStream: " + ex.Message);
                            }
                            finally
                            {
                                fsOut.Close();
                                fsCrypt.Close();
                            }
                        }        
                    }       
                }  
            } 
        }

        // Decrypt Value 
        public static string FileDecryptJson(string inputFile, string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[32];

            using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
            {
                fsCrypt.Read(salt, 0, salt.Length);

                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Padding = PaddingMode.PKCS7;
                    AES.Mode = CipherMode.CFB;

                    using (CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            string decryptedData = sr.ReadToEnd();
                            return decryptedData;
                        }
                    }
                }

            }
        }

        // Encrypt Value 
        public static void FileEncryptJson(string inputFile, string password, Database data)
        {
            // Generate random salt
            byte[] salt = GenerateRandomSalt();
            // Convert password string to byte array
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Set output file name
            using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Create))
            {
                fsCrypt.Write(salt, 0, salt.Length);

                // Set Rijndael symmetric encryption algorithm
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Padding = PaddingMode.PKCS7;

                    var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CFB;

                    using (CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
                            sw.Write(jsonData);
                        }
                    }
                }
            }
        }


    }
}



