using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Cryptography_LB3.Services;
using System.Security.Cryptography;

namespace Cryptography_LB3
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                bool isExit = false;

                Console.WriteLine("Please select an action:");
                Console.WriteLine("1) Encrypt using TripleDES;\n2) Encrypt using RSACng;\n3) Create DSA Sign;\n4) Get SHA384Managed Hash;\n5) Decrypt TripleDES file using key file\n6) Decrypt using RSACng \n7) Exit");
                var selected = Console.ReadLine();
                int action;
                var isValid = int.TryParse(selected, out action);

                if (!isValid)
                {
                    Console.WriteLine("Ivalid input!");
                    continue;
                }

                switch ((Actions)action)
                {
                    case Actions.EncryptTripleDES:
                        Console.WriteLine("===============================================================================");
                        Console.WriteLine("Please enter value that will be encrypted:");
                        var value = Console.ReadLine();
                        TripleDesService.EncryptTripleDES(value);
                        Console.WriteLine("===============================================================================\n");
                        break;
                    case Actions.DecryptTripleDES:
                        ProcessDecryptTripleDESAction();
                        break;
                    case Actions.RSACngEncrypt:
                        Console.WriteLine("===============================================================================");
                        Console.WriteLine("Please enter path to file that will be encrypted:");
                        var encryptFilePath = Console.ReadLine();
                        var encriptedFileBytes = File.ReadAllBytes(encryptFilePath);
                        var keys = RSACngService.GenerateKeys();
                        var encrypted = RSACngService.Encrypt(keys[RSACngService.PublicKeyName], encriptedFileBytes);

                        Console.WriteLine($"Encrypted data: {System.Text.Encoding.UTF8.GetString(encrypted)}");
                        Console.WriteLine("===============================================================================\n");
                        break;
                    case Actions.RSACngDecrypt:
                        Console.WriteLine("===============================================================================");
                        Console.WriteLine("Please enter path to file that will be decrypted:");
                        var decryptFilePath = Console.ReadLine();
                        var decryptFileBytes = File.ReadAllBytes(decryptFilePath);
                        Console.WriteLine($"Encrypted data: {System.Text.Encoding.UTF8.GetString(decryptFileBytes)}");

                        Console.WriteLine("Please enter path to file with private key:");
                        var privateKeyFilePath = Console.ReadLine();
                        var privateKeyFileBytes = File.ReadAllBytes(privateKeyFilePath);

                        var decrypted = RSACngService.Decrypt(privateKeyFileBytes, decryptFileBytes);

                        Console.WriteLine($"Decrypted data: {System.Text.Encoding.UTF8.GetString(decrypted)}");
                        Console.WriteLine("===============================================================================\n");
                        break;
                    case Actions.DSA:
                        Console.WriteLine("===============================================================================");
                        Console.WriteLine("Please enter path to file:");
                        var signatureFilePath = Console.ReadLine();
                        var signatureFileBytes = File.ReadAllBytes(signatureFilePath);
                        var service = new DSACngService();
                        var signature = service.CreateSignature(signatureFileBytes);
                        Console.WriteLine($"Signature: {System.Text.Encoding.UTF8.GetString(signature)}");
                        break;
                    case Actions.SHA384Managed:
                        Console.WriteLine("===============================================================================");
                        Console.WriteLine("Please enter path to file:");
                        var hashFilePath = Console.ReadLine();
                        var hashFileBytes = File.ReadAllBytes(hashFilePath);
                        SHA384 shaM = new SHA384Managed();
                        var result = shaM.ComputeHash(hashFileBytes);
                        Console.WriteLine($"File SHA384Managed Hash: {System.Text.Encoding.UTF8.GetString(result)}");
                        break;
                    case Actions.Exit:
                        isExit = true;
                        break;
                    default:
                        Console.WriteLine("Ivalid input!");
                        break;
                }

                if (isExit)
                {
                    break;
                }
                continue;
            }
        }

        public static void ProcessDecryptTripleDESAction()
        {
            Console.WriteLine("===============================================================================");
            Console.WriteLine("Please enter path to encrypted file:");
            byte[] encriptedFileBytes;
            while (true)
            {
                var encryptedPath = Console.ReadLine();
                if (!File.Exists(encryptedPath))
                {
                    Console.WriteLine("Invalid file path! try again");
                    continue;
                }
                encriptedFileBytes = File.ReadAllBytes(encryptedPath);
                break;
            }
            Console.WriteLine("Please enter path to key file:");
            byte[] keyFileBytes;
            while (true)
            {
                var keyFilePath = Console.ReadLine();
                if (!File.Exists(keyFilePath))
                {
                    Console.WriteLine("Invalid file path! try again");
                    continue;
                }
                keyFileBytes = File.ReadAllBytes(keyFilePath);
                break;
            }
            Console.WriteLine("Please enter path to initialization vector file:");
            byte[] vectorFileBytes;
            while (true)
            {
                var vectorFilePath = Console.ReadLine();
                if (!File.Exists(vectorFilePath))
                {
                    Console.WriteLine("Invalid file path! try again");
                    continue;
                }
                vectorFileBytes = File.ReadAllBytes(vectorFilePath);
                break;
            }

            TripleDesService.DecryptTripleDES(encriptedFileBytes, keyFileBytes, vectorFileBytes);
            Console.WriteLine("===============================================================================\n");
        }
    }
}
