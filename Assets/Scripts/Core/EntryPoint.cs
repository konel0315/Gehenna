using UnityEngine;

namespace Gehenna
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameConfig gameConfig;
        
        private GameManager gameManager;

        private void Awake()
        {
            Initialize();
            Run();
        }

        private void Initialize()
        {
            gameConfig.Initialize();
            gameManager = new GameManager(gameConfig);
            gameManager.Initialize();
        }

        private void Run()
        {
            gameManager.Run();
        }

        private void Update()
        {
            gameManager.ManualUpdate();
        }

        private void LateUpdate()
        {
            gameManager.ManualLateUpdate();
        }

        private void FixedUpdate()
        {
            gameManager.ManualFixedUpdate();
        }
    }
}