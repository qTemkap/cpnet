using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography_LB3
{
    public class RSACngService
    {
        static byte[] privateKey;
        static byte[] publicKey;
        public const string PrivateKeyName = "PrivateKey";
        public const string PublicKeyName = "PublicKey";

        public static Dictionary<string, byte[]> GenerateKeys(string folder = "")
        {
            var keysDictionary = new Dictionary<string, byte[]>();
            try
            {
                CngKey cngKey;

                CngKeyCreationParameters cng = new CngKeyCreationParameters
                {
                    KeyUsage = CngKeyUsages.AllUsages
                };

                if (!CngKey.Exists("rsaKey"))
                    cngKey = CngKey.Create(CngAlgorithm.Rsa, "rsaKey", cng);
                else
                    cngKey = CngKey.Open("rsaKey");

                RSACng rsaKey = new RSACng(cngKey)
                {
                    KeySize = 2048
                };

                // Try importing / exporting to blobs (Example only)

                byte[] rsaPrvKeyExport = rsaKey.Key.Export(CngKeyBlobFormat.GenericPrivateBlob);
                byte[] rsaPubKeyExport = rsaKey.Key.Export(CngKeyBlobFormat.GenericPublicBlob);

                CngKey cngPrv = CngKey.Import(rsaPrvKeyExport, CngKeyBlobFormat.GenericPrivateBlob);
                CngKey cngPub = CngKey.Import(rsaPubKeyExport, CngKeyBlobFormat.GenericPublicBlob);

                // Try importing / exporting to parameters (Example only)

                RSAParameters pub = rsaKey.ExportParameters(false);
                RSAParameters prv = rsaKey.ExportParameters(true);

                RSACng rsaPrv = new RSACng();
                rsaPrv.ImportParameters(prv);

                RSACng rsaPub = new RSACng();
                rsaPub.ImportParameters(pub);

                // These are our keys

                privateKey = rsaKey.Key.Export(CngKeyBlobFormat.GenericPrivateBlob);
                publicKey = rsaKey.Key.Export(CngKeyBlobFormat.GenericPublicBlob);

                string prvResult = ByteArrayToHexString(privateKey, 0, privateKey.Length);
                string pubResult = ByteArrayToHexString(publicKey, 0, publicKey.Length);

                Console.WriteLine("\nPrivate key - length = " + privateKey.Length + "\n" + prvResult + "\n");
                Console.WriteLine("\nPublic key - length = " + publicKey.Length + "\n" + pubResult + "\n");

                //Save keys
                File.WriteAllBytes($"{folder}PrivateKey.bin", privateKey);
                File.WriteAllBytes($"{folder}PublicKey.bin", publicKey);

                keysDictionary.Add(PrivateKeyName, privateKey);
                keysDictionary.Add(PublicKeyName, publicKey);

                Console.WriteLine("Keys saved");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }

            return keysDictionary;
        }

        static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
                if (a1[i] != a2[i])
                    return false;

            return true;
        }

        public static string ByteArrayToHexString(byte[] bytes, int start, int length)
        {
            string delimitedStringValue = BitConverter.ToString(bytes, start, length);
            return delimitedStringValue.Replace("-", "");
        }

        public static byte[] Sign512(byte[] data, byte[] privateKey)
        {
            CngKey key = CngKey.Import(privateKey, CngKeyBlobFormat.GenericPrivateBlob);
            RSACng crypto = new RSACng(key);
            return crypto.SignData(data, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
        }

        public static bool VerifySignature512(byte[] data, byte[] signature, byte[] publicKey)
        {
            CngKey key = CngKey.Import(publicKey, CngKeyBlobFormat.GenericPublicBlob);
            RSACng crypto = new RSACng(key);
            return crypto.VerifyData(data, signature, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
        }

        public static byte[] Encrypt(byte[] publicKey, byte[] data)
        {
            CngKey key = CngKey.Import(publicKey, CngKeyBlobFormat.GenericPublicBlob);
            RSACng crypto = new RSACng(key);
            var result = crypto.Encrypt(data, RSAEncryptionPadding.OaepSHA512);

            File.WriteAllBytes(@"EnryptedFile.bin", result);
            return result;
        }
        public static byte[] Decrypt(byte[] privateKey, byte[] data)
        {
            CngKey key = CngKey.Import(privateKey, CngKeyBlobFormat.GenericPrivateBlob);
            RSACng crypto = new RSACng(key);
            var result = crypto.Decrypt(data, RSAEncryptionPadding.OaepSHA512);
            return result;
        }
    }
}
