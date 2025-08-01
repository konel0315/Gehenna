using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gehenna
{
    public class IntroUI : BaseUI<IntroUIModel>
    {
        [SerializeField] private Image logo;

        public override UILayerType LayerType => UILayerType.Overlay;
        
        protected override void OnOpen()
        {
            base.OnOpen();
            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, 0f);
            
            Sequence seq = DOTween.Sequence();
            seq.Append(logo.DOFade(1f, model.FadeInDuration))
                .AppendInterval(model.HoldDuration)
                .Append(logo.DOFade(0f, model.FadeOutDuration))
                .OnComplete(() => model.OnComplete?.Invoke());
        }
    }
}