using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            container.Register(() => ManagerFactory.CreateManager<PoolingManager>());
            container.Register(() => ManagerFactory.CreateManager<AudioManager>());
            container.Register(() => ManagerFactory.CreateManager<InputManager>());
            container.Register(() => ManagerFactory.CreateManager<UIManager>());
            container.Register(() => ManagerFactory.CreateManager<CameraManager>());
            container.Register(() => ManagerFactory.CreateManager<GameContextManager>());
            container.Register(() => ManagerFactory.CreateManager<GameFlowManager>());
            
            container.Register(() => ManagerFactory.CreateManager<DialogueManager>());
            container.Register(() => UnityEngine.Object.FindObjectOfType<Dialoguetestmanager>());
            
            
            
            ManagerParamFactory paramFactory = new ManagerParamFactory(config, container, new IParamFactory[]
            {
                new ResourceParamFactory(),
                new PoolingParamFactory(),
                new AudioParamFactory(),
                new InputParamFactory(),
                new UIParamFactory(),
                new CameraParamFactory(),
                new GameContextParamFactory(),
                new GameFlowParamFactory(),
                
                new DialogueParamFactory(),
                new DTParmaFactory(),
            });
            
            foreach (var subManager in container.GetAll())
            {
                Type managerType = subManager.GetType();
                ManagerParam param = paramFactory.CreateContext(managerType);
                subManager.Initialize(param);
            }
        }
    
        public void ManualUpdate(float deltaTime)
        {
            foreach (var each in container.GetAll())
            {
                each.ManualUpdate(deltaTime);
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