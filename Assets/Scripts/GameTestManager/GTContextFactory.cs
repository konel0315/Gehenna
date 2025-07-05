using System;

namespace Gehenna
{
    public class GTContextFactory : IContextFactory
    {
        public Type ManagerType => typeof(GameTestManager);

        public ManagerContext Create(GameConfig config, ManagerContainer container)
        {
            return new GTContext(
                gameConfig: config,
                dialogueManager: container.Resolve<DialogueManager>()
            );
        }
    }
}