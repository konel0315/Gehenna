using System;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "CryptoConfig", menuName = "Gehenna/CryptoConfig", order = 0)]
    public class CryptoConfig : SerializedScriptableObject
    {
        [FormerlySerializedAs("EncryptionKeyString")] [SerializeField] private string encryptionKeyString;
        [FormerlySerializedAs("InitializationVectorString")] [SerializeField] private string initializationVectorString;

        public byte[] Key;
        public byte[] IV;

        public void Initialize()
        {
            Key = Convert.FromBase64String(encryptionKeyString);
            IV = Convert.FromBase64String(initializationVectorString);
        }
    }
}