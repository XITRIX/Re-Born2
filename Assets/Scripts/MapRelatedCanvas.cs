using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class MapRelatedCanvas : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Canvas>().worldCamera = GlobalDirector.Shared.uiOverlayCamera;
    }
}
