using UnityEngine;
using DG.Tweening;

namespace Gehenna
{
    public class MoveUICommand : IDialogueCommand
    {
        private string uiKey;
        private Vector2 targetPos;
        private float duration;

        public MoveUICommand(string commandData)
        {
            ParseCommandData(commandData);
        }

        private void ParseCommandData(string data)
        {
            var parts = data.Split(',');

            if (parts.Length >= 1)
            {
                uiKey = parts[0].Trim();
            }

            if (parts.Length == 4 &&
                float.TryParse(parts[1].Trim(), out float x) &&
                float.TryParse(parts[2].Trim(), out float y) &&
                float.TryParse(parts[3].Trim(), out float d))
            {
                targetPos = new Vector2(x, y);
                duration = d;
            }
            else
            {
                targetPos = Vector2.zero;
                duration = 0f;
            }
        }

        public void Execute(DialogueParam param)
        {
            if (!param.UIManager.TryGetUI<CommonUIModel>(uiKey, out var ui))
                return;

            if (!ui.TryGetComponent<RectTransform>(out var rectTransform))
                return;

            rectTransform
                .DOAnchorPos(targetPos, duration)
                .SetEase(Ease.OutCubic);
        }
    }
}