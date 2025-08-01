using Unity.Behavior.GraphFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Gehenna
{
    public class BattleCharacterStatusUI : BaseUI<BattleCharacterStatusUIModel>
    {
        public override UILayerType LayerType => UILayerType.Widget;

        [SerializeField] private Image[] icons;
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Slider manaSlider;

        protected override void OnOpen()
        {
            base.OnOpen();
            
            UpdateViews();
        }

        public void UpdateViews()
        {
            UpdateIcon();
            UpdateHp();
            UpdateMana();
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
    }
}