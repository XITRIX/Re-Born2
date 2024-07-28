using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class GlobalDirector : MonoBehaviour
{
    public static GlobalDirector Shared { get; private set; }
    public Dictionary<string, GameObject> GameObjectsStash { get; } = new();
    private Dictionary<string, bool> _gameKeys = new();

    public List<Identifiable> maps;
    public GameObject dialogHUD;
    public AudioSource backgroundAudioSource;

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
        UIDialogMessage.SetBackstageColor(Color.clear);
        UIDialogMessage.SetBackstageImage(null);
        
        if (Shared.currentMap != null)
        {
            Destroy(Shared.currentMap);
            PlayerInputScript.Shared.AllCharacters.Clear();
        }

        Shared.lastMapId = Shared.currentMap?.objectId ?? "Initial";
        Shared.PrepareToLoadMap();
        Shared.currentMap = Instantiate(Shared.maps.First(v => v.objectId == map));
    }

    public static bool GetGameKey(string key)
    {
        return Shared._gameKeys.ContainsKey(key) && Shared._gameKeys[key];
    }

    public static void SetGameKey(string key, bool value)
    {
        Shared._gameKeys[key] = value;
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

    public static void PlayBackgroundAudio(AudioResource audio)
    {
        var source = Shared.backgroundAudioSource;
        if (source.isPlaying)
            source.Pause();

        source.resource = audio;
        source.Play();
    }

    public static void PauseBackgroundAudio()
    {
        var source = Shared.backgroundAudioSource;
        if (source.isPlaying)
            source.Pause();
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
