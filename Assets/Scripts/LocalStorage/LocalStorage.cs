using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Gehenna
{
    public static class LocalStorage
    {
        private static CryptoService _cryptoService;

        static LocalStorage()
        {
            _cryptoService = new CryptoService();
            _cryptoService.Initialize();
        }
        
        public static bool Save<T>(string key, T value)
        {
            try
            {
                string encrypted = null;
                
                if (typeof(T) == typeof(int))
                {
                    encrypted = _cryptoService.Encrypt(value.ToString());
                }
                else if (typeof(T) == typeof(float))
                {
                    encrypted = _cryptoService.Encrypt(value.ToString());
                }
                else if (typeof(T) == typeof(string))
                {
                    encrypted = _cryptoService.Encrypt(value as string);
                }
                else if (typeof(T) == typeof(bool))
                {
                    encrypted = _cryptoService.Encrypt(value.ToString());
                }
                else if (typeof(T).IsClass || typeof(T).IsValueType)
                {
                    string json = JsonConvert.SerializeObject(value);
                    encrypted = _cryptoService.Encrypt(json);               
                }
                else
                {
                    Debug.LogError($"Unsupported type {typeof(T)}");
                    return false;
                }

                if (string.IsNullOrEmpty(encrypted))
                {
                    return false;
                }
                
                PlayerPrefs.SetString(key, encrypted);
                PlayerPrefs.Save();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save key {key}: {e.Message}");
                return false;
            }
        }

        public static bool TryLoad<T>(string key, out T value)
        {
            value = default;

            if (!PlayerPrefs.HasKey(key))
            {
                return false;
            }

            try
            {
                string cipherText = PlayerPrefs.GetString(key);
                string decrypted = _cryptoService.Decrypt(cipherText);
                
                if (typeof(T) == typeof(int))
                {
                    if (int.TryParse(decrypted, out int result))
                        value = (T)(object)result;
                    else
                        return false;
                }
                else if (typeof(T) == typeof(float))
                {
                    if (float.TryParse(decrypted, out float result))
                        value = (T)(object)result;
                    else
                        return false;
                }
                else if (typeof(T) == typeof(string))
                {
                    value = (T)(object)decrypted;
                }
                else if (typeof(T) == typeof(bool))
                {
                    if (bool.TryParse(decrypted, out bool result))
                        value = (T)(object)result;
                    else
                        return false;
                }
                else if (typeof(T).IsClass || typeof(T).IsValueType)
                {
                    value = JsonConvert.DeserializeObject<T>(decrypted);
                }
                else
                {
                    Debug.LogError($"Unsupported type {typeof(T)}");
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load key {key}: {e.Message}");
                return false;
            } 
        }
    }
}