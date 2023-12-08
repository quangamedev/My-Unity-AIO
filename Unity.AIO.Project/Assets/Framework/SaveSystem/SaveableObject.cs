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

    public string Id => id;

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

#if UNITY_EDITOR
    private static Dictionary<string, SaveableObject> s_globalLookup = new Dictionary<string, SaveableObject>();
    private void OnValidate()
    {
        if (Application.IsPlaying(gameObject)) return;
        if (string.IsNullOrEmpty(gameObject.scene.path)) return;
            
        if (string.IsNullOrEmpty(Id) || !IsUnique(Id))
        {
            GenerateId();
        }

        s_globalLookup[Id] = this;
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

        if (s_globalLookup[candidate].Id != candidate)
        {
            s_globalLookup.Remove(candidate);
            return true;
        }

        return false;
    }
#endif
}