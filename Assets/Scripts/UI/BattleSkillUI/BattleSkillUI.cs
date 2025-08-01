using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gehenna
{
    public class BattleSkillUI : BaseSubUI<BattleSkillUIModel>
    {
        [SerializeField] private Button button;

        public void AddListener(UnityAction callback)
        {
            button.onClick.AddListener(callback);
        }
    }
}