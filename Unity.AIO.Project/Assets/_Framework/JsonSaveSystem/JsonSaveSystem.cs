using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JsonSaveSystem : MonoBehaviour
{
	[SerializeField] JObjectSaveStrategy strategy;

	private void Update()
	{
#if UNITY_EDITOR

#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current[Key.L].wasPressedThisFrame)
            Load();
        else if (Keyboard.current[Key.S].wasPressedThisFrame)
            Save();
#else
		if (Input.GetKeyDown(KeyCode.L))
			Load("save");
		else if (Input.GetKeyDown(KeyCode.S))
			Save("save");
#endif

#endif
	}

	/// <summary>
	/// Will load the last scene that was saved and restore the state. This
	/// must be run as a coroutine.
	/// </summary>
	/// <param name="saveFile">The save file to consult for loading.</param>
	public IEnumerator LoadLastScene(string saveFile)
	{
		JObject state = LoadJsonFromFile(saveFile);
		IDictionary<string, JToken> stateDict = state;
		int buildIndex = SceneManager.GetActiveScene().buildIndex;
		if (stateDict.ContainsKey("lastSceneBuildIndex"))
		{
			buildIndex = (int) stateDict["lastSceneBuildIndex"];
		}

		yield return SceneManager.LoadSceneAsync(buildIndex);
		RestoreFromToken(state);
	}

	/// <summary>
	/// Save the current scene to the provided save file.
	/// </summary>
	public void Save(string saveFile)
	{
		JObject state = LoadJsonFromFile(saveFile);
		CaptureAsToken(state);
		SaveFileAsJSon(saveFile, state);
	}

	/// <summary>
	/// Delete the state in the given save file.
	/// </summary>
	public void Delete(string saveFile)
	{
		File.Delete(GetPathFromSaveFile(saveFile));
	}

	public void Load(string saveFile)
	{
		RestoreFromToken(LoadJsonFromFile(saveFile));
	}

	public IEnumerable<string> ListSaves()
	{
		foreach (string path in Directory.EnumerateFiles(Application.persistentDataPath))
		{
			if (Path.GetExtension(path) == strategy.GetExtension())
			{
				yield return Path.GetFileNameWithoutExtension(path);
			}
		}
	}

	// PRIVATE

	private JObject LoadJsonFromFile(string saveFile)
	{
		return strategy.LoadFromFile(saveFile);
	}

	private void SaveFileAsJSon(string saveFile, JObject state)
	{
		strategy.SaveToFile(saveFile, state);
	}


	private void CaptureAsToken(JObject state)
	{
		IDictionary<string, JToken> stateDict = state;
		foreach (JsonSaveableEntity saveable in FindObjectsOfType<JsonSaveableEntity>())
		{
			stateDict[saveable.GetUniqueIdentifier()] = saveable.CaptureAsJToken();
		}

		stateDict["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
	}


	private void RestoreFromToken(JObject state)
	{
		IDictionary<string, JToken> stateDict = state;
		foreach (JsonSaveableEntity saveable in FindObjectsOfType<JsonSaveableEntity>())
		{
			string id = saveable.GetUniqueIdentifier();
			if (stateDict.ContainsKey(id))
			{
				saveable.RestoreFromJToken(stateDict[id]);
			}
		}
	}


	private string GetPathFromSaveFile(string saveFile)
	{
		return strategy.GetPathFromSaveFile(saveFile);
	}
}