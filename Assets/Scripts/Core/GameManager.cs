using System;
using System.Collections.Generic;

namespace Gehenna
{
    public class GameManager
    {
        private GameConfig config;
        private ManagerContainer container;

        public GameManager(GameConfig config)
        {
            this.config = config;
        }
    
        public void Initialize()
        {
            container = new ManagerContainer();
            container.Register(() => ManagerFactory.CreateManager<ResourceManager>());
            container.Register(() => ManagerFactory.CreateManager<AudioManager>());
            container.Register(() => ManagerFactory.CreateManager<InputManager>());
            container.Register(() => ManagerFactory.CreateManager<UIManager>());
            container.Register(() => ManagerFactory.CreateManager<DialogueManager>());
            container.Register(() => ManagerFactory.CreateManager<PoolingManager>());
            container.Register(() => ManagerFactory.CreateManager<GameFlowManager>());
            
            var gtm = UnityEngine.Object.FindObjectOfType<GameTestManager>();
            container.Register(() => gtm);

            ManagerContextFactory contextFactory = new ManagerContextFactory(config, container, new IContextFactory[]
            {
                new AudioContextFactory(),
                new GameFlowContextFactory(),
                new InputContextFactory(),
                new PoolingContextFactory(),
                new ResourceContextFactory(),
                new UIContextFactory(),
                new DialogueContextFactory()
                
                ,new GTContextFactory()
            });
            
            foreach (var subManager in container.GetAll())
            {
                Type managerType = subManager.GetType();
                ManagerContext context = contextFactory.CreateContext(managerType);
                subManager.Initialize(context);
            }
            
        }
    
        public void ManualUpdate()
        {
            foreach (var each in container.GetAll())
            {
                each.ManualUpdate();
            }
        }
    
        public void ManualLateUpdate()
        {
            foreach (var each in container.GetAll())
            {
                each.ManualLateUpdate();
            }
        }
    
        public void ManualFixedUpdate()
        {
            foreach (var each in container.GetAll())
            {
                each.ManualFixedUpdate();
            }
        }

        public void Run()
        { 
            container.Resolve<GameFlowManager>().Run();

        }
        
    }
}