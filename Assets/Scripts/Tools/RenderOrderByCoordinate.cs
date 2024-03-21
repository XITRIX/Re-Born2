using UnityEngine;
using UnityEngine.Serialization;

public class RenderOrderByCoordinate : MonoBehaviour
{
    public bool isStatic;
    public SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        
        UpdateOrder();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isStatic) return;
        UpdateOrder();
    }

    private void UpdateOrder()
    {
        spriteRenderer.sortingOrder = (int) (-transform.position.y * 10);
    }
}
