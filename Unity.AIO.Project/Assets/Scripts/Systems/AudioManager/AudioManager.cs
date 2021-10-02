using System.Collections.Generic;
using System.Linq;
using DesignPatterns.Singleton;
using UnityEngine;

namespace ManagersAndSystems.AudioManager
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioSource _audioSource;
    
        public List<AudioClip> effectsList = new List<AudioClip>();
        public List<AudioClip> tracksList = new List<AudioClip>();

        public void PlaySound(string clip, float volumeScale = 0.7f, bool randomizePitch = false)
        {
            AudioClip audioClip = GetAudioClipInList(clip, effectsList);

            if (audioClip == null) return;
        
            if (randomizePitch)
                _audioSource.pitch = Random.Range(1f, 2f);

            _audioSource.PlayOneShot(audioClip, volumeScale);

            if (randomizePitch)
                _audioSource.pitch = 1;
        }

        public void PlaySound(string clip)
        {
            AudioClip audioClip = GetAudioClipInList(clip, effectsList);

            if (audioClip == null) return;
        
            _audioSource.PlayOneShot(audioClip);
        }

        public void PlayTrack(string clip)
        {
            AudioClip audioClip = GetAudioClipInList(clip, tracksList);
        
            if (audioClip == null) return;
        
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        public void PlayTrack(int trackID)
        {
            AudioClip audioClip = tracksList[trackID];
        
            if (audioClip == null) return;
        
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        private AudioClip GetAudioClipInList(string clip, List<AudioClip> list)
        {
            //find the audio clip with the name passed in in the clip list
            AudioClip audioClip = list.Where(obj => obj.name == clip).FirstOrDefault();
            if (audioClip != null)
                return audioClip;
            else
            {
                Debug.LogError("Audio clip " + "'" + clip + "'" + " not found in audio manager");
                return null;
            }
        }

    }
}