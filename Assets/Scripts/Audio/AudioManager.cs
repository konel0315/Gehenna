namespace Gehenna
{
    public class AudioManager : ISubManager
    {
        private AudioContext context;
        private AudioConfig audioConfig;

        private VolumeMixer volumeMixer;
        private MusicPlayer musicPlayer;
        private AudioCuePlayer cuePlayer;
        
        public void Initialize(ManagerContext context)
        {
            if (context is not AudioContext audioContext)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.context = audioContext;
            this.audioConfig = audioContext.GameConfig.AudioConfig;

            volumeMixer = new VolumeMixer();
            musicPlayer = new MusicPlayer();
            cuePlayer = new AudioCuePlayer();
            
            volumeMixer.Initialize();
            musicPlayer.Initialize(volumeMixer);
            cuePlayer.Initialize(volumeMixer);
            
            audioContext.PoolingManager.CreateObjectPool(() => AudioHandleFactory.CreateDynamicAudioHandler(), audioConfig.maxDynamicAudioHandlers);
            if (!audioContext.GameConfig.TryGetCatalog<AudioCatalog>(out var audioCatalog))
            {
                GehennaLogger.Log(this, LogType.Error, "AudioCatalog not found in GameConfig");
                return;
            }
            
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }

        public void CleanUp()
        {
            volumeMixer?.CleanUp();
            musicPlayer?.CleanUp();
            cuePlayer?.CleanUp();

            context = null;
            volumeMixer = null;
            musicPlayer = null;
            cuePlayer = null;
        }
        
        public void ManualUpdate() { }
        public void ManualLateUpdate() { }
        public void ManualFixedUpdate() { }
    }
}

