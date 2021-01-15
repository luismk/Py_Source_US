using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace PangyaAPI.Crypt
{
    public static class Cryptor
    {
        public static void pangya_client_decrypt(ref byte[] source, byte key)
        {
            source = ClientCipher.Decrypt(source, key);
            //ClientCipher._pangya_client_decrypt(source, key);//
        }

        public static void pangya_client_encrypt(ref byte[] source, byte key, byte salt = 0)
        {
            source = ClientCipher.Encrypt(source, key, salt);
        }

        public static void pangya_server_decrypt(ref byte[] source, byte key)
        {
            source = ServerCipher.Decrypt(source, key);
        }

        public static byte[] pangya_server_encrypt(byte[] source, byte key, byte salt = 0)
        {
            source = ServerCipher.Encrypt(source, key, salt);
            return source;
        }

        public static uint pangya_deserialize(uint deserialize)
        {
            return DeserializeCipher.Decrypt(deserialize);
        }

        public static void pangya_deserialize(ref uint deserialize)
        {
            deserialize = DeserializeCipher.Decrypt(deserialize);
        }
        public static byte[] GenerateKeyId()
        {
            var packet_client_16 = new byte[] { 0xB3, 0x3E, 0x00, 0x00, 0x11, 0x01, 0x00, 0x06, 0x00, 0x6D, 0x75, 0x6F, 0x73, 0x01, 0x1E, 0x49, 0x73, 0x5D, 0x52, 0x18, 0x46, 0x06, 0x7B, 0x7B, 0x02, 0x02, 0x74, 0x71, 0x75, 0x70, 0x05, 0x05, 0x02, 0x07, 0x72, 0x73, 0x76, 0x77, 0x04, 0x7C, 0x76, 0x06, 0x73, 0x0A, 0x04, 0x70, 0x02, 0x74, 0x01, 0x42, 0x34, 0x46, 0x36, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, };
            byte pNum = (byte)new Random().Next(1, 255);
            int ParseKey = 16 << 8 + pNum;

            var packet = packet_client_16;
            pangya_client_decrypt(ref packet, 16);
            //pangya_client_encrypt(ref packet, 17, 0);
            //pangya_client_decrypt(ref packet, 17);
            //File.WriteAllBytes("KeysNew.hex", ((MemoryStream)binaryWriter.BaseStream).ToArray());
            return packet;
        }
    }
}
