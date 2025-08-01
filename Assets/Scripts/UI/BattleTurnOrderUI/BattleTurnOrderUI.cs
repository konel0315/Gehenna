using UnityEngine;
using UnityEngine.UI;

namespace Gehenna
{
    public class BattleTurnOrderUI : BaseSubUI<BattleTurnOrderUIModel>
    {
        [SerializeField] private Image icon;
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Slider manaSlider;

        public void UpdateViews()
        {
            UpdateIcon();
            UpdateHp();
            UpdateMana();
            UpdateFocus();
            UpdateEmpty();
        }

        private void UpdateIcon()
        {
            
        }

        private void UpdateHp()
        {
            
        }

        private void UpdateMana()
        {
            
        }

        private void UpdateFocus()
        {
            
        }

        private void UpdateEmpty()
        {
            
        }
    }
}