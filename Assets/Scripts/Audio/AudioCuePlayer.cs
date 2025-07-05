using System.Collections.Generic;

namespace Gehenna
{
    public class AudioCuePlayer
    {
        private VolumeMixer mixer;
        
        private Dictionary<AudioType, PreloadedAudioHandler> preloadHandler = new Dictionary<AudioType, PreloadedAudioHandler>();
        private List<DynamicAudioHandler> dynamicHandlers = new List<DynamicAudioHandler>();
        
        public void Initialize(VolumeMixer mixer)
        {
            this.mixer = mixer;
        }

        public void CleanUp()
        {
            mixer = null;
        }
        
        public BaseAudioHandler GetPreloadHandler(AudioType audioType)
        {
            return preloadHandler.TryGetValue(audioType, out var handler) ? handler : null;
        }

        public BaseAudioHandler GetEmptyHandler()
        {
            return dynamicHandlers.Find(h => !h.IsPlaying);
        }
    }
}