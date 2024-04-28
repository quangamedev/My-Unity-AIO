using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

public class JsonSaveableEntity : MonoBehaviour
{
	[SerializeField] string uniqueIdentifier = "";

	// CACHED STATE
	static Dictionary<string, JsonSaveableEntity> globalLookup = new Dictionary<string, JsonSaveableEntity>();

	public string GetUniqueIdentifier()
	{
		return uniqueIdentifier;
	}

	public JToken CaptureAsJToken()
	{
		JObject state = new JObject();
		IDictionary<string, JToken> stateDict = state;
		foreach (IJsonSaveable jsonSaveable in GetComponents<IJsonSaveable>())
		{
			JToken token = jsonSaveable.CaptureAsJToken();
			string component = jsonSaveable.GetType().ToString();
#if UNITY_EDITOR
			Debug.Log($"{name} Capture {component} = {Environment.NewLine} {token.ToString()}", gameObject);
#endif
			stateDict[jsonSaveable.GetType().ToString()] = token;
		}

		return state;
	}

	public void RestoreFromJToken(JToken s)
	{
		JObject state = s.ToObject<JObject>();
		IDictionary<string, JToken> stateDict = state;
		foreach (IJsonSaveable jsonSaveable in GetComponents<IJsonSaveable>())
		{
			string component = jsonSaveable.GetType().ToString();
			if (stateDict.ContainsKey(component))
			{
#if UNITY_EDITOR
				Debug.Log($"{name} Restore {component} => {Environment.NewLine} {stateDict[component].ToString()}", gameObject);
#endif
				jsonSaveable.RestoreFromJToken(stateDict[component]);
			}
		}
	}

#if UNITY_EDITOR
	private static Dictionary<string, JsonSaveableEntity> s_globalLookup = new Dictionary<string, JsonSaveableEntity>();

	private void OnValidate()
	{
		if (Application.IsPlaying(gameObject)) return;
		if (string.IsNullOrEmpty(gameObject.scene.path)) return;

		if (string.IsNullOrEmpty(uniqueIdentifier) || !IsUnique(uniqueIdentifier))
		{
			uniqueIdentifier = Guid.NewGuid().ToString();
		}

		s_globalLookup[uniqueIdentifier] = this;
	}

	private bool IsUnique(string candidate)
	{
		if (!s_globalLookup.ContainsKey(candidate)) return true;

		if (s_globalLookup[candidate] == this) return true;

		if (s_globalLookup[candidate] == null)
		{
			s_globalLookup.Remove(candidate);
			return true;
		}

		if (s_globalLookup[candidate].uniqueIdentifier != candidate)
		{
			s_globalLookup.Remove(candidate);
			return true;
		}

		return false;
	}
#endif
}