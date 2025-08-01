using UnityEngine;

namespace Gehenna
{
    public static class DialogueCommandFactory
    {
        public static IDialogueCommand Create(ActionType actionType, string param)
        {
            switch (actionType)
            {
                case ActionType.ShowUI:
                    return new ShowUICommand(param);
                case ActionType.MoveUI:
                    return new MoveUICommand(param);
                case ActionType.HideUI:
                    return new HideUICommand(param);
                case ActionType.RotateUI:
                    return new RotationCommand(param);
                case ActionType.FadeUI:
                    return new FadeCommand(param);
                default:
                    GehennaLogger.Log(null, LogType.Warning, $"Unknown ActionType: {actionType}");
                    return null;
            }
        }
    }

}