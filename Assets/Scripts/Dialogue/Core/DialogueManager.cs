using System.Collections.Generic;
using Sirenix.OdinInspector.Editor.Examples.Internal;
using UnityEngine;

namespace Gehenna
{
    public class DialogueManager : ISubManager
    {
        private Dictionary<string, List<DialogueLine>> dialogueData;
        private DialogueContext context;

        private string currentEventName;
        private int currentLineIndex;
        private List<DialogueLine> currentLines;

        public void Initialize(ManagerContext context)
        {
            if (context is not DialogueContext dialogueContext)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }

            this.context = dialogueContext;

            var dialogueSO = dialogueContext.GameConfig.DialogueTableSO;
            var tables = dialogueSO.GetTables();

            var grouped = DialogueConverter.GroupByEventName(tables);

            dialogueData = new Dictionary<string, List<DialogueLine>>();
            foreach (var pair in grouped)
            {
                dialogueData[pair.Key] = DialogueConverter.ConvertTablesToLines(pair.Value);
            }

            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }

        public void StartDialogue(string eventName)
        {
            context.InputManager.SetHandler(new DialogueInputHandler(this));
            if (!dialogueData.TryGetValue(eventName, out currentLines) || currentLines.Count == 0)
            {
                return;
            }

            currentEventName = eventName;
            currentLineIndex = 0;

            ShowCurrentLine();
        }

        public void Next()
        {
            if (currentLines == null || currentLineIndex >= currentLines.Count)
            {
                StopDialogue();
                return;
            }
            DialogueLine currentLine = currentLines[currentLineIndex];
            int nextId = GetNextLineId(currentLine);
            int nextIndex = currentLines.FindIndex(line => line.ID == nextId);
             if (nextIndex == -1)
             {
                 StopDialogue();
                 return;
             }
            currentLineIndex = nextIndex;
            ShowCurrentLine();
        }

        private int GetNextLineId(DialogueLine line)
        {
            foreach (var pair in line.Branch.ConditionalNextIDs)
            {
                if (pair.Key.Evaluate())
                    return pair.Value;
            }
            return line.Branch.DefaultNextID;
        }

        private void ShowCurrentLine()
        {
            if(currentLineIndex!=0){context.UIManager.CloseUI(UIType.Example);}
            DialogueLine line = currentLines[currentLineIndex];
            
            line.Commands?.Execute(context);
            var uiModel = new TestUIModel(
                speaker: line.Speaker,
                text: line.Text,
                portrait: line.Portrait,
                size: new Vector2(1910,530),
                position: new Vector2(0, -270)         
            );
            
            context.UIManager.OpenUI(UIType.Example, uiModel);
            
        }

        public void StopDialogue()
        {

            currentEventName = null;
            currentLineIndex = 0;
            currentLines = null;

            context.InputManager.SetHandler(new IdleInputHandler());
            context.UIManager.CloseUI(UIType.Example);
        }

        public void ManualUpdate() { }
        public void ManualLateUpdate() { }
        public void ManualFixedUpdate() { }
        public void CleanUp() { }
    }
}
