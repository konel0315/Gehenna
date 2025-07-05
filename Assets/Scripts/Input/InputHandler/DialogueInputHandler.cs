
namespace Gehenna
{
    public class DialogueInputHandler : IInputHandler
    {
        private DialogueManager _dialogueManager;

        public DialogueInputHandler(DialogueManager dialogueManager)
        {
            _dialogueManager = dialogueManager;
        }

        public void Example() { }

        public void OnNextDialogue()
        {
            _dialogueManager.Next();
        }
    }
}
