using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlobalDirector : MonoBehaviour
{
    public static GlobalDirector Shared { get; private set; }
    public Dictionary<string, GameObject> GameObjectsStash { get; } = new();

    public List<Identifiable> maps;
    
    public GlobalDirector()
    {
        Shared = this;
    }

    public static void LoadMap(string map)
    {
        Shared._lastMapId = Shared._currentMap?.objectId ?? "Initial";
        Shared.PrepareToLoadMap();
        Shared._currentMap = Instantiate(Shared.maps.First(v => v.objectId == map));
    }

    public static string GetLastMapId() => Shared._lastMapId;
    
    public static GameObject GetEntityById(string id)
    {
        return Shared.GameObjectsStash.TryGetValue(id, out var value) ? value : null;
    }
    
    private void PrepareToLoadMap()
    {
        foreach (var keyValuePair in GameObjectsStash)
        {
            Destroy(keyValuePair.Value); 
        }
        GameObjectsStash.Clear();

        _currentMap = null;
    }

    private Identifiable _currentMap;
    private string _lastMapId;
}
