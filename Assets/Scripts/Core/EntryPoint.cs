using UnityEngine;

namespace Gehenna
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameConfig gameConfig;
        
        private GameManager gameManager;
        private TimeService timeService;

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            Run();
        }

        private void Initialize()
        {
            gameManager = new GameManager(gameConfig);
            gameConfig.Initialize();
            gameManager.Initialize();
        }
        
        private void Run()
        {
            timeService = new TimeService(gameConfig);
            timeService.Start();
            gameManager.Run();
        }

        private void Update()
        {
            while (timeService.ShouldUpdate(out var deltaTime))
            {
                gameManager.ManualUpdate(deltaTime);
            }
        }
        
        private void FixedUpdate()
        {
            gameManager.ManualFixedUpdate();
        }
    }
}