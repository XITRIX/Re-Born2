using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour
{
    public static CameraScript Shared { get; private set; }

    public GameObject followedObject;
    public float cameraSpeed = 1;

    private Transform Transform { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        Shared = this;
        Debug.Log("Camera created");
    }

    private void Start()
    {
        Transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        if (!followedObject) return;
        
        var position = Transform.position;

        var target = Vector3.Lerp(transform.position, followedObject.transform.position, Time.deltaTime * cameraSpeed);
        target.z = position.z;
        
        Transform.position = target;

        // Transform.position = position;
    }
}