using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileObject : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyByTimer());
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = Vector2.up;
    }
    
    IEnumerator DestroyByTimer()
    {
        yield return new WaitForSeconds(3);
    }
}
