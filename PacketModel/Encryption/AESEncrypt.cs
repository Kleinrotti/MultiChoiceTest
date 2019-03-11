using System;
using System.IO;
using System.Security.Cryptography;

namespace PacketModel.Encryption
{
    internal static class AESEncrypt
    {
        //UNSECURE!!!
        public static byte[] Tempkey = { 0x01, 0x02, 0x01, 0x08, 0x05, 0x23, 0x12 };

        public static byte[] TempIV = { 0x02, 0x22, 0x34, 0x78 };

        public static byte[] encryptStream(byte[] plain, byte[] Key, byte[] IV)
        {
            byte[] encrypted; ;
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