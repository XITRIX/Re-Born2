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
    
    private readonly List<CharacterScript> _allCharacters = new();
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
        _controlMap.Player.PrevCharacter.Enable();
        
        _controlMap.Player.NextCharacter.started += SetNextCharacter;
        _controlMap.Player.NextCharacter.Enable();
        
        _controlMap.Player.ToggleFollowing.started += ToggleFollowing;
        _controlMap.Player.ToggleFollowing.Enable();
        
        _controlMap.Player.Interact.started += PerformInteraction;
        _controlMap.Player.Interact.Enable();
        
        _controlMap.Player.Move.Enable();
        _controlMap.Player.Run.Enable();
    }

    public static void SpawnCharacters(List<CharacterScriptableObject> characters, Vector2 atPoint)
    {
        foreach (var character in characters)
        {
            var characterObject = Instantiate(Shared.characterPrefab, atPoint, Quaternion.identity);
            characterObject.characterModel = character;
            InternalAddCharacter(characterObject);
        }
        Shared.UpdateCharacters();
    }

    public static void AddCharacter(CharacterScript character)
    {
        InternalAddCharacter(character);
        Shared.UpdateCharacters();
    }

    private static void InternalAddCharacter(CharacterScript character)
    {
        Shared._allCharacters.Add(character);
        for (var i = 0; i < Shared._allCharacters.Count; i++)
            Shared._allCharacters[(i + 1) % Shared._allCharacters.Count].GetComponent<FollowerAIScript>().followTarget = Shared._allCharacters[i].gameObject;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var run = IsRunning ? 2 : 1;
        var movementDirection = _controlMap.Player.Move.ReadValue<Vector2>();
        ActiveCharacter.MoveByVector(movementDirection, speed * run);
    }

    private void SetPrevCharacter(InputAction.CallbackContext ctx)
    {
        ActiveCharacter.MoveByVector(Vector2.zero, speed);
        activeCharacterIndex -= 1;
        if (activeCharacterIndex < 0) activeCharacterIndex = _allCharacters.Count - 1;
        UpdateCharacters();
    }

    private void SetNextCharacter(InputAction.CallbackContext ctx)
    {
        ActiveCharacter.MoveByVector(Vector2.zero, speed);
        activeCharacterIndex += 1;
        activeCharacterIndex %= _allCharacters.Count;
        UpdateCharacters();
    }

    private void ToggleFollowing(InputAction.CallbackContext ctx)
    {
        enableFollowing = !enableFollowing;
        UpdateCharacters();
        
        foreach (var character in _allCharacters)
            character.MoveByVector(Vector2.zero, speed);
    }
    
    private void PerformInteraction(InputAction.CallbackContext ctx)
    {
        var objectsToInteract = _allCharacters[activeCharacterIndex].objectsToInteract;
        if (objectsToInteract.Count <= 0) return;

        var obj = objectsToInteract.LastOrDefault();
        if (obj == null || obj.InteractionScenario == null) return;
        
        obj.InteractionScenario();
        
        Debug.Log($"Interact with {obj.objectId}");
    }

    private void UpdateCharacters()
    {
        for (var i = 0; i < _allCharacters.Count; i++)
        {
            var enableAI = enableFollowing && i != activeCharacterIndex;
            _allCharacters[i].GetComponent<FollowerAIScript>().AIEnabled = enableAI;

            var layer = (_allCharacters.Count - 1 - i + activeCharacterIndex) % _allCharacters.Count;
            _allCharacters[i].GetComponent<SpriteRenderer>().sortingOrder = layer;
        }

        CameraScript.Shared.followedObject = _allCharacters[activeCharacterIndex].gameObject;
    }

    private CharacterScript ActiveCharacter => _allCharacters[activeCharacterIndex];
    private bool IsRunning => Math.Abs(_controlMap.Player.Run.ReadValue<float>() - 1) < 0.1f;
}
