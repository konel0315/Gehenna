using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gehenna
{
    public class BaseAudioHandler
    {
        protected AudioSource audioSource;
        protected CancellationTokenSource fadeTokenSource;
        
        public AudioClip CurrentClip => audioSource.clip;
        public bool IsPlaying => audioSource.isPlaying;
        
        public BaseAudioHandler(AudioSource audioSource)
        {
            this.audioSource = audioSource;
        }

        public void Pause()
        {
            if (audioSource.isPlaying)
                audioSource.Pause();
        }

        public void Resume()
        {
            audioSource.UnPause();
        }

        public void Stop()
        {
            audioSource.Stop();
        }

        public void SetVolume(float volume)
        {
            audioSource.volume = Mathf.Clamp01(volume);
        }
    }
}