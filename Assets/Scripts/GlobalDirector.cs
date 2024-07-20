using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GlobalDirector : MonoBehaviour
{
    public static GlobalDirector Shared { get; private set; }
    public Dictionary<string, GameObject> GameObjectsStash { get; } = new();

    public List<Identifiable> maps;
    public GameObject dialogHUD;

    public Identifiable currentMap;
    public string lastMapId;
    
    public GlobalDirector()
    {
        Shared = this;
    }

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    public static void LoadMap(string map)
    {
        if (Shared.currentMap != null)
        {
            Destroy(Shared.currentMap);
            PlayerInputScript.Shared.allCharacters.Clear();
        }

        Shared.lastMapId = Shared.currentMap?.objectId ?? "Initial";
        Shared.PrepareToLoadMap();
        Shared.currentMap = Instantiate(Shared.maps.First(v => v.objectId == map));
    }

    public static string GetLastMapId() => Shared.lastMapId;
    
    public static GameObject GetEntityById(string id)
    {
        return Shared.GameObjectsStash.TryGetValue(id, out var value) ? value : null;
    }

    public static void ShowDialog()
    {
        // Shared.dialogHUD.SetActive(true);
        PlayerInputScript.Shared.DisablePlayerInput();
    }
    
    public static void CloseDialog()
    {
        // Shared.dialogHUD.SetActive(false);
        PlayerInputScript.Shared.EnablePlayerInput();
    }
    
    private void PrepareToLoadMap()
    {
        foreach (var keyValuePair in GameObjectsStash)
        {
            Destroy(keyValuePair.Value); 
        }
        GameObjectsStash.Clear();

        currentMap = null;
    }
}
