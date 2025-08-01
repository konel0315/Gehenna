using UnityEngine;
using DG.Tweening;

namespace Gehenna
{
    public class RotationCommand : IDialogueCommand
    {
        private string uiKey;
        private Vector3 rotation;
        private float duration;

        public RotationCommand(string commandData)
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

            if (parts.Length == 5 &&
                float.TryParse(parts[1].Trim(), out float x) &&
                float.TryParse(parts[2].Trim(), out float y) &&
                float.TryParse(parts[3].Trim(), out float z) &&
                float.TryParse(parts[4].Trim(), out float d))
            {
                rotation = new Vector3(x, y, z);
                duration = d;
            }
            else
            {
                rotation = Vector3.zero;
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
                .DORotate(rotation, duration, RotateMode.FastBeyond360)
                .SetEase(Ease.OutCubic);
        }
    }
}