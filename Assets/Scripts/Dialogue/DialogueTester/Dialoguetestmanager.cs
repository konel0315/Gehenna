using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Gehenna
{
    public class Dialoguetestmanager : MonoBehaviour, ISubManager
    {
        private DialogueManager _dialogueManager;

        [SerializeField, LabelText("대화 테이블")]
        private DialogueTableSO dialogueTableSO;

        [SerializeField, LabelText("실행할 대화 키"), ValueDropdown(nameof(GetDialogueKeys))]
        private string selectedDialogueKey;
        
        [Button("대화 실행")]
        private void PlaySelectedDialogue()
        {
            if (string.IsNullOrEmpty(selectedDialogueKey))
            {
                GehennaLogger.Log(this, LogType.Warning, "대화 키가 선택되지 않았습니다.");
                return;
            }

            if (_dialogueManager == null)
            {
                GehennaLogger.Log(this, LogType.Error, "DialogueManager가 초기화되지 않았습니다.");
                return;
            }

            if (!dialogueTableSO.GroupedTables.ContainsKey(selectedDialogueKey))
            {
                GehennaLogger.Log(this, LogType.Error, $"해당 키({selectedDialogueKey})의 대화가 없습니다.");
                return;
            }

            _dialogueManager.StartDialogue(selectedDialogueKey);
            GehennaLogger.Log(this, LogType.Success, $"{selectedDialogueKey} 대화 실행됨");
        }


        private IEnumerable<string> GetDialogueKeys()
        {
            if (dialogueTableSO == null || dialogueTableSO.GroupedTables == null)
                yield break;

            foreach (var key in dialogueTableSO.GroupedTables.Keys)
            {
                yield return key;
            }
        }

        public void Initialize(ManagerParam context)
        {
            if (context is not DTParam gtContext)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context type");
                return;
            }

            _dialogueManager = gtContext.DialogueManager;

            GehennaLogger.Log(this, LogType.Success, "GameTestManager Initialized");
        }

        public void CleanUp() { }
        public void ManualUpdate(float deltaTime) { }
        public void ManualLateUpdate() { }
        public void ManualFixedUpdate() { }
    }
}
