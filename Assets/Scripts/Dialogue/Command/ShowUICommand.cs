using UnityEngine;
using System;

namespace Gehenna
{
    public class ShowUICommand : IDialogueCommand
    {
        private UIType? uiType;
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
                string rawUiName = parts[0].Trim();

                if (Enum.TryParse<UIType>(rawUiName, true, out var parsed))
                {
                    uiType = parsed;
                }
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

        public void Execute(DialogueContext context)
        {
            if (!uiType.HasValue)
            {
                Debug.Log("4");
                return;
            }

            var model = new BaseUIModel(position, size);
            context.UIManager.OpenUI(uiType.Value, model);
            
        }

    }
}