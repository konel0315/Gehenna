using UnityEngine;
using UnityEngine.UI;

namespace Gehenna
{
    public class BattleActionOrderUI : BaseSubUI<BattleActionOrderUIModel>
    {
        [SerializeField] private Image icon;

        public void UpdateViews()
        {
            UpdateIcon();
            UpdateFocus();
            UpdateEmtpy();
        }

        private void UpdateIcon()
        {
            
        }

        private void UpdateFocus()
        {
            
        }

        private void UpdateEmtpy()
        {
            
        }
    }
}