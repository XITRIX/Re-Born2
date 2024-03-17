using System;
using UnityEngine;

public class Identifiable : MonoBehaviour
{
    public string objectId;

    public virtual void Awake()
    {
        if (string.IsNullOrEmpty(objectId)) return;
        GlobalDirector.Shared.GameObjectsStash[objectId] = gameObject;
    }
    
    public virtual void OnDestroy()
    {
        if (string.IsNullOrEmpty(objectId) || 
            !GlobalDirector.Shared.GameObjectsStash.ContainsKey(objectId) ||
            GlobalDirector.Shared.GameObjectsStash[objectId] != gameObject) return;
        
        GlobalDirector.Shared.GameObjectsStash[objectId] = null;
        Debug.Log($"{objectId} destroyed");
    }
}
