using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

namespace Gehenna
{
    public class BattleCommandUI : BaseSubUI<BattleCommandUIModel>
    {
        [SerializeField] private Button button;
        [SerializeField] private Image focusImage;
        
        public void AddListener(UnityAction callback)
        {
            button.onClick.AddListener(callback);
        }
    }
}