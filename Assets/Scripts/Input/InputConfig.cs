using UnityEngine;
using UnityEngine.InputSystem;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "InputConfig", menuName = "Gehenna/Config/InputConfig")]
    public class InputConfig : BaseConfig
    {
        public override void Initialize()
        {
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }
    }
}