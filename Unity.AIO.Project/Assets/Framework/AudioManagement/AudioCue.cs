using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "NewSoundEffect", menuName = "Audio/New Sound Effect")]
public class AudioCue : ScriptableObject
{
	#region config

	[SerializeField] private AudioSource _audioCueObject;

	[SerializeField] private AudioClip[] clips;

	[SerializeField] private Vector2 volume = new Vector2(0.5f, 0.5f);

	[Range(0.55f, 1.78f)]
	[SerializeField] private float minPitch = 0.55f;

	[Range(0.63f, 1.78f)]
	[SerializeField] private float maxPitch = 1.78f;


	[SerializeField] private SoundClipPlayOrder playOrder;

	[SerializeField] private int playIndex = 0;

	#endregion

	#region PreviewCode

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

#endif

	#endregion

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

	public AudioSource Play(AudioSource audioSourceParam = null, Vector3 position = new Vector3(), bool loop = false)
	{
		if (clips.Length == 0)
		{
			return null;
		}

		var source = audioSourceParam;
		if (source == null)
		{
			source = _audioCueObject.SpawnFromPool();
			source.transform.position = position;
		}

		// set source config:
		source.clip = GetAudioClip();
		source.volume = Random.Range(volume.x, volume.y);
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