using UnityEngine;

public class PoliceCarLightEmitter : MonoBehaviour
{
    public float animationSpeed = 2;
    public float maxLightIntencity = 80;
    
    public Light blueLight;
    public Light redLight;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        blueLight.intensity = Mathf.Abs(Mathf.Sin(Time.time * animationSpeed)) * maxLightIntencity;
        redLight.intensity = Mathf.Abs(Mathf.Cos(Time.time * animationSpeed)) * maxLightIntencity;
    }
}
