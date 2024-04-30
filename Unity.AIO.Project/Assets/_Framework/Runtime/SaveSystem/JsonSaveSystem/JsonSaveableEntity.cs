using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

public class JsonSaveableEntity : MonoBehaviour
{
	[SerializeField] string uniqueIdentifier = "";

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
			this.Log($"{name} captures {component}\r\n{token}");
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
				this.Log($"{name} restores {component}\r\n{stateDict[component]}");
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