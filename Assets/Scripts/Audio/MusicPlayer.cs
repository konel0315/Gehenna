using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gehenna
{
    public class MusicPlayer
    {
        private VolumeMixer mixer;
        private DynamicAudioHandler currentHandler;
        
        public AudioClip CurrentClip => currentHandler?.CurrentClip;
        
        public void Initialize(VolumeMixer mixer)
        {
            this.mixer = mixer;
            currentHandler = AudioHandleFactory.CreateDynamicAudioHandler();
        }
        
        public void CleanUp()
        {
            currentHandler?.Stop();
            
            mixer = null;
            currentHandler = null;
        }

        public void Play(AudioBundle audioBundle)
        {
            float volume = mixer.GetFinalVolume(audioBundle.BusType, audioBundle.BaseVolume);
            currentHandler.Play(audioBundle.AudioClip, volume: volume, audioBundle.Loop);
        }

        public void Pause()
        {
            if (currentHandler.IsPlaying)
                currentHandler.Pause();
            else
                GehennaLogger.Log(this, LogType.Warning, "Cannot pause, audio handler is not available.");
        }

        public void Resume()
        {
            if (!currentHandler.IsPlaying)
                currentHandler.Resume();
            else
                GehennaLogger.Log(this, LogType.Warning, "Cannot resume, audio handler is already playing.");
        }

        public void Stop()
        {
            if (currentHandler.IsPlaying)
                currentHandler.Stop();
            else
                GehennaLogger.Log(this, LogType.Warning, "Cannot stop, audio handler is not available.");
        }

        public void FadeIn(AudioBundle audioBundle, float duration)
        {
            float volume = mixer.GetFinalVolume(audioBundle.BusType, audioBundle.BaseVolume);
            currentHandler.FadeIn
            (
                clip: audioBundle.AudioClip,
                duration: duration,
                targetVolume: volume,
                loop: audioBundle.Loop
            ).Forget();
        }

        public void FadeOut(float duration)
        {
            currentHandler.FadeOut(duration).Forget();
        }

        public void FadeTo(AudioBundle nextBundle, float duration)
        {
            FadeToInternal().Forget();

            return;
            
            async UniTask FadeToInternal()
            {
                if (currentHandler.IsPlaying)
                {
                    Play(nextBundle);
                    return;
                }
                
                float volume = mixer.GetFinalVolume(nextBundle.BusType, nextBundle.BaseVolume);
                
                await currentHandler.FadeOut(duration);
                await currentHandler.FadeIn(nextBundle.AudioClip, duration, volume, nextBundle.Loop);
            }
        }
    }
}