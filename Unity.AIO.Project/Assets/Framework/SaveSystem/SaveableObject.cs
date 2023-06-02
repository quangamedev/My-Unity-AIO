using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class must be added to objects that has 1 or more components that need saving.
/// </summary>
public class SaveableObject : MonoBehaviour
{
    [Tooltip("Generate this before saving or loading.")] [SerializeField]
    private string id = string.Empty;

    public string Id
    {
        get
        {
            return id;
        }
    }

    [ContextMenu("Generate Id")]
    private void GenerateId()
    {
        id = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Save the state of the GameObject that this component is added on.
    /// </summary>
    /// <returns></returns>
    public object SaveSate()
    {
        //Dictionary of all saveable components on the GameObject
        //The Key (string) is the type/component that implements ISaveable. In this demo, the component is SaveSystemDemo
        //The Value (object) is the data that will be saved. In this demo, the object is SaveData struct in SaveSystemDemo
        var state = new Dictionary<string, object>();

        //Loops through all components that implements ISaveable on the GameObject
        foreach (var saveable in GetComponents<ISaveable>())
        {
            //Sets the Value of the Dictionary according to classes that implements ISaveable
            state[saveable.GetType().ToString()] = saveable.SaveState();
        }

        return state;
    }

    /// <summary>
    /// Loads the state passed in to the GameObject.
    /// </summary>
    /// <param name="state"></param>
    public void LoadState(object state)
    {
        //Casts the state passed in to a Dictionary
        var stateDictionary = (Dictionary<string, object>)state;

        //Loops through all components that implements ISaveable on the GameObject
        foreach (var saveable in GetComponents<ISaveable>())
        {
            //get the string of the found Saved components to get data from the Dictionary
            var typeName = saveable.GetType().ToString();

            //If a value is found successfully, call the LoadState method in the component with ISaveble and pass in the object
            if (stateDictionary.TryGetValue(typeName, out object value))
                saveable.LoadState(value);
        }
    }
}