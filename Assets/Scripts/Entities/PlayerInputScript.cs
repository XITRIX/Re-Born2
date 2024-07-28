using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    public static PlayerInputScript Shared { get; private set; }

    public CharacterScript characterPrefab;
    public float speed = 3f;
    public int activeCharacterIndex;
    public bool enableFollowing = true;
    
    public readonly List<CharacterScript> AllCharacters = new();
    private PlayerControlMap _controlMap;
    
    public PlayerInputScript()
    {
        Shared = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        _controlMap = new PlayerControlMap();
        
        _controlMap.Player.PrevCharacter.started += SetPrevCharacter;
        _controlMap.Player.NextCharacter.started += SetNextCharacter;
        _controlMap.Player.ToggleFollowing.started += ToggleFollowing;
        _controlMap.Player.Interact.started += PerformInteraction;
        
        EnablePlayerInput();
    }

    public static void SetPlayerInputEnabled(bool enabled)
    {
        if (enabled) Shared.EnablePlayerInput();
        else Shared.DisablePlayerInput();
    }

    public void EnablePlayerInput()
    {
        _controlMap.Player.PrevCharacter.Enable();
        _controlMap.Player.NextCharacter.Enable();
        _controlMap.Player.ToggleFollowing.Enable();
        _controlMap.Player.Interact.Enable();
        
        _controlMap.Player.Move.Enable();
        _controlMap.Player.Run.Enable();
    }

    public void DisablePlayerInput()
    {
        _controlMap.Player.PrevCharacter.Disable();
        _controlMap.Player.NextCharacter.Disable();
        _controlMap.Player.ToggleFollowing.Disable();
        _controlMap.Player.Interact.Disable();
        
        _controlMap.Player.Move.Disable();
        _controlMap.Player.Run.Disable();
    }

    public static void SpawnCharacters(List<CharacterScriptableObject> characters, Vector2 atPoint)
    {
        SetFollowing(true);
        
        foreach (var character in Shared.AllCharacters)
            Destroy(character);
        Shared.AllCharacters.Clear();
        
        foreach (var character in characters)
        {
            var characterObject = Instantiate(Shared.characterPrefab, atPoint, Quaternion.identity, GlobalDirector.Shared.currentMap.transform);
            characterObject.characterModel = character;
            InternalAddCharacter(characterObject);
        }
        Shared.UpdateCharacters();
    }

    public static void SetFollowing(bool following)
    {
        Shared.enableFollowing = following;
        Shared.UpdateCharacters();
        
        foreach (var character in Shared.AllCharacters)
            character.MoveByVector(Vector2.zero, Shared.speed);
    }

    // public static void AddCharacter(CharacterScript character)
    // {
    //     InternalAddCharacter(character);
    //     Shared.UpdateCharacters();
    // }

    private static void InternalAddCharacter(CharacterScript character)
    {
        Shared.AllCharacters.Add(character);
        for (var i = 0; i < Shared.AllCharacters.Count; i++)
            Shared.AllCharacters[(i + 1) % Shared.AllCharacters.Count].GetComponent<FollowerAIScript>().followTarget = Shared.AllCharacters[i].gameObject;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (AllCharacters.Count == 0) return;
        
        var run = IsRunning ? 2 : 1;
        var movementDirection = _controlMap.Player.Move.ReadValue<Vector2>();
        ActiveCharacter.MoveByVector(movementDirection, speed * run);
    }

    private void SetPrevCharacter(InputAction.CallbackContext ctx)
    {
        ActiveCharacter.MoveByVector(Vector2.zero, speed);
        activeCharacterIndex -= 1;
        if (activeCharacterIndex < 0) activeCharacterIndex = AllCharacters.Count - 1;
        UpdateCharacters();
    }

    private void SetNextCharacter(InputAction.CallbackContext ctx)
    {
        ActiveCharacter.MoveByVector(Vector2.zero, speed);
        activeCharacterIndex += 1;
        activeCharacterIndex %= AllCharacters.Count;
        UpdateCharacters();
    }

    private void ToggleFollowing(InputAction.CallbackContext ctx)
    {
        SetFollowing(!enableFollowing);
    }
    
    private void PerformInteraction(InputAction.CallbackContext ctx)
    {
        var objectsToInteract = AllCharacters[activeCharacterIndex].objectsToInteract;
        if (objectsToInteract.Count <= 0) return;

        var obj = objectsToInteract.LastOrDefault();
        if (obj == null || obj.InteractionScenario == null) return;
        
        obj.InteractionScenario();
        
        Debug.Log($"Interact with {obj.objectId}");
    }

    private void UpdateCharacters()
    {
        if (AllCharacters.Count == 0) return;
        
        for (var i = 0; i < AllCharacters.Count; i++)
        {
            var enableAI = enableFollowing && i != activeCharacterIndex;
            AllCharacters[i].GetComponent<FollowerAIScript>().AIEnabled = enableAI;

            var layer = (AllCharacters.Count - 1 - i + activeCharacterIndex) % AllCharacters.Count;
            AllCharacters[i].GetComponent<SpriteRenderer>().sortingOrder = layer;
        }

        CameraScript.Shared.followedObject = AllCharacters[activeCharacterIndex].gameObject;
    }

    private CharacterScript ActiveCharacter => AllCharacters[activeCharacterIndex];
    private bool IsRunning => Math.Abs(_controlMap.Player.Run.ReadValue<float>() - 1) < 0.1f;
}
