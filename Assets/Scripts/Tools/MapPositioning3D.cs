using UnityEngine;

public class MapPositioning3D : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        var rotation = transform.rotation;
        rotation.eulerAngles = new Vector3(-90, 0, 0);
        transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
