using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class GlobalDirector : MonoBehaviour
{
    public static GlobalDirector Shared { get; private set; }
    public Dictionary<string, GameObject> GameObjectsStash { get; } = new();
    private Dictionary<string, bool> _gameKeys = new();

    public Dictionary<CharacterScriptableObject, float> health = new();
    public List<Identifiable> maps;
    public GameObject dialogHUD;
    public AudioSource backgroundAudioSource;
    public Volume glitchEffect;
    public UniversalRenderPipelineAsset renderPipelineGlitchAsset;
    public UniversalRenderPipelineAsset renderPipeline2DLightAsset;
    
    public Identifiable currentMap;
    public string lastMapId;

    public static string InitialMapId = "Initial";
    
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
        SetGlitchEffectWeight(0);
        
        UIDialogMessage.SetBackstageColor(Color.clear);
        UIDialogMessage.SetBackstageImage(null);
        
        Shared.lastMapId = Shared.currentMap?.objectId ?? InitialMapId;
        
        if (Shared.currentMap != null)
        {
            Destroy(Shared.currentMap.gameObject);
            PlayerInputScript.Shared.AllCharacters.Clear();
        }

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

    public static float GetHealth(CharacterScriptableObject character)
    {
        return Shared.health[character];
    }

    public static void SetHealth(CharacterScriptableObject character, float health)
    {
        Shared.health[character] = health;
    }

    public static void SetGlitchEffectWeight(float weight)
    {
        Shared.glitchEffect.weight = weight;
        GraphicsSettings.renderPipelineAsset = weight != 0 ? Shared.renderPipelineGlitchAsset : Shared.renderPipeline2DLightAsset;
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
        PlayBackgroundAudio(audio, true);
    }

    public static void PlayBackgroundAudio(AudioResource audio, bool loop)
    {
        var source = Shared.backgroundAudioSource;
        if (source.isPlaying)
            source.Pause();

        source.resource = audio;
        source.loop = loop;
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
    
    public static Color WithAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
}
