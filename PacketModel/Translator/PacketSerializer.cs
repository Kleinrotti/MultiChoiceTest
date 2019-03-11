using PacketModel.Encryption;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PacketModel.Translator
{
    public static class PacketSerializer
    {
        /// <summary>
        /// Convert Object to encrypted byte array
        /// </summary>
        /// <param name="anySerializableObject"></param>
        /// <returns></returns>
        public static byte[] Serialize(object anySerializableObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(memoryStream, anySerializableObject);
                return AESEncrypt.encryptStream(memoryStream.ToArray(), AESEncrypt.Tempkey, AESEncrypt.TempIV);
                //return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Decrypt byte array and convert to object
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static object Deserialize(byte[] message)
        {
            using (var memoryStream = new MemoryStream(AESEncrypt.decryptStream(message, AESEncrypt.Tempkey, AESEncrypt.TempIV)))
            {
                return new BinaryFormatter().Deserialize(memoryStream);
            }
            //using (var memoryStream = new MemoryStream(message))
            //{
            //    return new BinaryFormatter().Deserialize(memoryStream);
            //}
        }
    }
}