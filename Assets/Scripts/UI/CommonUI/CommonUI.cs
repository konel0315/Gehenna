using TMPro;
using UnityEngine;

namespace Gehenna
{
    public class CommonUI : BaseUI<CommonUIModel>
    {
        public override UILayerType LayerType => UILayerType.Overlay;
        
        [SerializeField] private TextMeshProUGUI MainText;
        [SerializeField] private TextMeshProUGUI ButtonText;
        [SerializeField] private UnityEngine.UI.Image Image;
        [SerializeField] private UnityEngine.UI.Image ButtonImage;

        protected override void OnOpen()
        {
            if (model is not CommonUIModel commonModel)
                return;

            if (MainText != null)
            {
                MainText.gameObject.SetActive(commonModel.TextPosition.HasValue && commonModel.TextSize.HasValue);
                MainText.text = commonModel.Text;

                var rect = MainText.rectTransform;

                if (commonModel.TextPosition.HasValue)
                    rect.anchoredPosition = commonModel.TextPosition.Value;

                if (commonModel.TextSize.HasValue)
                    rect.sizeDelta = commonModel.TextSize.Value;
            }

            if (ButtonText != null)
            {
                ButtonText.gameObject.SetActive(!string.IsNullOrEmpty(commonModel.ButtonText));
                ButtonText.text = commonModel.ButtonText;
            }

            if (Image != null)
            {
                Image.gameObject.SetActive(commonModel.ImagePosition.HasValue&&commonModel.ImageSize.HasValue);
                
                if (!string.IsNullOrEmpty(commonModel.Image))
                {
                    var sprite = Resources.Load<Sprite>($"Sprites/{commonModel.Image}");
                    if (sprite != null)
                        Image.sprite = sprite;
                }

                var rect = Image.rectTransform;

                if (commonModel.ImagePosition.HasValue)
                    rect.anchoredPosition = commonModel.ImagePosition.Value;

                if (commonModel.ImageSize.HasValue)
                    rect.sizeDelta = commonModel.ImageSize.Value;
            }

            if (ButtonImage != null)
            {
                bool hasButtonImage = commonModel.ButtonPosition.HasValue && commonModel.ButtonSize.HasValue;
                ButtonImage.gameObject.SetActive(hasButtonImage);

                if (hasButtonImage)
                {
                    var sprite2 = Resources.Load<Sprite>($"Sprites/{commonModel.ButtonImage}");
                    if (sprite2 != null)
                        ButtonImage.sprite = sprite2;
                }

                var rect = ButtonImage.rectTransform;

                if (commonModel.ButtonPosition.HasValue)
                    rect.anchoredPosition = commonModel.ButtonPosition.Value;

                if (commonModel.ButtonPosition.HasValue)
                    rect.sizeDelta = commonModel.ButtonSize.Value;
            }
        }
    }
}

