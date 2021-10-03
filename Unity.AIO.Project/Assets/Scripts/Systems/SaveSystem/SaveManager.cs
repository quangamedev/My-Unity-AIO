/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   3/10/21
--------------------------------------*/

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using DesignPatterns.Singleton;

namespace Systems.SaveSystem
{
    public class SaveManager : Singleton<SaveManager>
    {
        private string _path;

        protected override void Awake()
        {
            base.Awake();
            _path = Application.persistentDataPath + "/save.dat";
        }
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
                Load();
            else if (Input.GetKeyDown(KeyCode.S))
                Save();
        }

        [ContextMenu("Save")]
        public void Save()
        {
            //loads the previous saved files to the state variable
            //this step is necessary so saves from other places like scenes will not be overriden
            var state = LoadFile();
            
            //Saves states to a Dictionary 
            SaveState(state);
            
            //Saves the state to a file
            SaveFile(state);
        }

        [ContextMenu("Load")]
        public void Load()
        {
            //loads the previous saved files to the state variable
            var state = LoadFile();
            
            //loads the states to GameObjects
            LoadState(state);
        }

        /// <summary>
        /// Saves the file to the designated path.
        /// </summary>
        /// <param name="state">The data that will be saved.</param>
        private void SaveFile(object state)
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
        private Dictionary<string, object> LoadFile()
        {
            //if there is no previous saved data, return an empty Dictionary
            if (!File.Exists(_path)) return new Dictionary<string, object>();

            //Opens the file with open mode
            using var stream = File.Open(_path, FileMode.Open) ;
            
            var formatter = new BinaryFormatter();

            //Deserializes the stream and cast it back to a Dictionary
            return (Dictionary<string, object>) formatter.Deserialize(stream);
        }

        /// <summary>
        /// Saves the states of  objects that has the SaveableObject component.
        /// </summary>
        /// <param name="state">The dictionary of states that will be saved.</param>
        private void SaveState(Dictionary<string, object> state)
        {
            //Finds and Loops through all GameObjects with the SaveableObject component
            foreach (var saveable in FindObjectsOfType<SaveableObject>())
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
            foreach (var saveable in FindObjectsOfType<SaveableObject>())
            {
                //get the specific values in the dictionary according to its id and load in the saved data if it is valid
                if(state.TryGetValue(saveable.Id, out object value))
                    saveable.LoadState(value);
            }
        }
    }
}