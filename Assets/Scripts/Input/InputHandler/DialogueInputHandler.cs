
using UnityEngine;

namespace Gehenna
{
    public class DialogueInputHandler : IInputHandler
    {
        private DialogueManager _dialogueManager;
        private UIManager _uiManager;

        public DialogueInputHandler(DialogueManager dialogueManager,UIManager uiManager)
        {
            _dialogueManager = dialogueManager;
            _uiManager = uiManager;
        }
        
        public void Submit()
        {
            _dialogueManager.Next();
        }

        public void Auto()
        {
            if (_uiManager.TryGetUI<DialogueUIModel>("DialogueUI", out var baseUI))
            {
                DialogueUI ui = baseUI as DialogueUI;
                ui.SetAutoMode(!ui.IsAutoMode);
            }
        }

        public void Move(Vector2 direction)
        {
            
            if (direction.y > 0.1f)
            {
                _dialogueManager.MoveChoiceIndex(1);
            }
            else if (direction.y < -0.1f)
            {
                _dialogueManager.MoveChoiceIndex(-1);
            }
        }

        public void Cancel()
        {
        }

        public void Log()
        {
            _dialogueManager.OpenLogUI();
        }

        public void BeginSkipHold()
        {
            _dialogueManager.BeginSkipHold();
        }

        public void CancelSkipHold()
        {
            _dialogueManager.EndSkipHold();
        }
    }
}