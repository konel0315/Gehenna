using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gehenna
{
    public class TestUI : BaseUI
    {
        public TextMeshProUGUI speakerText;
        public TextMeshProUGUI dialogueText;
        public Image portraitImage;

        public override void OnOpen()
        {
            if (model is not TestUIModel testModel)
                return;

            speakerText.text = testModel.Speaker;
            dialogueText.text = testModel.Text;
            // TODO: portraitImage.sprite = LoadPortrait(testModel.Portrait);
        }
    }
}