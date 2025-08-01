using UnityEngine;

namespace Gehenna
{
    public class ShowUICommand : IDialogueCommand
    {
        private string uiKey;           // UI를 열기 위한 문자열 키
        private Vector2 position;
        private Vector2 size;

        public ShowUICommand(string commandData)
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
                float.TryParse(parts[3].Trim(), out float width) &&
                float.TryParse(parts[4].Trim(), out float height))
            {
                position = new Vector2(x, y);
                size = new Vector2(width, height);
            }
            else
            {
                position = Vector2.zero;
                size = Vector2.zero;
            }
        }

        public void Execute(DialogueParam param)
        {
            var model = new CommonUIModel
            {
                ImagePosition = position,
                ImageSize = size,
            };
            param.UIManager.TryOpenUI<CommonUIModel>(uiKey,model,out _);
        }
    }
} 