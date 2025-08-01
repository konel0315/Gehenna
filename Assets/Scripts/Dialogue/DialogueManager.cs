using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gehenna
{
    public class DialogueManager : ISubManager
    {
        private DialogueParam param;
        private Dictionary<string, List<DialogueTable>> dialogueData;
        private Dictionary<string, List<DialogueActionData>> EventData;
        private DialogueExecutor executor;
        private DialogueLog dialogueLog = new DialogueLog();

        private List<DialogueActionData> currentEvents;
        private List<DialogueTable> currentLines;
        private int currentLineIndex;

        private Dictionary<int, List<ConditionBranch>> choiceTable;
        private Dictionary<int, List<ConditionBranch>> autoBranchTable;

        //private bool isWaitingForChoice = false;
        private bool hasChoices = false;
        private bool choiceConfirmed = false;
        
        private List<ConditionBranch> currentChoices;

        private int nextLineID=0;

        private bool isSkipHolding = false;
        private float skipTimer = 0f;
        private const float skipThreshold = 3f;
        
        private int selectedChoiceIndex = -1;
        
        public void Initialize(ManagerParam param)
        {
            if (param is not DialogueParam dialogueParam)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid param");
                return;
            }

            this.param = dialogueParam;

            if (!dialogueParam.GameConfig.TryGetTableSO<DialogueTableSO>(out var testSO))
            {
                GehennaLogger.Log(this, LogType.Error, "Missing TestSO");
                return;
            }
            var testEventSO = Resources.Load<DialogueActionDataSO>("DialogueCommand/DialogueCommand");
            if (testEventSO == null)
            {
                GehennaLogger.Log(this, LogType.Error, "Missing DialogueActionDataSO in Resources");
                return;
            }
            
            executor = new DialogueExecutor(dialogueParam.UIManager,this);
            dialogueData = testSO.GetGroupedTables();
            EventData = testEventSO.GetGroupedTables();
            GehennaLogger.Log(this, LogType.Success, "DialogueManager Initialized");
        }

        public void StartDialogue(string dialogueKey)
        {
            var handler = new DialogueInputHandler(this,param.UIManager);
            param.InputManager.SetHandler(handler);
            Debug.Log(handler);
            if (!dialogueData.TryGetValue(dialogueKey, out currentLines) || currentLines.Count == 0)
            {
                GehennaLogger.Log(this, LogType.Warning, $"No dialogue for key: {dialogueKey}");
                return;
            }
            if (!EventData.TryGetValue(dialogueKey, out currentEvents))
                currentEvents = new List<DialogueActionData>();
            
            choiceTable = new Dictionary<int, List<ConditionBranch>>();
            autoBranchTable = new Dictionary<int, List<ConditionBranch>>();
            foreach (var line in currentLines)
            {
                if (line.Type == "Choice")
                {
                    var group = ConditionGroupBuilder.Build(line);
                    choiceTable[line.ID] = group.Conditions;
                }
                if (line.Type == "AutoBranch")
                {
                    var group = ConditionGroupBuilder.Build(line);
                    autoBranchTable[line.ID] = group.Conditions;
                }
            }
            
            currentLineIndex = 0;
            ShowCurrentLine();
        }

        public void Next()
        {
            if (param.UIManager.TryGetUI<DialogueUIModel>(UIKey.DialogueUI, out var ui) && ui is DialogueUI dialogueUI)
            {
                if (dialogueUI.IsTyping)
                {
                    dialogueUI.SkipTyping();
                    return;
                }
            }
            
            if (hasChoices && !choiceConfirmed)
            {
                return;
            }

            if(currentLineIndex < 0 || currentLineIndex >= currentLines.Count)
            {
                EndDialogue();
                return;
            }

            int nextIdToUse = nextLineID != 0 ? nextLineID : currentLines[currentLineIndex].DefaultID;
            int nextIndex = currentLines.FindIndex(x => x.ID == nextIdToUse);

            if (nextIndex == -1)
            {
                EndDialogue();
                return;
            }

            currentLineIndex = nextIndex;
            nextLineID = 0;
            ShowCurrentLine();
        }

        public void SkipAll()
        {
            if (param.UIManager.TryGetUI<DialogueUIModel>(UIKey.DialogueUI, out var ui) && ui is DialogueUI dialogueUI)
            {
                if (dialogueUI.IsTyping)
                {
                    dialogueUI.SkipTyping();
                    return;
                }
            }
            
            if (hasChoices && !choiceConfirmed)
            {
                return;
            }
            
            while (currentLines != null && currentLineIndex >= 0 && currentLineIndex < currentLines.Count)
            {
                var current = currentLines[currentLineIndex];

                foreach (var evt in currentEvents)
                {
                    if (evt.ID == current.ID)
                    {
                        var command = DialogueCommandFactory.Create(evt.ActionsType, evt.Param);
                        command?.Execute(param);
                    }
                }

                Next();
                
                if (hasChoices && !choiceConfirmed)
                    break;
            }
        }
        
        
        public void AddLogEntry(string speaker, string text, string portrait)
        {
            dialogueLog.AddEntry(new DialogueLogEntry(speaker,text,portrait));
        }
        public List<DialogueLogEntry> GetAllLogEntries()
        {
            return dialogueLog.Entries.ToList();
        }
        public void OpenLogUI()
        {
            var model = new DialogueLogUIModel
            {
                OnRequestLog = GetAllLogEntries,
                SizeDelta = new Vector2(1920f, 500f),
                AnchoredPosition = new Vector2(0f, 0f)
            };

            if (param.UIManager.TryGetUI<DialogueLogUIModel>(UIKey.DialogueLogUI, out var result) && result.gameObject.activeSelf)
            {
                param.UIManager.TryCloseUI<DialogueLogUIModel>(UIKey.DialogueLogUI);
            }
            else
            {
                param.UIManager.TryOpenUI<DialogueLogUIModel>(UIKey.DialogueLogUI, model, out _);
            }
        }


        public void CloseLogUI()
        {
            param.UIManager.TryCloseUI<DialogueLogUIModel>(UIKey.DialogueLogUI);
        }


        public void BeginSkipHold()
        {
            if (param.UIManager.TryGetUI<DialogueLogUIModel>(UIKey.DialogueLogUI, out var result))
            {
                if (result.gameObject.activeSelf)
                {
                    CloseLogUI();
                    return;
                }
            }

            if (param.UIManager.TryGetUI<DialogueUIModel>(UIKey.DialogueUI, out var ui) && ui is DialogueUI dialogueUI)
            {
                dialogueUI.ShowSkipUI();
                dialogueUI.UpdateSkipGauge(0f);
            }

            isSkipHolding = true;
            skipTimer = 0f;
        }

        public void EndSkipHold()
        {
            if (!isSkipHolding) return;

            isSkipHolding = false;
            skipTimer = 0f;

            if (param.UIManager.TryGetUI<DialogueUIModel>(UIKey.DialogueUI, out var ui) && ui is DialogueUI dialogueUI)
            {
                dialogueUI.HideSkipUI();
            }
        }

        public void ManualUpdate(float deltaTime)
        {
            if (!isSkipHolding) return;

            skipTimer += deltaTime;

            if (param.UIManager.TryGetUI<DialogueUIModel>(UIKey.DialogueUI, out var ui) && ui is DialogueUI dialogueUI)
            {
                float ratio = Mathf.Clamp01(skipTimer / skipThreshold);
                dialogueUI.UpdateSkipGauge(ratio);
            }

            if (skipTimer >= skipThreshold)
            {
                SkipAll();
                EndSkipHold();
            }
        }

        public void CleanUp() { }
        public void ManualLateUpdate() { }
        public void ManualFixedUpdate() { }

        private void UpdateChoiceArrowUI()
        {
            if (param.UIManager.TryGetUI<DialogueUIModel>(UIKey.DialogueUI, out var ui) && ui is DialogueUI dialogueUI)
            {
                dialogueUI.SetChoiceArrow(selectedChoiceIndex);
            }
        }
        
        private void OnChoiceSelected(int nextID)
        {
            nextLineID = nextID;
            
            choiceConfirmed = true;
            hasChoices = true;
            
            currentChoices = null;
        }
        
        public void MoveChoiceIndex(int delta)
        {
            if (!hasChoices || currentChoices == null || currentChoices.Count == 0)
                return;

            if (selectedChoiceIndex == -1)
            {
                selectedChoiceIndex = 0;
            }
            else{
                selectedChoiceIndex += delta;
                if (selectedChoiceIndex < 0) selectedChoiceIndex = currentChoices.Count - 1;
                else if (selectedChoiceIndex >= currentChoices.Count) selectedChoiceIndex = 0;
                
            }
            

            nextLineID = currentChoices[selectedChoiceIndex].NextID;
            choiceConfirmed = true;
            UpdateChoiceArrowUI();
        }
        
        private void ShowCurrentLine()
        {
            var line = currentLines[currentLineIndex];

            if (line.Type == "Choice" && choiceTable.TryGetValue(line.ID, out var choices) && nextLineID == 0)
            {
                
                hasChoices = true;
                choiceConfirmed = false;
                
                //isWaitingForChoice = true;
                currentChoices = choices;
                selectedChoiceIndex = -1;

                executor.ShowChoices(choices, OnChoiceSelected);
                UpdateChoiceArrowUI();
            }
            else
            {
                hasChoices = false;
                currentChoices = null;

                if (param.UIManager.TryGetUI<DialogueUIModel>(UIKey.DialogueUI, out var ui) && ui is DialogueUI dialogueUI)
                {
                    dialogueUI.HideChoices();
                }
                if (line.Type == "AutoBranch"&& autoBranchTable.TryGetValue(line.ID, out var AB)&&nextLineID==0)
                {
                    foreach (var branch in AB)
                    {
                        var evaluator = CreateAutoBranch.CreateAB(branch.Condition);
                        if (evaluator != null && evaluator.Evaluate(param))
                        {
                            nextLineID = branch.NextID;
                            break;
                        }
                    }
                    
                }
            }
            foreach (var evt in currentEvents)
            {
                if (evt.ID == line.ID)
                {
                    var command = DialogueCommandFactory.Create(evt.ActionsType, evt.Param);
                    command?.Execute(param);
                }
            }
            
            executor.Execute(line);
        }

        private void EndDialogue()
        {
            currentLines = null;
            currentLineIndex = 0;
            param.UIManager.TryCloseUI<DialogueUIModel>(UIKey.DialogueUI);
            //param.InputManager.SetHandler(new IdleInputHandler()); 후에 기본상태 핸들러가 존재할때 수정
        }
    }
}
