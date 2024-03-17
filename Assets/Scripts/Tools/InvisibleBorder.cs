using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InvisibleBorder : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}