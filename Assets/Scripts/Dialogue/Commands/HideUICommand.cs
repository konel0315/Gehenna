using UnityEngine;
using System;

namespace Gehenna
{
    public class HideUICommand : IDialogueCommand
    {
        private string uiKey;

        public HideUICommand(string commandData)
        {
            ParseCommandData(commandData);
        }

        private void ParseCommandData(string data)
        {
            uiKey = data.Trim();
        }

        public void Execute(DialogueParam param)
        {
            param.UIManager.TryCloseUI<CommonUIModel>(uiKey);
        }
    }
}