using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    [System.Serializable]
    public class DialogueActionData
    {
        [ValueDropdown("$DialogueKeys")] public string DialogueKey;

        [ValueDropdown("$IDs")] public int ID;
        
        [OnValueChanged(nameof(UpdateParam))]
        public ActionType ActionsType;
        
        [ShowIf("@ActionsType == ActionType.ShowUI||ActionsType == ActionType.HideUI||ActionsType == ActionType.MoveUI||ActionsType == ActionType.RotateUI")]
        [OnValueChanged(nameof(UpdateParam))]
        [ValueDropdown(nameof(GetUITypeKeys))]
        public string UIType;
        
        [ShowIf("@ActionsType == ActionType.ShowUI || ActionsType == ActionType.MoveUI")]
        [OnValueChanged(nameof(UpdateParam))]
        public Vector2 Position;
        
        [ShowIf("@ActionsType == ActionType.ShowUI")]
        [OnValueChanged(nameof(UpdateParam))]
        public Vector2 Size;
        
        [ShowIf("@ActionsType == ActionType.RotateUI")]
        [OnValueChanged(nameof(UpdateParam))]
        public Vector3 Rotation;
        
        [ShowIf("@ActionsType == ActionType.MoveUI||ActionsType == ActionType.RotateUI")]
        [OnValueChanged(nameof(UpdateParam))]
        public float Speed;
        
        
        [ReadOnly]
        public string Param;
        
        [HideInInspector]
        public List<string> DialogueKeys = new List<string>();

        [HideInInspector]
        public List<int> IDs = new List<int>();
        
        private void UpdateParam()
        {
            switch (ActionsType)
            {
                case ActionType.ShowUI:
                    Param = $"{UIType},{Position.x},{Position.y},{Size.x},{Size.y}";
                    break;
                case ActionType.MoveUI:
                    Param = $"{UIType},{Position.x},{Position.y},{Speed}";
                    break;
                case ActionType.HideUI:
                    Param = $"{UIType}";
                    break;
                case ActionType.RotateUI:
                    Param = $"{UIType},{Rotation.x},{Rotation.y},{Rotation.z},{Speed}";
                    break;
                default:
                    Param = string.Empty;
                    break;
            }
        }
#if UNITY_EDITOR
        private static IEnumerable<string> GetUITypeKeys()
        {
            return UIKey.AllKeys;
        }
#endif
    }
}