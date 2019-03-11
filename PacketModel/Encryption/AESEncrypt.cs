using System;
using System.IO;
using System.Security.Cryptography;

namespace PacketModel.Encryption
{
    internal static class AESEncrypt
    {
        //UNSECURE!!!
        public static byte[] Tempkey = Convert.FromBase64String("AAECAwQFBgcICQoLDA0ODw==");

        public static byte[] TempIV = Convert.FromBase64String("LKECAwgfBgcICdoLpA0ODw==");

        /// <summary>
        /// Encrypt specified stream.
        /// </summary>
        /// <param name="plain"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static byte[] encryptStream(byte[] plain, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            using (MemoryStream mstream = new MemoryStream())
            {
                using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(mstream,
                        aesProvider.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plain, 0, plain.Length);
                    }
                    encrypted = mstream.ToArray();
                }
            }
            return encrypted;
        }

        /// <summary>
        /// Decrypt specified stream.
        /// </summary>
        /// <param name="encrypted"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static byte[] decryptStream(byte[] encrypted, byte[] Key, byte[] IV)
        {
            byte[] plain;
            int count;
            using (MemoryStream mStream = new MemoryStream(encrypted))
            {
                using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
                {
                    aesProvider.Mode = CipherMode.CBC;
                    using (CryptoStream cryptoStream = new CryptoStream(mStream,
                     aesProvider.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                    {
                        plain = new byte[encrypted.Length];
                        count = cryptoStream.Read(plain, 0, plain.Length);
                    }
                }
            }
            byte[] returnval = new byte[count];
            Array.Copy(plain, returnval, count);
            return returnval;
        }
    }
}