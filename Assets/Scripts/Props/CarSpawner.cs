using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public List<GameObject> carPrefabs;
    public float timeBetweenSpawns = 5;
    public float timeVariation = 1;

    public float carSpeed = 12;

    private float _nextSpawnTime;
    // Update is called once per frame
    void Update()
    {
        if (Time.time < _nextSpawnTime) return;
        _nextSpawnTime = Time.time + timeBetweenSpawns + Random.Range(0, timeVariation);
        
        var car = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Count)], transform.parent);
        car.transform.position = transform.position;
        car.transform.rotation = transform.rotation;

        var carMovement = car.AddComponent<CarMovementScript>();
        carMovement.speed = carSpeed;
    }
}
