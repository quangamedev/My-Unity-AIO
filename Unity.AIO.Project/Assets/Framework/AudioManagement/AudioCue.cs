using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "NewSoundEffect", menuName = "Audio/New Sound Effect")]
public class AudioCue : ScriptableObject
{
	[SerializeField] private AudioSource _audioCueObject;

	[SerializeField] private AudioClip[] clips;

	[SerializeField] private AudioMixerGroup output;

	[Range(0, 1)]
	[SerializeField] private float minVolume = 0.8f;

	[Range(0, 1)]
	[SerializeField] private float maxVolume = 1f;

	[Range(0, 3)]
	[SerializeField] private float minPitch = 0.55f;

	[Range(0, 3)]
	[SerializeField] private float maxPitch = 1.78f;


	[SerializeField] private SoundClipPlayOrder playOrder;

	[SerializeField] private int playIndex = 0;

	private AudioClip GetAudioClip()
	{
		// get current clip
		var clip = clips[playIndex >= clips.Length ? 0 : playIndex];

		// find next clip
		switch (playOrder)
		{
			case SoundClipPlayOrder.FirstToLast:
				playIndex = (playIndex + 1) % clips.Length;
				break;
			case SoundClipPlayOrder.Random:
				playIndex = Random.Range(0, clips.Length);
				break;
			case SoundClipPlayOrder.LastToFirst:
				playIndex = (playIndex + clips.Length - 1) % clips.Length;
				break;
		}

		// return clip
		return clip;
	}

	public AudioSource Play(Vector3 position)
	{
		return Play(null, position);
	}

	public AudioSource Play(AudioSource audioSourceParam = null, Vector3 position = new Vector3(), bool loop = false, AudioMixerGroup audioMixerGroup = null)
	{
		if (clips.Length == 0)
		{
			return null;
		}

		var source = audioSourceParam;
		if (!source)
		{
			source = _audioCueObject.SpawnFromPool();
			source.transform.position = position;
		}

		if (audioMixerGroup)
			output = audioMixerGroup;

		if (output)
			source.outputAudioMixerGroup = output;

		// set source config:
		source.clip = GetAudioClip();
		source.volume = Random.Range(minVolume, maxVolume);
		source.pitch = Random.Range(minPitch, maxPitch);
		source.loop = loop;
		source.Play();

#if UNITY_EDITOR
		if (source != previewer)
		{
			source.ReturnToPool(source.clip.length / source.pitch);
		}
#else
            source.ReturnToPool(source.clip.length / source.pitch);
#endif

		return source;
	}

#if UNITY_EDITOR
	private AudioSource previewer;

	private void OnEnable()
	{
		previewer = EditorUtility
			.CreateGameObjectWithHideFlags("AudioPreview", HideFlags.HideAndDontSave,
				typeof(AudioSource))
			.GetComponent<AudioSource>();
	}

	private void OnDisable()
	{
		DestroyImmediate(previewer.gameObject);
	}

	public void PlayPreview()
	{
		Play(previewer);
	}

	public void StopPreview()
	{
		previewer.Stop();
	}

	private float lastUpdatedMinVolume;
	private float lastUpdatedMaxVolume;
	private float lastUpdatedMinPitch;
	private float lastUpdatedMaxPitch;
	private void OnValidate()
	{
		if (minVolume > maxVolume && minVolume > lastUpdatedMinVolume)
		{
			maxVolume = minVolume;
		}
		else if (minVolume > maxVolume && maxVolume < lastUpdatedMaxVolume)
		{
			minVolume = maxVolume;
		}
		
		lastUpdatedMinVolume = minVolume;
		lastUpdatedMaxVolume = maxVolume;

		if (minPitch > maxPitch && minPitch > lastUpdatedMinPitch)
		{
			maxPitch = minPitch;
		}
		else if (minPitch > maxPitch && maxPitch < lastUpdatedMaxPitch)
		{
			minPitch = maxPitch;
		}

		lastUpdatedMinPitch = minPitch;
		lastUpdatedMaxPitch = maxPitch;
	}
#endif

	enum SoundClipPlayOrder
	{
		Random,
		FirstToLast,
		LastToFirst
	}
}


[CustomEditor(typeof(AudioCue))]
public class SoundEffectSOEditorCustom : Editor
{
	public override void OnInspectorGUI()
	{
		AudioCue target = (AudioCue) this.target;
		DrawDefaultInspector();

		if (GUILayout.Button("Preview"))
		{
			target.PlayPreview();
		}

		if (GUILayout.Button("Stop Preview"))
		{
			target.StopPreview();
		}
	}
}