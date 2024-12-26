using System.Collections;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float delay = 1;

    private float _lastShotTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (Time.time - _lastShotTime < delay) return;
        Instantiate(projectilePrefab, transform.position, transform.rotation);
        _lastShotTime = Time.time;
    }
}
