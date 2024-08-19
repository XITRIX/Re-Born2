using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/Character")]
[System.Serializable]
public class CharacterScriptableObject : ScriptableObject
{
    public string charName;
    public Sprite avatar;
    public List<Sprite> tileset;
    public float size = 1;
}
