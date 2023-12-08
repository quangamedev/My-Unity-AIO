using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class SaveSystem : Singleton<SaveSystem>
{
    private string _path;

    protected override void Awake()
    {
        base.Awake();
        _path = Application.persistentDataPath + "/save.dat";
    }

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
            Load();
        else if (Input.GetKeyDown(KeyCode.S))
            Save();
    #endif

    #endif
    }

    [ContextMenu("Save")]
    public void Save()
    {
        //loads the previous saved files to the state variable
        //this step is necessary so saves from other places like scenes will not be overriden
        var state = LoadFromFile();

        //Saves states to a Dictionary
        SaveState(state);

        //Saves the state to a file
        SaveToFile(state);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        //loads the previous saved files to the state variable
        var state = LoadFromFile();

        //loads the states to GameObjects
        LoadState(state);
    }

    /// <summary>
    /// Saves the file to the designated path.
    /// </summary>
    /// <param name="state">The data that will be saved.</param>
    private void SaveToFile(object state)
    {
        //Opens the file with create mode
        using var stream = File.Open(_path, FileMode.Create);

        //Creates new Binary Formatter object
        var formatter = new BinaryFormatter();

        //Serializes the data into the file
        formatter.Serialize(stream, state);
    }

    /// <summary>
    /// Loads in the save file.
    /// </summary>
    /// <returns>Saved data if there is any</returns>
    private Dictionary<string, object> LoadFromFile()
    {
        //if there is no previous saved data, return an empty Dictionary
        if (!File.Exists(_path)) return new Dictionary<string, object>();

        //Opens the file with open mode
        using var stream = File.Open(_path, FileMode.Open);

        var formatter = new BinaryFormatter();

        //Deserializes the stream and cast it back to a Dictionary
        return (Dictionary<string, object>)formatter.Deserialize(stream);
    }

    /// <summary>
    /// Saves the states of  objects that has the SaveableObject component.
    /// </summary>
    /// <param name="state">The dictionary of states that will be saved.</param>
    private void SaveState(Dictionary<string, object> state)
    {
        //Finds and Loops through all GameObjects with the SaveableObject component
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            //set the specific values in the dictionary according to its id
            state[saveable.Id] = saveable.SaveSate();
        }
    }

    /// <summary>
    /// Loads the states of  objects that has the SaveableObject component.
    /// </summary>
    /// <param name="state">The dictionary of states that will be loaded.</param>
    private void LoadState(Dictionary<string, object> state)
    {
        //Finds and Loops through all GameObjects with the SaveableObject component
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            //get the specific values in the dictionary according to its id and load in the saved data if it is valid
            if (state.TryGetValue(saveable.Id, out object value))
                saveable.LoadState(value);
        }
    }
}