using UnityEngine;
using DG.Tweening;

namespace Gehenna
{
    public class FadeCommand : IDialogueCommand
    {
        private string uiKey;
        private float startAlpha;
        private float endAlpha;
        private float duration;

        public FadeCommand(string commandData)
        {
            ParseCommandData(commandData);
        }

        private void ParseCommandData(string data)
        {
            var parts = data.Split(',');

            if (parts.Length >= 4 &&
                float.TryParse(parts[1].Trim(), out float start) &&
                float.TryParse(parts[2].Trim(), out float end) &&
                float.TryParse(parts[3].Trim(), out float d))
            {
                uiKey = parts[0].Trim();
                startAlpha = Mathf.Clamp01(start / 100f);
                endAlpha = Mathf.Clamp01(end / 100f);
                duration = d;
            }
            else
            {
                uiKey = parts[0].Trim();
                startAlpha = 1f;
                endAlpha = 1f;
                duration = 0f;
            }
        }

        public void Execute(DialogueParam param)
        {
            if (!param.UIManager.TryGetUI<CommonUIModel>(uiKey, out var ui))
                return;

            if (!ui.TryGetComponent<CanvasGroup>(out var canvasGroup))
                canvasGroup = ui.gameObject.AddComponent<CanvasGroup>();

            canvasGroup.alpha = startAlpha;
            canvasGroup.DOFade(endAlpha, duration).SetEase(Ease.Linear);
        }
    }
}