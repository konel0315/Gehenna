using System;

namespace Gehenna
{
    public class DTParmaFactory : IParamFactory
    {
        public Type ManagerType => typeof(Dialoguetestmanager);

        public ManagerParam Create(GameConfig config, ManagerContainer container)
        {
            return new DTParam(
                gameConfig: config,
                dialogueManager: container.Resolve<DialogueManager>()
            );
        }
    }
}