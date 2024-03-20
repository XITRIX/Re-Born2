using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : Identifiable
{
    [CanBeNull]
    public Action InteractionScenario => () =>
    {
        EventBus.Trigger("InteractionEvent", objectId);
    };
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        var entity = col.GetComponent<CharacterScript>();
        if (!entity || string.IsNullOrEmpty(objectId)) { return; }
        
        // Debug.Log($"Enter: {entity.objectId}");

        entity.objectsToInteract.Add(this);
        EventBus.Trigger("InteractionTriggerEnterUnit", objectId);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        var entity = col.GetComponent<CharacterScript>();
        if (!entity || string.IsNullOrEmpty(objectId)) { return; }
        
        // Debug.Log($"Exit: {entity.objectId}");
        
        entity.objectsToInteract.Remove(this);
        EventBus.Trigger("InteractionTriggerExitUnit", objectId);
    }
}
