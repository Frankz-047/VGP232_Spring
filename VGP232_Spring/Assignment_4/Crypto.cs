using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public enum CryptoAlgorithm
{
    RSA,
    AES
}

namespace Assignment_4
{
    public class Crypto
    {
        private AesCryptoServiceProvider aes;

        private RSACryptoServiceProvider rsa;

        private CryptoAlgorithm CryptoAlgorithmMode;

        public Crypto(CryptoAlgorithm ca)
        {
            CryptoAlgorithmMode = ca;
            if (CryptoAlgorithmMode == CryptoAlgorithm.AES)
            {
                aes = new AesCryptoServiceProvider();
            }
            else if (CryptoAlgorithmMode == CryptoAlgorithm.RSA)
            {
                rsa = new RSACryptoServiceProvider();
            }
        }

        public void SaveK1(string path)
        {
            switch (CryptoAlgorithmMode)
            {
                case CryptoAlgorithm.RSA:
                    string rasPrivateKey = rsa.ToXmlString(false);
                    File.WriteAllText(path, rasPrivateKey);
                    break;
                case CryptoAlgorithm.AES:
                    byte[] aesSharedKey = aes.Key;
                    File.WriteAllBytes(path, aesSharedKey);
                    break;
                default:
                    break;
            }
        }

        public void SaveK2(string path)
        {
            switch (CryptoAlgorithmMode)
            {
                case CryptoAlgorithm.RSA:
                    string rasPublicKey = rsa.ToXmlString(true);
                    File.WriteAllText(path, rasPublicKey);
                    break;
                case CryptoAlgorithm.AES:
                    byte[] aesIV = aes.IV;
                    File.WriteAllBytes(path, aesIV);
                    break;
                default:
                    break;
            }
        }

        public void LoadK1(string path)
        {
            switch (CryptoAlgorithmMode)
            {
                case CryptoAlgorithm.RSA:
                    string rsaKey = File.ReadAllText(path);
                    rsa.FromXmlString(rsaKey);
                    break;
                case CryptoAlgorithm.AES:
                    byte[] aeskey = File.ReadAllBytes(path);
                    aes.Key = aeskey;
                    break;
                default:
                    break;
            }
        }

        public void LoadK2(string path)
        {
            switch (CryptoAlgorithmMode)
            {
                case CryptoAlgorithm.RSA:
                    string rsaKey = File.ReadAllText(path);
                    rsa.FromXmlString(rsaKey);
                    break;
                case CryptoAlgorithm.AES:
                    byte[] aeskey = File.ReadAllBytes(path);
                    aes.IV = aeskey;
                    break;
                default:
                    break;
            }
        }

        public byte[] Encrypt(byte[] data)
        {
            if (CryptoAlgorithmMode == CryptoAlgorithm.AES)
            {
                if (data == null || data.Length == 0)
                {
                    return null;
                }

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
            else
            {
                byte[] encryptedData = rsa.Encrypt(data, true);
                return encryptedData;
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            if (CryptoAlgorithmMode == CryptoAlgorithm.AES)
            {
                if (data == null || data.Length == 0)
                {
                    return null;
                }

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
            else
            {
                byte[] decryptedData = rsa.Decrypt(data, true);
                return decryptedData;
            }
        }
    }
}
