using UnityEngine;

public class SpriteMimicCameraAngle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main == null) return;
        
        var rotation = transform.rotation;
        var rotationAngle = rotation.eulerAngles;
        rotationAngle.x = Camera.main.transform.rotation.eulerAngles.x;
        rotation.eulerAngles = rotationAngle;
        transform.rotation = rotation;
    }
}
