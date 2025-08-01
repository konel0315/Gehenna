using TMPro;
using UnityEngine;

namespace Gehenna
{
    public class BattleDamageUI : BaseUI<BattleDamageUIModel>
    {
        public override UILayerType LayerType => UILayerType.Widget;

        [SerializeField] private TextMeshProUGUI damageText;

        protected override void OnOpen()
        {
            base.OnOpen();

            UpdateViews();
        }

        public void UpdateViews()
        {
            UpdateDamage();
        }

        private void UpdateDamage()
        {
            
        }
    }
}