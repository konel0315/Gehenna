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

        public void Play(AudioTrack audioTrack)
        {
            float volume = mixer.GetFinalVolume(audioTrack.BusType, audioTrack.BaseVolume);
            currentHandler.Play(audioTrack.AudioClip, volume: volume, audioTrack.Loop);
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

        public void FadeIn(AudioTrack audioTrack, float duration)
        {
            float volume = mixer.GetFinalVolume(audioTrack.BusType, audioTrack.BaseVolume);
            currentHandler.FadeIn
            (
                clip: audioTrack.AudioClip,
                duration: duration,
                targetVolume: volume,
                loop: audioTrack.Loop
            ).Forget();
        }

        public void FadeOut(float duration)
        {
            currentHandler.FadeOut(duration).Forget();
        }

        public void FadeTo(AudioTrack nextTrack, float duration)
        {
            FadeToInternal().Forget();

            return;
            
            async UniTask FadeToInternal()
            {
                if (currentHandler.IsPlaying)
                {
                    Play(nextTrack);
                    return;
                }
                
                float volume = mixer.GetFinalVolume(nextTrack.BusType, nextTrack.BaseVolume);
                
                await currentHandler.FadeOut(duration);
                await currentHandler.FadeIn(nextTrack.AudioClip, duration, volume, nextTrack.Loop);
            }
        }
    }
}