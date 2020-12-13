using System.Security.Cryptography;
using CWBlazor.Server.Hubs.ChatModels;

namespace CWBlazor.Server.Hubs.ChatServices
{
    public static class CryptoService
    {
        private const int KeySize = 4096;

        public static Keys GeneratePair()
        {
            byte[] publicKey, privateKey;

            using (var rsa = new RSACryptoServiceProvider(KeySize))
            {
                var a = rsa.SignatureAlgorithm;
                publicKey = rsa.ExportCspBlob(false);
                privateKey = rsa.ExportCspBlob(true);
            }

            return new Keys
            {
                PublicKey = publicKey, PrivateKey = privateKey
            };
        }

        public static Keys UpdateKeysBySymmetricKey(byte[] input, Keys keys)
        {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider(KeySize))
            {
                rsa.ImportCspBlob(keys.PrivateKey);
                decrypted = rsa.Decrypt(input, true);
            }

            return new Keys
            {
                PrivateKey = keys.PrivateKey, PublicKey = keys.PublicKey, SymmetricKey = decrypted,
            };
        }

        public static string Encrypt(string value, Keys keys)
        {
            return value;
        }

        public static string Decrypt(string value, Keys keys)
        {
            return value;
        }
    }
}