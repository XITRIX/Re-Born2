using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour
{
    public static CameraScript Shared { get; private set; }

    public GameObject followedObject;
    public GameObject overrideFollowedObject;
    public bool followOverridenObject;
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
        var objectToFollow = followOverridenObject ? overrideFollowedObject : followedObject;
        if (!objectToFollow) return;
        
        var position = Transform.position;

        var target = Vector3.Lerp(transform.position, objectToFollow.transform.position, Time.deltaTime * cameraSpeed);
        target.z = position.z;
        
        Transform.position = target;

        // Transform.position = position;
    }

    public static void StartFollowingObject(GameObject obj)
    {
        Shared.overrideFollowedObject = obj;
        Shared.followOverridenObject = true;
    }
    
    public static void StopFollowingObject()
    {
        Shared.followOverridenObject = false;
        Shared.overrideFollowedObject = null;
    }
}