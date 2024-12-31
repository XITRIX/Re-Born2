using UnityEngine;

public class CarMovementScript : MonoBehaviour
{
    public float speed = 2;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        position += transform.forward * speed * Time.deltaTime;
        transform.position = position;
    }
}
