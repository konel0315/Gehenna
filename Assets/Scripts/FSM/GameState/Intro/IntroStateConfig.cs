using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "IntroStateConfig", menuName = "Gehenna/GameState/IntroStateConfig")]
    public class IntroStateConfig : BaseGameStateConfig
    {
        public float FadeInDuration;
        public float HoldDuration;
        public float FadeOutDuration;
    }
}