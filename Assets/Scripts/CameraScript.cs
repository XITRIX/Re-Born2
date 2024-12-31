using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour
{
    public static CameraScript Shared { get; private set; }

    public GameObject followedObject;
    public GameObject overrideFollowedObject;
    public bool followOverridenObject;
    public float cameraSpeed = 1;

    public bool useSidePerspective = true;
    public float sidePerspectiveYOffset = 10;
    public float sidePerspectiveAngle = 9;

    private Transform Transform { get; set; }
    public PixelPerfectCamera ppCamera; 

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
        _UpdateCameraPosition();
    }

    private void _UpdateCameraPosition(bool force = false)
    {
        var objectToFollow = followOverridenObject ? overrideFollowedObject : followedObject;
        if (!objectToFollow) return;
        
        var position = Transform.position;
        
        var followPosition = objectToFollow.transform.position;
        if (useSidePerspective)
            followPosition.y -= sidePerspectiveYOffset;

        var target = force ? 
            followPosition : 
            Vector3.Lerp(transform.position, followPosition, Time.deltaTime * cameraSpeed);
        
        target.z = position.z;
        
        Transform.position = target;

        var rotation = transform.rotation;
        var rotationAngle = rotation.eulerAngles;
        rotationAngle.x = useSidePerspective ? -sidePerspectiveAngle : 0;
        rotation.eulerAngles = rotationAngle;
        transform.rotation = rotation;

        // Transform.position = position;
    }

    public static void UpdateCameraPosition(bool force = false)
    {
        Shared._UpdateCameraPosition(force);
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

    public static void SetAssetsPpu(int ppu)
    {
        Shared.GetComponent<PixelPerfectCamera>().assetsPPU = ppu;
    }
}