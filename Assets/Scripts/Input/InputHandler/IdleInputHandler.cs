using UnityEngine;

namespace Gehenna
{
    public class IdleInputHandler : IInputHandler
    {
        public void Example()
        {
            GehennaLogger.Log(this, LogType.Info, "Example");
        }

        public void OnNextDialogue()
        {
        }
    }
}
