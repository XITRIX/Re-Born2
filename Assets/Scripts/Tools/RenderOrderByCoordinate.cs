using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RenderOrderByCoordinate : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _spriteRenderer.sortingOrder = (int) (-transform.position.y * 10);
    }
}
