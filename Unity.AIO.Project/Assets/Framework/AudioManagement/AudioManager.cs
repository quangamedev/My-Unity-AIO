using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AudioManager for 2D audio.
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    [System.Serializable]
    public class SoundClip
    {
        public string name;
        public AudioClip clip;
    }
    
    public AudioSource audioSource;

    public List<SoundClip> effectsList = new List<SoundClip>();

    public List<AudioClip> tracksList = new List<AudioClip>();

    public void PlaySound(string clip, float volumeScale = 0.7f, bool randomizePitch = false)
    {
        AudioClip audioClip = GetAudioClipInList(clip, effectsList);

        if (audioClip != null)
        {
            if (randomizePitch)
                audioSource.pitch = Random.Range(1f, 2f);

            audioSource.PlayOneShot(audioClip, volumeScale);

            if (randomizePitch)
                audioSource.pitch = 1;
        }
    }

    public void PlaySound(string clip)
    {
        AudioClip audioClip = GetAudioClipInList(clip, effectsList);

        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    private void PlayTrack(int trackID)
    {
        AudioClip audioClip = tracksList[trackID];
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public void MuteAudio()
    {
        audioSource.volume = 0;
    }

    public void UnMuteAudio()
    {
        audioSource.volume = 1;
    }

    private AudioClip GetAudioClipInList(string clip, List<SoundClip> list)
    {
        //find the audio clip with the name passed in in the clip list
        AudioClip audioClip = list.FirstOrDefault(obj => obj.name == clip)?.clip;
        if (audioClip != null)
            return audioClip;
        else
        {
            Debug.LogError("Audio clip " + "'" + clip + "'" + " not found in audio manager");
            return null;
        }
    }
}