namespace Gehenna
{
    public class AudioManager : ISubManager
    {
        private AudioParam param;
        private AudioConfig config;

        private VolumeMixer volumeMixer;
        private MusicPlayer musicPlayer;
        private AudioCuePlayer cuePlayer;
        
        public void Initialize(ManagerParam param)
        {
            if (param is not AudioParam audioParam)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.param = audioParam;

            if (!audioParam.GameConfig.TryGetConfig<AudioConfig>(out config))
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid config");
                return;
            }

            volumeMixer = new VolumeMixer();
            musicPlayer = new MusicPlayer();
            cuePlayer = new AudioCuePlayer();
            
            volumeMixer.Initialize();
            musicPlayer.Initialize(volumeMixer);
            cuePlayer.Initialize(volumeMixer);
            
            audioParam.PoolingManager.CreateObjectPool<DynamicAudioHandler>(() => AudioHandleFactory.CreateDynamicAudioHandler(), config.maxDynamicAudioHandlers);
            if (!audioParam.GameConfig.TryGetCatalog<AudioCatalog>(out var audioCatalog))
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

            param = null;
            volumeMixer = null;
            musicPlayer = null;
            cuePlayer = null;
        }
        
        public void ManualUpdate(float deltaTime) { }
        public void ManualFixedUpdate() { }
    }
}

