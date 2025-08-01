using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gehenna
{
    public class DialogueExecutor
    {
        public UIManager uiManager;
        public DialogueManager dialogueManager;
        public float defaultSpeed=0.05f;
        public DialogueExecutor(UIManager uiManager,DialogueManager dialogueManager)
        {
            this.uiManager = uiManager;
            this.dialogueManager = dialogueManager;
        }

        public void Execute(DialogueTable Line)
        { 
            if (!string.IsNullOrWhiteSpace(Line.DefaultSpeed) && float.TryParse(Line.DefaultSpeed, out var parsedSpeed))
            {
                defaultSpeed = parsedSpeed;
            }

            
            var parsed = DialogueTextParser.Parse(Line.Text, defaultSpeed);
            
            var model = new DialogueUIModel
            {
                Speaker = Line.Speaker,
                Segments = parsed.Segment,
                Text = Line.Text,
                Portrait = Line.Portrait,
                
                SizeDelta = new Vector2(1920, 250),
                AnchoredPosition = new Vector2(0, -270),
                OnAutoNext = dialogueManager.Next,
                OnLog = dialogueManager.OpenLogUI,
                OnNext = dialogueManager.Next,
            };
            dialogueManager.AddLogEntry(Line.Speaker, Line.Text, Line.Portrait);
            if (Line.ID == 1)
            {
                uiManager.TryOpenUI<DialogueUIModel>(UIKey.DialogueUI,model,out var result);
            }
            else
            {
                if (uiManager.TryGetUI<DialogueUIModel>(UIKey.DialogueUI, out var ui) && ui is DialogueUI dialogueUI)
                {
                    dialogueUI.UpdateDialogue(model);
                }
            }
        }
        public void ShowChoices(List<ConditionBranch> choices, Action<int> onSelect)
        {
            
            if (!uiManager.TryGetUI<DialogueUIModel>(UIKey.DialogueUI, out var ui) || ui is not DialogueUI dialogueUI)
            {
                var model = new DialogueUIModel();
                uiManager.TryOpenUI<DialogueUIModel>(UIKey.DialogueUI,model,out ui);
                dialogueUI = ui as DialogueUI;
            }

            dialogueUI.ShowChoices(choices, onSelect);
        }
    }
}