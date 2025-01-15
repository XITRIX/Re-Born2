using UnityEngine;

public class UIACatRotation : MonoBehaviour
{
    public GameObject staticCat;
    public GameObject rotatingCat;
    public float rotationSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotationSpeed == 0)
        {
            rotatingCat.SetActive(false);
            staticCat.SetActive(true);
        }
        else
        {
            rotatingCat.SetActive(true);
            staticCat.SetActive(false);
        }
        
        transform.Rotate(Vector3.down, rotationSpeed, Space.Self);
    }
}
