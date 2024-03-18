using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    public float speed = 3f;
    public List<CharacterScript> allCharacters;
    public int activeCharacterIndex;
    public bool enableFollowing = true;
    
    private PlayerControlMap _controlMap;
    
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
        
        _controlMap.Player.Move.Enable();
        _controlMap.Player.Run.Enable();

        for (var i = 0; i < allCharacters.Count; i++)
            allCharacters[(i + 1) % allCharacters.Count].GetComponent<FollowerAIScript>().followTarget = allCharacters[i].gameObject;
        UpdateCharacters();
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
        if (activeCharacterIndex < 0) activeCharacterIndex = allCharacters.Count - 1;
        UpdateCharacters();
    }

    private void SetNextCharacter(InputAction.CallbackContext ctx)
    {
        ActiveCharacter.MoveByVector(Vector2.zero, speed);
        activeCharacterIndex += 1;
        activeCharacterIndex %= allCharacters.Count;
        UpdateCharacters();
    }

    private void ToggleFollowing(InputAction.CallbackContext ctx)
    {
        enableFollowing = !enableFollowing;
        UpdateCharacters();
        
        foreach (var character in allCharacters)
            character.MoveByVector(Vector2.zero, speed);
    }

    private void UpdateCharacters()
    {
        for (var i = 0; i < allCharacters.Count; i++)
        {
            var enableAI = enableFollowing && i != activeCharacterIndex;
            allCharacters[i].GetComponent<NavMeshAgent>().enabled = enableAI;
            // allCharacters[i].GetComponent<AITest>().AllowToMove = enableAI;
            allCharacters[i].GetComponent<BoxCollider2D>().isTrigger = enableAI;

            var layer = (allCharacters.Count - 1 - i + activeCharacterIndex) % allCharacters.Count;
            allCharacters[i].GetComponent<SpriteRenderer>().sortingOrder = layer;
        }

        CameraScript.Shared.followedObject = allCharacters[activeCharacterIndex].gameObject;
    }

    private CharacterScript ActiveCharacter => allCharacters[activeCharacterIndex];
    private bool IsRunning => Math.Abs(_controlMap.Player.Run.ReadValue<float>() - 1) < 0.1f;
}
