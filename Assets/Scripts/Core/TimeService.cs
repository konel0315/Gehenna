using System.Diagnostics;
using UnityEngine;

namespace Gehenna
{
    public class TimeService
    {
        private GameConfig gameConfig;
        private readonly double fixedDeltaTime;
        
        private Stopwatch stopwatch;
        private double lastElapsedTime;
        private double accumulatedTime;
        private float timeScale;
        private bool isPaused;
        
        public float TimeScale
        {
            get => timeScale;
            set => timeScale = Mathf.Max(0, value);
        }

        public TimeService(GameConfig gameConfig)
        {
            this.gameConfig = gameConfig;
            this.fixedDeltaTime = gameConfig.FixedDeltaTime;
            
            stopwatch = new Stopwatch();
            lastElapsedTime = 0;
            accumulatedTime = 0;
            timeScale = 1f;
        }
        
        public void Start()
        {
            stopwatch.Start();
            lastElapsedTime = 0;
            accumulatedTime = 0;
        }
        
        public bool ShouldUpdate(out float deltaTime)
        {
            deltaTime = 0;
            
            if (isPaused)
                return false;
            
            double currentTime = stopwatch.Elapsed.TotalSeconds;
            double frameDelta = currentTime - lastElapsedTime;
            
            accumulatedTime += frameDelta * timeScale;
            lastElapsedTime = currentTime;
            
            if (accumulatedTime >= fixedDeltaTime)
            {
                deltaTime = (float)fixedDeltaTime;
                accumulatedTime -= fixedDeltaTime;
                return true;
            }
            
            return false;
        }
        
        public void Pause() => isPaused = true;
        public void Resume() => isPaused = false;
    }
}