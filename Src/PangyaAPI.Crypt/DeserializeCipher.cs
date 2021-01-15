using System;
using System.IO;

namespace PangyaAPI.Crypt
{
    public static class DeserializeCipher
    {

        public static uint Decrypt(uint pangya_build_date)
        {
            byte[] pval = BitConverter.GetBytes(pangya_build_date);
            for (byte i = 0; i < CryptoOracle.CryptTableDeserialize.Length; i++)
            {
                pval[i % 4] ^= CryptoOracle.CryptTableDeserialize[i];
            }
            uint result = BitConverter.ToUInt32(pval, 0);
            return result;
        }

        public static uint Encrypt(uint pangya_build_date)
        {
            byte[] pval = BitConverter.GetBytes(pangya_build_date);
            int i, index;
            for (i = 0, index = 0; i < CryptoOracle.CryptTableDeserialize.Length; i++)
            {
                pval[index] ^= CryptoOracle.CryptTableDeserialize[i];
                index = (index == 3) ? 0 : ++index;
            }
            uint result = BitConverter.ToUInt32(pval, 0);
            return result;
        }
    }
}
