using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

/// <summary>
/// For simple data saving.
/// Useful for saving simple values or simple systems.
/// Avoid saving large systems with this
/// </summary>
[RequireComponent(typeof(JsonSaveableEntity))]
public class GameData : MonoBehaviour, IJsonSaveable
{
    public GameDataKeys Keys => _keys;
    [SerializeField] private GameDataKeys _keys;

    public JToken CaptureAsJToken()
    {
        return JToken.FromObject(Keys);
    }

    public void RestoreFromJToken(JToken s)
    {
        _keys = s.ToObject<GameDataKeys>();
    }

    [System.Serializable]
    public struct GameDataKeys
    {
        public int Level;
        public int Gold;
    }
}
