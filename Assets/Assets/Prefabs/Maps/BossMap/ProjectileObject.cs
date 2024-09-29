using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileObject : MonoBehaviour
{
    public float speed = 3;
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
        _rigidbody2D.velocity = transform.rotation * Vector3.up * speed;
    }
    
    IEnumerator DestroyByTimer()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<CharacterScript>();
        if (player == null) return;

        if (GlobalDirector.Shared.health.ContainsKey(player.characterModel))
            GlobalDirector.Shared.health[player.characterModel] -= 5;
    }
}
