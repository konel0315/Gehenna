using UnityEngine;

namespace Gehenna
{
    public class PreloadedAudioHandler : BaseAudioHandler
    {
        public PreloadedAudioHandler(AudioSource audioSource, AudioClip assignedClip) : base(audioSource)
        {
            audioSource.clip = assignedClip;
        }
        
        public void Play(float volume = 1f, bool loop = false)
        {
            SetVolume(volume);
            audioSource.loop = loop;
            audioSource.Play();
        }
    }
}