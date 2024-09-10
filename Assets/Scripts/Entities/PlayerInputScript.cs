using System;
using System.Collections;
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

    private bool _canToggleFollowing = false;
    public bool CanToggleFollowing
    {
        get => _canToggleFollowing;
        set
        {
            _canToggleFollowing = value;
            if (_canToggleFollowing && _controlMap.Player.Move.enabled)
                _controlMap.Player.ToggleFollowing.Enable();
            else 
                _controlMap.Player.ToggleFollowing.Disable();
        }
    }
    
    public readonly List<CharacterScript> AllCharacters = new();
    private PlayerControlMap _controlMap;

    public static List<CharacterScriptableObject> CharactersInParty = new();
    public static List<CharacterScriptableObject> CharactersInFolowers = new();
    
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
        _controlMap.Player.Interact.Enable();
        
        _controlMap.Player.Move.Enable();
        _controlMap.Player.Run.Enable();
        
        if (_canToggleFollowing)
            _controlMap.Player.ToggleFollowing.Enable();
    }

    public void DisablePlayerInput()
    {
        _controlMap.Player.PrevCharacter.Disable();
        _controlMap.Player.NextCharacter.Disable();
        _controlMap.Player.Interact.Disable();
        
        _controlMap.Player.Move.Disable();
        _controlMap.Player.Run.Disable();
        
        _controlMap.Player.ToggleFollowing.Disable();
    }

    public static void SpawnAllCharacters(Vector2 atPoint)
    {
        SetFollowing(true);
        
        foreach (var character in Shared.AllCharacters)
            Destroy(character.gameObject);
        Shared.AllCharacters.Clear();
        
        foreach (var character in CharactersInParty)
        {
            var characterObject = Instantiate(Shared.characterPrefab, atPoint, Quaternion.identity, GlobalDirector.Shared.currentMap.transform);
            characterObject.characterModel = character;
            characterObject.transform.localScale = new Vector3(character.size, character.size, 1);
            characterObject.playable = true;
            InternalAddCharacter(characterObject);
        }
        
        foreach (var character in CharactersInFolowers)
        {
            var characterObject = Instantiate(Shared.characterPrefab, atPoint, Quaternion.identity, GlobalDirector.Shared.currentMap.transform);
            characterObject.characterModel = character;
            characterObject.transform.localScale = new Vector3(character.size, character.size, 1);
            characterObject.playable = false;
            InternalAddCharacter(characterObject);
        }
        
        Shared.UpdateCharacters();

        var cameraPos = Camera.main.transform.position;
        cameraPos.x = atPoint.x;
        cameraPos.y = atPoint.y;
        Camera.main.transform.position = cameraPos;
    }

    public static void SpawnAllCharacters(SpawnPoint spawnPoint)
    {
        SpawnAllCharacters(spawnPoint.transform.position);
        
        foreach (var character in Shared.AllCharacters)
            character.SetDirection(spawnPoint.direction);
    }

    public static void SpawnCharacters(List<CharacterScriptableObject> characters, Vector2 atPoint)
    {
        SpawnCharacters(characters, atPoint, true);
    }
    
    public static void SpawnCharacters(List<CharacterScriptableObject> characters, Vector2 atPoint, bool playable)
    {
        SetFollowing(true);
        
        foreach (var character in Shared.AllCharacters)
            Destroy(character.gameObject);
        Shared.AllCharacters.Clear();
        
        foreach (var character in characters)
        {
            var characterObject = Instantiate(Shared.characterPrefab, atPoint, Quaternion.identity, GlobalDirector.Shared.currentMap.transform);
            characterObject.characterModel = character;
            characterObject.transform.localScale = new Vector3(character.size, character.size, 1);
            characterObject.playable = playable;
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

    public static void AddCharacter(CharacterScriptableObject character, Vector2 atPoint, CharacterScript.Direction direction, bool playable = true)
    {
        if (playable) 
            CharactersInParty.Add(character);
        else
            CharactersInFolowers.Add(character);
        
        var characterObject = Instantiate(Shared.characterPrefab, atPoint, Quaternion.identity, GlobalDirector.Shared.currentMap.transform);
        characterObject.characterModel = character;
        characterObject.transform.localScale = new Vector3(character.size, character.size, 1);
        characterObject.playable = playable;
        characterObject.SetDirection(direction);
        InternalAddCharacter(characterObject);
            
        Shared.UpdateCharacters();
    }

    public static void AddExistingCharacter(CharacterScript character, bool playable = true)
    {
        if (playable) 
            CharactersInParty.Add(character.characterModel);
        else
            CharactersInFolowers.Add(character.characterModel);
        
        character.playable = playable;
        InternalAddCharacter(character);
            
        Shared.UpdateCharacters();
    }

    public static IEnumerator MoveDeltaCoroutine(Vector2 point)
    {
        return MoveDeltaCoroutine(point, false);
    }

    public static IEnumerator MoveDeltaCoroutine(Vector2 point, bool forceWalking)
    {
        Vector3 point3 = point;
        var targetPos = Shared.ActiveCharacter.transform.position + point3;
        var target = Instantiate(new GameObject(), targetPos, Quaternion.identity);
        yield return MoveCoroutine(target, forceWalking);
        Destroy(target);
    }

    public static IEnumerator MoveToPointCoroutine(Vector2 point)
    {
        return MoveToPointCoroutine(point, false);
    }

    public static IEnumerator MoveToPointCoroutine(Vector2 point, bool forceWalking)
    {
        var target = new GameObject(); 
        target.transform.position = point;
        yield return MoveCoroutine(target, forceWalking);
        Destroy(target);
    }
    
    public static IEnumerator MoveCoroutine(GameObject target)
    {
        return MoveCharCoroutine(Shared.ActiveCharacter, target, false);
    }
    
    public static IEnumerator MoveCoroutine(GameObject target, bool forceWalking)
    {
        return MoveCharCoroutine(Shared.ActiveCharacter, target, forceWalking);
    }

    public static IEnumerator MoveCharToPointCoroutine(CharacterScript character, Vector2 point)
    {
        return MoveCharToPointCoroutine(character, point, false);
    }

    public static IEnumerator MoveCharToPointCoroutine(CharacterScript character, Vector2 point, bool forceWalking)
    {
        var target = new GameObject(); 
        target.transform.position = point;
        yield return MoveCharCoroutine(character, target, forceWalking);
        Destroy(target);
    }

    public static IEnumerator MoveCharCoroutine(CharacterScript character, GameObject target)
    {
        return MoveCharCoroutine(character, target, false);
    }

    public static IEnumerator MoveCharCoroutine(CharacterScript character, GameObject target, bool forceWalking)
    {
        var isActiveCharacter = Shared.ActiveCharacter == character;
        if (isActiveCharacter)
            Shared.DisablePlayerInput();
            
        var ai = character.GetComponent<FollowerAIScript>();
        ai.overrideFollowTarget = target;
        ai.needToOverrideFollowTarget = true;
        ai.AIEnabled = true;
        
        var prevValue = ai.forceWalking;
        ai.forceWalking = forceWalking;

        var playerTransform = character.transform;
        var targetTransform = target.transform;

        var startTime = Time.time;
        while (Time.time - startTime < 10)
        {
            yield return new WaitForFixedUpdate();
            if (Vector2.Distance(playerTransform.position, targetTransform.position) <= 2)
                break;
        }
        
        ai.AIEnabled = false;
        ai.needToOverrideFollowTarget = false;
        ai.overrideFollowTarget = null;
        ai.forceWalking = prevValue;
        
        if (isActiveCharacter)
            Shared.EnablePlayerInput();
    }

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

        var ai = ActiveCharacter.GetComponent<FollowerAIScript>();
        if (ai.AIEnabled) return;
        
        var run = IsRunning ? 2 : 1;
        var movementDirection = _controlMap.Player.Move.ReadValue<Vector2>();
        ActiveCharacter.MoveByVector(movementDirection, speed * run);
    }

    private void SetPrevCharacter(InputAction.CallbackContext ctx)
    {
        ActiveCharacter.MoveByVector(Vector2.zero, speed);
        do
        {
            activeCharacterIndex -= 1;
            if (activeCharacterIndex < 0) activeCharacterIndex = AllCharacters.Count - 1;
        } while (!ActiveCharacter.playable);

        UpdateCharacters();
    }

    private void SetNextCharacter(InputAction.CallbackContext ctx)
    {
        ActiveCharacter.MoveByVector(Vector2.zero, speed);
        do
        {
            activeCharacterIndex += 1;
            activeCharacterIndex %= AllCharacters.Count;
        } while (!ActiveCharacter.playable);
        
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

    public static CharacterScript GetInGameCharacter(CharacterScriptableObject characterModel)
    {
        return Shared.AllCharacters.Find(character => character.characterModel == characterModel);
    }

    public CharacterScript ActiveCharacter => AllCharacters[activeCharacterIndex];
    private bool IsRunning => Math.Abs(_controlMap.Player.Run.ReadValue<float>() - 1) < 0.1f;
}
