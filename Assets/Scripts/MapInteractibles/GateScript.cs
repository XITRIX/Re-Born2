using UnityEngine;

public class GateScript : Identifiable
{
    public GameObject closedPrefab;
    public GameObject openedPrefab;

    public bool IsOpen
    {
        get => !closedPrefab.activeSelf;
        set
        {
            closedPrefab.SetActive(!value);
            if (openedPrefab != null)
                openedPrefab.SetActive(value);
        }
    }
}
