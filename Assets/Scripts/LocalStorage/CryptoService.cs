using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Gehenna
{
    public class CryptoService
    {
        private CryptoConfig _config;

        public void Initialize()
        {
            _config = Resources.Load<CryptoConfig>("Crypto/CryptoConfig");
            
            if (_config == null)
            {
                throw new ArgumentNullException(nameof(_config));
            }
            
            if (_config.Key.Length != 16 || _config.IV.Length != 16)
            {
                throw new ArgumentException("Key and IV must be exactly 16 bytes long.");
            }

            GehennaLogger.Log(this, LogType.Success, "Initializing");
        }

        public void CleanUp()
        {
            _config = null;
        }
        
        public string Encrypt(string plainText)
        {
            if (_config == null) throw new InvalidOperationException($"{nameof(CryptoService)} not initialized.");

            try
            {
                using var aesAlg = Aes.Create();
                aesAlg.Key = _config.Key;
                aesAlg.IV = _config.IV;

                using var encryptor = aesAlg.CreateEncryptor();
                using var ms = new System.IO.MemoryStream();
                using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                using (var sw = new System.IO.StreamWriter(cs))
                {
                    sw.Write(plainText);
                }
                
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                Debug.LogError($"Encryption failed: {e.Message}");
                return null;
            }
        }

        public string Decrypt(string cipherText)
        {
            if (_config == null)
            {
                throw new InvalidOperationException($"{nameof(CryptoService)} not initialized.");
            }

            try
            {
                using var aesAlg = Aes.Create();
                aesAlg.Key = _config.Key;
                aesAlg.IV = _config.IV;

                using var decryptor = aesAlg.CreateDecryptor();
                byte[] bytes = Convert.FromBase64String(cipherText);

                using var ms = new System.IO.MemoryStream(bytes);
                using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using var sr = new System.IO.StreamReader(cs);

                return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                Debug.LogError($"Decryption failed: {e.Message}");
                return null;
            }
        }
    }
}