using PacketModel.Encryption;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PacketModel.Translator
{
    public static class PacketSerializer
    {
        public static byte[] Serialize(object anySerializableObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(memoryStream, anySerializableObject);
                return AESEncrypt.encryptStream(memoryStream.ToArray(), AESEncrypt.Tempkey, AESEncrypt.TempIV);
            }
        }

        public static object Deserialize(byte[] message)
        {
            using (var memoryStream = new MemoryStream(AESEncrypt.decryptStream(message, AESEncrypt.Tempkey, AESEncrypt.TempIV)))
            {
                return new BinaryFormatter().Deserialize(memoryStream);
            }
        }
    }
}