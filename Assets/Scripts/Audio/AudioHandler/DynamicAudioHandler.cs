using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gehenna
{
    public class DynamicAudioHandler : BaseAudioHandler
    {
        public DynamicAudioHandler(AudioSource audioSource) : base(audioSource) { }
        
        public void Play(AudioClip clip, float volume = 1f, bool loop = false)
        {
            audioSource.clip = clip;
            SetVolume(volume);
            audioSource.loop = loop;
            audioSource.Play();
        }
        
        public async UniTask FadeOut(float duration, CancellationToken cancellationToken = default)
        {
            fadeTokenSource?.Cancel();
            fadeTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            CancellationToken token = fadeTokenSource.Token;

            float startVolume = audioSource.volume;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
                await UniTask.Yield(token);
                if (token.IsCancellationRequested) 
                    break;
            }

            Stop();
            SetVolume(startVolume);
        }
        
        public async UniTask FadeIn(AudioClip clip, float duration, float targetVolume = 1f, bool loop = false, CancellationToken cancellationToken = default)
        {
            fadeTokenSource?.Cancel();
            fadeTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            CancellationToken token = fadeTokenSource.Token;

            Play(clip, 0f, loop);

            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                if (token.IsCancellationRequested) break;

                audioSource.volume = Mathf.Lerp(0f, targetVolume, elapsed / duration);

                await UniTask.Yield(token);
            }

            if (!token.IsCancellationRequested)
                SetVolume(targetVolume);
        }
    }
}