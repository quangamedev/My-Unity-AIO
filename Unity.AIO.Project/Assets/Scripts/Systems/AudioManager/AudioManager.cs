/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   11/7/22
--------------------------------------*/

using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DesignPatterns.Singleton;

[System.Serializable]
public class SoundClip
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
}

namespace Systems.AudioManager
{
    public class AudioManager : Singleton<AudioManager>
    {
        public AudioSource audioSource;

        //public List<AudioSource> audioSourceList = new List<AudioSource>();
        public List<SoundClip> effectsList = new List<SoundClip>();

        public List<AudioClip> tracksList = new List<AudioClip>();
        // Start is called before the first frame update

        // Update is called once per frame
        void FixedUpdate()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
                return;

            //if the is no audio playing and the application is in focus
            //Application.isFocused is used to fix a bug where new music would play if the player re enters the game
            if (!audioSource.isPlaying && Application.isFocused)
            {
                //get a random number indicating track to play
                int rand = Random.Range(0, tracksList.Count);

                //if the clip in audio source is not null, we need to change music
                if (audioSource.clip != null)
                {
                    //int to prevent the game from being stuck in an never ending while loop
                    //used for fall back, but tries should never exceed 100
                    int tries = 0;

                    //a while loop to find a track that is different from the previous track
                    while (tracksList[rand].name == audioSource.clip.name || tries > 100)
                    {
                        //get a random number for the TrackID
                        rand = Random.Range(0, tracksList.Count);
                        tries++;
                        if (tries == 100)
                            Debug.LogError("Cannot find non-duplicate");
                    }

                    print(tracksList[rand].name + " " + audioSource.clip);
                }

                //instance call to make this object not be destroyed on load
                PlayTrack(rand);
            }
        }

        public void PlaySound(string clip, float volumeScale = 0.7f, bool randomizePitch = false)
        {
            AudioClip _audioClip = GetAudioClipInList(clip, effectsList);

            if (_audioClip != null)
            {
                if (randomizePitch)
                    audioSource.pitch = Random.Range(1f, 2f);

                audioSource.PlayOneShot(_audioClip, volumeScale);

                if (randomizePitch)
                    audioSource.pitch = 1;
            }
        }

        public void PlaySound(string clip)
        {
            AudioClip _audioClip = GetAudioClipInList(clip, effectsList);

            if (_audioClip != null)
            {
                audioSource.PlayOneShot(_audioClip);
            }
        }

        private void PlayTrack(int trackID)
        {
            AudioClip _audioClip = tracksList[trackID];
            if (_audioClip != null)
            {
                audioSource.clip = _audioClip;
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
            AudioClip _audioClip = list.FirstOrDefault(obj => obj.name == clip)?.clip;
            if (_audioClip != null)
                return _audioClip;
            else
            {
                Debug.LogError("Audio clip " + "'" + clip + "'" + " not found in audio manager");
                return null;
            }
        }
    }
}