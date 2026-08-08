using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act6MapScript : MonoBehaviour
{
    public List<GameObject> investigateObjects = new(); 
    public List<GameObject> hideObjects = new();

    public GameObject roomDoor;

    public GameObject roomLight;
    public List<GameObject> redLights = new();

    public GameObject wardrobeLDore;
    public GameObject wardrobeRDore;
    public BoxCollider wardrobeCollider;

    private static Act6MapScript _singleton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _singleton = this;
    }

    public static void DisableInvestigateObjects()
    {
        foreach (var investigateObject in _singleton.investigateObjects)
        {
            investigateObject.SetActive(false);
        }
    }

    public static void SetEnableRoomLight(bool enable)
    {
        _singleton.roomLight.SetActive(enable);
        foreach (var redLight in _singleton.redLights)
        {
            redLight.SetActive(!enable);
        }
    }

    public static void SetWardrobeOpen(bool open)
    {
        _singleton.wardrobeLDore.transform.localRotation = Quaternion.Euler(0f, 0f, open ? 100f : 0f);
        _singleton.wardrobeRDore.transform.localRotation = Quaternion.Euler(0f, 0f, open ? -100f : 0f);
    }

    public static void SetWardrobeColliderEnabled(bool enable)
    {
        _singleton.wardrobeCollider.gameObject.SetActive(enable);
    }

    public static IEnumerator SetRoomDoorOpen(bool open)
    {
        Transform doorTransform = _singleton.roomDoor.transform;
        float targetZ = open ? 1f : 0f;

        while (!Mathf.Approximately(doorTransform.localPosition.z, targetZ))
        {
            Vector3 position = doorTransform.localPosition;
            position.z = Mathf.MoveTowards(position.z, targetZ, Time.deltaTime);
            doorTransform.localPosition = position;
            yield return null;
        }
    }
}
