using System.Collections;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ShootEveryTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        Instantiate(projectilePrefab, transform.position, transform.rotation);
    }

    IEnumerator ShootEveryTime()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(1);
            Shoot();
        }
    }
}
