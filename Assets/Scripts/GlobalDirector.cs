using System.Collections.Generic;
using UnityEngine;

public class GlobalDirector : MonoBehaviour
{
    public static GlobalDirector Shared { get; private set; }
    public Dictionary<string, GameObject> GameObjectsStash { get; } = new();
    
    public GlobalDirector()
    {
        Shared = this;
    }
}
