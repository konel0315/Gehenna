using UnityEngine;

namespace Gehenna
{
    public class GameTestManager : MonoBehaviour , ISubManager
    {
        private DialogueManager _dialogueManager;
        
        private void Awake()
        {
            
        }
        public void Initialize(ManagerContext context)
        {
            if (context is not GTContext gtContext)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context type");
                return;
            }

            _dialogueManager = gtContext.DialogueManager;

            _dialogueManager.StartDialogue("첫번째 대화");
            
        }

        public void CleanUp()
        {
        }

        public void ManualUpdate()
        {
        }

        public void ManualLateUpdate()
        {
        }

        public void ManualFixedUpdate()
        {
        }
        
    }
}